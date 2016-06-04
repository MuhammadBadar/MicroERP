using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using System.Transactions;

namespace QST.ERP.Services
{
    public interface IBDMServiceContract
    {
        #region Contract Rules

        #region Department

        decimal AddDepartment(DepartmentDE mod);
        void ModifyDepartment(DepartmentDE mod);
        void DeleteDepartment(string siteCode, decimal id);
        List<DepartmentDE> GetAllDepartments(string siteCode);
        IQueryable<DepartmentDE> GetAllQuerableDepartments(string siteCode);
        DepartmentDE GetDepartmentById(string siteCode, decimal id);

        #endregion

        #region User

        decimal AddUser(UserDE mod);
        void ModifyUser(UserDE mod);
        void DeleteUser(string siteCode, decimal id);
        List<UserDE> GetAllUsers(string siteCode);
        IQueryable<UserDE> GetAllQuerableUsers(string siteCode);
        UserDE GetUserById(string siteCode, decimal id);

        #endregion

        #region Region

        decimal AddRegion(RegionDE mod);
        void ModifyRegion(RegionDE mod);
        void DeleteRegion(string siteCode, decimal id);
        List<RegionDE> GetAllRegions(string siteCode);
        IQueryable<RegionDE> GetAllQuerableRegions(string siteCode);
        RegionDE GetRegionByCode(string siteCode, string regionCode);
        bool RegionCodeExists(string siteCode, string regionCode);

        #endregion

        #region City

        decimal AddCity(CityDE mod);
        void ModifyCity(CityDE mod);
        void DeleteCity(string siteCode, decimal id);
        List<CityDE> GetAllCities(string siteCode);
        List<CityDE> GetCitiesByRegion(string siteCode, string regionCode);
        List<CityVW> GetViewOfAllCities(string siteCode);
        IQueryable<CityDE> GetAllQuerableCities(string siteCode);
        CityDE GetCityByCode(string siteCode, string cityCode);
        bool CityCodeExists(string siteCode, string cityCode);

        #endregion

        #region Area

        decimal AddArea(AreaDE mod);
        void ModifyArea(AreaDE mod);
        void DeleteArea(string siteCode, decimal id);
        List<AreaDE> GetAllAreas(string siteCode);
        List<AreaDE> GetAreasByCity(string siteCode, string cityCode);
        List<AreaVw> GetViewOfAllAreas(string siteCode);
        IQueryable<AreaDE> GetAllQuerableAreas(string siteCode);
        AreaDE GetAreaByCode(string siteCode, string areaCode);
        bool AreaCodeExists(string siteCode, string areaCode);

        #endregion

        #region Entity

        int AddEntity(EntityDE mod);
        //int AddEntity(EntityDE mod, TransactionScope tran);
        bool ModifyEntity(EntityDE mod);
        bool DeleteEntity(string siteCode, string entityTypeCode, int id);
        EntityDE GetEntityById(string siteCode, string entityTypeCode, int id);
        List<EntityDE> GetAllEntities(string siteCode);
        List<EntityView> GetViewOfAllEntities(string siteCode);
        List<EntityView> GetViewOfAllEntities(string siteCode, string entityTypeCode);
        IQueryable<EntityDE> GetAllQuerableEntities(string siteCode);
        
        
        #endregion

        #region ExpenseGroup

        void AddExpenseGroup(ExpenseGroupDE mod);
        void ModifyExpenseGroup(ExpenseGroupDE mod);
        void DeleteExpenseGroup(string siteCode, string groupCode);
        List<ExpenseGroupDE> GetAllExpenseGroups(string siteCode);
        IQueryable<ExpenseGroupDE> GetAllQuerableExpenseGroups(string siteCode);
        ExpenseGroupDE GetExpenseGroupByCode(string siteCode, string groupCode);
        bool ExpenseGroupCodeExists(string siteCode, string cityCode);

        #endregion

        #region ExpenseHead

        void AddExpenseHead(ExpenseHeadDE mod);
        void ModifyExpenseHead(ExpenseHeadDE mod);
        void DeleteExpenseHead(string siteCode, string groupCode);
        List<ExpenseHeadDE> GetAllExpenseHeads(string siteCode);
        List<ExpenseHeadVw> GetViewOfAllExpenseHeads(string siteCode);
        IQueryable<ExpenseHeadDE> GetAllQuerableExpenseHeads(string siteCode);
        ExpenseHeadDE GetExpenseHeadByCode(string siteCode, string groupCode);
        bool ExpenseHeadCodeExists(string siteCode, string cityCode);

        #endregion

        #region Occupation

        List<OccupationBE> GetAllOccupations();
        
        #endregion

        #region Person

        int AddPerson(Person mod);
        bool ModifyPerson(Person mod);
        bool DeletePerson(string siteCode, string entityTypeCode, int id);
        Person GetPersonById(string siteCode, string entityTypeCode, int id);
        
        List<PersonView> GetViewOfAllPersons(string siteCode, string entityTypeCode=null);

        #endregion

        #region Employee

        int AddEmployee(EmployeeCoreBE mod);
        bool ModifyEmployee(EmployeeCoreBE mod);
        bool DeleteEmployee(string siteCode, int id);
        EmployeeCoreBE GetEmployeeById(string siteCode, int id);

        List<EmployeeCoreBE> GetViewOfAllEmployees(string siteCode);

        #endregion

        #region Bank

        int AddBank(BankBE mod);
        bool ModifyBank(BankBE mod);
        bool DeleteBank(string siteCode, int id);
        BankBE GetBankById(string siteCode, int id);
        List<BankBE> GetAllBanks(string siteCode);

        #endregion

        #endregion
    }
}
