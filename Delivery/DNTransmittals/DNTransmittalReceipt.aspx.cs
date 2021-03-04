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
using System.IO;
using System.Configuration;
using Microsoft.Reporting.WebForms;
using Microsoft.Reporting;
using SecurityExtensions;

namespace Delivery.DNTransmittals
{
    public partial class DNTransmittalReceipt : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (this.Page.IsPostBack)
                {
                    this.btnAdvance.Text = ((this.HiddenField1.Value == "true") ? "Advance" : "Simple");
                }
                else
                {
                    string ReportServerUrl = ConfigurationManager.AppSettings["ReportServerUrl"].ToString();
                    if (string.IsNullOrEmpty(ReportServerUrl))
                    {
                        throw new Exception("Missing Report Server Url from web.config file.");
                    }
                    ViewState["ReportServerUrl"] = ReportServerUrl;

                    CompareValidatorReceivedDate.ValueToCompare = DateTime.Now.ToShortDateString(); 
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        private void SearchDeliveryNotes()
        {
            DeliveryNote dn = new DeliveryNote();

            string FromFulfilmentDate = txtFulfilmentDate.Text.Trim();
            string ToFulfilmentDate = txtFulfilmentDate2.Text.Trim();

            if (FromFulfilmentDate != string.Empty || ToFulfilmentDate != string.Empty)
            {
                if (FromFulfilmentDate == string.Empty && ToFulfilmentDate != string.Empty)
                {
                    FromFulfilmentDate = ToFulfilmentDate;
                }

                if (ToFulfilmentDate == string.Empty && FromFulfilmentDate != string.Empty)
                {
                    ToFulfilmentDate = FromFulfilmentDate;
                }

                dn.FulfilmentDate = DateTime.ParseExact(FromFulfilmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dn.FulfilmentDate2 = DateTime.ParseExact(ToFulfilmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

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

                dn.OrderDate = DateTime.ParseExact(FromOrderDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dn.OrderDate2 = DateTime.ParseExact(ToOrderDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            dn.Warehouse = txtWhseCode.Text;
            dn.DeliveryNoteNo = txtDeliveryNote.Text;
            dn.DeliveryOrderNo = txtDeliveryOrderNo.Text;
            dn.ManifestNo = txtManifestNo.Text;
            dn.OrderNo = txtOrderNumber.Text;
            dn.CustomerCode = txtOrderBy.Text;
            dn.InvoiceToCode = txtInvoiceTo.Text;
            dn.DeliveryToCode = txtDeliveryTo.Text;
            dn.YourRef = txtYourRef.Text;
            dn.Description = txtDescription.Text;
            dn.SelectionCode = txtSelectionCode.Text;
            dn.DealingType = txtDealingType.Text;
            Business.SearchOption searchOption = rblFilter.SelectedValue.ToSearchOptionEnum();

            gvDeliveryNotes.DataSource = dn.Retrieve(searchOption);
            gvDeliveryNotes.DataBind();

            pnlDataEntry.Visible = gvDeliveryNotes.Rows.Count != 0;
            pnlButtonEntry.Visible = pnlDataEntry.Visible;
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchDeliveryNotes();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
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

        protected void gvDeliveryNotes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDeliveryNotes.PageIndex = e.NewPageIndex;
                SearchDeliveryNotes();
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
            so.Description = txtSODescription.Text;
            Business.SearchOption searchOption = rblSalesOrderFilter.SelectedValue.ToSearchOptionEnum(); 

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

        private void DisplayDealingTypesInBrowserGridView()
        {
            DealingType dt = new DealingType();

            dt.AccountCategoryCode = txtDealingType2.Text;
            dt.Description = txtDealingTypeDescription.Text;
            Business.SearchOption searchOption = rblDealingTypeFilter.SelectedValue.ToSearchOptionEnum();

            gvDealingTypes.DataSource = dt.Retrieve(searchOption);
            gvDealingTypes.DataBind();
        }

        protected void btnDealingTypeSearch_Click(object sender, EventArgs e)
        {
            DisplayDealingTypesInBrowserGridView();
        }

        protected void gvDealingTypes_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvDealingTypes, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDealingTypes_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvDealingTypes.SelectedRow;
                String DealingType = gvDealingTypes.DataKeys[row.RowIndex].Values["AccountCategoryCode"] == DBNull.Value ? "" : gvDealingTypes.DataKeys[row.RowIndex].Values["AccountCategoryCode"].ToString();

                txtDealingType.Text = DealingType;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseDealingTypeDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDealingTypes_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDealingTypes.PageIndex = e.NewPageIndex;
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

            Business.SearchOption searchOption = rblInvoiceToCustomerFilter.SelectedValue.ToSearchOptionEnum();

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

            Business.SearchOption searchOption = rblDeliveryToCustomerFilter.SelectedValue.ToSearchOptionEnum();

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

        private void DisplayWarehousesInGridView()
        {
            DataTable Warehouses = (DataTable)ViewState["Warehouses"];

            gvWarehouses.DataSource = Warehouses;
            gvWarehouses.DataBind();
        }

        protected void btnSearchWarehouseBrowser_Click(object sender, EventArgs e) 
        {
            Business.SearchOption searchOption = rblWarehouseFilter.SelectedValue.ToSearchOptionEnum();
            String warehouse = txtWarehouse.Text;
            String description = txtWarehouseDescription.Text;
            ViewState["Warehouses"] = Warehouse.Retrieve(warehouse, description, searchOption);
            DisplayWarehousesInGridView();
        }

        protected void gvWarehouses_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvWarehouses, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvWarehouses_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvWarehouses.SelectedRow;
                String warehouse = gvWarehouses.DataKeys[row.RowIndex].Values["Warehouse"] == DBNull.Value ? "" : gvWarehouses.DataKeys[row.RowIndex].Values["Warehouse"].ToString();

                txtWhseCode.Text = warehouse;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseWarehouseDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvWarehouses_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvWarehouses.PageIndex = e.NewPageIndex;
                DisplayWarehousesInGridView();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        private void DisplayDeliveryNoteNosInGridView()
        {
            Business.SearchOption searchOption = rblDeliveryNoteNoFilter.SelectedValue.ToSearchOptionEnum();
            DeliveryNoteReference dnr = new DeliveryNoteReference();
            if (!string.IsNullOrEmpty(txtDeliveryNoteNo.Text.Trim()))
                dnr.DeliveryNoteNo = txtDeliveryNoteNo.Text;
            if (!string.IsNullOrEmpty(txtDescription.Text.Trim()))
                dnr.Description = txtDescription.Text;

            gvDeliveryNoteNos.DataSource = dnr.Retrieve(searchOption);
            gvDeliveryNoteNos.DataBind();
        }

        protected void btnSearchDeliveryNoteNoBrowser_Click(object sender, EventArgs e) 
        {
            DisplayDeliveryNoteNosInGridView();
        }

        protected void gvDeliveryNoteNos_RowDataBound(object sender, GridViewRowEventArgs e)
        {
            try
            {
                if (e.Row.RowType == DataControlRowType.DataRow)
                {
                    e.Row.Attributes["onmouseover"] = "javascript:SetMouseOver(this);this.style.textDecoration='none'";
                    e.Row.Attributes["onmouseout"] = "javascript:SetMouseOut(this)";
                    e.Row.Attributes["onclick"] = ClientScript.GetPostBackClientHyperlink(gvDeliveryNoteNos, "Select$" + e.Row.RowIndex);
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDeliveryNoteNos_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                GridViewRow row = gvDeliveryNoteNos.SelectedRow;
                String DeliveryNoteNo = gvDeliveryNoteNos.DataKeys[row.RowIndex].Values["Delivery note number"] == DBNull.Value ? "" : gvDeliveryNoteNos.DataKeys[row.RowIndex].Values["Delivery note number"].ToString();

                txtDeliveryNote.Text = DeliveryNoteNo;

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseDeliveryNoteNoDialog();", true);
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDeliveryNoteNos_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            try
            {
                gvDeliveryNoteNos.PageIndex = e.NewPageIndex;
                DisplayDeliveryNoteNosInGridView();
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
            Business.SearchOption searchOption = rblSelectionCodeFilter.SelectedValue.ToSearchOptionEnum();

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

        protected void btnReceiveSelected_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 rowsSelected = 0;
                foreach (GridViewRow row in gvDeliveryNotes.Rows)
                {
                    CheckBox cb = (CheckBox)(row.FindControl("chkDeliveryNoteSelector"));
                    if (cb != null && cb.Checked)
                    {
                        DeliveryNoteTransmittal dnt = new DeliveryNoteTransmittal();
                        dnt.Debtor = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Debtor"]);
                        dnt.Name = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Name"]);
                        dnt.OrderNo = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Order"]);
                        dnt.YourRef = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Your ref."]);
                        dnt.DeliveryNoteNo = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["DN no."]);
                        dnt.FulfilmentDate = DateTime.ParseExact(string.Format("{0:dd/MM/yyyy}", gvDeliveryNotes.DataKeys[row.RowIndex].Values["Fulfilment date"]), "dd/MM/yyyy", CultureInfo.InvariantCulture);
                        dnt.OrderDate = DateTime.ParseExact(string.Format("{0:dd/MM/yyyy}", gvDeliveryNotes.DataKeys[row.RowIndex].Values["Order date"]), "dd/MM/yyyy", CultureInfo.InvariantCulture); 
                        dnt.Description = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Description"]);
                        dnt.InvoiceToCode = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Invoice to"]);
                        dnt.DeliveryToCode = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Delivery to"]);
                        dnt.Warehouse = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Warehouse"]);
                        dnt.DeliveryOrderNo = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Delivery order no."]);
                        dnt.ManifestNo = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Manifest no."]);
                        dnt.DealingType = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Dealing type"]);
                        dnt.SelectionCode = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["Selection code"]);
                        dnt.ReceivedDate = DateTime.ParseExact(txtReceivedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);  
                        dnt.Remarks = txtRemarks.Text;  
                        dnt.SysCreator = HttpContext.Current.User.Identity.Name;
                        dnt.Insert();

                        rowsSelected++;
                        ltlMessage.Text += String.Format("Receive successful. Delivery note number {0} has been received.", dnt.DeliveryNoteNo).ToSuccessMessageFormat();
                    }
                }

                if (rowsSelected == 0)
                {
                    ltlMessage.Text = "No rows received. Please select at least 1 record to receive.".ToErrorMessageFormat();
                    return;
                }
                    
                SearchDeliveryNotes();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void lbtnExportToExcel_Click(object sender, EventArgs e)
        {
            string FromFulfilmentDate = txtFulfilmentDate.Text.Trim();
            string ToFulfilmentDate = txtFulfilmentDate2.Text.Trim();
            string FromOrderDate = txtOrderDate.Text.Trim();
            string ToOrderDate = txtOrderDate2.Text.Trim();

            if (FromFulfilmentDate != string.Empty || ToFulfilmentDate != string.Empty)
            {
                if (FromFulfilmentDate == string.Empty && ToFulfilmentDate != string.Empty)
                {
                    FromFulfilmentDate = ToFulfilmentDate;
                }

                if (ToFulfilmentDate == string.Empty && FromFulfilmentDate != string.Empty)
                {
                    ToFulfilmentDate = FromFulfilmentDate;
                }
            }

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
            }

            string Warehouse = txtWhseCode.Text;
            string DeliveryNoteNo = txtDeliveryNote.Text;
            string DeliveryOrderNo = txtDeliveryOrderNo.Text;
            string ManifestNo = txtManifestNo.Text;
            string OrderNo = txtOrderNumber.Text;
            string Debtor = txtOrderBy.Text;
            string InvoiceToCode = txtInvoiceTo.Text;
            string DeliveryToCode = txtDeliveryTo.Text;
            string YourRef = txtYourRef.Text;
            string Description = txtDescription.Text;
            string SelectionCode = txtSelectionCode.Text;
            string DealingType = txtDealingType.Text;
            Business.SearchOption searchOption = rblFilter.SelectedValue.ToSearchOptionEnum();

            Int32 option;
            switch (searchOption)
            {
                case Business.SearchOption.StartsWith: option = 2; break;
                case Business.SearchOption.Contains: option = 3; break;
                case Business.SearchOption.EndsWith: option = 4; break;
                default: option = 1; break;
            }

            string nullString = null;

            this.ltlMessage.Text = string.Empty;
            ReportViewer1.Visible = true;
            ReportViewer1.ProcessingMode = ProcessingMode.Remote;
            IReportServerCredentials irsc = new ReportServerNetworkCredentials();
            ReportViewer1.ServerReport.ReportServerCredentials = irsc;
            ReportViewer1.ServerReport.ReportServerUrl = new Uri((string)ViewState["ReportServerUrl"]);
            ReportViewer1.ServerReport.ReportPath = "/Delivery/DNTransmittalsToBeReceived";
            ReportViewer1.ShowParameterPrompts = true;


            ReportParameter[] myParam = {
                                            new ReportParameter("CustomerCode", Debtor == string.Empty ? nullString : Debtor)
                                            ,new ReportParameter("OrderNo", OrderNo == string.Empty ? nullString : OrderNo)
                                            ,new ReportParameter("YourRef", YourRef == string.Empty ? nullString : YourRef)
                                            ,new ReportParameter("DeliveryNoteNo", DeliveryNoteNo == string.Empty ? nullString : DeliveryNoteNo)
                                            ,new ReportParameter("FromFulfilmentDate", FromFulfilmentDate == string.Empty ? nullString : FromFulfilmentDate)  // string.Format("{0:MM/dd/yyyy}", DateTime.ParseExact(FromFulfilmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                                            ,new ReportParameter("ToFulfilmentDate", ToFulfilmentDate == string.Empty ? nullString : ToFulfilmentDate) // string.Format("{0:MM/dd/yyyy}", DateTime.ParseExact(ToFulfilmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                                            ,new ReportParameter("FromOrderDate", FromOrderDate == string.Empty ? nullString : FromOrderDate) // string.Format("{0:MM/dd/yyyy}", DateTime.ParseExact(FromOrderDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                                            ,new ReportParameter("ToOrderDate", ToOrderDate == string.Empty ? nullString : ToOrderDate) // string.Format("{0:MM/dd/yyyy}", DateTime.ParseExact(ToOrderDate, "dd/MM/yyyy", CultureInfo.InvariantCulture)))
                                            ,new ReportParameter("Description", Description == string.Empty ? nullString : Description)
                                            ,new ReportParameter("InvoiceToCode", InvoiceToCode == string.Empty ? nullString : InvoiceToCode)
                                            ,new ReportParameter("DeliveryToCode", DeliveryToCode == string.Empty ? nullString : DeliveryToCode)
                                            ,new ReportParameter("WhseCode", Warehouse == string.Empty ? nullString : Warehouse)
                                            ,new ReportParameter("DeliveryOrderNo", DeliveryOrderNo == string.Empty ? nullString : DeliveryOrderNo)
                                            ,new ReportParameter("ManifestNo", ManifestNo == string.Empty ? nullString : ManifestNo)
                                            ,new ReportParameter("DealingType", DealingType   == string.Empty ? nullString : DealingType  )
                                            ,new ReportParameter("SelectionCode", SelectionCode  == string.Empty ? nullString : SelectionCode )
                                            ,new ReportParameter("SearchOption", option.ToString())
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
            Response.AddHeader("Content-disposition", "filename=DNTransmittalsToBeReceived.xls");
            Response.OutputStream.Write(bytes, 0, bytes.Length);
            Response.OutputStream.Flush();
            Response.OutputStream.Close();
            Response.Flush();
            Response.Close();
        }
    }
}