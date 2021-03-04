<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="SOKanbanInquiry.aspx.cs" Inherits="EDI.SalesOrders.SOKanbanInquiry" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery-1.7.1.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery-ui-1.8.17.custom.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/DialogBrowser.js") %>' type="text/javascript"></script>

    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.core.js") %>'
        type="text/javascript"></script>

    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/jquery.ui.datepicker.js") %>'
        type="text/javascript"></script>

    <script type="text/javascript">
        function pageLoad(sender, args) {
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
        }
    </script>

    <script type="text/javascript">
        $('.expand-one').click(function () {
            $('.content-one').slideToggle('slow');
        });

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

        function CloseDialog() {
            $(function () {
                $("#CustomerBrowser").dialog("close");
            });
        };

        function CloseSODialog() {
            $(function () {
                $("#SalesOrderBrowser").dialog("close");
            });
        };

        function CloseSelectionCodeDialog() {
            $(function () {
                $("#SelectionCodeBrowser").dialog("close");
            });
        };

        function CloseInvoiceToDialog() {
            $(function () {
                $("#InvoiceToBrowser").dialog("close");
            });
        };

        function CloseDeliveryToDialog() {
            $(function () {
                $("#DeliveryToBrowser").dialog("close");
            });
        };

        function ExpandCollapse(ctrl) {
            var collPanel = $find("BodyContentPlaceholder_cpeFilterContent");
            objHiddenField = document.getElementById("<%=HiddenField1.ClientID%>");
            if (collPanel.get_Collapsed()) {
                collPanel.set_Collapsed(false);
                ctrl.value = "Simple";
                objHiddenField.value = 'false';
            }
            else {
                collPanel.set_Collapsed(true);
                ctrl.value = "Advance";
                objHiddenField.value = 'true';
            }
        }
    </script>

    <style type="text/css">
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
        
        .modal
        {
            position: fixed;
            top: 0;
            left: 0;
            background-color: black;
            z-index: 99;
            opacity: 0.8;
            filter: alpha(opacity=80);
            -moz-opacity: 0.8;
            min-height: 100%;
            width: 100%;
        }
        
        .loading
        {
            font-family: Arial;
            font-size: 10pt;
            border: 5px solid #67CFF5;
            width: 200px;
            height: 100px;
            display: none;
            position: fixed;
            background-color: White;
            z-index: 999;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server">
    </ajaxToolkit:ToolkitScriptManager>

    <h1 class="title-regular">
        Kanban to Sales Orders Inquiry
    </h1>

    <asp:Literal EnableViewState="false" runat="server" ID="ltlMessage"></asp:Literal>

    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <div class="grid_9 omega align-right">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="../App_Resources/images/loader-life.gif" alt="Loading Data" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvSalesOrders" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>

    <br class="clear" />

    <div class="grid_10 inline alpha">
        <label for="txtRefID">Reference No.</label><br />
        <asp:TextBox ID="txtRefID" runat="server" CssClass="grid_5 text" ></asp:TextBox>
        <br class="clear" />

        <label for="txtOrderDate">Sales Order date</label><br />
        <asp:TextBox ID="txtOrderDate" runat="server" CssClass="datepicker grid_5 text" ></asp:TextBox>
        <br class="clear" />

        <label for="txtDeliveryDate">Delivery date</label><br />
        <asp:TextBox ID="txtDeliveryDate" runat="server" CssClass="datepicker grid_5 text" ></asp:TextBox>
        <br class="clear" />

        <asp:UpdatePanel ID="UpdatePanel" runat="server">
            <ContentTemplate>
                <label for="txtOrderBy">Order by</label><br />
                <asp:TextBox ID="txtOrderBy" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseCustomers" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br class="clear" />

        <asp:UpdatePanel ID="upOrderNumber" runat="server">
            <ContentTemplate>
                <label for="txtOrderNumber">Sales order</label><br />
                <asp:TextBox ID="txtOrderNumber" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseSalesOrders" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="grid_9 inline omega">
        <label for="txtYourRef">Your reference</label><br />
        <asp:TextBox ID="txtYourRef" runat="server" CssClass="grid_5 text" ></asp:TextBox>
        <br class="clear" />

        <label for="txtOrderDate2">to</label><br />
        <asp:TextBox ID="txtOrderDate2" runat="server"  CssClass="datepicker grid_5 text"></asp:TextBox>
        <br class="clear" />

        <label for="txtDeliveryDate2">to</label><br />
        <asp:TextBox ID="txtDeliveryDate2" runat="server"  CssClass="datepicker grid_5 text"></asp:TextBox>
        <br class="clear" />

        <asp:UpdatePanel ID="UpdatePanel14" runat="server">
            <ContentTemplate>
                <label for="txtDeliveryTo">Delivery to</label><br />
                <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseDeliveryTo" ClientIDMode="Static" 
                    ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" 
                    CausesValidation="false"  /> 
                </span>
                <br class="clear" />

                <label for="ddlStatus">Status</label><br />
                <asp:DropDownList runat="server" ID="ddlStatus" >
                    <asp:ListItem Value="-1" Text="--All--" Selected="True" /> 
                    <asp:ListItem Value="0" Text="Processing" /> 
                    <asp:ListItem Value="1" Text="Success" /> 
                    <asp:ListItem Value="2" Text="Failed" /> 
                </asp:DropDownList> 
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <div class="grid_20">
        <asp:Panel ID="pnlFilterContent"  runat="server" Height="0px" >
            <div class="grid_10 inline alpha">
                <label for="txtDeliveryOrderNo">Delivery order no.</label><br />
                <asp:TextBox ID="txtDeliveryOrderNo" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />

                <label for="txtItemCode">Item code</label><br />
                <asp:TextBox ID="txtItemCode" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />

                <label for="txtItemDescription">Item description</label><br />
                <asp:TextBox ID="txtItemDescription" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />

                <label for="txtOrderDescription">Description</label><br />
                <asp:TextBox ID="txtOrderDescription" runat="server" CssClass="grid_5 text" ></asp:TextBox>
            </div>

            <div class="grid_9 inline omega">
                <label for="txtManifestNo">Manifest no.</label><br />
                <asp:TextBox ID="txtManifestNo" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />

                <label for="txtCustomerPartNo">Customer part no.</label><br />
                <asp:TextBox ID="txtCustomerPartNo" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />

                <label for="txtDealingType">Dealing type</label><br />
                <asp:TextBox ID="txtDealingType" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />

                <label for="txtRIT">RIT</label><br />
                <asp:TextBox ID="txtRIT" runat="server" CssClass="grid_5 text" ></asp:TextBox> 
                <br class="clear" />
            </div>
        </asp:Panel>
    </div>

    <asp:HiddenField ID="HiddenField1" Value="true" runat="server" />
    <ajaxToolkit:CollapsiblePanelExtender ID="cpeFilterContent" runat="Server"
        TargetControlID="pnlFilterContent"
        ExpandControlID=""
        CollapseControlID="" 
        Collapsed="True"
        TextLabelID=""
        ImageControlID="imgFilterHeader"    
        ExpandedText=""
        CollapsedText=""
        SuppressPostBack="true"/>
        

    <div class="grid_10 inline alpha">
        <label for="rblFilter">Filter</label><br />
        <asp:RadioButtonList ID="rblFilter" runat="server" RepeatColumns="4" >
            <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
            <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
            <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
            <asp:ListItem Text="Ends with" Value="EndsWith" /> 
        </asp:RadioButtonList>
    </div>

    <div class="grid_9 inline omega">
        <br class="clear" />
        <br />
        <div class="grid_3 inline alpha">
            <asp:UpdatePanel ID="UpdatePanel1" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="Button" onclick="btnSearch_Click" Width="112" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="grid_4 inline alpha">
            <asp:Button ID="btnAdvance" SkinID="AltButton" OnClientClick="ExpandCollapse(this); return false;" runat="server" Text="Advance"  Width="112" />
        </div>
    </div>

    <div class="loading" align="center">
        Loading. Please wait.<br />
        <br />
        <img src="../App_Resources/images/loader.gif" alt="" />
    </div>

    <div class="grid-viewer grid_19 clearfix">
        <asp:UpdatePanel ID="upItems" runat="server" UpdateMode="Always">
            <ContentTemplate>
                 <asp:GridView ID="gvSalesOrders" runat="server" SkinID="GridView" EnableViewState="true"
                        DataKeyNames="ID,RefID" 
                        RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                        HeaderStyle-ForeColor="White" AllowSorting="false"
                        OnPageIndexChanging="gvSalesOrders_PageIndexChanging" >
                        <Columns>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="updDownload" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="imgBtnDownload" runat="server" 
                                                ImageUrl="~/App_Themes/Default/images/listing/page_white_excel.png" 
                                                onclick="imgBtnDownload_Click" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="imgBtnDownload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Download
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField ItemStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <asp:UpdatePanel ID="updUpload" runat="server">
                                        <ContentTemplate>
                                            <asp:ImageButton ID="imgBtnUpload" runat="server" Visible='<%#Eval("Status").ToString() == "Failed" %>' 
                                                ImageUrl="~/App_Themes/Default/images/listing/Upload.png" 
                                                onclick="imgBtnUpload_Click" />
                                            </ContentTemplate>
                                            <Triggers>
                                                <asp:PostBackTrigger ControlID="imgBtnUpload" />
                                            </Triggers>
                                        </asp:UpdatePanel>
                                </ItemTemplate>
                                <HeaderTemplate>
                                    Reprocess
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="Status" HeaderText="Status" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="SalesOrder" HeaderText="Order no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="RefID" HeaderText="Ref. no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="OrderDate" HeaderText="Order date" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="DeliveryDate" HeaderText="Delivery date" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="OrderedBy" HeaderText="Ordered by" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="DeliverTo" HeaderText="Deliver to" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="YourRef" HeaderText="Your ref." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="DeliveryOrderNo" HeaderText="Delivery order no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ManifestNo" HeaderText="Manifest no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="RIT" HeaderText="RIT" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ItemCode" HeaderText="Item code" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="CustomerPartNo" HeaderText="Customer part no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="ItemDescription" HeaderText="Item description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Quantity" HeaderText="Quantity" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="DealingType" HeaderText="Dealing type" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="SysCreated" HeaderText="Date uploaded" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="SysCreator" HeaderText="Uploaded by" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                        </Columns>
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
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
            <label for="txtCustomerCode">Alias</label><br />
            <asp:TextBox ID="txtAlias" runat="server" CssClass="grid_5 text" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel2" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnCustomerSearch" runat="server" Text="Search" SkinID="Button" onclick="btnCustomerSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel3" runat="server" UpdateMode="Always">
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

    <br class="clear" />
    <div id='SalesOrderBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_6 inline alpha">
            <label for="txtDescription">Description</label><br />
            <asp:TextBox ID="txtDescription" runat="server"  CssClass="text"></asp:TextBox>
            <br class="clear" />
            <label for="rblFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblSalesOrderFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_6 inline omega">
            <label for="txtYourRef">Your reference</label><br />
            <asp:TextBox ID="txtYourRef2" runat="server" CssClass="grid_5 text" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel4" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSearchSOBrowser" runat="server" Text="Search" SkinID="Button" onclick="btnSearchSOBrowser_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel5" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvBrowseSalesOrders" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="OrderNumber" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false"
                            OnRowDataBound="gvBrowseSalesOrders_RowDataBound" onselectedindexchanged="gvBrowseSalesOrders_SelectedIndexChanged"
                            OnPageIndexChanging="gvBrowseSalesOrders_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="OrderNumber" HeaderText="Sales order" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="YourRef" HeaderText="Your reference" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvBrowseSalesOrders" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvBrowseSalesOrders" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <br class="clear" />
    <div id='SelectionCodeBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_4 inline alpha" style="text-align: left !important">
            <label for="txtSelectionCode2">Selection code</label><br />
            <asp:TextBox ID="txtSelectionCode2" runat="server"  CssClass="text" Width="110"></asp:TextBox>
            <br class="clear" />
            <label for="rbltxtSelectionCodeFilter">Filter</label><br />
            <asp:RadioButtonList ID="RadioButtonList1" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_6 inline omega" style="text-align: left !important">
            <label for="txtSelectionCodeDescription">Description</label><br />
            <asp:TextBox ID="txtSelectionCodeDescription" runat="server" CssClass="text" Width="190" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSelectionCodeSearch" runat="server" Text="Search" SkinID="Button" onclick="btnSelectionCodeSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvSelectionCodes" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="SelectionCode" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false"
                            OnRowDataBound="gvSelectionCodes_RowDataBound" onselectedindexchanged="gvSelectionCodes_SelectedIndexChanged"
                            OnPageIndexChanging="gvSelectionCodes_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="SelectionCode" HeaderText="Selection code" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvSelectionCodes" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvSelectionCodes" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <br class="clear" />
    <div id='InvoiceToBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_10 inline alpha">
            <label for="txtInvoiceToCustomerName">Customer name</label><br />
            <asp:TextBox ID="txtInvoiceToCustomerName" runat="server"  CssClass="text"></asp:TextBox>
            <br class="clear" />
            <label for="rblFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblInvoiceToCustomerFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_9 inline omega">
            <label for="txtInvoiceToCustomerCode">Alias</label><br />
            <asp:TextBox ID="txtInvoiceToCustomerCode" runat="server" CssClass="grid_5 text" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel9" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnInvoiceToCustomerSearch" runat="server" Text="Search" SkinID="Button" onclick="btnInvoiceToCustomerSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel10" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvInvoiceToCustomers" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="CustomerCode" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false"
                            OnRowDataBound="gvInvoiceToCustomers_RowDataBound" onselectedindexchanged="gvInvoiceToCustomers_SelectedIndexChanged"
                            OnPageIndexChanging="gvInvoiceToCustomers_PageIndexChanging" >
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
                    <asp:AsyncPostBackTrigger ControlID="gvInvoiceToCustomers" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvInvoiceToCustomers" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
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
    </div>
</asp:Content>
