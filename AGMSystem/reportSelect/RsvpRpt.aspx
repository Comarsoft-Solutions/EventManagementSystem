<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="RsvpRpt.aspx.cs" Inherits="AGMSystem.reportSelect.RsvpRpt" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
             <div class="row">
                    <div class="col-12">
                        <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                            <h4 class="mb-sm-0"> RSVP Report</h4>

                            <div class="page-title-right">
                                <ol class="breadcrumb m-0">
                                    <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                                    <li class="breadcrumb-item active">RSVP Report</li>
                                </ol>
                            </div>

                        </div>
                    </div>
                </div>
                <!-- end page title -->
    
     
                             <div class="table-responsive table-card">
                                 <div class="mb-3">
    <label for=" Events" class="form-label">Select Event <span class="text-danger"></span></label>
    <asp:DropDownList ID="txtEvents" CssClass="form-control" OnTextChanged="txtEvents_TextChanged" AutoPostBack="true" runat="server" ></asp:DropDownList>
</div>
</div>
</asp:Content>
