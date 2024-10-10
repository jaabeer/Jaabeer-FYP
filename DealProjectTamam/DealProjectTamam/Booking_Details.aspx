<%@ Page Title="" Language="C#" MasterPageFile="~/Client1.Master" AutoEventWireup="true" CodeBehind="Booking_Details.aspx.cs" Inherits="DealProjectTamam.Booking_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" type="text/css" href="~/Styles/booking_details.css" />
    <link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/animate.css/4.1.1/animate.min.css" />
    <style>
        .container {
            max-width: 900px;
            margin: auto;
            padding: 20px;
            animation: fadeIn 2s ease-in-out;
        }

        .booking-details {
            background: #fff;
            padding: 30px;
            border-radius: 10px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
            transition: transform 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
        }

        .booking-details:hover {
            transform: scale(1.02);
            box-shadow: 0 0 25px rgba(0, 0, 0, 0.2);
        }

        h2 {
            font-size: 28px;
            margin-bottom: 20px;
            color: #333;
            text-align: center;
            animation: fadeInDown 1s;
        }

        .error-label {
            color: red;
            margin-bottom: 10px;
            display: block;
        }

        .badge-info {
            background: #17a2b8;
            color: #fff;
            padding: 5px 10px;
            border-radius: 5px;
            display: inline-block;
        }

        .form-row {
            display: flex;
            flex-wrap: wrap;
            margin-bottom: 15px;
        }

        .form-group {
            flex: 1;
            padding: 0 10px;
            margin-bottom: 15px;
        }

        .form-group label {
            display: block;
            margin-bottom: 5px;
            font-weight: bold;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 5px;
            box-sizing: border-box;
            transition: border-color 0.3s ease-in-out, box-shadow 0.3s ease-in-out;
        }

        .form-control:focus {
            border-color: #007bff;
            box-shadow: 0 0 5px rgba(0, 123, 255, 0.5);
        }

        .full-width {
            flex: 100%;
        }

        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            margin-right: 10px;
            background: #F97D09;
            color: #fff;
            transition: background-color 0.3s ease-in-out, transform 0.3s ease-in-out;
        }

        .btn:hover {
            background: #ca5e07;
            transform: translateY(-2px);
            box-shadow: 0 5px 15px rgba(249, 125, 9, 0.4);
        }

        .info {
            display: block;
            margin-top: 10px;
            color: #888;
        }

        @keyframes fadeIn {
            0% {
                opacity: 0;
            }
            100% {
                opacity: 1;
            }
        }

        @keyframes fadeInDown {
            0% {
                opacity: 0;
                transform: translateY(-20px);
            }
            100% {
                opacity: 1;
                transform: translateY(0);
            }
        }

        @keyframes fadeInUp {
            0% {
                opacity: 0;
                transform: translateY(20px);
            }
            100% {
                opacity: 1;
                transform: translateY(0);
            }
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
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container animate__animated animate__fadeIn">
        <section class="booking-details">
            <fieldset>
                <h2>Booking Info</h2>
                <asp:Label ID="lblerror" runat="server" Text="" CssClass="error-label"></asp:Label> 
                <h4>REFERENCE INDEX: <asp:Label ID="lblref" runat="server" Text=""></asp:Label></h4>
                <div class="form-row">
                    <div class="form-group">
                        <label for="txt_fname">First Name</label>
                        <asp:TextBox ID="txt_fname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txt_lname">Last Name</label>
                        <asp:TextBox ID="txt_lname" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label for="txt_email">Email Address</label>
                        <asp:TextBox ID="txt_email" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txt_contact">Contact Number</label>
                        <asp:TextBox ID="txt_contact" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <asp:TextBox ID="txt_propid" runat="server" ReadOnly="true" Visible="false"/>
                    <div class="form-group">
                        <label for="txt_villa">Villa</label>
                        <asp:TextBox ID="txt_villa" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txt_region">Region</label>
                        <asp:TextBox ID="txt_region" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label for="txt_checkin">Check-in</label>
                        <asp:TextBox ID="txt_checkin" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txt_checkout">Check-out</label>
                        <asp:TextBox ID="txt_checkout" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group">
                        <label for="txt_type">Booking Type</label>
                        <asp:TextBox ID="txt_type" runat="server" CssClass="form-control" ReadOnly="true"></asp:TextBox>
                    </div>
                    <div class="form-group">
                        <label for="txt_total">Total Cost</label>
                        <asp:TextBox ID="txt_total" runat="server" CssClass="form-control" ReadOnly="true" TextMode="Number"></asp:TextBox>
                    </div>
                </div>
                <div class="form-row">
                    <div class="form-group full-width">
                        <label for="txtsreq">Anything : <span></span></label>
                        <asp:TextBox ID="txtsreq" runat="server" CssClass="form-control" TextMode="MultiLine"></asp:TextBox>
                    </div>
                    <span class="info">.</span>
                </div>
                <div class="form-row">
                    <div class="form-group full-width">
                        <asp:Button ID="next_step" runat="server" Text="Confirm Booking" CssClass="btn btn-primary" OnClick="next_step_Click" />
                       <%-- <asp:Button ID="cancel" runat="server" Text="Discard Booking" CssClass="btn btn-secondary" OnClientClick="return confirm('Are you sure to discard this booking');" CausesValidation="false" OnClick="cancel_Click" />--%>
                    </div>
                </div>
            </fieldset>
        </section>
    </div>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/wow/1.1.2/wow.min.js"></script>
    <script>
        new WOW().init();
    </script>
</asp:Content>
