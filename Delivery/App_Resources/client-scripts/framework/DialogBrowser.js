$(function () {
    $("#CustomerBrowser").dialog('destroy');
    $("#CustomerBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 900,
        height: 520,
        title: "Browse Customers"
    });

    $("#BrowseCustomers").off("click");
    $(document).on("click", "#BrowseCustomers", function () {
        $("#CustomerBrowser").dialog("open");
    });

    $("#SalesOrderBrowser").dialog('destroy');
    $("#SalesOrderBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 700,
        height: 520,
        title: "Browse Sales Orders"
    });

    $("#BrowseSalesOrders").off("click");
    $(document).on("click", "#BrowseSalesOrders", function () {
        $("#SalesOrderBrowser").dialog("open");
    });

    $("#SelectionCodeBrowser").dialog('destroy');
    $("#SelectionCodeBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 600,
        height: 420,
        title: "Browse Selection Codes"
    });

    $("#BrowseSelectionCodes").off("click");
    $(document).on("click", "#BrowseSelectionCodes", function () {
        $("#SelectionCodeBrowser").dialog("open");
    });

    $("#InvoiceToBrowser").dialog('destroy');
    $("#InvoiceToBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 900,
        height: 520,
        title: "Browse Customers"
    });

    $("#BrowseInvoiceTo").off("click");
    $(document).on("click", "#BrowseInvoiceTo", function () {
        $("#InvoiceToBrowser").dialog("open");
    });

    $("#DeliveryToBrowser").dialog('destroy');
    $("#DeliveryToBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 900,
        height: 520,
        title: "Browse Customers"
    });

    $("#BrowseDeliveryTo").off("click");
    $(document).on("click", "#BrowseDeliveryTo", function () {
        $("#DeliveryToBrowser").dialog("open");
    });

    $("#DealingTypeBrowser").dialog('destroy');
    $("#DealingTypeBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 600,
        height: 420,
        title: "Browse Dealing Types"
    });

    $("#BrowseDealingTypes").off("click");
    $(document).on("click", "#BrowseDealingTypes", function () {
        $("#DealingTypeBrowser").dialog("open");
    });

    $("#WarehouseBrowser").dialog('destroy');
    $("#WarehouseBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 630,
        height: 500,
        title: "Browse Warehouses"
    });

    $("#BrowseWarehouses").off("click");
    $(document).on("click", "#BrowseWarehouses", function () {
        $("#WarehouseBrowser").dialog("open");
    });

    $("#DeliveryNoteNoBrowser").dialog('destroy');
    $("#DeliveryNoteNoBrowser").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 630,
        height: 500,
        title: "Browse Delivery Notes"
    });

    $("#BrowseDeliveryNotes").off("click");
    $(document).on("click", "#BrowseDeliveryNotes", function () {
        $("#DeliveryNoteNoBrowser").dialog("open");
    });

    $("#DeleteConfirmationDialog").dialog('destroy');
    $("#DeleteConfirmationDialog").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 400,
        height: 180,
        title: "Delete Confirmation"
    });

    $("#ShowDeleteConfirmationDialog").off("click");
    $(document).on("click", "#ShowDeleteConfirmationDialog", function () {
        $("#DeleteConfirmationDialog").dialog("open");
    });

    $("#UpdateConfirmationDialog").dialog('destroy');
    $("#UpdateConfirmationDialog").dialog({
        dialogClass: 'DynamicDialogStyle',
        autoOpen: false,
        resizable: true,
        draggable: true,
        modal: true,

        open: function (type, data) {
            $(this).parent().appendTo("form");
        },

        width: 400,
        height: 220,
        title: "Update Confirmation"
    });

    $("#ShowUpdateConfirmationDialog").off("click");
    $(document).on("click", "#ShowUpdateConfirmationDialog", function () {
        $("#UpdateConfirmationDialog").dialog("open");
    });

//    $("#ChangeReceivedDateDialog").dialog('destroy');
//    $("#ChangeReceivedDateDialog").dialog({
//        dialogClass: 'DynamicDialogStyle',
//        autoOpen: false,
//        resizable: true,
//        draggable: true,
//        modal: true,

//        open: function (type, data) {
//            $(this).parent().appendTo("form");
//        },

//        width: 420,
//        height: 370,
//        title: "Change Received Date"
//    });

//    $("#ShowChangeReceivedDateDialog").off("click");
//    $(document).on("click", "#ShowChangeReceivedDateDialog", function () {
//        $("#ChangeReceivedDateDialog").dialog("open");
//    });
});

