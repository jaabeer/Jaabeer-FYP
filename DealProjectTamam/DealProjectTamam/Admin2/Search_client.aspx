<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="Search_client.aspx.cs" Inherits="DealProjectTamam.Admin2.Search_client" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Roboto', sans-serif;
            background-color: #e0f7fa;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 85%;
            margin: 0 auto;
            padding: 20px;
            background-color: #fff;
            box-shadow: 0 2px 5px rgba(0, 0, 0, 0.1);
            border-radius: 8px;
        }

        .header {
            background-color: #0288d1;
            color: white;
            padding: 20px;
            text-align: center;
            border-radius: 8px 8px 0 0;
        }

        .header h1 {
            margin: 0;
            font-size: 28px;
        }

        .search-box {
            display: flex;
            justify-content: space-between;
            align-items: center;
            margin: 20px 0;
        }

        .form-control {
            width: 70%;
            padding: 10px;
            border: 1px solid #ccc;
            border-radius: 4px;
            transition: border-color 0.3s ease;
        }

        .form-control:focus {
            border-color: #0288d1;
        }

        .btn {
            padding: 10px 20px;
            font-size: 16px;
            font-weight: bold;
            text-align: center;
            text-decoration: none;
            border: none;
            border-radius: 4px;
            cursor: pointer;
            transition: background-color 0.3s ease;
        }

        .btn-primary {
            background-color: #0288d1;
            color: white;
        }

        .btn-primary:hover {
            background-color: #0277bd;
        }

        .tableCss {
            width: 100%;
            margin-top: 20px;
            border: 1px solid #ddd;
            border-collapse: collapse;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .tableCss thead {
            background-color: #0288d1;
            color: #fff;
        }

        .tableCss th,
        .tableCss td {
            border: 1px solid #ddd;
            padding: 10px;
            text-align: center;
        }

        .btn-danger {
            background-color: #d32f2f;
            color: white;
        }

        .btn-danger:hover {
            background-color: #b71c1c;
        }
    </style>
    <link href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="header">
            <h1>Search Client</h1>
        </div>
        <div class="search-box">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter client name"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvs" CssClass="tableCss"
            runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender">
            <Columns>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                        <%# Eval("Client_fname") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                        <%# Eval("Client_lname") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NIC">
                    <ItemTemplate>
                        <%# Eval("Client_nic") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <%# Eval("Client_email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Profile Picture">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server"
                            ImageUrl='<%# Eval("Client_profilepicture", "~/images/{0}") %>'
                            Width="50" CssClass="img-thumbnail"
                            PostBackUrl='<%# Eval("Client_id", "view_client_profile.aspx?ID={0}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkblock" CssClass='<%# Convert.ToString(Eval("Client_status")) == "True" ? "btn btn-danger block" : "btn btn-primary unblock" %>'
                            runat="server" OnClick="lnkblock_Click"
                            CommandArgument='<%# Eval("Client_id") %>'
                            Text='<%# Convert.ToString(Eval("Client_status")) == "True" ? "Block" : "Unblock" %>'>
                        </asp:LinkButton>
                    </ItemTemplate>
                </asp:TemplateField>
            </Columns>
        </asp:GridView>
    </div>
    <script type="text/javascript">
        $(function () {
            $("#<%= txtSearch.ClientID %>").autocomplete({
                source: function (request, response) {
                    $.ajax({
                        url: '<%= ResolveUrl("Search_Client.aspx/GetClientNames") %>',
                        data: "{ 'prefix': '" + request.term + "' }",
                        dataType: "json",
                        type: "POST",
                        contentType: "application/json; charset=utf-8",
                        success: function (data) {
                            response($.map(data.d, function (item) {
                                return { label: item, value: item };
                            }));
                        },
                        error: function (response) {
                            alert(response.responseText);
                        },
                        failure: function (response) {
                            alert(response.responseText);
                        }
                    });
                },
                minLength: 2
            });
        });
    </script>
</asp:Content>
