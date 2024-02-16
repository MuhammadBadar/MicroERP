namespace QST.MicroERP.Core.Entities.CTL
{
    public class SettingsTypeDE : BaseDomain
    {
        #region Constructor
        public SettingsTypeDE ( )
        {
            KeyCode = null;
            Description = null;
            Name = null;
            SettingList = new List<SettingsDE> ();
        }
        #endregion
        #region Properties
        public int? ParentId { get; set; }
        public int ModuleId { get; set; }
        public string? KeyCode { get; set; }
        public bool IsSystemDefined { get; set; }
        public bool IstAccountLevel { get; set; }
        public string? Description { get; set; }
        public string? Name { get; set; }
        public string? ParentName { get; set; }
        public bool IsRequired { get; set; }
        public List<SettingsDE> SettingList { get; set; }
        #endregion
    }
}