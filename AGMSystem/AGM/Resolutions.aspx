﻿<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="Resolutions.aspx.cs" Inherits="AGMSystem.Resolutions" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="row">
        <div class="col-12">
            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                <h4 class="mb-sm-0">AGM</h4>
                <div class="page-title-right">
                    <ol class="breadcrumb m-0">
                        <li class="breadcrumb-item"><a href="javascript: void(0);">AGM</a></li>
                        <li class="breadcrumb-item active">Resolutions</li>
                    </ol>
                </div>
            </div>
        </div>
    </div>

     <div class="mb-3">
            <label for=" Resolution" class="form-label">Enter Resolution <span class="text-danger"></span></label>
            <asp:DropDownList ID="txtAgms" OnTextChanged="txtAgms_TextChanged" AutoPostBack="true" CssClass="form-control" runat="server" ></asp:DropDownList>
        </div>

    <asp:HiddenField ID="txtAGMID" runat="server" />
    <div class="mb-3">
            <label for=" Resolution" class="form-label">Enter Resolution <span class="text-danger"></span></label>
            <asp:TextBox ID="txtResolution"  CssClass="form-control"  placeholder="Resolution" runat="server" ></asp:TextBox>
        </div>
    
    <div class="mb-3">
        <label for="Description" class="form-label">Resolution Description <span class="text-danger"></span></label>
        <asp:TextBox ID="txtDetails" CssClass="form-control" placeholder="Resolution Description" runat="server" TextMode="MultiLine"></asp:TextBox>
    </div>

    <div class="col-lg-12">
            <div class="text-end">
                <asp:Button ID="btnCreate"  CssClass="btn btn-primary" runat="server" Text="Add Resolution" OnClick="btnCreate_Click" />
            </div>
        </div>

    
    <!-- Small Tables -->
    
    <div class="form-group row gutters">

        <div class="form-group row gutters col-12">

            <div class="col-sm-12 align-content-center">
                <asp:GridView ID="grdResolutions" Width="100%" runat="server"
                    AutoGenerateColumns="False" AutoGenerateSelectButton="false" OnPageIndexChanging="grdResolutions_PageIndexChanging"
                    DataKeyNames="ID"
                    CssClass="table table-condensed" GridLines="None" role="grid" aria-describedby="DataTables_Table_0_info"
                    Style="border-collapse: collapse !important"
                    AllowPaging="True" AllowSorting="True" PageSize="10" OnRowCommand="grdResolutions_RowCommand">
                    <Columns>
                        <asp:BoundField Visible="false" DataField="ID" HeaderText=" ID"></asp:BoundField>
                        <asp:BoundField DataField="Resolution" HeaderText="Resolution"></asp:BoundField>
                        <asp:BoundField DataField="Details" HeaderText="Details"></asp:BoundField>

                        <%--<asp:TemplateField HeaderText="View">
                                            <ItemTemplate>
                                                <asp:LinkButton ID="view" runat ="server" ForeColor="Green" CssClass="fa fa-file-archive-o fa-2x" CommandArgument='<%#Eval("ID")%>' CommandName ="selectrecord" ></asp:LinkButton>
                                            </ItemTemplate>
                                        </asp:TemplateField>     --%>
                    </Columns>
                </asp:GridView>
            </div>
        </div>

    </div>
</asp:Content>
