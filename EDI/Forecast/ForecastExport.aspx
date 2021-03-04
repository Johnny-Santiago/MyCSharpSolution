<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="ForecastExport.aspx.cs" Inherits="EDI.Forecast.ForecastExport" %>

<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery-1.7.1.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery-ui-1.8.17.custom.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/DialogBrowser.js") %>' type="text/javascript"></script>

    <style type="text/css">
        .parameter_window
        {
            visibility: hidden;
        }
        
        .custom_parameter_window
        {
            width: 720px;
            padding: .8em;
            margin-bottom: 1em;
            border: 1px solid #ddd;
    
            background-color: #D6E7F1;
            background-position: 2px .8em;
            color: #264409;
            border-color: #C2E0EF;
        }
    </style>

    <script type="text/javascript" >
        $(document).ready(function () {
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl00").addClass("button");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl03_txtValue").addClass("text");
            $("#ParameterTable_BodyContentPlaceholder_ReportViewer1_ctl00").css("background-color", "#f0f9ff");
            $("#ParameterTable_BodyContentPlaceholder_ReportViewer1_ctl00").addClass("parameter_window");
            $("#BodyContentPlaceholder_ReportViewer1 > ul:nth-child(1) > li:nth-child(1)").addClass("parameter_window");
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
                buttonImage: "../App_Resources/images/ico-cal.png",
                buttonImageOnly: true,
                showAnim: 'slideDown'
            });
        });

        $("#anim").change(function () {
            $("#datepicker").datepicker("option", "showAnim", $(this).val());
        });

        $(document).ready(function () {
            $(".monthPicker").datepicker({
                dateFormat: 'mm/yy',
                changeMonth: true,
                changeYear: true,
                showButtonPanel: true,
                showOn: 'both',
                buttonImage: "../App_Resources/images/ico-cal.png",
                buttonImageOnly: true,
                onClose: function (dateText, inst) {
                    var month = $("#ui-datepicker-div .ui-datepicker-month :selected").val();
                    var year = $("#ui-datepicker-div .ui-datepicker-year :selected").val();
                    $(this).val($.datepicker.formatDate('mm/yy', new Date(year, month, 1)));
                }
            });

            $(".monthPicker").focus(function () {
                $(".ui-datepicker-calendar").hide();
                //                $("#ui-datepicker-div").position({
                //                    my: "center top",
                //                    at: "center bottom",
                //                    of: $(this)
                //                });
            });
        });

        $('input').live("keypress", function (e) {
            /* ENTER PRESSED*/
            if (e.keyCode == 13) {
                /* FOCUS ELEMENT */
                var inputs = $(this).parents("form").eq(0).find(":input");
                var idx = inputs.index(this);

                if (idx == inputs.length - 1) {
                    inputs[0].select()
                } else {
                    inputs[idx + 1].focus(); //  handles submit buttons
                    inputs[idx + 1].select();
                }
                return false;
            }
        });

        var oldgridcolor;
        function SetMouseOver(element) {
            oldgridcolor = element.style.backgroundColor;
            element.style.backgroundColor = '#a7cbec';
            element.style.cursor = 'pointer';
            element.style.textDecoration = 'underline';
        }
        function SetMouseOut(element) {
            element.style.backgroundColor = oldgridcolor;
            element.style.textDecoration = 'none';

        }

        function CloseDialog() {
            $(function () {
                $("#CustomerBrowser").dialog("close");
            });
        };
    </script>

    <style type="text/css">
        .SearchButton {
          display: inline-block;
          width: 16px;
          height: 16px;
          position: relative;
          left: -20px;
          top: 15px;
        }
        
        .DynamicDialogStyle
        {
            background-color: #F7FAFE;
            font-size: small;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <h1 class="title-regular">Download Forecast Template</h1>

    <asp:Literal EnableViewState="true" runat="server" ID="ltlMessage"></asp:Literal>
    <asp:Label ID="lblMessage" runat="server" ></asp:Label> 

    <div class="grid_10 inline alpha">
        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
            <ContentTemplate>
                <label for="txtCustomerCode">Customer</label>
                <asp:RequiredFieldValidator ID="rfvCustomerCode" runat="server" ErrorMessage="Required"
                    ControlToValidate="txtCustomerCode" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                <br />
                <asp:TextBox ID="txtCustomerCode" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseCustomers" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>

        <br class="clear" />
        <asp:Button ID="btnDownloadTemplate" runat="server" SkinID="Button" onclick="btnDownloadTemplate_Click" Text="Download Template" />  
    </div>

    <div class="grid_9 inline omega">
        <asp:Label ID="lblPeriod" runat="server" Text="Period/Year"></asp:Label>
        <asp:RequiredFieldValidator ID="rfvPeriod" runat="server" ErrorMessage="Required"
            ControlToValidate="txtPeriod" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
        <br />
        <asp:TextBox ID="txtPeriod" runat="server" MaxLength="7" ReadOnly="false" CssClass="monthPicker text" ForeColor="Blue" Width="110"></asp:TextBox>
    </div>

    <br class="clear" />
    <div id='CustomerBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_10 inline alpha" >
            <label for="txtCustomerName">Customer name</label><br />
            <asp:TextBox ID="txtCustomerName" runat="server"  CssClass="text"></asp:TextBox>
            <br class="clear" />
            <label for="rblFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblCustomerFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_9 inline omega">
            <label for="txtAlias">Alias</label><br />
            <asp:TextBox ID="txtAlias" runat="server" CssClass="grid_5 text" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnCustomerSearch" runat="server" Text="Search" SkinID="Button" onclick="btnCustomerSearch_Click"  CausesValidation="false" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <br class="clear" />
        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvCustomers" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="CustomerCode" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false"
                            OnRowDataBound="gvCustomers_RowDataBound" onselectedindexchanged="gvCustomers_SelectedIndexChanged"
                            OnPageIndexChanging="gvCustomers_PageIndexChanging" >
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
                    <asp:AsyncPostBackTrigger ControlID="gvCustomers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvCustomers" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <rsweb:ReportViewer ID="ReportViewer1" 
                        runat="server" 
                        AsyncRendering="true" 
                        Width="750px" 
                        Height="450px" 
                        OnLoad="ReportViewer_Load"
                        InteractivityPostBackMode="AlwaysSynchronous"
                        PromptAreaCollapsed="true" 
                        ShowParameterPrompts="false"
                        ShowPromptAreaButton="false"
                        ShowExportControls="false"
                        >
                        
    </rsweb:ReportViewer>

    <script type="text/javascript">
        // This replaces a method in the ReportViewer javascript. If Microsoft updates 
        // this particular method, it may cause problems, but that is unlikely to 
        // happen.The purpose of this is to redirect the user to the error page when 
        // an error occurs. The ReportViewer.ReportError event is not (always?) raised 
        // for Remote Async reports
        function OnReportFrameLoaded() {
            this.m_reportLoaded = true;
            this.ShowWaitFrame(false);

            if (this.IsAsync) {
                if (this.m_reportObject == null) {
                    /* window.location = '<%= HttpRuntime.AppDomainAppVirtualPath %>/Error.aspx'; */
                    /* document.getElementById("ltlMessage").value = "new text value"; */

                    document.getElementById('<%=lblMessage.ClientID %>').innerHTML = "<div class='error'>Sorry, an error is encountered while processing the report. Please check the invoice number.</div>";
                    $("#ReportFrameBodyContentPlaceholder_ReportViewer1").css("visibility", "hidden");
                    //                    document.getElementById("BodyContentPlaceholder_ReportViewer1_ctl01_ctl05_ctl01").removeAttribute('href');  //.setAttribute("disabled", "true");
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



