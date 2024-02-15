using MicroERP.Core.Entities.LMS;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.LMS;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.LMS
{
    public class CourseService
    {
        #region Class Members/Class Variables

        private CourseDAL _CourseDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public CourseService()
        {
            _CourseDAL = new CourseDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region Course
        public bool ManagementCourse(CourseDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.course.ToString());
                retVal = _CourseDAL.ManageCourse(mod);
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
        public List<CourseDE> SearchCourses(CourseDE mod)
        {
            List<CourseDE> Courses = new List<CourseDE>();
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
                Courses = _CourseDAL.SearchCourses(whereClause);

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
            return Courses;
        }
        #endregion
    }
}
