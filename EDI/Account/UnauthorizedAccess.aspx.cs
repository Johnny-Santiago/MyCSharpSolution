using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;

namespace EDI.Account 
{
    public partial class UnauthorizedAccess : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            labelMessage.Text = "<p>You have attempted to access a page that you are not authorized to view.</p><p>If you have any questions, please contact the site administrator</p>".ToErrorMessageFormat();
        }
    }
}