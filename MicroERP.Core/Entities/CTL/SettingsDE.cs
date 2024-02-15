namespace MicroERP.Core.Entities.CTL
{
    public class SettingsDE : BaseDomain
    {
        #region Constructor
        public SettingsDE ( )
        {
            KeyCode = null;
            Name = null;
            Description = null;
        }
        #endregion
        #region Properties
        public int? ParentId { get; set; }
        public string? KeyCode { get; set; }
        public int ModuleId { get; set; }
        public string? Value { get; set; }
        public string? AccountCode { get; set; }
        public bool IsSystemDefined { get; set; }
        public bool IstAccountLevel { get; set; }
        public int? EnumTypeId { get; set; }
        public string? Name { get; set; }
        public string? ParentName { get; set; }
        public string? PParentName { get; set; }
        public int PParentId { get; set; }
        public string? Description { get; set; }
        public string? SettingType { get; set; }
        public string? TypeKeyCode { get; set; }
        public int TypeModuleId { get; set; }
        public int? LevelId { get; set; }
        public string? Level { get; set; }
        public string? Module { get; set; }
        public bool ShouldDisplay { get; set; }
        #endregion
    }
}
