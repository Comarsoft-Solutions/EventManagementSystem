<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="ProjectEnquiries.aspx.cs" Inherits="AGMSystem.ProjectEnquiries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Project Enquiries</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Projects</a></li>
                        <li class="breadcrumb-item active">Enquiries</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div class="table-responsive table-card">
        <div class="col-xl-11" style="margin: 0 auto; text-align: center;">
            <table style="width: 100%">

                <tr>
                    <td colspan="12">
                        <asp:HiddenField ID="txtEventID" runat="server" />
                    </td>
                </tr>
                <tr>
                    <td colspan="2">
                        <asp:TextBox ID="txtProjectName" placeholder="Project Name" CssClass="form-control" runat="server"></asp:TextBox>
                    </td>
                    <td></td>
                    <td colspan="2">
                        <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" Class="pull-right" runat="server" Text="Filter Search" />
                    </td>
                </tr>
            </table>
            <br />
        </div>
        <div class="col-xl-11" style="margin: 0 auto; text-align: center;">
            <div class="card">
                <div class="card-header align-items-center d-flex">
                    <h2 class="card-title mb-0 flex-grow-1">Projects</h2>

                </div>
                <!-- end card header -->

                <div class="card-body">

                    <div class="col-sm-12 align-content-center" style="margin: 0 auto; text-align: left;">
                        <asp:GridView ID="grdProjectEnquiries" Width="100%" runat="server"
                            AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdProjectEnquiries_PageIndexChanging"
                            DataKeyNames="ID"
                            CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                            Style="border-collapse: collapse !important"
                            AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdProjectEnquiries_RowCommand">
                            <Columns>
                                <asp:BoundField DataField="Name" HeaderText="Project Name"></asp:BoundField>
                                <asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
                                <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                                <asp:BoundField DataField="MaturityDate" HeaderText="Maturity Date"></asp:BoundField>
                                <asp:BoundField DataField="ProjectStatus" HeaderText="Project Status"></asp:BoundField>
                                <asp:TemplateField HeaderText="Action">
                                    <ItemTemplate>
                                        <asp:LinkButton ID="View" runat="server" ForeColor="green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="viewRecord"></asp:LinkButton>
                                        <asp:LinkButton ID="Attach" runat="server" ForeColor="Black" CssClass="mdi mdi-attachment" CommandArgument='<%#Eval("ID")%>' CommandName="attachRecord"></asp:LinkButton>
                                    </ItemTemplate>
                                </asp:TemplateField>
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
        </div>
    </div>
</asp:Content>
