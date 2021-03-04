<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="Login.aspx.cs" Inherits="EDI.Account.Login" %>
<asp:Content ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1 class="title-regular clearfix">
        Log in
    </h1>
    <div class="notice">
        <strong>Forgot your password? </strong>Please go to the <a href='RecoverPassword.aspx'> password recovery page</a> to recover your password.
    </div>
    <asp:Login ID="myLogin" runat="server" OnLoginError="myLogin_LoginError" OnAuthenticate="myLogin_Authenticate" >
        <LayoutTemplate>
            <asp:Literal ID="FailureText" EnableViewState="false" runat="server"></asp:Literal>
            <label for="Username">
                Username:
            </label>
            <br class="clear"/>
            <asp:TextBox ID="UserName" runat="server" class="text"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rvfUserName" runat="server" ErrorMessage="Required"
            ControlToValidate="UserName" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator> 
            <br class="clear"/>
            <label for="Password">
                Password:
            </label>
            <br class="clear"/>
            <asp:TextBox ID="Password" runat="server" TextMode="Password" class="text"></asp:TextBox>
            <asp:RequiredFieldValidator ID="rfvPassword" runat="server" ErrorMessage="Required"
            ControlToValidate="Password" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator> 
            <br class="clear"/>
            <asp:Checkbox id="RememberMe" runat="server" Text="Remember my login" Visible="false"></asp:Checkbox>
            <asp:button id="Login" CommandName="Login" runat="server" Text="Login" SkinID="Button"></asp:button>
        </LayoutTemplate>
    </asp:Login>
</asp:Content>
