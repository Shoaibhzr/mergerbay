using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Net.Mail;
using System.Text;

namespace MergerBay.Utilities.Services.SMTP_Services
{
    public static class EmailServices
    {
        public static void Send_Email(string MessageTo,string Subject,string body, List<string> CC=null,List<string> BCC=null, MemoryStream ms = null)
        {
            try
            {
                CC  = new List<string>();
                BCC = new List<string>();

                MailAddress from = new MailAddress(Config.From_Email, "NoReply");
                MailAddress to = new MailAddress(MessageTo);
                MailMessage message = new MailMessage(from, to);
                message.Subject = Subject;
                message.Body = body;
                message.IsBodyHtml = true;
                if (CC.Count > 0)
                {
                    for (int i = 0; i <= CC.Count - 1; i++)
                    {
                        MailAddress copycc = new MailAddress(CC[i]);
                        message.CC.Add(copycc);
                    }
                }
                if (BCC.Count> 0)
                {
                    for (int i = 0; i <= BCC.Count - 1; i++)
                    {
                        MailAddress copybcc = new MailAddress(BCC[i]);
                        message.Bcc.Add(copybcc);
                    }
                }
                if (ms != null)
                {
                    ms.Position = 0;
                    Attachment attachment = new Attachment(ms, "fileName.pdf", "application/pdf");
                    message.Attachments.Add(attachment);
                }
                //smtp.gmail.com
                NetworkCredential NetworkCred = new NetworkCredential();
               
                NetworkCred.UserName = Config.Login_UserName;
                NetworkCred.Password = Config.Login_Password;
                string server = Config.Server;
                SmtpClient client = new SmtpClient(server);
                client.UseDefaultCredentials = false;
                 client.Port = Config.Port;
               // client.Port = 25;
                client.EnableSsl = Config.Is_SSL;
                client.Credentials = NetworkCred;
                client.Send(message);
            }
            catch (Exception ex)
            {
                throw;
            }
        }
        public static void sendEmail(string body, string ToEmail)
        {
            try
            {
                string sdrEmail = "developer@meggafone.com";
                string sdrPass = "Meggafone2020";
                //smtp.gmail.com
                SmtpClient client = new SmtpClient("smtp.office365.com");
                client.Port = 587;
                client.EnableSsl = true;
               
                client.Timeout = 10000000;
                client.DeliveryMethod = SmtpDeliveryMethod.Network;
                client.UseDefaultCredentials = true;
                client.Credentials = new NetworkCredential(sdrEmail, sdrPass);
                MailMessage mail = new MailMessage(sdrEmail, ToEmail, "Email Confirmation", body);
                mail.IsBodyHtml = true;
                mail.BodyEncoding = UTF8Encoding.UTF8;
                client.Send(mail);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
