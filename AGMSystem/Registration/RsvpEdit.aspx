<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="RsvpEdit.aspx.cs" Inherits="AGMSystem.Registration.RsvpEdit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Attendee Edit</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Registration</a></li>
                                            <li class="breadcrumb-item active">Attendee Edit</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>


    <%-- ID --%>
    <div class="row">
        <asp:HiddenField ID="txtEventID" runat="server" />
        <asp:HiddenField ID="txtQueryID" runat="server" />
        <asp:HiddenField ID="txtMemberID" runat="server" />
   
        <div class="column">
            <%-- First Name --%>
            <div class="mb-3">
                <label for="First Name" class="form-label">First Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtFirstname" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
            </div>
         
        </div>
        
        <div class="column">
            <%-- last Name --%>
            <div class="mb-3">
                <label for="Last Name" class="form-label">Last Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtLastName" CssClass="form-control" placeholder="Surname" runat="server"></asp:TextBox>
            </div>
        </div>
</div>

    


    <div class="row">
        <div class="column">
            <%-- Phone Number --%>
            <div class="mb-3">
                <label for="PhoneNumber" class="form-label">Phone Number <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" placeholder="Phone Number" runat="server" TextMode="Phone"></asp:TextBox>
            </div>

           
        </div>

        <div class="column">
            <%-- Email --%>
            <div class="mb-3">
                <label for="Email" class="form-label">Email <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Email" runat="server" TextMode="Email"></asp:TextBox>
            </div>
        </div>

    </div>

    <div class="row">

        <div class="column">
    <%-- Company --%>
    <div class="mb-3">
        <label for="designation" class="form-label">Designation <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtDesignation" CssClass="form-control" placeholder="Designation" runat="server"></asp:TextBox>
    </div>
</div>
        <%-- tshirt --%>


        <div class="column">
            <%-- Company --%>
            <div class="mb-3">
                <label for="Company" class="form-label">Company <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtCompany" CssClass="form-control" placeholder="Company" runat="server"></asp:TextBox>
            </div>
        </div>

    </div>

    
       <div class="column">
          <div class="mb-3">
    <label for="tshirt size" class="form-label">Accomodation <span class="text-danger">*</span></label>
    <asp:TextBox ID="txtAccomodation" CssClass="form-control" placeholder="XXL" runat="server"></asp:TextBox>
</div>
       </div>
    
       <div class="column">
          <div class="mb-3">
    <label for="Transport" class="form-label">Transport <span class="text-danger">*</span></label>
    <asp:TextBox ID="txtTransport" CssClass="form-control" placeholder="XXL" runat="server"></asp:TextBox>
</div>
       </div>
   
    <div class="row">   
          <%-- Query --%>
 <%-- <div class="mb-3 column">
      <label for="Query" class="form-label">Query <span class="text-danger"></span></label>
      <asp:TextBox ID="txtQuery" CssClass="form-control" placeholder="Query" runat="server" TextMode="MultiLine"></asp:TextBox>
  </div>--%>
        <%-- Golf --%>
            <%--    <div class="col-2">
    <div class="mb-1 mt-5 margin-right-0">
        <label for="address" class="form-label">Are You Playing Golf? </label>
            </div>
</div>
<div class="col-1">
    <div class="mb-2 mt-5 m-1">
        <asp:CheckBox ID="chkGolf" runat="server" class="form-label"/>
            </div>
</div>--%>
        <%-- end Golf --%>
  
</div> 


                
        <div class="mt-4">

        <asp:Button ID="btnUpdate" Width="100%" OnClick="btnUpdate_Click" CssClass="btn btn-success" runat="server" Text="Update" Style="left: 0px; top: 0px" />

    </div>
    
   

</asp:Content>
