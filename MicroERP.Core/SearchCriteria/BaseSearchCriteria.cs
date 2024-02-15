using MicroERP.Core.Enums;

namespace MicroERP.Core.SearchCriteria
{
    public abstract class BaseSearchCriteria
    {
        public DBoperations DBoperation { get; set; }
        public int Id { get; set; }
        public bool IsActive { get; set; }
        public bool IncludeSubordinatesData { get; set; }

    }
}
