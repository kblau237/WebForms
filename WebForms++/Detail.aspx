<%@ Page Title="" ClientIDMode="Static" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Detail.aspx.cs" Inherits="WFPlusPlusBiggy._Detail" %>

<%--clientidmode static for ids and jquery do i need this?, well alert($("#txtName").val()) wont work without it --%>

<%--added clientidmode kmb--%>
<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">
    <style type="text/css">
        .redBorder {
            border: 1px solid red;
        }

        .hide {
            visibility: hidden;
        }

        .red {
            color: red;
            position: relative;
            top: -5px;
        }

        .moveDown {
            position: relative;
            top: 5px;
        }

        .noshow {
            visibility: hidden;
        }
    </style>

    <script type="text/javascript">

        function sendRegistration() {
            var arForm = $("#ctl01").serializeArray();

            $.ajax({
                url: "Detail.aspx/PostDetail",
                type: "POST",
                contentType: "application/json; charset=utf-8",
                data: JSON.stringify({ formVars: arForm }),
                dataType: "json",
                success: function (result) {

                    if (result.d == "Success") {
                        window.location.href = "BiggySearch.aspx";
                        return;
                    }

                    $("input").removeClass("redBorder");
                    $.each($.parseJSON(result.d), function (idx, obj) {
                        $("input[id='" + obj.key + "']").addClass("redBorder");

                        $("div[id='" + obj.key + "Div']").removeClass("hide");
                        $("div[id='" + obj.key + "Err']").text(obj.error);
                    });

                },
                error: function (xhr, status) {
                    alert("An error occurred: " + status + " " + xhr);
                }
            });
        }

        $(function () {

            $("#btnDelete").click(function () {
                if (confirm("Are you sure?")) {
                    $.ajax({
                        url: 'Detail.aspx/Delete',
                        type: "POST",
                        data: "{'theId':'" + $("#txtSalesOrderDetailId").text() + "'}",
                        asynch: false,
                        contentType: "application/json; charset=utf-8",
                        dataType: "json",
                        success: function (result) {

                            if (result.d == "Success") {
                                window.location.href = "BiggySearch.aspx";
                                return;
                            }
                        },
                        failure: function (response) {
                            alert(response.d);
                        }
                    }).done(function (response) {
                        //alert("done "+response );
                    });
                }

            })

            $.ajax({
                url: 'Detail.aspx/GetSelecting',
                type: "POST",
                asynch: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {
                    data = JSON.parse(data.d);
                    $.each(data, function (i, anObj) {
                        $("#txtMySelect").append($('<option>').text(anObj.Text).attr('value', anObj.Value));
                    });
                },
                failure: function (response) {
                    alert(response.d);
                }
            }).done(function (response) {
                //alert("done "+response );
            });

            $.ajax({
                url: 'Detail.aspx/GetFields',
                type: "POST",
                data: "{'theHiddenValue':'" + $("#theHiddenValue").val() + "'}",
                asynch: false,
                contentType: "application/json; charset=utf-8",
                dataType: "json",
                success: function (data) {

                    if (data.d == "Add") {

                        $("#btnDelete").addClass("noshow");

                        return;
                    }

                    data = JSON.parse(data.d);
                    $("#txtSalesOrderDetailId").text(data[0].SalesOrderDetailID);
                    $("#txtSalesOrderID").val(data[0].SalesOrderID);
                    $("#txtCarrierTrackingNumber").val(data[0].CarrierTrackingNumber);
                    $("#txtOrderQty").val(data[0].OrderQty);
                    $("#txtProductID").val(data[0].ProductID);
                    $("#txtSpecialOfferID").val(data[0].SpecialOfferID);
                    $("#txtUnitPrice").val(data[0].UnitPrice);
                    $("#txtUnitPriceDiscount").val(data[0].UnitPriceDiscount);
                    $("#txtSalesOrderDetailID").text(data[0].SalesOrderDetailID);
                    $("#txtrowguid").text(data[0].rowguid);
                    $("#txtModifiedDate").text(data[0].ModifiedDate);

                    $("#hiddenSalesOrderDetailId").val(data[0].SalesOrderDetailID);
                    $("#hiddenrowguid").val(data[0].rowguid);
                    $("#hiddenModifiedDate").val(data[0].ModifiedDate);

                    $("#txtMySelect").val(data[0].ForeignToNames);
                },
                failure: function (response) {
                    alert(response.d);
                }
            }).done(function (response) {

            });

            $("#btnSubmit").click(sendRegistration);

            $("#btnNew").click(function () {
                $("#txtSalesOrderDetailId").text("");
                $("#txtSalesOrderID").val("");
                $("#txtCarrierTrackingNumber").val("");
                $("#txtOrderQty").val("");
                $("#txtProductID").val("");
                $("#txtSpecialOfferID").val("");
                $("#txtUnitPrice").val("");
                $("#txtUnitPriceDiscount").val("");
                $("#txtSalesOrderDetailID").text("");
                $("#txtrowguid").text("");
                $("#txtModifiedDate").text("");

                $("#hiddenSalesOrderDetailId").val("");
                $("#hiddenrowguid").val("");
                $("#hiddenModifiedDate").val("");
            })
        });
    </script>

    <div>
        <div>Sales Order Id:</div>
        <div>
            <input type="text" id="txtSalesOrderID" name="txtSalesOrderID" />
            <div id="txtSalesOrderIDErr" class="red" />
        </div>
    </div>
    <div>
        <div>Sales Order Detail Id:</div>
        <div>
            <div id="txtSalesOrderDetailId" />

        </div>
        <input type="hidden" id="hiddenSalesOrderDetailId" name="hiddenSalesOrderDetailId" />
        <div id="txtCompanyDiv" class="hide">
            <div id="txtCompanyErr" class="red" />
        </div>
    </div>
    <div>
        <div>Carrier Tracking Number:</div>
        <div>
            <input type="text" id="txtCarrierTrackingNumber" name="txtCarrierTrackingNumber" />
            <div id="txtCarrierTrackingNumberErr" class="red" />
        </div>
    </div>
    <div>
        <div>Order Qty:</div>
        <div>
            <input type="text" id="txtOrderQty" name="txtOrderQty" />
            <div id="txtOrderQtyErr" class="red" />
        </div>
    </div>
    <div>
        <div>Product Id:</div>
        <div>
            <input type="text" id="txtProductID" name="txtProductID" />
            <div id="txtProductIDErr" class="red" />
        </div>
    </div>
    <div>
        <div>Special Offer Id:</div>
        <div>
            <input type="text" id="txtSpecialOfferID" name="txtSpecialOfferID" />
            <div id="txtSpecialOfferIDErr" class="red" />
        </div>
    </div>
    <div>
        <div>Unit Price:</div>
        <div>
            <input type="text" id="txtUnitPrice" name="txtUnitPrice" />
            <div id="txtUnitPriceErr" class="red" />
        </div>
    </div>
    <div>
        <div>Unit Price Discount:</div>
        <div>
            <input type="text" id="txtUnitPriceDiscount" name="txtUnitPriceDiscount" />
            <div id="txtUnitPriceDiscountErr" class="red" />
        </div>
    </div>
    <div>
        <div>row guid:</div>
        <div>
            <div id="txtrowguid" />

        </div>
        <input type="hidden" id="hiddenrowguid" name="hiddenrowguid" />
    </div>
    <div>
        <div>Modified Date:</div>
        <div>
            <div id="txtModifiedDate" />

        </div>
        <input type="hidden" id="hiddenModifiedDate" name="hiddenModifiedDate" />
    </div>
    <div class="moveDown">
        <select id="txtMySelect" name="txtMySelect">
        </select>
    </div>
    <div class="moveDown">
        <input type="button" id="btnSubmit" value="Save" />
        <input type="button" id="btnDelete" value="Delete" />
    </div>
    <input type="hidden" id="theHiddenValue" name="theHiddenValue" runat="server" />
</asp:Content>
