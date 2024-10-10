<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="Searchowner.aspx.cs" Inherits="DealProjectTamam.AdminS.Searchowner" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #e0f7fa;
            margin: 0;
            padding: 0;
        }

        .container {
            width: 90%;
            margin: 0 auto;
            padding: 20px;
        }

        .header {
            background-color: #007bff;
            color: white;
            padding: 20px;
            text-align: center;
            border-radius: 8px 8px 0 0;
            margin-bottom: 20px;
        }

        .header h1 {
            margin: 0;
            font-size: 24px;
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

        .tableCss {
            width: 100%;
            margin: 20px 0;
            border: 1px solid #dee2e6;
            border-collapse: collapse;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
        }

        .tableCss thead {
            background-color: #007bff;
            color: #fff;
            padding: 10px;
            text-align: center;
        }

        .tableCss th,
        .tableCss td {
            border: 1px solid #dee2e6;
            padding: 10px;
            text-align: center;
        }

        .btn-danger {
            background-color: red;
        }

        .btn-primary {
            background-color: #007bff;
        }
    </style>
    <link href="https://code.jquery.com/ui/1.12.1/themes/base/jquery-ui.css" rel="stylesheet" />
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://code.jquery.com/ui/1.12.1/jquery-ui.min.js"></script>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="header">
            <h1>Search Owner</h1>
        </div>
        <div class="search-box">
            <asp:TextBox ID="txtSearch" runat="server" CssClass="form-control" placeholder="Enter Owner name"></asp:TextBox>
            <asp:Button ID="btnSearch" runat="server" Text="Search" CssClass="btn btn-primary" OnClick="btnSearch_Click" />
        </div>
        <asp:GridView ID="gvs" CssClass="tableCss"
            runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender">
            <Columns>
                <asp:TemplateField HeaderText="First Name">
                    <ItemTemplate>
                    
                       <%# Eval("Own_fname") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Last Name">
                    <ItemTemplate>
                          <%# Eval("Own_lname") %>
                       
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="NIC">
                    <ItemTemplate>
                        <%# Eval("Own_nic") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Email">
                    <ItemTemplate>
                        <%# Eval("Own_email") %>
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Profile Picture">
                    <ItemTemplate>
                        <asp:ImageButton ID="ImageButton1" runat="server"
                            ImageUrl='<%# Eval("Own_profpic", "~/images/{0}") %>'
                            Width="50" CssClass="img-thumbnail"
                            PostBackUrl='<%# Eval("Own_id", "view_client_profile.aspx?ID={0}") %>' />
                    </ItemTemplate>
                </asp:TemplateField>
                <asp:TemplateField HeaderText="Action">
                    <ItemTemplate>
                        <asp:LinkButton ID="lnkblock" CssClass='<%# Convert.ToString(Eval("Own_status")) == "True" ? "btn btn-danger block" : "btn btn-primary unblock" %>'
                            runat="server" OnClick="lnkblock_Click"
                            CommandArgument='<%# Eval("Own_id") %>'
                            Text='<%# Convert.ToString(Eval("Own_status")) == "True" ? "Block" : "Unblock" %>'>
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
                        url: '<%= ResolveUrl("Search_Owner.aspx/GetOwnerNames") %>',
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
