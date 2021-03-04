<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="STNReport.aspx.cs" Inherits="Delivery.STNReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a" Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.core.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.datepicker.js") %>' type="text/javascript"></script>

    <style type="text/css">
        .parameter_window
        {
            border: 1px solid gray;
        }
    </style>
    <script type="text/javascript" >
        $(document).ready(function () {
            //            var sel = $("select#BodyContentPlaceholder_ReportViewer1_ctl01_ctl05_ctl00");
            //            sel.find("option[value='Select a format']").remove();
            //            sel.find("option[value='XML']").remove();
            //            sel.find("option[value='CSV']").remove();
            //            sel.find("option[value='IMAGE']").remove();
            //            sel.find("option[value='MHTML']").remove();
            //            sel.find("option[value='PDF']").remove();
            //            sel.find("option[value='EXCEL']").remove();
            //            sel.find("option[value='WORD']").remove();
            //            sel.append("<option style='visibility: hidden;'></option>");
            //            sel.append("<option>CSV</option>");
            //            sel.val("CSV");
            //            //sel.css("visibility", "hidden");

            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl00").addClass("button");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl03_txtValue").addClass("text grid_2 datepicker");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl05_txtValue").addClass("text grid_2 datepicker");
            $("#ParameterTable_BodyContentPlaceholder_ReportViewer1_ctl00").css("background-color", "#f0f9ff");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl03_ddDropDownButton").css("visibility", "hidden");
            $("#BodyContentPlaceholder_ReportViewer1_ctl00_ctl05_ddDropDownButton").css("visibility", "hidden");
            $("#ParameterTable_BodyContentPlaceholder_ReportViewer1_ctl00").addClass("parameter_window");
        });
    </script>

    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({
                //                dateFormat: 'dd/mm/yy',
                changeMonth: true,
                changeYear: true,
                showOn: "both",
                buttonImage: "../App_Resources/images/ico-cal.png",
                buttonImageOnly: true,
                showAnim: 'slideDown'
            });
        });
    </script>

    <script type="text/javascript">
        $("#anim").change(function () {
            $("#datepicker").datepicker("option", "showAnim", $(this).val());
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true" AsyncPostBackTimeout = "1500" >
    </asp:ScriptManager>
    <h1 class="title-regular">STN Report</h1>

    <asp:Literal EnableViewState="true" runat="server" ID="ltlMessage"></asp:Literal>

    <asp:Label ID="lblMessage" runat="server" ></asp:Label>

    <rsweb:ReportViewer ID="ReportViewer1" 
                        runat="server" 
                        AsyncRendering="true" 
                        Width="750px" 
                        Height="480px" 
                        OnLoad="ReportViewer_Load"
                        InteractivityPostBackMode="AlwaysSynchronous"
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

                    document.getElementById('<%=lblMessage.ClientID %>').innerHTML = "<div class='error'>Sorry, an error is encountered while processing the report. Please contact MIS for assistance.</div>";
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

