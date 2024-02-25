using QST.MicroERP.Core.Entities.NTF;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using QST.MicroERP.Core.Enums;
using QST.MicroERP.DAL.CTL;
using QST.MicroERP.DAL;
using MySql.Data.MySqlClient;
using QST.MicroERP.DAL.NTF;
using QST.MicroERP.Core.Entities;
using QST.MicroERP.Core.Entities.ATT;
using QST.MicroERP.Core.Translators.NTF;
using Ubiety.Dns.Core.Common;
using QST.MicroERP.Core.Constants;

namespace QST.MicroERP.Services.NTF
{
    public class NotificationService
    {
        #region Class Members/Class Variables
        
        private Logger _logger;
        private Dictionary<string, string> _ntfDict; // Notificaitons Dictionary

        private NotificationTemplateDAL _nTemDAL;
        private CoreDAL _corDAL;
        private NotificationLogDAL _nLogDAL;

        #endregion
        public NotificationService()
        {
            _logger = LogManager.GetLogger("fileLogger");
            _ntfDict = new Dictionary<string, string>();
            _nTemDAL = new NotificationTemplateDAL();
            _corDAL = new CoreDAL();
            _nLogDAL = new NotificationLogDAL();
        }

        #region Send Email
        public void SendEmail(NotificationDE mail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(MailConstants.SENDER);
                message.To.Add(new MailAddress(mail.ReceiverMail));
                message.Subject = mail.MailSubject;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = mail.MailBody;
                smtp.Host = MailConstants.HOST;
                smtp.Port = MailConstants.PORT;
                smtp.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = MailConstants.USER_NAME,
                    Password = MailConstants.PASSWORD
                };
                smtp.EnableSsl = false;
                smtp.SendAsync(message, null);

                smtp.SendCompleted += (sender, e) =>
                {
                    if (e.Error != null)
                        Console.WriteLine($"Error sending email: {e.Error.Message}");
                    else if (e.Cancelled)
                        Console.WriteLine("Email sending canceled.");
                    else
                        Console.WriteLine("Email sent successfully.");
                };

                //smtp.SendCompleted += EmailSendingResult(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }
        public bool SendEmail(NotificationLogDE mail)
        {
            bool retVal = false;
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                message.From = new MailAddress(MailConstants.SENDER);
                message.To.Add(new MailAddress(mail.To));
                message.Subject = mail.Subject;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = mail.Body;
                smtp.Host = MailConstants.HOST;
                smtp.Port = MailConstants.PORT;
                smtp.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = MailConstants.USER_NAME,
                    Password = MailConstants.PASSWORD
                };
                smtp.EnableSsl = false;
                smtp.SendAsync(message, null);

                smtp.SendCompleted += (sender, e) =>
                {
                    //if (e.error != null)
                    //    console.writeline($"error sending email: {e.error.message}");
                    //else if (e.cancelled)
                    //    console.writeline("email sending canceled.");
                    //else
                    //    console.writeline("email sent successfully.");
                    if (e.Error == null && !e.Cancelled)
                        retVal = true; 
                };

                //smtp.SendCompleted += EmailSendingResult(sender, e);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }

            return retVal;
        }


        public void SendEmailWithAttachment(IFormFile? pdfFile, NotificationDE mail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
                if (mail.Attachment != null)
                {
                    Attachment attachment = new Attachment(pdfFile.OpenReadStream(), "VoucherReport"); // Replace with the actual file path
                    attachment.ContentType.MediaType = "application/pdf"; // Set the MIME type to PDF
                    message.Attachments.Add(attachment);
                }
                message.From = new MailAddress("tech@qamsoft.com");
                message.To.Add(new MailAddress(mail.ReceiverMail));
                message.Subject = mail.MailSubject;
                message.IsBodyHtml = true;
                message.BodyEncoding = Encoding.UTF8;
                message.Body = mail.MailBody;
                smtp.Host = "mail.qamsoft.com";
                smtp.Port = 26;
                smtp.Credentials = new System.Net.NetworkCredential()
                {
                    UserName = "tech@qamsoft.com",
                    Password = "Qamsoft#123"
                };
                smtp.EnableSsl = false;
                smtp.SendCompleted += (sender, e) =>
                {
                    if (e.Error != null)
                        Console.WriteLine($"Error sending email: {e.Error.Message}");
                    else if (e.Cancelled)
                        Console.WriteLine("Email sending canceled.");
                    else
                        Console.WriteLine("Email sent successfully.");
                };

                smtp.SendAsync(message, null);
            }
            catch (Exception ex)
            {
                _logger.Error(ex);
                throw;
            }
        }

        #endregion

        #region NotificationTemplate
        public bool ManagementNotificationTemplate(NotificationTemplateDE mod)
        {
            MySqlCommand cmd = null;
            try
            {
                bool check = true;
                cmd = MicroERPDataContext.OpenMySqlConnection();


                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _corDAL.GetnextId(TableNames.notificationtemplate.ToString());
                    check = _nTemDAL.ManageNotificationTemplate(mod);
                }
                else if (mod.DBoperation == DBoperations.Update)
                {
                    check = _nTemDAL.ManageNotificationTemplate(mod);
                }
                else if (mod.DBoperation == DBoperations.Delete)
                {
                    check = _nTemDAL.AlterNotificationTemplate(mod, mod.Id);
                }
                else if (mod.DBoperation == DBoperations.Activate)
                {
                    check = _nTemDAL.AlterNotificationTemplate(mod, mod.Id);
                }
                else if (mod.DBoperation == DBoperations.DeActivate)
                {
                    check = _nTemDAL.AlterNotificationTemplate(mod, mod.Id);
                }
                if (check == true)
                    mod.DBoperation = DBoperations.NA;


                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }


        }
        public List<NotificationTemplateDE> SearchNotificationTemplates(NotificationTemplateSearchCriteria sc)
        {
            List<NotificationTemplateDE> mod = new List<NotificationTemplateDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();


                #region Search

                string whereClause = " Where 1=1";
                if (sc.Id != default)
                    whereClause += $" AND Id={sc.Id}";
                if (sc.KeyCode != default)
                    whereClause += $" AND KeyCode like ''" + sc.KeyCode + "''";
                if (sc.TemplateName != default)
                    whereClause += $" AND TemplateName like ''" + sc.TemplateName + "''";
                if (sc.Subject != default)
                    whereClause += $" AND Subject like ''" + sc.Subject + "''";
                if (sc.IsActive != default)
                    whereClause += $" AND IsActive ={sc.IsActive}";

                mod = _nTemDAL.SearchNotificationTemplates(whereClause);

                #endregion


            }
            catch (Exception exp)
            {

                throw exp;

            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }
            return mod;
        }

        #endregion

        #region NotificationLog
        public bool ManagementNotificationLog(NotificationLogDE mod)
        {
            MySqlCommand cmd = null;
            try
            {
                bool check = true;
                cmd = MicroERPDataContext.OpenMySqlConnection();


                if (mod.DBoperation == DBoperations.Insert)
                {
                    mod.Id = _corDAL.GetnextId(TableNames.NTF_NotificationLog.ToString());
                    check = _nLogDAL.ManageNotificationLog(mod);
                }
                else if (mod.DBoperation == DBoperations.Update)
                {
                    check = _nLogDAL.ManageNotificationLog(mod);
                }
                else if (mod.DBoperation == DBoperations.Delete)
                {
                    check = _nLogDAL.AlterNotificationLog(mod, mod.Id);
                }
                else if (mod.DBoperation == DBoperations.Activate)
                {
                    check = _nLogDAL.AlterNotificationLog(mod, mod.Id);
                }
                else if (mod.DBoperation == DBoperations.DeActivate)
                {
                    check = _nLogDAL.AlterNotificationLog(mod, mod.Id);
                }
                if (check == true)
                    mod.DBoperation = DBoperations.NA;


                return true;
            }
            catch
            {
                return false;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }


        }
        public List<NotificationLogDE> SearchNotificationLogs(NotificationLogSearchCriteria sc)
        {
            List<NotificationLogDE> NotificationLog = new List<NotificationLogDE>();
            bool closeConnectionFlag = false;
            MySqlCommand cmd = null;
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                #region Search

                string whereClause = " Where 1=1";
                if (sc.ClientId != default)
                    whereClause += $" AND ClientId={sc.ClientId}";
                if (sc.Id != default)
                    whereClause += $" AND Id={sc.Id}";
                if (!string.IsNullOrWhiteSpace(sc.Subject))
                    whereClause += $" AND Subject like ''" + sc.Subject + "''";
                if (!string.IsNullOrWhiteSpace(sc.Body))
                    whereClause += $" AND Body like ''" + sc.Body + "''";
                if (!string.IsNullOrWhiteSpace(sc.UserId))
                    whereClause += $" AND Name like ''" + sc.UserId + "''";
                if (sc.IsSent != default)
                    whereClause += $" AND IsSent ={sc.IsSent}";
                if (sc.IsActive != default)
                    whereClause += $" AND IsActive ={sc.IsActive}";

                NotificationLog = _nLogDAL.SearchNotificationLogs(whereClause);

                #endregion
            }
            catch (Exception exp)
            {
                throw exp;
            }
            finally
            {
                if (closeConnectionFlag)
                    MicroERPDataContext.CloseMySqlConnection(cmd);
            }

            return NotificationLog;
        }

        #endregion

        #region Generate Notifications

        public NotificationLogDE GenerateNotification(NotificationTemplates template, BaseDomain domain)
        {
            NotificationLogDE mod = new NotificationLogDE();
            
            _ntfDict.Clear();

            switch (template) 
            {
                case NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart:
                    var att = (AttendanceDE)domain;
                    mod.ClientId = att.ClientId;
                    mod.UserId = att.UserId;
                    mod.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart.ToString();
                    mod.DBoperation = DBoperations.Insert;
                    mod.IsSent = false;
                    
                    var ntSc = new NotificationTemplateSearchCriteria();
                    ntSc.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart.ToString();
                    var ntfTemplate = SearchNotificationTemplates(ntSc).FirstOrDefault();
                    if (ntfTemplate.Id > 0)
                    {
                        ntSc.KeyCode = NotificationTemplates.ATT_TaskDetail.ToString();
                        var ntfTempTaskDetail = SearchNotificationTemplates(ntSc).FirstOrDefault();
                        att.Translate(ref _ntfDict);
                        mod = ntfTemplate.Translate(_ntfDict);
                        mod.Body = mod.Body.Replace("#TASKS#", ntfTempTaskDetail.Body);
                        var lineBaseContent = mod.Body;
                        //NotificationTemplateModes
                        var mode = NotificationTemplateModes.Add;


                        int sectionNo = 0;
                        att.UserTasks.ForEach(task =>
                        {
                            //var lineBaseContent = mod.Body;
                            sectionNo += 1;
                            //var sectionNo = leaveVM.DBOperation == DBOperations.NA ? 2 : 1;
                            
                            _ntfDict.Clear();
                            task.Translate(ref _ntfDict);
                            mod = mod.AddTaskLine(lineBaseContent,_ntfDict,sectionNo);
                        });
                        //ntfTemplate.T
                        //var ntfTempTaskDetail = 
                    }

                    break;
                case NotificationTemplates.ATT_NotificationToSupersor_OnDayEnd: break;
            }

            return mod;
        }

        #endregion

        public void SendNotifications()
        {
            var notificationsToSend = SearchNotificationLogs(new NotificationLogSearchCriteria { 
             IsSent = false,
             ClientId = 1
            });
            
            foreach(var ntf in notificationsToSend)
            {
                //if(SendEmail(ntf))
                {
                    // Set IsSent Flag to True
                    ntf.IsSent = true;
                    ntf.DBoperation = DBoperations.Update;
                    ManagementNotificationLog(ntf);
                }
            }
        }
    }
}
