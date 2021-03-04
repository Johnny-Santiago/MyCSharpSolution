<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="RecoverPassword.aspx.cs" Inherits="EDI.Account.RecoverPassword" %>

<asp:Content ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <h1 class="title-regular clearfix">
        Recover Password
    </h1>

    <asp:PasswordRecovery ID="PasswordRecovery1" runat="server" 
        onanswerlookuperror="PasswordRecovery1_AnswerLookupError" 
        onuserlookuperror="PasswordRecovery1_UserLookupError"
        OnSendingMail="PasswordRecovery_SendingMail" >
        <QuestionTemplate>
            <h1 class="title-regular-3 clearfix">
                Identity Confirmation
            </h1>
            <div id="divfailureText" class="error" visible="false" runat="server" >
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
            </div>
            <strong>User Name: </strong><asp:Literal ID="UserName" runat="server" /><br /><br />

            <p>Answer the following question to receive your password.</p>
            <asp:Literal ID="Question" runat="server" /><br />
            <asp:Label ID="AnswerLabel" runat="server" AssociatedControlID="Answer">Answer:</asp:Label><br class="clear" />
            <asp:TextBox ID="Answer" CssClass="text" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="AnswerRequired" runat="server" ControlToValidate="Answer"
                    CssClass="validator" ErrorMessage="Required" ToolTip="Required" /><br />
            <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" SkinID="Button" />
        </QuestionTemplate>
        <UserNameTemplate>
            <h1 class="title-regular-3 clearfix">
                Forgot Your Password?
            </h1>
            <div id="divfailureText" class="error" visible="false" runat="server" >
                <asp:Literal ID="FailureText" runat="server" EnableViewState="False"></asp:Literal>
            </div>
            <p>Enter your User Name to receive your password.</p>
            <asp:Label ID="UserNameLabel" runat="server" AssociatedControlID="UserName">User Name:</asp:Label>
            <asp:TextBox ID="UserName"  CssClass="text" runat="server"></asp:TextBox>
            <asp:RequiredFieldValidator ID="UserNameRequired" runat="server" ControlToValidate="UserName"
                    CssClass="validator" ErrorMessage="Required" ToolTip="Required" /><br />
            <asp:Button ID="SubmitButton" runat="server" CommandName="Submit" Text="Submit" SkinID="Button" />
        </UserNameTemplate>
        <SuccessTemplate>
            <div class="success">
                <p>Your password has been sent to your e-mail account.</p>
            </div>
        </SuccessTemplate>
        <MailDefinition BodyFileName="~/EmailTemplates/PasswordRecovery.txt" 
            Subject="Your password has been reset...">
        </MailDefinition>
      </asp:PasswordRecovery>
</asp:Content>
