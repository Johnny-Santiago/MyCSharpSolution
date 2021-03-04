<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="DeliveryReport.aspx.cs" Inherits="Delivery.DeliveryReport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<%--<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>--%>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery-1.7.1.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery-ui-1.8.17.custom.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/DialogBrowser.js") %>' type="text/javascript"></script>

    <style type="text/css">
        .parameter_window
        {
            border: 1px solid gray;
        }
        
        .hidden
        {
            visibility: hidden;
        }
        
        .SearchButton {
          display: inline-block;
          width: 16px;
          height: 16px;
          position: relative;
          left: -20px;
          top: 14px;
        }
        
        .DynamicDialogStyle
        {
            background-color: #F7FAFE;
            font-size: small;
        }
    </style>
    <script type="text/javascript" >
        $(document).ready(function () {
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl00").addClass("button");
            $("#ParameterTable_BodyContentPlaceholder_ReportViewer1_ctl00").css("background-color", "#f0f9ff");
            $("#ParameterTable_BodyContentPlaceholder_ReportViewer1_ctl00").addClass("parameter_window");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl03_txtValue").addClass("datepicker grid_3 text");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl03_ddDropDownButton").addClass("hidden");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl05_txtValue").addClass("datepicker grid_3 text");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl05_ddDropDownButton").addClass("hidden");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl07_txtValue").addClass("grid_3 text");
        });
    </script>

    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.core.js") %>'
        type="text/javascript"></script>

    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.datepicker.js") %>'
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({
                changeMonth: true,
                changeYear: true,
                showOn: "both",
                buttonImage: "App_Resources/images/ico-cal.png",
                buttonImageOnly: true,
                showAnim: 'slideDown'
            });
        });
    
        $("#anim").change(function () {
            $("#datepicker").datepicker("option", "showAnim", $(this).val());
        });
    </script>

    <script type="text/javascript">
        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#FFFF99';
            element.style.cursor = 'pointer';
            element.style.textDecoration = 'underline';
        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';
        }

        function CloseDeliveryToDialog() {
            $(function () {
                $("#DeliveryToBrowser").dialog("close");
            });
        };
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>

    <%--<ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>--%>

    <h1 class="title-regular">Delivery Order Report</h1>

    <asp:Label ID="lblMessage" runat="server" ></asp:Label>

    <asp:Literal EnableViewState="false" runat="server" ID="ltlMessage"></asp:Literal>

    <%--<asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <div class="grid_9 omega align-right">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="../App_Resources/images/loader-life.gif" alt="Loading Data" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
    </asp:UpdatePanel>

    <br class="clear" />
    
    <div class="grid_10 inline alpha">
        <label for="txtFromDeliveryDate">Delivery date from</label><br />
        <asp:TextBox ID="txtFromDeliveryDate" runat="server" CssClass="datepicker grid_5 text" ></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvFromDeliveryDate" runat="server" ErrorMessage="Required"
            ControlToValidate="txtFromDeliveryDate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
        <br class="clear" />

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <label for="txtDeliveryTo">Customer</label><br />
                <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseDeliveryTo" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>--%>

    <%--<div class="grid_9 inline omega">
        <label for="txtToDeliveryDate">to</label><br />
        <asp:TextBox ID="txtToDeliveryDate" runat="server"  CssClass="datepicker grid_5 text"></asp:TextBox>
        <asp:RequiredFieldValidator ID="rfvToDeliveryDate" runat="server" ErrorMessage="Required"
            ControlToValidate="txtToDeliveryDate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
        <br class="clear" />
        <br />

        <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
            <ContentTemplate>
                <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="Button" onclick="btnSearch_Click" Width="112" />
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <br class="clear" />
    <div id='DeliveryToBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_10 inline alpha">
            <label for="txtDeliveryToCustomerName">Customer name</label><br />
            <asp:TextBox ID="txtDeliveryToCustomerName" runat="server"  CssClass="text"></asp:TextBox>
            <br class="clear" />
            <label for="rblFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblDeliveryToCustomerFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_9 inline omega">
            <label for="txtDeliveryToCustomerCode">Alias</label><br />
            <asp:TextBox ID="txtDeliveryToCustomerCode" runat="server" CssClass="grid_5 text" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel12" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnDeliveryToCustomerSearch" runat="server" Text="Search" SkinID="Button" onclick="btnDeliveryToCustomerSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel13" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvDeliveryToCustomers" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="CustomerCode" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false"
                            OnRowDataBound="gvDeliveryToCustomers_RowDataBound" onselectedindexchanged="gvDeliveryToCustomers_SelectedIndexChanged"
                            OnPageIndexChanging="gvDeliveryToCustomers_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="CustomerCode" HeaderText="Customer code" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="CustomerName" HeaderText="Customer name" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Alias" HeaderText="Alias" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Address" HeaderText="Address" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="City" HeaderText="City" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Country" HeaderText="Country" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="PostalCode" HeaderText="Postal code" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvDeliveryToCustomers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvDeliveryToCustomers" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>--%>
    

    <%--<asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Always">
        <ContentTemplate>--%>
            <rsweb:ReportViewer ID="ReportViewer1" 
                                runat="server" 
                                AsyncRendering="true" 
                                Width="750px" 
                                Height="450px" 
                                InteractivityPostBackMode="AlwaysSynchronous"
                                ShowParameterPrompts="false" 
                                OnLoad="ReportViewer_Load">
            </rsweb:ReportViewer>
        <%--</ContentTemplate>
    </asp:UpdatePanel>--%>

    <script type="text/javascript">
        function OnReportFrameLoaded() {
            this.m_reportLoaded = true;
            this.ShowWaitFrame(false);

            if (this.IsAsync) {
                if (this.m_reportObject == null) {
                    /*
                     window.location = '<%= HttpRuntime.AppDomainAppVirtualPath %>/Error.aspx'; 
                     document.getElementById("ltlMessage").value = "new text value";
                     */

                    document.getElementById('<%=lblMessage.ClientID %>').innerHTML = "<div class='error'>Sorry, an error is encountered while processing the report. Please check the invoice number.</div>";
                    $("#ReportFrameBodyContentPlaceholder_ReportViewer1").css("visibility", "hidden");
                                        document.getElementById("BodyContentPlaceholder_ReportViewer1_ctl01_ctl05_ctl01").removeAttribute('href');  //.setAttribute("disabled", "true");
                    $(document).ready(function () {
                        $("select#BodyContentPlaceholder_ReportViewer1_ctl01_ctl05_ctl00")
                            .find('option')
                            .remove()
                            .end()
                            .append('<option value=""></option>')
                            .val('')
                        ;
                    });
                    document.getElementById("BodyContentPlaceholder_ReportViewer1_ctl01_ctl05_ctl00").setAttribute("disabled", "true"); //-->dropdown
                }
                else {
                    this.m_reportObject.OnFrameVisible();

                }
            }
        }
        RSClientController.prototype.OnReportFrameLoaded = OnReportFrameLoaded;
    </script>
</asp:Content>
