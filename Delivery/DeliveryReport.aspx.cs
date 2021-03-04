using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Microsoft.Reporting.WebForms;
using SecurityExtensions;
using Extensions;
using System.Configuration;
using Business;
using System.Data;
using System.Globalization;
using System.Reflection;
using AjaxControlToolkit;

namespace Delivery
{
    public partial class DeliveryReport : System.Web.UI.Page
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
            ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]); //"http://10.ABC.4.XYZ/ReportServer");
            ReportViewer1.ServerReport.ReportPath = "/Delivery/DeliveryReport";
            ReportViewer1.ShowParameterPrompts = true;
            ReportViewer1.ServerReport.SetParameters(ReportViewer1.GetCurrentParameters());
            ReportViewer1.ServerReport.Refresh();
        }

        //protected void btnSearch_Click(object sender, EventArgs e)
        //{
        //    string _FromDeliveryDate = txtFromDeliveryDate.Text.Trim();
        //    string _ToDeliveryDate = txtToDeliveryDate.Text.Trim();
        //    string _Customer = txtDeliveryTo.Text.Trim();

        //    if (_FromDeliveryDate != string.Empty || _ToDeliveryDate != string.Empty)
        //    {
        //        if (_FromDeliveryDate == string.Empty && _ToDeliveryDate != string.Empty)
        //        {
        //            _FromDeliveryDate = _ToDeliveryDate;
        //        }

        //        if (_ToDeliveryDate == string.Empty && _FromDeliveryDate != string.Empty)
        //        {
        //            _ToDeliveryDate = _FromDeliveryDate;
        //        }
        //    }

        //    DateTime FromDeliveryDate = DateTime.ParseExact(_FromDeliveryDate, "MM/dd/yyyy", CultureInfo.InvariantCulture); 
        //    DateTime ToDeliveryDate = DateTime.ParseExact(_ToDeliveryDate, "MM/dd/yyyy", CultureInfo.InvariantCulture); 

        //    IReportServerCredentials irsc = new ReportServerNetworkCredentials();
        //    ReportViewer1.ServerReport.ReportServerCredentials = irsc;
        //    ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]); //"http://10.ABC.4.XYZ/ReportServer");
        //    ReportViewer1.ServerReport.ReportPath = "/Delivery/DeliveryReport";
        //    List<ReportParameter> parameters = new List<ReportParameter>();
        //    parameters.Add(new ReportParameter("FromDeliveryDate", _FromDeliveryDate));
        //    parameters.Add(new ReportParameter("ToDeliveryDate", _ToDeliveryDate));
        //    if (_Customer != string.Empty)
        //        parameters.Add(new ReportParameter("DeliveryTo", _Customer));
        //    ReportViewer1.ServerReport.SetParameters(parameters);
        //    ReportViewer1.ProcessingMode = ProcessingMode.Remote;
        //    ReportViewer1.ShowParameterPrompts = false;
        //    ReportViewer1.ShowPromptAreaButton = false;
        //    ReportViewer1.ServerReport.Refresh();
        //}

        //private void DisplayDeliveryToCustomersInGridView()
        //{
        //    Customer customer = new Customer();
        //    customer.Alias = txtDeliveryToCustomerCode.Text;
        //    customer.CustomerName = txtDeliveryToCustomerName.Text;

        //    SearchOption searchOption = rblDeliveryToCustomerFilter.SelectedValue.ToSearchOptionEnum();

        //    gvDeliveryToCustomers.DataSource = customer.Retrieve(searchOption);
        //    gvDeliveryToCustomers.DataBind();
        //}

        //protected void btnDeliveryToCustomerSearch_Click(object sender, EventArgs e)
        //{
        //    DisplayDeliveryToCustomersInGridView();
        //}

        //protected void gvDeliveryToCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        //{
        //    try
        //    {
        //        if (e.Row.RowType == DataControlRowType.DataRow)
        //        {
        //            e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
        //            e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
        //            e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvDeliveryToCustomers, "Select$" + e.Row.RowIndex);
        //        }
        //    }
        //    catch (Exception ex)
        //    {
        //        ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
        //    }
        //}

        //protected void gvDeliveryToCustomers_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    try
        //    {
        //        GridViewRow row = gvDeliveryToCustomers.SelectedRow;
        //        String CustomerCode = gvDeliveryToCustomers.DataKeys[row.RowIndex].Values["CustomerCode"] == DBNull.Value ? "" : gvDeliveryToCustomers.DataKeys[row.RowIndex].Values["CustomerCode"].ToString();

        //        txtDeliveryTo.Text = CustomerCode;

        //        ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseDeliveryToDialog();", true);
        //    }
        //    catch (Exception ex)
        //    {
        //        ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
        //    }
        //}

        //protected void gvDeliveryToCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        //{
        //    try
        //    {
        //        gvDeliveryToCustomers.PageIndex = e.NewPageIndex;
        //        DisplayDeliveryToCustomersInGridView();
        //    }
        //    catch (Exception ex)
        //    {
        //        ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
        //    }
        //}
    }
}