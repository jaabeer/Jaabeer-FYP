<%@ Page Title="" Language="C#" MasterPageFile="~/Owner2Master.Master" AutoEventWireup="true" CodeBehind="ActiveBooking1.aspx.cs" Inherits="DealProjectTamam.ActiveBooking1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main {
            font-family: Arial, sans-serif;
            padding: 20px;
            background-color: #f4f4f9;
        }

        .wrap {
            max-width: 1200px;
            margin: auto;
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }

        .breadcrumbs ul {
            list-style: none;
            padding: 0;
            display: flex;
            flex-wrap: wrap;
        }

        .breadcrumbs ul li {
            margin-right: 10px;
        }

        .breadcrumbs ul li a, .breadcrumbs ul li asp\:Button {
            background-color: #007bff;
            color: #fff;
            padding: 10px 15px;
            text-decoration: none;
            border-radius: 4px;
            display: inline-block;
        }

        .breadcrumbs ul li a:hover, .breadcrumbs ul li asp\:Button:hover {
            background-color: #0056b3;
        }

        h3 {
            color: #333;
            margin-bottom: 20px;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        .table th, .table td {
            border: 1px solid #e6e5e5;
            padding: 10px;
            text-align: left;
        }

        .table th {
            background-color: #007bff;
            color: #fff;
        }

        .table-striped tbody tr:nth-of-type(odd) {
            background-color: #f9f9f9;
        }

        .gradient-button {
            background: linear-gradient(to right, #007bff, #00c6ff);
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            border-radius: 4px;
            cursor: pointer;
        }

        .gradient-button:hover {
            background: linear-gradient(to right, #0056b3, #0094ff);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="main">
        <div class="wrap">
            <!--breadcrumbs-->
            <nav class="breadcrumbs">
                <!--crumbs-->
                <ul>
                    <li>
                        <asp:Button runat="server" ID="btn_all_bookings" OnClick="btn_all_bookings_Click" Text="All" CssClass="gradient-button" />
                    </li>
                    <li>&nbsp;</li>
                    <li>
                        <asp:Button runat="server" ID="btn_confirmed_bookings" OnClick="btn_confirmed_bookings_Click" Text="Confirmed" CssClass="gradient-button" />
                    </li>
                    <li>&nbsp;</li>
                    <li>&nbsp;</li>
                    <li>
                        <asp:Button runat="server" ID="btn_oayment_done" OnClick="btn_oayment_done_Click" Text="Awaiting Confirmation" CssClass="gradient-button" />
                    </li>
                    <li>&nbsp;</li>
                    <li>
                        <asp:Button runat="server" ID="btn_pending_bookings" OnClick="btn_pending_bookings_Click" Text="Pending Payment" CssClass="gradient-button" />
                    </li>
                </ul>
                <!--//crumbs-->
            </nav>

            <div style="padding: 5px">
                <br />
                <h3 id="h3_booking" runat="server">
                    <asp:Label ID="lblheader" runat="server" Text="View Bookings"></asp:Label>
                </h3>
                <hr />
                <asp:GridView ID="gvs" CssClass="table table-striped table-bordered align-items-center"
                    runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static">
                    <Columns>
                        <asp:TemplateField HeaderText="Villa Name" HeaderStyle-BackColor="#007bff">
                            <ItemTemplate>
                                <h5><%# Eval("Villa_name") %></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="First Name" HeaderStyle-BackColor="#007bff">
                            <ItemTemplate>
                                <h5><%# Eval("Client_fname") %></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Last Name" HeaderStyle-BackColor="#007bff">
                            <ItemTemplate>
                                <h5><%# Eval("Client_lname") %></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Booking Date" HeaderStyle-BackColor="#007bff">
                            <ItemTemplate>
                                <h5><%# Convert.ToDateTime(Eval("Bk_date")).ToString("dd-MMMM-yyyy") %></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Check-in Date" HeaderStyle-BackColor="#007bff">
                            <ItemTemplate>
                                <h5><%# Convert.ToDateTime(Eval("Bk_checkin")).ToString("dd-MMMM-yyyy")%></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Check-out Date" HeaderStyle-BackColor="#007bff" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <h5><%# Convert.ToDateTime(Eval("Bk_checkout")).ToString("dd-MMMM-yyyy") %></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Status" HeaderStyle-BackColor="#007bff" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <h5><%# Eval("Bk_state").ToString() == "C" ? "Confirmed" : "Pending" %></h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Reference" HeaderStyle-BackColor="#007bff" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <h5>
                                    <asp:HyperLink runat="server" NavigateUrl='<%# "Active_Booking_Details1.aspx?ID="+ Eval("Bk_id") %>' Text='<%# Eval("Bk_ref") %>'> </asp:HyperLink>
                                </h5>
                            </ItemTemplate>
                            <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                        </asp:TemplateField>

                        <asp:TemplateField HeaderText="Profile picture" HeaderStyle-BackColor="#007bff" HeaderStyle-Width="10%">
                            <ItemTemplate>
                                <asp:ImageButton ID="ImageButton1" runat="server"
                                    ImageUrl='<%# Eval("Client_profilepicture","~/images/{0}") %>'
                                    ControlStyle-Width="50"
                                    PostBackUrl='<%# Eval("Client_id", "view_client_profile.aspx?ID={0}") %>' />
                            </ItemTemplate>
                        </asp:TemplateField>

                    </Columns>
                </asp:GridView>

            </div>

        </div>
    </main>
</asp:Content>
