<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="error-page.aspx.cs" Inherits="Delivery.App_Resources.messages.error_page" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1 class="title-regular clearfix">
        Sorry, an error has occurred...
    </h1>
    <p class="error">
        Unfortunately an error has occurred during the processing of your page request. <%--Please be assured we log and review all errors, even if you do not report this error we will endeavor to correct it. --%>
    </p>
</asp:Content>
