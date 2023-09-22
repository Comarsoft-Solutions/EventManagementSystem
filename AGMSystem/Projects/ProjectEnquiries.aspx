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
        <table style="width: 100%">

        <tr>
            <td colspan="12">
                <asp:HiddenField ID="txtEventID" runat="server" />
            </td>
        </tr>
        <tr>
           <%-- <td colspan="2">
                <asp:DropDownList ID="cboEvent" CssClass="form-control dropdown-mega" AutoPostBack="true"  runat="server"></asp:DropDownList>
            </td>--%>
            <td colspan="2">
                <asp:TextBox ID="txtProjectName" placeholder="Project Name" CssClass="form-control" runat="server"></asp:TextBox>
            </td>
            <td ></td>
            <td colspan="2">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" Class="pull-right" runat="server" Text="Filter Search" />
            </td>
        </tr>
                                                     </table>
    <%--<table class="table table-nowrap mb-0">
        <thead class="table-light">
            <tr>
                <th scope="col">Project Name</th>
                <th scope="col">Start Date</th>
                <th scope="col">Maturity Date</th>
                <th scope="col">Status</th>
                <th scope="col">Action</th>
            </tr>
        </thead>
        <tbody>
            <tr>
                <td><a href="#" class="fw-semibold">Tomatoes</a></td>
                <td>07 feb, 2023 </td>
                <td>07 jun, 2023</td>
                <td><span class="badge bg-success">Paid</span></td>
                <td>
                    <asp:Button ID="Edit"  CssClass="btn btn-sm btn-success" runat="server" Text="Edit" />
                    <asp:Button ID="View"  CssClass="btn btn-sm btn-primary" runat="server" Text="View" />
                    <asp:Button ID="Delete"  CssClass="btn btn-sm btn-danger" runat="server" Text="Delete" />

                </td>
            </tr>
            <tr>
                <td><a href="#" class="fw-semibold">Avocado</a></td>
                <td>23 Jan, 2023</td>
                <td>23 Dec, 2023</td>
                <td><span class="badge bg-danger">Inactive</span></td>
                 <td>
                    <asp:Button ID="Button1"  CssClass="btn btn-sm btn-success" runat="server" Text="Edit" />
                    <asp:Button ID="Button2"  CssClass="btn btn-sm btn-primary" runat="server" Text="View" />
                    <asp:Button ID="Button3"  CssClass="btn btn-sm btn-danger" runat="server" Text="Delete" />

                </td>
            </tr>
          
            
        </tbody>
    </table>--%>
        
      <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
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
                                <%--<asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-pencil" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>--%>
                                <asp:LinkButton ID="View" runat="server" ForeColor="green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="viewRecord"></asp:LinkButton>
                                <%--<asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>--%>
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
</asp:Content>
