import * as React from "react";
import * as ReactDOM from "react-dom";
import { Label } from "../../../react_components/l10n";
import { ToolBox } from "../toolbox";
import ToolboxToolReactAdaptor from "../toolboxToolReactAdaptor";
import { Range } from "rc-slider";
import "./signLanguage.less";
import {
    RequiresBloomEnterpriseWrapper,
    enterpriseFeaturesEnabled
} from "../../../react_components/requiresBloomEnterprise";
import { BloomApi } from "../../../utils/bloomApi";
import { HelpLink } from "../../../react_components/helpLink";
import { Link } from "../../../react_components/link";
import { UrlUtils } from "../../../utils/urlUtils";

// The recording process can be in one of these states:
// idle...the initial state, returned to when stopped; top label shows "Start Recording"; stop button and second label hidden
// countdown{3,2,1}...record button has been pressed, top label shows countdown, not recording, stop button shows,
//  "press any key to cancel" shows in bottom label
// recording...top label shows "Recording", stop button shows, recording is happening, bottom label shows "press any key to stop"
// Transitions:
// Start recording button: state idle -> countdown{3}, * -> idle (save video if any)
// Stop button or any key: * -> idle
interface IComponentState {
    recording: boolean;
    countdown: number;
    enabled: boolean;
    stateClass: string; // one of idle, countdown3, countdown2, countdown1, recording
    haveRecording: boolean;
    originalExists: boolean;
    cameraAccess: boolean;
    cameraUnavailable: boolean;
    videoStatistics: {
        duration: string;
        fileSize: string;
        frameSize: string;
        framesPerSecond: string;
        fileFormat: string;
        startSeconds: string;
        endSeconds: string;
    };
}

// incomplete typescript definitions for MediaRecorder and related types.
// Can't find complete ones, so rather than just do without type checking altogether,
// I've made declarations as accurately as I can figure out for the methods we actually use.
interface BlobEvent {
    data: Blob;
}

interface MediaRecorder {
    new (source: MediaStream, options: any);
    start(): void;
    stop(): void;
    ondataavailable: (ev: BlobEvent) => void;
    onstop: () => void;
}

declare var MediaRecorder: {
    prototype: MediaRecorder;
    new (s: MediaStream, options: any): MediaRecorder;
};

const MAX_DURATION: string = "-1.0"; // temporary placeholder for maximum duration
const DEFAULT_START: string = "0.0";

// This react class implements the UI for the sign language (video) toolbox.
// Note: this file is included in toolboxBundle.js because webpack.config says to include all
// tsx files in bookEdit/toolbox.
// The toolbox is included in the list of tools because of the one line of immediately-executed code
// which passes an instance of SignLanguageTool to ToolBox.registerTool().
export class SignLanguageToolControls extends React.Component<
    {},
    IComponentState
> {
    public static kToolID = "signLanguage";
    public readonly state: IComponentState = {
        recording: false,
        countdown: 0,
        enabled: false,
        stateClass: "idle",
        haveRecording: false,
        originalExists: false,
        cameraAccess: true,
        cameraUnavailable: false,
        videoStatistics: {
            duration: "",
            fileSize: "",
            frameSize: "",
            framesPerSecond: "",
            fileFormat: "",
            startSeconds: DEFAULT_START,
            endSeconds: MAX_DURATION
        }
    };
    private videoStream: MediaStream;
    private chunks: Blob[];
    private mediaRecorder: MediaRecorder;
    private timerId: number;
    public render() {
        let videoStats = <div />;
        if (
            // Protects against showing slider and stats when
            // the video info hasn't yet been loaded from the file.
            this.state.haveRecording &&
            this.state.videoStatistics.duration != ""
        ) {
            videoStats = this.getVideoStats();
        }
        return (
            <RequiresBloomEnterpriseWrapper>
                <div className={"signLanguageFrame " + this.state.stateClass}>
                    {this.getCameraMessageLabel()}
                    <div className={this.state.enabled ? "" : "disabled"}>
                        <div
                            id="videoMonitorWrapper"
                            className={
                                this.state.enabled ? "" : "disabledVideoMonitor"
                            }
                        >
                            <video id="videoMonitor" autoPlay={true} />
                        </div>
                        <div className="button-label-wrapper">
                            <div id="videoPlayAndLabelWrapper">
                                <div className="videoButtonWrapper">
                                    <button
                                        id="videoToggleRecording"
                                        className={
                                            "video-button ui-button" +
                                            (this.state.stateClass !== "idle"
                                                ? " started"
                                                : "") +
                                            (this.state.recording
                                                ? " recordingNow"
                                                : "") +
                                            (this.state.enabled &&
                                            this.state.cameraAccess
                                                ? " enabled"
                                                : " disabled")
                                        }
                                        onClick={() => this.toggleRecording()}
                                    />
                                    <Label
                                        className={
                                            "startRecording idle" +
                                            (this.state.enabled &&
                                            this.state.cameraAccess
                                                ? " enabled"
                                                : " disabled")
                                        }
                                        l10nKey="EditTab.Toolbox.SignLanguage.StartRecording"
                                        onClick={() => this.toggleRecording()}
                                    >
                                        Start Recording
                                    </Label>
                                    <span className="countdown3 countdownNumber">
                                        3
                                    </span>
                                    <span className="countdown2 countdownNumber">
                                        2
                                    </span>
                                    <span className="countdown1 countdownNumber">
                                        1
                                    </span>
                                    <Label
                                        className="recording recordingLabel"
                                        l10nKey="EditTab.Toolbox.SignLanguage.Recording"
                                    >
                                        Recording
                                    </Label>
                                </div>
                            </div>
                        </div>
                        <div
                            id="editOutsideWrapper"
                            className={
                                "videoButtonWrapper" +
                                (this.state.haveRecording &&
                                this.state.stateClass === "idle"
                                    ? ""
                                    : " disabled ")
                            }
                        >
                            <button
                                id="editOutsideButton"
                                onClick={() => this.editOutside()}
                            />
                            <Label
                                className="commandLabel"
                                l10nKey="EditTab.Toolbox.SignLanguage.EditOutside"
                                onClick={() => this.editOutside()}
                            >
                                Edit outside of Bloom
                            </Label>
                        </div>
                        <div
                            id="restoreOriginalWrapper"
                            className={
                                "videoButtonWrapper" +
                                (this.state.originalExists ? "" : " disabled ")
                            }
                        >
                            <button
                                id="restoreOriginalButton"
                                onClick={() => this.restoreOriginal()}
                            />
                            <Label
                                className="commandLabel"
                                l10nKey="EditTab.Toolbox.SignLanguage.RestoreOriginal"
                                onClick={() => this.restoreOriginal()}
                            >
                                Restore Original
                            </Label>
                        </div>
                        <div
                            id="importRecordingWrapper"
                            className={
                                "videoButtonWrapper" +
                                (this.state.stateClass === "idle"
                                    ? ""
                                    : " disabled")
                            }
                        >
                            <button
                                id="videoImport"
                                onClick={() => this.importRecording()}
                            />
                            <Label
                                className="commandLabel"
                                l10nKey="EditTab.Toolbox.SignLanguage.ImportVideo"
                                onClick={() => this.importRecording()}
                            >
                                Import Video
                            </Label>
                        </div>
                        <div
                            id="deleteRecordingWrapper"
                            className={
                                "videoButtonWrapper" +
                                (this.state.haveRecording &&
                                this.state.stateClass === "idle"
                                    ? ""
                                    : " disabled ")
                            }
                        >
                            <button
                                id="videoDelete"
                                onClick={() => this.deleteRecording()}
                            >
                                X
                            </button>
                            <Label
                                className="commandLabel"
                                l10nKey="EditTab.Toolbox.SignLanguage.DeleteVideo"
                                onClick={() => this.deleteRecording()}
                            >
                                Delete Video
                            </Label>
                        </div>
                        <Link
                            id="showInFolderLink"
                            l10nKey="EditTab.Toolbox.SignLanguage.ShowInFolder"
                            onClick={() => this.showInFolder()}
                        >
                            Open File Location
                        </Link>
                        <div>
                            <button
                                id="videoStopRecording"
                                className={"video-button ui-button notIdle"}
                                onClick={() => this.toggleRecording()}
                            />
                        </div>
                        <Label
                            l10nKey="EditTab.Toolbox.SignLanguage.PressCancel"
                            className="counting stopLabel"
                        >
                            Press any key to cancel
                        </Label>
                        <Label
                            l10nKey="EditTab.Toolbox.SignLanguage.PressStop"
                            className="recording stopLabel"
                        >
                            Press any key to stop
                        </Label>
                        {videoStats}
                        <div className="helpLinkWrapper">
                            <HelpLink
                                l10nKey="Common.Help"
                                helpId="Tasks/Edit_tasks/Sign_Language_Tool/Sign_Language_Tool_overview.htm"
                            >
                                Help
                            </HelpLink>
                        </div>
                    </div>
                </div>
            </RequiresBloomEnterpriseWrapper>
        );
    }

    private getVideoStats() {
        const maxDuration = SignLanguageTool.convertTimeStringToSecondsNumber(
            this.state.videoStatistics.duration
        );
        const start = parseFloat(this.state.videoStatistics.startSeconds);
        let end = parseFloat(this.state.videoStatistics.endSeconds);
        if (end === -1.0) {
            end = maxDuration;
        }
        const valueArray: number[] = [start, end];
        return (
            <div id="videoStatsWrapper">
                <Label
                    l10nKey="EditTab.Toolbox.SignLanguage.Trim"
                    className="trimLabel"
                >
                    Trim
                </Label>
                <Range
                    className="videoTrimSlider"
                    count={1}
                    value={valueArray}
                    onChange={v => this.setTrimPoints(v[0], v[1])}
                    onAfterChange={
                        // set video back to start point in case we were viewing the end point
                        v => SignLanguageTool.setCurrentVideoPoint(v[0])
                    }
                    step={0.1}
                    min={0.0}
                    allowCross={false}
                    pushable={false}
                    max={maxDuration}
                />
                <div>
                    {/* duration is stored with tenths of seconds, but only displayed with seconds*/
                    this.state.videoStatistics.duration.substr(
                        0,
                        this.state.videoStatistics.duration.length - 2
                    )}
                </div>
                <div>{this.state.videoStatistics.fileSize}</div>
                <div>{this.state.videoStatistics.frameSize}</div>
                <div>{this.state.videoStatistics.framesPerSecond}</div>
                <div>{this.state.videoStatistics.fileFormat}</div>
            </div>
        );
    }

    private setTrimPoints(newStartSeconds: number, newEndSeconds: number) {
        const newStartString = newStartSeconds.toFixed(1);
        const newEndString = newEndSeconds.toFixed(1);
        SignLanguageTool.setVideoTimingsInSrcAttr(newStartString, newEndString);
        const stats = this.state.videoStatistics;
        const oldEnd = stats.endSeconds;
        const oldStart = stats.startSeconds;
        stats.startSeconds = newStartString;
        stats.endSeconds = newEndString;
        if (oldStart !== stats.startSeconds) {
            SignLanguageTool.setCurrentVideoPoint(newStartSeconds); // we're changing the start point, so show it.
        }
        if (oldEnd !== stats.endSeconds) {
            SignLanguageTool.setCurrentVideoPoint(newEndSeconds); // we're changing the end point, so show it.
        }
        this.setState({ videoStatistics: stats });
    }

    public getCameraMessageLabel() {
        if (!this.state.enabled) {
            return (
                <Label
                    key="CameraOn"
                    l10nKey="EditTab.Toolbox.SignLanguage.SelectSignLanguageBox"
                >
                    To use this tool, select a sign language box on the page.
                </Label>
            );
        }
        if (this.state.cameraAccess) {
            return (
                <Label
                    key="CameraOn"
                    l10nKey="EditTab.Toolbox.SignLanguage.WhatCameraSees"
                >
                    Here is what your camera sees:
                </Label>
            );
        } else if (this.state.cameraUnavailable) {
            return (
                <Label
                    key="CameraUnavailable"
                    l10nKey="EditTab.Toolbox.SignLanguage.CameraUnavailable"
                >
                    Camera not available (perhaps in use by another program)
                </Label>
            );
        } else {
            return (
                <Label
                    key="NoCamera"
                    l10nKey="EditTab.Toolbox.SignLanguage.NoCameraFound"
                >
                    No camera found
                </Label>
            );
        }
    }

    public getVideoStatsFromFile() {
        BloomApi.get("signLanguage/getStats", result => {
            if (result.statusText != "OK") {
                this.setState({
                    videoStatistics: {
                        duration: "",
                        fileSize: "",
                        frameSize: "",
                        framesPerSecond: "",
                        fileFormat: "",
                        startSeconds: DEFAULT_START,
                        endSeconds: MAX_DURATION
                    }
                });
            } else {
                this.setState({ videoStatistics: result.data });
            }
        });
    }

    private importRecording() {
        BloomApi.post("signLanguage/importVideo");
    }

    private deleteRecording() {
        BloomApi.post("signLanguage/deleteVideo");
    }

    private showInFolder() {
        const path = SignLanguageTool.getSelectedVideoPath();
        BloomApi.postJson(
            "common/showInFolder",
            JSON.stringify({ folderPath: path })
        );
    }

    private editOutside() {
        BloomApi.post("signLanguage/editVideo");
    }

    private restoreOriginal() {
        BloomApi.post("signLanguage/restoreOriginal");
    }

    public turnOnVideo() {
        enterpriseFeaturesEnabled().then(enabled => {
            const constraints = { video: true };
            if (enabled) {
                navigator.mediaDevices
                    .getUserMedia(constraints)
                    .then(stream => this.startMonitoring(stream))
                    .catch(reason => this.errorCallback(reason));
            }
        });
    }

    public turnOffVideo() {
        if (this.videoStream) {
            const oldStream = this.videoStream;
            this.videoStream = null; // prevent recursive calls
            oldStream.getVideoTracks().forEach(t => t.stop());
            oldStream.getAudioTracks().forEach(t => t.stop());
        }
    }

    // callback from getUserMedia when it fails.
    private errorCallback(reason) {
        // something wrong! Developers note: Bloom and Firefox cannot both use it, so be careful about
        // "open in browser".
        // reason.name seems to be "NotFoundError" if there is no camera at all.
        this.setState({
            cameraAccess: false,
            cameraUnavailable:
                reason &&
                reason.name &&
                (reason.name === "NotReadableError" || // Gecko63 or so
                    reason.name == "SourceUnavailableError") // Gecko45
        });
        // In case the user plugs in a camera, try once a second to turn it on.
        window.setTimeout(() => this.turnOnVideo(), 1000);
    }

    // callback from getUserMedia when it succeeds; gives us a stream we can monitor and record from.
    private startMonitoring(stream: MediaStream) {
        this.setState({
            cameraAccess: true
        });
        this.videoStream = stream;
        const videoMonitor = document.getElementById(
            "videoMonitor"
        ) as HTMLVideoElement;
        videoMonitor.srcObject = stream;
    }

    // Not just a normal function definition, because then when we pass it to addEventListener,
    // 'this' is not what we expect. Nor can we just pass a locally-defined function, because
    // we have to be able to remove it later.
    private onKeyPress = () => {
        this.toggleRecording();
    };

    // Called when the record or stop button is clicked, or if a key is pressed while not in the idle state
    // ...depending on the current state it either starts or ends the recording. It works as the action function
    // for things that only stop the recording because those controls are not enabled in the idle state.
    private toggleRecording() {
        if (!this.videoStream) {
            return;
        }
        const oldState = this.state.stateClass;
        const wasRecording = this.state.recording;
        if (wasRecording) {
            document.removeEventListener("keydown", this.onKeyPress);
            this.setState({ recording: false, stateClass: "idle" });
            // triggers all the interesting behavior defined in onstop below.
            this.mediaRecorder.stop();
            return;
        }
        if (oldState === "idle") {
            document.addEventListener("keydown", this.onKeyPress);
            this.setState({ stateClass: "countdown3" });
            this.timerId = window.setTimeout(() => {
                this.setState({ stateClass: "countdown2" });
                this.timerId = window.setTimeout(() => {
                    this.setState({ stateClass: "countdown1" });
                    this.timerId = window.setTimeout(() => {
                        this.setState({
                            stateClass: "recording",
                            recording: true
                        });
                        this.startRecording();
                    }, 1000);
                }, 1000);
            }, 1000);
            return;
        }
        // we're in one of the countdown states. Back to idle.
        document.removeEventListener("keydown", this.onKeyPress);
        window.clearTimeout(this.timerId);
        this.setState({ stateClass: "idle" });
    }

    public componentDidUpdate(prevProps, prevState: IComponentState) {
        const currentState = this.state.stateClass;
        const previousState = prevState.stateClass;
        if (currentState === previousState) {
            return; // some other part of state changing?
        }
        switch (currentState) {
            case "countdown3":
            case "countdown2":
            case "countdown1":
                SignLanguageTool.showOverlayToHideVideo();
                break;
            case "recording":
                SignLanguageTool.showOverlayToHideVideo();
                SignLanguageTool.addRecordingLabelToOverlay(document);
                break;
            default:
                // back to 'idle'
                SignLanguageTool.removeVideoOverlay();
                if (this.state.haveRecording) {
                    this.getVideoStatsFromFile();
                }
                break;
        }
    }

    private startRecording() {
        // OK, we want to start recording.
        this.chunks = [];
        const options = {
            // I found a couple of examples online with these rates for video/mp4 and the results
            // look reasonable. It's possible we could get useful recordings with lower rates.
            audioBitsPerSecond: 128000,
            videoBitsPerSecond: 2500000
            // Setting this after the move to Geckofx60 results in an error, but
            // the default seems to produce the same result.
            //mimeType: "video/mp4"
        };
        this.mediaRecorder = new MediaRecorder(this.videoStream, options);
        this.mediaRecorder.ondataavailable = e => {
            // called periodically during recording and once more with the rest of the data
            // when recording stops. So all the chunks which make up the recording come here.
            this.chunks.push(e.data);
        };
        this.mediaRecorder.onstop = () => {
            // raised when the user clicks stop and we call this.mediaRecorder.stop() above.
            const blob = new Blob(this.chunks, { type: "video/webm" });
            this.chunks = []; // enable garbage collection?
            BloomApi.postDataWithConfig("signLanguage/recordedVideo", blob, {
                headers: {
                    "Content-Type": "video/mp4"
                }
            });
            // Don't know why this is necessary, but for some reason, the stream we have is no
            // longer useful after calling mediaRecorder.stop(). The monitor freezes and
            // nothing happens when I click record. So dispose of it and start a new one.
            this.turnOffVideo();
            this.turnOnVideo();
        };

        // All set, get the actual recording going.
        this.mediaRecorder.start();
    }

    public static setup(root): SignLanguageToolControls {
        return ReactDOM.render(
            <SignLanguageToolControls />,
            root
        ) as SignLanguageToolControls;
    }
}

export class SignLanguageTool extends ToolboxToolReactAdaptor {
    private reactControls: SignLanguageToolControls;

    public makeRootElement(): HTMLDivElement {
        const root = document.createElement("div");
        root.setAttribute("class", "signLanguageBody");
        this.reactControls = SignLanguageToolControls.setup(root);
        return root as HTMLDivElement;
    }

    public isExperimental(): boolean {
        return true;
    }

    public beginRestoreSettings(settings: string): JQueryPromise<void> {
        // Nothing to do, so return an already-resolved promise.
        const result = $.Deferred<void>();
        result.resolve();
        return result;
    }

    // Specify 'true' to get only containers marked as selected
    private static getVideoContainers(
        selected?: boolean
    ): HTMLCollectionOf<Element> {
        let classes = "bloom-videoContainer";
        if (selected) {
            classes += " bloom-selected";
        }
        return ToolBox.getPage().getElementsByClassName(classes);
    }

    public static getSelectedVideoPathAndTiming(): string {
        const containers = this.getVideoContainers(true);
        if (containers.length == 0) {
            return null;
        }
        const container = containers[0];
        const videos = container.getElementsByTagName("video");
        if (videos.length == 0) {
            return null;
        }
        const sources = videos[0].getElementsByTagName("source");
        if (sources.length == 0) {
            return null;
        }
        return sources[0].getAttribute("src");
    }

    public static getSelectedVideoPath(): string {
        // strip off the ?now= param we sometimes use to prevent use of cached old versions,
        // and the #t= fragment used to trim videos.
        return UrlUtils.extractPathComponent(
            SignLanguageTool.getSelectedVideoPathAndTiming()
        );
    }

    public detachFromPage() {
        // Decided NOT to remove bloom-selected here. It's harmless (only the edit stylesheet
        // does anything with it) and leaving it allows us to keep the same one selected
        // when we come back to the page. This is especially important when refreshing the
        // page after selecting or recording a video.
        const containers = SignLanguageTool.getVideoContainers(false);
        for (let i = 0; i < containers.length; i++) {
            containers[i].removeEventListener(
                "click",
                this.containerClickListener
            );
        }

        this.reactControls.turnOffVideo();
    }

    public id(): string {
        return "signLanguage";
    }

    // This function is saved in a variable so we can remove the same listener we added.
    private containerClickListener: EventListener = (event: MouseEvent) => {
        // The reason for the listener: to select the current element
        const currentContainers = SignLanguageTool.getVideoContainers(false);
        for (let i = 0; i < currentContainers.length; i++) {
            currentContainers[i].classList.remove("bloom-selected");
        }
        const container = event.currentTarget as HTMLElement;
        container.classList.add("bloom-selected");
        this.updateStateForSelected(container);
        // And now in most locations we want to prevent the default behavior where click starts playback.
        // This may need adjustment for zoom.
        // The idea here is that it should be possible to select a video by clicking it
        // without starting it playing. On the other hand, the playback controls should work.
        // So we prevent the default (playback-related) behavior if the click is in the area
        // that the actual controls occupy.
        // This is fairly rough (the central circle is approximated with a square, and we don't
        // try to account for the fact that it disappears once first clicked).
        // It's also fairly fragile...future versions of Gecko may not have the exact same
        // controls in the same places. But it's the best I've been able to figure out.
        const clientRect = container.getBoundingClientRect(); // real pixels, larger rect when zoomed
        const scale = clientRect.height / container.offsetHeight; // e.g., 1.3 at 130%
        const buttonRadius = 28 * scale;
        const x = event.clientX - clientRect.left; // clientX and Y are also real pixels
        const y = event.clientY - clientRect.top;
        // The fudge factor of 30 was determined experimentally. I don't know why
        // Firefox puts the play button just above center.
        const heightOfPlayCenter = (clientRect.height - 30 * scale) / 2;
        if (
            y < clientRect.height - 40 * scale && // above the control bar across the bottom
            (y < heightOfPlayCenter - buttonRadius || // above the play button
            y > heightOfPlayCenter + buttonRadius || // below the play button
            x < clientRect.width / 2 - buttonRadius || // left of play button
                x > clientRect.width / 2 + buttonRadius)
        ) {
            // right of play button
            event.preventDefault();
        }
    };

    public newPageReady() {
        const containers = SignLanguageTool.getVideoContainers(false);
        if (containers.length === 0) {
            if (this.reactControls.state.enabled) {
                this.reactControls.turnOffVideo();
                this.reactControls.setState({ enabled: false });
            }
        } else {
            // We want one video container to be selected, so pick the first.
            // If one is already marked selected, presumably from a previous use of this page,
            // we'll leave that one active.
            const selectedVideos = SignLanguageTool.getVideoContainers(true);
            if (selectedVideos.length === 0) {
                containers[0].classList.add("bloom-selected");
                this.updateStateForSelected(containers[0]);
            } else {
                this.updateStateForSelected(selectedVideos[0]);
            }
            for (let i = 0; i < containers.length; i++) {
                const container = containers[i];
                // UpdateMarkup is called fairlyfrequently. Not sure what effect having
                // the same listener attached multiple times might have, so play safe by
                // removing it before adding.
                container.removeEventListener(
                    "click",
                    this.containerClickListener
                );
                container.addEventListener(
                    "click",
                    this.containerClickListener
                );
            }
            // we turn it off when we leave a page, so even if we already have enabled:true,
            // we need to turn it on for this page now we know there is something to record.
            this.reactControls.turnOnVideo();
            if (!this.reactControls.state.enabled) {
                this.reactControls.setState({ enabled: true });
            }
        }
    }

    private updateStateForSelected(container: Element) {
        const videos = container.getElementsByTagName("video");
        if (videos.length === 0) {
            this.reactControls.setState({
                haveRecording: false,
                originalExists: false
            });
            return;
        }
        const sources = videos[0].getElementsByTagName("source");

        if (sources.length === 0) {
            this.reactControls.setState({
                haveRecording: false,
                originalExists: false
            });
            return;
        }

        const src = sources[0].getAttribute("src");
        if (!src) {
            this.reactControls.setState({
                haveRecording: false,
                originalExists: false
            });
            return;
        }
        const urlTimingObj = SignLanguageTool.parseVideoSrcAttribute(src);
        const start = urlTimingObj.start;
        const end = urlTimingObj.end; // could be -1.0
        const stats = this.reactControls.state.videoStatistics;
        stats.startSeconds = start;
        stats.endSeconds = end;
        this.reactControls.setState({ videoStatistics: stats });
        BloomApi.get(
            // extractPathComponent doesn't unencode the path, so if needed this
            // URL should already be %encoded. But video filenames in Bloom are currently
            // all GUIDs, even for imported videos, so this isn't really an issue.
            "toolbox/fileExists?filename=" + UrlUtils.extractPathComponent(src),
            result => {
                const fileExists: boolean = result.data;
                if (fileExists) {
                    this.reactControls.getVideoStatsFromFile();
                    SignLanguageTool.setCurrentVideoPoint(
                        parseFloat(
                            this.reactControls.state.videoStatistics
                                .startSeconds
                        )
                    );
                }
                this.reactControls.setState({ haveRecording: fileExists });
            }
        );
        BloomApi.get(
            "toolbox/fileExists?filename=" + src.replace(".mp4", ".orig"),
            result => {
                this.reactControls.setState({ originalExists: result.data });
            }
        );
    }

    private static overlayClass: string = "bloom-videoOverlay";

    // Make an overlay and slap it over the selected edit pane video still while we're recording
    public static showOverlayToHideVideo(): void {
        const container = SignLanguageTool.getVideoContainers(true)[0]; // 'true' gets only the selected video
        if (
            !container.previousElementSibling.classList.contains(
                SignLanguageTool.overlayClass
            )
        ) {
            // bloom-ui class makes sure this div is removed before saving
            const overlayDiv =
                "<div class='" +
                SignLanguageTool.overlayClass +
                " bloom-ui'><label></label></div";
            container.parentElement.insertBefore(
                SignLanguageTool.createNode(
                    container.ownerDocument,
                    overlayDiv
                ),
                container
            );
        }
    }

    // Grab the "Recording" label in React-land and stick it in the edit pane overlay
    public static addRecordingLabelToOverlay(reactDoc: Document): void {
        const container = SignLanguageTool.getVideoContainers(true)[0];
        const recordingLabel = SignLanguageTool.getRecordingLabel(reactDoc);
        container.previousElementSibling.firstChild.textContent = recordingLabel;
    }

    // Remove the overlay hiding the video, now that we're done recording
    public static removeVideoOverlay(): void {
        const container = SignLanguageTool.getVideoContainers(true)[0];
        const overlayElement = container.previousElementSibling;
        if (
            overlayElement &&
            overlayElement.classList.contains(SignLanguageTool.overlayClass)
        ) {
            container.previousElementSibling.remove();
        }
    }

    private static createNode(doc: Document, html: string): Node {
        const template = doc.createElement("template");
        template.innerHTML = html.trim();
        return template.content.firstChild;
    }

    private static getRecordingLabel(doc: Document): string {
        const labelElement = doc.getElementsByClassName("recordingLabel")[0]; // should only be one
        return labelElement !== null ? labelElement.textContent.trim() : null;
    }

    public static setVideoTimingsInSrcAttr(
        newStartString: string,
        newEndString: string
    ) {
        const video = this.getSelectedVideoElement();
        const source = video.getElementsByTagName(
            "source"
        )[0] as HTMLSourceElement;
        let src = source.getAttribute("src");
        const urlTimingObj = SignLanguageTool.parseVideoSrcAttribute(src);
        src = urlTimingObj.url;
        source.setAttribute(
            "src",
            src + "#t=" + newStartString + "," + newEndString
        );
    }

    private static getSelectedVideoElement(): HTMLVideoElement {
        const selectedContainers = SignLanguageTool.getVideoContainers(true); // s/b only one selected container
        return selectedContainers[0].getElementsByTagName(
            "video"
        )[0] as HTMLVideoElement;
    }

    public static setCurrentVideoPoint(
        timeInSeconds: number,
        videoElement?: HTMLVideoElement
    ): void {
        if (!videoElement) {
            videoElement = SignLanguageTool.getSelectedVideoElement();
        }
        videoElement.currentTime = timeInSeconds;
    }

    // Returns an Object containing the results of executing a regexp on the src attribute of the source element.
    // url: the url without timings
    // start: the start time in seconds, or default of 0.0
    // end: the end time in seconds, or default of -1.0 (temporary placeholder for maxDuration)
    public static parseVideoSrcAttribute(srcAttr: string): UrlTimingObject {
        const re = /(.*)#t=([0-9]*[.][0-9]+)[,]([0-9]*[.][0-9]+)+/;
        const matches = re.exec(srcAttr);
        // The matches object returned above is a RegExpExecArray. We create a specialized object to return.
        const result: UrlTimingObject = new UrlTimingObject();
        if (matches) {
            result.url = matches[1];
            if (matches.length > 2) {
                result.start = matches[2];
            }
            if (matches.length > 3) {
                result.end = matches[3];
            }
        } else {
            result.url = srcAttr;
        }
        return result;
    }

    public static convertTimeStringToSecondsNumber(duration: string): number {
        if (duration === "" || duration === MAX_DURATION) {
            return 0;
        }
        // from https://stackoverflow.com/questions/9640266/convert-hhmmss-string-to-seconds-only-in-javascript/9640417
        // though not the 'accepted' answer.
        return duration
            .split(":")
            .reverse()
            .reduce(
                (prev, curr, i) => prev + parseFloat(curr) * Math.pow(60, i),
                0
            );
    }
}

class UrlTimingObject {
    public url: string;
    public start: string;
    public end: string;

    constructor() {
        this.url = "";
        this.start = DEFAULT_START;
        this.end = MAX_DURATION;
    }
}
