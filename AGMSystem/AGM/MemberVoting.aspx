<%@ Page Title="" Language="C#" MasterPageFile="~/OnlinePages.Master" AutoEventWireup="true" CodeBehind="MemberVoting.aspx.cs" Inherits="AGMSystem.AGM.MemberVoting" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <h4 class="mb-sm-0">Member Voting</h4>
             <div class="page-title-right">
                 <ol class="breadcrumb m-0">
                     <li class="breadcrumb-item"><a href="javascript: void(0);">AGMS</a></li>
                     <li class="breadcrumb-item active">Voting</li>
                 </ol>
             </div>
         </div>
     </div>
 </div>
 <asp:HiddenField ID="txtMemberID" runat="server" />
 <asp:HiddenField ID="txtname" runat="server" />
 <asp:HiddenField ID="txtEventID" runat="server" />

    <asp:panel id="form1" runat="server">
    <asp:Label ID="resultLabel" runat="server" Text=""></asp:Label>
    <br />
    <asp:Button ID="btnSubmit" runat="server" Text="Submit" OnClick="btnSubmit_Click" />
</asp:panel>
</asp:Content>
