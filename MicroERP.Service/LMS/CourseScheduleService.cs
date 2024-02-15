using MicroERP.Core.Entities.LMS;
using MicroERP.Core.Enums;
using MicroERP.DAL;
using MicroERP.DAL.CTL;
using MicroERP.DAL.LMS;
using MySql.Data.MySqlClient;
using NLog;

namespace MicroERP.Service.LMS
{
    public class CourseScheduleService
    {
        #region Class Members/Class Variables

        private CourseScheduleDAL _CourseScheduleDAL;
        private CoreDAL _corDAL;
        private Logger _logger;

        #endregion
        #region Constructors
        public CourseScheduleService()
        {
            _CourseScheduleDAL = new CourseScheduleDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
        }
        #endregion
        #region CourseSchedule
        public CourseScheduleDE ManagementCourseSchedule(CourseScheduleDE mod)
        {
            bool retVal = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                MicroERPDataContext.StartTransaction(cmd);

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _corDAL.GetnextId(TableNames.courseschedule.ToString());
                retVal = _CourseScheduleDAL.ManageCourseSchedule(mod, cmd);
                if (mod.DBoperation == DBoperations.Insert || mod.DBoperation == DBoperations.Update)
                    foreach (var line in mod.ClassTimings)
                    {
                        line.SchId = mod.Id;
                        if (line.DBoperation == DBoperations.Insert)
                        {
                            line.Id = _corDAL.GetnextLineId(TableNames.classtiming.ToString(), mod.Id, "SchId");
                            line.Id += 1;
                        }
                        retVal = _CourseScheduleDAL.ManageClassTiming(line, cmd);
                    }
                if (retVal == true)
                    mod.DBoperation = DBoperations.NA;
                MicroERPDataContext.EndTransaction(cmd);
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
                string whereClause = " Where 1=1";
                mod.ClassTimings = _CourseScheduleDAL.SearchClassTimings(whereClause += $" AND SchId={mod.Id} AND IsActive ={true}");
            }
            return mod;
        }
        public List<CourseScheduleDE> SearchCourseSchedules(CourseScheduleDE mod)
        {
            List<CourseScheduleDE> Schedules = new List<CourseScheduleDE>();
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
                Schedules = _CourseScheduleDAL.SearchCourseSchedules(whereClause);
                foreach (var line in Schedules)
                {
                    whereClause = "where 1=1";
                    line.ClassTimings = _CourseScheduleDAL.SearchClassTimings(whereClause += $" AND SchId={line.Id} AND IsActive ={true}");
                }

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
            return Schedules;
        }
        #endregion
    }
}
