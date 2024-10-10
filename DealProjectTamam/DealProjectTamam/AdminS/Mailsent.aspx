<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="Mailsent.aspx.cs" Inherits="DealProjectTamam.AdminS.Mailsent" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
    <h4>Mail sent to Owner</h4>

    <div class="carousel-item active" style="padding-left: 50px">
        <asp:Image ID="img_sent" ImageUrl="~/images/mail_sent.gif" runat="server" Height="500px" Width="600px" BorderStyle="None" BorderColor="White" BorderWidth="5px" />
     <div style="padding-left: 250px" >
         <br />
        <asp:LinkButton ID="Back" runat="server" Text="Back"  PostBackUrl="~/Admin/Villa_creation.aspx" CssClass ="btn btn-info" Font-Size="Large" Font-Bold="True"></asp:LinkButton>
    </div>   
    </div>


</asp:Content>
