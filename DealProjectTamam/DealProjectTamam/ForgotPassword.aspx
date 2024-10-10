<%@ Page Title="Forgot Password" Language="C#" MasterPageFile="~/DealTamam2.Master" AutoEventWireup="true" CodeBehind="ForgotPassword.aspx.cs" Inherits="DealProjectTamam.ForgotPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .btn-green {
            background-color: #28a745; /* green color */
            color: white;
            border: none;
        }
        .btn-green:hover {
            background-color: #218838; /* darker shade for hover effect */
        }
        .background-image {
            background-image: url('/images/region/south.jpg'); /* path to the south.jpg */
            background-size: cover;
            background-position: center;
            height: 100vh; /* full viewport height */
        }
        .card-transparent {
            background-color: rgba(255, 255, 255, 0.8); /* semi-transparent background */
            border-radius: 1rem;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <section class="vh-100 background-image">
        <div class="container py-5 h-100">
            <div class="row d-flex justify-content-center align-items-center h-100">
                <div class="col col-xl-10">
                    <div class="card card-transparent">
                        <div class="card-body p-4 p-lg-5 text-black">
                            <asp:Label ID="lblMessage" runat="server" ForeColor="Red"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg" Placeholder="Email address"></asp:TextBox>
                            <asp:Button ID="btnSendOTP" runat="server" Text="Send OTP" CssClass="btn btn-green btn-lg btn-block" OnClick="btnSendOTP_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
