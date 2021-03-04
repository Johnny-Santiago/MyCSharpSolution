using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using Extensions;
using Business;
using System.Data;
using System.Globalization;

namespace Delivery
{
    public partial class SalesOrders : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!this.Page.IsPostBack)
            {
                this.btnAdvance.Text = ((this.HiddenField1.Value == "true") ? "Advance" : "Simple");
            }
        }

        private void DisplaySalesOrdersInGridView()
        {
            SalesOrder so = new SalesOrder();

            string FromOrderDate = txtOrderDate.Text.Trim();
            string ToOrderDate = txtOrderDate2.Text.Trim(); 

            if (FromOrderDate != string.Empty || ToOrderDate != string.Empty)
            {
                if (FromOrderDate == string.Empty && ToOrderDate != string.Empty)
                {
                    FromOrderDate = ToOrderDate;
                }

                if (ToOrderDate == string.Empty && FromOrderDate != string.Empty)
                {
                    ToOrderDate = FromOrderDate;
                }

                so.OrderDate = DateTime.ParseExact(FromOrderDate, "MM/dd/yyyy", CultureInfo.InvariantCulture); 
                so.OrderDate2 = DateTime.ParseExact(ToOrderDate, "MM/dd/yyyy", CultureInfo.InvariantCulture); 
            }
            
            so.OrderNumber = txtOrderNumber.Text;
            so.OrderBy = txtOrderBy.Text;
            so.InvoiceTo = txtInvoiceTo.Text;
            so.YourRef = txtYourRef.Text;
            so.SelectionCode = txtSelectionCode.Text;
            SearchOption searchOption = rblFilter.SelectedValue.ToSearchOptionEnum();

            gvSalesOrders.DataSource = so.Retrieve(searchOption);
            gvSalesOrders.DataBind();
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            DisplaySalesOrdersInGridView();
        }

        private void DisplayCustomersInGridView()
        {
            Customer customer = new Customer();
            customer.Alias = txtAlias.Text;
            customer.CustomerName = txtCustomerName.Text;

            SearchOption searchOption = rblCustomerFilter.SelectedValue.ToSearchOptionEnum();

            gvCustomers.DataSource = customer.Retrieve(searchOption);
            gvCustomers.DataBind();
        }

        protected void btnCustomerSearch_Click(object sender, EventArgs e) 
        {
            DisplayCustomersInGridView();
        }

        protected void gvSalesOrders_PageIndexChanging(object sender, GridViewPageEventArgs e) 
        {
            try
            {
                gvSalesOrders.PageIndex = e.NewPageIndex;
                DisplaySalesOrdersInGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
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

                txtOrderBy.Text = CustomerCode;

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

        private void DisplaySalesOrdersInBrowserGridView()
        {
            SalesOrder so = new SalesOrder();

            so.YourRef = txtYourRef2.Text;
            so.Description = txtDescription.Text;
            //so.SelectionCode = txtSelectionCode.Text;
            SearchOption searchOption = rblSalesOrderFilter.SelectedValue.ToSearchOptionEnum();

            gvBrowseSalesOrders.DataSource = so.Retrieve(searchOption);
            gvBrowseSalesOrders.DataBind();
        }

        protected void btnSearchSOBrowser_Click(object sender, EventArgs e)
        {
            DisplaySalesOrdersInBrowserGridView();
        }

        protected void gvBrowseSalesOrders_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvBrowseSalesOrders, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvBrowseSalesOrders_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvBrowseSalesOrders.SelectedRow;
                String OrderNumber = gvBrowseSalesOrders.DataKeys[row.RowIndex].Values["OrderNumber"] == DBNull.Value ? "" : gvBrowseSalesOrders.DataKeys[row.RowIndex].Values["OrderNumber"].ToString();

                txtOrderNumber.Text = OrderNumber;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseSODialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvBrowseSalesOrders_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvBrowseSalesOrders.PageIndex = e.NewPageIndex;
                DisplaySalesOrdersInBrowserGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        private void DisplaySelectionCodesInBrowserGridView()
        {
            SelectionCode sc = new SelectionCode();

            sc.selectionCode = txtSelectionCode2.Text;
            sc.Description = txtSelectionCodeDescription.Text;
            SearchOption searchOption = rblSalesOrderFilter.SelectedValue.ToSearchOptionEnum();

            gvSelectionCodes.DataSource = sc.Retrieve(searchOption);
            gvSelectionCodes.DataBind();
        }

        protected void btnSelectionCodeSearch_Click(object sender, EventArgs e) 
        {
            DisplaySelectionCodesInBrowserGridView();
        }

        protected void gvSelectionCodes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvSelectionCodes, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvSelectionCodes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvSelectionCodes.SelectedRow;
                String SelectionCode = gvSelectionCodes.DataKeys[row.RowIndex].Values["SelectionCode"] == DBNull.Value ? "" : gvSelectionCodes.DataKeys[row.RowIndex].Values["SelectionCode"].ToString();

                txtSelectionCode.Text = SelectionCode;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseSelectionCodeDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvSelectionCodes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvSelectionCodes.PageIndex = e.NewPageIndex;
                DisplaySalesOrdersInBrowserGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        private void DisplayInvoiceToCustomersInGridView()
        {
            Customer customer = new Customer();
            customer.Alias = txtInvoiceToCustomerCode.Text;
            customer.CustomerName = txtInvoiceToCustomerName.Text;

            SearchOption searchOption = rblInvoiceToCustomerFilter.SelectedValue.ToSearchOptionEnum();

            gvInvoiceToCustomers.DataSource = customer.Retrieve(searchOption);
            gvInvoiceToCustomers.DataBind();
        }

        protected void btnInvoiceToCustomerSearch_Click(object sender, EventArgs e)
        {
            DisplayInvoiceToCustomersInGridView();
        }

        protected void gvInvoiceToCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvInvoiceToCustomers, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvInvoiceToCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvInvoiceToCustomers.SelectedRow;
                String CustomerCode = gvInvoiceToCustomers.DataKeys[row.RowIndex].Values["CustomerCode"] == DBNull.Value ? "" : gvInvoiceToCustomers.DataKeys[row.RowIndex].Values["CustomerCode"].ToString();

                txtInvoiceTo.Text = CustomerCode;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseInvoiceToDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvInvoiceToCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvInvoiceToCustomers.PageIndex = e.NewPageIndex;
                DisplayInvoiceToCustomersInGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        private void DisplayDeliveryToCustomersInGridView()
        {
            Customer customer = new Customer();
            customer.Alias = txtDeliveryToCustomerCode.Text;
            customer.CustomerName = txtDeliveryToCustomerName.Text;

            SearchOption searchOption = rblDeliveryToCustomerFilter.SelectedValue.ToSearchOptionEnum();

            gvDeliveryToCustomers.DataSource = customer.Retrieve(searchOption);
            gvDeliveryToCustomers.DataBind();
        }

        protected void btnDeliveryToCustomerSearch_Click(object sender, EventArgs e)
        {
            DisplayDeliveryToCustomersInGridView();
        }

        protected void gvDeliveryToCustomers_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvDeliveryToCustomers, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDeliveryToCustomers_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvDeliveryToCustomers.SelectedRow;
                String CustomerCode = gvDeliveryToCustomers.DataKeys[row.RowIndex].Values["CustomerCode"] == DBNull.Value ? "" : gvDeliveryToCustomers.DataKeys[row.RowIndex].Values["CustomerCode"].ToString();

                txtDeliveryTo.Text = CustomerCode;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseDeliveryToDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDeliveryToCustomers_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDeliveryToCustomers.PageIndex = e.NewPageIndex;
                DisplayDeliveryToCustomersInGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        
    }
}