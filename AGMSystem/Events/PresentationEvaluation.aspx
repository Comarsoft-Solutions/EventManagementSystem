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
 <asp:HiddenField ID="txtEventID" runat="server" />                   
    <div class="row row-cols-2">
<div class="col-4 mb-3">
    <label for="fullname" class="form-label">Full Name</label>
    <asp:TextBox ID="txtFullName" placeholder="Malcolm Mach" Enabled="false" CssClass="form-control" runat="server"></asp:TextBox>
</div>
<div class="col-4 mb-3">
    <label for="Company" class="form-label">Company </label>
    <asp:TextBox ID="txtCompany" placeholder="CUG" CssClass="form-control" Enabled="false" runat="server"></asp:TextBox>
</div>
          </div> 
    <hr />
    <asp:Panel ID="pnlPresenters" Visible =" false" runat="server">
                                     <div class="mb-3">
    <label for=" Events" class="form-label">Select Presenter <span class="text-danger"></span></label>
    <asp:DropDownList ID="txtPresenter"  CssClass="form-control" runat="server" ></asp:DropDownList>
</div></asp:Panel>   
 <div class="mb-3">
     <%--<label for="Question1" class="form-label">1.	Purpose communicated clearly</label>--%>
     <span>
     <asp:Label ID="Question1" runat="server" Text="1.	Purpose communicated clearly"></asp:Label></span>
     <div class="row row-cols-2">
         <div class="col-4">
     <asp:DropDownList ID="cbo1" CssClass="form-control" runat="server" CausesValidation="true"></asp:DropDownList>
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
     <asp:DropDownList ID="cbo6" CssClass="form-control" runat="server" ></asp:DropDownList>
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
     <asp:DropDownList ID="cbo7" CssClass="form-control" runat="server" ></asp:DropDownList>
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
     <asp:DropDownList ID="cbo8" placeholder="Rating" CssClass="form-control" runat="server" ></asp:DropDownList>
             
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
     <asp:DropDownList ID="cbo9" CssClass="form-control" runat="server" ></asp:DropDownList>
             </div>
         <div class="col-8">
     <asp:TextBox ID="txt9" placeholder="Comment" CssClass="form-control" runat="server" TextMode="MultiLine"></asp:TextBox>
             </div>
         </div>
 </div>

 <div class="text-end">
     <asp:Button ID="btnSubmit"  CssClass="btn btn-primary" OnClick="btnSubmit_Click" runat="server" Text="Submit" />
 </div>

    <hr />
    <asp:Panel ID="pnlEvaluated" Visible="false" runat="server">
            <div class="card" >
                <div class="card-header align-items-center d-flex">
                    <h4 class="card-title mb-0 flex-grow-1">Evaluated Presenters </h4>
                    
                </div>
                
</div>
        <div class="form-group row gutters">

    <div class="form-group row gutters col-12">

        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdPresenters" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                DataKeyNames="ID" OnPageIndexChanging="grdPresenters_PageIndexChanging"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10">
                <Columns>
                    <asp:BoundField Visible="false" DataField="ID" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="Name" HeaderText="Full Name"></asp:BoundField>
                    <asp:BoundField DataField="Company" HeaderText="Company"></asp:BoundField>
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>
    </asp:Panel>
</asp:Content>
