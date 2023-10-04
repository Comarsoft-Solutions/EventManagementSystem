<%@ Page Title="" Language="C#" MasterPageFile="~/OnlinePages.Master" AutoEventWireup="true" CodeBehind="TransportAndAccomodation.aspx.cs" Inherits="AGMSystem.Online.TransportAndAccomodation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

            <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Transport And Accomodation Confirmations</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Confirmations</a></li>
                                            <li class="breadcrumb-item active">Transport And Accomodation</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>

   <%-- <div class="mb-3">
            <label for=" Resolution" class="form-label">Select Event <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtEvents" OnTextChanged="txtEvents_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>--%>

    <%-- ID --%>
    <div class="row">
        <asp:HiddenField ID="txtEventID" runat="server" />
        <asp:HiddenField ID="txtQueryID" runat="server" />
        <asp:HiddenField ID="txtMemberID" runat="server" />
        <div class="column">
    <div class="mb-3">

        <label for="old ID Number" class="form-label">Full Name <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtFullName" CssClass="form-control" placeholder="Eng Malcolm Munyaradzi Machi" runat="server" AutoPostBack="true" OnTextChanged="txtFullName_TextChanged" ></asp:TextBox>
    </div>
</div>
        <div class="column">
            <%-- First Name --%>
            <div class="mb-3">
                <label for="First Name" class="form-label">First Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtFirstname" ReadOnly="true" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
            </div>
         
        </div>
        
        <div class="column">
            <%-- last Name --%>
            <div class="mb-3">
                <label for="Last Name" class="form-label">Last Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtLastName" ReadOnly="true" CssClass="form-control" placeholder="Surname" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="column">
    <%-- Company --%>
    <div class="mb-3">
        <label for="Company" class="form-label">Company <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtCompany" ReadOnly="true" CssClass="form-control" placeholder="Company" runat="server"></asp:TextBox>
    </div>
</div>
</div>

    


    <div class="row">
        <div class="column">
            <%-- Phone Number --%>
            <div class="mb-3">
                <label for="PhoneNumber" class="form-label">Phone Number <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtPhoneNumber" ReadOnly="true" CssClass="form-control" placeholder="Phone Number" runat="server" TextMode="Phone"></asp:TextBox>
            </div>

           
        </div>

        <div class="column">
            <%-- Email --%>
            <div class="mb-3">
                <label for="Email" class="form-label">Email <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtEmail" ReadOnly="true" CssClass="form-control" placeholder="Email" runat="server" TextMode="Email"></asp:TextBox>
            </div>
        </div>

    </div>

    <div class="row">

        <div class="column">

            <%-- Category --%>
            <div class="mb-3">
                <label for="Category" class="form-label">Accomodation <span class="text-danger">*</span></label>
                <asp:TextBox runat="server" ReadOnly="true" ID="txtAccomodation" CssClass="form-control dropdown">
                    
                </asp:TextBox>
            </div>

        </div>
        <%-- tshirt --%>
       <div class="column">
          <div class="mb-3">
    <label for="tshirt size" class="form-label">Transport <span class="text-danger">*</span></label>
    <asp:TextBox ID="txtTransport" ReadOnly="true" CssClass="form-control" placeholder="XXL" runat="server"></asp:TextBox>
</div>
       </div>


      

    </div>


   
    

       
    

                
        <div class="mt-4 pull-right" >

        <asp:Button ID="btnDone" Width="40%" OnClick="btnDone_Click" CssClass="btn btn-success" runat="server" Text="Confirm" Style="left: 0px; top: 0px" />

    </div>
    
   


</asp:Content>
