<%@ Page Title="" Language="C#" MasterPageFile="~/DealTamam2.Master" AutoEventWireup="true" CodeBehind="RegisterClient.aspx.cs" Inherits="DealProjectTamam.RegisterClient" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" rel="stylesheet" />
    <script type="text/javascript">

        function val_mobile_number(source, args) {
            if (args.Value.length != 8)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function val_nic_number(source, args) {
            if (args.Value.length != 14)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function val_office_number(source, args) {
            if (args.Value.length != 0)
                if (args.Value.length != 7)
                    args.IsValid = false;
                else
                    args.IsValid = true;
            else
                args.IsValid = true;
        }

    </script>
    <style>
        .f-item {
            margin-bottom: 15px;
        }
        .gradient-button {
            background: linear-gradient(to right, #6a11cb, #2575fc);
            border: none;
            color: white;
            padding: 10px 20px;
            text-align: center;
            text-decoration: none;
            display: inline-block;
            font-size: 16px;
            margin: 4px 2px;
            cursor: pointer;
            border-radius: 4px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="main container">
        <div class="wrap">
            <div class="row">
                <!--three-fourth content-->
                <div class="col-md-9">

                    <fieldset>
                        <h2>Client Details</h2>

                        <asp:ValidationSummary runat="server" CssClass="text-danger" />
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblResult1" Display="Dynamic" CssClass="col-form-label text-danger"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblResult2" Display="Dynamic" CssClass="col-form-label text-danger"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblResult3" Display="Dynamic" CssClass="col-form-label text-danger"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblResult4" Display="Dynamic" CssClass="col-form-label text-danger"></asp:Label>
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" ID="lblResult5" Display="Dynamic" CssClass="col-form-label text-danger"></asp:Label>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtFirstName" Display="Dynamic">First name</asp:Label>
                                <asp:TextBox runat="server" ID="txtFirstName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="First Name is required." />
                                <asp:RegularExpressionValidator ID="regFirst_name" ControlToValidate="txtFirstName" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets allowed for First Name"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtLastName" Display="Dynamic">Last name</asp:Label>
                                <asp:TextBox runat="server" ID="txtLastName" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Last Name is required." />
                                <asp:RegularExpressionValidator ID="regLast_name" ControlToValidate="txtLastName" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets allowed for Last Name"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtNIC" Display="Dynamic">National Identity Card Number</asp:Label>
                                <asp:TextBox runat="server" ID="txtNIC" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtNIC" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="NIC number is required." />
                                <asp:CustomValidator ID="val_NIC" ControlToValidate="txtNIC" Display="Dynamic" CssClass="text-danger" Text="(NIC should contain 14 characters)"
                                    ClientValidationFunction="val_nic_number"
                                    runat="server" />
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtDOB" Display="Dynamic">Date of birth</asp:Label>
                                <asp:TextBox runat="server" ID="txtDOB" TextMode="Date" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtDOB" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Date of birth is required." />
                                <asp:RangeValidator ID="rvdob" runat="server"
                                    ControlToValidate="txtDOB"
                                    Text="Out of Range, You should be 18 to 45 years old" CssClass="text-danger"
                                    ErrorMessage="You should be 18 to 45 years old" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="rdlGender" Display="Dynamic">Gender</asp:Label>
                            <asp:RadioButtonList ID="rdlGender" runat="server" RepeatDirection="Horizontal" CssClass="form-control">
                                <asp:ListItem Text="Male" Value="Male" />
                                <asp:ListItem Text="Female" Value="Female" />
                            </asp:RadioButtonList>
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="rdlGender" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Gender is required." />
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtAddress" Display="Dynamic">Address</asp:Label>
                                <asp:TextBox runat="server" ID="txtAddress" TextMode="MultiLine" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtAddress" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Address is required." />
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtEmail" Display="Dynamic">Email Address</asp:Label>
                                <asp:TextBox runat="server" ID="txtEmail" Display="Dynamic" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Email is required." />
                                <asp:RegularExpressionValidator
                                    ID="regEmail"
                                    ControlToValidate="txtEmail"
                                    Text="(Invalid email)"
                                    ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    runat="server" CssClass="text-danger" Display="Dynamic" />
                            </div>
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtMobile" Display="Dynamic">Mobile Number</asp:Label>
                                <asp:TextBox runat="server" ID="txtMobile" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Mobile Number is required." />
                                <asp:CustomValidator ID="val_mobNumber" ControlToValidate="txtMobile" CssClass="text-danger" Text="(Mobile Phone Number must contain 8 numbers)"
                                    ClientValidationFunction="val_mobile_number"
                                    runat="server" />
                            </div>

                           
                        </div>

                        <div class="form-row">
                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtPassword" Display="Dynamic">Password</asp:Label>
                                <asp:TextBox runat="server" ID="txtPassword" Type="Password" CssClass="form-control" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtPassword" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Password is required." />
                                <asp:RegularExpressionValidator ID="regPassword" ControlToValidate="txtPassword" Display="Dynamic"
                                    ValidationExpression="^(?=.*\d{2})(?=.*[a-zA-Z]{2}).{6,}$" runat="server" CssClass="text-danger" ErrorMessage="Password Not Strong"></asp:RegularExpressionValidator>
                            </div>

                            <div class="form-group col-md-6">
                                <asp:Label runat="server" AssociatedControlID="txtConfirmPassword" Display="Dynamic">Confirm Password</asp:Label>
                                <asp:TextBox runat="server" ID="txtConfirmPassword" Type="Password" CssClass="form-control"/>
                                <asp:RequiredFieldValidator ID="ReqCpassword" runat="server" ControlToValidate="txtConfirmPassword"
                                    CssClass="text-danger" Display="Dynamic"
                                    ErrorMessage="Confirm Password field is required." />
                                <asp:CompareValidator runat="server" ControlToCompare="txtPassword"
                                    ControlToValidate="txtConfirmPassword"
                                    CssClass="text-danger" Display="Dynamic"
                                    ErrorMessage="The password and confirmation password do not match." />
                            </div>
                        </div>

                        <div class="form-group">
                            <asp:Button ID="btnRegister" runat="server" Text="Register" CssClass="gradient-button" OnClick="btnRegister_Click"/>
                            <asp:Button ID="btnsuccess" runat="server" Visible="false" Text="Registration successful" CssClass="gradient-button" CausesValidation="false"/>
                        </div>
                    </fieldset>
                </div>
                <!--//three-fourth content-->

                <!--right sidebar-->
              
                <!--//right sidebar-->
            </div>
        </div>
    </main>
</asp:Content>
