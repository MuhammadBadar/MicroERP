using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain;
using QST.ERP.Domain.BDM;
using QST.ERP.Domain.Data;
using QST.ERP.DAL.UoWorks;
using QST.ERP.DAL;
using System.Data.SqlClient;
using System.Transactions;
using QST.ERP.Domain.Translators;

namespace QST.ERP.Services
{
    public class BDMService : Service, IBDMServiceContract
    {
        #region DataMembers

        private readonly IRepository<DepartmentDE> _deptRepo;
        private readonly IRepository<UserDE> _userRepo;
        private readonly IRepository<RegionDE> _regionRepo;
        private readonly IRepository<CityDE> _cityRepo;
        private readonly IRepository<CityVW> _cityVwRepo;
        private readonly IRepository<AreaDE> _areaRepo;
        private readonly IRepository<AreaVw> _areaVwRepo;

        private readonly IRepository<EntityDE> _etyRepo;
        private readonly IRepository<EntityView> _etyVwRepo;

        private readonly IRepository<AddressDE> _addRepo;
        private readonly IRepository<ContactDE> _cntRepo;
        private readonly IRepository<ExpenseGroupDE> _expGrpRepo;
        private readonly IRepository<ExpenseHeadDE> _expHeadRepo;
        private readonly IRepository<ExpenseHeadVw> _expHeadVwRepo;
        private readonly IRepository<OccupationBE> _occupationRepo;
        private readonly IRepository<Person> _personRepo;
        private readonly IRepository<PersonView> _personViewRepo;

        private readonly IUnitOfWork _uow;
        
        #endregion

        #region Constructors

        public BDMService()
        {
            _uow = new BDMDataUoWork(DBHelper.ConnectionString);
            _deptRepo = new EFRepository<DepartmentDE>(_uow);
            _userRepo = new EFRepository<UserDE>(_uow);
            _regionRepo = new EFRepository<RegionDE>(_uow);
            _cityRepo = new EFRepository<CityDE>(_uow);
            _cityVwRepo = new EFRepository<CityVW>(_uow);
            _areaRepo = new EFRepository<AreaDE>(_uow);
            _areaVwRepo = new EFRepository<AreaVw>(_uow);
            _etyRepo = new EFRepository<EntityDE>(_uow);
            _etyVwRepo = new EFRepository<EntityView>(_uow);
            _addRepo = new EFRepository<AddressDE>(_uow);
            _cntRepo = new EFRepository<ContactDE>(_uow);
            _expGrpRepo = new EFRepository<ExpenseGroupDE>(_uow);
            _expHeadRepo = new EFRepository<ExpenseHeadDE>(_uow);
            _expHeadVwRepo = new EFRepository<ExpenseHeadVw>(_uow);
            _occupationRepo = new EFRepository<OccupationBE>(_uow);

            _personRepo = new EFRepository<Person>(_uow);
            _personViewRepo = new EFRepository<PersonView>(_uow);
        }

        public BDMService(IUnitOfWork uom)
        {
            _deptRepo = new EFRepository<DepartmentDE>(uom);
            _userRepo = new EFRepository<UserDE>(uom);
            _regionRepo = new EFRepository<RegionDE>(_uow);
            _cityRepo = new EFRepository<CityDE>(_uow);
            _areaRepo = new EFRepository<AreaDE>(_uow);
            _areaVwRepo = new EFRepository<AreaVw>(_uow);
            _etyRepo = new EFRepository<EntityDE>(_uow);
            _etyVwRepo = new EFRepository<EntityView>(_uow);
            _addRepo = new EFRepository<AddressDE>(_uow);
            _cntRepo = new EFRepository<ContactDE>(_uow);
            _expGrpRepo = new EFRepository<ExpenseGroupDE>(_uow);
            _expHeadRepo = new EFRepository<ExpenseHeadDE>(_uow);
            _expHeadVwRepo = new EFRepository<ExpenseHeadVw>(_uow);
            _occupationRepo = new EFRepository<OccupationBE>(_uow);

            _personRepo = new EFRepository<Person>(_uow);
            _personViewRepo = new EFRepository<PersonView>(_uow);
        }

        #endregion

        #region Department

        public decimal AddDepartment(DepartmentDE mod)
        {
            _deptRepo.Insert(mod);
            _deptRepo.CommitAllChanges();
            return mod.ID;
        }

        public void ModifyDepartment(DepartmentDE mod)
        {
            _deptRepo.Update(mod);
            _deptRepo.CommitAllChanges();
        }

        public void DeleteDepartment(string siteCode, decimal id)
        {
            throw new NotImplementedException();
        }

        public IQueryable<DepartmentDE> GetAllQuerableDepartments(string siteCode)
        {
            return _deptRepo.Query.Where(m => m.SiteCode == siteCode);
        }

        public List<DepartmentDE> GetAllDepartments(string siteCode)
        {
            return _deptRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public IQueryable<DepartmentDE> GetAllQuerableDepartments()
        {
            return _deptRepo.Query;
        }

        public DepartmentDE GetDepartmentById(string siteCode, decimal id)
        {
            return _deptRepo.GetById(siteCode, id);
        }

        #endregion
        
        #region User

        public decimal AddUser(UserDE mod)
        {
            _userRepo.Insert(mod);
            _userRepo.CommitAllChanges();
            return mod.ID;

        }

        public void ModifyUser(UserDE mod)
        {
            throw new NotImplementedException();
        }

        public void DeleteUser(string siteCode, decimal id)
        {
            throw new NotImplementedException();
        }

        public List<UserDE> GetAllUsers(string siteCode)
        {
            return _userRepo.GetAll().Where(m => m.SiteCode == siteCode).ToList();
        }

        public IQueryable<UserDE> GetAllQuerableUsers(string siteCode)
        {
            return _userRepo.Query.Where(m => m.SiteCode == siteCode);
        }

        public UserDE GetUserById(string siteCode, decimal id)
        {
            return _userRepo.GetById(siteCode, id);
        }

        #endregion

        #region Region

        public decimal AddRegion(RegionDE mod)
        {
            _regionRepo.Insert(mod);
            _userRepo.CommitAllChanges();
            return mod.ID;
        }

        public void ModifyRegion(RegionDE mod)
        {
            _regionRepo.Update(mod);
            _regionRepo.CommitAllChanges();
        }

        public void DeleteRegion(string siteCode, decimal id)
        {
            _regionRepo.Delete(_regionRepo.GetById(siteCode, id));
        }

        public List<RegionDE> GetAllRegions(string siteCode)
        {
            return _regionRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public IQueryable<RegionDE> GetAllQuerableRegions(string siteCode)
        {
            throw new NotImplementedException();
        }

        public RegionDE GetRegionByCode(string siteCode, string regionCode)
        {
            return _regionRepo.GetById(siteCode, regionCode);
        }

        public bool RegionCodeExists(string siteCode, string regionCode)
        {
            return _regionRepo.GetAll().Where(m => m.SiteCode == siteCode && m.RegionCode == regionCode).Count() > 0 ? true : false;
        }

        #endregion

        #region City

        public decimal AddCity(CityDE mod)
        {
            _cityRepo.Insert(mod);
            _cityRepo.CommitAllChanges();
            return mod.ID;
        }

        public void ModifyCity(CityDE mod)
        {
            _cityRepo.Update(mod);
            _cityRepo.CommitAllChanges();
            
        }

        public void DeleteCity(string siteCode, decimal id)
        {
             _cityRepo.Delete(_cityRepo.GetById(siteCode, id));
        }

        public List<CityDE> GetAllCities(string siteCode)
        {
            return _cityRepo.GetAll().Where(m => m.SiteCode == siteCode).ToList();
        }

        public List<CityDE> GetCitiesByRegion(string siteCode, string regionCode)
        {
            return _cityRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true && m.RegionCode == regionCode).ToList();
        }

        public List<CityVW> GetViewOfAllCities(string siteCode)
        {
            return _cityVwRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public IQueryable<CityDE> GetAllQuerableCities(string siteCode)
        {
            throw new NotImplementedException();
        }

        public CityDE GetCityByCode(string siteCode, string cityCode)
        {
            return _cityRepo.GetById(siteCode, cityCode);
        }

        public bool CityCodeExists(string siteCode, string cityCode)
        {
            return _cityRepo.GetById(siteCode, cityCode) != null ? true : false; // .GetAll().Where(m => m.SiteCode == siteCode && m.CityCode == cityCode).Count() > 0 ? true : false;
        }

        #endregion

        #region Area

        public decimal AddArea(AreaDE mod)
        {
            _areaRepo.Insert(mod);
            _areaRepo.CommitAllChanges();
            return mod.ID;
        }

        public void ModifyArea(AreaDE mod)
        {
            _areaRepo.Update(mod);
            _areaRepo.CommitAllChanges();
        }

        public void DeleteArea(string siteCode, decimal id)
        {
            _areaRepo.Delete(_areaRepo.GetById(siteCode, id));
        }

        public List<AreaDE> GetAllAreas(string siteCode)
        {
            return _areaRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public List<AreaDE> GetAreasByCity(string siteCode, string cityCode)
        {
            return _areaRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true && m.CityCode == cityCode).ToList();
        }

        public List<AreaVw> GetViewOfAllAreas(string siteCode)
        {
            return _areaVwRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public IQueryable<AreaDE> GetAllQuerableAreas(string siteCode)
        {
            throw new NotImplementedException();
        }

        public AreaDE GetAreaByCode(string siteCode, string areaCode)
        {
            return _areaRepo.GetById(siteCode, areaCode);
        }

        public bool AreaCodeExists(string siteCode, string areaCode)
        {
            return _areaRepo.GetAll().Where(m => m.SiteCode == siteCode && m.AreaCode == areaCode).Count() > 0 ? true : false;
        }

        #endregion

        #region Entity

        private int GetNextEntityId(string siteCode, string entityTypeCode)
        {
            int retVal = 1;
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select Max(ID) from Entity where SiteCode=@SiteCode AND EntityTypeCode=@EntityTypeCode", con);

                cmd.Parameters.AddWithValue("@SiteCode", siteCode);
                cmd.Parameters.AddWithValue("@EntityTypeCode", entityTypeCode);
                con.Open();
                object val = cmd.ExecuteScalar();
                if (val != null && val is int)
                {
                    retVal = (int)val + 1;
                }
            }
            catch (Exception ex)
            {
                retVal = -1;
            }
            finally
            {
                con.Close();
            }

            return retVal;
        }
        
        //private int AddEntity(EntityDE mod, )
        public int AddEntity(EntityDE mod)
        {
            int retVal = 0;
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            
            try
            {
            
                //trans.Commit();
                mod.ID = GetNextEntityId(mod.SiteCode, mod.EntityTypeCode);
                StringBuilder sb = new StringBuilder();
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    SqlCommand cmd = new SqlCommand();

                    #region CommandText & Params

                    sb.Append("INSERT INTO [dbo].[Entity]");
                    sb.Append("([SiteCode]");
                    sb.Append(",[ID]");
                    sb.Append(",[EntityTypeCode]");
                    sb.Append(",[Code]");
                    sb.Append(",[EntityName]");
                    sb.Append(",[IsActive])");
                    sb.Append("VALUES");
                    sb.Append("(@SiteCode");
                    sb.Append(",@ID");
                    sb.Append(",@EntityTypeCode");
                    sb.Append(",@Code");
                    sb.Append(",@EntityName");
                    sb.Append(",@IsActive)");
        
                    cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                    cmd.Parameters.AddWithValue("@ID", mod.ID);
                    cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                    cmd.Parameters.AddWithValue("@Code", mod.Code);
                    cmd.Parameters.AddWithValue("@EntityName", mod.EntityName);
                    cmd.Parameters.AddWithValue("@IsActive", mod.IsActive);

                    #endregion

                    cmd.Connection = con;
                    cmd.CommandText = sb.ToString();
                    con.Open();
                    
                    cmd.ExecuteNonQuery();
                    retVal = mod.ID;
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                retVal = -1;
            }
            finally 
            {
                con.Close();
            }

            return retVal;
        }


        public bool ModifyEntity(EntityDE mod)
        {
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                //mod.ID = GetNextEntityId(mod.SiteCode, mod.EntityTypeCode);
                StringBuilder sb = new StringBuilder();
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    SqlCommand cmd = new SqlCommand();

                    #region CommandText & Params

                    sb.Append("UPDATE [dbo].[Entity] SET ");
                    sb.Append("[Code] = @Code");
                    sb.Append(",[EntityName] = @EntityName");
                    sb.Append(",[IsActive] = @IsActive");
                    sb.Append(" WHERE ");
                    sb.Append(" [SiteCode] = @SiteCode");
                    sb.Append(" AND [ID] = @ID");
                    sb.Append(" AND [EntityTypeCode] = @EntityTypeCode");

                    cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                    cmd.Parameters.AddWithValue("@ID", mod.ID);
                    cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                    cmd.Parameters.AddWithValue("@Code", mod.Code);
                    cmd.Parameters.AddWithValue("@EntityName", mod.EntityName);
                    cmd.Parameters.AddWithValue("@IsActive", mod.IsActive);

                    #endregion

                    cmd.Connection = con;
                    cmd.CommandText = sb.ToString();
                    con.Open();

                    cmd.ExecuteNonQuery();
                    trans.Complete();
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }

            return true;
            //_etyRepo.Update(mod);
            //foreach (var add in mod.Addresses)
            //    _addRepo.Update(add);
            //foreach (var cnt in mod.Contacts)
            //    _cntRepo.Update(cnt);
            ////_uow.Context.Entry(mod).State =  EntityState.Modified;
            //_etyRepo.CommitAllChanges();
        }

        public bool DeleteEntity(string siteCode, string entityTypeCode, int id)
        {
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);

            try
            {
                StringBuilder sb = new StringBuilder();
                SqlCommand cmd = new SqlCommand();
                sb.Append("DELETE FROM  [dbo].[Entity]");
                sb.Append(" WHERE [SiteCode] = @SiteCode");
                sb.Append(" AND [ID]=@ID");
                sb.Append(" AND [EntityTypeCode]=@EntityTypeCode");

                cmd.Parameters.AddWithValue("@SiteCode", siteCode);
                cmd.Parameters.AddWithValue("@EntityTypeCode", entityTypeCode);
                cmd.Parameters.AddWithValue("@ID", id);

                cmd.Connection = con;
                cmd.CommandText = sb.ToString();
                con.Open();
                cmd.ExecuteNonQuery();
                
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }
            
            return true;
        }

        public EntityDE GetEntityById(string siteCode, string entityTypeCode, int id)
        {
            EntityDE mod = new EntityDE();
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            
            try
            {
                //using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    StringBuilder sb = new StringBuilder();
                    SqlCommand cmd = new SqlCommand();
                    sb.Append("SELECT * FROM  [dbo].[Entity]");
                    sb.Append(" WHERE [SiteCode] = @SiteCode");
                    sb.Append(" AND [EntityTypeCode]=@EntityTypeCode");
                    sb.Append(" AND [ID]=@ID");
                    
                    cmd.Parameters.AddWithValue("@SiteCode", siteCode);
                    cmd.Parameters.AddWithValue("@EntityTypeCode", entityTypeCode);
                    cmd.Parameters.AddWithValue("@ID", id);

                    cmd.Connection = con;
                    cmd.CommandText = sb.ToString();
                    con.Open();
                    SqlDataReader rdr = cmd.ExecuteReader();
                    if (rdr.HasRows)
                    {
                        while (rdr.Read())
                        {
                            mod.Code = rdr["Code"] != DBNull.Value ? (string)rdr["Code"] : string.Empty;
                            mod.EntityName = rdr["EntityName"] != DBNull.Value ? (string)rdr["EntityName"] : string.Empty;
                            mod.IsActive = rdr["IsActive"] != DBNull.Value ? (bool)rdr["IsActive"] : false;
                        }
                    }
                    else
                        mod = null;
                    
                    //trans.Complete();
                }
            }
            catch (Exception ex)
            {
                mod = null;
            }
            finally
            {
                con.Close();
            }
            return mod;
        }

        public List<EntityDE> GetAllEntities(string siteCode)
        {
            _uow.LazyLoadingEnabled = false;
            var entities = _etyRepo.Query.Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
            foreach (var ety in entities)
            {
                ety.Addresses = _addRepo.Query.Where(m => m.SiteCode == siteCode && m.EntityID == ety.ID).ToList();
                ety.Contacts = _cntRepo.Query.Where(m => m.SiteCode == siteCode && m.EntityID == ety.ID).ToList();
            }
            
            return entities;
        }

        public List<EntityView> GetViewOfAllEntities(string siteCode)
        {
            List<EntityView> mod = new List<EntityView>();

            try
            {
                mod = _etyVwRepo.Query.Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                mod = null;
            }
            return mod;
        }

        public List<EntityView> GetViewOfAllEntities(string siteCode, string entityTypeCode)
        {
            List<EntityView> mod = new List<EntityView>();

            try
            {
                mod = _etyVwRepo.Query.Where(m => m.SiteCode == siteCode && m.EntityTypeCode == entityTypeCode && m.IsActive == true).ToList();
            }
            catch (Exception ex)
            {
                mod = null;
            }
            return mod;
        }

        public IQueryable<EntityDE> GetAllQuerableEntities(string siteCode)
        {
            throw new NotImplementedException();
        }

        //public EntityDE GetEntityById(string siteCode, string entityTypeCode, int id)
        //{
        //    //return _etyRepo.Query.Where(m => m.SiteCode == siteCode && m.ID == id).FirstOrDefault();
        //    var mod = _etyRepo.GetById(siteCode, id);
        //    //var mod = _etyRepo.Query.Where(m => m.SiteCode == siteCode && m.ID == id).FirstOrDefault();
        //    return mod;
        //}

        #endregion

        #region Expense Group

        public void AddExpenseGroup(ExpenseGroupDE mod)
        {
            _expGrpRepo.Insert(mod);
            _expGrpRepo.CommitAllChanges();
        }

        public void ModifyExpenseGroup(ExpenseGroupDE mod)
        {
            _expGrpRepo.Update(mod);
            _expGrpRepo.CommitAllChanges();
        }

        public void DeleteExpenseGroup(string siteCode, string groupCode)
        {
            throw new NotImplementedException();
        }

        public List<ExpenseGroupDE> GetAllExpenseGroups(string siteCode)
        {
            return _expGrpRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public IQueryable<ExpenseGroupDE> GetAllQuerableExpenseGroups(string siteCode)
        {
            return _expGrpRepo.Query.Where(m => m.SiteCode == siteCode);
        }

        public ExpenseGroupDE GetExpenseGroupByCode(string siteCode, string groupCode)
        {
            return _expGrpRepo.GetById(siteCode, groupCode);
        }

        public bool ExpenseGroupCodeExists(string siteCode, string expenseGroupCode)
        {
            return _expGrpRepo.GetById(siteCode, expenseGroupCode) != null ? true : false;
        }

        #endregion

        #region Expense Head

        public void AddExpenseHead(ExpenseHeadDE mod)
        {
            _expHeadRepo.Insert(mod);
            _expHeadRepo.CommitAllChanges();
        }

        public void ModifyExpenseHead(ExpenseHeadDE mod)
        {
            _expHeadRepo.Update(mod);
            _expHeadRepo.CommitAllChanges();
        }

        public void DeleteExpenseHead(string siteCode, string groupCode)
        {
            throw new NotImplementedException();
        }

        public List<ExpenseHeadDE> GetAllExpenseHeads(string siteCode)
        {
            return _expHeadRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public List<ExpenseHeadVw> GetViewOfAllExpenseHeads(string siteCode)
        {
            return _expHeadVwRepo.GetAll().Where(m => m.SiteCode == siteCode && m.IsActive == true).ToList();
        }

        public IQueryable<ExpenseHeadDE> GetAllQuerableExpenseHeads(string siteCode)
        {
            return _expHeadRepo.Query.Where(m => m.SiteCode == siteCode);
        }

        public ExpenseHeadDE GetExpenseHeadByCode(string siteCode, string groupCode)
        {
            return _expHeadRepo.GetById(siteCode, groupCode);
        }

        public bool ExpenseHeadCodeExists(string siteCode, string expenseGroupCode)
        {
            return _expHeadRepo.GetById(siteCode, expenseGroupCode) != null ? true : false;
        }

        #endregion

        public List<OccupationBE> GetAllOccupations()
        {
            return _occupationRepo.GetAll().ToList();
        }

        #region Person

        public int AddPerson(Person mod)
        {
            //mod.ID = GetNextEntityId(mod.SiteCode, mod.EntityTypeCode);
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            StringBuilder sb = new StringBuilder();
            try
            {
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    mod.ID = AddEntity(mod.ToEntity());
                    if (mod.ID != -1)
                    {
                        #region Add Person

                        SqlCommand cmd = new SqlCommand();

                        #region CommandText & Params

                        sb = new StringBuilder();

                        sb.Append("INSERT INTO [dbo].[Person]");
                        sb.Append("([SiteCode]");
                        sb.Append(",[EntityTypeCode]");
                        sb.Append(",[PersonId]");
                        sb.Append(",[ParentId]");
                        sb.Append(",[ParentRelationId]");
                        sb.Append(",[MeritalStatusId]");
                        sb.Append(",[DOB]");
                        sb.Append(",[Age]");
                        sb.Append(",[Picture]");
                        sb.Append(",[Gender]");
                        sb.Append(",[Signature]");
                        sb.Append(",[ThumbImpression]");
                        sb.Append(",[Phone]");
                        sb.Append(",[NationalId])");
                        sb.Append("VALUES");
                        sb.Append("(@SiteCode");
                        sb.Append(",@EntityTypeCode");
                        sb.Append(",@PersonId");
                        sb.Append(",@ParentId");
                        sb.Append(",@ParentRelationId");
                        sb.Append(",@MeritalStatusId");
                        sb.Append(",@DOB");
                        sb.Append(",@Age");
                        sb.Append(",@Picture");
                        sb.Append(",@Gender");
                        sb.Append(",@Signature");
                        sb.Append(",@ThumbImpression");
                        sb.Append(",@Phone");
                        sb.Append(",@NationalId)");

                        cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                        cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                        cmd.Parameters.AddWithValue("@PersonId", mod.ID);
                        cmd.Parameters.AddWithValue("@ParentId", mod.ParentId);
                        cmd.Parameters.AddWithValue("@ParentRelationId", mod.ParentRelationId);
                        cmd.Parameters.AddWithValue("@MeritalStatusID", mod.MeritalStatusId);
                        cmd.Parameters.AddWithValue("@DOB", mod.DOB);
                        cmd.Parameters.AddWithValue("@Age", mod.Age);
                        cmd.Parameters.AddWithValue("@Picture", mod.Picture);
                        cmd.Parameters.AddWithValue("@Gender", mod.Gender);
                        cmd.Parameters.AddWithValue("@Signature", mod.Signature);
                        cmd.Parameters.AddWithValue("@ThumbImpression", mod.ThumbImpression);
                        cmd.Parameters.AddWithValue("@Phone", mod.Phone);
                        cmd.Parameters.AddWithValue("@NationalId", mod.NationalId);

                        #endregion

                        con.Open();
                        cmd.CommandText = sb.ToString();
                        cmd.Connection = con;
                        cmd.ExecuteNonQuery();


                        #endregion

                        trans.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                mod.ID = -1;
            }
            finally
            {
                con.Close();
            }

            return mod.ID;
        }
        
        public bool ModifyPerson(Person mod)
        {
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                //mod.ID = GetNextEntityId(mod.SiteCode, mod.EntityTypeCode);
                StringBuilder sb = new StringBuilder();
                using (TransactionScope trans = new TransactionScope(TransactionScopeOption.Required))
                {
                    EntityDE ety = mod.ToEntity();
                    if (ModifyEntity(ety))
                    {

                        SqlCommand cmd = new SqlCommand();

                        #region CommandText & Params

                        sb.Append("UPDATE [dbo].[Person] SET ");
                        sb.Append(" [ParentId] = @ParentId");
                        sb.Append(",[ParentRelationId] = @ParentRelationId");
                        sb.Append(",[MeritalStatusId] = @MeritalStatusId");
                        sb.Append(",[DOB] = @DOB");
                        sb.Append(",[Age] = @Age");
                        sb.Append(",[Picture] = @Picture");
                        sb.Append(",[Gender] = @Gender");
                        sb.Append(",[Signature] = @Signature");
                        sb.Append(",[ThumbImpression] = @ThumbImpression");
                        sb.Append(",[Phone] = @Phone");
                        sb.Append(",[NationalId] = @NationalId");
                        sb.Append(" WHERE ");
                        sb.Append(" [SiteCode] = @SiteCode");
                        sb.Append(" AND [PersonId] = @PersonId");
                        sb.Append(" AND [EntityTypeCode] = @EntityTypeCode");

                        cmd.Parameters.AddWithValue("@SiteCode", mod.SiteCode);
                        cmd.Parameters.AddWithValue("@EntityTypeCode", mod.EntityTypeCode);
                        cmd.Parameters.AddWithValue("@PersonId", mod.ID);

                        cmd.Parameters.AddWithValue("@ParentId", mod.ParentId);
                        cmd.Parameters.AddWithValue("@ParentRelationId", mod.ParentRelationId);
                        cmd.Parameters.AddWithValue("@MeritalStatusID", mod.MeritalStatusId);
                        cmd.Parameters.AddWithValue("@DOB", mod.DOB);
                        cmd.Parameters.AddWithValue("@Age", mod.Age);
                        cmd.Parameters.AddWithValue("@Picture", mod.Picture);
                        cmd.Parameters.AddWithValue("@Gender", mod.Gender);
                        cmd.Parameters.AddWithValue("@Signature", mod.Signature);
                        cmd.Parameters.AddWithValue("@ThumbImpression", mod.ThumbImpression);
                        cmd.Parameters.AddWithValue("@NationalId", mod.NationalId);
                        cmd.Parameters.AddWithValue("@Phone", mod.Phone);


                        #endregion

                        cmd.Connection = con;
                        cmd.CommandText = sb.ToString();
                        con.Open();

                        cmd.ExecuteNonQuery();
                        trans.Complete();
                    }
                }
            }
            catch (Exception ex)
            {
                return false;
            }
            finally
            {
                con.Close();
            }

            return true;
        }

        public bool DeletePerson(string siteCode, string entityTypeCode, int id)
        {
            throw new NotImplementedException();
        }
        public Person GetPersonById(string siteCode, string entityTypeCode, int id)
        {
            Person mod = new Person();
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);

            try
            {
                //using (TransactionScope trans = new TransactionScope())
                {
                    EntityDE ety = GetEntityById(siteCode, entityTypeCode, id);
                    mod = ety.ToPerson();
                    if (mod != null)
                    {
                        StringBuilder sb = new StringBuilder();
                        SqlCommand cmd = new SqlCommand();
                        sb.Append("SELECT * FROM  [dbo].[Person]");
                        sb.Append(" WHERE [SiteCode] = @SiteCode");
                        sb.Append(" AND [EntityTypeCode]=@EntityTypeCode");
                        sb.Append(" AND [PersonId]=@PersonId");

                        cmd.Parameters.AddWithValue("@SiteCode", siteCode);
                        cmd.Parameters.AddWithValue("@EntityTypeCode", entityTypeCode);
                        cmd.Parameters.AddWithValue("@PersonId", id);

                        cmd.Connection = con;
                        cmd.CommandText = sb.ToString();
                        con.Open();
                        SqlDataReader rdr = cmd.ExecuteReader();

                        if (rdr.HasRows)
                        {
                            while (rdr.Read())
                            {
                                mod.SiteCode = siteCode;
                                mod.EntityTypeCode = entityTypeCode;
                                mod.ID = id;
                                mod.ParentId = rdr["ParentId"] != DBNull.Value ? (int)rdr["ParentId"] : 0;
                                mod.ParentRelationId = rdr["ParentRelationId"] != DBNull.Value ? (int)rdr["ParentRelationId"] : 0;
                                mod.MeritalStatusId = rdr["MeritalStatusId"] != DBNull.Value ? (int)rdr["MeritalStatusId"] : 0;
                                mod.DOB = rdr["DOB"] != DBNull.Value ? (DateTime)rdr["DOB"] : new DateTime(1900, 1, 1);
                                mod.Age = rdr["Age"] != DBNull.Value ? (int)rdr["Age"] : 0;
                                mod.Picture = rdr["Picture"] != DBNull.Value ? (string)rdr["Picture"] : string.Empty;
                                mod.Gender = rdr["Gender"] != DBNull.Value ? (string)rdr["Gender"] : string.Empty;
                                mod.Signature = rdr["Signature"] != DBNull.Value ? (string)rdr["Signature"] : string.Empty;
                                mod.ThumbImpression = rdr["ThumbImpression"] != DBNull.Value ? (string)rdr["ThumbImpression"] : string.Empty;
                                mod.Phone = rdr["Phone"] != DBNull.Value ? (string)rdr["Phone"] : string.Empty;
                                mod.NationalId = rdr["NationalId"] != DBNull.Value ? (string)rdr["NationalId"] : string.Empty;
                            }
                            
                        }
                        else
                            mod = null;
                        
                    }
                    //trans.Complete();
                }
            }
            catch (Exception ex)
            {
                mod = null;
            }
            finally
            {
                con.Close();
                
            }
            return mod;
        }
        
        public List<PersonView> GetViewOfAllPersons(string siteCode, string entityTypeCode)
        {
            List<PersonView> mod = new List<PersonView>();
            try
            {
                var qry = _personViewRepo.Query.Where(m => m.SiteCode == siteCode);
                if(!string.IsNullOrEmpty(entityTypeCode))
                    qry = qry.Where(m => m.EntityTypeCode == entityTypeCode);
                mod = qry.ToList();
            }
            catch (Exception ex)
            {
                mod = null;
            }
            return mod;
        }
        
        #endregion

        #region EmployeeCore

        public int AddEmployee(EmployeeCoreBE mod)
        {
            return AddPerson(mod.ToPerson());
        }

        public bool ModifyEmployee(EmployeeCoreBE mod)
        {
            return ModifyPerson(mod.ToPerson());
        }

        public bool DeleteEmployee(string siteCode, int id)
        {
            return DeletePerson(siteCode, EntityTypes.EMP.ToString(), id);
        }

        public EmployeeCoreBE GetEmployeeById(string siteCode, int id)
        {
            Person p = GetPersonById(siteCode, EntityTypes.EMP.ToString(), id);
            return p.ToEmployee();
        }

        public List<EmployeeCoreBE> GetViewOfAllEmployees(string siteCode)
        {
            List<EmployeeCoreBE> list = new List<EmployeeCoreBE>();
            try
            {
                var pvList = GetViewOfAllPersons(siteCode, EntityTypeCodes.EMP.ToString());//.Select(m => m.ToDonor()).ToList();
                list = pvList.Select(m => m.ToEmployee()).ToList();
            }
            catch (Exception ex)
            {
                list = null;
            }
            return list;
        }

        #endregion
        
        #region Bank
        public int AddBank(BankBE mod)
        {
            return AddEntity(mod.ToEntity());
        }

        public bool ModifyBank(BankBE mod)
        {
            return ModifyEntity(mod.ToEntity());
        }

        public bool DeleteBank(string siteCode, int id)
        {
            return DeleteEntity(siteCode, EntityTypeCodes.BNK.ToString(), id);
        }

        public BankBE GetBankById(string siteCode, int id)
        {
            EntityDE mod = GetEntityById(siteCode, EntityTypeCodes.BNK.ToString(), id);
            return mod.ToBank();
        }

        public List<BankBE> GetAllBanks(string siteCode)
        {
            List<BankBE> mod = new List<BankBE>();
            try
            {
                var list = GetViewOfAllEntities(siteCode, EntityTypeCodes.BNK.ToString()); // (siteCode).Where(m => m.EntityTypeCode == EntityTypeCodes.BNK.ToString()).ToList(); //, EntityTypeCodes.BNK.ToString());//.Select(m => m.ToDonor()).ToList();
                mod = list.Select(m => m.ToBank()).ToList();
            }
            catch (Exception ex)
            {
                mod = null;
            }
            return mod;
        }

        #endregion
        

    }
}
