import { SettingsVM } from "./SettingsVM";

export class SettingsTypeVM {
    name?: string;
    description?: string;
    id?: number = 0;
    parentKeyCode?: string;
    keyCode: string = "";
    isSystemDefined: Boolean = false;
    parentId: number = 0;
    isActive?: Boolean;
    parentName?: string
    uploadStatus: Boolean = false;
    isRequired: boolean
    istAccountLevel: Boolean = false;
    settingList?: SettingsVM[] = new Array();
    moduleId?: number = 0
    clientId?: number
}