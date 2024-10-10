<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="Notif_all_Tickets.aspx.cs" Inherits="DealProjectTamam.AdminS.Notif_all_Tickets" %>
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

    <br />
    <h4>All Support Tickets </h4>
    <hr />
    <asp:GridView ID="gvs" CssClass="table table-striped table-bordered"
        runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static">
        <Columns>

            <asp:TemplateField HeaderText="First Name">
                <ItemTemplate>
                    <%# Eval("firstname") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Last Name">
                <ItemTemplate>
                    <%# Eval("lastname") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Category">
                <ItemTemplate>
                    <%# Eval("Category") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            <asp:TemplateField HeaderText="Title">
                <ItemTemplate>
                    <%# Eval("title") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

             <asp:TemplateField HeaderText="Status">
                <ItemTemplate>
                    <%# Eval("status") %>
                </ItemTemplate>
                <ItemStyle HorizontalAlign="Justify"></ItemStyle>
            </asp:TemplateField>

            

            <asp:TemplateField HeaderText="Action">
                <ItemTemplate>
                    <%-- Assign the User_Id to the link button using the CommandArgument --%>
                    <asp:LinkButton ID="lnkblock" CssClass="btn alert-success"
                        runat="server" 
                        Text="View Details"
                        PostBackUrl='<%# Eval("id", "New_sup_details.aspx?ID={0}") %>'
                        Font-Bold="True"></asp:LinkButton>

                        
                </ItemTemplate>
            </asp:TemplateField>

        </Columns>
    </asp:GridView>

</asp:Content>

