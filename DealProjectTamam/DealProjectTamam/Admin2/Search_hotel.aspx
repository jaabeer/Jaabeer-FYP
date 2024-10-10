<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="Search_hotel.aspx.cs" Inherits="DealProjectTamam.Admin2.Search_hotel" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }

        h4 {
            font-size: 24px;
            color: #333;
        }

        .table-container {
            margin: 20px auto;
            max-width: 90%;
            background-color: #fff;
            padding: 20px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        .header {
            background-color: #007bff;
            color: white;
            padding: 20px;
            text-align: center;
            border-radius: 8px 8px 0 0;
            margin-bottom: 20px;
        }

        .search-box {
            display: flex;
            justify-content: center;
            align-items: center;
            margin-bottom: 20px;
        }

        .form-control {
            width: 70%;
            padding: 10px;
            margin-right: 10px;
            border: 1px solid #ced4da;
            border-radius: 5px;
        }

        .btn {
            padding: 10px 20px;
            font-size: 14px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            border: none;
            border-radius: 5px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-primary {
            background-color: #007bff;
            color: white;
        }

        .btn:hover {
            opacity: 0.9;
        }

        #gvs {
            width: 100%;
            border-collapse: collapse;
        }

        #gvs th, #gvs td {
            padding: 16px;
            border: 1px solid #ddd;
            text-align: center;
            font-size: 14px;
        }

        #gvs th {
            background-color: #007bff;
            color: white;
            text-transform: uppercase;
            letter-spacing: 1px;
        }

        #gvs tr:nth-child(even) {
            background-color: #f8f9fa;
        }

        #gvs tr:hover {
            background-color: #e9ecef;
        }

        .btn-advertise {
            background-color: #17a2b8;
        }

        .btn-advertise:hover {
            background-color: #138496;
        }

        .btn-unload {
            background-color: #dc3545;
        }

        .btn-unload:hover {
            background-color: #c82333;
        }

        .image-container {
            width: 50px;
            height: 50px;
            overflow: hidden;
            border-radius: 5px;
        }

        .image-container img {
            width: 100%;
            height: auto;
        }

         .btn-danger {
            background-color: #dc3545;
            color: white;
        }

        .btn-danger:hover {
            background-color: #c82333;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="table-container">
        <div class="header">
            <h1>Search Hotel</h1>
        </div>
        <div class="search-box">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter hotel name"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvs" CssClass="table table-striped table-bordered"
            runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static">
            <Columns>
                <asp:TemplateField HeaderText="Hotel Name">
                    <ItemTemplate>
                        <%# Eval("Hotel_name") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Street">
                    <ItemTemplate>
                        <%# Eval("Hotel_street") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Town">
                    <ItemTemplate>
                        <%# Eval("Hotel_town") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Region">
                    <ItemTemplate>
                        <%# Eval("Dist_region") %>
                    </ItemTemplate>
                    <ItemStyle HorizontalAlign="Center"></ItemStyle>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Picture">
                    <ItemTemplate>
                        <div class="image-container">
                            <asp:Image ID="Image1" runat="server"
                                ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/main/" + Eval("Hotel_image", "{0}") %>' />
                        </div>
                    </ItemTemplate>
                </asp:TemplateField>

                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <%-- Assign the User_Id to the link button using the CommandArgument --%>
                        <asp:LinkButton ID="lnkblock" CssClass='<%# (Eval("Hotel_status").ToString() == "True" ? "btn btn-unload" : "btn btn-advertise") %>'
                            runat="server" OnClick="lnkblock_Click"
                            CommandArgument='<%# Eval("Hotel_id") %>'
                            Text='<%# (Eval("Hotel_status").ToString() == "True" ? "Undisplay" : "Display") %>'></asp:LinkButton>
                        <asp:LinkButton ID="lnkDelete" runat="server" CssClass="btn btn-danger"
                            OnClick="lnkDelete_Click" CommandArgument='<%# Eval("Hotel_id") %>'
                            Text="Delete" OnClientClick="return confirm('Are you sure you want to delete this hotel?');" style="margin-left: 5px;"></asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
</asp:Content>