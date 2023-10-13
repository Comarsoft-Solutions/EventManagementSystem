<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="MemberEvaluations.aspx.cs" Inherits="AGMSystem.Events.MemberEvaluations" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    
     <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <h4 class="mb-sm-0">Member Evaluation </h4>
             <div class="page-title-right">
                 <ol class="breadcrumb m-0">
                     <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                     <li class="breadcrumb-item active">Member Evaluation </li>
                 </ol>
             </div>
         </div>
     </div>
 </div>

          <div class="row row-cols-3">
<div class="col-4 mb-3">
    <label for="First Name" class="form-label">First Name</label>
    <asp:TextBox ID="txtFirstName" placeholder="Mali" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
</div>
<div class="col-4 mb-3">
    <label for="Last Name" class="form-label">Last Name </label>
    <asp:TextBox ID="txtLastName" placeholder="8 to 10 Sept 2023" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
</div>
<div class="col-4 mb-3">
    <label for="organiser" class="form-label">Company</label>
    <asp:TextBox ID="txtCompany" placeholder="ZAPF" CssClass="form-control" ReadOnly="true" runat="server"></asp:TextBox>
</div>
          </div>

    <asp:HiddenField ID="txtmemberID" runat="server" />
      <div class="form-group row gutters">

    <div class="form-group row gutters col-12">
        <%--<div class="row col-2"  > <asp:Button visible="false" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-success" runat="server" Text="Export to Excel" />  </div>--%>
        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdEventEvaluations" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                DataKeyNames="ID"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" >
                <Columns>
                   <asp:BoundField Visible="false" DataField="ID" HeaderText=" ID"></asp:BoundField>
                                    <asp:BoundField DataField="Question" HeaderText="Question "></asp:BoundField>
                                    <asp:BoundField DataField="Response" HeaderText="Response"></asp:BoundField>
                    <%--<asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="Select" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                            <asp:LinkButton ID="Edit" runat="server" ForeColor="Green" CssClass="mdi mdi-pencil" CommandArgument='<%#Eval("ID")%>' CommandName="editRecord"></asp:LinkButton>
                            <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="deleteRecord"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>

      <div class="form-group row gutters">

    <div class="form-group row gutters col-12">
        <%--<div class="row col-2"  > <asp:Button visible="false" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-success" runat="server" Text="Export to Excel" />  </div>--%>
        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdPresentationEvaluation" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                DataKeyNames="ID" OnPageIndexChanging="grdPresentationEvaluation_PageIndexChanging"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" >
                <Columns>
                   <asp:BoundField Visible="false" DataField="ID" HeaderText=" ID"></asp:BoundField>
                                    <asp:BoundField DataField="Question" HeaderText="Question "></asp:BoundField>
                                    <asp:BoundField DataField="Rating" HeaderText="Rating"></asp:BoundField>
                                    <asp:BoundField DataField="Comment" HeaderText="Comment"></asp:BoundField>
                  <%--  <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="Select" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                            <asp:LinkButton ID="Edit" runat="server" ForeColor="Green" CssClass="mdi mdi-pencil" CommandArgument='<%#Eval("ID")%>' CommandName="editRecord"></asp:LinkButton>
                            <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="deleteRecord"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>

</asp:Content>
