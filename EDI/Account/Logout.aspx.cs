using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;

namespace EDI.Account 
{
    public partial class Logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "<p>You have been logged out of the system. To log in, please return to the <a href='Login.aspx'> login page</a></p>".ToNoticeMessageFormat();
        }
    }
}