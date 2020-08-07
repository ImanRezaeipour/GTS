using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Net.Mail;
using GTS.Clock.Infrastructure.Log;

namespace GTS.Clock.Business.FeatureServices
{
    class EmailUtility
    {

        private  GTSWinSvcLogger logger = new GTSWinSvcLogger();
        public void SendEmail(string to,string subject,string message) 
        {
            string from = "TA@fefalat.com";

            //string to = "kpahlevani@yahoo.com";

            System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
            mail.To.Add(to);
            mail.From = new MailAddress(from, "GTS Mail Engin", System.Text.Encoding.UTF8);
            mail.Subject = subject;
            mail.SubjectEncoding = System.Text.Encoding.UTF8;
            mail.Body = String.Format("<table><tr><td>{0}</td></tr></table>", message);
            mail.BodyEncoding = System.Text.Encoding.UTF8;
            mail.IsBodyHtml = true;
            mail.Priority = MailPriority.High;

            SmtpClient client = new SmtpClient();
            client.Credentials = new System.Net.NetworkCredential("ta@falat.net", "Ta@1234");
            client.Port = 587;
            client.Host = "smail.fefalat.com";
            client.EnableSsl = false;
            try
            {
                client.Send(mail);
            }
            catch (Exception ex)
            {
                logger.Error("", ex.Message, ex);
                throw ex;
            } 
        }
    }
}
