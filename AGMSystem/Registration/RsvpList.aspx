<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="RsvpList.aspx.cs" Inherits="AGMSystem.RsvpList" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

         <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Payment Confirmation</h4>

                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Registration</a></li>
                                            <li class="breadcrumb-item active">Payment Confirmation</li>
                                        </ol>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- end page title -->
    
     
                                     <div class="table-responsive table-card">
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
            <td colspan="2"><asp:TextBox ID="txtLnameSearch" placeholder="Lastname" CssClass="form-control" runat="server"></asp:TextBox></td>
            <td colspan="2"><asp:TextBox ID="txtCompanySearch" placeholder="Comapany" CssClass="form-control" runat="server"></asp:TextBox></td>
         
            <td colspan="2"></td>
            <td colspan="2">
                <asp:Button ID="btnSearch" OnClick="btnSearch_Click" CssClass="btn btn-primary" runat="server" Text="Filter Search" />
            </td>
        </tr>
                                                     </table>
                                         <div class="mb-3">
            <label for=" Resolution" class="form-label">Select Event <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtEvents" OnTextChanged="txtEvents_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>

      <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdPaymentConfirmation" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdPaymentConfirmation_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdPaymentConfirmation_RowCommand">
                    <Columns>
                       <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name "></asp:BoundField>
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name"></asp:BoundField>
                                        <asp:BoundField DataField="TshirtSize" HeaderText="T-shirt Size"></asp:BoundField>
                                        <asp:BoundField DataField="Golf" HeaderText="Golf"></asp:BoundField>
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                               <%-- <asp:LinkButton ID="View" runat="server" ForeColor="green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                                <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                                <asp:LinkButton ID="Attach" runat="server" ForeColor="Black" CssClass="mdi mdi-attachment" CommandArgument='<%#Eval("ID")%>' CommandName="attachRecord"></asp:LinkButton>--%>
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</div>
</asp:Content>
