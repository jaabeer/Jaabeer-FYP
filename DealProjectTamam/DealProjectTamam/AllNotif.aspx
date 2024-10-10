<%@ Page Title="" Language="C#" MasterPageFile="~/Owner2Master.Master" AutoEventWireup="true" CodeBehind="AllNotif.aspx.cs" Inherits="DealProjectTamam.AllNotif" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css">
    <style>
        .notification-header {
            background-color: #f8f9fa;
            padding: 20px;
            border-bottom: 1px solid #e9ecef;
            display: flex;
            align-items: center;
            justify-content: space-between;
        }
        .notification-buttons {
            display: flex;
            gap: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="notification-header">
            <h2>All Notifications</h2>
            <div class="notification-buttons">
                <asp:Button ID="btnAllNotif" runat="server" OnClick="btnAllNotif_Click" CssClass="btn btn-primary" Text="All Notification" />
                <asp:Button ID="btnCreation" runat="server" OnClick="btnCreation_Click" CssClass="btn btn-secondary" Text="Creation Notifs" />
                <asp:Button ID="btnMainUpdate" runat="server" OnClick="btnMainUpdate_Click" CssClass="btn btn-success" Text="Main Update Notifs" />
                <asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" CssClass="btn btn-info" Text="Update Notifs" />
                <asp:Button ID="btnReadAll" runat="server" OnClick="btnReadAll_Click" CssClass="btn btn-warning" Text="Mark All as Read" />
            </div>
        </div>
        <div class="notification-content">
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div class="comment card">
                        <div class="card-body">
                            <div class="third">
                                <img alt="avatar" src="images/uploads/images.png" class="img-thumbnail" width="50" height="50" />
                                <div>
                                    <span><strong>Admin</strong></span><br />
                                    <%# Eval("Notif_date") %>
                                </div>
                            </div>
                            <div class="comment-content mt-3">
                                <p><b>Details:</b> <%# Eval("Notif_details") %></p>
                                <p><b>Admin Comment:</b> <%# Eval("Comment") %></p>
                                <asp:LinkButton ID="btnread" runat="server" CssClass="btn btn-sm btn-outline-primary" CommandArgument='<%# Eval("id") %>' OnClick="btnread_Click" Text="Mark as Read" />
                            </div>
                        </div>
                    </div>
                </ItemTemplate>
            </asp:Repeater>
        </div>
    </div>
</asp:Content>
