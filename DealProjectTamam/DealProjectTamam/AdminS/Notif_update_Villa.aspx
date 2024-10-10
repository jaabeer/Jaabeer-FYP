<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="Notif_update_Villa.aspx.cs" Inherits="DealProjectTamam.AdminS.Notif_update_Villa" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">

    <style>
        #gvs {
            width: 100%;
            border-collapse: collapse;
            margin: 20px 0;
            font-size: 18px;
            text-align: left;
        }

        #gvs th, #gvs td {
            padding: 12px 15px;
            border: 1px solid #ddd;
        }

        #gvs thead {
            background-color: #0000FF; /* Blue color for header background */
            color: #ffffff;
        }

        #gvs tbody tr:nth-child(even) {
            background-color: #f3f3f3;
        }

        #gvs tbody tr:hover {
            background-color: #ddd;
        }

        .btn-view {
            background-color: #0000FF; /* Blue color for button */
            color: white;
            border: none;
            padding: 10px 20px;
            cursor: pointer;
            text-decoration: none;
            border-radius: 5px;
        }

        .btn-view:hover {
            background-color: #0000CC;
        }
    </style>

</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4>Villa Modification Notification </h4>
    <hr />
    <asp:Button ID="btnReadAll" runat="server" Text="Mark All As Read" OnClick="btnReadAll_Click" />
    <br />
    <asp:GridView ID="gvs" CssClass="table table-striped table-bordered"
        runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static">
        <Columns>

            <asp:TemplateField HeaderText="Property Name">
                <ItemTemplate>
                    <%# Eval("Villa_name") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Update Details">
                <ItemTemplate>
                    <%# Eval("Notif_details") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Town">
                <ItemTemplate>
                    <%# Eval("Villa_town") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Region">
                <ItemTemplate>
                    <%# Eval("Dist_region") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Picture">
                <ItemTemplate>
                    <asp:ImageButton ID="ImageButton1" runat="server"
                        ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image", "{0}") %>'
                        ControlStyle-Width="50"
                        />
                </ItemTemplate>
                </asp:TemplateField>

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <%-- Assign the User_Id to the link button using the CommandArgument --%>
                    <asp:LinkButton ID="lnkblock" CssClass="btn alert-success"
                        runat="server" 
                        Text="View Details"
                        PostBackUrl='<%# Eval("id", "New_vdetails.aspx?ID={0}") %>'
                        Font-Bold="True"></asp:LinkButton>

                        
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</asp:Content>

