<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="LogisticsManagement.aspx.cs" Inherits="AGMSystem.EventManagement" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
      <div class="row">
                            <div class="col-12">
                                <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                    <h4 class="mb-sm-0">Logistics Management</h4>
                                    <div class="page-title-right">
                                        <ol class="breadcrumb m-0">
                                            <li class="breadcrumb-item"><a href="javascript: void(0);">Events</a></li>
                                            <li class="breadcrumb-item active">Event Management</li>
                                        </ol>
                                    </div>
                                </div>
                            </div>
                        </div>
   
    <asp:HiddenField runat="server" ID="txtEventID" />
    <asp:HiddenField runat="server" ID="txtComboID" />
    <div class="col-6"> 
        <asp:Label runat="server"> Select Event </asp:Label>
        <asp:DropDownList runat="server" CssClass="form-control" id="dlEvents" AutoPostBack="true" OnTextChanged="dlEvents_TextChanged"></asp:DropDownList>
    </div>
    <div class="row row-cols-5">
        <div class="col-3"> 
        <asp:Label runat="server"> Create Combo </asp:Label>
        <asp:TextBox runat="server" CssClass="form-control" id="txtCombo" placeHolder="Flying/Double room"></asp:TextBox>
    </div>
        <div class="col-3"> 
        <asp:Label runat="server"> Select Transport </asp:Label>
        <asp:DropDownList runat="server" CssClass="form-control" id="dlTransport" ></asp:DropDownList>
    </div>
        <div class="col-3"> 
        <asp:Label runat="server"> Select Accomodation </asp:Label>
        <asp:DropDownList runat="server" CssClass="form-control" id="dlAccomodation" ></asp:DropDownList>
    </div>
        <div class="col-1"> 
        <asp:Label runat="server"> Price </asp:Label>
        <asp:TextBox runat="server" CssClass="form-control" id="txtPrice" placeHolder="8000"></asp:TextBox>
    </div>
          <div class="col-1"> 
        <asp:Label runat="server"> Capacity </asp:Label>
        <asp:TextBox runat="server" CssClass="form-control" id="txtComboCapacity" placeHolder="2"></asp:TextBox>
    </div>

        <div class="col-1 text-end">
            <div class="mb-3"></div>
            <asp:Button ID="btnCreate" CssClass="btn btn-primary" OnClick="btnCreate_Click" runat="server" Text="Create" />
        </div>
    </div>
    <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdLogistics" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdLogistics_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdLogistics_RowCommand">
                    <Columns>
                        <asp:BoundField DataField="Combo" HeaderText="Logistics"></asp:BoundField>
                        <asp:BoundField DataField="Price" HeaderText="Price "></asp:BoundField>
                        <asp:BoundField DataField="ComboCapacity" HeaderText="Capacity "></asp:BoundField>
                        <asp:BoundField DataField="Accomodation" HeaderText="Accomodation "></asp:BoundField>
                        <asp:BoundField DataField="Transport" HeaderText="Transport "></asp:BoundField>

                      
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
