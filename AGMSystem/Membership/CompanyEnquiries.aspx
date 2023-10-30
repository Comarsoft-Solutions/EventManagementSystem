<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="CompanyEnquiries.aspx.cs" Inherits="AGMSystem.Membership.CompanyEnquiries" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Member Enquiries</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Membership</a></li>
                                            <li class="breadcrumb-item active">Company Enquiries</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
    <div class="table-responsive table-card">
        <table style="width: 100%">

        <tr>
            <td colspan="12">
                <asp:HiddenField ID="txtEventID" runat="server" />
            </td>
        </tr>
                                                     <tr>
           
            <td colspan="2"><asp:TextBox ID="txtCompanySearch" placeholder="Company" CssClass="form-control" runat="server"></asp:TextBox></td>
         
            <td colspan="2"></td>
            <td colspan="2">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" runat="server" Text="Filter Search" />
            </td>
        </tr>
                                                     </table>

      <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdCompanyEnquiries" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdMemberEnquiries_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdCompanyEnquiries_RowCommand">
                    <Columns>
                        <asp:BoundField Visible="false" DataField="ID" HeaderText="ID"></asp:BoundField>
                        <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                        <asp:BoundField DataField="Address" HeaderText="Address"></asp:BoundField>
                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number"></asp:BoundField>
                        <asp:BoundField DataField="EmailAddress" HeaderText="Email Address"></asp:BoundField>
                        <asp:TemplateField HeaderText="Edit">
                            <ItemTemplate>
                                <asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-pencil" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Delete">
                            <ItemTemplate>
                                <asp:LinkButton ID="Edit" runat="server" ForeColor="red" CssClass="mdi mdi-delete-forever" CommandArgument='<%#Eval("ID")%>' CommandName="deleteRecord"></asp:LinkButton>
                               
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</div>
</asp:Content>
