<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="AddPresenter.aspx.cs" Inherits="AGMSystem.Events.AddPresenter" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
    <div class="col-12">
        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
            <h4 class="mb-sm-0">Event Evaluation</h4>
            <div class="page-title-right">
                <ol class="breadcrumb m-0">
                    <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                    <li class="breadcrumb-item active">Evaluation</li>
                </ol>
            </div>
        </div>
    </div>
</div>
<asp:HiddenField ID="txtMemberID" runat="server" />
<asp:HiddenField ID="txtname" runat="server" />
<asp:HiddenField ID="txtEventID" runat="server" />

    
    <div class="col-12"> 
        <asp:Label runat="server"> Select Event </asp:Label>
        <asp:DropDownList runat="server" CssClass="form-control" id="txtEvents" AutoPostBack="true" OnTextChanged="txtEvents_TextChanged"></asp:DropDownList>
    </div>
                           <table style="width: 100%">

    <tr>
        <td colspan="12">
            <asp:HiddenField ID="HiddenField1" runat="server" />
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
    <asp:Panel ID="pnlEnquiries" Visible="false" runat="server">
        <div class="form-group row gutters">

    <div class="form-group row gutters col-12">

        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdMemberEnquiries" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                DataKeyNames="ID"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdMemberEnquiries_RowCommand">
                <Columns>
                    <asp:BoundField Visible="false" DataField="ID" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="FirstName" HeaderText="FirstName"></asp:BoundField>
                    <asp:BoundField DataField="LastName" HeaderText="LastName"></asp:BoundField>
                    <asp:BoundField DataField="PensionFund" HeaderText="Company "></asp:BoundField>
                    <asp:BoundField DataField="PhoneNumber" HeaderText="PhoneNumber"></asp:BoundField>
                    <asp:BoundField DataField="Email" HeaderText="Email"></asp:BoundField>
                    <asp:TemplateField HeaderText="Add">
                        <ItemTemplate>
                            <asp:LinkButton ID="Attach" runat="server" ForeColor="Green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="select"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>
    </asp:Panel>
    
        <div class="form-group row gutters">

            <div class="form-group row gutters col-8">

                <div class="col-sm-12 align-content-center">
                    <asp:GridView ID="grdPresenters" Width="100%" runat="server"
                        AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                        DataKeyNames="ID"
                        CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                        Style="border-collapse: collapse !important"
                        AllowPaging="True" AllowSorting="True" >
                        <Columns>
                            <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                            <asp:BoundField DataField="Company" HeaderText="Company"></asp:BoundField>
                            <%--<asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>--%>
                            <asp:TemplateField HeaderText="Delete">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lnkRecSel" runat="server" ForeColor="red" Text="Edit" CommandName="selectrecord" CommandArgument="<%# Container.DataItemIndex %>">
                                                      
                                    </asp:LinkButton>

                                </ItemTemplate>
                            </asp:TemplateField>


                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
</asp:Content>
