<%@ Page Title="Reference" ClientIDMode="Static" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="Reference.aspx.cs" Inherits="WFPlusPlusBiggy._Reference" %>

<%--clientidmode--%>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <h2><%: Title %></h2>
    <h3>Reference</h3>
    <style type="text/css">
        .noshow {
            visibility: hidden;
        }
    </style>

    <script type="text/javascript">
        $(function () {

            var p = $("#example");
            $("#example").DataTable({
                "order": [[0, "asc"]],
                "pagingType": "full",
                "deferRender": true
            });

            //readonly the id
            $("#param0").attr("readonly", "readonly");

            $("#btnAdd").click(function () {

                if ($(this).val() == "Add Reference") //general note
                {
                    $("#Button2").addClass("noshow");

                    $('#myModalEditReference').modal('show');
                    $("#myModalEditReference").draggable({
                        handle: ".modal-header"
                    });

                }
            })

            function EditJS(fromDomElem, param0, param1, param2, param3, param4, param5, param6, param7) {
                $("#Button2").removeClass("noshow");
                for (var arg = 0; arg < arguments.length; ++arg) {

                    if (arg == 0) //skip fromDomElem
                    {
                        continue;
                    }

                    var arr = arguments[arg];

                    if (fromDomElem == "ReferenceRows") {
                        theParam = arg - 1;  //see view, increment by 10 to be unique grid, base 0
                    }

                    if (arr != "True" && arr != "False") {
                        $("#" + "param" + theParam).val(arr);

                    }
                    else {
                        $("#" + "param" + theParam).attr('checked', $.parseJSON(arr.toLowerCase()));
                    }

                }
                if (fromDomElem == "ReferenceRows") {

                    $('#myModalEditReference').modal('show');
                    $("#myModalEditReference").draggable({
                        handle: ".modal-header"
                    });
                }
            }

            (function ($) {
                var table = document.getElementById("example");
                var rows = $(".theEdit");
                for (i = 0; i < rows.length; i++) {
                    var currentRowMain = table.rows[i + 1];
                    var currentRow = $(".theEdit")[i];
                    var createClickHandler =
                        function (row) {
                            return function () {
                                var cell = row.getElementsByTagName("td")[0];
                                var cellName = row.getElementsByTagName("td")[1];
                                var cellPosition = row.getElementsByTagName("td")[2];
                                var cellOffice = row.getElementsByTagName("td")[3];
                                var cellAge = row.getElementsByTagName("td")[4];
                                var cellStartDate = row.getElementsByTagName("td")[5];
                                var cellSalary = row.getElementsByTagName("td")[6];
                                var id = cell.innerHTML;
                                var idName = cellName.innerHTML;
                                var idPosition = cellPosition.innerHTML;
                                var idOffice = cellOffice.innerHTML;
                                var idAge = cellAge.innerHTML;
                                var idStartDate = cellStartDate.innerHTML;
                                var idSalary = cellSalary.innerHTML;
                                EditJS("ReferenceRows", id, idName, idPosition, idOffice, idAge, idStartDate, idSalary)
                            };
                        };
                    currentRow.onclick = createClickHandler(currentRowMain);
                }
            })(jQuery);

            //delete
            (function ($) {
                var table = document.getElementById("example");
                var rows = $(".theDel");
                for (i = 0; i < rows.length; i++) {
                    var currentRowMain = table.rows[i + 1];
                    var currentRow = $(".theDel")[i];
                    var createClickHandler =
                        function (row) {
                            return function () {
                                var cell = row.getElementsByTagName("td")[0];
                                var cellName = row.getElementsByTagName("td")[1];
                                var cellPosition = row.getElementsByTagName("td")[2];
                                var cellOffice = row.getElementsByTagName("td")[3];
                                var cellAge = row.getElementsByTagName("td")[4];
                                var cellStartDate = row.getElementsByTagName("td")[5];
                                var cellSalary = row.getElementsByTagName("td")[6];
                                var id = cell.innerHTML;
                                var idName = cellName.innerHTML;
                                var idPosition = cellPosition.innerHTML;
                                var idOffice = cellOffice.innerHTML;
                                var idAge = cellAge.innerHTML;
                                var idStartDate = cellStartDate.innerHTML;
                                var idSalary = cellSalary.innerHTML;

                                $("#theDeleteId").val(id);
                                $('#myModalDelete').modal('show');

                            };
                        };
                    currentRow.onclick = createClickHandler(currentRowMain);
                }
            })(jQuery);
        })
    </script>
    <input class="btn btn-secondary" type="button" value="Add Reference" id="btnAdd" data-gridname="myModalEditReference" />
    <div class="DataTable" style="width: 100%" id="HeaderStyle">
        <asp:PlaceHolder ID="thePlaceHolder" runat="server"></asp:PlaceHolder>
    </div>

    <!-- Modal -->
    <div class="modal fade" id="myModalEditReference" tabindex="-1" role="dialog" aria-labelledby="myModalEditLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalEditLabel">Edit/Add Reference</h4>
                </div>
                <div class="modal-body">
                    <div>
                        <div class="alabel">Id:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param0" />
                        </div>
                    </div>
                    <div>
                        <div class="alabel">Name:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param1" />
                        </div>
                    </div>
                    <div>
                        <div class="alabel">Position:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param2" />
                        </div>
                    </div>
                    <div>
                        <div class="alabel">Office:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param3" />
                        </div>
                    </div>
                    <div>
                        <div class="alabel">Age:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param4" />
                        </div>
                    </div>
                    <div>
                        <div class="alabel">Start date:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param5" />
                        </div>
                    </div>
                    <div>
                        <div class="alabel">Salary:</div>
                        <div>
                            <asp:TextBox runat="server" ID="param6" />
                        </div>
                    </div>
                </div>
                <div class="modal-footer">
                    <button type="button" class="btn btn-secondary bootBtnMargin" data-dismiss="modal">Close</button>
                    <asp:Button ID="Button1" class="btn btn-primary bootBtnMargin clickOnce"
                        Text="Save Modal"
                        OnClick="SaveModal_Click"
                        runat="server" />
                </div>
            </div>
        </div>
    </div>
    <%--end of modal --%>

    <!-- Modal -->
    <div class="modal fade" id="myModalDelete" tabindex="-1" role="dialog" aria-labelledby="myModalDeleteLabel"
        aria-hidden="true">
        <div class="modal-dialog" role="document">
            <div class="modal-content smallerDeleteConfirmContent">
                <div class="modal-header">
                    <h4 class="modal-title" id="myModalDeleteLabel">Delete Master</h4>
                </div>
                <div class="modal-body smallerDeleteConfirmBody">
                    <input runat="server" type="hidden" id="theDeleteId" name="theDeleteId" />
                    <asp:Button ID="Button2" class="btn btn-primary bootBtnMargin clickOnce"
                        Text="Delete Modal"
                        OnClick="DeleteModal_Click"
                        runat="server" />
                    <button type="button" class="btn btn-secondary bootBtnMargin" data-dismiss="modal">Close</button>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
