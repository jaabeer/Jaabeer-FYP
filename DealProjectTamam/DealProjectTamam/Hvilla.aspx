<%@ Page Title="" Language="C#" MasterPageFile="~/DealTamam.Master" AutoEventWireup="true" CodeBehind="Hvilla.aspx.cs" Inherits="DealProjectTamam.Hvilla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        .hp-room-item {
            position: relative;
            overflow: hidden;
            margin-bottom: 30px;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            transition: all 0.3s ease;
        }
        .hp-room-item:hover {
            box-shadow: 0 8px 16px rgba(0, 0, 0, 0.2);
        }
        .hp-room-item img {
            width: 100%;
            height: auto;
            display: block;
        }
        .hr-text {
            padding: 20px;
            background: #fff;
        }
        .hr-text h3 {
            margin-bottom: 10px;
            font-size: 20px;
            color: #333;
        }
        .hr-text h2 {
            font-size: 24px;
            color: #ff6b6b;
            margin-bottom: 10px;
        }
        .hr-text table {
            width: 100%;
            margin-bottom: 10px;
        }
        .hr-text table td {
            padding: 5px 0;
        }
        .primary-btn {
            display: inline-block;
            padding: 10px 20px;
            background: #F0B27A;
            color: #fff;
            border-radius: 5px;
            text-decoration: none;
            transition: all 0.3s ease;
        }
        .primary-btn:hover {
            background: #e65b5b;
        }
    </style>
    <section class="services-section spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title">
                        <span>Explore</span>
                        <h2>Our Latest Villa</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="rptLatestVilla" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-6">
                            <div class="hp-room-item">
                                <asp:Image ID="imgVilla" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' CssClass="img-fluid" Width="300px" Height="250px"/>
                                <div class="hr-text">
                                    <h3><%# Eval("Villa_name") %></h3>
                                    <h2>Rs <%# Eval("Villa_priceday") %><span>/Per Day</span></h2>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="r-o">Town:</td>
                                                <td><%# Eval("Villa_town") %></td>
                                            </tr>
                                            <tr>
                                                <td class="r-o">Weekly Price:</td>
                                                <td>Rs <%# Eval("Villa_priceweek") %></td>
                                            </tr>
                                            <tr>
                                                <td class="r-o">Monthly Price:</td>
                                                <td>Rs <%# Eval("Villa_pricemonth") %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:LinkButton ID="btnMoreDetails" runat="server" CssClass="primary-btn" OnClick="btnMoreDetails_Click">Book Now</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
    <section class="services-section spad">
        <div class="container">
            <div class="row">
                <div class="col-lg-12">
                    <div class="section-title">
                        <span>Explore</span>
                        <h2>Our Villa Based on Rating</h2>
                    </div>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="rptVillaRating" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-6">
                            <div class="hp-room-item">
                                <asp:Image ID="imgVillaRating" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' CssClass="img-fluid" Width="300px" Height="250px"/>
                                <div class="hr-text">
                                    <h3><%# Eval("Villa_name") %></h3>
                                    <h3 style="color: orangered;">Rating <%# Eval("Villa_rating") %></h3>
                                    <h2>Rs <%# Eval("Villa_priceday") %><span>/Per Day</span></h2>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="r-o">Town:</td>
                                                <td><%# Eval("Villa_town") %></td>
                                            </tr>
                                            <tr>
                                                <td class="r-o">Weekly Price:</td>
                                                <td>Rs <%# Eval("Villa_priceweek") %></td>
                                            </tr>
                                            <tr>
                                                <td class="r-o">Monthly Price:</td>
                                                <td>Rs <%# Eval("Villa_pricemonth") %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:LinkButton ID="btnMoreDetailsRating" runat="server" CssClass="primary-btn" OnClick="btnMoreDetails_Click">Book Now</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
    <section class="services-section spad">
        <div class="container">
            <div the="col-lg-12">
                <div class="section-title">
                    <span>Explore</span>
                    <h2>Our Villa Based on Price</h2>
                </div>
            </div>
            <div class="row">
                <asp:Repeater ID="rptVillaPrice" runat="server">
                    <ItemTemplate>
                        <div class="col-lg-3 col-md-6">
                            <div class="hp-room-item">
                                <asp:Image ID="imgVillaPrice" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' CssClass="img-fluid" Width="300px" Height="250px"/>
                                <div class="hr-text">
                                    <h3><%# Eval("Villa_name") %></h3>
                                    <h2>Rs <%# Eval("Villa_priceday") %><span>/Per Day</span></h2>
                                    <table>
                                        <tbody>
                                            <tr>
                                                <td class="r-o">Town:</td>
                                                <td><%# Eval("Villa_town") %></td>
                                            </tr>
                                            <tr>
                                                <td class="r-o">Weekly Price:</td>
                                                <td>Rs <%# Eval("Villa_priceweek") %></td>
                                            </tr>
                                            <tr>
                                                <td class="r-o">Monthly Price:</td>
                                                <td>Rs <%# Eval("Villa_pricemonth") %></td>
                                            </tr>
                                        </tbody>
                                    </table>
                                    <asp:LinkButton ID="btnMoreDetailsPrice" runat="server" CssClass="primary-btn" OnClick="btnMoreDetails_Click">Book Now</asp:LinkButton>
                                </div>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </div>
        </div>
    </section>
</asp:Content>
