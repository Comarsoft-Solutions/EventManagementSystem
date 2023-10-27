<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="BulkUploads.aspx.cs" Inherits="AGMSystem.Membership.BulkUploads" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <h4 class="mb-sm-0">Member Creation</h4>
             <div class="page-title-right">
                 <ol class="breadcrumb m-0">
                     <li class="breadcrumb-item"><a href="javascript: void(0);">Membership</a></li>
                     <li class="breadcrumb-item active">Membership Upload </li>
                 </ol>
             </div>
         </div>
     </div>
 </div>
 <%-- end breadcrumb --%>
    <!-- START PAGE CONTAINER -->
  <div class="container container-boxed">
      <!-- RECENT ACTIVITY -->
      <div class="block block-condensed">
          </div>
          <div class="row">

              <div class="col-md-6">
                  <asp:HiddenField ID="txtID" runat="server" />
                  <asp:HiddenField ID="txtSystemRef" runat="server" />
                  <asp:HiddenField ID="txtFundID" runat="server" />
                  <asp:HiddenField ID="txtViewType" runat="server" />
                  <asp:HiddenField ID="txtbatchID" runat="server" />
                  <asp:HiddenField ID="txtRegNo" runat="server" />
                  <asp:HiddenField ID="txtFilenames" runat="server" />
                  <asp:HiddenField ID="contentType" runat="server" />
                  <asp:HiddenField ID="HiddenField1" runat="server" />
              </div>
          </div>
          <div class="block-content">
              <form class="form-horizontal">
                  <div class="row">
                      <table style="width: 100%">
                          <tr>
                              <td colspan="12">
                                  <asp:Panel ID="pnlComms" Width="100%" runat="server">
                                      <asp:Label ID="lblComms" runat="server" Text="" Font-Bold="True" ForeColor="White"></asp:Label>
                                  </asp:Panel>
                              </td>
                          </tr>
                          <tr>
                              <td colspan="12">
                                  <asp:HiddenField ID="txtMemberID" runat="server" />
                              </td>
                          </tr>
                          <tr>
                              <td colspan="1">
                                  <asp:Label ID="lblCurrentMembers" runat="server" Font-Names="Courier New" Text=""></asp:Label>
                              </td>
                              <td colspan="2"></td>
                              <td colspan="1">
                                  <asp:Label ID="lblFailedUploads" runat="server" Font-Names="Courier New" Text=""></asp:Label>
                              </td>
                              <td colspan="2"></td>
                          </tr>
                          <tr>
                              <td colspan="2">File:</td>
                              <td colspan="4">
                                  <asp:FileUpload ID="flContributionsUpload" runat="server" accept=".xls,.xlsx,.csv" />
                              </td>
                              <td colspan="3">
                                  <asp:Button ID="btnUpload" OnClick="btnUpload_Click" CssClass="btn btn-primary" runat="server" Text="Upload" />
                              </td>
                              <td colspan="3">
                                  <asp:Button ID="btnDownload" OnClick="btnDownload_Click" CssClass="btn btn-primary" runat="server" Text="Download Template" />
                              </td>
                          </tr>
                          <tr>
                              <td colspan="12" style="text-align: center">

                                  <%--<asp:Button ID="btnDownload" OnClick="btnDownload_Click" CssClass="btn btn-primary" runat="server" Text="Download Template" />--%>
                                  
                                              <%-- <asp:Button ID="btnUpload" OnClick="btnUpload_Click" CssClass="btn btn-primary" runat="server" Text="Upload" />--%>


                          </tr>
                          <tr>

                              <td colspan="2">Select Sheet:</td>
                              <td colspan="4">
                                  <asp:ListBox ID="lstWrkSheets" OnSelectedIndexChanged="lstWrkSheets_SelectedIndexChanged" CssClass="form-control list-group" AutoPostBack="true" runat="server"></asp:ListBox>
                              </td>
                          </tr>
                          <tr>
                              <td colspan="2">
                                  <asp:Label ID="lblWrkSheetPrompt" runat="server" Text=""></asp:Label>
                                  <asp:TextBox ID="txtFilePath" Visible="false" runat="server"></asp:TextBox>
                                  <asp:TextBox ID="txtFileName" Visible="false" runat="server"></asp:TextBox>
                              </td>

                          </tr>
                          <tr>
                              <td colspan="12">
                                  <hr />
                              </td>
                          </tr>

                          <tr>
                              <td colspan="12" style="text-align: center">
                                  <%--<asp:Button ID="btnProcess" CssClass="btn btn-primary btn-clean" OnClick="btnProcess_Click" OnClientClick="return confirm('Are you sure want you want to upload these contributions?');" runat="server" Text="Process Upload" />--%>
                                  <asp:Button ID="btnProcess" CssClass="btn btn-primary btn-clean" OnClick="btnProcess_Click"  OnClientClick="return confirm('Are you sure want you want to upload these Members?');" runat="server" Text="Process Upload" />
                                  
                                                                              | 
                                  <%--<asp:Button ID="btnDiscard" CssClass="btn btn-primary btn-clean" OnClick="btnDiscard_Click" runat="server" Text="Discard Upload" />--%>
                                  <asp:Button ID="btnDiscard" CssClass="btn btn-primary btn-clean" OnClick="btnDiscard_Click"  runat="server" Text="Discard Upload" />
                              </td>
                          </tr>
                          <tr>
                              <td colspan="12" style="width: 100%;">
                                  <table style="max-width: 100vw">

                                      <tr>

                                          <td colspan="12" style="width: 100%;">
                                              
                              <asp:Panel ID="pnlClientsView" Visible="false" runat="server">
                                              <table style="max-width: 100vw">
                                                  <tr>
                                                      <td colspan="12">Pending Members Upload</td>
                                                  </tr>
                                                  <tr>
                                                      <td>
                                                          <div style="display: block; overflow: scroll; height: 40vh; display: grid;">
                                                         
                                                              <asp:GridView ID="grdClientsView"  PageSize="10" AutoGenerateColumns="false" DataKeyNames="ID" CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                                                  Style="border-collapse: collapse !important" OnPageIndexChanging="grdClientsView_PageIndexChanging"
                                                                  AllowPaging="True" AllowSorting="True" runat="server">
                                                                  <columns>
                                                                      <asp:BoundField DataField="ID" HeaderText="UploadID" Visible="false"></asp:BoundField>
                                                                      <asp:BoundField DataField="Company" HeaderText="Company"></asp:BoundField>
                                                                      <asp:BoundField DataField="Surname" HeaderText="Surname"></asp:BoundField>
                                                                      <asp:BoundField DataField="Firstname" HeaderText="Forename(s)"></asp:BoundField>
                                                                      <asp:BoundField DataField="NationalID" HeaderText="Identity Number"></asp:BoundField>
                                                                      <asp:BoundField DataField="PensionFund" HeaderText="Pension Scheme"></asp:BoundField>
                                                                      <asp:BoundField DataField="Email" HeaderText="Email Address"></asp:BoundField>
                                                                  </columns>
                                                              </asp:GridView>
                                                          </div>
                                                      </td>
                                                  </tr>
                                              </table>


                                  
                              </asp:Panel>

                                          </td>

                                      </tr>
                                      <tr>
                                          <asp:Panel ID="pnlerror" Visible="false" runat="server">
                                          <td colspan="12" style="width: 100%;">
                                              <table style="max-width: 100vw">
                                                  <tr>
                                                      <td colspan="12">Failed Members Upload</td>
                                                  </tr>
                                                  <tr>
                                                      <td>
                                                          <div style="display: block; overflow: scroll; height: 40vh; display: grid;">
                                                              <%--<asp:GridView ID="grdUploadError" OnPageIndexChanging="OnPageIndexChanging" PageSize="10" AutoGenerateColumns="false" CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"--%>
                                                              <asp:GridView ID="grdUploadError"  PageSize="10" AutoGenerateColumns="false" CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                                                  Style="border-collapse: collapse !important" OnPageIndexChanging="grdUploadError_PageIndexChanging"
                                                                  AllowPaging="True" AllowSorting="True" runat="server">
                                                                  <columns>
                                                                      <asp:BoundField DataField="ID" HeaderText="ID" />
                                                                      <asp:BoundField DataField="NationalID" HeaderText="National ID" />
                                                                      <asp:BoundField DataField="Description" HeaderText="Description" />
                                                                      <asp:BoundField DataField="DateCreated" HeaderText="Date Of Upload" />
                                                                  </columns>
                                                              </asp:GridView>
                                                          </div>
                                                      </td>
                                                  </tr>
                                              </table>




                                          </td>
                                          </asp:Panel>
                                      </tr>
                                  </table>
                              </td>
                          </tr>
                      </table>
                  </div>
              </form>
              <div class="row">
                  <div class="col-md-12 text-right">
                  </div>
              </div>

          </div>
      </div>
      <!-- END RECENT -->
  <!-- END PAGE CONTAINER -->
</asp:Content>
