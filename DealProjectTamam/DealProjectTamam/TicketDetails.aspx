<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="TicketDetails.aspx.cs" Inherits="DealProjectTamam.TicketDetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="main">
        <div class="wrap">
            <div class="row">
                <!--three-fourth content-->
                <div class="two-third">

                    <fieldset>
                        <h2>Customer Support</h2>

                   
                         <asp:Label ID="lblmsg" runat="server" ></asp:Label>
                        <div class="row">
                            <div class="f-item one-half">
                                <asp:Label runat="server" AssociatedControlID="txtFirstName" Display="Dynamic">First name</asp:Label>
                                
                                <asp:TextBox ID="txtFirstName" runat="server" ReadOnly="true"></asp:TextBox>
                            </div>

                            <div class="f-item one-half">
                                <asp:Label runat="server" AssociatedControlID="txtLastName" Display="Dynamic">Last name</asp:Label>
                                <asp:TextBox runat="server" ID="txtLastName" ReadOnly="true" />
                                
                            </div>
                        </div>

                 <div class="row">

                            <div class="f-item one-half">
                                <asp:Label runat="server" AssociatedControlID="txtEmail" Display="Dynamic">Email Address</asp:Label>
                                <asp:TextBox runat="server" ID="txtEmail" Display="Dynamic" ReadOnly="true" />
                                
                            </div>

                          <div class="f-item one-half">
                                <asp:Label runat="server" AssociatedControlID="txtMobile" Display="Dynamic">Mobile Number</asp:Label>
                                <asp:TextBox runat="server" ID="txtMobile" ReadOnly="true" />
                                
                            </div>

                        </div>

                         <div class="row">

                            <div class="f-item full-width">
                                <asp:Label runat="server" AssociatedControlID="ddlCategory" Display="Dynamic">Issue Category</asp:Label>
                                <asp:DropDownList ID="ddlCategory"   runat="server" ReadOnly="true"></asp:DropDownList>
                                
                            </div>

                        </div>



                          <div class="row">

                            <div class="f-item full-width">

                                <asp:Label runat="server" AssociatedControlID="txtTitle" Display="Dynamic">Title</asp:Label>
                                <asp:TextBox ID="txttitle"  runat="server" ReadOnly="true"></asp:TextBox>
                                
                            </div>

                        </div>


                         <div class="row">

                            <div class="f-item full-width">
                                <asp:Label runat="server" AssociatedControlID="txtdspt" Display="Dynamic">Description</asp:Label>
                                <asp:TextBox ID="txtdspt" TextMode="MultiLine" runat="server" ReadOnly="true"></asp:TextBox>
                                
                            </div>

                        </div>

                        <hr />
                        <div class="row">
                            <asp:DropDownList ID="ddlStatus" runat="server" ></asp:DropDownList>
                            <br />
                            <div class="f-item full-width">
                                <asp:Label runat="server" AssociatedControlID="txtdspt" Display="Dynamic">Your Comment:</asp:Label>
                                <asp:TextBox ID="txtcomment" TextMode="MultiLine" runat="server"></asp:TextBox>
                                 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtcomment" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Comment is required." />
                            </div>

                        </div>

                        <div class="row">
                            <div class="f-item full-width">

                                <asp:Button ID="btnSend" runat="server" Text="Comment" class="gradient-button" Width="50%" Style="margin-left:25%" OnClick="btnSend_Click"   />
                                <asp:Button ID="btnsuccess" runat="server" Visible="false" Text="Comment posted Succesfully" class="gradient-button"  CausesValidation="false"/>
                            </div>
                        </div>
                    </fieldset>


                    <div class="comments">
						<h2>Comments</h2>	
						<!--single comment-->
                        <asp:Repeater ID="Repeater1" runat="server">
                            <ItemTemplate>
						<div class="comment depth-0">
							<div class="third">
								<figure><img alt="avatar" src="../images/support.png" width="" /></figure>
								<address>
									<span><%# Eval("Username") %> </span><br />
									<%# Eval("Date") %> 
								</address>
							</div>
							<div class="comment-content"><%# Eval("Comment") %> </div>
							
						</div>
                            </ItemTemplate>
                            </asp:Repeater>
                        </div>


                </div>
                </div>
            </div>
            </main>
</asp:Content>
