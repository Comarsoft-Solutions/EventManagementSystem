<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="MemberEnquiries.aspx.cs" Inherits="AGMSystem.MemberEnquiries" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Member Enquiries</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Membership</a></li>
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
                    <asp:TextBox ID="txtFnameSearch" placeholder="Firstname" CssClass="form-control" runat="server"></asp:TextBox>
                </td>
                <td colspan="2">
                    <asp:TextBox ID="txtLnameSearch" placeholder="Lastname" CssClass="form-control" runat="server"></asp:TextBox></td>
                <td colspan="2">
                    <asp:TextBox ID="txtCompanySearch" placeholder="Company" CssClass="form-control" runat="server"></asp:TextBox></td>

                <td colspan="2"></td>
                <td colspan="2">
                    <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" runat="server" Text="Filter Search" />
                </td>
            </tr>
        </table>
            </div>
        <br />

        
        <div class="col-xl-11" style="margin: 0 auto; text-align: center;">
            <div class="card" >
                <div class="card-header align-items-center d-flex">
                    <h2 class="card-title mb-0 flex-grow-1" >Members</h2>
                    
                </div>
                <!-- end card header -->

                <div class="card-body">

                <div class="col-sm-12 align-content-center">
                    <asp:GridView ID="grdMemberEnquiries" Width="100%" runat="server"
                        AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdMemberEnquiries_PageIndexChanging"
                        DataKeyNames="ID"
                        CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                        Style="border-collapse: collapse !important"
                        AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdMemberEnquiries_RowCommand">
                        <Columns>
                            <asp:BoundField Visible="false" DataField="ID" HeaderText="ID"></asp:BoundField>
                            <asp:BoundField DataField="FirstName" HeaderText="FirstName"></asp:BoundField>
                            <asp:BoundField DataField="LastName" HeaderText="LastName"></asp:BoundField>
                            <asp:BoundField DataField="PensionFund" HeaderText="Pension Fund"></asp:BoundField>
                            <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber"></asp:BoundField>
                            <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                            <asp:BoundField DataField="NationalID" HeaderText="Identity Number"></asp:BoundField>
                            <asp:TemplateField HeaderText="View">
                                <ItemTemplate>
                                    <%--<asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-pencil" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                                    <asp:LinkButton ID="View" runat="server" ForeColor="green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="viewRecord"></asp:LinkButton>
                                    <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>--%>
                                    <asp:LinkButton ID="Attach" runat="server" ForeColor="Green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="attachRecord"></asp:LinkButton>
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
