<%@ Page Title="Delivery Order and Manifest" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="SODOManifest.aspx.cs" Inherits="Delivery.SODOManifest" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:Literal EnableViewState="False" runat="server" ID="ltlMessage"></asp:Literal>
    <asp:FormView SkinID="FormView" ID="formViewSalesOrder" runat="server" DataKeyNames="Id,OrderNumber" 
        EnableViewState="true" OnDataBound="formViewSalesOrder_DataBound">
        <EmptyDataTemplate>
            <h1 class="title-regular clearfix">
                No Sales Order Found
            </h1>
            <div class="notice">
                Sorry, no sales order available with this ID.
            </div>
            <asp:Button ID="btnBack" CausesValidation="false" runat="server" Text="Back to listing page" OnClick="btnBack_Click" SkinID="Button" />
        </EmptyDataTemplate>
        <ItemTemplate>
            <h1 class="title-regular clearfix">
                <span class="grid_15 alpha">
                    Sales Order
                    <%# Eval("OrderNumber")%>
                </span>
                <span class="grid_3 omega align-right">
                    <asp:LinkButton ID="LinkButton1" OnClientClick="print()" runat="server" Text="Print Info" CssClass="button small white"></asp:LinkButton>
                </span>
            </h1>

            <div class="grid_10 inline alpha">
                <label>Order by</label>
                <br />
                <asp:TextBox ID="txtOrderBy" Text='<%# Bind("OrderBy") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />

                <label>Name</label>
                <br />
                <asp:TextBox ID="txtCustomerName" Text='<%# Bind("CustomerName") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />

                <label>Sales order date</label>
                <asp:TextBox ID="txtOrderDate" Text='<%# Bind("OrderDate", "{0:dd/MM/yyyy}") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br class="clear" />

                <label>Your reference</label>
                <br />
                <asp:TextBox ID="txtYourRef" Text='<%# Bind("YourRef") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />
            </div>
            <div class="grid_9 inline omega">
                <label>Delivery order no.</label>
                <br />
                <asp:TextBox ID="txtDNOrder" Text='<%# Bind("DNOrder") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />

                <label>Manifest no.</label>
                <br />
                <asp:TextBox ID="txtManifest" Text='<%# Bind("Manifest") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />

                <label>RIT</label>
                <br />
                <asp:TextBox ID="txtRIT" Text='<%# Bind("RIT") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>

                <p>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="Button" />
                    <asp:Button ID="btnBack" CausesValidation="false" runat="server" Text="Back" OnClick="btnBack_Click" SkinID="AltButton" />
                </p>
            </div>
            
        </ItemTemplate>
        <EditItemTemplate>
            <h1 class="title-regular clearfix">
                <span class="grid_19 alpha">
                    SO <%# Eval("OrderNumber")%> - Delivery Order No. and Manifest No.
                </span>
                <%--<span class="grid_3 omega align-right">
                    <asp:LinkButton ID="LinkButton1" OnClientClick="print()" runat="server" Text="Print Info" CssClass="button small white"></asp:LinkButton>
                </span>--%>
            </h1>

            <div class="grid_10 inline alpha">
                <label>Order by</label>
                <br />
                <asp:TextBox ID="txtOrderBy" Text='<%# Bind("OrderBy") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />

                <label>Name</label>
                <br />
                <asp:TextBox ID="txtCustomerName" Text='<%# Bind("CustomerName") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />

                <label>Sales order date</label>
                <asp:TextBox ID="txtOrderDate" Text='<%# Bind("OrderDate", "{0:dd/MM/yyyy}") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br class="clear" />

                <label>Your reference</label>
                <br />
                <asp:TextBox ID="txtYourRef" Text='<%# Bind("YourRef") %>' runat="server" CssClass="text" ReadOnly="true" BackColor="#FFFFC0"></asp:TextBox>
                <br />
            </div>
            <div class="grid_9 inline omega">
                <label>Delivery order no.</label>
                <br />
                <asp:TextBox ID="txtDNOrder" Text='<%# Bind("DNOrder") %>' runat="server" CssClass="text"></asp:TextBox>
                <br />

                <label>Manifest no.</label>
                <br />
                <asp:TextBox ID="txtManifest" Text='<%# Bind("Manifest") %>' runat="server" CssClass="text"></asp:TextBox>
                <br />

                <label>RIT</label>
                <br />
                <asp:TextBox ID="txtRIT" Text='<%# Bind("RIT") %>' runat="server" CssClass="text" ></asp:TextBox>
                <br />
                <br />

                <p>
                    <asp:Button ID="btnSave" runat="server" Text="Save" OnClick="btnSave_Click" SkinID="Button" />
                    <asp:Button ID="btnBack" CausesValidation="false" runat="server" Text="Back" OnClick="btnBack_Click" SkinID="AltButton" />
                </p>
            </div>
        </EditItemTemplate>
    </asp:FormView>
</asp:Content>
