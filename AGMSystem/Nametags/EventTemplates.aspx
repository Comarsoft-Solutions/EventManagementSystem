<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="EventTemplates.aspx.cs" Inherits="AGMSystem.Nametags.EventTemplates" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                   <div class="col-12">
                       <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                           <h4 class="mb-sm-0">Payment Confirmation</h4>

                           <div class="page-title-right">
                               <ol class="breadcrumb m-0">
                                   <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                                   <li class="breadcrumb-item active">Event Templates</li>
                               </ol>
                           </div>

                       </div>
                   </div>
               </div>
               <!-- end page title -->

                                     <div class="mb-3 col-6">
    <label for=" Resolution" class="form-label">Select Event <span class="text-danger"></span></label>
    <asp:DropDownList ID="txtEvents" OnTextChanged="txtEvents_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
</div>

     <asp:HiddenField ID="EventID" runat="server" />
 <div class="col" >
     <div class="col-6">
         <div class="mb-3">
             <label for="CompanyName" class="form-label">Event Theme</label>
             <asp:TextBox ID="txtEventTheme" placeholder="Event Theme" CssClass="form-control" runat="server"></asp:TextBox>
         </div>
     </div>
     <!--end col-->
     <div class="col-6">
         <div class="mb-3">
             <label for="Address" class="form-label">Sponsor</label>
             <asp:TextBox ID="txtSponsor" placeholder="Sponsor" CssClass="form-control" runat="server"></asp:TextBox>
         </div>
     </div>
     
     
     <!--end col-->
    
 </div>

    <div class="col-lg-12">
    <div class="text-end">
        <asp:Button ID="btnSave" OnClick="btnSave_Click" CssClass="btn btn-primary" runat="server" Text="Save" />
    </div>
</div>
</asp:Content>
