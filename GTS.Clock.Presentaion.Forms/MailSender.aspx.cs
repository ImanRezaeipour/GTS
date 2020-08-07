using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

public partial class MailSender : System.Web.UI.Page
{
    protected void Page_Load(object sender, EventArgs e)
    {

    }
    protected void btnSend_Click(object sender, EventArgs e)
    {
        string from = "TA@fefalat.com";

        string to = "kpahlevani@yahoo.com";

        System.Net.Mail.MailMessage mail = new System.Net.Mail.MailMessage();
        mail.To.Add(to);
        mail.From = new MailAddress(from, "GTS Mail Engin", System.Text.Encoding.UTF8);
        mail.Subject = "This is a test mail";
        mail.SubjectEncoding = System.Text.Encoding.UTF8;
        mail.Body = "<table><tr><td>This is Email Body Text</td></tr></table>";
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
            Exception ex2 = ex;
            string errorMessage = string.Empty;
            while (ex2 != null)
            {
                errorMessage += ex2.ToString();
                ex2 = ex2.InnerException;
            }
            HttpContext.Current.Response.Write(errorMessage);
        } 

    }
}