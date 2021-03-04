using System;
using System.Configuration;
using Extensions;
using Microsoft.Reporting.WebForms;
using System.Web;
using SecurityExtensions;

namespace EDI.ForecastOrder
{
    public partial class ForecastOrderReport : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                if (!Page.IsPostBack)
                {
                    if (!HttpContext.Current.User.Identity.Name.Equals(string.Empty))
                    {
                        ViewState["Email"] = HttpContext.Current.User.Identity.Name;

                        try
                        {
                            string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
                            if (string.IsNullOrEmpty(ReportServerUrl))
                            {
                                throw new Exception("Missing Report Server Url from web.config file.");
                            }

                            LoadReportViewer(ReportServerUrl);
                        }
                        catch (Exception ex)
                        {
                            this.ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
                        }
                    }
                }
            }
        }

        private void LoadReportViewer(string ServerUrl)
        {
            //String urlReportServer = ServerUrl;
            //rvForecastOrder.ProcessingMode = ProcessingMode.Remote; // ProcessingMode will be Either Remote or Local
            //rvForecastOrder.ServerReport.ReportServerUrl = new Uri(urlReportServer); //Set the ReportServer Url
            //rvForecastOrder.ServerReport.ReportPath = "/SalesOrderReports/SOReports"; //Passing the Report Path

            String urlReportServer = ServerUrl;
            rvForecastOrder.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new ReportServerNetworkCredentials();
            rvForecastOrder.ServerReport.ReportServerCredentials = irsc;
            rvForecastOrder.ServerReport.ReportServerUrl = new Uri(urlReportServer); 
            rvForecastOrder.ServerReport.ReportPath = "/Forecast/ForecastOrder"; //"/SalesOrderReports/SOReports";
            rvForecastOrder.ShowParameterPrompts = true;
            rvForecastOrder.ServerReport.SetParameters(rvForecastOrder.GetCurrentParameters());
            rvForecastOrder.ServerReport.Refresh();
        }
    }
}