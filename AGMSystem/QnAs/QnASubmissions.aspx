<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="QnASubmissions.aspx.cs" Inherits="AGMSystem.QnAs.QnASubmissions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <meta http-equiv="refresh" content="120">
    <div class="row">
                         <div class="col-12">
                             <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                 <h4 class="mb-sm-0">Q&A Submissions and Responses</h4>
                                     <div class="page-title-right">
                                     <ol class="breadcrumb m-0">
                                         <li class="breadcrumb-item"><a href="javascript: void(0);">ZAPF</a></li>
                                         <li class="breadcrumb-item active">Responses</li>
                                     </ol>
                                 </div>
                             </div>   
                         </div>
                     </div>
 <table style="width:100%">
     
     <tr>
         <td colspan="12">
             <asp:GridView ID="grdCheckin" Width="100%" runat="server"
                 AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdCheckin_PageIndexChanging"
                 DataKeyNames="ID"
                 CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                 Style="border-collapse: collapse !important"
                 AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdCheckin_RowCommand">
                 <Columns>
                     <%--<asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>--%>
                                     <asp:BoundField DataField="QName" HeaderText="Participant"></asp:BoundField>
                                     <asp:BoundField DataField="Company" HeaderText="Company"></asp:BoundField>
                                     <asp:BoundField DataField="Question" HeaderText="Question"></asp:BoundField>
                                     
                     <asp:TemplateField HeaderText="Respond">
                         <ItemTemplate>
                             <asp:LinkButton ID="Edit" runat="server" ForeColor="blue" CssClass="mdi mdi-check-bold" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                            <%-- <asp:LinkButton ID="View" runat="server" ForeColor="green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                             <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="selectRecord"></asp:LinkButton>
                             <asp:LinkButton ID="Attach" runat="server" ForeColor="Black" CssClass="mdi mdi-attachment" CommandArgument='<%#Eval("ID")%>' CommandName="attachRecord"></asp:LinkButton>--%>
                         </ItemTemplate>
                     </asp:TemplateField>
                 </Columns>
             </asp:GridView>
         </td>
     </tr>
     <tr>
         <td colspan="12"><hr /></td>
     </tr>
     <tr>
         <td colspan="12">
             <asp:HiddenField ID="txtRecID" runat="server" />
             <asp:HiddenField ID="txtQID" runat="server" />
         </td>
     </tr>
     <tr>
         <td colspan="2">
             Participant Name & Company:
         </td>
         <td colspan="10">
             <asp:Label ID="lblYouName" runat="server" Text=""></asp:Label>
         </td>
     </tr>
     <tr>
         <td colspan="2">Question:</td>
         <td colspan="10">
             <asp:Label ID="lblQuestion" runat="server" Text=""></asp:Label>
         </td>
     </tr>
     <tr>
         <td colspan="2">Question Submission Time</td>
         <td colspan="10">
             <asp:Label ID="LBLSubmissionTime" runat="server" Text=""></asp:Label>
         </td>
     </tr>
     <tr>
         <td colspan="12" style="text-align:center">
          <hr />
         </td>
     </tr>
     <tr>
         <td colspan="2">Response:</td>
         <td colspan="10">
             <asp:TextBox ID="txtResponse" CssClass="form-control" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
         </td>
     </tr>
     
     <tr>
         <td colspan="12"><hr /></td>
     </tr>
     <tr>
         <td colspan="12">
             <asp:Button ID="btnRespond" OnClick="btnRespond_Click" CssClass="btn btn-primary" runat="server" Text="Respond" />
         </td>
     </tr>
     
 </table>
</asp:Content>
