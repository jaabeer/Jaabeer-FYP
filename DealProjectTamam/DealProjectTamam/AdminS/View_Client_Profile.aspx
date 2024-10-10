<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="View_Client_Profile.aspx.cs" Inherits="DealProjectTamam.AdminS.View_Client_Profile" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
        <style type="text/css">
        .content {
            width: 700px;
            border: solid 1px black;
            background-color: #eeeeee;
            font-family: 'Segoe UI';
            height: auto;
            font-weight: 500;
        }

        .box2 {
            margin-top: 2px;
            text-align: center;
            height: auto
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
        <br />
    <h4>Client Profile</h4>
    <hr />
    <div class="content">
        <asp:DataList
            ID="dlstClientDetails"
            RepeatColumns="1"
            runat="server">
            <ItemTemplate>
                <div class="card  text-grey h-50"
                    style="width: 630px; float: left; margin: 30px; background-color: #a0ced9">
                    <asp:Image runat="server" ImageUrl='<%# Eval("Client_profilepicture", "~/images/{0}")%>'
                        ID="Image1" CssClass="card-img-top mx-auto" Width="190px" Height="200px" />
                    <div class="card-body h">
                        <h2 class="card-title"><%# Eval("Client_fname") +" "+Eval("Client_lname")%></h2>

                        <p class="card-text" style="margin: 0em">
                            <span><strong>Name:
                            </strong></span>
                            <span class="text-dark" style="margin: 0em">
                                <%# Eval("Client_fname")%>
                            </span>
                        </p>

                        <p class="card-text" style="margin: 0em">
                            <span><strong>National Identity Card Number:
                            </strong></span>
                            <span class="text-dark" style="margin: 0em">
                                <%# Eval("Client_nic")%>
                            </span>
                        </p>

                        <p class="card-text" style="margin: 0em">
                            <span><strong>Date of Birth:
                            </strong></span>
                            <span class="text-dark">
                                <%# Eval("Client_dob","{0:dd/MM/yyyy}")%>
                            </span>
                        </p>

                        <p class="card-text" style="margin: 0em">
                            <span><strong>Gender:
                            </strong></span>
                            <span class="text-dark">
                                <%# Eval("Gender")%>
                            </span>
                        </p>
                        <p class="card-text" style="margin: 0em">
                            <span><strong>Address:
                            </strong></span>
                            <span class="text-dark">
                                <%# Eval("Client_address")%>
                            </span>
                        </p>
                        <p class="card-text" style="margin: 0em">
                            <span><strong>Mobile number:
                            </strong></span>
                            <span class="text-dark">
                                <%# Eval("Client_contact")%>
                            </span>
                        </p>



                        <p class="card-text" style="margin: 0em">
                            <span><strong>Email:
                            </strong></span>
                            <span class="text-dark">
                                <%# Eval("Client_email")%>
                            </span>
                        </p>

                    </div>
                    <div class="box2">
                        <asp:LinkButton ID="lnk_client_profile" runat="server" Text="Back"
                            OnClientClick="JavaScript:window.history.back(1); return false;"
                            CssClass="btn btn-info" Font-Bold="True"></asp:LinkButton>
                    </div>
                </div>
            </ItemTemplate>
        </asp:DataList>
    </div>
</asp:Content>

