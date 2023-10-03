<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="NameTagPrinting.aspx.cs" Inherits="AGMSystem.Nametags.NameTagPrinting" %>
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

                                        
                                         <div class="mb-3 col-6">
    <label for=" Resolution" class="form-label">Select Print Option <span class="text-danger"></span></label>
    <asp:DropDownList ID="cboPrintOptions" OnTextChanged="cboPrintOptions_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
</div>

    <asp:Panel ID="pnlCompanySearch" Visible="false" runat="server">
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
         <asp:Button ID="btnCompanySearch" OnClick="btnCompanySearch_Click" CssClass="btn btn-primary" runat="server" Text="Search" />
     </td>
 </tr>
                                              </table>
    </asp:Panel>
    <asp:Panel ID="pnlMemberSearch" Visible="false" runat="server">
         <table style="width: 100%">

 <tr>
     <td colspan="12">
         <asp:HiddenField ID="HiddenField1" runat="server" />
     </td>
 </tr>
                                              <tr>
     <td colspan="2">
         <asp:TextBox ID="txtFnameSearch" placeholder="First Name" CssClass="form-control" runat="server"></asp:TextBox>
     </td>
     <td colspan="2"><asp:TextBox ID="txtLnameSearch" placeholder="Last Name" CssClass="form-control" runat="server"></asp:TextBox></td>
     <td colspan="2"><asp:TextBox ID="txtNatID" placeholder="National ID" CssClass="form-control" runat="server"></asp:TextBox></td>
  
     <td colspan="2"></td>
     <td colspan="2">
         <asp:Button ID="btnMemberSearch" OnClick="btnMemberSearch_Click" CssClass="btn btn-primary" runat="server" Text="Search" />
     </td>
 </tr>
                                              </table>
    </asp:Panel>

      <div class="form-group row gutters">

    <div class="form-group row gutters col-12">
        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdMembers" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdMembers_PageIndexChanging"
                DataKeyNames="ID"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdMembers_RowCommand">
                <Columns>
                   <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name "></asp:BoundField>
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name"></asp:BoundField>
                                    <asp:BoundField DataField="NationalID" HeaderText="National ID"></asp:BoundField>
                                    <asp:BoundField DataField="PensionFund" HeaderText="Company"></asp:BoundField>
                    <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <%--<asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>--%>
                            <asp:CheckBox ID="chkEdit" runat="server"  ForeColor="blue" CommandArgument='<%#Eval("ID") %>' CommandName="selectRecord" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>
        <div class="col-lg-12">
    <div class="text-end">
        <asp:Button ID="btnPrint" OnClick="btnPrint_Click" CssClass="btn btn-primary" runat="server" Text="Print" />
    </div>
</div>
</asp:Content>
