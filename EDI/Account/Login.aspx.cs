using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Text.RegularExpressions;
using System.Text;
using System.Security.Cryptography;
using Extensions;

namespace EDI.Account 
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (Request.IsAuthenticated && !string.IsNullOrEmpty(Request.QueryString["ReturnUrl"]))
                    Response.Redirect("~/App_Resources/messages/UnauthorizedAccess.aspx");
            }
        }

        protected void myLogin_Authenticate(object sender, AuthenticateEventArgs e)
        {
            if (System.Web.Security.Membership.ValidateUser(myLogin.UserName, myLogin.Password))
            {
                e.Authenticated = true;
            }
            else
            {
                e.Authenticated = false;
            }
        }

        protected void myLogin_LoginError(object sender, EventArgs e)
        {
            myLogin.FailureText = "Your login attempt was not successful. Please try again.".ToErrorMessageFormat();
            System.Web.Security.MembershipUser usrInfo = System.Web.Security.Membership.GetUser(myLogin.UserName);
            if (usrInfo != null)
            {
                if (usrInfo.IsLockedOut)
                {
                    myLogin.FailureText = "Your account has been locked out because of too many invalid login attempts. Please contact MIS at local 123 to have your account unlocked.".ToErrorMessageFormat();
                }
                else if (!usrInfo.IsApproved)
                {
                    myLogin.FailureText = "Your account has not yet been approved. You cannot login until an administrator has approved your account.<br>To have your account approved, please contact MIS at local 123.".ToErrorMessageFormat();
                }
            }
        }
    }
}