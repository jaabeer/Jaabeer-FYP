<%@ Page Title="" Language="C#" MasterPageFile="~/OwnerHome.Master" AutoEventWireup="true" CodeBehind="About_us.aspx.cs" Inherits="DealProjectTamam.About_us" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            margin: 0;
            padding: 0;
        }
        .background-container {
            background-image: url('images/R.jpeg');
            background-size: cover;
            background-position: center;
            background-attachment: fixed;
            padding-top: 70px; /* Adjust this if your navbar height is different */
        }
        .about-container {
            background: rgba(255, 255, 255, 0.8);
            max-width: 800px;
            margin: 20px auto;
            padding: 30px;
            box-shadow: 0 2px 5px rgba(0,0,0,0.1);
            border-radius: 10px;
            animation: fadeIn 1s ease-in-out;
        }
        .about-header {
            text-align: center;
            margin-bottom: 20px;
        }
        .about-header h1 {
            color: #2c3e50;
            font-size: 2.5em;
        }
        .about-content {
            color: #000; /* Black text */
            line-height: 1.8;
        }
        .about-content h2 {
            color: #000; /* Black header */
            margin-top: 20px;
            font-size: 1.5em;
        }
        .about-content p {
            margin: 10px 0;
            font-size: 1em;
        }
        .white-text {
            color: #fff; /* White text */
        }
        @keyframes fadeIn {
            from { opacity: 0; }
            to { opacity: 1; }
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <!-- Background container -->
    <div class="background-container">
        <div class="about-container">
            <div class="about-header">
                <h1 class="white-text">Welcome to DealTamam!</h1> <!-- White text -->
            </div>
            <div class="about-content white-text">
                <p>At DealTamam, we strive to offer the best villa and hotel booking experience for travelers worldwide. Our mission is to make finding and booking the perfect accommodation as easy and seamless as possible. Here’s what sets us apart:</p>
                
                <h2>Wide Range of Options</h2>
                <p>Whether you are looking for a luxurious villa by the sea or a cozy hotel in the city, DealTamam provides a diverse selection of accommodations to meet all your needs and preferences.</p>
                
                <h2>User-Friendly Interface</h2>
                <p>Our platform is designed with simplicity and efficiency in mind, ensuring that you can easily navigate and find the best deals without any hassle.</p>
                
                <h2>Trusted Reviews</h2>
                <p>We offer genuine reviews from past guests to help you make informed decisions. Our rating system ensures you get a clear picture of the quality and service of each property.</p>
                
                <h2>Secure Booking</h2>
                <p>Your safety and privacy are our top priorities. DealTamam employs advanced security measures to protect your personal information and ensure secure transactions.</p>
                
                <h2>24/7 Customer Support</h2>
                <p>Our dedicated customer support team is always ready to assist you, providing round-the-clock help for any questions or issues you may have.</p>
                
                <h2>Best Price Guarantee</h2>
                <p>We are committed to offering you the best prices for your stay. Our competitive rates and exclusive deals ensure you get the most value for your money.</p>
                
                <p>Experience the convenience and reliability of DealTamam for your next trip. Start exploring our vast array of villas and hotels today and book your perfect getaway!</p>
            </div>
        </div>
    </div>
    <br />
    <br />
</asp:Content>
