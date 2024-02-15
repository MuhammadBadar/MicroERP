using MicroERP.Core.Entities.NTF;
using Microsoft.AspNetCore.Http;
using NLog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace MicroERP.Services.NTF
{
    public class NotificationService
    {
        private Logger _logger;
        public NotificationService()
        {
            _logger = LogManager.GetLogger("fileLogger");
        }
        public void SendEmail(NotificationDE mail)
        {
            try
            {
                MailMessage message = new MailMessage();
                SmtpClient smtp = new SmtpClient();
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
    }
}
