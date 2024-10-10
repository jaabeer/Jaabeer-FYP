<%@ Page Title="" Language="C#" MasterPageFile="~/Client1.Master" AutoEventWireup="true" CodeBehind="CartVilla.aspx.cs" Inherits="DealProjectTamam.CartVilla" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

	    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>

         	<style>
        .custom-button {
            background-color: #F97D09;
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
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
    <div class="row">
				<!--sidebar-->
				

		<div class="three-fourth">
					
						
						<div class="row deals  results">

							<asp:Repeater ID="Repeater1" runat="server">
                             <ItemTemplate>
								<article class="full-width">
							<figure><a href="hotel.html" title=""><asp:Image ID="Image1" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' width="300px" Height="200px" /></a></figure>
							<div class="details">
								<h3><%# Eval("Villa_name")%>  <asp:LinkButton ID="lbtnview_booking" Text="View Villa" runat="server" PostBackUrl= '<%# "cvilla_details.aspx?Parameter="+ Eval("Villa_id") %>'></asp:LinkButton>
									
								</h3>
								<br />
								<h5><span class="Check-in">Check-in: <%# Eval("Bk_checkin")%> </span> &nbsp&nbsp&nbsp&nbsp&nbsp
								<span class="Check-out">Check-out: <%# Eval("Bk_checkout")%></span></h5>
								<span class="price">Total Amount:  <em>Rs. <%# Eval("BK_amnt")%> </em> </span>
								<div class="description">
									<p style="text-overflow:ellipsis; overflow:hidden; white-space:nowrap;"> <%# Eval("Villa_desc")%></p>
								</div>
								
								<asp:LinkButton ID="btnDiscard" runat="server" CssClass="custom-button" OnClick="btnDiscard_Click" CommandArgument='<%# Eval("Bk_id") %>' Text="Discard" />
								
							</div>
						</article>
						</ItemTemplate>
					 </asp:Repeater>
						
						</div>
			


		</div>
				 <aside class="one-fourth right-sidebar">

                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                    <!--Need Help Booking?-->
                    <article class="widget">
                        <h4>Issue with your properties </h4>
                        <p>Call our customer support team on the number below to speak to one of our advisors who will help you with creation issues.</p>
                        <p class="number">(+230) 216-21-21</p>
                        <asp:Button ID="Button3" CssClass="custom-button" runat="server" Text="Contact Support" ValidationGroup="vg_support" />

                    </article>
                        </ContentTemplate>
                    </asp:UpdatePanel>

                </aside>
		</div>
		<br />
		<br /><br />


</asp:Content>