using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using QST.ERP.Domain.AlKhair;
using System.Data.SqlClient;
using System.Transactions;
using QST.ERP.Domain.Data;
using QST.ERP.Domain.BDM;
using QST.ERP.DAL.UoWorks;
using QST.ERP.DAL;
using QST.ERP.Domain;

using QST.ERP.Domain.Translators;

namespace QST.ERP.Services
{
    public class AlKhairService : IAlKhairServiceContract
    {
        private IBDMServiceContract _bdmSvc;
        private readonly IRepository<GiftTypeView> _giftTypeVwRepo;
        private readonly IRepository<VoucherBE> _vchRepo;
        private readonly IRepository<VoucherNoBE> _vchNoRepo;
        private readonly IUnitOfWork _uow;

        public AlKhairService()
        {
            _uow = new BDMDataUoWork(DBHelper.ConnectionString);
            _giftTypeVwRepo = new EFRepository<GiftTypeView>(_uow);
            _vchRepo = new EFRepository<VoucherBE>(_uow);
            _vchNoRepo = new EFRepository<VoucherNoBE>(_uow);
            _bdmSvc = new BDMService();
        }

        #region GiftTypes
        
        public int AddGiftType(GiftTypeBE mod)
        {
            EntityDE ety = new EntityDE();
            ety.SiteCode = mod.SiteCode;
            ety.EntityTypeCode = mod.EntityTypeCode;
            ety.EntityName = mod.EntityName;
            ety.Code = mod.Code;

            int retVal = 0;
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                _bdmSvc = new BDMService();
                StringBuilder sb = new StringBuilder();
                using (TransactionScope trans = new TransactionScope())
                {
                    mod.ID = _bdmSvc.AddEntity(ety);
                
                    SqlCommand cmd = new SqlCommand();

                    #region CommandText & Params

                    sb.Append("INSERT INTO [dbo].[GiftType]");
                    sb.Append("([GiftTypeID]");
                    sb.Append(",[IsCondition]");
                    sb.Append(",[Price])");
                    sb.Append("VALUES");
                    sb.Append("(@GiftTypeID");
                    sb.Append(",@IsCondition");
                    sb.Append(",@Price)");
        
                    cmd.Parameters.AddWithValue("@GiftTypeID", mod.ID);
                    cmd.Parameters.AddWithValue("@IsCondition", mod.IsCondition);
                    cmd.Parameters.AddWithValue("@Price", mod.Price);

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

        public bool ModifyGiftType(GiftTypeBE mod)
        {
            EntityDE ety = new EntityDE();
            ety.SiteCode = mod.SiteCode;
            ety.EntityTypeCode = mod.EntityTypeCode;
            ety.ID = mod.ID;
            ety.EntityName = mod.EntityName;
            ety.Code = mod.Code;
            ety.IsActive = mod.IsActive;

            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                //mod.ID = GetNextEntityId(mod.SiteCode, mod.EntityTypeCode);
                StringBuilder sb = new StringBuilder();
                using (TransactionScope trans = new TransactionScope())
                {
                    _bdmSvc = new BDMService();
                    if (_bdmSvc.ModifyEntity(ety))
                    {
                        SqlCommand cmd = new SqlCommand();

                        #region CommandText & Params

                        sb.Append("UPDATE [dbo].[GiftType] SET ");
                        sb.Append("[GiftTypeID] = @GiftTypeID");
                        sb.Append(",[IsCondition] = @IsCondition");
                        sb.Append(",[Price] = @Price");
                        sb.Append(" WHERE ");
                        sb.Append(" [GiftTypeID] = @GiftTypeID");

                        cmd.Parameters.AddWithValue("@GiftTypeID", mod.ID);
                        cmd.Parameters.AddWithValue("@IsCondition", mod.IsCondition);
                        cmd.Parameters.AddWithValue("@Price", mod.Price);


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

        public bool DeleteGiftType(string siteCode, string GiftTypeTypeCode, int id)
        {
            throw new NotImplementedException();
        }

        public GiftTypeBE GetGiftTypeById(string siteCode, string GiftTypeTypeCode, int id)
        {
            throw new NotImplementedException();
        }

        public List<GiftTypeBE> GetAllGiftTypes(string siteCode)
        {
            throw new NotImplementedException();
        }


        public List<GiftTypeBE> GetAllGiftTypes(string siteCode, string giftTypeCode)
        {
            throw new NotImplementedException();
        }

        public List<GiftTypeView> GetViewOfAllGiftTypes(string siteCode, string giftTypeCode)
        {
            List<GiftTypeView> mod = new List<GiftTypeView>();

            try
            {
                mod = _giftTypeVwRepo.Query.Where(m => m.SiteCode == siteCode && m.IsActive == true && m.EntityTypeCode == giftTypeCode).ToList();
            }
            catch (Exception ex)
            {
                mod = null;
            }
            return mod;

        }

        #endregion

        #region Donor

        public int AddDonor(DonorBE mod)
        {
            mod.EntityTypeCode = EntityTypeCodes.DNR.ToString();
            return _bdmSvc.AddPerson(mod.ToPerson());
        }

        public bool ModifyDonor(DonorBE mod)
        {
            mod.EntityTypeCode = EntityTypeCodes.DNR.ToString();
            return _bdmSvc.ModifyPerson(mod.ToPerson());
        }

        public bool DeleteDonor(string siteCode, int id)
        {
            return _bdmSvc.DeleteEntity(siteCode, EntityTypeCodes.DNR.ToString(), id);
        }

        public DonorBE GetDonorById(string siteCode, int id)
        {
            return _bdmSvc.GetPersonById(siteCode, EntityTypeCodes.DNR.ToString(), id).ToDonor();
        }

        //public List<DonorBE> GetAllDonors(string siteCode)
        //{
        //    List<DonorBE> list = new List<DonorBE>();
        //    try
        //    {
        //        list = _bdmSvc.GetViewOfAllPersons(siteCode, EntityTypeCodes.DNR.ToString()).Select(m => m.ToDonor()).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        list = null;
        //    }
        //    return list;
        //}

        
        //public List<DonorView> GetViewOfAllDonors(string siteCode)
        //{
        //    List<DonorView> list = new List<DonorView>();
        //    try
        //    {
        //        var pvList = _bdmSvc.GetViewOfAllPersons(siteCode, EntityTypeCodes.DNR.ToString());//.Select(m => m.ToDonor()).ToList();
        //        list = pvList.Select(m => m.ToDonor()).ToList();
        //    }
        //    catch (Exception ex)
        //    {
        //        list = null;
        //    }
        //    return list;
        //}

        public List<DonorBE> GetViewOfAllDonors(string siteCode)
        {
            List<DonorBE> list = new List<DonorBE>();
            try
            {
                var pvList = _bdmSvc.GetViewOfAllPersons(siteCode, EntityTypeCodes.DNR.ToString());//.Select(m => m.ToDonor()).ToList();
                list = pvList.Select(m => m.ToDonor()).ToList();
            }
            catch (Exception ex)
            {
                list = null;
            }
            return list;
        }
        #endregion

        #region Voucher

        public int AddVoucher(VoucherBE mod)
        {
            try
            {
                VoucherNoBE vchNo = _vchNoRepo.GetById(mod.UserName);
                if (vchNo != null)
                {
                    vchNo.VoucherNo = mod.VchNo;
                    _vchNoRepo.Update(vchNo);
                }
                else
                {
                    vchNo = new VoucherNoBE();
                    vchNo.VoucherNo = mod.VchNo;
                    vchNo.UserName = mod.UserName;
                    _vchNoRepo.Insert(vchNo);
                }
                
                _vchRepo.Insert(mod);
                _uow.Commit();
            }
            catch (Exception ex)
            {
                mod.ID = -1;
            }
            return mod.ID; 
        }

        public bool ModifyVoucher(VoucherBE mod)
        {
            bool retVal = true;
            try
            {
                _vchRepo.Update(mod);
                _uow.Commit();
            }
            catch (Exception ex)
            {
                retVal = false;
            }

            return retVal;
        }

        public bool DeleteVoucher(string siteCode, int id)
        {
            bool retVal = true;
            try
            {
                var mod = _vchRepo.GetById(siteCode, id);
                _vchRepo.Delete(mod);
            }
            catch (Exception ex)
            {
                retVal = false;
            }
            return retVal;
        }

        public VoucherBE GetVoucherById(string siteCode, int id)
        {
            VoucherBE retVal = new VoucherBE();
            try
            {
                _vchRepo.GetById(siteCode, id);
            }
            catch (Exception ex)
            {
                retVal = null;
            }
            return retVal;
        }

        public List<VoucherBE> GetAllVouchers(string siteCode)
        {
            List<VoucherBE> retVal = new List<VoucherBE>();
            try
            {
                retVal = _vchRepo.GetAll().ToList();
            }
            catch (Exception ex)
            {
                retVal = null;
            }

            return retVal;
        }

        public List<VoucherBE> GetViewOfAllVouchers(string siteCode)
        {
            throw new NotImplementedException();
        }

        #endregion

        public string GetNextVoucherNo(string userName)
        {
            string vchNo = string.Empty;

            int retVal = 1;
            SqlConnection con = new SqlConnection(DBHelper.ConnectionString);
            try
            {
                SqlCommand cmd = new SqlCommand("select VoucherNo from VoucherNo where UserName=@UserName", con);

                cmd.Parameters.AddWithValue("@UserName", userName);
                con.Open();
                object val = cmd.ExecuteScalar();
                if (val != null && val is string)
                {
                    retVal = Convert.ToInt32((string)val) + 1;
                }
                //else
                //{
                //    VoucherNoBE voucherNo = new VoucherNoBE();
                //    voucherNo.VoucherNo = "1";
                //    voucherNo.UserName = userName;
                //    _vchNoRepo.Insert(voucherNo);
                //    _uow.Commit();
                //}
            }
            catch (Exception ex)
            {
                retVal = -1;
            }
            finally
            {
                con.Close();
            }

            return retVal.ToString();
        }
    }
}
