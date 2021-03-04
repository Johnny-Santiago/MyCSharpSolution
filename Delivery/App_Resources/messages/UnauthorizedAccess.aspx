<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="UnauthorizedAccess.aspx.cs" Inherits="Delivery.App_Resources.messages.UnauthorizedAccess" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1 class="title-regular clearfix">
        Unauthorized Access
    </h1>
    <div class="error">
        <p>You have attempted to access a page that you are not authorized to view.</p><p>If you have any questions, please contact the intranet site administrator.</p>
    </div>
</asp:Content>
