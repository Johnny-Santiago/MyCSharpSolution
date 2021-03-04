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
using System.Globalization;

namespace EDI.Forecast
{
    public partial class ForecastExport : System.Web.UI.Page
    {
        private string[] formats = { "MM/dd/yyyy" };

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

        private void DisplayCustomersInGridView()
        {
            Customer customer = new Customer();
            customer.Alias = txtAlias.Text;
            customer.CustomerName = txtCustomerName.Text;

            Business.SearchOption searchOption = rblCustomerFilter.SelectedValue.ToSearchOptionEnum();

            gvCustomers.DataSource = customer.Retrieve(searchOption);
            gvCustomers.DataBind();
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e)
        {
            DisplayCustomersInGridView();
        }

        protected void gvCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvCustomers, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvCustomers.SelectedRow;
                String CustomerCode = gvCustomers.DataKeys[row.RowIndex].Values["CustomerCode"] == DBNull.Value ? "" : gvCustomers.DataKeys[row.RowIndex].Values["CustomerCode"].ToString();

                txtCustomerCode.Text = CustomerCode;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvCustomers.PageIndex = e.NewPageIndex;
                DisplayCustomersInGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void btnDownloadTemplate_Click(object sender, EventArgs e)
        {
            try
            {
                QuotationFile quotationFile = new QuotationFile();
                quotationFile.CustomerCode = txtCustomerCode.Text;
                quotationFile.ForecastDate = DateTime.ParseExact(txtPeriod.Text.Insert(3, "01/"), formats, CultureInfo.InvariantCulture, DateTimeStyles.None);
                quotationFile.SysCreator = HttpContext.Current.User.Identity.Name;
                quotationFile.Export();

                this.ltlMessage.Text = string.Empty;
                ReportViewer1.ProcessingMode = ProcessingMode.Remote;
                IReportServerCredentials irsc = new ReportServerNetworkCredentials();
                ReportViewer1.ServerReport.ReportServerCredentials = irsc;
                ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]);
                ReportViewer1.ServerReport.ReportPath = "/Forecast/ForecastTemplate";

                ReportParameter[] myParam = {
                                                new ReportParameter("CustomerCode", quotationFile.CustomerCode)
                                                ,new ReportParameter("ForecastDate", string.Format("{0:MM/dd/yyyy}", quotationFile.ForecastDate))
                                                ,new ReportParameter("SysCreator",quotationFile.SysCreator)
                                            };

                ReportViewer1.ServerReport.SetParameters(myParam);
                ReportViewer1.ServerReport.Refresh();

                string mimeType;
                string encoding;
                string extension;
                string[] streams;
                Warning[] warnings;
                byte[] bytes = ReportViewer1.ServerReport.Render("Excel", string.Empty, out mimeType, out encoding, out extension, out streams, out warnings);

                Response.Clear();
                Response.ContentType = "application/excel";
                Response.AddHeader("Content-disposition", string.Format("filename={0:yyyyMM}_{1}_Forecast.xls", quotationFile.ForecastDate, quotationFile.CustomerCode));
                Response.AddHeader("Content-Length", bytes.Length.ToString());
                Response.OutputStream.Write(bytes, 0, bytes.Length);
                Response.OutputStream.Flush();
                Response.OutputStream.Close();
                Response.Flush();
                Response.Close();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void ReportViewer_Load(object sender, EventArgs e)
        {
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new ReportServerNetworkCredentials();
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]);
            ReportViewer1.ServerReport.ReportPath = "/Forecast/ForecastTemplate";
            ReportViewer1.ShowParameterPrompts = true;
            ReportViewer1.ServerReport.SetParameters(ReportViewer1.GetCurrentParameters());
            ReportViewer1.ServerReport.Refresh();
        }
    }
}