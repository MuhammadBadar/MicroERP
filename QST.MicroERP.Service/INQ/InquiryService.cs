using QST.MicroERP.Core.Entities.INQ;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL.INQ;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service.INQ
{
    public class InquiryService
    {
        #region Class Members/Class Variables

        private InquiryDAL _inqryDAL;
        private CoreDAL _corDAL;

        #endregion
        #region Constructors
        public InquiryService()
        {
            _inqryDAL = new InquiryDAL();
            _corDAL = new CoreDAL();
        }

        #endregion
        #region Inquiry
        public bool ManagementInquiry(InquiryDE mod)
        {
            MySqlCommand cmd = null;
            try
            {
                bool check = true;
                cmd = MicroERPDataContext.OpenMySqlConnection();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.inquiry.ToString());
                check = _inqryDAL.ManageInquiry(mod);
                if (check == true)
                    mod.DBoperation = DBoperations.NA;

            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return true;

        }
        public List<InquiryDE> SearchInquiry(InquiryDE mod)
        {
            List<InquiryDE> Inquiry = new List<InquiryDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.ServicesId != default)
                    whereClause += $" AND ServicesId={mod.ServicesId}";
                if (mod.Name != default)
                    whereClause += $" AND Name like ''" + mod.Name + "''";
                if (mod.Cell != default)
                    whereClause += $" AND Cell like ''" + mod.Cell + "''";
                if (mod.Comments != default)
                    whereClause += $" AND Comments like ''" + mod.Comments + "''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                Inquiry = _inqryDAL.SearchInquirys(whereClause);

                #endregion
            }
            catch (Exception)
            {
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return Inquiry;
        }
        #endregion
    }
}
