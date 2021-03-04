<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="ForecastOrderReport.aspx.cs" Inherits="EDI.ForecastOrder.ForecastOrderReport" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <style type="text/css">
        .parameter_window
        {
            border: 1px solid gray;
        }
    </style>

    <script type="text/javascript" >
        $(document).ready(function () {
            $("#BodyContentPlaceholder_rvForecastOrder_ctl00_ctl00").addClass("button");
            $("#BodyContentPlaceholder_rvForecastOrder_ctl00_ctl03_txtValue").addClass("text grid_2 datepicker");
            $("#BodyContentPlaceholder_rvForecastOrder_ctl00_ctl05_txtValue").addClass("text grid_2 datepicker");
            $("#BodyContentPlaceholder_rvForecastOrder_ctl00_ctl03_ddDropDownButton").css("visibility", "hidden");
            $("#BodyContentPlaceholder_rvForecastOrder_ctl00_ctl05_ddDropDownButton").css("visibility", "hidden");
            $("#BodyContentPlaceholder_rvForecastOrder_ctl00_ctl07_txtValue").addClass("text grid_4");
            $("#ParameterTable_BodyContentPlaceholder_rvForecastOrder_ctl00").css("background-color", "#f0f9ff");
            $("#ParameterTable_BodyContentPlaceholder_rvForecastOrder_ctl00").addClass("parameter_window");
        });
    </script>

    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.core.js") %>'
        type="text/javascript"></script>

    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.datepicker.js") %>'
        type="text/javascript"></script>

    <script type="text/javascript">
        $(function () {
            $(".datepicker").datepicker({
                                dateFormat: 'mm/yy',
                changeMonth: true,
                changeYear: true,

                showOn: "both",
                showButtonPanel: true,
                buttonImage: '../App_Resources/images/ico-cal.png',
                buttonImageOnly: true,
                showAnim: 'slideDown',
                onClose: function (dateText, inst) {
                    $(this).datepicker('setDate', new Date(inst.selectedYear, inst.selectedMonth, 1));
                }
            });
            $(".datepicker").focus(function () {
                $(".ui-datepicker-calendar").hide();
                //                $("#ui-datepicker-div").position({
                //                    my: "center top",
                //                    at: "center bottom",
                //                    of: $(this)
                //                });
            });
        });
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server" EnablePartialRendering="true">
    </asp:ScriptManager>
    <h1 class="title-regular-2">Forecast order report</h1>

    <asp:Literal EnableViewState="False" runat="server" ID="ltlMessage"></asp:Literal>
        <rsweb:ReportViewer ID="rvForecastOrder" runat="server" 
            Width="850px" 
            Height="250px"             
            InteractivityPostBackMode="AlwaysSynchronous"                         
            CssClass="grid-2" 
            Font-Names="Verdana" 
            Font-Size="8pt" 
            InteractiveDeviceInfos="(Collection)"
            WaitMessageFont-Names="Verdana" 
            WaitMessageFont-Size="14pt"
            ProcessingMode="Remote">      
         </rsweb:ReportViewer>
    <hr class="clearfix"/>
    </asp:Content>
