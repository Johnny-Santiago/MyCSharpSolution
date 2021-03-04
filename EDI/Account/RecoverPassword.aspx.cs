using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Net.Mail;

namespace EDI.Account 
{
    public partial class RecoverPassword : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void PasswordRecovery1_UserLookupError(object sender, EventArgs e)
        {
            PasswordRecovery1.UserNameTemplateContainer.FindControl("divfailureText").Visible = true;
        }

        protected void PasswordRecovery1_AnswerLookupError(object sender, EventArgs e)
        {
            PasswordRecovery1.QuestionTemplateContainer.FindControl("divfailureText").Visible = true;
        }

        protected void PasswordRecovery_SendingMail(object sender, MailMessageEventArgs e)
        {
            MailMessage msg = new MailMessage();
            msg.To.Add(new MailAddress(e.Message.To.ToString()));
            msg.From = new MailAddress(e.Message.From.ToString());
            msg.Subject = e.Message.Subject;
            msg.IsBodyHtml = false;
            msg.Body = e.Message.Body;
            SmtpClient smtp = new SmtpClient();
            smtp.EnableSsl = true;
            smtp.Send(msg);
            e.Cancel = true;
        }
    }
}