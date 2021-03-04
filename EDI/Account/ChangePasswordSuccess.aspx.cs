using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;

namespace EDI.Account 
{
    public partial class ChangePasswordSuccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "<p>Your password has been changed successfully. An email confirmation has been sent to your email account.</p>".ToNoticeMessageFormat();
        }
    }
}