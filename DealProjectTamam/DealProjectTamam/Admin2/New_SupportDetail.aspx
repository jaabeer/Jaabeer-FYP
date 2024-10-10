<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="New_SupportDetail.aspx.cs" Inherits="DealProjectTamam.Admin2.New_SupportDetail" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        body {
            font-family: Arial, sans-serif;
            background-color: #f8f9fa;
        }
        .container {
            max-width: 1200px;
            margin: auto;
            padding: 20px;
        }
        .header {
            text-align: center;
            padding: 20px;
            background-color: #007bff;
            color: white;
            border-radius: 8px;
            margin-bottom: 20px;
        }
        .card {
            background-color: white;
            border: 1px solid #ddd;
            border-radius: 8px;
            box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
            margin-bottom: 20px;
            padding: 20px;
        }
        .card h6 {
            font-size: 1.25rem;
            margin-bottom: 15px;
            color: #333;
        }
        .form-group {
            margin-bottom: 15px;
        }
        .form-control {
            width: 100%;
            padding: 10px;
            border: 1px solid #ddd;
            border-radius: 4px;
        }
        .form-control[readonly] {
            background-color: #e9ecef;
        }
        .btn {
            padding: 10px 20px;
            border: none;
            border-radius: 4px;
            color: black !important; /* Black color for button text */
            cursor: pointer;
            background-color: #007bff !important; /* Blue color for buttons */
        }
        .comments-section {
            margin-top: 20px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container">
        <div class="header">
            <h1>Support Issues</h1>
        </div>
        <div class="card">
            <h6>User Details</h6>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtid" Display="Dynamic" ForeColor="Black">ID</asp:Label>
                <asp:TextBox runat="server" ID="txtid" TextMode="MultiLine" class="form-control" ForeColor="Black" Visible="false" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtfname" Display="Dynamic" ForeColor="Black">First Name</asp:Label>
                <asp:TextBox runat="server" ID="txtfname" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtlname" Display="Dynamic" ForeColor="Black">Last Name</asp:Label>
                <asp:TextBox runat="server" ID="txtlname" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtemail" Display="Dynamic" ForeColor="Black">Email</asp:Label>
                <asp:TextBox runat="server" ID="txtemail" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtphone" Display="Dynamic" ForeColor="Black">Contact Number</asp:Label>
                <asp:TextBox runat="server" ID="txtphone" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
        </div>
        <div class="card">
            <h6>Issue Details</h6>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtcategory" Display="Dynamic" ForeColor="Black">Category</asp:Label>
                <asp:TextBox runat="server" ID="txtcategory" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txttitle" Display="Dynamic" ForeColor="Black">Title</asp:Label>
                <asp:TextBox runat="server" ID="txttitle" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtdescription" Display="Dynamic" ForeColor="Black">Description</asp:Label>
                <asp:TextBox runat="server" ID="txtdescription" TextMode="MultiLine" class="form-control" ForeColor="Black" ReadOnly="true" />
            </div>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="ddlstatus" Display="Dynamic" ForeColor="Black">Status</asp:Label>
                <asp:DropDownList runat="server" ID="ddlstatus" class="form-control" ForeColor="Black" />
            </div>
        </div>
        <div class="card comments-section">
            <h6>Comments</h6>
            <asp:Repeater ID="Repeater1" runat="server">
                <ItemTemplate>
                    <div>
                        <asp:Label ID="lbldate" runat="server" Text='<%# Eval("Date") %>' CssClass="text-xs"></asp:Label> <br />
                        <asp:Label ID="lblusername" runat="server" Text='<%# Eval("Username") %>' CssClass="badge-light"></asp:Label> | <asp:Label ID="lblcomment" runat="server" Text='<%# Eval("Comment") %>'></asp:Label> <br /> <br />
                    </div>
                </ItemTemplate>
            </asp:Repeater>
            <div class="form-group">
                <asp:Label runat="server" AssociatedControlID="txtcomment" Display="Dynamic" ForeColor="Black">Add Comment</asp:Label>
                <asp:TextBox runat="server" ID="txtcomment" TextMode="MultiLine" class="form-control" ForeColor="Blue" />
            </div>
            <div class="form-group">
                <asp:Button ID="btncomment" CssClass="btn" OnClick="btncomment_Click" runat="server" Text="Add Comment" />
                <asp:Label ID="lblcommentmsg" runat="server" Text=""></asp:Label>
            </div>
        </div>
        <center>
            <asp:LinkButton ID="Back" runat="server" Text="Back" PostBackUrl="~/Admin2/Newsupport.aspx" CssClass="btn" Font-Size="Large" Font-Bold="True" CausesValidation="false"></asp:LinkButton>
        </center>
    </div>
</asp:Content>