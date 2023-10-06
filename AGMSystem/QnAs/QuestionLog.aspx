<%@ Page Title="" Language="C#" MasterPageFile="~/OnlinePages.Master" AutoEventWireup="true" CodeBehind="QuestionLog.aspx.cs" Inherits="AGMSystem.QnAs.QuestionLog" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
                         <div class="col-12">
                             <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                 <h4 class="mb-sm-0">Your Questions</h4>
                                     <div class="page-title-right">
                                     <ol class="breadcrumb m-0">
                                         <li class="breadcrumb-item"><a href="javascript: void(0);">ZAPF</a></li>
                                         <li class="breadcrumb-item active">Question and Answer</li>
                                     </ol>
                                 </div>
                             </div>   
                         </div>
                     </div>
 <table style="width:100%">
     <tr>
             <td colspan="2">Enter your name:</td>
         <td colspan="10">
             <asp:TextBox ID="txtQName" CssClass="form-control" runat="server"></asp:TextBox>
         </td>
     </tr>
     <tr>
         <td colspan="2">Your Company:</td>
         <td colspan="10">
             <asp:TextBox ID="txtYourCompany" CssClass="form-control" runat="server"></asp:TextBox>
         </td>
     </tr>
     <tr>
         <td colspan="12" style="text-align:center">
             <asp:Button ID="btnSaveName" OnClick="btnSaveName_Click" CssClass="btn btn-primary" runat="server" Text="Save/Search Name" />
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
             Your Name:
         </td>
         <td colspan="10">
             <asp:Label ID="lblYouName" runat="server" Text=""></asp:Label>
         </td>
     </tr>
     <tr>
         <td colspan="2">Your Question:</td>
         <td colspan="10">
             <asp:TextBox ID="txtYourQuestion" CssClass="form-control" TextMode="MultiLine" Height="100px" runat="server"></asp:TextBox>
         </td>
     </tr>
     <tr>
         <td colspan="12" style="text-align:center">
             <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click" CssClass="btn btn-primary" runat="server" Text="Submit Question" />
         </td>
     </tr>
     <tr>
         <td colspan="12"><hr /></td>
     </tr>
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
                                     <asp:BoundField DataField="Question" HeaderText="Question"></asp:BoundField>
                                     <asp:BoundField DataField="ResponseStatus" HeaderText="Status"></asp:BoundField>
                                     <asp:BoundField DataField="DateCreated" HeaderText="Submission Time"></asp:BoundField>
                                     
                     <asp:TemplateField HeaderText="View Response">
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
         <td colspan="12"></td>
     </tr>
     <tr>
         <td colspan="2">Your Question:</td>
         <td colspan="10">
             <asp:Label ID="lblQues" runat="server" Text=""></asp:Label>
         </td>
     </tr>
     <tr>
         <td colspan="2">Your Response:</td>
         <td colspan="10">
             <asp:Label ID="lblResponse" runat="server" Text=""></asp:Label>
         </td>
     </tr>
 </table>

</asp:Content>
