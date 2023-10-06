<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="Dashboard.aspx.cs" Inherits="AGMSystem.Dashboard" %>

<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- start page title -->
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">Dashboard</h4>

            </div>
        </div>
    </div>
    <!-- end page title -->

    <div class="row">
        <div class="col-xl-12">
            <div class="card crm-widget">
                <div class="card-body p-0">
                    <div class="row row-cols-xxl-4 row-cols-md-4 row-cols-3 g-0">
                        <div class="col">
                            <div class="py-4 px-4">
                                <h5 class="text-muted text-uppercase fs-13">Members</h5>
                                <div class="d-flex align-items-center">
                                    <div class="flex-grow-1 ms-4">
                                        <h2 class="mb-0">
                                            <asp:Label ID="lblMembers" runat="server" Text=""></asp:Label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                        <div class="col">
                            <div class="mt-4 mt-md-0 py-4 px-4">
                                <h5 class="text-muted text-uppercase fs-13">Events</h5>
                                <div class="d-flex align-items-center">
                                    <div class="flex-grow-1 ms-4">
                                        <h2 class="mb-0">
                                            <asp:Label ID="lblEvents" runat="server" Text=""></asp:Label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                        <div class="col">
                            <div class="mt-3 mt-md-0 py-4 px-4">
                                <h5 class="text-muted text-uppercase fs-13">Total Projects</h5>
                                <div class="d-flex align-items-center">
                                    <div class="flex-grow-1 ms-4">
                                        <h2 class="mb-0">
                                            <asp:Label ID="lblTotalProjects" runat="server" Text=""></asp:Label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                        <div class="col">
                            <div class="mt-4 mt-md-0 py-4 px-4">
                                <h5 class="text-muted text-uppercase fs-13">AGMS</h5>
                                <div class="d-flex align-items-center">
                                    <div class="flex-grow-1 ms-4">
                                        <h2 class="mb-0">
                                            <asp:Label ID="lblOpenProjects" runat="server" Text=""></asp:Label>
                                        </h2>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <!-- end col -->
                        <!-- end col -->
                    </div>
                    <!-- end row -->
                </div>
                <!-- end card body -->
            </div>
            <!-- end card -->
        </div>
        <!-- end col -->
    </div>
    <!-- end row -->

    <asp:Panel ID="pnlOnGoing" Visible ="false" runat="server">
    <div class="row">
        <div class="col-xl-12">
            <div class="card" >
                <div class="card-header align-items-center d-flex">
                    <h4 class="card-title mb-0 flex-grow-1">OnGoing Events</h4>
                    
                </div>
                <!-- end card header -->

                <div class="card-body">
                    <div class="table-responsive table-card">
                        
                               <div class="form-group row gutters">

    <div class="form-group row gutters col-12">

        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdOngoingEvents" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" 
                DataKeyNames="ID"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" >
                <Columns>
                    <asp:BoundField Visible="false" DataField="ID" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="EventName" HeaderText="Event Name"></asp:BoundField>
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                    <asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>
                    <asp:BoundField DataField="AttendanceFee" HeaderText="Attendance Fee"></asp:BoundField>
                 
                   
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>
                           
                        <!-- end table -->
                    </div>
                    <!-- end table responsive -->
                </div>
                <!-- end card body -->
            </div>
            <!-- end card -->
        </div>
        <!-- end col -->
        <!-- end col -->

    </div>
    <!-- end row -->

    </asp:Panel>
    <asp:Panel ID="pnlUpcomming" runat="server">
    <div class="row">
        <div class="col-xl-12">
            <div class="card" ">
                <div class="card-header align-items-center d-flex">
                    <h4 class="card-title mb-0 flex-grow-1">Upcoming Events</h4>
                    
                </div>
                <!-- end card header -->

                <div class="card-body">
                    <div class="table-responsive table-card">
                        
                               <div class="form-group row gutters">

    <div class="form-group row gutters col-12">

        <div class="col-sm-12 align-content-center">
            <asp:GridView ID="grdEvents" Width="100%" runat="server"
                AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdEvents_PageIndexChanging"
                DataKeyNames="ID"
                CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                Style="border-collapse: collapse !important"
                AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdEvents_RowCommand">
                <Columns>
                    <asp:BoundField Visible="false" DataField="ID" HeaderText="ID"></asp:BoundField>
                    <asp:BoundField DataField="EventName" HeaderText="Event Name"></asp:BoundField>
                    <asp:BoundField DataField="StartDate" HeaderText="Start Date"></asp:BoundField>
                    <asp:BoundField DataField="EndDate" HeaderText="End Date"></asp:BoundField>
                    <asp:BoundField DataField="AttendanceFee" HeaderText="Attendance Fee"></asp:BoundField>
                 <%--   <asp:TemplateField HeaderText="View">
                        <ItemTemplate>
                          
                            <asp:LinkButton ID="Attach" runat="server" ForeColor="Green" CssClass="mdi mdi-eye" CommandArgument='<%#Eval("ID")%>' CommandName="attachRecord"></asp:LinkButton>
                        </ItemTemplate>
                    </asp:TemplateField>--%>
                   
                </Columns>
            </asp:GridView>
        </div>
    </div>

</div>
                           
                        <!-- end table -->
                    </div>
                    <!-- end table responsive -->
                </div>
                <!-- end card body -->
            </div>
            <!-- end card -->
        </div>
        <!-- end col -->
        <!-- end col -->

    </div>
    <!-- end row -->

    </asp:Panel>
</asp:Content>
