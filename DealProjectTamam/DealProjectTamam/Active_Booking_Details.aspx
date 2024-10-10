<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="Active_Booking_Details.aspx.cs" Inherits="DealProjectTamam.Active_Booking_Details" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <div style="padding: 50px">
        <h2>
            <asp:Label ID="lbl_client" runat="server"></asp:Label></h2>
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
                    <td><h5>
                        <asp:TextBox ID="txt_price" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                 <tr>
                    <th><h5>Payment Date</h5></th>
                    <td><h5>
                        <asp:TextBox ID="txtpaydate" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                 <tr>
                    <th><h5>Payment Type</h5></th>
                    <td><h5>
                        <asp:TextBox ID="txtpaytype" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                 <tr>
                    <th><h5>Amount Paid</h5></th>
                    <td><h5>
                        <asp:TextBox ID="txtpaid" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
                <tr>
                    <th><h5>Amount Remained</h5></th>
                    <td><h5>
                        <asp:TextBox ID="txtrem" runat="server" BorderStyle="None" /></h5>
                    </td>
                </tr>
            </table>
            <asp:TextBox ID="txt_villa_name" runat="server" Visible="false"></asp:TextBox>
            <asp:TextBox ID="txt_villa_address" runat="server" Visible="false"></asp:TextBox>
        </div>

        <div class="actions">
            <asp:Button runat="server" ID="btn_approve"  Text="Approve" OnClick="btn_approve_Click" class="gradient-button"  /><br />
            <asp:Button runat="server" ID="btn_reject"  Text="Reject" OnClick="btn_reject_Click" class="gradient-button" />
            <asp:Button runat="server" ID="btn_back"  PostBackUrl="ActiveBooking.aspx?Parameter=All" Text="Back" class="gradient-button" />
            <asp:Button runat="server"  ID="btn_gen_pdf" Text ="View PDF" class="gradient-button"  OnClick="btn_gen_pdf_Click" />
           
            <asp:Label ID="lbl_message" runat="server" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
        </div>
    </div>
</asp:Content>
