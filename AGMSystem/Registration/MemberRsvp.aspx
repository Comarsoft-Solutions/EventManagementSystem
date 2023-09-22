<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="MemberRsvp.aspx.cs" Inherits="AGMSystem.MemberRsvp" %>
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
    <div class="text-center mt-5 mb-4">
        <h5>
            <asp:Label ID="lblUsername" runat="server" Text=""></asp:Label>
        </h5>

    </div>
    <%-- ID --%>
    <div class="row">
        <div class="column">
    <div class="mb-3">
        <asp:HiddenField ID="txtEventID" runat="server" />
        <asp:HiddenField ID="txtQueryID" runat="server" />
        <asp:HiddenField ID="txtMemberID" runat="server" />

        <label for="ID Number" class="form-label">ID Number <span class="text-danger">*</span></label>
        <asp:TextBox ID="txtNatID" CssClass="form-control" placeholder="ID-Number" runat="server" AutoPostBack="true" ></asp:TextBox>
    </div>
</div>
        <div class="column">
            <%-- First Name --%>
            <div class="mb-3">
                <label for="First Name" class="form-label">First Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtFirstname" CssClass="form-control" placeholder="Name" runat="server"></asp:TextBox>
            </div>
            <%--<div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="categorynameValidator" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtFirstname"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>--%>
        </div>
</div>
    <%-- <div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator6" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtNatID"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>--%>

    <div class="row">
        <div class="column">
            <%-- last Name --%>
            <div class="mb-3">
                <label for="Last Name" class="form-label">Last Name <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtLastName" CssClass="form-control" placeholder="Surname" runat="server"></asp:TextBox>
            </div>
            <%--<div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="categorynameValidator" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtFirstname"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>--%>
        </div>
        <div class="column">
            <%-- Job Title --%>
            <div class="mb-3">
                <label for="Job Title" class="form-label">Job Title <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtJobTitle" CssClass="form-control" placeholder="Designation" runat="server"></asp:TextBox>
            </div>

            <%--<div class="input-group text-center">--%>
            <%-- <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtLastName"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>--%>
            <%--</div>--%>
        </div>

    </div>



    <div class="row">
        <div class="column">
            <%-- Phone Number --%>
            <div class="mb-3">
                <label for="PhoneNumber" class="form-label">Phone Number <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtPhoneNumber" CssClass="form-control" placeholder="Phone Number" runat="server" TextMode="Phone"></asp:TextBox>
            </div>

            <%-- <div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator3" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtPhoneNumber"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>--%>
        </div>

        <div class="column">
            <%-- Email --%>
            <div class="mb-3">
                <label for="Email" class="form-label">Email <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtEmail" CssClass="form-control" placeholder="Email" runat="server" TextMode="Email"></asp:TextBox>
            </div>

            <%--<div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator4" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtEmail"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>--%>
        </div>

    </div>

    <div class="row">

        <div class="column">

            <%-- Category --%>
            <div class="mb-3">
                <label for="Category" class="form-label">Category <span class="text-danger">*</span></label>
                <asp:DropDownList runat="server" ID="txtCategory" CssClass="form-control dropdown">
                    <asp:ListItem Text="Member" />
                    <asp:ListItem Text="Pensioner" />
                    <asp:ListItem Text="Actuaries" />
                    <asp:ListItem Text="Trustees" />
                    <asp:ListItem Text="Asset Manager" />
                    <asp:ListItem Text="Fund Administrator" />
                    <asp:ListItem Text="Risk Analyser" />
                    <asp:ListItem Text="Other" />
                </asp:DropDownList>
            </div>

        </div>

        <div class="column">
            <%-- Company --%>
            <div class="mb-3">
                <label for="Company" class="form-label">Company <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtCompany" CssClass="form-control" placeholder="Company" runat="server"></asp:TextBox>
            </div>

            <%--  <div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtCompany"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>--%>
        </div>

    </div>


    <%-- Query --%>
    <div class="mb-3">
        <label for="Query" class="form-label">Query <span class="text-danger"></span></label>
        <asp:TextBox ID="txtQuery" CssClass="form-control" placeholder="Query" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>
    
             
     <div class="row">
        <!-- Hoverable Rows -->
        <div class="col-xs-6">
<table class="table table-hover table-nowrap mb-0">
    <thead>
        <tr>
            <th scope="col">
            </th>
            <th scope="col">Service Provider</th>
            <th scope="col">Quantity</th>
            <th scope="col">Logistic Type</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th scope="row">
                <div class="form-check">
                    <div>
                <asp:RadioButton ID="RadioButton1" GroupName="Transport" runat="server"  />

            </div>
                </div>
            </th>
            <td>Air Zim  1</td>
            <td>200</td>
            <td>Air Transport</td>
        </tr>
        <tr>
            <th scope="row">
                <div class="form-check">
                    <div>
                <asp:RadioButton ID="RadioOpen" GroupName="Transport" runat="server"  />

            </div>
                </div>
            </th>
            <td>Fast Jet</td>
            <td>50</td>
            <td>Air Transport</td>
        </tr>
    </tbody>
</table></div>
        <div  class="col-xs-6">
        <table class="table table-hover table-nowrap mb-0">
    <thead>
        <tr>
            <th scope="col">
            </th>
            <th scope="col">Service Provider</th>
            <th scope="col">Quantity</th>
            <th scope="col">Logistic Type</th>
        </tr>
    </thead>
    <tbody>
        <tr>
            <th scope="row">
                <div class="form-check">
                    <div>
                <asp:RadioButton ID="RadioButton2" GroupName="accomodation" runat="server"  />

            </div>
                </div>
            </th>
            <td>Holiday Inn</td>
            <td>70</td>
            <td>Accomodation</td>
        </tr>
        <tr>
            <th scope="row">
                <div class="form-check">
                    <div>
                <asp:RadioButton ID="RadioButton3" GroupName="accomodation" runat="server"  />

            </div>
                </div>
            </th>
            <td>Own Accomodation</td>
            <td>n/a</td>
            <td>Accomodation</td>
        </tr>
    </tbody>
</table>
            </div>
  </div>

    <div class="mt-4">

        <asp:Button ID="btnRegister" Width="100%"  CssClass="btn btn-success" runat="server" Text="Save" Style="left: 0px; top: 0px" />

    </div>
</asp:Content>
