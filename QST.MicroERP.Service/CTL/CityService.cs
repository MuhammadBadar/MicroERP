﻿using QST.MicroERP.Core.Entities.CTL;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL;
using QST.MicroERP.DAL.CTL;
using MySql.Data.MySqlClient;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QST.MicroERP.Service.CTL
{
    public partial class CatalogueService
    {
        //private CityDAL _ctyDAL;
        //private CoreDAL _corDAL;
        //private Logger _logger;


        //public CatalogueService()
        //{
        //    _ctyDAL = new CityDAL();
        //    _corDAL = new CoreDAL();
        //    _logger = LogManager.GetLogger("fileLogger");
        //}

        public bool ManageCity(CityDE _City)
        {
            // class veriables/datamembers

            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                if (_City.DBoperation == DBoperations.Insert)
                    _City.Id = _coreDAL.GetnextId(TableNames.City.ToString());
                retVal = _ctyDAL.ManageCity(_City, cmd);
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }

        //public List<VocabularyDE> SearchVocabulary(VocabularySearchCriteria mod)
        public List<CityDE> SearchCity(CityDE cty)
        // public List<StudentDE> SearchStudent(StudentDE _student)
        {
            // public List<TopicDE> SearchTopic(TopicDE _topic)
            {
                List<CityDE> retVal = new List<CityDE>();
                bool closeConnectionFlag = false;
                MySqlCommand? cmd = null;
                try
                {
                    cmd = MicroERPDataContext.OpenMySqlConnection();
                    closeConnectionFlag = true;
                    string WhereClause = "Where 1=1";
                    if (cty.Id != default)
                        WhereClause += $" AND Id={cty.Id}";
                    if (cty.Name != default)
                        WhereClause += $" and Name like ''" + cty.Name + "''";
                    if (cty.IsActive != default)
                        WhereClause += $" AND IsActive={cty.IsActive}";

                    retVal = _ctyDAL.SearchCity(WhereClause, cmd);
                    return retVal;
                }
                catch (Exception ex)
                {
                    _logger.Error(ex);
                    throw;
                }
                finally
                {
                    if (closeConnectionFlag)
                        MicroERPDataContext.CloseMySqlConnection(cmd);
                }
            }

        }
    }
}


