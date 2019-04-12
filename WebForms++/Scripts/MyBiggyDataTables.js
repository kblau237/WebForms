$(document).ready(function () {
    $('#datatab').DataTable(
     {
         "processing": true,
         "serverSide": true,
         "ajax":
             {
                 "url": "BiggySearch.aspx/GetData",
                 "contentType": "application/json",
                 "type": "GET",
                 "dataType": "JSON",
                 "data": function (d) {
                     //into iis
                     d.columns = "";
                     d.mydata1 = $("#theSearch").val();
                     return d;
                 },
                 "dataSrc": function (json) {
                     //from iis to here
                     json.draw = json.d.draw;
                     json.recordsTotal = json.d.recordsTotal;
                     json.recordsFiltered = json.d.recordsFiltered;
                     json.data = json.d.data;

                     var return_data = json;
                     return return_data.data;
                 }
             },
         "columns": [
                     { "data": "SalesOrderID" },
                     { "data": "SalesOrderDetailID" },
                     { "data": "CarrierTrackingNumber" }
         ]
         ,
         "createdRow": function (row, data, dataIndex) {
             $(row).click(function () {
                 $("#hiddenRowNumber").val(data.SalesOrderDetailID)
                 $("form").submit();
             });
         }
     });

    $("#btnSearch").click(function () {
        if ($.fn.dataTable.isDataTable('#datatab')) {
            table = $('#datatab').DataTable();
            //https://datatables.net/reference/api/ajax.reload()
            table.ajax.reload();
        }
    });

    $("#btnAddBiggy").click(function () {

        $("#hiddenRowNumber").val("add");
        $("form").submit();

    });
});