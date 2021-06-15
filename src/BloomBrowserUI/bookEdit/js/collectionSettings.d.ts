// Type definitions for RuntimeInformationInjector injected GetSettings()
// If you want to use GetSettings() in your .ts file, reference this file.

declare function GetSettings(): ICollectionSettings;

interface ICollectionSettings {
    isSourceCollection: boolean;
    languageForNewTextBoxes: string;
    defaultSourceLanguage: string;
    currentCollectionLanguage2: string;
    currentCollectionLanguage3: string;
    browserRoot: string;
    topics: string[];
}
