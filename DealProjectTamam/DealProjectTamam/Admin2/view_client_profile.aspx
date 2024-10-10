<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="view_client_profile.aspx.cs" Inherits="DealProjectTamam.Admin2.view_client_profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style type="text/css">
        .content {
            max-width: 700px;
            margin: auto;
            padding: 20px;
            border: solid 1px #ddd;
            background-color: #f9f9f9;
            font-family: 'Segoe UI', sans-serif;
        }

        .card {
            margin: 20px 0;
            background-color: #fff;
            border: 1px solid #ddd;
            border-radius: 5px;
            box-shadow: 0 0 10px rgba(0,0,0,0.1);
        }

        .card-body {
            padding: 20px;
        }

        .card-title {
            margin-bottom: 10px;
            font-size: 24px;
            color: #333;
        }

        .card-text {
            margin: 10px 0;
            color: #555;
        }

        .box2 {
            margin-top: 20px;
            text-align: center;
        }

        .card-img-top {
            width: 250px;
            height: 300px;
            border-radius: 10px;
            object-fit: cover;
            display: block;
            margin: 10px auto;
            padding: 5px;
            border: 1px solid #ddd;
        }

        .btn-custom {
            background-color: #4CAF50;
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            text-decoration: none;
            font-size: 16px;
        }

        .btn-custom:hover {
            background-color: #45a049;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4>Client Profile</h4>
    <hr />
    <div class="content">
        <asp:DataList ID="dlstClientDetails" RepeatColumns="1" runat="server">
            <ItemTemplate>
                <div class="card">
                    <asp:Image runat="server" ImageUrl='<%# Eval("Client_profilepicture", "~/images/{0}")%>' ID="Image1" CssClass="card-img-top" />
                    <div class="card-body">
                        <h2 class="card-title"><%# Eval("Client_fname") + " " + Eval("Client_lname")%></h2>
                        <p class="card-text"><strong>Name: </strong><span class="text-dark"><%# Eval("Client_fname")%></span></p>
                        <p class="card-text"><strong>National Identity Card Number: </strong><span class="text-dark"><%# Eval("Client_nic")%></span></p>
                        <p class="card-text"><strong>Date of Birth: </strong><span class="text-dark"><%# Eval("Client_dob", "{0:dd/MM/yyyy}")%></span></p>
                        <p class="card-text"><strong>Gender: </strong><span class="text-dark"><%# Eval("Gender")%></span></p>
                        <p class="card-text"><strong>Address: </strong><span class="text-dark"><%# Eval("Client_address")%></span></p>
                        <p class="card-text"><strong>Mobile number: </strong><span class="text-dark"><%# Eval("Client_contact")%></span></p>
                        <p class="card-text"><strong>Email: </strong><span class="text-dark"><%# Eval("Client_email")%></span></p>
                    </div>
                    <div class="box2">
                        <asp:LinkButton ID="lnk_client_profile" runat="server" Text="Back" OnClientClick="JavaScript:window.history.back(1); return false;" CssClass="btn-custom"></asp:LinkButton>
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>
