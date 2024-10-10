<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="mail_sent.aspx.cs" Inherits="DealProjectTamam.mail_sent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
    <h4>Mail sent to Admin 2 </h4>

    <div class="carousel-item active" style="padding-left: 50px">
        <asp:Image ID="img_sent" ImageUrl="~/images/mail.gif" runat="server" Height="500px" Width="600px" BorderStyle="None" BorderColor="White" BorderWidth="5px" />
     <div style="padding-left: 250px" >
         <br />
        <asp:LinkButton ID="Back" runat="server" Text="Back"  PostBackUrl="CreateHotel.aspx" CssClass ="btn btn-info" Font-Size="Large" Font-Bold="True"></asp:LinkButton>
    </div>   
    </div>


</asp:Content>