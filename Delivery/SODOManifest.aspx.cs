using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using Business;
using Extensions;

namespace Delivery
{
    public partial class SODOManifest : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            try
            {
                if (!Page.IsPostBack)
                {
                    String editMode = Page.RouteData.Values["edit_mode"].ToString();
                    Int32 Id = Int32.Parse(Page.RouteData.Values["Id"].ToString());
                   
                    if (string.IsNullOrEmpty(editMode))
                    {
                        Response.Redirect("SalesOrders.aspx");
                    }

                    if (editMode == "view")
                        formViewSalesOrder.ChangeMode(FormViewMode.ReadOnly);
                    else if (editMode == "edit")
                        formViewSalesOrder.ChangeMode(FormViewMode.Edit);
                    else if (editMode == "new")
                        formViewSalesOrder.ChangeMode(FormViewMode.Insert);
                    else
                        throw new InvalidOperationException("error");

                    if (formViewSalesOrder.CurrentMode != FormViewMode.Insert)
                    {
                        RetrieveSalesOrderByID(Id);
                        DisplayDataInFormView();
                    }
                }
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        private void RetrieveSalesOrderByID(Int32 Id)
        {
            SalesOrder so = new SalesOrder(Id);
            ViewState["SalesOrder"] = so.Info;
        }

        private void DisplayDataInFormView()
        {
            DataTable dt = (DataTable)ViewState["SalesOrder"];
            formViewSalesOrder.DataSource = dt;
            formViewSalesOrder.DataBind();
        }

        protected void btnBack_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/SalesOrders.aspx");
        }

        protected void btnEdit_Click(object sender, EventArgs e)
        {
            Response.RedirectToRoute("sodomanifest-details", new { edit_mode = "edit", Id = RouteData.Values["Id"] });
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            try
            {
                Int32 Id = Int32.Parse(Page.RouteData.Values["Id"].ToString());

                SalesOrder so = new SalesOrder();
                so.Id = Id;
                so.OrderNumber = formViewSalesOrder.DataKey["OrderNumber"].ToString();
                so.DNOrder = ((TextBox)formViewSalesOrder.FindControl("txtDNOrder")).Text;
                so.Manifest = ((TextBox)formViewSalesOrder.FindControl("txtManifest")).Text;
                so.RIT = ((TextBox)formViewSalesOrder.FindControl("txtRIT")).Text;
                so.SysCreator = HttpContext.Current.User.Identity.Name;

                if (formViewSalesOrder.CurrentMode == FormViewMode.Edit)
                {
                    so.Update();
                }

                Response.RedirectToRoute("sodomanifest-details", new { edit_mode = "edit", Id = Id });
            }
            catch (Exception ex)
            {
                ltlMessage.Text = ex.Message.ToString().ToErrorMessageFormat();
            }
        }

        protected void formViewSalesOrder_DataBound(object sender, System.EventArgs e)
        {
            if (Page.Request.UrlReferrer != null)
            {
                if (Page.Request.UrlReferrer.AbsolutePath.Contains("new") && !IsPostBack)
                {
                    ltlMessage.Text = "Successfully saved data.".ToSuccessMessageFormat();
                }
                if (Page.Request.UrlReferrer.AbsolutePath.Contains("edit") && !IsPostBack)
                {
                    ltlMessage.Text = "Successfully updated data.".ToSuccessMessageFormat();
                }
            }
        }
    }
}