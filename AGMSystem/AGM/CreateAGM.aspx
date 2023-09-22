<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="CreateAGM.aspx.cs" Inherits="AGMSystem.CreateAGM" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">AGM</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">AGM</a></li>
                        <li class="breadcrumb-item active">Create</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 80%; justify-content: center; align-content: center; align-items: center; align-content: center; padding-left: 150px;">


        <%--AGM  Name --%>
        <div class="row row-col-2">
        <div class="col-6 mb-3">
            <asp:HiddenField ID="txtID" runat="server" />
            <asp:HiddenField ID="txtEventID" runat="server" />
            <label for="AGM Name" class="form-label">AGM Name <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtName" CssClass="form-control" placeholder="AGM Name" runat="server"></asp:TextBox>
        <div class="input-group text-center">
            <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                ErrorMessage="Field is required" ControlToValidate="txtName"
                Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        </div>
        <div class="col-6 mb-3">
            <label for="Venue" class="form-label">AGM Venue <span class="text-danger">*</span></label>
            <asp:TextBox ID="TxtVenue" CssClass="form-control" placeholder="Venue " runat="server"></asp:TextBox>
<div class="input-group text-center">
            <asp:RequiredFieldValidator Display="Dynamic" ID="VenueValidator" runat="server"
                ErrorMessage="Field is required" ControlToValidate="TxtVenue"
                Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
        </div>
        </div>
</div>
        
        <%-- Start Date --%>
        <div class="row row-col-2">
        <div class="col 6 mb-3">
            <label for="Start Date" class="form-label">Start Date <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtStartDate" CssClass="form-control" placeholder="Start Date" runat="server" TextMode="Date"></asp:TextBox>
        </div>

        <%-- <div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator1" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtStartDate"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>--%>

        <%-- End Date --%>
        <div class="col-6 mb-3 ">
            <label for="End Date" class="form-label">End Date <span class="text-danger">*</span></label>
            <asp:TextBox ID="txtEndDate" TextMode="Date" CssClass="form-control" placeholder="End Date" runat="server"></asp:TextBox>
        </div>

        <%-- <div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="RequiredFieldValidator2" runat="server"
                                        ErrorMessage="Field is required" ControlToValiDate="txtEndDate"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                </div>--%>

        </div>
        <div class="row row-col-2">
            <div class="col-6 mb-3">
                <div>
                    <label for="Attendee Settings" class="form-label">Attendee Settings <span class="text-danger">*</span></label>
                </div>
                <asp:RadioButtonList ID="rbAttendeeSettings" runat="server" RepeatDirection="Vertical" GroupName="Options">
                    <asp:ListItem Value="false">Open</asp:ListItem>
                    <asp:ListItem Value="true">By Invitation</asp:ListItem>
                </asp:RadioButtonList>
                <div class="input-group text-center">
                    <asp:RequiredFieldValidator Display="Dynamic" ID="rbAttendeeSettingsValidator" runat="server"
                        ErrorMessage="Field is required" ControlToValidate="rbAttendeeSettings"
                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

            </div>
            <div class="col-6 mb-3">
                <div>
                    <label for="Event Settings" class="form-label">Event Settings <span class="text-danger">*</span></label>
                </div>
                <asp:RadioButtonList ID="rbEventSettings" runat="server" RepeatDirection="Vertical" GroupName="Options" AutoPostBack="true" OnSelectedIndexChanged="rbEventSettings_SelectedIndexChanged">
                    <asp:ListItem Value="false">Separate AGM</asp:ListItem>
                    <asp:ListItem Value="true">Event Based</asp:ListItem>
                </asp:RadioButtonList>
                <div class="input-group text-center">
                    <asp:RequiredFieldValidator Display="Dynamic" ID="rbEventSettingsValidator2" runat="server"
                        ErrorMessage="Field is required" ControlToValidate="rbEventSettings"
                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                </div>

            </div>

        </div>
        <%-- End Settings row-col-2 --%>
        <asp:Panel ID="pnlSearch" runat="server">
        <div class="row row-cols-2" >
            <div class="col-6 mb-3">
                <label  for="Project" class="form-label">Add Project <span class="text-danger"></span></label>
                <asp:TextBox ID="txtEventSearch" TextMode="Search" CssClass="form-control" placeholder="Enter Search Parameter" runat="server"></asp:TextBox>
            </div>
            <div class="col-2 mb-3">
                <div class="mb-4"></div>
                <asp:Button ID="btnSearchEvent" OnClick="btnSearchEvent_Click" CssClass="btn btn-primary" runat="server" Text="Search" />
            </div>
        </div>

        </asp:Panel>
        <%-- End Search area--%>
        <div class="form-group row gutters">

            <div class="form-group row gutters col-12">

                <div class="col-sm-12 align-content-center">
                    <asp:GridView ID="grdEventSelect" Width="100%" runat="server"
                        AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                        DataKeyNames="ID"
                        CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                        Style="border-collapse: collapse !important"
                        AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdEventSelect_RowCommand">
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>
                            <asp:BoundField DataField="EventName" HeaderText="Event Name"></asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                            <asp:BoundField DataField="StatusID" HeaderText="Event Status"></asp:BoundField>

                            <asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:LinkButton ID="view" runat="server" ForeColor="Green" CssClass="mdi mdi-check" CommandArgument='<%#Eval("EventName")%>' CommandName="selectEvent"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
        <%-- End Result Grid --%>
        <div class="form-group row gutters">

            <div class="form-group row gutters col-12">

                <div class="col-sm-12 align-content-center">
                    <asp:GridView ID="gridSelectedEvent" Width="100%" runat="server"
                        AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                        DataKeyNames="ID"
                        CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                        Style="border-collapse: collapse !important"
                        AllowPaging="True" AllowSorting="True" PageSize="10" >
                        <Columns>
                            <asp:BoundField DataField="ID" HeaderText="ID"></asp:BoundField>
                            <asp:BoundField DataField="EventName" HeaderText="Event Name"></asp:BoundField>
                            <asp:BoundField DataField="Venue" HeaderText="Event Venue"></asp:BoundField>
                            <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                            <asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>
                            <asp:BoundField DataField="StatusID" HeaderText="Event Status"></asp:BoundField>

                            <%--<asp:TemplateField HeaderText="Select">
                                <ItemTemplate>
                                    <asp:LinkButton ID="view" runat="server" ForeColor="Green" CssClass="mdi mdi-check" CommandArgument='<%#Eval("ID")%>' CommandName="selectEvent"></asp:LinkButton>
                                </ItemTemplate>
                            </asp:TemplateField>--%>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>

        </div>
        <%-- End Selected Grid --%>
    </div>
    <%-- End Div --%>
    <div class="text-end">

        <asp:Button ID="btncreate" OnClick="btncreate_Click" CssClass="btn btn-success" runat="server" Text="Create" />
    </div>

</asp:Content>
