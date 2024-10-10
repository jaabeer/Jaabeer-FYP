<%@ Page Title="" Language="C#" MasterPageFile="~/Client1.Master" AutoEventWireup="true" CodeBehind="Villa_Details.aspx.cs" Inherits="DealProjectTamam.Villa_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <style>
        /* General Styles */
        body {
            font-family: Arial, sans-serif;
            background-color: #f4f4f4;
            margin: 0;
            padding: 0;
        }

        main.main {
            padding: 20px;
        }

        .wrap {
            max-width: 1200px;
            margin: 0 auto;
            padding: 20px;
            background-color: white;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        /* Button Styles */
        .gradient-button {
            background-color: #f97d09;
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            border-radius: 5px;
            transition: background-color 0.2s;
        }

        .gradient-button:hover {
            background-color: #ca5e07;
        }

        /* Image Gallery */
        .gallery ul {
            list-style: none;
            padding: 0;
            margin: 0;
            display: flex;
            overflow-x: scroll;
        }

        .gallery ul li {
            margin-right: 10px;
        }

        .gallery ul li img {
            max-width: 200px;
            height: auto;
            border-radius: 5px;
            cursor: pointer;
            transition: transform 0.2s;
        }

        .gallery ul li img:hover {
            transform: scale(1.1);
        }

        /* Tabs Navigation */
        .inner-nav ul {
            list-style: none;
            padding: 0;
            margin: 0;
            display: flex;
            border-bottom: 1px solid #ddd;
        }

        .inner-nav ul li {
            margin-right: 10px;
        }

        .inner-nav ul li a {
            text-decoration: none;
            color: #333;
            padding: 10px 15px;
            border-radius: 5px 5px 0 0;
            background-color: #f9f9f9;
            transition: background-color 0.2s;
        }

        .inner-nav ul li a:hover {
            background-color: #f97d09;
            color: white;
        }

        /* Active tab */
        .inner-nav ul li a.active {
            background-color: #f97d09;
            color: white;
        }

        /* Remove icons */
        .inner-nav ul li a:before {
            content: none !important;
        }

        .inner-nav ul li a:after {
            content: none !important;
        }

        /* Room Information */
        .room-information {
            margin-top: 10px;
            border: 1px solid #eee;
            padding: 10px;
            border-radius: 5px;
            background-color: #f9f9f9;
        }

        .room-information .row {
            margin-bottom: 5px;
            font-size: 14px;
        }

        .room-information .first {
            font-weight: bold;
            margin-right: 5px;
        }

        .room-information .second {
            color: #f97d09;
        }

        /* Review Section */
        .reviews {
            list-style: none;
            padding: 0;
            margin: 0;
        }

        .reviews li {
            margin-bottom: 20px;
            border: 1px solid #eee;
            padding: 10px;
            border-radius: 5px;
            background-color: #f9f9f9;
        }

        .reviews .left {
            margin-right: 10px;
        }

        .reviews .rev p {
            font-size: 14px;
        }

        /* Media Queries */
        @media (max-width: 768px) {
            .inner-nav ul {
                flex-direction: column;
            }

            .gallery ul {
                flex-direction: column;
            }
        }

        /* Custom Styles for Inputs */
        .edit_field input,
        .edit_field select {
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 5px;
            width: calc(100% - 22px);
            margin-bottom: 10px;
        }

        .edit_field a {
            text-decoration: none;
            color: #f97d09;
            margin-left: 10px;
        }

        .edit_field a:hover {
            text-decoration: underline;
        }

        .btn.btn-sm.btn-outline-secondary {
            background-color: #F97D09; /* Orange color */
            border-color: #F97D09; /* Ensure the border is the same color */
            color: white; /* White text color */
        }

        .btn.btn-sm.btn-outline-secondary:hover {
            background-color: #ca5e07; /* Darker shade for hover */
            border-color: #ca5e07;
        }

    </style>

    <main class="main">
        <div class="wrap">
            <div class="row">
                <section class="three-fourth">
                    <!--gallery-->
                    <div class="gallery">
                        <ul id="image-gallery" class="cS-hidden">
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <li data-thumb="">
                                        <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/" + Eval("Img_name") %>' alt="" />
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </div>
                    <!--//gallery-->

                    <!--inner navigation-->
                    <nav class="inner-nav">
                        <ul>
                            <li class="availability"><a href="#availability" class="active" title="Availability">Availability</a></li>
                            <li class="description"><a href="#description" title="Description">Description</a></li>
                            <li class="facilities"><a href="#facilities" title="Facilities">Facilities</a></li>
                            <li class="location"><a href="#location" title="Location">Location</a></li>
                            <li class="reviews"><a href="#reviews" title="Reviews">Reviews</a></li>
                        </ul>
                    </nav>
                    <!--//inner navigation-->

                    <!--availability-->
                    <br />
                    <br />
                    <br />
                    <section id="availability" class="tab-content">
                        <article>
                            <h2>Villa Availability</h2>
                            <div class="text-wrap">
                                <a href="#field1" class="gradient-button edit right" title="Change dates">Change dates</a>
                                <p><asp:Label ID="lblbooking" runat="server" Text=""></asp:Label></p>
                                <div class="edit_field" id="field1">
                                    <label for="new_name">Your Booking Dates:</label>
                                    <table border="0" style="border-block: none">
                                        <tr>
                                            <td><asp:TextBox ID="txtcheckin" runat="server" TextMode="Date"></asp:TextBox></td>
                                            <td><asp:TextBox ID="txtcheckout" runat="server" TextMode="Date"></asp:TextBox></td>
                                        </tr>
                                    </table>
                                    <asp:Button ID="btnsave" CssClass="gradient-button" runat="server" Text="Save" OnClick="btnsave_Click" />
                                    <a href="#">Cancel</a>
                                </div>
                                <!--//edit fields-->
                            </div>

                            <h2>Booking Type Available</h2>
                            <ul class="room-types">
                                <!--room-->
                                <li>
                                    <figure class="left" id="gallery1">
                                        <asp:Image ID="villa_img" runat="server" alt="" />
                                    </figure>
                                    <div class="meta">
                                        <h3>Booking Per Day</h3>
                                        <p>Prices are per day and VAT Included in price</p>
                                    </div>
                                    <div class="room-information">
                                        <div class="row">
                                            <span class="first">Min:</span>
                                            <span class="second">1 day</span>
                                        </div>
                                        <div class="row">
                                            <span class="first">Price:</span>
                                            <span class="second">Rs.<asp:Label ID="lbl_day" runat="server"></asp:Label></span>
                                        </div>
                                        <asp:Button ID="btn_daily" runat="server" Text="Book now" class="gradient-button" OnClick="btn_booking_Click" />
                                    </div>
                                </li>
                                <!--//room-->

                                <!--room-->
                                <li>
                                    <figure class="left" id="gallery2">
                                        <asp:Image ID="Image2" runat="server" alt="" />
                                    </figure>
                                    <div class="meta">
                                        <h3>Booking Per Week</h3>
                                        <p>Prices are per Week and VAT Included in price</p>
                                    </div>
                                    <div class="room-information">
                                        <div class="row">
                                            <span class="first">Min:</span>
                                            <span class="second">7 days</span>
                                        </div>
                                        <div class="row">
                                            <span class="first">Price:</span>
                                            <span class="second">Rs.<asp:Label ID="lbl_week" runat="server"></asp:Label></span>
                                        </div>
                                        <asp:Button ID="btnbooking_week" runat="server" Text="Book now" class="gradient-button" OnClick="btn_bookingweek_Click" />
                                    </div>
                                </li>
                                <!--//room-->

                                <!--room-->
                                <li>
                                    <figure class="left" id="gallery3">
                                        <asp:Image ID="Image3" runat="server" alt="" />
                                    </figure>
                                    <div class="meta">
                                        <h3>Booking Per Month</h3>
                                        <p>Prices are per month and VAT Included in price</p>
                                    </div>
                                    <div class="room-information">
                                        <div class="row">
                                            <span class="first">Min:</span>
                                            <span class="second">28 days</span>
                                        </div>
                                        <div class="row">
                                            <span class="first">Price:</span>
                                            <span class="second">Rs.<asp:Label ID="lbl_month" runat="server"></asp:Label></span>
                                        </div>
                                        <asp:Button ID="btnbookingmonth" runat="server" Text="Book now" class="gradient-button" OnClick="btn_bookingmonth_Click" />
                                    </div>
                                </li>
                                <!--//room-->
                            </ul>
                        </article>
                    </section>
                    <!--//availability-->

                    <!--description-->
                    <section id="description" class="tab-content">
                        <article>
                            <h2>General</h2>
                            <div class="text-wrap">
                                <p><asp:Label ID="lbl_desc" runat="server" Text=""></asp:Label></p>
                            </div>

                            <h2>Check-in</h2>
                            <div class="text-wrap">
                                <p>From 10:00 hours </p>
                            </div>

                            <h2>Check-out</h2>
                            <div class="text-wrap">
                                <p>Until 11:59 hours </p>
                            </div>

                          <%--  <h2>Cancellation / Prepayment</h2>
                            <div class="text-wrap">
                                <p>Cancellation and prepayment policies vary according to room type. Please check the <a href="#">conditions</a> when selecting your villa. </p>
                            </div>--%>

                            

                          <%--  <h2>Payment Method</h2>--%>
                            <div class="text-wrap">
                                <p></p>
                            </div>
                        </article>
                    </section>
                    <!--//description-->

                    <!--facilities-->
                    <section id="facilities" class="tab-content">
                        <article>
                            <h2>Facilities</h2>
                            <div class="text-wrap">
                                <ul class="three-col">
                                    <asp:Repeater ID="Repeater2" runat="server">
                                        <ItemTemplate>
                                            <li><%# Eval("Fac_name")%> </li>
                                        </ItemTemplate>
                                    </asp:Repeater>
                                </ul>
                            </div>

                          

                            <h2>Internet</h2>
                            <div class="text-wrap">
                                <p><asp:Label ID="lblwifi" runat="server" Text="Label"></asp:Label></p>
                            </div>

                            <h2>Parking</h2>
                            <div class="text-wrap">
                                <p><asp:Label ID="lblparking" runat="server" Text="Label"></asp:Label></p>
                            </div>
                        </article>
                    </section>
                    <!--//facilities-->

                    <!--location-->
                    <section id="location" class="tab-content">
                        <article>
                            <!--map-->
                            <div class="gmap" id="map_canvas">
                                <iframe
                                    id="gmap" runat="server"
                                    width="100%"
                                    height="100%"
                                    style="border: 0"
                                    loading="lazy"
                                    referrerpolicy="no-referrer-when-downgrade"></iframe>
                            </div>
                            <!--//map-->
                        </article>
                    </section>
                    <!--//location-->

                    <!--reviews-->
                    <section id="reviews" class="tab-content">
                        <article>
                            <h2>Villa Rating and Reviews</h2>
                            <div class="score">
                                <span class="achieved"><asp:Label ID="lblrating" runat="server" Text=""></asp:Label></span>
                                <span>/ 5</span>
                                <p class="info">Based on 1000 reviews</p>
                            </div>
                            <div>
                                <p class="disclaimer">reviews are written by our customers 
                                    <b><i><asp:Label ID="lblpropname" runat="server" Text="" Font-Size="Small"></asp:Label></i></b>.
                                </p>
                                <asp:Label ID="lblreview" runat="server" Text=""></asp:Label>
                                <a href="#addreview" class="gradient-button edit" runat="server" id="btnrev">Add Reviews</a>

                                <div class="edit_field" id="addreview">
                                    <asp:Label ID="lblrating_error" runat="server" Text=""></asp:Label> <br />
                                    <label for="new_name">Positive Review</label>
                                    <asp:TextBox class="form-control" ID="txtpos" runat="server" BorderStyle="Solid"></asp:TextBox>
                                    <br />
                                    <label for="new_name">Negative Review</label>
                                    <asp:TextBox class="form-control" ID="txtneg" runat="server" BorderStyle="Solid"></asp:TextBox>
                                    <br />
                                    <label for="new_name">Rating</label>
                                    <asp:DropDownList ID="ddlrating" runat="server"></asp:DropDownList>
                                    <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br /> <br />
                                    <asp:Button ID="btnreview" CssClass="gradient-button" runat="server" OnClick="btnreview_Click" Text="Save" />
                                    <br /> <br />
                                    <asp:Button ID="btnreview_update" CssClass="gradient-button" runat="server" OnClick="btnreview_update_Click" Text="Update" />
                                    <a href="#">Cancel</a>
                                </div>
                            </div>
                        </article>

                        <article>
                            <h2>Guest reviews</h2>
                            <ul class="reviews">
                                <asp:Repeater ID="Repeater4" runat="server">
                                    <ItemTemplate>
                                        <li>
                                            <figure class="left">
                                                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Client/images/profile/" + Eval("Client_profilepicture") %>' Width="70px" Height="70px" />
                                                <address><span><%# Eval("Client_fname") %> <%# Eval("Client_lname") %></span><br />
                                                    <b>Rating: <span id="stars"></span></b>
                                                    <asp:Label ID="lblstar" runat="server" Text='<%# Eval("Rating") %>'></asp:Label> / 5 <br />
                                                    <%# Eval("Review_Date") %>
                                                </address>
                                            </figure>
                                            <div class="rev pro">
                                                <p><%# Eval("Positive_Review") %></p>
                                            </div>
                                            <div class="rev con">
                                                <p><%# Eval("Negative_Review") %></p>
                                            </div>
                                        </li>
                                    </ItemTemplate>
                                </asp:Repeater>
                            </ul>
                        </article>
                    </section>
                    <!--//reviews-->
                </section>
                <!--//hotel content-->
            </div>
        </div>

        <script type="text/javascript">
            $(document).ready(function () {
                $('#image-gallery').lightSlider({
                    gallery: true,
                    item: 1,
                    thumbItem: 6,
                    slideMargin: 0,
                    speed: 500,
                    auto: true,
                    loop: true,
                    onSliderLoad: function () {
                        $('#image-gallery').removeClass('cS-hidden');
                    }
                });

                $('#gallery1,#gallery2,#gallery3,#gallery4').lightGallery({
                    download: false
                });
            });

            document.querySelectorAll('a[href^="#"]').forEach(anchor => {
                anchor.addEventListener('click', function (e) {
                    e.preventDefault();

                    document.querySelector(this.getAttribute('href')).scrollIntoView({
                        behavior: 'smooth'
                    });
                });
            });

            var stars = document.getElementById('lblstar').textContent;
            document.getElementById("stars").innerHTML = getStars(stars);

            function getStars(rating) {
                rating = Math.round(rating * 2) / 2;
                let output = [];

                for (var i = rating; i >= 1; i--)
                    output.push('<i class="fa fa-star" aria-hidden="true" style="color: gold;"></i>&nbsp;');

                if (i == .5) output.push('<i class="fa fa-star-half-o" aria-hidden="true" style="color: gold;"></i>&nbsp;');

                for (let i = (5 - rating); i >= 1; i--)
                    output.push('<i class="fa fa-star-o" aria-hidden="true" style="color: gold;"></i>&nbsp;');

                return output.join('');
            }
        </script>
    </main>
</asp:Content>
