<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="MembershipCompanyMapping.aspx.cs" Inherits="AGMSystem.Membership.MembershipCompanyMapping" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Member Company Mapping</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Membership</a></li>
                        <li class="breadcrumb-item active"> Mapping</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
     <div class="row">
        <asp:Label ID="lblComms" runat="server" Text=""></asp:Label>
    </div>
    <div class="col-sm-12 ">
    <div class="col-6">
        <div class="mb-3">
            <label for="Nameinput" class="form-label">Name</label>
            <asp:TextBox ID="txtName" placeholder=" Name" CssClass="form-control" runat="server" ReadOnly="true"></asp:TextBox>
            <asp:HiddenField ID="txtMemberID" runat="server" />
        </div>
    </div>
    <!--end col-->
    <div class="col-6">
        <div class="mb-3">
            <label for="Designationinput" class="form-label">Designation </label>
            <asp:TextBox ID="txtDesignation" ReadOnly="true" placeholder="Designation" CssClass="form-control" runat="server"></asp:TextBox>
        </div>
    </div>
    </div>
    <div class="row">
    <div class="col-5">
        <div class="mb-3">
            <label for="UnassignedCompanies" class="form-label">Unassigned Companies </label>
            <asp:ListBox SelectionMode="Multiple" ID="UnassignedCompanies" runat="server" CssClass="form-control list-group" ></asp:ListBox>
        </div>

    </div>
        <div class="col-2 text-center">
            <div>
            <asp:Button ID="btnAdd" OnClick="btnAdd_Click" runat="server" Text=">" />

            </div>
            <div>
            <asp:Button ID="btnRemove" OnClick="btnRemove_Click"  runat="server" Text="<" />

            </div>
        </div>
    <div class="col-5" CssClass="form-control">
       <div class="mb-3">
            <label for="AssignedCompanies" class="form-label">Assigned Companies </label>
           <asp:ListBox ID="lstAssignedComps" SelectionMode="Multiple" runat="server" CssClass="form-control list-group"  ></asp:ListBox>
        </div>

    </div>

    </div>
    
        <div class="col-lg-12">
            <div class="text-end">
                <asp:Button ID="btnAssign" onClick="btnAssign_Click" CssClass="btn btn-primary" runat="server" Text="Done" />
            </div>
        </div>
</asp:Content>
