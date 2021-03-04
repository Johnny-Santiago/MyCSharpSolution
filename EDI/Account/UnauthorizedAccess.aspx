<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="UnauthorizedAccess.aspx.cs" Inherits="EDI.Account.UnauthorizedAccess" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1 class="title-regular clearfix">
        Unauthorized Access
    </h1>
    <asp:Literal runat="server" EnableViewState="False" ID="labelMessage"></asp:Literal>
</asp:Content>
