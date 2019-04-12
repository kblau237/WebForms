<%@ Page Title="Home Page" Language="C#" MasterPageFile="~/Site.Master" AutoEventWireup="true" CodeBehind="BiggySearch.aspx.cs" Inherits="WFPlusPlusBiggy._BiggySearch" %>

<asp:Content ID="BodyContent" ContentPlaceHolderID="MainContent" runat="server">
    <input class="btn btn-secondary" type="button" value="Add" id="btnAddBiggy" />
    <%--took out font awesome, don't need--%>
    <div>
        <div class="alabel">Sales Order Detail Id:</div>
        <div>
            <input type="text" id="theSearch" />
        </div>
        <input class="btn btn-secondary" type="button" value="Show Results" id="btnSearch" />
    </div>
    <div class="DataTable" id="HeaderStyle" style="width: 100%">
        <table id="datatab" class="display table table-striped table-bordered">
            <thead>
                <tr>
                    <th>SalesOrderID</th>
                    <th>SalesOrderDetailID</th>
                    <th>CarrierTrackingNumber</th>
                </tr>
            </thead>
            <tfoot>
                <tr>
                    <th>SalesOrderID</th>
                    <th>SalesOrderDetailID</th>
                    <th>CarrierTrackingNumber</th>
                </tr>
            </tfoot>
            <tbody>
            </tbody>
        </table>
    </div>
    <input type="hidden" name="hiddenRowNumber" id="hiddenRowNumber" />
</asp:Content>
