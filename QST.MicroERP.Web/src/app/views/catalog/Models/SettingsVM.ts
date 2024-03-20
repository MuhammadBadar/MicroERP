export class SettingsVM {
    name: string = "";
    clientId: number
    moduleId: number
    module: string
    description?: string;
    id: number = 0;
    parentId: number = 0;
    isActive?: Boolean;
    istAccountLevel: Boolean = false;
    uploadStatus: Boolean = false;
    keyCode: string = "";
    isSystemDefined = false;
    enumTypeId: number = 0;
    settingType?: string
    parentName?: string
    pParentName?: string
    typeKeyCode?: string;
    levelId: number = 0
    level!: string;
    value: string
    accountCode?: string
    dBoperation: number
    isChecked: boolean
    cityId: number
    pParentId: number
    typeModuleId: number
}