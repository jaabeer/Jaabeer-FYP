<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="Ticket.aspx.cs" Inherits="DealProjectTamam.Ticket" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div class="three-fourth">
					<div class="comments">
						<h2>All Tickets</h2>	

						<div>
						<asp:Button ID="btnAll" runat="server" OnClick="btnAll_Click" Text="All Tickets" />
						<asp:Button ID="btnClosed" runat="server" OnClick="btnClosed_Click" Text="Closed Tickets" />
						<asp:Button ID="btnOpen" runat="server" OnClick="btnOpen_Click" Text="In Progress" />
						</div>
						<br /><br />
						
						<!--single comment-->
						<asp:Repeater ID="Repeater1" runat="server">
							<ItemTemplate>
						<div class="comment depth-0">
							<div class="third">
								<figure><img alt="avatar" src="../images/support.png" width="30%" /></figure>
								<address>
									<span>Index:</span><br />
									 <%# Eval("reference") %>
								</address>
							</div>
							<div><h3><b>Title:</b> <%# Eval("title") %></h3> </div> <br />
							<div> <b>Status :</b>  <%# Eval("status_name") %></div>
							<div> <b>Description</b>  <%# Eval("description") %></div>
							<asp:LinkButton ID="btnread" runat="server" CssClass="reply" PostBackUrl='<%# "Ticket_details.aspx?Parameter="+ Eval("id") %>'  Text="More info" />
						
						</div>
								</ItemTemplate>
							</asp:Repeater>
						</div>
				</div>

</asp:Content>
