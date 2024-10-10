<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="New_vdetails.aspx.cs" Inherits="DealProjectTamam.AdminS.New_vdetails" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <style>
        .header {
            background-color: #007bff;
            color: white;
            padding: 15px;
            border-radius: 5px;
        }

        .card {
            border: none;
            border-radius: 5px;
            box-shadow: 0 4px 8px rgba(0, 0, 0, 0.1);
        }

        .form-control {
            border-radius: 0.25rem;
        }

        .btn-custom {
            background-color: #007bff;
            color: white;
            border-radius: 0.25rem;
            padding: 10px 20px;
        }

        .btn-custom:hover {
            background-color: #0056b3;
        }

        .btn-danger-custom {
            background-color: #dc3545;
            color: white;
            border-radius: 0.25rem;
            padding: 10px 20px;
        }

        .btn-danger-custom:hover {
            background-color: #bd2130;
        }

        .text-danger {
            color: #dc3545 !important;
        }

        .text-primary {
            color: #007bff !important;
        }

        .table-custom th {
            background-color: #007bff;
            color: white;
            text-align: left;
            padding: 10px;
        }

        .table-custom td {
            padding: 10px;
            border: 1px solid #ddd;
        }

        .main-picture {
            height: 200px;
            width: 300px;
            border-style: inset;
            border-color: #666666;
            border-width: 5px;
        }

        .description-box {
            columns: 54;
          
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <div class="container mt-5">
        <!-- Page Heading -->
        <h1 class="header">Villa Details</h1>

        <div class="row mt-4">
            <div class="col-lg-6">
                <!-- Owner Location and Price -->
                <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="text-primary">Owner, Location and Price</h6>
                    </div>

                    <div class="card-body">
                        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="VgVillaDetails" />
                        <div>
                            <div>
                                <asp:Label runat="server" ID="lblMsg" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                        </div>

                        <!-- Villa id hidden -->
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_id" Display="Dynamic" Visible="false">Villa ID</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_id" CssClass="form-control" Visible="false" />
                        </div>

                        <!-- Owner id hidden -->
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtOwn_id" Display="Dynamic" Visible="false">Owner ID</asp:Label>
                            <asp:TextBox runat="server" ID="txtOwn_id" CssClass="form-control" Visible="false" />
                        </div>
                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtOwn_email" Display="Dynamic" Visible="false">Owner Email</asp:Label>
                            <asp:TextBox runat="server" ID="txtOwn_email" CssClass="form-control" Visible="false" />
                        </div>

                        <div class="form-group">
                            <a href="#" class="btn btn-light btn-icon-split">
                                <span class="icon text-gray-600">
                                    <i class="fas fa-arrow-right"></i>
                                </span>
                                <span id="sp_owner" class="text">
                                    <asp:Label ID="lbl_owner" runat="server"></asp:Label>
                                </span>
                            </a>
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_name" Display="Dynamic" ForeColor="Black">Villa Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_name" CssClass="form-control" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_name" Display="Dynamic" CssClass="text-danger" ErrorMessage="Villa Name is required." ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_phone" Display="Dynamic" ForeColor="Black">Phone number</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_phone" TextMode="Number" MaxLength="7" CssClass="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_email" Display="Dynamic" ForeColor="Black">Villa Email</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_email" CssClass="form-control" ReadOnly="true" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RegularExpressionValidator ID="regEmail" ControlToValidate="txtProp_email" Text="(Invalid email)" ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" CssClass="text-danger" Display="Dynamic" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_street" Display="Dynamic" ForeColor="Black">Street</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_street" CssClass="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_street" Display="Dynamic" ForeColor="Black" CssClass="text-danger" ErrorMessage="Street is required." ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_town" Display="Dynamic" ForeColor="Black">Town</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_town" CssClass="form-control" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_town" Display="Dynamic" ForeColor="Black" CssClass="text-danger" ErrorMessage="Town is required." ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="ddlDistrict" Display="Dynamic" ForeColor="Black">District</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlDistrict" CssClass="form-control" Display="Dynamic" ForeColor="Black" ValidationGroup="VgVillaDetails">
                                <asp:ListItem Value="-1">Select District</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlDistrict" InitialValue="-1" runat="server" CssClass="text-danger" ErrorMessage="District required." Display="Dynamic" ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_priceday" Display="Dynamic" ForeColor="Black">Price per day</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_priceday" TextMode="Number" MaxLength="4" CssClass="form-control" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceday" Display="Dynamic" CssClass="text-danger" ErrorMessage="Price per day is required." ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_priceweek" Display="Dynamic" ForeColor="Black">Price per week</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_priceweek" TextMode="Number" MaxLength="5" CssClass="form-control" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceweek" Display="Dynamic" CssClass="text-danger" ErrorMessage="Price per week is required." ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_pricemonth" Display="Dynamic" ForeColor="Black">Price per month</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_pricemonth" TextMode="Number" MaxLength="6" CssClass="form-control" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_pricemonth" Display="Dynamic" CssClass="text-danger" ErrorMessage="Price per month is required." ValidationGroup="VgVillaDetails" />
                        </div>

                        <div class="form-group">
                            <asp:Label runat="server" AssociatedControlID="txtProp_postcode" Display="Dynamic" ForeColor="Black">Postal Code</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_postcode" TextMode="Number" MaxLength="5" CssClass="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                        </div>

                    </div>
                </div>
                <!-- end of Owner Location and Price -->

                <!-- Start of Description -->
                <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="text-primary">Villa Description</h6>
                    </div>
                    <div class="card-body">
                        <div class="form-group">
                            <asp:Label ID="lbl_Prop_Desc" runat="server" Text=""></asp:Label>
                            <div class="table-responsive">
                                <asp:TextBox runat="server" ID="txt_Description" TextMode="MultiLine" CssClass="form-control description-box" ForeColor="#003366" BorderColor="#336699" BackColor="White" />
                            </div>
                        </div>
                    </div>
                </div>
                <!-- end of Description -->

                <!-- Villa Accessibility -->
                <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="text-primary">Villa Accessibility</h6>
                    </div>
                    <div class="card-body">
                        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdditional_info" />
                        <div class="table-responsive">
                            <asp:Panel ID="pnl_Details" runat="server" Visible="false">
                                <asp:Label ID="lblMsg_additional_info" runat="server" Text=""></asp:Label>
                                <table id="tbl_Details" class="table table-custom" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Accessibility</th>
                                            <th>Value</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblDet_id" runat="server" Text="Detail ID" Visible="false"></asp:Label>
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtDet_Id" runat="server" Enabled="false" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddlDet_name" Display="Dynamic" CssClass="form-control" Width="300px" ValidationGroup="vgAdditional_info">
                                                <asp:ListItem Value="-1">Select Villa accessibility</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDet_name" InitialValue="-1" Display="None" CssClass="text-danger" ErrorMessage="Please select a value for accessibility." ValidationGroup="vgAdditional_info" />
                                        </td>
                                        <td>
                                            <asp:TextBox ID="txtCount" TextMode="Number" runat="server" CssClass="form-control" />
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCount" Display="None" CssClass="text-danger" ErrorMessage="Please assign a value for chosen accessibility." ValidationGroup="vgAdditional_info" />
                                            <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgAdditional_info" ControlToValidate="txtCount" ErrorMessage="Value must be greater than zero" Display="None" Operator="GreaterThan" Type="Integer" ValueToCompare="0" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:GridView ID="gvs_2" OnSelectedIndexChanged="gvs_2_SelectedIndexChanged" DataKeyNames="Det_id" AutoGenerateColumns="false" Width="500" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_2_PageIndexChanging" runat="server" BorderWidth="1px">
                                <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="true" Height="30" />
                                <AlternatingRowStyle BackColor="#f9f9f9" />
                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Villa Details">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDet_name" Text='<%#Eval("Det_name")%>' runat="server" Font-Size="Medium" Font-Bold="True" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                    <asp:TemplateField HeaderText="Value">
                                        <ItemTemplate>
                                            <asp:Label ID="lblCount" Text='<%#Eval("Count")%>' runat="server" Font-Size="Medium" Font-Bold="True" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <!-- end of Villa Accessibility -->
            </div>

            <div class="col-lg-6">
                <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="text-primary">picture</h6>
                    </div>
                    <div class="card-body">
                        <div>
                            <asp:Image ID="img_main" runat="server" CssClass="main-picture" />
                        </div>
                    </div>
                </div>

                <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="text-primary"></h6>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lbl_slideshow" runat="server"></asp:Label>
                        <asp:DataList ID="dlstImages" RepeatColumns="3" runat="server">
                            <ItemTemplate>
                                <asp:Image ID="Image1" ImageUrl='<%# "~/Property/" + Eval("Villa_id") + "/" + Eval("Img_name", "{0}") %>' runat="server" Height="150" Width="180" />
                            </ItemTemplate>
                        </asp:DataList>
                    </div>
                </div>

                <!-- Villa Facilities -->
                <div class="card mb-4">
                    <div class="card-header">
                        <h6 class="text-primary">Villa Facilities</h6>
                    </div>
                    <div class="card-body">
                        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgFacility" />
                        <div class="table-responsive">
                            <asp:Panel ID="Pnl_facilities" runat="server" Visible="false">
                                <asp:Label ID="lblMsg_facilities" runat="server" Text=""></asp:Label>
                                <table id="tbl_facilities" class="table table-custom" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Facility</th>
                                        </tr>
                                    </thead>
                                    <tr>
                                        <td>
                                            <asp:Label ID="lblFacility_ID" runat="server" Text="Facility ID" Visible="false"></asp:Label>
                                            <asp:TextBox ID="txt_Facility_ID" runat="server" Enabled="false" Visible="false" />
                                        </td>
                                    </tr>
                                    <tr>
                                        <td>
                                            <asp:DropDownList runat="server" ID="ddl_Facility" Display="Dynamic" CssClass="form-control">
                                                <asp:ListItem Value="-1">Select facility</asp:ListItem>
                                            </asp:DropDownList>
                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddl_Facility" InitialValue="-1" Display="None" CssClass="text-danger" ErrorMessage="Please select a facility." ValidationGroup="vgFacility" />
                                        </td>
                                    </tr>
                                </table>
                            </asp:Panel>
                            <asp:GridView ID="gvs_3" OnSelectedIndexChanged="gvs_3_SelectedIndexChanged" DataKeyNames="Fac_id" AutoGenerateColumns="false" OnPreRender="gvs_3_PreRender" Width="500" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_3_PageIndexChanging" runat="server" BorderWidth="1px">
                                <HeaderStyle BackColor="#007bff" ForeColor="White" Font-Bold="true" Height="30" />
                                <AlternatingRowStyle BackColor="#f9f9f9" />
                                <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                                <Columns>
                                    <asp:TemplateField HeaderText="Villa's Facilities">
                                        <ItemTemplate>
                                            <asp:Label ID="lblDet_name" Text='<%#Eval("Fac_name")%>' runat="server" Font-Size="Medium" Font-Bold="True" />
                                        </ItemTemplate>
                                    </asp:TemplateField>
                                </Columns>
                            </asp:GridView>
                        </div>
                    </div>
                </div>
                <!-- Villa Facilities -->
            </div>
        </div>
    </div>

    <div class="mt-4">
        <table class="table table-borderless">
            <tr>
                <td class="text-center">
                    <asp:LinkButton ID="lnkapprove" CssClass="btn btn-custom" runat="server" OnClick="lnkapprove_Click" OnClientClick="return confirm('Are you sure you want to approve this villa?');" Text="Approves"></asp:LinkButton>
                </td>
                <td class="text-center">
                    <asp:LinkButton ID="lnkreject" CssClass="btn btn-danger-custom" runat="server" OnClick="LinkButton1_Click" OnClientClick="return confirm('Are you sure you want to reject this villa?');" Text="Decline"></asp:LinkButton>
                </td>
            </tr>
        </table>
        <panel id="pnlComment" runat="server">
            <asp:Label ID="lblComment" runat="server" CssClass="form-control" Text="Rejection Reason"></asp:Label>
            <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnReject" runat="server" CssClass="btn btn-custom mt-3" OnClick="btnReject_Click" Text="Confirm Reject" />
        </panel>
        <panel id="pnlUpdate" runat="server">
            <asp:Label ID="Label1" runat="server" CssClass="form-control" Text="Optional Comment"></asp:Label>
            <asp:TextBox ID="txt_optcomment" TextMode="MultiLine" runat="server" CssClass="form-control"></asp:TextBox>
            <asp:Button ID="btnRead" runat="server" CssClass="btn btn-custom mt-3" OnClick="btnRead_Click" Text="Mark as Seen" />
        </panel>
        <center>
            <asp:LinkButton ID="Back" runat="server" Text="Back" PostBackUrl="~/AdminS/New_villa.aspx" CssClass="btn btn-custom mt-4" Font-Bold="True" CausesValidation="false"></asp:LinkButton>
        </center>
    </div>
</asp:Content>
