<%@ Page Title="Login Page" Language="C#" MasterPageFile="~/AGMDefault.Master" AutoEventWireup="true" CodeBehind="AGMLogin.aspx.cs" Inherits="AGMSystem.AGMLogin" %>

<asp:Content ID="Content1" ContentPlaceHolderID="MainContent" runat="server">

    <div class="text-center mt-2">
        <h5 class="text-primary">Welcome!</h5>
    </div>
    <div class="mb-3">
        <label for="username" class="form-label">Username</label>
        <asp:TextBox ID="txtUsername" CssClass="form-control" placeholder="Enter Username" runat="server"></asp:TextBox>
    </div>

    <div class="mb-3">
        <div class="float-end">
            <a href="auth-pass-reset-basic.html" class="text-muted">Forgot password?</a>
        </div>
        <label class="form-label" for="password-input">Password</label>
        <div class="position-relative auth-pass-inputgroup mb-3">
            <asp:TextBox ID="txtPassword" CssClass="form-control" placeholder="Enter Password" runat="server" TextMode="Password"></asp:TextBox>
        </div>
    </div>

    <div class="mt-4">
        <asp:Button ID="btnLogin" OnClick="btnLogin_Click" CssClass="btn btn-success w-100" runat="server" Text="Sign In" />

    </div>

</asp:Content>
