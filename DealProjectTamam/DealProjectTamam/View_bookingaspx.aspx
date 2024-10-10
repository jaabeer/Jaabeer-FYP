<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="View_bookingaspx.aspx.cs" Inherits="DealProjectTamam.View_bookingaspx" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        #gvs {
            width: 100%;
        }

        th {
            background: #494e5d;
            color: white;
        }

        .tableCss {
            border: solid 1px #e6e5e5;
        }

            /*for header*/
            .tableCss thead {
                background-color: #0094ff;
                color: #fff;
                padding: 10px;
                text-align: center;
            }

            .tableCss td {
                border: solid 1px #e6e5e5;
                padding: 10px;
            }

        /*for footer*/
        .tabTask tfoot {
            background-color: #000;
            color: #fff;
            padding: 10px;
        }

        /*for body*/
        .tabTask tbody {
            background-color: #e9e7e7;
            color: #000;
            padding: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <div style="padding: 30px">
        <br />
        <h3 id="h3_booking" runat="server">View Booking</h3>
        <hr />
        <asp:GridView ID="gvs" CssClass="table table-striped table-bordered align-items-center"
            runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static">
            <Columns>

                <asp:TemplateField HeaderText="Last Name" HeaderStyle-BackColor="#009999">
                    <ItemTemplate>
                        <h5><%# Eval("Client_lname") %></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="First Name" HeaderStyle-BackColor="#009999">
                    <ItemTemplate>
                        <h5><%# Eval("Client_fname") %></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Booking Date" HeaderStyle-BackColor="#009999">
                    <ItemTemplate>
                        <h5><%# Convert.ToDateTime(Eval("Bk_date")).ToString("dd-MMMM-yyyy") %></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Check-in Date" HeaderStyle-BackColor="#009999">
                    <ItemTemplate>
                        <h5><%# Convert.ToDateTime(Eval("Bk_checkin")).ToString("dd-MMMM-yyyy")%></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Check-out Date" HeaderStyle-BackColor="#009999" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <h5><%# Convert.ToDateTime(Eval("Bk_checkout")).ToString("dd-MMMM-yyyy") %></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Status" HeaderStyle-BackColor="#009999" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <h5><%# Eval("Bk_state").ToString() == "C" ? "Confirmed" : "Pending" %></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Reference" HeaderStyle-BackColor="#009999" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <h5><asp:HyperLink runat="server" NavigateUrl='<%# "View_booking_details.aspx?ID="+ Eval("Bk_id") %>' Text='<%# Eval("Bk_ref") %>'> </asp:HyperLink></h5>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Justify"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Profile picture" HeaderStyle-BackColor="#009999" HeaderStyle-Width="10%">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server"
                            ImageUrl='<%# Eval("Client_profilepicture","~/images/{0}") %>'
                            ControlStyle-Width="50"
                            PostBackUrl='<%# Eval("Client_id", "view_client_profile.aspx?ID={0}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>

            </Columns>
        </asp:GridView>

        <asp:Button ID ="btn_back" runat="server" Text="Back" PostBackUrl="~/MyVillas.aspx" />
    </div>
</asp:Content>
