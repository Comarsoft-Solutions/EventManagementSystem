<%@ Page Title="" Language="C#" MasterPageFile="~/OnlinePages.Master" AutoEventWireup="true" CodeBehind="EventEvaluation.aspx.cs" Inherits="AGMSystem.Events.EventEvaluation" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Event Evaluation</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                        <li class="breadcrumb-item active">Evaluation</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <asp:HiddenField ID="txtMemberID" runat="server" />
    <asp:HiddenField ID="txtname" runat="server" />
    <asp:HiddenField ID="txtEventID" runat="server" />
    
                                         
    
      <asp:Panel ID="pnlEventDetails" Visible="false" runat="server">
          <div class="row row-cols-3">
    <div class="col-4 mb-3">
        <label for="eventName" class="form-label">Event Name</label>
        <asp:TextBox ID="txtEventName" placeholder="POs and Chairs" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="col-4 mb-3">
        <label for="Event Date" class="form-label">Date </label>
        <asp:TextBox ID="txtDate" placeholder="8 to 10 Sept 2023" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="col-4 mb-3">
        <label for="organiser" class="form-label">Primary Event Organiser</label>
        <asp:TextBox ID="txtOrganiser" placeholder="ZAPF" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
              </div>
    </asp:Panel>
       
    <div class="mb-3">
        <label for="organiser" class="form-label">1.	Rate the success of the event (1: not successful: 10 very successful)</label>
        <asp:TextBox ID="txt1" placeholder="9" CssClass="form-control" runat="server"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="VertimeassageInput" class="form-label">2.	Describe what worked well:</label>
        <asp:TextBox ID="txt2" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Enter your description"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="VertimeassageInput" class="form-label">3.	Describe what did not work well or requires improvement:</label>
        <asp:TextBox ID="txt3" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Enter your description"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="VertimeassageInput" class="form-label">4.	Were there any unforeseen problems:
                                                                If Yes, how could you prepare better in the future?
                                                                </label>
        <asp:TextBox ID="txt4" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Enter your description"></asp:TextBox>
    </div>
    <div class="mb-3">
        <label for="VertimeassageInput" class="form-label">5.	What would you do differently if you ran this event again?</label>
        <asp:TextBox ID="txt5" CssClass="form-control" runat="server" TextMode="MultiLine" placeholder="Enter your description"></asp:TextBox>
    </div>
    <div class="text-end">
        <asp:Button ID="btnSubmit" OnClick="btnSubmit_Click"  CssClass="btn btn-primary" runat="server" Text="Submit" />
    </div>


</asp:Content>
