<%@ Page Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="AGMEvent.aspx.cs" Inherits="AGMSystem.AGMEventsaspx" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Events Creation</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                        <li class="breadcrumb-item active">Creation</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>
    <div style="width: 80%; justify-content: center; align-content: center; align-items: center; align-content: center; padding-left: 150px;">

        <div class="Row">
            <%--Event  Name --%>
            <div class="row row-cols-2">
                <div class="col-6">
                    <div class="mb-3">
                        <asp:HiddenField ID="txtID" runat="server" />
                        <asp:HiddenField ID="txtEPID" runat="server" />
                        <label for="Event Name" class="form-label">Event Name <span class="text-danger">*</span></label>
                        <asp:TextBox AutoPostBack="true" ID="txtEventname" CssClass="form-control" placeholder="Event Name" runat="server" OnTextChanged="txtEventname_TextChanged"></asp:TextBox>
                    </div>

                </div>
                <div class="col-6">
                    <div class="mb-3">
                        <label for="Event Venue" class="form-label">Event Venue <span class="text-danger">*</span></label>
                        <asp:TextBox ID="TxtVenue" CssClass="form-control" placeholder="Venue " runat="server"></asp:TextBox>
                    </div>

                    <%--<div class="input-group text-center">
                                    <asp:RequiredFieldValidator Display="Dynamic" ID="categorynameValidator" runat="server"
                                        ErrorMessage="Field is required" ControlToValidate="txtEventname"
                                        Font-Size="Small" ForeColor="Red"></asp:RequiredFieldValidator>
                                    </div>--%>
                </div>
            </div>
            <%-- Start Date --%>
            <div class="row row-cols-2">
                <div class="col-6">
                    <div class="mb-3">
                        <label for="Start Date" class="form-label">Start Date <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtStartDate" CssClass="form-control" placeholder="Start Date" runat="server" TextMode="Date"></asp:TextBox>
                    </div>
                </div>

                <%-- End Date --%>
                <div class="col-6">
                    <div class="mb-3">
                        <label for="End Date" class="form-label">End Date <span class="text-danger">*</span></label>
                        <asp:TextBox ID="txtEndDate" TextMode="Date" CssClass="form-control" placeholder="End Date" runat="server"></asp:TextBox>
                    </div>
                </div>
            </div>
        </div>
        <div class="row row-cols-3">
            <div class="col-4 mb-3 mr-3">
                <label for="Attendance Price" class="form-label">Attendance Fee <span class="text-danger">*</span></label>
                <asp:TextBox ID="txtAttendanceFee" TextMode="Number" CssClass="form-control" placeholder="300" runat="server"></asp:TextBox>

            </div>
            <div class="col-6">
                <div class="col-4">
                    <div class="mb-3">
                        <div>
                            <label for="Attendee Settings" class="form-label">Attendee Settings <span class="text-danger">*</span></label>
                        </div>

                    </div>

                </div>
                <div class="row row-cols-2">
                    <div class="col-3">
                        <div class="mb-3">
                            <asp:RadioButton AutoPostBack="true" OnCheckedChanged="RadioOpen_CheckedChanged" ID="RadioOpen" GroupName="AttendeeSettings" runat="server" Text="Open" />

                        </div>

                    </div>
                    <div class="col-3">
                        <div class="mb-1">
                            <asp:RadioButton  ID="RadioInvitation" GroupName="AttendeeSettings" runat="server" CssClass="radio" Text="Invitation" />

                        </div>
                    </div>
                    <%--<asp:RadioButtonList ID="rbAttendeeSettings" runat="server" RepeatDirection="Horizontal" GroupName="Options">
                    <asp:ListItem Value="false">Open</asp:ListItem>
                    <asp:ListItem Value="true">By Invitation</asp:ListItem>
                </asp:RadioButtonList>--%>
                </div>
            </div>
            
    <div class=" col-2 text-end">
        
                    <div class="mb-4"></div>
        <asp:Button ID="txtSaveEvemt" OnClick="btnSaveEvemt_Click" CssClass="btn btn-primary" runat="server" Text="Save Event" />
    </div>
        </div>
           
        <%-- Projects --%>
        <hr class="md-5"/>
        <div class="mb-4"> <label for="LogisticsType" class="form-label">Projects Section <span class="text-danger"></span></label></div>
     
        <div class="row row-cols-2">
            <div class="col-6 mb-3">
                <label  for="Project" class="form-label">Add Project <span class="text-danger"></span></label>
                <asp:TextBox ID="txtProjectListSearch" TextMode="Search" CssClass="form-control" placeholder="Enter Search Parameter" runat="server"></asp:TextBox>
            </div>
            <div class="col-2 mb-3">
                <div class="mb-4"></div>
                <asp:Button ID="btnAddProject" OnClick="btnSearchProject_Click" CssClass="btn btn-primary" runat="server" Text="Search" />
            </div>
        </div>
        
            <div class="col-12 mb-3">
                <div class="form-group row gutters">

                    <div class="form-group row gutters col-12">

                        <div class="col-sm-12 align-content-center">
                            <asp:GridView ID="gridProjectList" Width="100%" runat="server" OnRowCommand="gridProjectList_RowCommand"
                                AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                                DataKeyNames="ID"
                                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                Style="border-collapse: collapse !important"
                                AllowPaging="True" AllowSorting="True" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Project Name"></asp:BoundField>
                                    <asp:BoundField DataField="Description" HeaderText="Description"></asp:BoundField>
                                    <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                                    <asp:BoundField DataField="MaturityDate" HeaderText="MaturityDate"></asp:BoundField>
                                    <asp:BoundField DataField="StatusID" HeaderText="Event Status"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Select">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" ForeColor="Green" CssClass="mdi mdi-check" CommandArgument='<%# Eval("ID") + "," + Eval("Name") %>' CommandName="SelectProject"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        
            <div class="col-12">
                <div class="form-group row gutters">

                    <div class="form-group row gutters col-12">

                        <div class="col-sm-12 align-content-center">
                            <asp:GridView ID="gridAddedProjects" Width="100%" runat="server"
                                AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                                DataKeyNames="ID"
                                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                Style="border-collapse: collapse !important"
                                AllowPaging="True" AllowSorting="True" PageSize="10">
                                <Columns>
                                    <asp:BoundField DataField="Name" HeaderText="Name"></asp:BoundField>
                                    <asp:BoundField DataField="ProjectMembers" HeaderText="Member Count"></asp:BoundField>
                                    <asp:TemplateField HeaderText="Action">
                                        <ItemTemplate>
                                            <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="DeleteRecord"></asp:LinkButton>
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>

                </div>
            </div>
        <hr class="md-5"/>
        <div class="mb-4"> <label for="LogisticsType" class="form-label">Logistics Section <span class="text-danger"></span></label></div>
        
        <%-- Logistics --%>

        <div class="row row-cols-4">
            <div class="col-3">
                <div class="mb-3">
                    <label for="LogisticsType" class="form-label">Select Logistic type <span class="text-danger"></span></label>
                    <asp:DropDownList ID="txtLogisticsType" runat="server" CssClass="form-control dropdown"></asp:DropDownList>

                </div>
                <asp:HiddenField ID="txtTransportID" runat="server" />
                <asp:HiddenField ID="txtAccomodationID" runat="server" />
                <asp:HiddenField ID="txtProjectID" runat="server" />

            </div>
            <!--end col-->
            <div class="col-4">
                <div class="mb-3">
                    <label for="serviceProvider" class="form-label">Enter Service provider</label>
                    <asp:TextBox ID="txtServiceProvider" CssClass="form-control" placeholder="Air Zimbabwe" runat="server"></asp:TextBox>
                </div>
            </div>
            <!--end col-->
            <div class="col-2">
                <div class="mb-3">
                    <label for="capacity" class="form-label">Max Capacity</label>
                    <asp:TextBox ID="txtCapacity" TextMode="Number" CssClass="form-control" placeholder="50" runat="server"></asp:TextBox>
                </div>
            </div>
            <!--end col-->
          <%--  <div class="col-2">
                <div class="mb-3">
                    <label for="price" class="form-label">Unit Price</label>
                    <asp:TextBox ID="txtPrice" TextMode="Number" CssClass="form-control" placeholder="300" runat="server"></asp:TextBox>
                </div>
            </div>--%>
            <!--end col-->
            <div class="col-1">
                <div class="text-end">
                    <div class="mb-4"></div>
                    <asp:Button ID="btnAddLogistics" OnClick="btnAddLogistics_Click" CssClass="btn btn-primary" runat="server" Text="Add" />
                </div>
            </div>
            <!--end col-->
            <!--end row-->
        </div>
       
        
                <div class="col-12 mb-3">
                    <div class="form-group row gutters">

                        <div class="form-group row gutters col-12">

                            <div class="col-sm-12 align-content-center">
                                <asp:GridView ID="gridAccomodation" Width="100%" runat="server"
                                    AutoGenerateColumns="False" AutoGenerateSelectButton="false"
                                    DataKeyNames="ID"
                                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                                    Style="border-collapse: collapse !important"
                                    AllowPaging="True" AllowSorting="True" PageSize="10">
                                    <Columns>
                                        <asp:BoundField Visible="false" DataField="ID" HeaderText="Transport ID"></asp:BoundField>
                                        <asp:BoundField DataField="Name" HeaderText=" Service Provider "></asp:BoundField>
                                        <asp:BoundField DataField="Capacity" HeaderText="Max Capacity"></asp:BoundField>
                                        <asp:BoundField DataField="Available" HeaderText="Available Space" Visible="false"></asp:BoundField>
                                        <asp:TemplateField HeaderText="Action">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="Delete" runat="server" ForeColor="Red" CssClass="mdi mdi-delete" CommandArgument='<%#Eval("ID")%>' CommandName="DeleteRecord"></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>
                                    </Columns>
                                </asp:GridView>
                            </div>
                        </div>

                    </div>
                </div>
                
           

    </div>

    <div class="text-end">

        <asp:Button ID="btnFinish" OnClick="btnFinish_Click" CssClass="btn btn-success" runat="server" Text="Finish" />
    </div>
    <%-- end button --%>



</asp:Content>
