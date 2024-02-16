using QST.MicroERP.Core.Enums;

namespace QST.MicroERP.Core.SearchCriteria
{
    public abstract class BaseSearchCriteria
    {
        public DBoperations DBoperation { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeSubordinatesData { get; set; }

    }
}
