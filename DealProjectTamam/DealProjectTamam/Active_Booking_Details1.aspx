<%@ Page Title="" Language="C#" MasterPageFile="~/Owner2Master.Master" AutoEventWireup="true" CodeBehind="Active_Booking_Details1.aspx.cs" Inherits="DealProjectTamam.Active_Booking_Details1" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .main {
            font-family: Arial, sans-serif;
            padding: 20px;
            background-color: #f4f4f9;
        }

        .b-info {
            background: #fff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 0 20px rgba(0, 0, 0, 0.1);
        }

        table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        th, td {
            border: 1px solid #e6e5e5;
            padding: 10px;
            text-align: left;
        }

        th {
            background-color: #0094ff;
            color: #fff;
        }

        .actions {
            margin-top: 20px;
            display: flex;
            gap: 10px;
        }

        .gradient-button {
            background: linear-gradient(to right, #007bff, #00c6ff);
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            font-size: 16px;
            border-radius: 4px;
            cursor: pointer;
        }

        .gradient-button:hover {
            background: linear-gradient(to right, #0056b3, #0094ff);
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="main">
        <h2>
            <asp:Label ID="lbl_client" runat="server"></asp:Label>
        </h2>
        <div class="b-info">
            <table>
                <asp:TextBox ID="txt_prop_id" runat="server" Visible="false"></asp:TextBox>
                <tr>
                    <th><h5>Booking Reference</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_bk_ref" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Last Name</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_lname" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>First Name</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_fname" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>NIC</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_NIC" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Address</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_address" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Email</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_email" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Booking Date</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_bk_date" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Check-in Date</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_check_in" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Check-out Date</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_check_out" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Booking Status</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_bk_status" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Total Price:</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txt_price" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Payment Date</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txtpaydate" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Payment Type</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txtpaytype" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Amount Paid</h5></th>
                    <td>
                        <h5><asp:TextBox ID="txtpaid" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
               
            </table>
            <asp:TextBox ID="txt_villa_name" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_villa_address" runat="server" Visible="false"></asp:TextBox>
        </div>
        <div class="actions">
            <asp:Button runat="server" ID="btn_approve" Text="Approve" OnClick="btn_approve_Click" CssClass="gradient-button" /><br />
            <asp:Button runat="server" ID="btn_reject" Text="Reject" OnClick="btn_reject_Click" CssClass="gradient-button" />
            <asp:Button runat="server" ID="btn_back" PostBackUrl="ActiveBooking1.aspx?Parameter=All" Text="Back" CssClass="gradient-button" />
            <asp:Button runat="server" ID="btn_gen_pdf" Text="View PDF" CssClass="gradient-button" OnClick="btn_gen_pdf_Click" />
            <asp:Label ID="lbl_message" runat="server" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
        </div>
    </div>
</asp:Content>
