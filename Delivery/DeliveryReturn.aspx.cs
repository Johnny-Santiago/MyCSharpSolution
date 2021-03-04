using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using SecurityExtensions;
using Extensions;
using System.Configuration;
using System.IO;
using System.Data;
using Business;

namespace Delivery
{
    public partial class DeliveryReturn : System.Web.UI.Page
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

        protected void btnViewReport_Click(object sender, EventArgs e)
        {
            try
            {
                String DeliveryNoteNo = txtDeliveryNoteNo.Text;
                String FromDateReturned = txtFromDateReturned.Text;
                String ToDateReturned = txtToDateReturned.Text;

                this.ltlMessage.Text = string.Empty;
                ReportViewer1.Visible = true;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new ReportServerNetworkCredentials();
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]);
                ReportViewer1.ServerReport.ReportPath = "/Delivery/DeliveryReturn";
                ReportViewer1.ShowParameterPrompts = true;

                string nullValue = null;
                ReportParameter[] myParam = {
                                                new ReportParameter("DeliveryNoteNo", DeliveryNoteNo == string.Empty ? nullValue : DeliveryNoteNo)
                                                ,new ReportParameter("FromDateReturned", FromDateReturned == string.Empty ? nullValue : FromDateReturned)
                                                ,new ReportParameter("ToDateReturned", ToDateReturned == string.Empty ? nullValue : ToDateReturned)
                                            };

                ReportViewer1.ServerReport.SetParameters(myParam);
                ReportViewer1.ServerReport.Refresh();
            }
            catch (Exception ex)
            {
                this.ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
                ReportViewer1.Visible = false;
                lblMessage.Visible = true;
            }
        }
    }
}