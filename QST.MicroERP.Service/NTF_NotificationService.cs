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
using QST.MicroERP.DAL.TMS;

namespace QST.MicroERP.Service
{
    public class NTF_NotificationService:BaseService
    {
        #region Class Members/Class Variables

        private Dictionary<string, string> _ntfDict; // Notificaitons Dictionary
        private NotificationTemplateDAL _nTemDAL;
        private NotificationLogDAL _nLogDAL;

        #endregion
        public NTF_NotificationService()
        {
            _ntfDict = new Dictionary<string, string>();
            _nTemDAL = new NotificationTemplateDAL();
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
        public async Task<bool> SendEmail ( NotificationLogDE mail )
        {
            bool retVal = false;
            MailMessage message = new MailMessage ();
            SmtpClient smtp = new SmtpClient ();

            message.From = new MailAddress (MailConstants.SENDER, MailConstants.SENDER_NAME);
            message.To.Add (new MailAddress (mail.To));
            message.Subject = mail.Subject;
            message.IsBodyHtml = true;
            message.BodyEncoding = Encoding.UTF8;
            message.Body = mail.Body;
            smtp.Host = MailConstants.HOST;
            smtp.Port = MailConstants.PORT;
            smtp.Credentials = new System.Net.NetworkCredential ()
            {
                UserName = MailConstants.USER_NAME,
                Password = MailConstants.PASSWORD
            };
            smtp.EnableSsl = false;

            TaskCompletionSource<bool> tcs = new TaskCompletionSource<bool> ();

            smtp.SendCompleted += ( sender, e ) =>
            {
                if (e.Error == null && !e.Cancelled)
                {
                    retVal = true;
                    tcs.SetResult (true);
                }
                else
                {
                    tcs.SetResult (false);
                }
            };
            try
            {
                await smtp.SendMailAsync (message);
                retVal = true; 
            }
            catch (Exception ex)
            {
                Console.WriteLine ("Exception while sending email: " + ex.Message);
                retVal = false;
            }

            return await tcs.Task;
        }

        //public bool SendEmail(NotificationLogDE mail)
        //{
        //    bool retVal = false;
        //    try
        //    {
        //        MailMessage message = new MailMessage();
        //        SmtpClient smtp = new SmtpClient();
        //        message.From = new MailAddress(MailConstants.SENDER);
        //        message.To.Add(new MailAddress(mail.To));
        //        message.Subject = mail.Subject;
        //        message.IsBodyHtml = true;
        //        message.BodyEncoding = Encoding.UTF8;
        //        message.Body = mail.Body;
        //        smtp.Host = MailConstants.HOST;
        //        smtp.Port = MailConstants.PORT;
        //        smtp.Credentials = new System.Net.NetworkCredential()
        //        {
        //            UserName = MailConstants.USER_NAME,
        //            Password = MailConstants.PASSWORD
        //        };
        //        smtp.EnableSsl = false;
        //        smtp.SendAsync(message, null);

        //        smtp.SendCompleted += (sender, e) =>
        //        {
        //            //if (e.error != null)
        //            //    console.writeline($"error sending email: {e.error.message}");
        //            //else if (e.cancelled)
        //            //    console.writeline("email sending canceled.");
        //            //else
        //            //    console.writeline("email sent successfully.");
        //            if (e.Error == null && !e.Cancelled)
        //                retVal = true;
        //        };

        //        //smtp.SendCompleted += EmailSendingResult(sender, e);
        //    }
        //    catch (Exception ex)
        //    {
        //        _logger.Error(ex);
        //        throw;
        //    }

        //    return retVal;
        //}
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
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                _entity = TableNames.NTF_NotificationTemplate.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");

                _logger.Info ($"Going to Call:_nTemDAL.ManageNotificationTemplate(mod)");
                if (_nTemDAL.ManageNotificationTemplate (mod))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    return true;
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<NotificationTemplateDE> SearchNotificationTemplates(NotificationTemplateSearchCriteria sc)
        {
            List<NotificationTemplateDE> mod = new List<NotificationTemplateDE>();
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                #region Search

                string whereClause = " Where 1=1";
                if (sc.Id != default)
                    whereClause += $" AND Id={sc.Id}";
                if (sc.ClientId != default && sc.ClientId != 0)
                    whereClause += $" AND ClientId={sc.ClientId}";
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
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return mod;
        }

        #endregion

        #region NotificationLog
        public bool ManagementNotificationLog(NotificationLogDE mod)
        {
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();
                _entity = TableNames.NTF_NotificationLog.ToString ();

                if (mod.DBoperation == DBoperations.Insert)
                    mod.Id = _coreDAL.GetNextIdByClient (_entity, mod.ClientId, "ClientId");

                _logger.Info ($"Going to Call:_nLogDAL.ManageNotificationLog(mod)");
                if (_nLogDAL.ManageNotificationLog (mod))
                {
                    mod.AddSuccessMessage (string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    _logger.Info ($"Success: " + string.Format (AppConstants.CRUD_DB_OPERATION, _entity, mod.DBoperation.ToString ()));
                    return true;
                }
                else
                {
                    mod.AddErrorMessage (string.Format (AppConstants.CRUD_ERROR, _entity));
                    _logger.Info ($"Error: " + string.Format (AppConstants.CRUD_ERROR, _entity));
                    return false;
                }
            }
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
        }
        public List<NotificationLogDE> SearchNotificationLogs(NotificationLogSearchCriteria sc)
        {
            List<NotificationLogDE> NotificationLog = new List<NotificationLogDE>();
            try
            {
                cmd = MicroERPDataContext.OpenMySqlConnection();

                #region Search

                string whereClause = " Where 1=1";
                if (sc.ClientId != default)
                    whereClause += $" AND ClientId={sc.ClientId}";
                if (sc.Id != default)
                    whereClause += $" AND Id={sc.Id}";
                if (sc.ClientId != default && sc.ClientId != 0)
                    whereClause += $" AND ClientId={sc.ClientId}";
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
            catch (Exception ex)
            {
                _logger.Error (ex);
                throw;
            }
            finally
            {
                if (cmd != null)
                    MicroERPDataContext.CloseMySqlConnection (cmd);
            }
            return NotificationLog;
        }

        #endregion

        #region Generate Notifications

        public NotificationLogDE GenerateNotification(NotificationTemplates template, BaseDomain domain)
        {
            NotificationLogDE mod = new NotificationLogDE();
            var ntfTemplate = new NotificationTemplateDE ();
            var ntSc = new NotificationTemplateSearchCriteria ();
            var att = (AttendanceDE)domain;
            _ntfDict.Clear();

            switch (template)
            {
                case NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart:
                    mod.ClientId = att.ClientId;
                    mod.UserId = att.UserId;
                    mod.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart.ToString();
                    mod.DBoperation = DBoperations.Insert;
                    mod.IsSent = false;

                    ntSc.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayStart.ToString();
                     ntfTemplate = SearchNotificationTemplates(ntSc).FirstOrDefault();
                    if (ntfTemplate != null &&  ntfTemplate.Id > 0)
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
                            mod = mod.AddTaskLine(lineBaseContent, _ntfDict, sectionNo);
                        });
                        //ntfTemplate.T
                        //var ntfTempTaskDetail = 
                    }

                    break;
                case NotificationTemplates.ATT_NotificationToSupervisor_OnDayEnd:
                    mod.ClientId = att.ClientId;
                    mod.UserId = att.UserId;
                    mod.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayEnd.ToString ();
                    mod.DBoperation = DBoperations.Insert;
                    mod.IsSent = false;
                    ntSc.KeyCode = NotificationTemplates.ATT_NotificationToSupervisor_OnDayEnd.ToString ();
                     ntfTemplate = SearchNotificationTemplates (ntSc).FirstOrDefault ();
                    if (ntfTemplate!=null && ntfTemplate.Id > 0)
                    {
                        ntSc.KeyCode = NotificationTemplates.ATT_DayEndTaskDetail.ToString ();
                        var ntfTempTaskDetail = SearchNotificationTemplates (ntSc).FirstOrDefault ();
                        att.Translate (ref _ntfDict);
                        mod = ntfTemplate.Translate (_ntfDict);
                        mod.Body = mod.Body.Replace ("#DAYENDSTATUS#", ntfTempTaskDetail.Body);
                        var lineBaseContent = mod.Body;
                        var mode = NotificationTemplateModes.Add;

                        int sectionNo = 0;
                        att.UserTasks.ForEach (task =>
                        {
                            sectionNo += 1;
                            _ntfDict.Clear ();
                            task.Translate (ref _ntfDict);
                            mod = mod.AddTaskLine (lineBaseContent, _ntfDict, sectionNo);
                        });
                    }
                    break;
            }
            return mod;
        }

        #endregion

        public async Task SendNotificationsAsync ()
        {
            var notificationsToSend = SearchNotificationLogs(new NotificationLogSearchCriteria
            {
                IsSent = false
            });

            foreach (var ntf in notificationsToSend)
            {
                if (await SendEmail(ntf))
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
