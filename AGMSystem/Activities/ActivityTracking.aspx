<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="ActivityTracking.aspx.cs" Inherits="AGMSystem.Activities.Activity_tracking" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
                 <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Activity Tracking</h4>

                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                                            <li class="breadcrumb-item active">Activity Tracking</li>
                                        </ol>
                                    </div>

                                </div>
                            </div>
                        </div>
                        <!-- end page title -->
    
    <asp:HiddenField ID="txtActivityID" runat="server" />
                                     <div class="table-responsive table-card">
                                         <div class="mb-3">
            <label for=" Events" class="form-label">Select Event <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtEvents" CssClass="form-control" AutoPostBack="true" OnTextChanged="txtEvents_TextChanged" runat="server" ></asp:DropDownList>
        </div>
                    <%--                     <div class="mb-3">
            <label for=" Events" class="form-label">Select Event <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtActivities" OnTextChanged="txtActivities_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>--%>

                                         <asp:Panel ID="pnlActivities" Visible="false" runat="server">
      <div class="form-group row gutters">

        <div class="form-group row gutters col-12">
            <%--<div class="row col-2"  > <asp:Button visible="false" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-success" runat="server" Text="Export to Excel" />  </div>--%>
            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdActivities" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdActivities_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdActivities_RowCommand">
                    <Columns>
                       <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText="Avtivity Name "></asp:BoundField>
                                        <asp:BoundField DataField="StartDate" HeaderText="Date"></asp:BoundField>
                        <asp:TemplateField HeaderText="Confirm">
                            <ItemTemplate>
                                <asp:LinkButton ID="Select" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                                
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
                                             </asp:Panel>
                                         <asp:Panel ID="pnltracking" Visible="false" runat="server"> 
                                             
                                             <div class="row">
                                                  <div class="col-6">
     <%-- name --%>
     <div class="mb-3">
         <label for="Activity Name" class="form-label">Activity Name <span class="text-danger">*</span></label>
         <asp:TextBox ID="txtActivity" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
     </div>
  
 </div>
 
 <div class="col-6">
     <%-- Start Date  --%>
     <div class="mb-3">
         <label for="Date" class="form-label">Date  <span class="text-danger">*</span></label>
         <asp:TextBox ID="txtDate" CssClass="form-control" placeholder="Surname" runat="server" ></asp:TextBox>
     </div>
 </div>
                                                 
                                                 <%-- barcode --%>
                                                 <div class="col-6">
   
    <div class="mb-3">
        <label for="Barcode " class="form-label">Barcode <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtBarcode" CssClass="form-control" placeholder="Full Name as on tag" runat="server"></asp:TextBox>
    </div>
 
</div>
                                                                                                  <%-- Save btn --%>
                                                 <div class="col-6">
   
    <div class="mb-3 mt-4">
        <asp:Button ID="btnAdd" OnClick="btnAdd_Click" CssClass="btn btn-success" runat="server" Text="Add" />
    </div>
 
</div>

                                             </div>
                                         </asp:Panel>
                                           <div class="form-group row gutters">

    <div class="form-group row gutters col-12">
        <%--<div class="row col-2"  > <asp:Button visible="false" ID="btnExport" OnClick="btnExport_Click" CssClass="btn btn-success" runat="server" Text="Export to Excel" />  </div>--%>
        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdActivityTracking" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdActivityTracking_PageIndexChanging"
                DataKeyNames="ID"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdActivityTracking_RowCommand">
                <Columns>
                   <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                    <asp:BoundField DataField="FirstName" HeaderText="First Name "></asp:BoundField>
                                    <asp:BoundField DataField="LastName" HeaderText="Last Name"></asp:BoundField>
                                    <asp:BoundField DataField="PensionFund" HeaderText="Company"></asp:BoundField>
                  <%--  <asp:TemplateField HeaderText="Confirm">
                        <ItemTemplate>
                            <asp:LinkButton ID="Select" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                            
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>
</div>
</asp:Content>
