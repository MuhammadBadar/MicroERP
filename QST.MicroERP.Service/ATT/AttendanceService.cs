using QST.MicroERP.Core.Enums;
using QST.MicroERP.Core.SearchCriteria;
using QST.MicroERP.Core.ViewModel;
using QST.MicroERP.DAL;
using Microsoft.VisualBasic;
using MySql.Data.MySqlClient;
using QST.MicroERP.Core.Extensions;
using NLog;
using Org.BouncyCastle.Utilities.IO;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Constants;
using QST.MicroERP.Service.CLT;
using QST.MicroERP.Service.SCH;
using QST.MicroERP.DAL.ATT;
using QST.MicroERP.DAL.TMS;
using QST.MicroERP.Core.Entities.TMS;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Entities.SCH;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.Core.Entities.CTL;

namespace QST.MicroERP.Service.ATT
{
    public class AttendanceService
    {
        #region Class Members/Class Variables

        private AttendanceDAL attDAL;
        private CoreDAL _corDAL;
        private Logger _logger;
        private ScheduleService _schSvc;
        private LogEventService _logEventSvc;
        private UserTaskDAL _uTaskDAL;

        #endregion
        #region Constructors
        public AttendanceService()
        {
            _logEventSvc = new LogEventService();
            attDAL = new AttendanceDAL();
            _corDAL = new CoreDAL();
            _logger = LogManager.GetLogger("fileLogger");
            _schSvc = new ScheduleService();
            _uTaskDAL = new UserTaskDAL();
        }
        #endregion
        public bool ManageAttendance(AttendanceDE Attendance)
        {
            bool retVal = false;
            bool closeConnectionFlag = false;
            MySqlCommand? cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;
                if (Attendance.DBoperation == DBoperations.Insert)
                    Attendance.Id = _corDAL.GetnextId(TableNames.ATT_Attendance.ToString());
                retVal = attDAL.ManageAttendance(Attendance, cmd);
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
        public List<AttendanceDE> SearchAttendance(AttendanceDE mod)
        {
            List<AttendanceDE> att = new List<AttendanceDE>();
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
                if (mod.UserId != default && mod.UserId != "")
                    whereClause += $" and UserId like ''" + mod.UserId + "''";
                if (mod.Date.HasValue)
                    whereClause += $" AND DATE(Date) = ''{mod.Date.Value.ToString("yyyy-MM-dd")}''";
                if (mod.FromDate.HasValue)
                    whereClause += $" and Date >= ''{mod.FromDate.Value:yyyy-MM-dd} 00:00:00''";
                if (mod.ToDate.HasValue)
                    whereClause += $" and Date <= ''{mod.ToDate.Value:yyyy-MM-dd} 23:59:59''";
                if (mod.IsActive != default)
                    whereClause += $" AND IsActive ={mod.IsActive}";
                att = attDAL.SearchAttendance(whereClause, cmd);
                return att;
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

        }
        public AttendanceDE GetLastAttendanceByInTime(string userId)
        {
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;

            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                closeConnectionFlag = true;

                // Construct the query to get the last attendance record for the user based on 'InTime'
                string query = "SELECT * FROM Attendance WHERE UserId = @UserId ORDER BY InTime DESC LIMIT 1";
                cmd.CommandText = query;
                cmd.CommandType = CommandType.Text;
                cmd.Parameters.AddWithValue("@UserId", userId);

                // Execute the query
                using (MySqlDataReader reader = cmd.ExecuteReader())
                {
                    if (reader.Read())
                    {
                        // Check if 'InTime' column is DBNull
                        DateTime inTime = reader["InTime"] != DBNull.Value ? Convert.ToDateTime(reader["InTime"]) : default;

                        // Map the data from the database to the AttendanceDE model
                        AttendanceDE lastAttendance = new AttendanceDE
                        {
                            Id = Convert.ToInt32(reader["Id"]),
                            UserId = reader["UserId"].ToString(),
                            //InTime = inTime,
                            //OutTime = reader["OutTime"] != DBNull.Value ? Convert.ToDateTime (reader["OutTime"]) : default (DateTime),
                            //// Add other properties as needed
                        };

                        return lastAttendance;
                    }
                }

                // If no records found, return a default AttendanceDE object or null
                return new AttendanceDE(); // Or return null, depending on your requirements
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
        public void MarkAttendance(string userId, DateTime loginTime)
        {
            try
            {
                // Create an instance of the AttendanceDE and set the necessary properties
                AttendanceDE attendance = new AttendanceDE
                {
                    UserId = userId,
                    //InTime = loginTime,
                    DBoperation = DBoperations.Insert
                };

                // Call the ManageAttendance method to insert the record
                bool success = ManageAttendance(attendance);

                if (!success)
                {
                    // Handle the case where marking attendance fails
                    // You can log an error, throw an exception, or take other appropriate actions
                    _logger.Error("Failed to mark attendance for user ID: " + userId);
                }
            }
            catch (Exception ex)
            {
                // Log the exception and handle it as needed
                _logger.Error(ex);
            }
        }
        public double GetDueSPs(string user, DateTime inputDate)
        {
            double dueSPs = 0.00;
            string schTimes = GetScheduleTime(user, inputDate);
            //if (schTimes.Contains (','))
            //{
            var timeIntervals = schTimes.Split(',');
            foreach (var timeInterval in timeIntervals)
            {
                var times = timeInterval.Split("-");
                if (times.Length > 1)
                {
                    //foreach(var time in times)
                    {
                        var timeParts = times[0].Split(':');
                        TimeSpan startTime = new TimeSpan(Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]), 0);

                        timeParts = times[1].Split(':');
                        TimeSpan endTime = new TimeSpan(Convert.ToInt32(timeParts[0]), Convert.ToInt32(timeParts[1]), 0);

                        dueSPs += (endTime - startTime).TotalHours;
                    }
                    // var timeParts = times
                    //TimeSpan startTime = new TimeSpan((times[0].Split(':'))[0])
                }
            }
            //}

            return dueSPs;
        }
        public string GetScheduleTime(string userId, DateTime inputDate)
        {
            // Initialize an empty string to store the schedule events
            string dayEvents = string.Empty;

            // Get the user's schedule based on the provided user ID
            var userSchedule = _schSvc.GetScheduleByUserId(userId);

            // Determine the day of the week from the provided date
            int dayOfWeekId = (int)inputDate.DayOfWeek;

            // Map the day of the week to a corresponding ID
            int dayId = -1;
            switch (dayOfWeekId)
            {
                // Map days of the week to specific IDs
                case 0: dayId = 1003007; break; // Sunday
                case 1: dayId = 1003001; break; // Monday
                case 2: dayId = 1003002; break; // Tuesday
                case 3: dayId = 1003003; break; // Wednesday
                case 4: dayId = 1003004; break; // Thursday
                case 5: dayId = 1003005; break; // Friday
                case 6: dayId = 1003006; break; // Saturday
            }

            // Retrieve the schedule for the specified day
            var daySchedule = userSchedule.ScheduleDays.Where(m => m.DayId == dayId).FirstOrDefault();

            // Check if the schedule for the specified day is not null
            if (daySchedule != null)
            {
                // Iterate through the schedule events for the specified day
                foreach (var eventDetail in daySchedule.ScheduleDayEvents)
                {
                    // Concatenate the start and end times into the dayEvents string
                    dayEvents += eventDetail.StartTime + " - " + eventDetail.EndTime + " ,";
                }

                // Remove the trailing comma from the concatenated string
                if (!string.IsNullOrEmpty(dayEvents))
                {
                    dayEvents = dayEvents.TrimEnd(',', ' '); // Trim the last comma and any extra whitespace
                }
            }

            // Return the final string representing schedule events for the specified day
            return dayEvents;
        }
        public List<AttendanceDE> GetAttendanceReport(AttendanceDE mod)
        {
            List<AttendanceDE> retVals = new List<AttendanceDE>();
            retVals = SearchAttendance(new AttendanceDE { FromDate = mod.FromDate, ToDate = mod.ToDate, UserId = mod.UserId });
            foreach (var att in retVals)
            {
                List<ScheduleDayEventDE> schEvents = new List<ScheduleDayEventDE>();
                List<UserTaskDE> userTasks = new List<UserTaskDE>();
                att.Day = att.Date.Value.DayOfWeek.ToString();
                if (att.DayEndTime == null)
                    att.DayEndTime = DateTime.Now;
                #region DayStart/End
                if (att.DayStartTime.Value.Date == att.DayEndTime.Value.Date)
                    att.DayStartandEnd = att.DayStartTime.Value.TimeOfDay.ToString(AppConstants.TIME_FORMAT_HHMMSS) + " - " + att.DayEndTime.Value.TimeOfDay.ToString(AppConstants.TIME_FORMAT_HHMMSS);
                else
                    att.DayStartandEnd = att.DayStartTime.Value.ConvertToDateWith24HFormat() + " - " + att.DayEndTime.Value.ConvertToDateWith24HFormat();
                #endregion
                #region In/OutTime
                var events = _logEventSvc.SearchLogEvents(new LogEventDE { UserId = att.UserId, Date = att.Date });
                if (events != null && events.Count > 0)
                    foreach (var evt in events)
                    {
                        if (evt.OutTime == null)
                            evt.OutTime = DateTime.Now;
                        if (evt.OutTime.Value.Date == evt.InTime.Value.Date)
                            att.InandOutTime += evt.InTime.Value.TimeOfDay.ToString(AppConstants.TIME_FORMAT_HHMMSS) + " - " + evt.OutTime.Value.TimeOfDay.ToString(AppConstants.TIME_FORMAT_HHMMSS);
                        else
                            att.InandOutTime += evt.InTime.Value.ConvertToDateWith24HFormat() + " - " + evt.OutTime.Value.ConvertToDateWith24HFormat();
                        if (evt != events.Last())
                            att.InandOutTime += ",";
                    }
                #endregion
                #region ScheduleTime & Due Time
                if (att.SchDayId > 0)
                {
                    schEvents = _schSvc.GetDayEvents(att.SchDayId);
                    if (schEvents != null && schEvents.Count > 0)
                    {
                        TimeSpan dueSps = new TimeSpan();
                        foreach (var schEvt in schEvents)
                        {
                            #region ScheduleTime 
                            att.SchTime += schEvt.StartTime.ToString() + " - " + schEvt.EndTime.ToString();
                            if (schEvt != schEvents.Last())
                                att.SchTime += ",";
                            att.SchTime += Environment.NewLine;
                            #endregion
                            #region DueTime
                            dueSps += TimeSpan.Parse(schEvt.EndTime) - TimeSpan.Parse(schEvt.StartTime);
                            att.DueSPs = dueSps.ToString();
                            #endregion
                        }
                    }
                }
                #endregion
                #region Late
                if (schEvents != null && schEvents.Count > 0)
                    if (att.DayStartTime != null && schEvents[0].StartTime != null)
                    {
                        if (TimeSpan.TryParse(schEvents[0].StartTime, out var schStartTime))
                        {
                            DateTime scheduleStartDateTime = att.DayStartTime.Value.Date + schStartTime;
                            if (att.DayStartTime > scheduleStartDateTime)
                                att.Late = (att.DayStartTime - scheduleStartDateTime).ToString();
                        }
                    }
                #endregion
                #region Day WorkedTime

                if (att.DayEndTime.Value < att.DayStartTime.Value)
                    att.DayEndTime = att.DayEndTime.Value.AddDays(1);
                TimeSpan workedTime = att.DayEndTime.Value - att.DayStartTime.Value;
                att.WorkedTime = new TimeSpan(workedTime.Hours, workedTime.Minutes, workedTime.Seconds).ToString();

                #endregion
                #region Missing & Extra Time

                if (att.DueSPs != null && att.WorkedTime != null)
                {
                    TimeSpan.TryParse(att.DueSPs, out var dueSps);
                    TimeSpan.TryParse(att.WorkedTime, out var workTime);
                    if (workTime > dueSps)
                        att.ExtraTime = (workTime - dueSps).ToString();
                    else if (dueSps > workTime)
                    {
                        att.MissingTime = (dueSps - workTime).ToString();
                    }
                }

                #endregion
                #region User Targets, ClaimedSPs, DayEndStatus, Claimed%, SPsGap, ClaimError, FinalScore

                string WhereClause = $"where   DATE(Date) = ''{att.Date.Value.ToString("yyyy-MM-dd")}''   and UserId = ''" + att.UserId + "''";
                userTasks = _uTaskDAL.SearchUserTask(WhereClause);
                var _uTasks = _uTaskDAL.SearchUserTask($"where  UserId = ''" + att.UserId + "''");
                double workedSPs = 0;
                double totalSPs = 0;
                double approvedSps = 0;
                att.UserTasks = userTasks;
                foreach (var task in userTasks)
                {
                    var _task = _uTaskDAL.SearchUserTask($"where  TaskId = ''" + task.TaskId + "''");
                    if (_task != null)
                    {
                        var lastExistenceTask = _task.OrderByDescending(x => x.Id).First();
                        if (task.Id == lastExistenceTask.Id)
                            task.IsLastExistence = true;
                        _task = _task.Where(x => x.Id <= task.Id).ToList();
                        float totalClaim = (float)_task.Select(obj => obj.WorkTime).Sum();
                        task.ClaimWorkTime = string.Join("+ ", _task.Select(obj => obj.WorkTime)) + " = " + totalClaim;
                        task.ExtraTime = Math.Max(0, totalClaim - task.Sp).ToString();
                        if (totalClaim > task.Sp)
                            task.IsOverdue = true;
                        else if (totalClaim < task.Sp && (task.ApprovedClaimId > 0 ? task.ApprovedClaimId == (int)ClaimPer.Claim_100Per : task.ClaimId == (int)ClaimPer.Claim_100Per))
                        {
                            task.IsOverdue = false;
                            task.IsEarlyFinshed = true;
                        }
                    }
                    #region Task Score & Task Completion Percentage
                    if (task.ClaimId > 0)
                    {
                        double currentClaim = 0;
                        if (task.LastClaim != null)
                            currentClaim = Math.Max(0, double.Parse(task.ClaimPercent) - double.Parse(task.LastClaim));
                        else
                            currentClaim = double.Parse(task.ClaimPercent);
                        task.TaskScore = Math.Round(currentClaim * task.Sp / 100, 2).ToString();
                        task.TaskComPer = Math.Round(double.Parse(task.TaskScore) * 100 / task.Sp, 2).ToString() + " %";
                    }
                    #endregion

                    if (task.ClaimPercent != null && task.ApprovedClaimId > 0)
                        task.ClaimError = Math.Max(0, int.Parse(task.ClaimPercent) - int.Parse(task.ApprovedClaim)).ToString();

                    #region Targets

                    var stringBuilder = new StringBuilder();
                    if (task.Module != null)
                        stringBuilder.Append(task.Module + "-");
                    stringBuilder.Append(task.TaskId.ToString() + "(" + task.Sp + "SPs" + ")" + ": " + task.Title);
                    if (task != userTasks.Last())
                        stringBuilder.Append(", ");
                    att.Targets += stringBuilder;

                    #endregion

                    if (task.ClaimPercent != null)
                    {
                        #region DayEndStatus

                        var dayEndStatus = new StringBuilder();
                        if (task.Module != null)
                            dayEndStatus.Append(task.Module + "- ");
                        dayEndStatus.Append(task.TaskId.ToString() + " : " + task.ClaimPercent + "%");
                        if (task.Comments != null)
                            dayEndStatus.Append("(" + task.Comments + ")");
                        if (task != userTasks.Last())
                            dayEndStatus.Append(", ");
                        att.DayEndStatus += dayEndStatus;

                        #endregion
                        #region Claimed SPs

                        task.WorkSP = Math.Round(double.Parse(task.ClaimPercent) * task.Sp / 100, 2);
                        workedSPs += double.Parse(task.ClaimPercent) * task.Sp / 100;
                        totalSPs += task.Sp;

                        #endregion
                        #region Approved Claim

                        string approveClaim = string.Empty;
                        if (task.ApprovedClaim != null)
                            approveClaim = task.ApprovedClaim;
                        else
                            approveClaim = task.ClaimPercent;
                        task.FinalScore = Math.Round(double.Parse(approveClaim) * task.Sp / 100, 2).ToString();
                        approvedSps += double.Parse(approveClaim) * task.Sp / 100;

                        #endregion
                    }
                }
                if (workedSPs > 0 && totalSPs > 0)
                {
                    att.ClaimedSPs = Math.Round(workedSPs, 2).ToString() + "/" + Math.Round(totalSPs, 2).ToString();
                    att.FinalScore = Math.Round(approvedSps, 2).ToString() + "/" + Math.Round(totalSPs, 2).ToString();
                    #region Claimed %

                    att.ClaimPer = Math.Round(workedSPs * 100 / totalSPs, 2).ToString() + " %";

                    #endregion
                    #region SPs Gap

                    att.SPsGap = Math.Round(totalSPs - approvedSps, 2).ToString();

                    #endregion
                }
                #endregion
                #region Default Values
                if (att.Late == null)
                    att.Late = "00:00:00";
                if (att.DueSPs == null)
                    att.DueSPs = "N/A";
                if (att.SchTime == null)
                    att.SchTime = "N/A";
                if (att.ExtraTime == null)
                    att.ExtraTime = "00:00:00";
                if (att.MissingTime == null)
                    att.MissingTime = "00:00:00";
                if (att.ClaimedSPs == null)
                    att.ClaimedSPs = "N/A";
                if (att.ClaimPer == null)
                    att.ClaimPer = "N/A";
                if (att.SPsGap == null)
                    att.SPsGap = "N/A";
                if (att.FinalScore == null)
                    att.FinalScore = "N/A";
                if (att.DayEndStatus == null)
                    att.DayEndStatus = "N/A";


                #endregion
            }
            return retVals;
        }
    }
}
