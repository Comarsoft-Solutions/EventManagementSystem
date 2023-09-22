<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="ProjectMembership.aspx.cs" Inherits="AGMSystem.ProjectMembership" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0"> Project Membership</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item active">Projects</li>
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
                <asp:HiddenField ID="txtID" runat="server" />
                <asp:HiddenField ID="txtMemberID" runat="server" />
                <asp:HiddenField ID="txtProjectID" runat="server" />
            </td>
        </tr>
                                                     <tr>
            <td colspan="2">
                <asp:DropDownList ID="cboProjects" CssClass="form-control dropdown-mega" AutoPostBack="true"  runat="server"></asp:DropDownList>
            </td>
            <td colspan="2">
                <asp:TextBox ID="txtFnameSearch" placeholder="Firstname" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
            <td colspan="2"><asp:TextBox ID="txtLnameSearch" placeholder="Lastname" CssClass="form-control" runat="server"></asp:TextBox></td>
            <td colspan="2">
                <asp:TextBox ID="txtNationalID" placeholder="IdentityNo" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
            <td colspan="2"></td>
            <td colspan="2">
                <asp:Button ID="btnSearch"  CssClass="btn btn-primary" runat="server" Text="Filter Search" />
            </td>
        </tr>
                                                     </table>
        <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdProjectMembership" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdProjectMembership_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdProjectMembership_RowCommand">
                    <Columns>
                        <asp:CheckBoxField />
                        <asp:BoundField DataField="FirstName" HeaderText="First Name"></asp:BoundField>
                        <asp:BoundField DataField="LastName" HeaderText="Last Name"></asp:BoundField>
                        <asp:BoundField DataField="PensionFund" HeaderText="Pension Fund"></asp:BoundField>
                        <%--<asp:BoundField DataField="Company" HeaderText="Company Name"></asp:BoundField>--%>
                        <%--<asp:BoundField DataField="Status" HeaderText="Status"></asp:BoundField>--%>
                        <%--<asp:TemplateField HeaderText="Action">
                            <ItemTemplate>
                                <asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-pencil" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                                <asp:LinkButton ID="View" runat="server" ForeColor="green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                                <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                            </ItemTemplate>
                        </asp:TemplateField>--%>
                        <asp:TemplateField HeaderText="Add">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Add" runat ="server" ForeColor="Green" CssClass="mdi mdi-plus-thick" CommandArgument='<%#Eval("ID")%>' CommandName ="addMember" ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>     
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
        
            
            <div class="text-end">
                <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Save" />
            </div>
</div>

</asp:Content>
