<%@ Page Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="Enquiries.aspx.cs" Inherits="AGMSystem.Enquiries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Enquiries</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                        <li class="breadcrumb-item active">Enquiries</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <table style="width: 100%">

        <tr>
            <td colspan="12">
                <asp:HiddenField ID="txtEventID" runat="server" />
            </td>
        </tr>
        <tr>
           <%-- <td colspan="2">
                <asp:DropDownList ID="cboEvent" runat="server"></asp:DropDownList>
            </td>--%>
            <td colspan="2">
                <asp:TextBox ID="txtFnameSearch" placeholder="Annual Meeting" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
            <td colspan="2"></td>
            <td colspan="2">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" runat="server" Text="Filter Search" />
            </td>
        </tr>
    </table>

    <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdEnquiries" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdEnquiries_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdEnquiries_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="EventName" HeaderText="First Name"></asp:BoundField>
                        <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                        <asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>
                        <asp:BoundField DataField="AttendanceFee" HeaderText="Fee(US$)"></asp:BoundField>

                        <%--<asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="view" runat ="server" ForeColor="Green" CssClass="fa fa-file-archive-o fa-2x" CommandArgument='<%#Eval("ID")%>' CommandName ="selectrecord" ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>     --%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
