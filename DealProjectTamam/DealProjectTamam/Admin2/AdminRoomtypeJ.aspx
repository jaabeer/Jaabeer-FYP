<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="AdminRoomtypeJ.aspx.cs" Inherits="DealProjectTamam.Admin2.AdminRoomtypeJ" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: 'Arial', sans-serif;
            background-color: #f8f9fa;
            margin: 0;
            padding: 0;
        }

        .content-header {
            padding: 20px;
            background-color: #6c757d;
            color: white;
            text-align: center;
        }

        .card {
            background-color: #fff;
            border-radius: 8px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
            margin: 20px auto;
            max-width: 800px;
            padding: 20px;
        }

        .table-responsive {
            margin: 20px 0;
        }

        .table {
            width: 100%;
            border-collapse: collapse;
            margin-bottom: 20px;
        }

        .table th, .table td {
            border: 1px solid #dee2e6;
            padding: 12px;
            text-align: left;
        }

        .table th {
            background-color: #343a40;
            color: white;
        }

        .table tbody tr:nth-of-type(odd) {
            background-color: #f8f9fa;
        }

        .btn {
            display: inline-block;
            font-weight: 400;
            color: #212529;
            text-align: center;
            vertical-align: middle;
            user-select: none;
            background-color: transparent;
            border: 1px solid transparent;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            border-radius: 0.25rem;
            transition: color 0.15s ease-in-out, background-color 0.15s ease-in-out, border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            margin-right: 10px;
        }

        .btn-primary {
            color: #fff;
            background-color: #007bff;
            border-color: #007bff;
        }

        .btn-warning {
            color: #212529;
            background-color: #ffc107;
            border-color: #ffc107;
        }

        .btn-danger {
            color: #fff;
            background-color: #dc3545;
            border-color: #dc3545;
        }

        .btn-secondary {
            color: #fff;
            background-color: #6c757d;
            border-color: #6c757d;
        }

        .form-control {
            display: block;
            width: 100%;
            padding: 0.375rem 0.75rem;
            font-size: 1rem;
            line-height: 1.5;
            color: #495057;
            background-color: #fff;
            background-clip: padding-box;
            border: 1px solid #ced4da;
            border-radius: 0.25rem;
            transition: border-color 0.15s ease-in-out, box-shadow 0.15s ease-in-out;
            margin-bottom: 10px;
        }

        .text-danger {
            color: #dc3545;
        }

        .btn:not(:last-child) {
            margin-bottom: 10px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="content-header">
        <h4>Add, modify or delete Room Type.</h4>
    </div>
    <div class="card">
        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdd" />
        <asp:Label ID="lblMsg" runat="server" Text=""></asp:Label>
        <div class="table-responsive">
            <table class="table table-bordered">
                <thead>
                    <tr>
                        <th>Field</th>
                        <th>Value</th>
                    </tr>
                </thead>
                <tbody>
                    <tr>
                        <td><asp:Label ID="lblFac_id" runat="server" Text="Facility ID" Visible="false"></asp:Label></td>
                        <td><asp:TextBox ID="txtFac_id" runat="server" Enabled="false" Visible="false" CssClass="form-control" /></td>
                    </tr>
                    <tr>
                        <td><asp:Label ID="lblFac_name" runat="server" Text="Room Type"></asp:Label></td>
                        <td>
                            <asp:TextBox ID="txtFac_name" runat="server" CssClass="form-control" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFac_name" Display="Dynamic" CssClass="text-danger" ErrorMessage="Facility name is required." ValidationGroup="vgAdd" />
                        </td>
                    </tr>
                    <tr>
                        <td colspan="2">
                            <asp:Button ID="btnInsert" runat="server" Text="Insert" ValidationGroup="vgAdd" CssClass="btn btn-primary" OnClick="btnInsert_Click" />
                            <asp:Button ID="btnUpdate" runat="server" Text="Update" ValidationGroup="vgAdd" CssClass="btn btn-warning" OnClick="btnUpdate_Click" />
                            <asp:Button ID="btnDelete" runat="server" CssClass="btn btn-danger" OnClientClick="return confirm('Are you sure you want to delete this record?')" Text="Delete" ValidationGroup="vgAdd" CausesValidation="false" OnClick="btnDelete_Click" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-secondary" OnClick="btnCancel_Click" />
                        </td>
                    </tr>
                </tbody>
            </table>
            <asp:GridView ID="gvs" OnSelectedIndexChanged="gvs_SelectedIndexChanged" DataKeyNames="Rmtype_id" AutoGenerateColumns="false" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_PageIndexChanging" runat="server" CssClass="table">
                <HeaderStyle BackColor="#343a40" ForeColor="white" Font-Bold="true" Height="30" />
                <AlternatingRowStyle BackColor="#f8f9fa" />
                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                <Columns>
                    <asp:TemplateField>
                        <ItemTemplate>
                            <asp:LinkButton ID="lbtnSelect" runat="server" CssClass="btn btn-info" CommandName="Select" Text="Select" Font-Bold="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                    <asp:TemplateField HeaderText="Rooms Facility Name">
                        <ItemTemplate>
                            <asp:Label ID="lblFacName" Text='<%#Eval("Rmtype_name")%>' runat="server" Font-Size="Medium" Font-Bold="True" />
                        </ItemTemplate>
                    </asp:TemplateField>
                </Columns>
            </asp:GridView>
        </div>
    </div>
</asp:Content>