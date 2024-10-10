<%@ Page Title="" Language="C#" MasterPageFile="~/Client3.master" AutoEventWireup="true" CodeBehind="PaymentForm.aspx.cs" Inherits="DealProjectTamam.PaymentForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            background-color: #f0f2f5;
        }

        .payment-container {
            font-family: Arial, sans-serif;
            background-color: #ffffff;
            color: #333;
            padding: 20px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
            margin: 50px auto;
            max-width: 400px;
        }
        
        .payment-container h2 {
            text-align: center;
            color: #5a5a5a;
            font-weight: bold;
            margin-bottom: 20px;
        }

        .payment-container .form-group {
            margin-bottom: 15px;
        }

        .payment-container .form-group label {
            display: block;
            margin-bottom: 5px;
            color: #666;
        }

        .payment-container .form-group input {
            width: 100%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            transition: border 0.3s;
        }

        .payment-container .form-group input:focus {
            border-color: #007bff;
        }

        .payment-container .button-container {
            text-align: center;
        }

        .payment-container .submit-button {
            background: linear-gradient(to right, #007bff, #00d4ff);
            color: white;
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background 0.3s, transform 0.3s;
        }

        .payment-container .submit-button:hover {
            background: linear-gradient(to right, #00d4ff, #007bff);
            transform: scale(1.05);
        }

        .qr-container {
            text-align: center;
            margin-top: 20px;
        }

        .qr-container img {
            width: 200px; /* Adjust as necessary */
            height: 200px;
        }

        .checkbox-container {
            text-align: center;
            margin-top: 10px;
            height:20px;
        }

        .checkbox-container input {
            transform: scale(0.8);
            margin-right: 10px;
        }

        .checkbox-container label {
            color: #666;
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
    <div class="payment-container">
        <h2>Payment</h2>

        <asp:Label ID="lblError" runat="server" ForeColor="Red"></asp:Label>
        <asp:Label ID="lblSuccess" runat="server" ForeColor="Green"></asp:Label>

        <div class="form-group">
            <label for="txtName">Name</label>
            <asp:TextBox ID="txtName" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        </div>
        
        <div class="form-group">
            <label for="txtEmail">Email</label>
            <asp:TextBox ID="txtEmail" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="txtAmount">Amount</label>
            <asp:TextBox ID="txtAmount" runat="server" ReadOnly="true" CssClass="form-control"></asp:TextBox>
        </div>

        <div class="form-group">
            <label for="ddlPaymentMethod">Payment Method</label>
            <asp:DropDownList ID="ddlPaymentMethod" runat="server" AutoPostBack="true" OnSelectedIndexChanged="ddlPaymentMethod_SelectedIndexChanged" CssClass="form-control">
                <asp:ListItem Text="Select Payment Method" Value="-1"></asp:ListItem>
                <asp:ListItem Text="Pay by Juice" Value="1"></asp:ListItem>
            </asp:DropDownList>
        </div>

        <asp:Panel runat="server" ID="pnlJuice" Visible="false">
            <div class="qr-container">
                <asp:Image ID="imgQR" runat="server" ImageUrl="~/images/juice.png" />
            </div>
            <div class="checkbox-container">
                <input type="checkbox" name="check" id="check" value="ch1" />
                <label>Yes, I have read and I agree to the <a href="#">booking conditions</a>.</label>
            </div>
            <br />
            <br />
            <br />
            <br />
           
            <div class="button-container">
                <asp:Button ID="btnJuice" runat="server" OnClick="btnJuice_Click" OnClientClick="return confirm('Do you confirm your payment?');" CssClass="submit-button" Text="Submit Booking" />
            </div>
        </asp:Panel>
    </div>
</asp:Content>
