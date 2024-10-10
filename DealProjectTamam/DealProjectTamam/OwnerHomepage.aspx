<%@ Page Title="" Language="C#" MasterPageFile="~/OwnerHome.Master" AutoEventWireup="true" CodeBehind="OwnerHomepage.aspx.cs" Inherits="DealProjectTamam.OwnerHomepage" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
     <!-- Hero Section Begin -->
 <section class="hero-section">
     <div class="container">
         <div class="row">
             <div class="col-lg-6">
                 <div class="hero-text">
                     <h1>Deal Tamam</h1>
                     <p>Here are the best hotel and villa booking sites, for finding low-priced rooms.
                    </p>
                    <%-- <a href="#" class="primary-btn">Discover Now</a>--%>
                 </div>
             </div>
            
         </div>
     </div>
     <div class="hero-slider owl-carousel">
         <div class="hs-item set-bg" data-setbg="img/hero/hero-1.jpg"></div>
         <div class="hs-item set-bg" data-setbg="img/hero/hero-2.jpg"></div>
         <div class="hs-item set-bg" data-setbg="img/hero/hero-3.jpg"></div>
     </div>
 </section>
 <!-- Hero Section End -->

<section class="aboutus-section spad">
    <div class="container">
        <div class="row">
            <div class="col-lg-6">
                <div class="about-text">
                    <div class="section-title">
                        <span>Why Book with us</span>
                        <h4>We are always here<br />Feel free to reach out to us via phone or email at any time. Receive around-the-clock assistance before, during, and after your journey.</h4>
                    </div>
                   <%-- <p class="f-para">Low Rates</p>
                    <p class="s-para">Take advantage of our low rates, backed by our price guarantee. No booking fees mean more savings for you!</p>
                    <a href="#" class="primary-btn about-btn">Learn More</a>--%>
                </div>
            </div>
            <div class="col-lg-6">
                <div class="about-pic">
                    <div class="row">
                        <div class="col-sm-6">
                            <img src="img/about/about.jpg" alt="">
                        </div>
                        <div class="col-sm-6">
                            <img src="img/about/about2.jpg" alt="">
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>

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
                            <asp:Image ID="imgVilla" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' CssClass="img-fluid"  Width="300px" Height="250px"/>
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
                              <%--  <a href="#" class="primary-btn">More Details</a>--%>
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
                    <h2>Our Villa based on rating</h2>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="rptVillaRating" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-6">
                        <div class="hp-room-item">
                            <asp:Image ID="imgVillaRating" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' CssClass="img-fluid"  Width="300px" Height="250px" />
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
                               <%-- <a href="#" class="primary-btn">More Details</a>--%>
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
                    <h2>Our Latest Hotel</h2>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="rptLatestHotel" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-6">
                        <div class="hp-room-item">
                            <asp:Image ID="imgHotel" runat="server" ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/main/" + Eval("Hotel_image") %>' CssClass="img-fluid"  Width="300px" Height="250px"/>
                            <div class="hr-text">
                                <h3><%# Eval("Hotel_name") %></h3>
                                <h2>Rs <%# Eval("Hotel_price") %><span>/Per Day</span></h2>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td class="r-o">Town:</td>
                                            <td><%# Eval("Hotel_town") %></td>
                                        </tr>
                                        
                                    </tbody>
                                </table>
                              <%--  <a href="#" class="primary-btn">More Details</a>--%>
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
                    <h2>Our Hotel based on rating</h2>
                </div>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="rptHotelRating" runat="server">
                <ItemTemplate>
                    <div class="col-lg-3 col-md-6">
                        <div class="hp-room-item">
                            <asp:Image ID="imgHotelRating" runat="server" ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/main/" + Eval("Hotel_image") %>' CssClass="img-fluid"  Width="300px" Height="250px" />
                            <div class="hr-text">
                                <h3><%# Eval("Hotel_name") %></h3>
                                <h3 style="color: orangered;">Rating <%# Eval("Hotel_rating") %></h3>
                                <h2>Rs <%# Eval("Hotel_price") %><span>/Per Day</span></h2>
                                <table>
                                    <tbody>
                                        <tr>
                                            <td class="r-o">Town:</td>
                                            <td><%# Eval("Hotel_town") %></td>
                                        </tr>
                                        
                                    </tbody>
                                </table>
                             <%--   <a href="#" class="primary-btn">More Details</a>--%>
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</section>

<section class="testimonial-section spad">
    <div class="container">
        <div class="row">
            <div class="col-lg-12">
                <div class="section-title">
                    <span>Testimonials</span>
                    <h2>What Customers Say?</h2>
                </div>
            </div>
        </div>
        <div class="row">
            <div class="col-lg-8 offset-lg-2">
                <div class="testimonial-slider owl-carousel">
                    <div class="ts-item">
                        <p>"Planning my travels is like having a personal concierge who knows all the hidden gems. Each recommendation feels tailor-made, turning every trip into a seamless adventure.".</p>
                        <div class="ti-author">
                            <div class="rating">
                                <i class="icon_star"></i>
                                <i class="icon_star"></i>
                                <i class="icon_star"></i>
                                <i class="icon_star"></i>
                                <i class="icon_star-half_alt"></i>
                            </div>
                            <h5>Mrs Hayla Smith</h5>
                        </div>
                        <img src="img/" alt="">
                    </div>
                    <div class="ts-item">
                        <p>"Exploring the world just got easier! This tourism website doesn't just offer trips, it crafts unforgettable experiences. It's like a passport to adventure, and every click is a step towards a new horizon."</p>
                        <div class="ti-author">
                            <div class="rating">
                                <i class="icon_star"></i>
                                <i class="icon_star"></i>
                                <i class="icon_star"></i>
                                <i class="icon_star"></i>
                                <i class="icon_star-half_alt"></i>
                            </div>
                            <h5>Mr John Doe</h5>
                        </div>
                        <img src="img/" alt="">
                    </div>
                </div>
            </div>
        </div>
    </div>
</section>



</asp:Content>
