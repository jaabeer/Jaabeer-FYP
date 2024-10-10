<%@ Page Title="" Language="C#" MasterPageFile="~/Client1.master" AutoEventWireup="true" CodeBehind="ClientHotel.aspx.cs" Inherits="DealProjectTamam.ClientHotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        /* Basic Reset */
        body, h1, h2, h3, h4, p, figure, figcaption {
            margin: 0;
            padding: 0;
            font-family: 'Segoe UI', Arial, sans-serif;
        }

        /* Grid and Layout */
        .row { display: flex; flex-wrap: wrap; }
        .one-fourth { width: 25%; padding: 20px; }
        .three-fourth { width: 75%; padding: 20px; }

        /* Styling Sidebar */
        .widget {
            background-color: #f3f3f3;
            border: 1px solid #ddd;
            padding: 15px;
            margin-bottom: 20px;
        }
        .widget h4 {
            font-size: 18px;
            margin-bottom: 10px;
            color: #333;
        }
        .checkbox label {
            margin-left: 5px;
        }

        /* Main Content Styling */
        .sort-by {
            padding: 10px;
            background-color: #e9ecef;
            margin-bottom: 20px;
        }
        .sort-by h3 {
            color: #007bff;
        }
        .sort {
            padding: 0;
            list-style: none;
        }
        .sort li {
            display: inline-block;
            margin-right: 15px;
        }

        /* Repeater Styles */
        .full-width {
            border: 1px solid #ddd;
            margin-bottom: 20px;
            overflow: hidden;
            display: flex;
        }
        figure {
            margin-right: 15px;
        }
        .details {
            padding: 10px;
            flex: 1;
        }
        .details h3 {
            color: #007bff;
            margin-bottom: 5px;
        }
        .gradient-button {
            background-color: #007bff;
            color: white;
            border: none;
            padding: 10px 15px;
            cursor: pointer;
        }
        .gradient-button:hover {
            background-color: #0056b3;
        }

        
     /* Basic styles reset */
    body, h1, h2, h3, h4, p, figure, figcaption {
        margin: 0;
        padding: 0;
        font-family: 'Segoe UI', Arial, sans-serif;
    }

    /* Container for repeater items */
    #Repeater1 {
        display: flex;
        flex-wrap: wrap;
        justify-content: space-between; /* Adjusts space between the cards */
    }

    /* Card styles */
    .hotel-card {
        display: flex;
        width: 48%; /* Adjust width for responsive spacing */
        margin-bottom: 20px;
        align-items: center;
        border: 1px solid #ccc;
        border-radius: 10px;
        overflow: hidden;
        box-shadow: 0 4px 8px rgba(0,0,0,0.1);
        transition: transform 0.3s ease, box-shadow 0.3s ease;
    }

    .hotel-card:hover {
        transform: translateY(-5px);
        box-shadow: 0 6px 16px rgba(0,0,0,0.2);
    }

    .hotel-image .hotel-img {
        width: 200px;
        height: 150px;
        object-fit: cover;
        border-radius: 10px 0 0 10px;
    }

    .hotel-info {
        padding: 15px;
        flex-grow: 1;
        background: #f9f9f9;
    }

    .hotel-info h3 {
        margin: 0;
        color: #333;
        font-size: 18px;
    }

    .hotel-location, .hotel-price {
        color: #666;
        font-size: 14px;
        margin: 5px 0;
    }

    .info-btn {
        padding: 8px 16px;
        background-color: #007bff;
        color: white;
        border: none;
        border-radius: 5px;
        cursor: pointer;
        transition: background-color 0.2s ease;
    }

    .info-btn:hover {
        background-color: #0056b3;
    }

    .sort-by {
    padding: 10px;
    background-color: #f9f9f9;
    border-radius: 5px;
    box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
}

.sort-by h3 {
    margin-bottom: 10px;
    color: #333;
    font-size: 18px;
}

.sorting-options {
    display: flex;
    justify-content: space-around;
}

.sorting-option {
    display: flex;
    align-items: center;
}

.sorting-option span {
    margin-right: 5px;
    font-weight: bold;
    color: #555;
}

.sort-button {
    background: none;
    border: none;
    color: #007bff;
    font-size: 16px;
    padding: 5px 8px;
    cursor: pointer;
    transition: color 0.3s ease;
}

.sort-button.asc {
    margin-right: 2px;
}

.sort-button:hover {
    color: #0056b3;
}

.desc {
    color: #d9534f;
}

.desc:hover {
    color: #c9302c;
}

/* Button Styles */
.gradient-button, .info-btn, .sort-button {
    background-color: #F97D09; /* Main button color */
    color: white;
    border: none;
    padding: 10px 15px;
    cursor: pointer;
    border-radius: 5px;
    transition: background-color 0.2s;
}

.gradient-button:hover, .info-btn:hover, .sort-button:hover {
    background-color: #ca5e07; /* Darker shade for hover */
}

.sort-button {
    background: none; /* Remove background for sort buttons */
    color: #F97D09; /* Ensure text color matches other buttons */
    padding: 5px 8px;
}

.sort-button.asc, .sort-button.desc {
    background-color: transparent; /* Adjust for specific sort button style */
}

.sort-button.desc:hover {
    color: #ca5e07; /* Adjust hover color for descending sort */
}

/* Additional styling to maintain visual consistency */
.sorting-option span {
    color: #555; /* Label color for sorting options */
}

/* Styling for specific sorting directions */
.sort-button.asc {
    margin-right: 2px;
}
/* Logout Button Styling */
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="row">
        <aside class="one-fourth">
            <article class="widget refine-search-results">
                <h4>search results</h4>
                <dl>
                    <dt>Price (per day)</dt>
                    <dd>
                        <div class="checkbox">
                            <asp:CheckBox ID="ch_price_per_day1" runat="server" />
                            <label for="ch_price_per_day1">Rs:1000-1500</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="ch_price_per_day2" runat="server" />
                            <label for="ch_price_per_day2">Rs: 1500-3000</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="ch_price_per_day3" runat="server" />
                            <label for="ch_price_per_day3">Rs: 3000-5000</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="ch_price_per_day4" runat="server" />
                            <label for="ch_price_per_day4">Rs:5000+</label>
                        </div>
                    </dd>
                    <dt>Location</dt>
                    <dd>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_region_1" runat="server" />
                            <label for="ch_north">Center</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_region_2" runat="server" />
                            <label for="ch_north">East</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_region_3" runat="server" />
                            <label for="ch_north">North</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_region_4" runat="server" />
                            <label for="ch_north">South</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_region_5" runat="server" />
                            <label for="ch_north">West</label>
                        </div>
                    </dd>
                    <dt>Facilities</dt>
                    <dd>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_1" runat="server" />
                            <label for="ch_north">AC</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_2" runat="server" />
                            <label for="ch_north">BBQ Area</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_3" runat="server" />
                            <label for="ch_north">Fitness Center</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_4" runat="server" />
                            <label for="ch_north">Kids Playground</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_5" runat="server" />
                            <label for="ch_north">Parking</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_6" runat="server" />
                            <label for="ch_north">Room Service</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_7" runat="server" />
                            <label for="ch_north">Spa</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_8" runat="server" />
                            <label for="ch_north">Swimming pool</label>
                        </div>
                        <div class="checkbox">
                            <asp:CheckBox ID="Ch_fac_9" runat="server" />
                            <label for="ch_north">Wifi</label>
                        </div>
                    </dd>
                </dl>
                <asp:Button ID="btn_filter" runat="server" Text="Search Filter" OnClick="btn_filter_Click" CssClass="gradient-button" />
            </article>
        </aside>

        <div class="three-fourth">
           <div class="sort-by">
    <h3>Sort by:</h3>
    <div class="sorting-options">
        <div class="sorting-option">
            <span>New Hotel</span>
            <asp:LinkButton ID="regis_asc" CssClass="sort-button asc" OnClick="regis_asc_Click" runat="server" Text="▲" />
            <asp:LinkButton ID="regis_desc" CssClass="sort-button desc" OnClick="regis_desc_Click" runat="server" Text="▼" />
        </div>
        <div class="sorting-option">
            <span>Price</span>
            <asp:LinkButton ID="price_asc" CssClass="sort-button asc" OnClick="price_asc_Click" runat="server" Text="▲" />
            <asp:LinkButton ID="price_desc" CssClass="sort-button desc" OnClick="price_desc_Click" runat="server" Text="▼" />
        </div>
        <div class="sorting-option">
            <span>Rating</span>
            <asp:LinkButton ID="rat_asc" CssClass="sort-button asc" OnClick="rat_asc_Click" runat="server" Text="▲" />
            <asp:LinkButton ID="rat_desc" CssClass="sort-button desc" OnClick="rat_desc_Click" runat="server" Text="▼" />
        </div>
    </div>
</div>

            <div class="row deals results">
          <asp:Repeater ID="Repeater1" runat="server">
    <ItemTemplate>
        <div class="hotel-card">
            <div class="hotel-image">
                <asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/main/" + Eval("Hotel_image") %>' CssClass="hotel-img" />
            </div>
            <div class="hotel-info">
                <h3><%# Eval("Hotel_name") %></h3>
                <p class="hotel-location"><%# Eval("Hotel_town") %>, Rating: <%# Eval("Hotel_Rating") %>/5</p>
                <p class="hotel-price">From Rs. <%# Eval("Hotel_price") %>/day</p>
                <asp:Button ID="Button1" runat="server" CssClass="info-btn" PostBackUrl='<%# "Hotel_details.aspx?Parameter="+ Eval("Hotel_id") %>' Text="More Info" />
            </div>
        </div>
    </ItemTemplate>
</asp:Repeater>



            </div>
        </div>
    </div>
</asp:Content>
