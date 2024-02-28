<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="ProjectCreation.aspx.cs" Inherits="AGMSystem.ProjectCreation" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Project Creation</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Projects</a></li>
                        <li class="breadcrumb-item active">Creation</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="txtID" runat="server" />
    <asp:HiddenField ID="txtStatusID" runat="server" />

    <div class="card">
        <div class="card-header align-items-center d-flex">
            <h4 class="card-title mb-0 flex-grow-1">Project Creation</h4>

        </div>
        <!-- end card header -->

        <div class="card-body">
            <div class="mb-3">
                <label for="pojName" class="form-label">Project Name</label>
                <asp:TextBox ID="txtProjName" placeholder="Orange Farm" CssClass="form-control" runat="server"></asp:TextBox>
            </div>
            <div class="row">
                <div class="col-6">
                    <div class="mb-3">
                        <label for="StartleaveDate" class="form-label">Start Date</label>
                        <asp:TextBox ID="txtStartDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>

                </div>
                <div class="col-6">
                    <div class="mb-3">
                        <label for="EndleaveDate" class="form-label">End Date</label>
                        <asp:TextBox ID="txtMaturity" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>

                </div>
            </div>
            <div class="row">
                <div class="col-6">
                    <div class="mb-3">
                        <label for="ExamDate" class="form-label">Exam Date</label>
                        <asp:TextBox ID="txtExamDate" CssClass="form-control" runat="server" TextMode="Date"></asp:TextBox>
                    </div>

                </div>
                <div class="col-6">
                    <div class="mb-3">
                        <label for="ExamDate" class="form-label">Venue </label>
                        <asp:TextBox ID="txtVenue" CssClass="form-control" runat="server"></asp:TextBox>
                    </div>

                </div>
            </div>

            <div class="mb-3">
                <label for="VertimeassageInput" class="form-label">Description</label>
                <asp:TextBox ID="txtDescription" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Enter your description"></asp:TextBox>
            </div>
            <div class="text-end">
                <asp:Button ID="btnCreate" OnClick="btnCreate_Click" CssClass="btn btn-primary" runat="server" Text="Create" />
            </div>
        </div>
    </div>

</asp:Content>
