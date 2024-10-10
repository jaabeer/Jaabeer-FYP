<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="Allnotification.aspx.cs" Inherits="DealProjectTamam.Allnotification" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    
				
				<div class="three-fourth">
					<div class="comments">
						<h2>All Notifications</h2>	

						<div>
						<asp:Button ID="btnAllNotif" runat="server" OnClick="btnAllNotif_Click" Text="All Notification" />
						<asp:Button ID="btnCreation" runat="server" OnClick="btnCreation_Click" Text="Creation Notifs" />
						<asp:Button ID="btnMainUpdate" runat="server" OnClick="btnMainUpdate_Click" Text="Main Update Notifs" />
						<asp:Button ID="btnUpdate" runat="server" OnClick="btnUpdate_Click" Text="Update Notifs" />
						</div>
						<br /><br />
						<asp:Button ID="btnReadAll" runat="server" OnClick="btnReadAll_Click" Text="Mark All as Read" />
						<br />
						<!--single comment-->
						<asp:Repeater ID="Repeater1" runat="server">
							<ItemTemplate>
						<div class="comment depth-0">
							<div class="third">
								<figure><img alt="avatar" src="images/uploads/images.png" /></figure>
								<address>
									<span>Admin</span><br />
									 <%# Eval("Notif_date") %>
								</address>
							</div>
							<div class="comment-content"> <b>Details:</b> <%# Eval("Notif_details") %></div>
							<div> <b>Admin Comment :</b>  <%# Eval("Comment") %></div>
							<asp:LinkButton ID="btnread" runat="server" CssClass="reply" CommandArgument='<%# Eval("id") %>' OnClick="btnread_Click"   Text="Mark as Read" />
						
						</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>
				</div>
	
</asp:Content>
