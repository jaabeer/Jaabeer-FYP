<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="NewSupport.aspx.cs" Inherits="DealProjectTamam.AdminS.NewSupport" %>
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
    <h4>Support Requests</h4>
    <hr />
    <asp:GridView ID="gvs" runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static">
        <Columns>
            <asp:TemplateField HeaderText="Reference Number">
                <ItemTemplate>
                    <%# Eval("reference") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <%# Eval("firstname") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <%# Eval("lastname") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <%# Eval("Category") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <%# Eval("title") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# Eval("status_name") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Left"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <asp:LinkButton ID="lnkblock" CssClass="btn-view"
                        runat="server" 
                        Text="View Details"
                        PostBackUrl='<%# Eval("id", "New_Support_Details.aspx?ID={0}") %>'></asp:LinkButton>
                </ItemTemplate>
            </asp:TemplateField>
        </Columns>
    </asp:GridView>
</asp:Content>
