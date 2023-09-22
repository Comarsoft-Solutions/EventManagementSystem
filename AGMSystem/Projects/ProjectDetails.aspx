<%@ Page Title="" Language="C#" MasterPageFile="~/AGMSystem.Master" AutoEventWireup="true" CodeBehind="ProjectDetails.aspx.cs" Inherits="AGMSystem.ProjectDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
   <!-- start page title -->
                    <div class="row">
                        <div class="col-12">
                            <div class="page-title-box d-sm-flex align-items-center justify-content-between">
                                <h4 class="mb-sm-0">Project Details</h4>

                                <div class="page-title-right">
                                    <ol class="breadcrumb m-0">
                                        <li class="breadcrumb-item"><a href="javascript: void(0);">Projects</a></li>
                                        <li class="breadcrumb-item active">Project Details</li>
                                    </ol>
                                </div>

                            </div>
                        </div>
                    </div>
                    <!-- end page title -->

                    <div class="row justify-content-center">
                        <div class="col-xxl-9">
                            <div class="card" id="demo">
                               
                                <!--end card-header-->
                                <div class="card-body p-4">
                                    <div class="row g-3">
                                        <div class="col-lg-3 col-6">
                                            <p class="text-muted mb-2 text-uppercase fw-semibold">Project Name</p>
                                            <h5 class="fs-14 mb-0"><span id="invoice-no">Avocadoes</span></h5>
                                        </div>
                                        <!--end col-->
                                        <div class="col-lg-3 col-6">
                                            <p class="text-muted mb-2 text-uppercase fw-semibold">Start Date</p>
                                            <h5 class="fs-14 mb-0"><span id="invoice-date">23 Nov, 2021</span></h5>
                                        </div>
                                        <div class="col-lg-3 col-6">
                                            <p class="text-muted mb-2 text-uppercase fw-semibold">Maturity Date</p>
                                            <h5 class="fs-14 mb-0"><span id="invoice-date">23 Nov, 2021</span></h5>
                                        </div>
                                        <!--end col-->
                                        <div class="col-lg-3 col-6">
                                            <p class="text-muted mb-2 text-uppercase fw-semibold">Status</p>
                                            <span class="badge badge-soft-success fs-11" id="payment-status">Active</span>
                                        </div>
                                        <!--end col-->
                                    </div>
                                    <!--end row-->
                                </div>
                         
                                <div class="card-body p-4">
                                    <div class="table-responsive">
                                        <table
                                            class="table table-borderless text-center table-nowrap align-middle mb-0">
                                            <thead>
                                                <tr class="table-active">
                                                    <th scope="col">Member Details</th>
                                                    <th scope="col">Company</th>
                                                    <th scope="col">Status</th>
                                                </tr>
                                            </thead>
                                            <tbody id="products-list">
                                                <tr>
                                                    <td class="text-start">
                                                        <span class="fw-medium">Malcolm Machiri</span>
                                                        <p class="text-muted mb-0">Managing Director
                                                        </p>
                                                    </td>
                                                    <td>Comarton</td>
                                                    <td>Active</td>
                                                </tr>
                                                <tr>
                                                    <td class="text-start">
                                                        <span class="fw-medium">Tafadzwa Kawhai</span>
                                                        <p class="text-muted mb-0">Finance Director
                                                    </p>
                                                    </td>
                                                    <td>Comarton</td>
                                                    <td>Active</td>
                                                </tr>
                                            
                                            </tbody>
                                        </table>
                                        <!--end table-->
                                    </div>
                                </div>
                                <!--end card-body-->
                            </div>
                            <!--end card-->
                        </div>
                        <!--end col-->
                    </div>
                    <!--end row-->
</asp:Content>
