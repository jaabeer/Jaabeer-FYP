<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="AdminFacilitiesJ.aspx.cs" Inherits="DealProjectTamam.Admin_J.AdminFacilitiesJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f4f4f9;
        }

        .content-header {
            padding: 25px;
            background-color: #007bff;
            color: white;
            text-align: center;
            margin-bottom: 25px;
            border-radius: 8px;
        }

        .card {
            background: #ffffff;
            padding: 20px;
            border-radius: 8px;
            box-shadow: 0 5px 10px rgba(0,0,0,0.1);
            margin-bottom: 25px;
        }

        .table {
            width: 100%;
            margin-bottom: 25px;
            border-collapse: collapse;
        }

        th, td {
            border: 1px solid #ddd;
            padding: 12px;
            text-align: left;
        }

        th {
            background-color: #007bff;
            color: white;
        }

        .btn {
            padding: 10px 20px;
            margin-right: 10px;
            border-radius: 5px;
            cursor: pointer;
            border: none;
        }

        .btn-primary { background-color: #007bff; color: white; }
        .btn-update { background-color: #28a745; color: white; } /* Green for update */
        .btn-delete { background-color: #dc3545; color: white; } /* Red for delete */
        .btn-secondary { background-color: #6c757d; color: white; }

        .btn:not(:last-child) {
            margin-bottom: 10px;
        }

        .form-control {
            width: 100%;
            padding: 12px;
            border: 1px solid #ccc;
            border-radius: 5px;
            margin-bottom: 10px;
            font-size: 16px;
        }

        .text-danger {
            color: #d9534f;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4 class="content-header">Add, modify or delete Room Facilities.</h4>

    <hr />
    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdd" />
    <div class="card">
        <div class="card-body">
            <div class="table-responsive">
                <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
                <br />
                <table class="table">
                    <thead>
                        <tr>
                            <th>Field</th>
                            <th>Value</th>
                        </tr>
                    </thead>
                    <tr>
                        <td>
                            <asp:Label ID="lblFac_id" runat="server" Text="Facility ID" Visible="false"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFac_id" runat="server" Enabled="false" Visible="false" CssClass="form-control"/>
                        </td>
                    </tr>
                    <tr>
                        <td>
                            <asp:Label ID="lblFac_name" runat="server" Text="Facility Name"></asp:Label>
                        </td>
                        <td>
                            <asp:TextBox ID="txtFac_name" runat="server" CssClass="form-control"></asp:TextBox>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFac_name" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Facility name is required." ValidationGroup="vgAdd" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnInsert" runat="server"
                                Text="Insert" ValidationGroup="vgAdd" CssClass="btn btn-primary" OnClick="btnInsert_Click" />
                            <asp:Button ID="btnUpdate" runat="server"
                                Text="Update" ValidationGroup="vgAdd" CssClass="btn btn-update" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-delete"
                                OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                Text="Delete" ValidationGroup="vgAdd" CausesValidation="false" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnCancel" runat="server"
                                Text="Cancel" CausesValidation="false" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </table>
                <br />
                <!-- set the primary for the category table as the DataKeynames-->
                <asp:GridView ID="gvs" OnSelectedIndexChanged="gvs_SelectedIndexChanged" DataKeyNames="Rooms_Fac_id" AutoGenerateColumns="false"
                    AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_PageIndexChanging" runat="server" BorderWidth="1px">
                    <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="true" Height="30" />
                    <AlternatingRowStyle BackColor="#f9f9f9" />
                    <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                        NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                    <Columns>
                        <asp:TemplateField>
                            <ItemTemplate>
                                <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-primary" CommandName="Select" Text="Select" Font-Bold="True" />
                            </ItemTemplate>
                        </asp:TemplateField>
                        <asp:TemplateField HeaderText="Rooms Facility Name">
                            <ItemTemplate>
                                <!-- display the category name -->
                                <asp:Label ID="lblFacName" Text='<%#Eval("Rooms_Fac_name")%>'
                                    runat="server" Font-Size="Medium" Font-Bold="True" />
                            </ItemTemplate>
                        </asp:TemplateField>
                    </Columns>
                </asp:GridView>
            </div>
        </div>
    </div>
</asp:Content>