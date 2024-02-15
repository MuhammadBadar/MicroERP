using MicroERP.Core.Entities.LMS;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.LMS;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.LMS
{
    public class CourseDetailService
    {
        #region Class Members/Class Variables

        private CourseDetailDAL _CourseDetailDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public CourseDetailService()
        {
            _CourseDetailDAL = new CourseDetailDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region CourseDetail
        public bool ManagementCourseDetail(CourseDetailDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.coursedetail.ToString());
                retVal = _CourseDetailDAL.ManageCourseDetail(mod);
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                return retVal;
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
        }
        public List<CourseDetailDE> SearchCourseDetails(CourseDetailDE mod)
        {
            List<CourseDetailDE> CourseDetails = new List<CourseDetailDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                #region Search

                string whereClause = " Where 1=1";
                if (mod.Id != default && mod.Id != 0)
                    whereClause += $" AND Id={mod.Id}";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                CourseDetails = _CourseDetailDAL.SearchCourseDetails(whereClause);

                #endregion
            }
            catch (Exception exp)
            {
                _logger.Error(exp);
                throw;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return CourseDetails;
        }
        #endregion
    }
}
