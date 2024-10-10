<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="MyVilla.aspx.cs" Inherits="DealProjectTamam.MyVilla" %>
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
                <h1 class="display-4">My Owned Villas</h1>
            </div>
        </div>
        <div class="row">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="col-md-6 mb-4">
                        <div class="card h-100 shadow-sm">
                            <asp:Image ID="Image1" runat="server" CssClass="card-img-top" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' />
                            <div class="card-body">
                                <h5 class="card-title font-weight-bold"><%# Eval("Villa_name") %></h5>
                                <p class="card-text text-muted"><%# Eval("Villa_town") %></p>
                                <p class="card-text"><small class="text-muted">Rating: <%# Eval("Villa_Rating") %>/5</small></p>
                                <p class="card-text">Price per Day from <strong>Rs. <%# Eval("Villa_priceday") %></strong></p>
                                <p class="card-text text-truncate"><%# Eval("Villa_desc") %></p>
                                <asp:LinkButton ID="lbtnview_booking" Text="" runat="server" PostBackUrl='<%# "" + Eval("Villa_id") %>' CssClass="btn btn-link"></asp:LinkButton>
                                <asp:TextBox ID="txt_booking" runat="server" Visible="false" Text='<%# Eval("Bk_id") %>'></asp:TextBox>
                                <asp:Button ID="Button1" runat="server" PostBackUrl='<%# "Modify_Villa.aspx?ID=" + Eval("Villa_id") %>' Text="Modify" CssClass="btn btn-custom" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
       
</asp:Content>
