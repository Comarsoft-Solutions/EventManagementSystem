<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="Activities.aspx.cs" Inherits="AGMSystem.Events.Activities" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Activities Creation</h4>

                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                                            <li class="breadcrumb-item active">Activities Creation</li>
                                        </ol>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- end page title -->
    
     
                                     <div class="table-responsive table-card">
                                         <div class="mb-3">
            <label for=" Events" class="form-label">Select Event <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtEvents" OnTextChanged="txtEvents_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>
  <div class="col-6">
      <%-- name --%>
      <div class="mb-3">
          <label for="First Name" class="form-label">Activity Name  <span class="text-danger">*</span></label>
          <asp:TextBox ID="txtName" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
      </div>
   
  </div>
  
  <div class="col-6">
      <%-- Start Date  --%>
      <div class="mb-3">
          <label for="Last Name" class="form-label">Date <span class="text-danger">*</span></label>
          <asp:TextBox ID="txtStartDate" CssClass="form-control" placeholder="Surname" runat="server" TextMode="Date"></asp:TextBox>
      </div>
  </div>

         
        <div class="mt-4">

        <asp:Button ID="btnSave" Width="50%" OnClick="btnSave_Click" CssClass="btn btn-success" runat="server" Text="Create" Style="left: 0px; top: 0px" />

    </div>
      <div class="form-group row gutters">

        <div class="form-group row gutters col-12">
            <%--<div class="row col-2"  > <asp:Button visible="false" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-success" runat="server" Text="Export to Excel" />  </div>--%>
            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdActivities" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdActivities_RowCommand">
                    <Columns>
                       <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Avtivity Name "></asp:BoundField>
                                        <asp:BoundField DataField="StartDate" HeaderText="Date"></asp:BoundField>
                       <%-- <asp:TemplateField HeaderText="Confirm">
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
</div>
</asp:Content>
