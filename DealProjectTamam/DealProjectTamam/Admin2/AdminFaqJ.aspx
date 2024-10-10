<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="AdminFaqJ.aspx.cs" Inherits="DealProjectTamam.Admin_J.AdminFaqJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
            background-color: #e6f7ff;
        }

        .content-header {
            padding: 20px;
            background-color: #1e3d7b;
            color: white;
            text-align: center;
            margin-bottom: 20px;
        }

        .table-responsive {
            background: #cce7ff;
            padding: 15px;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .table {
            width: 100%;
            margin-bottom: 20px;
            background-color: #f0f8ff;
        }

        th {
            background-color: #1e3d7b;
            color: white;
        }

        .table-bordered th, .table-bordered td {
            border: 1px solid #87cefa;
            padding: 8px;
            text-align: left;
        }

        .btn {
            padding: 8px 15px;
            margin-right: 10px;
            border-radius: 4px;
            cursor: pointer;
            color: white;
        }

        .btn-primary { background-color: #1e3d7b; }
        .btn-warning { background-color: #5f9ea0; }
        .btn-danger { background-color: #4682b4; }
        .btn-secondary { background-color: #87cefa; }

        .btn:not(:last-child) {
            margin-bottom: 10px;
        }

        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #87cefa;
            border-radius: 4px;
            box-sizing: border-box;
            margin-bottom: 10px;
            font-size: 16px;
            background-color: #f0f8ff;
        }

        .text-danger {
            color: #4682b4;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <br />
    <h4 style="font-family: Arial, sans-serif; color: #1e3d7b; font-size: 20px; font-weight: bold; text-transform: uppercase;">Add, modify or delete FAQ.</h4>

    <hr />
    <fieldset>
        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdd" />
        <div class="card shadow mb-4">
            <div class="card-body">
                <div class="table-responsive">
                    <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                    <br />
                    <table class="table table-bordered" width="100%" cellspacing="0">
                        <thead>
                            <tr>
                                <th>Field</th>
                                <th>Value</th>
                            </tr>
                        </thead>
                        <tr>
                            <td>
                                <asp:Label ID="lblFaq_id" runat="server" Text="FAQ ID" Visible="false"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtFaq_id" runat="server" Enabled="false" Visible="false" />
                            </td>
                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblcat" runat="server" Text="FAQ Category"></asp:Label>
                            </td>
                            <td>
                                <asp:DropDownList ID="ddlcat" runat="server" CssClass="form-control">
                                    <asp:ListItem Value="-1">Select a category for your issue</asp:ListItem>
                                </asp:DropDownList>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlcat" InitialValue="-1" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="FAQ Category is required." ValidationGroup="vgFAQ" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblquestion" runat="server" Text="FAQ Topic"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtques" runat="server" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtques" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="FAQ Topic is required." ValidationGroup="vgFAQ" />
                            </td>

                        </tr>
                        <tr>
                            <td>
                                <asp:Label ID="lblanswer" runat="server" Text="FAQ Answer"></asp:Label>
                            </td>
                            <td>
                                <asp:TextBox ID="txtanswer" runat="server" TextMode="MultiLine" CssClass="form-control"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtanswer" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="FAQ Answer is required." ValidationGroup="vgFAQ" />
                            </td>

                        </tr>
                        <tr>
                            <td colspan="2">
                                <asp:Button ID="btnInsert" runat="server"
                                    Text="Insert" ValidationGroup="vgFAQ" CssClass="btn btn-primary" OnClick="btnInsert_Click" />
                                <asp:Button ID="btnUpdate" runat="server"
                                    Text="Update" ValidationGroup="vgFAQ" CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
                                <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger"
                                    OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                    Text="Delete" ValidationGroup="vgFAQ" CausesValidation="false" OnClick="btnDelete_Click" />
                                <asp:Button ID="btnCancel" runat="server"
                                    Text="Cancel" CausesValidation="false" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                            </td>
                        </tr>
                    </table>
                    <br />
                    <!-- set the primary for the category table as the DataKeynames-->

                    <asp:GridView ID="gvs_1" OnSelectedIndexChanged="gvs_SelectedIndexChanged" DataKeyNames="faq_id" AutoGenerateColumns="false"
                        Width="100%" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_PageIndexChanging" runat="server" BorderWidth="1px">
                        <HeaderStyle BackColor="#1e3d7b" ForeColor="White" Font-Bold="true" Height="30" />
                        <AlternatingRowStyle BackColor="#cce7ff" />
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                            NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                        <Columns>
                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary" CommandName="Select" Text="Select" Font-Bold="True" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FAQ Topic">
                                <ItemTemplate>
                                    <!-- display the category name -->
                                    <asp:Label ID="lblfaqtopic" Text='<%#Eval("faq_question")%>'
                                        runat="server" Font-Size="Medium" Font-Bold="True" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FAQ Answer">
                                <ItemTemplate>
                                    <!-- display the category name -->
                                    <asp:Label ID="lblfaqanswer" Text='<%#Eval("faq_answer")%>'
                                        runat="server" Font-Size="Medium" Font-Bold="True" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="FAQ Category" HeaderStyle-HorizontalAlign="Center">
                                <ItemTemplate>
                                    <!-- display the category name -->
                                    <asp:Label ID="lblfaqcat" Text='<%#Eval("Category")%>'
                                        runat="server" Font-Size="Medium" Font-Bold="True" />
                                    <asp:Label ID="lblcatid" Text='<%#Eval("faq_cat")%>'
                                        runat="server" Font-Size="Medium" Font-Bold="True" Visible="false" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </div>
            </div>
        </div>
    </fieldset>
</asp:Content>
