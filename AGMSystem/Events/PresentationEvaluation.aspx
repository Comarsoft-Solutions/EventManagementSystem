<%@ Page Title="" Language="C#" MasterPageFile="~/OnlinePages.Master" AutoEventWireup="true" CodeBehind="PresentationEvaluation.aspx.cs" Inherits="AGMSystem.Events.PresentationEvaluation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
     <div class="col-12">
         <div class="page-title-box d-sm-flex align-items-center justify-content-between">
             <h4 class="mb-sm-0">Presentation Evaluation</h4>
             <div class="page-title-right">
                 <ol class="breadcrumb m-0">
                     <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                     <li class="breadcrumb-item active">Evaluation</li>
                 </ol>
             </div>
         </div>
     </div>
 </div>
 <asp:HiddenField ID="txtMemberID" runat="server" />
 <asp:HiddenField ID="txtname" runat="server" />
                                     <div class="mb-3">
    <label for=" Events" class="form-label">Select Event <span class="text-danger"></span></label>
    <asp:DropDownList ID="txtEvents"  CssClass="form-control" runat="server" ></asp:DropDownList>
</div>
    <asp:Panel ID="Panel1" Visible =" false" runat="server">
                                     <div class="mb-3">
    <label for=" Events" class="form-label">Select Presenter <span class="text-danger"></span></label>
    <asp:DropDownList ID="txtPresenter" OnTextChanged="txtPresenter_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
</div></asp:Panel>
      <%-- member search  --%>
    <div class="row row-cols-2">

      <div class="col-6">
      <div class="mb-3">
          <label for="Full Name" class="form-label">Full Name <span class="text-danger">*</span></label>
          <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="Full Name" runat="server" ></asp:TextBox>
      </div>
  </div>

         
        <div class=" col-4 mb-3 mt-4">

        <asp:Button ID="btnSearch" Width="50%" OnClick="btnSearch_Click" CssClass="btn btn-success" runat="server" Text="Search" Style="left: 0px; top: 0px" />

    </div>
        </div>
 <div class="mb-3">
     <%--<label for="Question1" class="form-label">1.	Purpose communicated clearly</label>--%>
     <span>
     <asp:Label ID="Question1" runat="server" Text="1.	Purpose communicated clearly"></asp:Label></span>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo1" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator
                ID="rfvOptions"
                runat="server"
                ControlToValidate="cbo1"
                InitialValue=""
                ErrorMessage="Please select an option."
                Display="Dynamic"
                ForeColor="Red"
                EnableClientScript="true"
                ValidationGroup="YourValidationGroup"
            />
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt1" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question2" class="form-label">2.	Organised and easy to follow</label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo2" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv2" runat="server"
                ControlToValidate="cbo2"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt2" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question3" class="form-label">3.	Presenter exhibited a good understanding of topic</label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo3"  CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv3" runat="server"
                ControlToValidate="cbo3"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt3" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question4" class="form-label">4.	Presenter was well-prepared</label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo4"  CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv4" runat="server"
                ControlToValidate="cbo4"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt4" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question5" class="form-label">5.	Time for presentation used effectively </label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo5"  CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv5" runat="server"
                ControlToValidate="cbo5"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt5" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question6" class="form-label">6.	Presenter spoke clearly / effectively</label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo6" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv6" runat="server"
                ControlToValidate="cbo6"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt6" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question7" class="form-label">7.	Slides enhanced presentation </label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo7" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv7" runat="server"
                ControlToValidate="cbo7"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt7" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question8" class="form-label">8.	Presenters responded effectively to audience questions and comments</label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo8" placeholder="Rating" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv8" runat="server"
                ControlToValidate="cbo8"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt8" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>
 <div class="mb-3">
     <label for="Question9" class="form-label">9.	Presentation  was done in a way that engaged audience</label>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo9" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
             <asp:RequiredFieldValidator ID="rv9" runat="server"
                ControlToValidate="cbo9"
                ErrorMessage="Select at least one rating"
                InitialValue=""
                Display="Dynamic">
            </asp:RequiredFieldValidator>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt9" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>

 <div class="text-end">
     <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
 </div>
</asp:Content>
