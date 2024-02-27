<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="MemberCreation.aspx.cs" Inherits="AGMSystem.MemberCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Member Creation</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Membership</a></li>
                        <li class="breadcrumb-item active">Member Creation</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <%-- end breadcrumb --%>
    <asp:HiddenField ID="txtEventID" runat="server" />
    <asp:HiddenField ID="txtQueryID" runat="server" />
    <asp:HiddenField ID="txtMemberID" runat="server" />

    <div class="col-6">
        <div class="mb-3">
            <label for="RegType" class="form-label">Select Registration Type</label>
            <asp:DropDownList ID="txtRegType" AutoPostBack="true" OnTextChanged="txtRegType_TextChanged" runat="server" CssClass="dropdown-form form-control">
            </asp:DropDownList>
        </div>
    </div>
    <asp:Panel ID="pnlMember" runat="server">

        <%--<div class="col-xl-12">--%>
        <div class="card">
            <div class="card-header align-items-center d-flex">
                <h4 class="card-title mb-0 flex-grow-1">Member Registration</h4>

            </div>
            <!-- end card header -->

            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="firstNameinput" class="form-label">ID Number</label>
                            <asp:TextBox AutoPostBack="true" OnTextChanged="txtNationalID_TextChanged" ID="txtNationalID" placeholder="292345464Y56" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="firstNameinput" class="form-label">First Name</label>
                            <asp:TextBox ID="txtFname" placeholder="First Name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="lastNameinput" class="form-label">Last Name</label>
                            <asp:TextBox ID="txtLname" placeholder="Last Name" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="phonenumberInput" class="form-label">Phone Number</label>
                            <asp:TextBox ID="txtPhoneNumber" placeholder="236 776 339 832" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="emailidInput" class="form-label">Email Address</label>
                            <asp:TextBox ID="txtEmail" placeholder="example@gamil.com" CssClass="form-control" runat="server" TextMode="Email"></asp:TextBox>
                        </div>
                    </div>
                    <!--end col-->

                    <div class="col-6">
                        <div class="mb-3">
                            <label for="companyNameinput" class="form-label">Company Name</label>
                            <asp:DropDownList runat="server" ID="txtCompanyName" CssClass="form-control dropdown">
                            </asp:DropDownList>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="ForminputState" class="form-label">Designation </label>
                            <asp:DropDownList runat="server" ID="txtDesignation" CssClass="form-control dropdown">
                            </asp:DropDownList>

                        </div>
                    </div>
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="tshirt" class="form-label">T-shirt Size </label>
                            <asp:TextBox ID="txtTshirt" placeholder="medium" CssClass="form-control" runat="server"></asp:TextBox>


                        </div>
                    </div>

                    <%--  --%>
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="ForminputState" class="form-label">Pension Fund </label>
                            <asp:TextBox runat="server" ID="txtPensionFund" CssClass="form-control " placeholder="Comarton Umbrella">
                    
                            </asp:TextBox>

                        </div>
                    </div>
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="address" class="form-label">Address </label>
                            <asp:TextBox ID="txtMemAddress" placeholder="1 McChlery" CssClass="form-control" runat="server"></asp:TextBox>


                        </div>
                    </div>
                    <%--       <div class="col-2">
    <div class="mb-1 margin-right-0">
        <label for="address" class="form-label">Are You Playing Golf? </label>
            </div>
</div>
<div class="col-1">
    <div class="mb-2 mt-1 m-1">
        <asp:CheckBox ID="chkGolf" runat="server" class="form-label"/>
            </div>
</div>--%>
                    <!--end col-->
                </div>
            </div>
        </div>
        <%--</div>--%>
    </asp:Panel>
    <%-- Company reg start --%>
    <asp:Panel ID="pnlCompany" runat="server">
        <asp:HiddenField ID="txtCompanyID" runat="server" />
        <div class="card">
            <div class="card-header align-items-center d-flex">
                <h4 class="card-title mb-0 flex-grow-1">Company Registration</h4>

            </div>
            <!-- end card header -->

            <div class="card-body">
                <div class="row">
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="CompanyName" class="form-label">Company Name</label>
                            <asp:TextBox ID="txtCompanyReg" placeholder="Comarsoft Solutions" OnTextChanged="txtCompanyName_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>
                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="Address" class="form-label">Address</label>
                            <asp:TextBox ID="txtAddress" placeholder="118 McChlery Ave" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="city" class="form-label">City</label>
                            <asp:TextBox ID="txtCity" placeholder="Harare" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="city" class="form-label">Email</label>
                            <asp:TextBox ID="txtCompEmail" placeholder="enquiries@comarsoft.co.zw" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!--end col-->
                    <!--end col-->
                    <div class="col-6">
                        <div class="mb-3">
                            <label for="city" class="form-label">Phone Number</label>
                            <asp:TextBox ID="txtCompPhone" placeholder="26377585945" CssClass="form-control" runat="server"></asp:TextBox>
                        </div>
                    </div>

                    <!--end col-->

                </div>
            </div>
        </div>
    </asp:Panel>
    <%-- end company panel --%>
    <div class="col-lg-12">
        <div class="text-end">
            <asp:Button ID="btnCreate" OnClick="btnCreate_Click" CssClass="btn btn-primary" runat="server" Text="Create" />
        </div>
    </div>
    <!--end col-->

    <!--ENd Member Reg-->


</asp:Content>
