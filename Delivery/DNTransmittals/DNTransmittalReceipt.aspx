<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="DNTransmittalReceipt.aspx.cs" Inherits="Delivery.DNTransmittals.DNTransmittalReceipt" Culture = "en-GB"  %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<%@ Register Assembly="Microsoft.ReportViewer.WebForms, Version=9.0.0.0, Culture=neutral, PublicKeyToken=b03f5f7f11d50a3a"
    Namespace="Microsoft.Reporting.WebForms" TagPrefix="rsweb" %>

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
                    dateFormat: 'dd/mm/yy',
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

            $("#BodyContentPlaceholder_txtReceivedDate").datepicker("setDate", new Date());
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

        //To close the dialog
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

        function CloseDealingTypeDialog() {
            $(function () {
                $("#DealingTypeBrowser").dialog("close");
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

        function CloseWarehouseDialog() {
            $(function () {
                $("#WarehouseBrowser").dialog("close");
            });
        };

        function CloseDeliveryNoteNoDialog() {
            $(function () {
                $("#DeliveryNoteNoBrowser").dialog("close");
            });
        };

        function CloseSelectionCodeDialog() {
            $(function () {
                $("#SelectionCodeBrowser").dialog("close");
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

        function SelectAll(frmId, id) {
            var frm = document.getElementById(frmId);
            for (i = 1; i < frm.rows.length; i++) {
                var checkbox = frm.rows[i].cells[0].childNodes[1];
                if (checkbox != null)
                    checkbox.checked = document.getElementById(id).checked;
            }
        };
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
        
        .Hidden
        {
            visibility: hidden;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <ajaxToolkit:ToolkitScriptManager ID="ToolkitScriptManager1" runat="server" AsyncPostBackTimeout="300">
    </ajaxToolkit:ToolkitScriptManager>

    <h1 class="title-regular">
        Delivery Note Transmittals - To Be Received
    </h1>

    <p>
        In this page you will be able to view and search list of delivery notes to match 
        with the delivery note returned from the customer for delivery note transmittal.
    </p>

    <asp:UpdatePanel ID="upd" runat="server">
        <ContentTemplate>
            <asp:Literal EnableViewState="false" runat="server" ID="ltlMessage"></asp:Literal>

            <div class="grid_9 omega align-right">
                <asp:UpdateProgress ID="UpdateProgress1" runat="server">
                    <ProgressTemplate>
                        <img src="../App_Resources/images/loader-life.gif" alt="Loading Data" />
                    </ProgressTemplate>
                </asp:UpdateProgress>
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:AsyncPostBackTrigger ControlID="gvDeliveryNotes" EventName="PageIndexChanging" />
        </Triggers>
    </asp:UpdatePanel>

    <br class="clear" />

    <div class="grid_10 inline alpha">
        <asp:UpdatePanel ID="UpdatePanel21" runat="server">
            <ContentTemplate>
                <label for="txtOrderBy">Order by</label><br />
                <asp:TextBox ID="txtOrderBy" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseCustomers" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br class="clear" />

        <label for="txtDeliveryTo">Delivery order no.</label><br />
        <asp:TextBox ID="txtDeliveryOrderNo" runat="server" CssClass="grid_5 text" ></asp:TextBox>
        <br class="clear" />

        <label for="txtFulfilmentDate">Fulfilment date</label><br />
        <asp:TextBox ID="txtFulfilmentDate" runat="server" CssClass="datepicker grid_4 text" ></asp:TextBox>
        <br class="clear" />
    </div>

    <div class="grid_9 inline omega">
        <asp:UpdatePanel ID="upDeliveryNote" runat="server">
            <ContentTemplate>
                <label for="txtDeliveryNote">Delivery note no.</label><br />
                <asp:TextBox ID="txtDeliveryNote" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <span class="SearchButton">
                    <asp:ImageButton ID="BrowseDeliveryNotes" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                </span>
            </ContentTemplate>
        </asp:UpdatePanel>
        <br class="clear" />

        <label for="txtDeliveryTo">Manifest no.</label><br />
        <asp:TextBox ID="txtManifestNo" runat="server" CssClass="grid_5 text" ></asp:TextBox>
        <br class="clear" />

        <label for="txtFulfilmentDate2">to</label><br />
        <asp:TextBox ID="txtFulfilmentDate2" runat="server"  CssClass="datepicker grid_4 text"></asp:TextBox>
        <br class="clear" />
    </div>

    <div class="grid_20">
        <asp:Panel ID="pnlFilterContent"  runat="server" Height="0px" >
            <div class="grid_10 inline alpha">
                <label for="txtOrderDate">SO date</label><br />
                <asp:TextBox ID="txtOrderDate" runat="server" CssClass="datepicker grid_4 text" ></asp:TextBox>
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
                <br class="clear" />

                <asp:UpdatePanel ID="UpdatePanel11" runat="server">
                    <ContentTemplate>
                        <label for="txtInvoiceTo">Invoice to</label><br />
                        <asp:TextBox ID="txtInvoiceTo" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                        <span class="SearchButton">
                            <asp:ImageButton ID="BrowseInvoiceTo" ClientIDMode="Static"  ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                        </span>
                        <br class="clear" />
                    </ContentTemplate>
                </asp:UpdatePanel>

                <label for="txtYourRef">Your ref.</label><br />
                <asp:TextBox ID="txtYourRef" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                <br class="clear" />

                <asp:UpdatePanel ID="UpdatePanel20" runat="server">
                    <ContentTemplate>
                        <label for="txtSelectionCode">Selection code</label><br />
                        <asp:TextBox ID="txtSelectionCode" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                        <span class="SearchButton">
                            <asp:ImageButton ID="BrowseSelectionCodes" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                        </span>
                    </ContentTemplate>
                </asp:UpdatePanel>
            </div>

            <div class="grid_9 inline omega">
                <asp:UpdatePanel ID="UpdatePanel14" runat="server">
                    <ContentTemplate>
                        <label for="txtOrderDate2">to</label><br />
                        <asp:TextBox ID="txtOrderDate2" runat="server"  CssClass="datepicker grid_4 text"></asp:TextBox>
                        <br class="clear" />

                        <asp:UpdatePanel ID="upWarehouse" runat="server">
                            <ContentTemplate>
                                <label for="txtWhseCode">Warehouse</label><br />
                                <asp:TextBox ID="txtWhseCode" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                                <span class="SearchButton">
                                    <asp:ImageButton ID="BrowseWarehouses" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                                </span>
                            </ContentTemplate>
                        </asp:UpdatePanel>
                        <br class="clear" />

                        <label for="txtDeliveryTo">Delivery to</label><br />
                        <asp:TextBox ID="txtDeliveryTo" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                        <span class="SearchButton">
                            <asp:ImageButton ID="BrowseDeliveryTo" ClientIDMode="Static" 
                            ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" 
                            CausesValidation="false" />
                        </span>
                        <br class="clear" />

                        <label for="txtDescription">Description</label><br />
                        <asp:TextBox ID="txtDescription" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                        <br class="clear" />

                        <label for="txtDealingType">Dealing type</label><br />
                        <asp:TextBox ID="txtDealingType" runat="server" CssClass="grid_5 text" ></asp:TextBox>
                        <span class="SearchButton">
                            <asp:ImageButton ID="BrowseDealingTypes" ClientIDMode="Static" ImageUrl="~/App_Themes/Default/images/listing/Select.png"  runat="server" CausesValidation="false" />
                        </span>
                    </ContentTemplate>
                </asp:UpdatePanel>
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
                    <asp:Button ID="btnSearch" runat="server" Text="Search" SkinID="Button" onclick="btnSearch_Click" CausesValidation="false" Width="107" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>
        <div class="grid_6 inline alpha">
            <asp:Button ID="btnAdvance" SkinID="AltButton" OnClientClick="ExpandCollapse(this); return false;" runat="server" Text="Advance" Width="107" />
            <asp:Button ID="btnClear" SkinID="AltButton" OnClientClick="this.form.reset(); return false;" runat="server" Text="Clear" Width="107" />
        </div>
    </div>

    <div class="grid-viewer grid_19 clearfix">
        <asp:UpdatePanel ID="upItems" runat="server" UpdateMode="Always">
            <ContentTemplate>
                 <asp:GridView ID="gvDeliveryNotes" runat="server" SkinID="GridView" EnableViewState="true"
                        DataKeyNames="ID,Debtor,Name,DN no.,Your ref.,Description,Fulfilment date,Delivery order no.,Manifest no.,Invoice to,Delivery to,Warehouse,Selection code,Dealing type,Order,Order date" ClientIDMode="Static" 
                        RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                        HeaderStyle-ForeColor="White" AllowSorting="false"
                        OnPageIndexChanging="gvDeliveryNotes_PageIndexChanging" >
                        <Columns>
                            <asp:TemplateField>
                                <ItemTemplate>
                                    <asp:CheckBox runat="server" ID="chkDeliveryNoteSelector" />
                                </ItemTemplate>
                                <HeaderTemplate>
                                    <asp:CheckBox runat="server" ID="chkSelectAll" ClientIDMode="Static" onclick="SelectAll('gvDeliveryNotes','chkSelectAll')" />
                                </HeaderTemplate>
                            </asp:TemplateField>
                            <asp:BoundField DataField="DN no." HeaderText="DN no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Debtor" HeaderText="Debtor" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Name" HeaderText="Name" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Your ref." HeaderText="Your ref." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Fulfilment date" HeaderText="Fulfilment date" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Delivery order no." HeaderText="Delivery order no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Manifest no." HeaderText="Manifest no." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Invoice to" HeaderText="Invoice to" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Delivery to" HeaderText="Delivery to" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Dealing type" HeaderText="Dealing type" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Order" HeaderText="Order" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Warehouse" HeaderText="Warehouse" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            <asp:BoundField DataField="Order date" HeaderText="Order date" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                        </Columns>
                    </asp:GridView>
            </ContentTemplate>
        </asp:UpdatePanel>
    </div>

    <asp:UpdatePanel ID="upDataEntry" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <asp:Panel ID="pnlDataEntry" runat="server" Visible="false" > 
                <div class="grid_10 inline alpha">
	                <label for="txtReceivedDate">Receive date</label><br />
                    <asp:TextBox ID="txtReceivedDate" runat="server" CssClass="datepicker grid_4 text" ></asp:TextBox>
                    <asp:RequiredFieldValidator ID="rfvReceivedDate" runat="server" ErrorMessage="Required"
                    ControlToValidate="txtReceivedDate" Display="Dynamic" CssClass="validator"></asp:RequiredFieldValidator>
                    <asp:CompareValidator ID="CompareValidatorReceivedDate" runat="server" 
                        ControlToValidate="txtReceivedDate" ErrorMessage="<br><br>Receive date should be on or before today." 
                        CssClass="validator" Operator="LessThanEqual" Type="Date" >
                    </asp:CompareValidator>
                </div>

                <div class="grid_9 inline omega">
	                <label for="txtRemarks">Remarks</label><br />
                    <asp:TextBox ID="txtRemarks" runat="server" CssClass="grid_8 text" ></asp:TextBox>
                </div>
            </asp:Panel>
            <br class="clear" />
            <div class="grid_7 inline alpha">
                <asp:Panel ID="pnlButtonEntry" runat="server" Visible="false" > 
                    <asp:Button SkinID="Button" runat="server" Width="129" ID="btnReceiveSelected" Text="Receive Selected" onclick="btnReceiveSelected_Click" CausesValidation="true" /> 
                    <asp:Button ID="lbtnExportToExcel" runat="server"  Width="129" SkinID="Button" Text="Export to Excel" onclick="lbtnExportToExcel_Click" />  
                </asp:Panel>
            </div>
            <div class="grid_4 inline omega">
                <asp:Button CausesValidation="false" ID="btnBack" runat="server" Text="Back to DN Received" PostBackUrl="DNTransmittalInquiry.aspx" SkinID="AltButton" />
            </div>
        </ContentTemplate>
        <Triggers>
            <asp:PostBackTrigger ControlID="lbtnExportToExcel" />
        </Triggers>
    </asp:UpdatePanel>

    <br class="clear" />
    <div id='WarehouseBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_7 inline alpha" >
            <asp:Label ID="lblWarehouse" runat="server" Text="Warehouse"></asp:Label>
            <asp:TextBox ID="txtWarehouse" runat="server"  CssClass="text" Width="150px" ></asp:TextBox>
            <br class="clear" />
            <label for="rblFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblWarehouseFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_8 inline omega">
            <asp:Label ID="lblDescription" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtWarehouseDescription" runat="server" CssClass="text" Width="190px" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel8" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSearchWarehouseBrowser" runat="server" Text="Search" SkinID="Button" onclick="btnSearchWarehouseBrowser_Click" CausesValidation="false" />  
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_14 clearfix">
            <asp:UpdatePanel ID="UpdatePanel15" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvWarehouses" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="Warehouse,Description" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false" 
                            OnRowDataBound="gvWarehouses_RowDataBound" 
                            onselectedindexchanged="gvWarehouses_SelectedIndexChanged"
                            OnPageIndexChanging="gvWarehouses_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="Warehouse" HeaderText="Warehouse" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvWarehouses" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvWarehouses" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
    </div>

    <br class="clear" />
    <div id='DeliveryNoteNoBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_7 inline alpha" >
            <asp:Label ID="lblDeliveryNoteNo" runat="server" Text="Delivery Note No"></asp:Label>
            <asp:TextBox ID="txtDeliveryNoteNo" runat="server"  CssClass="text" Width="150px" ></asp:TextBox>
            <br class="clear" />
            <label for="rblFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblDeliveryNoteNoFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_8 inline omega">
            <asp:Label ID="Label1" runat="server" Text="Description"></asp:Label>
            <asp:TextBox ID="txtDeliveryNoteNoDescription" runat="server" CssClass="text" Width="190px" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel16" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSearchDeliveryNoteNoBrowser" runat="server" Text="Search" SkinID="Button" onclick="btnSearchDeliveryNoteNoBrowser_Click" CausesValidation="false" />  
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_14 clearfix">
            <asp:UpdatePanel ID="UpdatePanel17" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvDeliveryNoteNos" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="Delivery note number,Description" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false" 
                            OnRowDataBound="gvDeliveryNoteNos_RowDataBound" 
                            onselectedindexchanged="gvDeliveryNoteNos_SelectedIndexChanged"
                            OnPageIndexChanging="gvDeliveryNoteNos_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="Delivery note number" HeaderText="Delivery note number" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Delivery note date" HeaderText="Delivery note date" DataFormatString="{0:dd/MM/yyyy}" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvDeliveryNoteNos" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvDeliveryNoteNos" EventName="SelectedIndexChanged" />
                </Triggers>
            </asp:UpdatePanel>
        </div>
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
        <br class="clear" />
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
            <label for="txtSODescription">Description</label><br />
            <asp:TextBox ID="txtSODescription" runat="server"  CssClass="text"></asp:TextBox>
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
    <div id='DealingTypeBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_4 inline alpha" style="text-align: left !important">
            <label for="txtDealingType2">Dealing type</label><br />
            <asp:TextBox ID="txtDealingType2" runat="server"  CssClass="text" Width="110"></asp:TextBox>
            <br class="clear" />
            <label for="rblDealingTypeFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblDealingTypeFilter" runat="server" RepeatColumns="4" >
                <asp:ListItem Text="Equals&nbsp;&nbsp;" Value="Equals" /> 
                <asp:ListItem Selected="True"  Text="Starts with&nbsp;&nbsp;" Value="StartsWith" /> 
                <asp:ListItem Text="Contains&nbsp;&nbsp;" Value="Contains" /> 
                <asp:ListItem Text="Ends with" Value="EndsWith" /> 
            </asp:RadioButtonList>
        </div>

        <div class="grid_6 inline omega" style="text-align: left !important">
            <label for="txtDealingTypeDescription">Description</label><br />
            <asp:TextBox ID="txtDealingTypeDescription" runat="server" CssClass="text" Width="190" ></asp:TextBox>
            <br class="clear" />
            <br />
            <asp:UpdatePanel ID="UpdatePanel6" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnDealingTypeSearch" runat="server" Text="Search" SkinID="Button" onclick="btnDealingTypeSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel7" runat="server" UpdateMode="Always">
                <ContentTemplate>
                     <asp:GridView ID="gvDealingTypes" runat="server" SkinID="GridView" EnableViewState="true"
                            DataKeyNames="AccountCategoryCode" 
                            RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                            HeaderStyle-ForeColor="White" AllowSorting="false"
                            OnRowDataBound="gvDealingTypes_RowDataBound" onselectedindexchanged="gvDealingTypes_SelectedIndexChanged"
                            OnPageIndexChanging="gvDealingTypes_PageIndexChanging" >
                            <Columns>
                                <asp:BoundField DataField="AccountCategoryCode" HeaderText="Dealing type" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                                <asp:BoundField DataField="Description" HeaderText="Description" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                            </Columns>
                        </asp:GridView>
                </ContentTemplate>
                <Triggers>
                    <asp:AsyncPostBackTrigger ControlID="gvDealingTypes" EventName="RowDataBound" />
                    <asp:AsyncPostBackTrigger ControlID="gvDealingTypes" EventName="SelectedIndexChanged" />
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

    <br class="clear" />
    <div id='SelectionCodeBrowser' style="display: none; font: 13px/1.5 'Segoe UI','Helvetica Neue',Arial,'Liberation Sans',FreeSans,sans-serif">
        <div class="grid_4 inline alpha" style="text-align: left !important">
            <label for="txtSelectionCode2">Selection code</label><br />
            <asp:TextBox ID="txtSelectionCode2" runat="server"  CssClass="text" Width="110"></asp:TextBox>
            <br class="clear" />
            <label for="rblSelectionCodeFilter">Filter</label><br />
            <asp:RadioButtonList ID="rblSelectionCodeFilter" runat="server" RepeatColumns="4" >
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
            <asp:UpdatePanel ID="UpdatePanel18" runat="server" UpdateMode="Conditional">
                <ContentTemplate>
                    <asp:Button ID="btnSelectionCodeSearch" runat="server" Text="Search" SkinID="Button" onclick="btnSelectionCodeSearch_Click" />
                </ContentTemplate>
            </asp:UpdatePanel>
        </div>

        <div class="grid-viewer grid_19 clearfix">
            <asp:UpdatePanel ID="UpdatePanel19" runat="server" UpdateMode="Always">
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

    <asp:UpdatePanel ID="UpdatePanel26" runat="server" UpdateMode="Always">
        <ContentTemplate>
            <rsweb:ReportViewer ID="ReportViewer1" 
                                runat="server" 
                                AsyncRendering="true" 
                                Width="750px" 
                                Height="450px" 
                                InteractivityPostBackMode ="AlwaysSynchronous"
                                PromptAreaCollapsed="true" 
                                ShowParameterPrompts="false"
                                ShowPromptAreaButton="false"
                                Visible="false" 
                                >
            </rsweb:ReportViewer>
        </ContentTemplate>
    </asp:UpdatePanel>
</asp:Content>
