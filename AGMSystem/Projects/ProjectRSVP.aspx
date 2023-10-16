<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="ProjectRSVP.aspx.cs" Inherits="AGMSystem.Projects.ProjectRSVP" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
            <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Project Registration</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Projects</a></li>
                                            <li class="breadcrumb-item active">Registration</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>

    <div class="mb-3">
            <label for=" Resolution" class="form-label">Select Project <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtProjects" OnTextChanged="txtProjects_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>

    <%-- ID --%>
    <div class="row">
        <asp:HiddenField ID="txtProjectID" runat="server" />
        <asp:HiddenField ID="txtQueryID" runat="server" />
        <asp:HiddenField ID="txtVenue" runat="server" />
        <asp:HiddenField ID="txtExamDate" runat="server" />
        <asp:HiddenField ID="txtmemberID" runat="server" />
        <div class="column">
    <div class="mb-3">

        <label for=" ID Number" class="form-label"> ID Number <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtNatID" CssClass="form-control" placeholder="63292921287T75" runat="server" AutoPostBack="true" OnTextChanged="txtNatID_TextChanged" ></asp:TextBox>
    </div>
</div>
            <%--    <div class="column">
    <div class="mb-3">

        <label for="new ID Number" class="form-label">New ID Number <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtNewID" CssClass="form-control" placeholder="633333333X63" runat="server"  ></asp:TextBox>
    </div>
</div>--%>
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

            <%-- Category --%>
            <div class="mb-3">
                <label for="Education" class="form-label">Highest Education Qalification <span class="text-danger">*</span></label>
                <asp:DropDownList runat="server" ID="txtEducation" CssClass="form-control dropdown">
                    
                </asp:DropDownList>
            </div>

        </div>
        <%-- tshirt --%>
       <div class="column">
          <div class="mb-3">
    <label for="tshirt size" class="form-label">Pension Fund  <span class="text-danger">*</span></label>
    <asp:TextBox ID="txtPensionFund" CssClass="form-control" placeholder="ZAPF" runat="server"></asp:TextBox>
</div>
       </div>


        <div class="column">
            <%-- Company --%>
            <div class="mb-3">
                <label for="Company" class="form-label">Company <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtCompany" CssClass="form-control" placeholder="Company" runat="server"></asp:TextBox>
            </div>
        </div>
        <%-- file --%>
        <div class="col-6">
                                    <label>Attachment POP</label>
                                    <asp:FileUpload ID="flRsvpUpload" CssClass="form-control fa-upload" runat="server" />

                                </div>

    </div>


   
    <div class="row">   
          <%-- Query --%>
  <div class="mb-3 column">
      <label for="Query" class="form-label">Query <span class="text-danger"></span></label>
      <asp:TextBox ID="txtQuery" CssClass="form-control" placeholder="Query" runat="server" TextMode="MultiLine"></asp:TextBox>
  </div>
        <%-- Membership --%>
                <div class="col-2">
    <div class="mb-1 mt-5 margin-right-0">
        <label for="Member" class="form-label">Are You A Member? </label>
            </div>
</div>
<div class="col-1">
    <div class="mb-2 mt-5 m-1">
        <asp:CheckBox ID="chkMmber" runat="server" class="form-label"/>
            </div>
</div>
        <%-- end Membership --%>
  
        <%-- Membership --%>
                <div class="col-2">
    <div class="mb-1 mt-5 margin-right-0">
        <label for="Member" class="form-label">Do You Want Online Study? </label>
            </div>
</div>
<div class="col-1">
    <div class="mb-2 mt-5 m-1">
        <asp:CheckBox ID="chkPrivate" runat="server" class="form-label"/>
            </div>
</div>
        <%-- end Membership --%>
  
</div> 

                
        <div class="mt-4">

        <asp:Button ID="btnRegister" Width="100%" OnClick="btnRegister_Click" CssClass="btn btn-success" runat="server" Text="RSVP" Style="left: 0px; top: 0px" />

    </div>
    
    <div class="col-12 mb-3">
        
            <asp:Panel ID="pnlConfirmed" Visible="false" runat="server">
        <div class="card" id="crdConfirmed">
<div class="card-header align-items-center d-flex">
    <h4 class="card-title mb-0 flex-grow-1">Confirmed Attendees</h4>
    
</div>
</div>
                </asp:Panel>
                    <div class="form-group row gutters">

                        <div class="form-group row gutters col-12">

                            <div class="col-sm-12 align-content-center">
                                <asp:GridView ID="GrdRegisteredMembers" Width="100%" runat="server"
                                    AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                                    DataKeyNames="ID" 
                                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                    Style="border-collapse: collapse !important"
                                    AllowPaging="True" AllowSorting="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name "></asp:BoundField>
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name"></asp:BoundField>
                                        <asp:BoundField DataField="PensionFund" HeaderText="Fund"></asp:BoundField>
                                        <asp:BoundField DataField="PhoneNumber" HeaderText="Phone Number"></asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
</asp:Content>
