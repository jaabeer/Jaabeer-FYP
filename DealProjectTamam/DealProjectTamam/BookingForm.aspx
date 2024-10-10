<%@ Page Title="" Language="C#" MasterPageFile="~/Client1.Master" AutoEventWireup="true" CodeBehind="BookingForm.aspx.cs" Inherits="DealProjectTamam.BookingForm" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script src="https://code.jquery.com/jquery-3.6.0.min.js"></script>
    <script src="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/js/bootstrap.bundle.min.js"></script>
    <script type="text/javascript">
        function val_mobile_number(source, args) {
            args.IsValid = args.Value.length === 8;
        }

        function val_nic_number(source, args) {
            args.IsValid = args.Value.length === 14;
        }

        $(document).ready(function () {
            $(".edit").click(function (e) {
                e.preventDefault();
                var target = $(this).attr("href");
                $(target).toggleClass("d-none");
            });

            $(".btn-cancel").click(function (e) {
                e.preventDefault();
                $(this).closest(".edit_field").addClass("d-none");
            });

            // Handle tab navigation
            $(".nav-link").click(function (e) {
                e.preventDefault();
                $(".nav-link").removeClass("active");
                $(this).addClass("active");

                $(".tab-content").removeClass("active-tab");
                var target = $(this).attr("href");
                $(target).addClass("active-tab");
            });
        });
    </script>
    <link href="https://cdn.jsdelivr.net/npm/bootstrap@5.1.3/dist/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        body {
            background-color: #f8f9fa;
        }

        .container {
            background-color: #ffffff;
            padding: 20px;
            border-radius: 10px;
            box-shadow: 0 0 10px rgba(0, 0, 0, 0.1);
        }

        .nav-link {
            margin: 5px 0;
        }

        .card {
            margin-bottom: 20px;
        }

        .edit_field {
            margin-top: 15px;
        }

        .form-control {
            margin-bottom: 10px;
        }

        .actions {
            margin-top: 15px;
        }

        .btn-primary {
            background-color: #F97D09;
            border-color: #F97D09;
        }

        .btn-cancel {
            background-color: #6c757d;
            border-color: #6c757d;
        }

        .active-tab {
            display: block !important;
        }

        .tab-content {
            display: none;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <div class="row">
            <!-- Three-fourth content -->
            <div class="col-md-9">
                <nav class="nav nav-pills flex-column flex-sm-row">
                    <a class="flex-sm-fill text-sm-center nav-link active" href="#MyProfile">My Profile</a>
                    <a class="flex-sm-fill text-sm-center nav-link" href="#MyBookings">My Bookings</a>
                   <%-- <a class="flex-sm-fill text-sm-center nav-link" href="#MyHistory">My History</a>
                    <a class="flex-sm-fill text-sm-center nav-link" href="#MyReviews">My Reviews</a>--%>
                </nav>

                <div id="MyProfile" class="tab-content active-tab mt-3">
                    <h1>My Profile Details</h1>
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Personal Details</h2>
                            <asp:Label ID="lblmsg" runat="server" Text="" CssClass="font-weight-bold"></asp:Label>
                            <table class="table table-bordered">
                                <tr>
                                    <th>Profile Picture:</th>
                                    <td>
                                        <center>
                                            <asp:Image ID="Image1" runat="server" CssClass="border rounded" Width="100px" Height="100px" />
                                        </center>
                                        <div class="edit_field d-none" id="prof_pic">
                                            <label for="btn_upload_pp">Select a new profile picture:</label>
                                            <asp:FileUpload ID="btn_upload_pp" runat="server" CssClass="form-control" ValidationGroup="vg_pp" />
                                            <asp:Button ID="btn_pp" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btn_pp_Click" ValidationGroup="vg_pp" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#prof_pic" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- First Name -->
                                <tr>
                                    <th>First Name:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtfname" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field1">
                                            <label for="txt_newfname">Your new First Name:</label>
                                            <asp:TextBox class="form-control" ID="txt_newfname" runat="server" ValidationGroup="vg_newfname" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_newfname" ValidationGroup="vg_newfname" CssClass="text-danger" ErrorMessage="First Name is required." />
                                            <asp:RegularExpressionValidator ID="regFirst_name" ControlToValidate="txt_newfname" ValidationGroup="vg_newfname" ValidationExpression="^[a-zA-Z ,-]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets allowed for First Name" />
                                            <asp:Button ID="btnfname" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btnfname_Click" ValidationGroup="vg_newfname" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field1" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- Last Name -->
                                <tr>
                                    <th>Last Name:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtlname" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field2">
                                            <label for="txt_newlname">Your new Last Name:</label>
                                            <asp:TextBox class="form-control" ID="txt_newlname" runat="server" ValidationGroup="vg_newlname" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_newlname" ValidationGroup="vg_newlname" CssClass="text-danger" ErrorMessage="Last Name is required." />
                                            <asp:RegularExpressionValidator ID="regLast_name" ControlToValidate="txt_newlname" ValidationGroup="vg_newlname" ValidationExpression="^[a-zA-Z ,-]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets allowed for Last Name" />
                                            <asp:Button ID="btnLname" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btnLname_Click" ValidationGroup="vg_newlname" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field2" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- NIC -->
                                <tr>
                                    <th>NIC:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtnic" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field_nic">
                                            <label for="new_nic">Your new NIC:</label>
                                            <asp:TextBox class="form-control" ID="new_nic" runat="server" ValidationGroup="vg_nic" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="new_nic" ValidationGroup="vg_nic" CssClass="text-danger" ErrorMessage="NIC number is required." />
                                            <asp:CustomValidator ID="val_NIC" ControlToValidate="new_nic" ClientValidationFunction="val_nic_number" ValidationGroup="vg_nic" CssClass="text-danger" Text="NIC should contain 14 characters" runat="server" />
                                            <asp:RegularExpressionValidator ID="rvNIC" ControlToValidate="new_nic" ValidationGroup="vg_nic" ValidationExpression="^[a-zA-Z0-9]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets and numbers allowed for NIC" />
                                            <asp:Button ID="btnNic" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btnNic_Click" ValidationGroup="vg_nic" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field_nic" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- Address -->
                                <tr>
                                    <th>Address:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtadress" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field_address">
                                            <label for="txt_newadd">Your new address:</label>
                                            <asp:TextBox class="form-control" ID="txt_newadd" runat="server" ValidationGroup="vg_newadd" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_newadd" ValidationGroup="vg_newadd" CssClass="text-danger" ErrorMessage="Address is required." />
                                            <asp:Button ID="btn_address" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btn_address_Click" ValidationGroup="vg_newadd" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field_address" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- Mobile -->
                                <tr>
                                    <th>Mobile:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtmobile" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field_mobile">
                                            <label for="txt_newmobile">Your new mobile number:</label>
                                            <asp:TextBox class="form-control" ID="txt_newmobile" runat="server" ValidationGroup="vg_newmobile" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_newmobile" ValidationGroup="vg_newmobile" CssClass="text-danger" ErrorMessage="Mobile number is required." />
                                            <asp:CustomValidator ID="val_mobile" ControlToValidate="txt_newmobile" ClientValidationFunction="val_mobile_number" ValidationGroup="vg_newmobile" CssClass="text-danger" Text="Mobile should contain 8 digits" runat="server" />
                                            <asp:RegularExpressionValidator ID="rvMobile" ControlToValidate="txt_newmobile" ValidationGroup="vg_newmobile" ValidationExpression="^[0-9]+$" runat="server" CssClass="text-danger" ErrorMessage="Only numbers allowed for mobile" />
                                            <asp:Button ID="btnmobile" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btnmobile_Click" ValidationGroup="vg_newmobile" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field_mobile" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- Email -->
                                <tr>
                                    <th>Email:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtemail" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field_email">
                                            <label for="new_email">Your new email:</label>
                                            <asp:TextBox class="form-control" ID="new_email" runat="server" ValidationGroup="vg_newemail" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="new_email" ValidationGroup="vg_newemail" CssClass="text-danger" ErrorMessage="Email is required." />
                                            <asp:RegularExpressionValidator ID="rvEmail" ControlToValidate="new_email" ValidationGroup="vg_newemail" ValidationExpression="^[^@\s]+@[^@\s]+\.[^@\s]+$" runat="server" CssClass="text-danger" ErrorMessage="Invalid email format" />
                                            <asp:Button ID="btn_email" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btn_email_Click" ValidationGroup="vg_newemail" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field_email" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- Date of Birth -->
                                <tr>
                                    <th>Date of Birth:</th>
                                    <td>
                                        <asp:TextBox class="form-control" ID="txtdob" runat="server" ReadOnly="True"></asp:TextBox>
                                        <div class="edit_field d-none" id="field_dob">
                                            <label for="txt_newdob">Your new date of birth:</label>
                                            <asp:TextBox class="form-control" ID="txt_newdob" runat="server" ValidationGroup="vg_newdob" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txt_newdob" ValidationGroup="vg_newdob" CssClass="text-danger" ErrorMessage="Date of birth is required." />
                                            <asp:CompareValidator ID="cvDOB" ControlToValidate="txt_newdob" Operator="DataTypeCheck" Type="Date" runat="server" CssClass="text-danger" ErrorMessage="Invalid date format" />
                                            <asp:Button ID="btndob" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btndob_Click" ValidationGroup="vg_newdob" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field_dob" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>

                                <!-- Password -->
                                <tr>
                                    <th>Password:</th>
                                    <td>############
                                        <div class="edit_field d-none" id="field4">
                                            <label for="new_password">Your new password:</label>
                                            <asp:TextBox class="form-control" ID="old_password" placeholder="Old Password" TextMode="Password" runat="server" ValidationGroup="vg_pass" />
                                            <asp:TextBox class="form-control" ID="new_password" placeholder="New Password" TextMode="Password" runat="server" ValidationGroup="vg_pass" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="new_password" CssClass="text-danger" ErrorMessage="Password is required." />
                                            <asp:RegularExpressionValidator ID="regPassword" ControlToValidate="new_password" ValidationGroup="vg_pass" ValidationExpression="^(?=.*\d{2})(?=.*[a-zA-Z]{2}).{6,}$" runat="server" CssClass="text-danger" ErrorMessage="Password Not Strong" />
                                            <asp:TextBox class="form-control" ID="confirm_new_password" placeholder="Confirm New Password" TextMode="Password" runat="server" ValidationGroup="vg_pass" />
                                            <asp:RequiredFieldValidator ID="ReqCpassword" runat="server" ControlToValidate="confirm_new_password" CssClass="text-danger" ErrorMessage="Confirm Password field is required." />
                                            <asp:CompareValidator runat="server" ControlToCompare="new_password" ControlToValidate="confirm_new_password" CssClass="text-danger" ErrorMessage="The password and confirmation password do not match." />
                                            <asp:Button ID="btnpassword" CssClass="btn btn-primary mt-2" runat="server" Text="Save" OnClick="btnpassword_Click" ValidationGroup="vg_pass" />
                                            <a href="#" class="btn btn-cancel mt-2">Cancel</a>
                                        </div>
                                    </td>
                                    <td>
                                        <a href="#field4" class="btn btn-primary edit">Edit</a>
                                    </td>
                                </tr>
                            </table>
                        </div>
                    </div>
                </div>

                <div id="MyBookings" class="tab-content mt-3">
                    <h1>My Booking Details</h1>
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Ongoing Bookings</h2>
                            <asp:Label ID="lblonmsg" runat="server" Text=""></asp:Label>
                            <asp:Repeater ID="Repeater3" runat="server">
                                <ItemTemplate>
                                    <div class="b-info">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th>Booking Index</th>
                                                <td>
                                                    <asp:Label ID="lbl_index" runat="server" Text='<%# Eval("Bk_ref") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Villa</th>
                                                <td>
                                                    <asp:Label ID="lbl_villa" runat="server" Text='<%# Eval("Villa_name") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Check-in Date</th>
                                                <td>
                                                    <asp:Label ID="lbl_cin" runat="server" Text='<%# Eval("Bk_checkin") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Check-out Date</th>
                                                <td>
                                                    <asp:Label ID="lbl_cout" runat="server" Text='<%# Eval("Bk_checkout") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Total Price:</th>
                                                <td><strong>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Bk_amnt") %>'></asp:Label></strong>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Status:</th>
                                                <td><strong>
                                                    <asp:Label ID="Label2" runat="server" Text='<%# Eval("Bk_state") %>'></asp:Label></strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="actions">
                                      <%--  <a href="#" class="btn btn-primary">Change booking</a>
                                        <a href="#" class="btn btn-danger">Cancel booking</a>--%>
                                        <asp:LinkButton ID="btn_PDF" CssClass="btn btn-secondary" runat="server" OnClick="btn_PDF_Click" CommandArgument='<%# Eval("Bk_id") %>' Text="View confirmation" ValidationGroup="vg_on_going" />
                                     <%--   <a href="#" class="btn btn-secondary">Print confirmation</a>--%>
                                    </div>
                                    <hr />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <div id="MyHistory" class="tab-content mt-3">
                    <h1>My Booking History</h1>
                    <div class="card">
                        <div class="card-body">
                            <h2 class="card-title">Previous Bookings</h2>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="b-info">
                                        <table class="table table-bordered">
                                            <tr>
                                                <th>Booking Index</th>
                                                <td>
                                                    <asp:Label ID="lbl_index" runat="server" Text='<%# Eval("Bk_ref") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Villa</th>
                                                <td>
                                                    <asp:Label ID="lbl_villa" runat="server" Text='<%# Eval("Villa_name") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Check-in Date</th>
                                                <td>
                                                    <asp:Label ID="lbl_cin" runat="server" Text='<%# Eval("Bk_checkin") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Check-out Date</th>
                                                <td>
                                                    <asp:Label ID="lbl_cout" runat="server" Text='<%# Eval("Bk_checkout") %>'></asp:Label>
                                                </td>
                                            </tr>
                                            <tr>
                                                <th>Total Price:</th>
                                                <td><strong>
                                                    <asp:Label ID="Label1" runat="server" Text='<%# Eval("Bk_amnt") %>'></asp:Label></strong>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                    <div class="actions">
                                        <asp:Button ID="btnBagain" CssClass="btn btn-primary" runat="server" PostBackUrl='<%# "Villa_Details.aspx?Parameter="+ Eval("Villa_id") %>' Text="Book again" />
                                        <asp:LinkButton ID="btnRemove" CssClass="btn btn-danger" runat="server" CommandArgument='<%# Eval("Bk_id")%>' OnClick="btnRemove_Click" Text="Remove from list" />
                                    </div>
                                    <hr />
                                </ItemTemplate>
                            </asp:Repeater>
                        </div>
                    </div>
                </div>

                <div id="MyReviews" class="tab-content mt-3">
                    <h1>My Reviews and Ratings</h1>
                    <asp:Repeater ID="Repeater2" runat="server">
                        <ItemTemplate>
                            <div class="card">
                                <div class="card-body">
                                    <h2 class="card-title"><%# Eval("Villa_name") %></h2>
                                    <div class="row">
                                        <div class="col-md-3">
                                            <asp:Image ID="Image2" runat="server" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/main/" + Eval("Villa_image") %>' CssClass="img-thumbnail" Width="100px" Height="100px" />
                                        </div>
                                        <div class="col-md-3">
                                            <h3>Rating</h3>
                                            <span class="badge badge-primary"><%# Eval("Rating") %></span>
                                            <span>/ 5</span>
                                        </div>
                                        <div class="col-md-6">
                                            <h3>Review</h3>
                                            <div class="rev pro">
                                                <p><%# Eval("Positive_Review") %></p>
                                            </div>
                                            <div class="rev con">
                                                <p><%# Eval("Negative_Review") %></p>
                                            </div>
                                        </div>
                                    </div>
                                    <div class="actions mt-2">
                                        <a href="#" class="btn btn-secondary">Update</a>
                                        <a href="#" class="btn btn-danger">Delete</a>
                                    </div>
                                </div>
                            </div>
                        </ItemTemplate>
                    </asp:Repeater>
                </div>
            </div>
        </div>
    </div>
</asp:Content>
