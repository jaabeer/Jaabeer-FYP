<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="AdminHomepage1.aspx.cs" Inherits="DealProjectTamam.Admin_J.AdminHomepage1" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

    <!-- Page Heading -->
    <div class="d-sm-flex align-items-center justify-content-between mb-4">
        <h1 class="h3 mb-0 text-gray-800">Dashboard</h1>
        <a href="#" class="d-none d-sm-inline-block btn btn-sm btn-primary shadow-sm">
            <i class="fas fa-download fa-sm text-white-50"></i> Generate Report
        </a>
    </div>

    <!-- Content Row -->
    <div class="row">
        <!-- Number of Hotels -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-primary shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-primary text-uppercase mb-1">
                                No of Hotels
                            </div>
                            <div class="h5 mb-0 font-weight-bold text-gray-800">
                                <asp:HyperLink ID="hy_no_of_villas" NavigateUrl="Search_hotel.aspx" runat="server"></asp:HyperLink>
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-building fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Number of Owners -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-success shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-success text-uppercase mb-1">
                                No of Owners
                            </div>
                            <div class="h5 mb-0 font-weight-bold text-gray-800">10</div>
                            <asp:HyperLink ID="hy_no_of_owners" NavigateUrl="SearchOwner.aspx" runat="server" CssClass="smalltext-white stretched-link"></asp:HyperLink>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-user-cog fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Number of Clients -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-info shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-info text-uppercase mb-1">
                                No of Clients
                            </div>
                            <div class="row no-gutters align-items-center">
                                <div class="col-auto">
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:HyperLink ID="hy_no_of_clients" NavigateUrl="Search_client.aspx" runat="server" CssClass="smalltext-white stretched-link"></asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-user-tag fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Ongoing Transactions -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-warning shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-warning text-uppercase mb-1">
                                Ongoing Transaction
                            </div>
                            <div class="h5 mb-0 font-weight-bold text-gray-800">18</div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-edit fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <!-- Content Row -->
    <div class="row">
        <!-- Booking per Hotel Pie Chart -->
        <div class="col-lg-6">
            <div class="card shadow mb-4">
                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 font-weight-bold text-primary">Booking per Hotel</h6>
                </div>
                <div class="card-body">
                    <div class="chart-area">
                        <ajaxToolkit:PieChart ID="PieChart_booking_per_hotel" 
                            Font-Bold="true" ChartHeight="300" ChartWidth="400"
                            runat="server" BorderColor="Black">
                        </ajaxToolkit:PieChart>
                    </div>
                </div>
            </div>
        </div>

        <!-- Booking per Month Bar Chart -->
        <div class="col-lg-6">
            <div class="card shadow mb-4">
                <div class="card-header py-3 d-flex flex-row align-items-center justify-content-between">
                    <h6 class="m-0 font-weight-bold text-primary">Booking per Month</h6>
                </div>
                <div class="card-body">
                    <div class="chart-area">
                        <ajaxToolkit:BarChart ID="BarChart_booking_per_month" runat="server" ChartHeight="300" ChartWidth="500"
                            ChartType="Column" ChartTitleColor="#0E426C" CategoryAxisLineColor="#D08AD9" ValueAxisLines="10"   
                            ValueAxisLineColor="#D08AD9" BaseLineColor="#A156AB" ViewStateMode="Inherit">
                        </ajaxToolkit:BarChart>
                    </div>
                </div>
            </div>
        </div>
    </div>

    <div class="row">
        <!-- Notification Card -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-info shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-info text-uppercase mb-1">
                                Notification
                            </div>
                            <div class="row no-gutters align-items-center">
                                <div class="col-auto">
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:HyperLink ID="hy_notif" NavigateUrl="Notif_update_Hotel.aspx" runat="server" CssClass="smalltext-white stretched-link"></asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-envelope fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Support Tickets Card -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-info shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <div class="text-xs font-weight-bold text-info text-uppercase mb-1">
                                Support Tickets
                            </div>
                            <div class="row no-gutters align-items-center">
                                <div class="col-auto">
                                    <div class="h5 mb-0 font-weight-bold text-gray-800">
                                        <asp:HyperLink ID="hy_tickets" NavigateUrl="Notif_AllTickets.aspx" runat="server" CssClass="smalltext-white stretched-link"></asp:HyperLink>
                                    </div>
                                </div>
                            </div>
                        </div>
                        <div class="col-auto">
                            <i class="fas fa-user-shield fa-2x text-gray-300"></i>
                        </div>
                    </div>
                </div>
            </div>
        </div>

        <!-- Year Selector -->
        <div class="col-xl-3 col-md-6 mb-4">
            <div class="card border-left-info shadow h-100 py-2">
                <div class="card-body">
                    <div class="row no-gutters align-items-center">
                        <div class="col mr-2">
                            <h5 class="m-0 font-weight-bold text-primary">Statistics for year</h5>
                            <asp:DropDownList runat="server" Width="100px" ID="ddlyear" ForeColor="Black" OnSelectedIndexChanged="ddlyear_SelectedIndexChanged" AutoPostBack="true" CssClass="form-control form-control-user" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>

</asp:Content>
