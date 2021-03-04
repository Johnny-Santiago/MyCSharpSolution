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
using SecurityExtensions;

namespace Delivery
{
    public partial class DNTransmittalInquiry : System.Web.UI.Page
    {
        private List<string> roles = new List<string>
		{
			"Administrator",
			"DN Transmittal Editor"
		};

        protected void Page_Load(object sender, EventArgs e)
        {
            if (this.Page.IsPostBack)
            {
                this.btnAdvance.Text = ((this.HiddenField1.Value == "true") ? "Advance" : "Simple");
            }
            else
            {
                CompareValidatorNewReceivedDate.ValueToCompare = DateTime.Now.ToShortDateString();
            }
        }

        private void SearchDeliveryNotes()
        {
            DeliveryNoteTransmittal dnt = new DeliveryNoteTransmittal();

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

                dnt.FulfilmentDate = DateTime.ParseExact(FromFulfilmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dnt.FulfilmentDate2 = DateTime.ParseExact(ToFulfilmentDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
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

                dnt.OrderDate = DateTime.ParseExact(FromOrderDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dnt.OrderDate2 = DateTime.ParseExact(ToOrderDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            string FromReceivedDate = txtReceived_Date.Text.Trim();
            string ToReceivedDate = txtReceived_Date2.Text.Trim();

            if (FromReceivedDate != string.Empty || ToReceivedDate != string.Empty)
            {
                if (FromReceivedDate == string.Empty && ToReceivedDate != string.Empty)
                {
                    FromReceivedDate = ToReceivedDate;
                }

                if (ToReceivedDate == string.Empty && FromReceivedDate != string.Empty)
                {
                    ToReceivedDate = FromReceivedDate;
                }

                dnt.ReceivedDate = DateTime.ParseExact(FromReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dnt.ReceivedDate2 = DateTime.ParseExact(ToReceivedDate, "dd/MM/yyyy", CultureInfo.InvariantCulture);
            }

            dnt.Warehouse = txtWhseCode.Text;
            dnt.DeliveryNoteNo = txtDeliveryNote.Text;
            dnt.DeliveryOrderNo = txtDeliveryOrderNo.Text;
            dnt.ManifestNo = txtManifestNo.Text;
            dnt.OrderNo = txtOrderNumber.Text;
            dnt.Debtor = txtOrderBy.Text;
            dnt.InvoiceToCode = txtInvoiceTo.Text;
            dnt.DeliveryToCode = txtDeliveryTo.Text;
            dnt.YourRef = txtYourRef.Text;
            dnt.Description = txtDescription.Text;
            dnt.SelectionCode = txtSelectionCode.Text;
            dnt.DealingType = txtDealingType.Text;
            SearchOption searchOption = rblFilter.SelectedValue.ToSearchOptionEnum();

            gvDeliveryNotes.DataSource = dnt.Retrieve(searchOption);
            gvDeliveryNotes.DataBind();

            pnlDataEntry.Visible = gvDeliveryNotes.Rows.Count != 0;
        }

        private void InitializeControl()
        {
            //DataTable reportingDepartments = (DataTable)this.ViewState["ReportingDepartments"];
            //string department = this.ddlDepartment.SelectedItem.Value;
            bool isEditor = base.User.IsInAnyRole(this.roles); // base.User.IsMemberOf(reportingDepartments, department);
            if (this.gvDeliveryNotes.Rows.Count > 0)
            {
                this.gvDeliveryNotes.Columns[0].Visible = isEditor;
                this.gvDeliveryNotes.Columns[1].Visible = isEditor;
            }
            this.ShowDeleteConfirmationDialog.Visible = isEditor;
            //this.btnDeleteSelected.Visible = isEditor;
            //this.pnlController.Visible = base.User.IsInAnyRole(this.roles);
            //this.ddlDepartment.Enabled = base.User.IsInRole(this.reviewer);
            //this.btnAdvance.Text = ((this.HiddenField1.Value == "true") ? "Advance" : "Simple");
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            try
            {
                SearchDeliveryNotes();
                InitializeControl();
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

            SearchOption searchOption = rblCustomerFilter.SelectedValue.ToSearchOptionEnum();

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
                InitializeControl();
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

        private void DisplayDealingTypesInBrowserGridView()
        {
            DealingType dt = new DealingType();

            dt.AccountCategoryCode = txtDealingType2.Text;
            dt.Description = txtDealingTypeDescription.Text;
            SearchOption searchOption = rblDealingTypeFilter.SelectedValue.ToSearchOptionEnum();

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
            SearchOption searchOption = rblDeliveryNoteNoFilter.SelectedValue.ToSearchOptionEnum();
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
            SearchOption searchOption = rblSelectionCodeFilter.SelectedValue.ToSearchOptionEnum();

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

        protected void btnDelete_Click(object sender, EventArgs e) 
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
                        dnt.DeliveryNoteNo = Convert.ToString(gvDeliveryNotes.DataKeys[row.RowIndex].Values["DN no."]);
                        dnt.Reason = txtReason.Text;
                        dnt.SysModifier = HttpContext.Current.User.Identity.Name;
                        dnt.Delete();

                        rowsSelected++;
                        ltlMessage.Text += String.Format("Delete successful. Delivery note number {0} has been deleted.", dnt.DeliveryNoteNo).ToSuccessMessageFormat();
                    }
                }

                ScriptManager.RegisterStartupScript(this, GetType(), "Close", "CloseDeleteConfirmationDialog();", true);

                if (rowsSelected == 0)
                {
                    ltlMessage.Text = "No rows deleted. Please select at least 1 record to delete.".ToErrorMessageFormat();
                    return;
                }

                SearchDeliveryNotes();
                InitializeControl();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void gvDeliveryNotes_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName.Equals("ChangeReceivedDate")) 
            {
                int index = Convert.ToInt32(e.CommandArgument);

                String ID = Convert.ToString(gvDeliveryNotes.DataKeys[index].Values["ID"]);
                String DeliveryNoteNo = Convert.ToString(gvDeliveryNotes.DataKeys[index].Values["DN no."]);
                DateTime ReceivedDate = Convert.ToDateTime(gvDeliveryNotes.DataKeys[index].Values["Received date"]);

                hfDNNoteID.Value = ID;
                txtDNNoteNo.Text = DeliveryNoteNo;
                txtOldReceivedDate.Text = string.Format("{0:dd/MM/yyyy}", ReceivedDate);

                System.Text.StringBuilder sb = new System.Text.StringBuilder();
                sb.Append(@"<script type='text/javascript'>");
                sb.Append("$('#ChangeReceivedDateDialog').dialog('open');");
                sb.Append(@"</script>");


                ScriptManager.RegisterClientScriptBlock(this, this.GetType(),
                           "ModalScript", sb.ToString(), false);

            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e) 
        {
            try
            {
                DeliveryNoteTransmittal dnt = new DeliveryNoteTransmittal();
                dnt.ID = Convert.ToInt32(hfDNNoteID.Value);
                dnt.ReceivedDate = DateTime.ParseExact(txtNewReceivedDate.Text, "dd/MM/yyyy", CultureInfo.InvariantCulture);
                dnt.Reason = txtReason2.Text;
                dnt.SysModifier = HttpContext.Current.User.Identity.Name;
                dnt.DeliveryNoteNo = txtDeliveryNote.Text;
                dnt.Update();

                ltlMessage.Text += String.Format("Update successful. Delivery note number {0} has been updated.", txtDNNoteNo.Text).ToSuccessMessageFormat();
                
                SearchDeliveryNotes();
                InitializeControl();
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }
    }
}