<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="EventList.aspx.cs" Inherits="AGMSystem.EventList" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Event List</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                        <li class="breadcrumb-item active">List</li>
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
                <td></td>
                <td colspan="2">
                    <asp:Button ID="btnSearch" CssClass="btn btn-primary" Class="pull-right" runat="server" Text="Filter Search" />
                </td>
            </tr>
        </table>
        <%-- End Crumb --%>
        <table class="table table-nowrap mb-0">
            <thead class="table-light">
                <tr>
                    <th scope="col">Event Name</th>
                    <th scope="col">Venue</th>
                    <th scope="col">Start Date</th>
                    <th scope="col">End Date</th>
                    <th scope="col">Status</th>
                    <th scope="col">Action</th>
                </tr>
            </thead>
            <tbody>
                <tr>
                    <td><a href="#" class="fw-semibold">Annual Conference</a></td>
                    <td>Zimbali Gardens</td>
                    <td>07 July, 2023</td>
                    <td>08 July, 2023</td>
                    <td><span class="badge bg-success">Pending</span></td>
                    <td>
                        <asp:Button ID="Edit" CssClass="btn btn-sm btn-success" runat="server" Text="Edit" />
                        <asp:Button ID="View" CssClass="btn btn-sm btn-primary" runat="server" Text="View" />
                        <asp:Button ID="Delete" CssClass="btn btn-sm btn-danger" runat="server" Text="Delete" />
                        <asp:Button ID="manage" OnClick="Button4_Click" CssClass="btn btn-sm btn-info" runat="server" Text="Manage" />

                    </td>
                </tr>
                <tr>
                    <td><a href="#" class="fw-semibold">Event B</a></td>
                    <td>Venue B</td>
                    <td>05 Jun, 2023</td>
                    <td>07 Jun, 2023</td>
                    <td><span class="badge bg-danger">Ongoing</span></td>
                    <td>
                        <asp:Button ID="Button1" CssClass="btn btn-sm btn-success" runat="server" Text="Edit" />
                        <asp:Button ID="Button2" CssClass="btn btn-sm btn-primary" runat="server" Text="View" />
                        <asp:Button ID="Button3" CssClass="btn btn-sm btn-danger" runat="server" Text="Delete" />
                        <asp:Button ID="Button4" OnClick="Button4_Click" CssClass="btn btn-sm btn-info" runat="server" Text="Manage" />

                    </td>
                </tr>


            </tbody>
        </table>
        <%-- To Do --%>
        <div class="form-group row gutters">

            <div class="form-group row gutters col-12">

                <div class="col-sm-12 align-content-center">
                    <asp:GridView ID="grdViewEvents" Width="100%" runat="server"
                        AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdViewEvents_PageIndexChanging"
                        DataKeyNames="ID"
                        CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                        Style="border-collapse: collapse !important"
                        AllowPaging="True" AllowSorting="True" OnRowCommand="grdViewEvents_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="EventName" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                            <asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>
                            <asp:TemplateField HeaderText="Edit">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRecSel" runat="server" ForeColor="green" Text="Edit" CommandName="selectrecord" CommandArgument="<%# Container.DataItemIndex %>">
                                                      
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="Register">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkReg" runat="server" ForeColor="green" Text="Register Attendees" CommandName="RegisterATT" CommandArgument="<%# Container.DataItemIndex %>">
                                                      
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>

                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
        <%-- End To Do --%>
    </div>
</asp:Content>
