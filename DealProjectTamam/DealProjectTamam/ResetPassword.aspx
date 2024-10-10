<%@ Page Title="Reset Password" Language="C#" MasterPageFile="~/DealTamam2.Master" AutoEventWireup="true" CodeBehind="ResetPassword.aspx.cs" Inherits="DealProjectTamam.ResetPassword" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .success-message {
            color: green;
        }
        .btn-custom {
            background-color: green ; /* same as the login button */
            color: white;
            border: none;
        }
        .btn-custom:hover {
            background-color: none ; /* darker shade for hover effect */
        }
        .background-image {
            background-image: url('/images/region/south.jpg'); /* path to the sea.jpg */
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
                            <asp:Label ID="lblMessage" runat="server" CssClass="success-message" ForeColor="Red"></asp:Label>
                            <asp:TextBox ID="txtEmail" runat="server" CssClass="form-control form-control-lg" Placeholder="Email address"></asp:TextBox>
                            <asp:TextBox ID="txtOTP" runat="server" CssClass="form-control form-control-lg" Placeholder="Enter OTP"></asp:TextBox>
                            <asp:TextBox ID="txtNewPassword" runat="server" TextMode="Password" CssClass="form-control form-control-lg" Placeholder="New Password"></asp:TextBox>
                            <asp:Button ID="btnResetPassword" runat="server" Text="Reset Password" CssClass="btn-green btn-custom btn-lg btn-block" OnClick="btnResetPassword_Click" />
                        </div>
                    </div>
                </div>
            </div>
        </div>
    </section>
</asp:Content>
