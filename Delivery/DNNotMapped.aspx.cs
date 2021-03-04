using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SecurityExtensions;
using Extensions;
using System.Configuration;

namespace Delivery
{
    public partial class DNNotMapped : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                try
                {
                    string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
                    if (string.IsNullOrEmpty(ReportServerUrl))
                    {
                        throw new Exception("Missing Report Server Url from web.config file.");
                    }
                    ViewState["ReportServerUrl"] = ReportServerUrl;
                }
                catch (Exception ex)
                {
                    this.ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
                }
            }
        }

        protected void ReportViewer_Load(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new ReportServerNetworkCredentials();
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]);
            ReportViewer1.ServerReport.ReportPath = "/Delivery/DNNotMapped";
            ReportViewer1.ShowParameterPrompts = true;
            ReportViewer1.ServerReport.SetParameters(ReportViewer1.GetCurrentParameters());
            ReportViewer1.ServerReport.Refresh();
        }
    }
}