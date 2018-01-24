﻿/// <reference path="../../toolbox.ts" />
import { getTheOneReaderToolsModel, DRTState, } from "../readerToolsModel";
import { beginInitializeLeveledReaderTool } from "../readerTools";
import { ITool } from "../../toolbox";
import { ToolBox } from "../../toolbox";

export default class LeveledReaderToolboxTool implements ITool {
    makeRootElements(): JQuery {
        throw new Error("Method not implemented.");
    }
    beginRestoreSettings(opts: string): JQueryPromise<void> {
        return beginInitializeLeveledReaderTool().then(() => {
            if (opts['leveledReaderState']) {
                getTheOneReaderToolsModel().setLevelNumber(parseInt(opts['leveledReaderState']));
            }
        });
    }

    configureElements(container: HTMLElement) { }
    isAlwaysEnabled(): boolean {
        return false;
    }

    showTool() {
        // change markup based on visible options
        getTheOneReaderToolsModel().setCkEditorLoaded(); // we don't call showTool until it is.
        if (!getTheOneReaderToolsModel().setMarkupType(2)) getTheOneReaderToolsModel().doMarkup();
    }

    hideTool() {
        getTheOneReaderToolsModel().setMarkupType(0);
    }

    updateMarkup() {
        // Most cases don't require setMarkupType(), but when switching pages
        // it will have been set to 0 by hideTool() on the old page.
        getTheOneReaderToolsModel().setMarkupType(2);
        getTheOneReaderToolsModel().doMarkup();
    }

    name() { return 'leveledReader'; }

    hasRestoredSettings: boolean;

    // Some things were impossible to do i18n on via the jade/pug
    // This gives us a hook to finish up the more difficult spots
    finishToolLocalization(paneDOM: HTMLElement) {
        // Unneeded in Leveled Reader, since Bloom.web.ExternalLinkController
        // 'translates' external links to include the current UI language.
    }
}

ToolBox.getMasterToolList().push(new LeveledReaderToolboxTool());
