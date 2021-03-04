<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="ChangePassword.aspx.cs" Inherits="EDI.Account.ChangePassword" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1 class="title-regular clearfix">
        Change Password
    </h1>
    <div class="notice">
        <strong>Note: </strong>
        The password must be at least seven characters long.
    </div>
    <asp:ChangePassword ID="ChangeUserPassword" runat="server" 
        CancelDestinationPageUrl="~/" EnableViewState="false" RenderOuterTable="false" 
         SuccessPageUrl="ChangePasswordSuccess.aspx" 
        onchangepassworderror="ChangeUserPassword_ChangePasswordError" OnSendingMail="ChangePassword_SendingMail">
        <ChangePasswordTemplate>
            <div id="divfailureText" class="error" visible="false" runat="server" >
                <asp:Literal ID="FailureText" EnableViewState="false" runat="server"></asp:Literal>
            </div>

            <asp:CompareValidator ID="NewPasswordCompare" runat="server" ControlToCompare="NewPassword" ControlToValidate="ConfirmNewPassword" 
                    Display="Dynamic"><div class="error">The Password and Confirmation Password must match.</div></asp:CompareValidator>
            <asp:Label ID="CurrentPasswordLabel" runat="server" AssociatedControlID="CurrentPassword">Old Password:</asp:Label>
            <span class="required-field-indicator">*</span><br class="clear" />
            <asp:TextBox ID="CurrentPassword" runat="server" CssClass="text" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="CurrentPasswordRequired" runat="server" ControlToValidate="CurrentPassword" 
                    CssClass="validator" ErrorMessage="Required" ToolTip="Required" /> <br />

            <asp:Label ID="NewPasswordLabel" runat="server" AssociatedControlID="NewPassword">New Password:</asp:Label>
            <span class="required-field-indicator">*</span><br class="clear" />
            <asp:TextBox ID="NewPassword" runat="server" CssClass="text" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="NewPasswordRequired" runat="server" ControlToValidate="NewPassword" 
                    CssClass="validator" ErrorMessage="Required" ToolTip="Required"/> <br />

            <asp:Label ID="ConfirmNewPasswordLabel" runat="server" AssociatedControlID="ConfirmNewPassword">Confirm New Password:</asp:Label>
            <span class="required-field-indicator">*</span><br class="clear" />
            <asp:TextBox ID="ConfirmNewPassword" runat="server" CssClass="text" TextMode="Password"></asp:TextBox>
            <asp:RequiredFieldValidator ID="ConfirmNewPasswordRequired" runat="server" ControlToValidate="ConfirmNewPassword" 
                    CssClass="failureNotification" Display="Dynamic" ErrorMessage="Confirm New Password is required."
                    ToolTip="Confirm New Password is required." ValidationGroup="ChangeUserPasswordValidationGroup">*</asp:RequiredFieldValidator>
            <p class="submitButton">
                <asp:Button ID="ChangePasswordPushButton" runat="server" CommandName="ChangePassword" Text="Change Password" SkinID="Button"/>
                <asp:Button ID="CancelPushButton" runat="server" CausesValidation="False" CommandName="Cancel" Text="Cancel" SkinID="AltButton"/>
            </p>
        </ChangePasswordTemplate>
        <MailDefinition BodyFileName="~/EmailTemplates/ChangePassword.htm" 
            IsBodyHtml="True" Subject="Your password has been changed!">
        </MailDefinition>
    </asp:ChangePassword>
</asp:Content>
