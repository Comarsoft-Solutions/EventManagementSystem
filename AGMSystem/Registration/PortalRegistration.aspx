<%@ Page Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="PortalRegistration.aspx.cs" Inherits="AGMSystem.Registration1" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Registration</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Registration</a></li>
                                            <li class="breadcrumb-item active">Attendee Registration</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>

    <div class="mb-3">
            <label for=" Resolution" class="form-label">Select Event <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtEvents" OnTextChanged="txtEvents_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>

    <%-- ID --%>
    <div class="row">
        <asp:HiddenField ID="txtEventID" runat="server" />
        <asp:HiddenField ID="txtQueryID" runat="server" />
        <asp:HiddenField ID="txtMemberID" runat="server" />
        <div class="column">
    <div class="mb-3">

        <label for="ID Number" class="form-label">ID Number <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtNatID" CssClass="form-control" placeholder="ID-Number" runat="server" AutoPostBack="true" OnTextChanged="txtNatID_TextChanged" on></asp:TextBox>
    </div>
</div>
        <div class="column">
            <%-- First Name --%>
            <div class="mb-3">
                <label for="First Name" class="form-label">First Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtFirstname" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
            </div>
         
        </div>
</div>

    <div class="row">
        <div class="column">
            <%-- last Name --%>
            <div class="mb-3">
                <label for="Last Name" class="form-label">Last Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtLastName" CssClass="form-control" placeholder="Surname" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="column">
           <div class="mb-3">
     <label for="tshirt size" class="form-label">Tshirt Size <span class="text-danger">*</span></label>
     <asp:TextBox ID="txtTshirtSize" CssClass="form-control" placeholder="XXL" runat="server"></asp:TextBox>
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
                <label for="Category" class="form-label">Designation <span class="text-danger">*</span></label>
                <asp:DropDownList runat="server" ID="txtCategory" CssClass="form-control dropdown">
                   
                </asp:DropDownList>
            </div>

        </div>

        <div class="column">
            <%-- Company --%>
            <div class="mb-3">
                <label for="Company" class="form-label">Company <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtCompany" CssClass="form-control" placeholder="Company" runat="server"></asp:TextBox>
            </div>
        </div>
        <div class="column">
            <%-- Company --%>
            <div class="mb-3">
                <label for="memType" class="form-label">Membership Type <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtMembershipType" CssClass="form-control" placeholder="Company" runat="server"></asp:TextBox>
            </div>

        </div>
        <div class="col-6">
                                    <label>Attachment POP</label>
                                    <asp:FileUpload ID="flRsvpUpload" CssClass="form-control fa-upload" runat="server" />

                                </div>

    </div>


    <%-- Query --%>
    <div class="mb-3">
        <label for="Query" class="form-label">Query <span class="text-danger"></span></label>
        <asp:TextBox ID="txtQuery" CssClass="form-control" placeholder="Query" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>

       
    <div class="col-12 mb-3">
                    <div class="form-group row gutters">

                        <div class="form-group row gutters col-12">

                            <div class="col-sm-12 align-content-center">
                                <asp:GridView ID="gridAccomodation" Width="100%" runat="server"
                                    AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                                    DataKeyNames="ID" OnRowCommand="gridAccomodation_RowCommand"
                                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                    Style="border-collapse: collapse !important"
                                    AllowPaging="True" AllowSorting="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="Combo" HeaderText="Logistics "></asp:BoundField>
                                        <asp:BoundField DataField="Transport" HeaderText="Transport"></asp:BoundField>
                                        <asp:BoundField DataField="AvailableTransport" HeaderText="Available Transport"></asp:BoundField>
                                        <asp:BoundField DataField="Accomodation" HeaderText="Accomodation"></asp:BoundField>
                                        <asp:BoundField DataField="AvailableAccomodation" HeaderText="Available Accomodation"></asp:BoundField>
                                        <asp:BoundField DataField="ComboCapacity" HeaderText="Combo Capacity"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Select">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Delete" runat="server" ForeColor="Blue" CssClass="mdi mdi-check-bold" CommandArgument='<%# Eval("ID") + "|" + Eval("acID") + "|" + Eval("agmtID") +"|"+ Eval("ComboCapacity") %>' CommandName="selectRecord"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
    <asp:HiddenField ID="txtLogisticsCombos" runat="server" />

                
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
                                    DataKeyNames="ID" OnRowCommand="gridAccomodation_RowCommand"
                                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                    Style="border-collapse: collapse !important"
                                    AllowPaging="True" AllowSorting="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="FirstName" HeaderText="First Name "></asp:BoundField>
                                        <asp:BoundField DataField="LastName" HeaderText="Last Name"></asp:BoundField>
                                        <asp:BoundField DataField="TshirtSize" HeaderText="T-shirt Size"></asp:BoundField>
                                        <asp:BoundField DataField="Golf" HeaderText="Golf "></asp:BoundField>
                                        
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
   

</asp:Content>
