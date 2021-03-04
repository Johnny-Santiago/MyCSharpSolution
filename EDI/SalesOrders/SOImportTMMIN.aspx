<%@ Page Title="" Language="C#" MasterPageFile="~/App_Resources/default.master" AutoEventWireup="true" CodeBehind="SOImportTMMIN.aspx.cs" Inherits="EDI.SalesOrders.SOImportTMMIN" %>
<asp:Content ID="Content1" ContentPlaceHolderID="HeadContentPlaceHolder" runat="server">
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/nicefileinput/jquery.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/nicefileinput/jquery.nicefileinput.min.js") %>' type="text/javascript"></script>
    <script src='<%# ResolveUrl ("~/App_Resources/client-scripts/framework/nicefileinput/jquery.nicefileinput.js") %>' type="text/javascript"></script>

    <script type="text/javascript">
        /* <![CDATA[ */             
        $(document).ready(function(){
	        $("input[type=file].nicefileinput").nicefileinput();
        });
        /* ]]> */
    </script>

    <script type="text/javascript">
        function UploadFile(fileUpload) {
            if (fileUpload.value != '') {
                document.getElementById("<%=btnUpload.ClientID %>").click();
            }
        }
    </script>

    <style type="text/css">
        
        .nice {
	        font-family: arial;
	        font-size: 12px;
	        -webkit-box-shadow: 0px 1px 0px #fff, 0px -1px 0px rgba(0,0,0,.1);
	        -moz-box-shadow: 0px 1px 0px #fff, 0px -1px 0px rgba(0,0,0,.1);
	        box-shadow: 0px 1px 0px #fff, 0px -1px 0px rgba(0,0,0,.1); 
	        -moz-border-radius: 4px; 
	        -webkit-border-radius: 4px;
	        border-radius: 4px;
        }
        .nice .NFI-button {
	        -moz-border-radius-topleft: 3px; 
	        -moz-border-radius-bottomleft: 3px;
	        -webkit-border-top-left-radius: 3px;
	        -webkit-border-bottom-left-radius: 3px;
	        border-top-left-radius: 3px; 
	        border-bottom-left-radius: 3px;

	        background-color: #0192DD;

	        background-image: linear-gradient(bottom, #1774A3 0%, #0194DD 56%);
	        background-image: -o-linear-gradient(bottom, #1774A3 0%, #0194DD 56%);
	        background-image: -moz-linear-gradient(bottom, #1774A3 0%, #0194DD 56%);
	        background-image: -webkit-linear-gradient(bottom, #1774A3 0%, #0194DD 56%);
	        background-image: -ms-linear-gradient(bottom, #1774A3 0%, #0194DD 56%);
	        background-image: -webkit-gradient(
		        linear,
		        left bottom,
		        left top,
		        color-stop(0, #1774A3),
		        color-stop(0.56, #0194DD)
	        );
	        text-shadow: 0px -1px 0px #0172bd;
	        border: solid #0172bd 1px;
	        border-bottom: solid #00428d 1px;
	
	        -webkit-box-shadow: inset 0px 1px 0px rgba(255,255,255,.2);
	        -moz-box-shadow: inset 0px 1px 0px rgba(255,255,255,.2);
	        box-shadow: inset 0px 1px 0px rgba(255,255,255,.2); 	
	
	        color: #fff;
	        width: 100px;
	        height: 30px;
	        line-height: 30px;
        }
        .nice .NFI-button:hover {
	        background: #333;
	        text-shadow: 0px -1px 0px #111;
	        border: solid #000 1px;
	
        }
        .nice .NFI-filename {
	        -moz-border-radius-topright: 3px; 
	        -moz-border-radius-bottomright: 3px;
	        -webkit-border-top-right-radius: 3px;
	        -webkit-border-bottom-right-radius: 3px;
	        border-top-right-radius: 3px; 
	        border-bottom-right-radius: 3px;

	        width: 200px;
	        border: solid #777 1px;
	        border-left: none;
	        height: 30px;
	        line-height: 30px;
	
	        background: #fff;
	        -webkit-box-shadow: inset 0px 2px 0px rgba(0,0,0,.05);
	        -moz-box-shadow: inset 0px 2px 0px rgba(0,0,0,.05);
	        box-shadow: inset 0px 2px 0px rgba(0,0,0,.05); 

	        color: #777;
	        text-shadow: 0px 1px 0px #fff;
        }


    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="SearchContentPlaceHolder" runat="server">
</asp:Content>
<asp:Content ID="Content3" ContentPlaceHolderID="BodyContentPlaceholder" runat="server">
    <asp:ScriptManager ID="ScriptManager1" EnablePartialRendering="true" runat="server" />

    <h1 class="title-regular">TMMIN - Import Sales Orders Per Kanban</h1>

    <div class="notice">
        <asp:Label ID="lblStep1" runat="server" Text="Choose the customer EDI or Excel file containing Sales Orders per Kanban. The file type should be in <em>xlsx</em>." />
    </div>

    <div class="grid_10 inline alpha">
        <asp:FileUpload ID="FileUpload1" CssClass="nicefileinput nice" runat="server" />
        <br />
        <asp:Button ID="btnUpload" Text="Upload" runat="server" OnClick="Upload" Style="display: none" />
    </div>

    <div class="grid_9 inline omega">
        <asp:Label ID="lblFilename" runat="server" Text="Filename" Width="75"></asp:Label>
        <asp:TextBox ID="txtFilename" runat="server" CssClass="text" Width="258" 
            ReadOnly="true" BackColor="#FFFFCC"  ></asp:TextBox> 
    </div>

    <br class="clear" />
    <asp:Literal EnableViewState="false" runat="server" ID="ltlMessage"></asp:Literal>

    <div class="grid-viewer grid_19 clearfix">
        <asp:GridView ID="gvSheet" runat="server"  EnableViewState="true"
                RowStyle-VerticalAlign="Bottom" HeaderStyle-Wrap="false"
                HeaderStyle-ForeColor="White" AllowSorting="false"
                AllowPaging="false" SkinID="GridView"
                DataKeyNames="ID,OrderDate,DeliveryDate,OrderedBy,OrderedByX,DeliverTo,DeliverToX,YourRef,YourRefX,CustomerPartNo,IchikohPartNo,Quantity,SalesPrice"
                OnRowDataBound="gvSheet_RowDataBound" >
            <Columns>
                <asp:BoundField DataField="ID" HeaderText="No." ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="Vendor" HeaderText="VEND" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OrderedBy" HeaderText="Ordered by" Visible="false" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="DeliverTo" HeaderText="Deliver to" Visible="false" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="CustomerPartNo" HeaderText="P/NO" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="IchikohPartNo" HeaderText="Ichikoh part no." Visible="false" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="CustomerPartName" HeaderText="P/NAME" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="Description" HeaderText="PO NUMBER" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="OrderDate" HeaderText="ORDER DATE" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="DeliveryDate" HeaderText="DELIVERY DATE" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" DataFormatString="{0:dd/MM/yyyy}" />
                <asp:BoundField DataField="Quantity" HeaderText="QTY ORDER" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="DeliveryOrderNo" HeaderText="Delivery order no." Visible="false" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="ManifestNo" HeaderText="DN/NO" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="YourRef" HeaderText="BARCODE NO" ReadOnly="True" ItemStyle-HorizontalAlign="Left" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="RIT" HeaderText="RIT" Visible="false" ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
                <asp:BoundField DataField="SalesPrice" HeaderText="Sales Price" Visible="false"  ReadOnly="True" ItemStyle-HorizontalAlign="Right" ItemStyle-Wrap="false" />
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>
