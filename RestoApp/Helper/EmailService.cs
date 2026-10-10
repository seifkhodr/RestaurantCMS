using System;
using System.Configuration;
using System.Net;
using System.Net.Mail;

namespace RestoApp.Helper
{
    public class EmailService
    {
        public static void SendEmail(
            string to,
            string subject,
            string body)
        {
            string smtpHost = ConfigurationManager.AppSettings["SmtpHost"];
            int smtpPort = int.Parse(ConfigurationManager.AppSettings["SmtpPort"]);
            string smtpUser = ConfigurationManager.AppSettings["SmtpUser"];
            string smtpPass = ConfigurationManager.AppSettings["SmtpPass"];

            MailMessage mail = new MailMessage();

            mail.From = new MailAddress(smtpUser);

            mail.To.Add(to);

            mail.Subject = subject;

            mail.Body = body;

            mail.IsBodyHtml = false;

            SmtpClient smtp = new SmtpClient(smtpHost, smtpPort);

            smtp.Credentials = new NetworkCredential(smtpUser, smtpPass);

            smtp.EnableSsl = true;

            smtp.Send(mail);
        }
    }
}
