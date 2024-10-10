<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="MyHotel.aspx.cs" Inherits="DealProjectTamam.MyHotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        .card-img-top {
            height: 200px;
            object-fit: cover;
        }
        .btn-custom {
            background-color: #ff4081;
            color: white;
        }
        .btn-custom:hover {
            background-color: #e6004c;
            color: white;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <div class="col-md-12 text-center mb-4">
                <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
                <h1 class="display-4">My Owned Hotels</h1>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="col-md-6 mb-4">
                        <div class="card h-100 shadow-sm">
                            <asp:Image ID="Image1" runat="server" CssClass="card-img-top" ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/main/" + Eval("Hotel_image") %>' />
                            <div class="card-body">
                                <h5 class="card-title font-weight-bold"><%# Eval("Hotel_name") %></h5>
                                <p class="card-text text-muted"><%# Eval("Hotel_town") %></p>
                                <p class="card-text"><small class="text-muted">Rating: <%# Eval("Hotel_Rating") %>/5</small></p>
                                <p class="card-text">Price per Day from <strong>Rs. <%# Eval("Hotel_price") %></strong></p>
                                <p class="card-text text-truncate"><%# Eval("Hotel_desc") %></p>
                                <asp:LinkButton ID="lbtnview_booking" Text="View Booking" runat="server" PostBackUrl='<%# "View_booking.aspx?ID=" + Eval("Hotel_id") %>' CssClass="btn btn-link"></asp:LinkButton>
                                <asp:TextBox ID="txt_booking" runat="server" Visible="false" Text='<%# Eval("Bk_id") %>'></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" PostBackUrl='<%# "Modify_Hotel.aspx?ID=" + Eval("Hotel_id") %>' Text="Modify" CssClass="btn btn-custom" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
       
</asp:Content>
