<%@ Page Title="" Language="C#" MasterPageFile="~/Owner2Master.Master" AutoEventWireup="true" CodeBehind="CreateVilla.aspx.cs" Inherits="DealProjectTamam.CreateVilla" %>
<%@ MasterType VirtualPath="~/Owner.Master" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link rel="stylesheet" href="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/css/bootstrap.min.css" />
    <script src="https://code.jquery.com/jquery-3.2.1.slim.min.js"></script>
    <script src="https://cdnjs.cloudflare.com/ajax/libs/popper.js/1.11.0/umd/popper.min.js"></script>
    <script src="https://maxcdn.bootstrapcdn.com/bootstrap/4.0.0/js/bootstrap.min.js"></script>
    <style>
        body {
            background-image: url('background-image-url.jpg');
            background-size: cover;
        }
        .form-container {
            background: rgba(255, 255, 255, 0.8);
            padding: 20px;
            border-radius: 10px;
            margin-top: 30px;
        }
        .btn-custom {
            background-color: #ff69b4;
            border: none;
            color: white;
            padding: 10px 20px;
            border-radius: 5px;
        }
    </style>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="container form-container">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <nav class="nav nav-tabs">
            <a class="nav-item nav-link active" href="#villa_details" data-toggle="tab">Villa Details</a>
            <a class="nav-item nav-link" href="#villa_description" data-toggle="tab">Description</a>
            <a class="nav-item nav-link" href="#villa_pictures" data-toggle="tab">Upload Pictures</a>
            <a class="nav-item nav-link" href="#villa_accessibilities" data-toggle="tab">Villa details</a>
            <a class="nav-item nav-link" href="#villa_facilities" data-toggle="tab">Villa Facilities</a>
            <a class="nav-item nav-link" href="#request_approval" data-toggle="tab">Request Approval</a>
        </nav>
        <div class="tab-content">
            <!--villa details-->
            <div id="villa_details" class="tab-pane fade show active">
                <h2>Villa Details</h2>
                <fieldset>
                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="VgVillaDetails" />
                    <div class="form-group">
                        <asp:Label runat="server" ID="lblResult1" Display="Dynamic" CssClass="col-form-label text-danger font-weight-bold"></asp:Label>
                        <asp:Label runat="server" ID="lblResult2" Display="Dynamic" CssClass="col-form-label text-danger font-weight-bold"></asp:Label>
                        <asp:Label runat="server" ID="lblResult3" Display="Dynamic" CssClass="col-form-label text-danger font-weight-bold"></asp:Label>
                        <asp:Label runat="server" ID="lblResult4" Display="Dynamic" CssClass="col-form-label text-danger font-weight-bold"></asp:Label>
                        <asp:Label runat="server" ID="lblResult5" Display="Dynamic" CssClass="col-form-label text-danger font-weight-bold"></asp:Label>
                        <asp:Label runat="server" ID="lblResult6" Display="Dynamic" CssClass="col-form-label text-danger font-weight-bold"></asp:Label>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtOwn_fname" Display="None" ForeColor="Black">Owner First Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtOwn_fname" class="form-control" ForeColor="Black" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtOwn_lname" Display="None" ForeColor="Black">Owner Last Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtOwn_lname" class="form-control" ForeColor="Black" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_name" Display="Dynamic" ForeColor="Black">Villa Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_name" class="form-control" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_name" Display="Dynamic" CssClass="text-danger" ErrorMessage="Villa Name is required." ValidationGroup="VgVillaDetails" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_phone" Display="Dynamic" ForeColor="Black">Villa Phone Number (fixed)</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_phone" TextMode="Number" MaxLength="7" class="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_phone" Display="Dynamic" CssClass="text-danger" ErrorMessage="Villa Phone Number is required." ValidationGroup="VgVillaDetails" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_email" Display="Dynamic" ForeColor="Black">Villa Email</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_email" class="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RegularExpressionValidator ID="RegularExpressionValidator1" ControlToValidate="txtProp_email" Text="(Invalid email)" ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*" runat="server" CssClass="text-danger" Display="Dynamic" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_street" Display="Dynamic" ForeColor="Black">Street</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_street" class="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_street" Display="Dynamic" CssClass="text-danger" ErrorMessage="Street is required." ValidationGroup="VgVillaDetails" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_town" Display="Dynamic" ForeColor="Black">Town</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_town" class="form-control" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_town" Display="Dynamic" CssClass="text-danger" ErrorMessage="Town is required." ValidationGroup="VgVillaDetails" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="ddlDistrict" Display="Dynamic" ForeColor="Black">District</asp:Label>
                            <asp:DropDownList runat="server" ID="ddlDistrict" Display="Dynamic" ForeColor="Black" class="form-control">
                                <asp:ListItem Value="-1">Select District</asp:ListItem>
                            </asp:DropDownList>
                            <asp:RequiredFieldValidator ControlToValidate="ddlDistrict" InitialValue="-1" runat="server" CssClass="text-danger" ErrorMessage="District required." Display="Dynamic" ValidationGroup="VgVillaDetails" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_priceday" Display="Dynamic" ForeColor="Black">Price per day</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_priceday" TextMode="Number" MaxLength="4" class="form-control" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceday" Display="Dynamic" CssClass="text-danger" ErrorMessage="Price per day is required." ValidationGroup="VgVillaDetails" />
                            <asp:CompareValidator ID="CV_priceday" runat="server" ValidationGroup="VgVillaDetails" ControlToValidate="txtProp_priceday" CssClass="text-danger" ErrorMessage="Price per day must be greater than zero" Display="Dynamic" Operator="GreaterThan" Type="Integer" ValueToCompare="0" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_priceweek" Display="Dynamic" ForeColor="Black">Price per week</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_priceweek" TextMode="Number" MaxLength="5" class="form-control" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceweek" Display="Dynamic" CssClass="text-danger" ErrorMessage="Price per week is required." ValidationGroup="VgVillaDetails" />
                            <asp:CompareValidator ID="CV_priceweek" runat="server" ValidationGroup="VgVillaDetails" ControlToValidate="txtProp_priceweek" CssClass="text-danger" ErrorMessage="Price per week must be greater than zero" Display="Dynamic" Operator="GreaterThan" Type="Integer" ValueToCompare="0" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_pricemonth" Display="Dynamic" ForeColor="Black">Price per month</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_pricemonth" TextMode="Number" MaxLength="6" class="form-control" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_pricemonth" Display="Dynamic" CssClass="text-danger" ErrorMessage="Price per month is required." ValidationGroup="VgVillaDetails" />
                            <asp:CompareValidator ID="CV_pricemonth" runat="server" ValidationGroup="VgVillaDetails" ControlToValidate="txtProp_pricemonth" CssClass="text-danger" ErrorMessage="Price per month must be greater than zero" Display="Dynamic" Operator="GreaterThan" Type="Integer" ValueToCompare="0" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_postcode" Display="Dynamic" ForeColor="Black">Postal Code</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_postcode" TextMode="Number" MaxLength="5" class="form-control" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-12">
                            <asp:Button ID="btnSave" runat="server" ValidateRequestMode="Enabled" OnClick="btnSave_Click" Text="Save" ValidationGroup="VgVillaDetails" class="btn btn-custom" CausesValidation="true" />
                            <asp:Button ID="btnCancel" runat="server" Text="Cancel" CausesValidation="false" ValidationGroup="VgVillaDetails" OnClick="btnCancel_Click" class="btn btn-secondary" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <!--villa description-->
            <div id="villa_description" class="tab-pane fade">
                <h2>Villa Description</h2>
                <fieldset>
                    <div class="form-group">
                        <asp:Label ID="lbl_Prop_Desc" runat="server" Text="" Font-Bold="True"></asp:Label>
                        <asp:TextBox runat="server" ID="txt_Description" TextMode="MultiLine" Columns="54" Rows="25" ValidationGroup="vgDescription" ForeColor="#003366" BorderColor="#336699" BackColor="White" class="form-control" />
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Button ID="Btn_Save_description" runat="server" ValidationGroup="vgDescription" OnClick="Btn_Save_description_Click" Text="Save" class="btn btn-custom" Visible="false" />
                            <asp:Button ID="Btn_Cancel_description" runat="server" OnClick="Btn_Cancel_description_Click" Text="Cancel" CausesValidation="false" ValidationGroup="vgDescription" class="btn btn-secondary" Visible="false" />
                        </div>
                    </div>
                </fieldset>
            </div>
            <!--pictures-->
            <div id="villa_pictures" class="tab-pane fade">
                <h2>Villa Pictures</h2>
                <fieldset>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_id" Display="Dynamic" Visible="false">Villa ID</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_id" class="form-control" Visible="false" ForeColor="Black" />
                        </div>
                    </div>
                    <div class="form-row">
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" Display="Dynamic" ForeColor="Black" Font-Bold="True">Main Picture</asp:Label>
                            <asp:Label ID="lbl_main_pic" runat="server" Font-Bold="True"></asp:Label>
                            <asp:FileUpload ID="btnupload_main_pic" CssClass="btn btn-outline-primary" runat="server" />
                            <asp:Button ID="btnSave_main_pic" runat="server" OnClick="btnSave_main_pic_Click" Text="Save" ValidationGroup="VgMainpic" Visible="false" class="btn btn-custom" />
                            <asp:Button ID="btnCancel_main_pic" runat="server" OnClick="btnCancel_main_pic_Click" Text="Cancel" CausesValidation="false" ValidationGroup="VgMainpic" Visible="false" class="btn btn-secondary" />
                        </div>
                        <div class="form-group col-md-6">
                            <asp:Label runat="server" Display="Dynamic" ForeColor="Black" Font-Bold="True">Pictures for slideshow</asp:Label>
                            <asp:Label ID="lbl_slideshow" runat="server" Font-Bold="True"></asp:Label>
                            <asp:FileUpload ID="btnupload_slideshow" CssClass="btn btn-outline-primary" runat="server" />
                            <asp:Button ID="btnSave_slideshow" runat="server" Text="Save" class="btn btn-custom" ValidationGroup="VgSlideshow" OnClick="btnSave_slideshow_Click" Visible="false" />
                            <asp:Button ID="btnCancel_slideshow" runat="server" Visible="false" Text="Cancel" CausesValidation="false" ValidationGroup="VgSlideshow" OnClick="btnCancel_slideshow_Click" class="btn btn-secondary" />
                        </div>
                    </div>
                </fieldset>
            </div>
            
            <div id="villa_accessibilities" class="tab-pane fade">
                <h2>Villa Details</h2>
                <fieldset>
                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdditional_info" />
                    <div>
                        <asp:Label ID="lblMsg_additional_info" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Details</th>
                                <th>Value</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblDet_id" runat="server" Text="Detail ID" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txtDet_Id" runat="server" Enabled="false" Visible="false" />
                                </td>
                                <td>
                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlDet_name" Display="Dynamic">
                                        <asp:ListItem Value="-1">Select details</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDet_name" InitialValue="-1" Display="None" CssClass="text-danger" ErrorMessage="Please select an accessibility." ValidationGroup="vgAdditional_info" />
                                    <asp:TextBox ID="txtCount" TextMode="Number" runat="server" class="form-control" />
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCount" Display="None" CssClass="text-danger" ErrorMessage="Please assign a value." ValidationGroup="vgAdditional_info" />
                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgAdditional_info" ControlToValidate="txtCount" ErrorMessage="Value must be greater than zero" Display="None" Operator="GreaterThan" Type="Integer" ValueToCompare="0" />
                                </td>
                            </tr>
                            <tr>
                                <td colspan="2">
                                    <asp:Button ID="btnInsert_Add_info" runat="server" Text="Insert" ValidationGroup="vgAdditional_info" CssClass="btn btn-outline-primary" Visible="false" OnClick="btnInsert_Add_info_Click" />
                                    <asp:Button ID="btnUpdate_Add_info" runat="server" Text="Update" ValidationGroup="vgAdditional_info" CssClass="btn btn-outline-warning" Visible="false" OnClick="btnUpdate_Add_info_Click" />
                                    <asp:Button ID="btnDelete_Add_info" runat="server" CssClass="btn btn-outline-danger" OnClick="btnDelete_Add_info_Click" OnClientClick="return confirm('Are you sure you want to delete this record?')" Text="Delete" ValidationGroup="vgAdditional_info" CausesValidation="false" Visible="false" />
                                    <asp:Button ID="btnCancel_Add_info" runat="server" Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-success" Visible="false" OnClick="btnCancel_Add_info_Click" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="gvs_2" OnSelectedIndexChanged="gvs_2_SelectedIndexChanged" DataKeyNames="Det_id" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_2_PageIndexChanging" runat="server" BorderWidth="1px">
                        <HeaderStyle BackColor="#9a9a9a" ForeColor="White" Font-Bold="true" Height="30" />
                        <AlternatingRowStyle BackColor="#f5f5f5" />
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                        <Columns>
                            <asp:TemplateField HeaderText="Action" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect_Detail" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Villa details" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblDet_name" Text='<%#Eval("Det_name")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Value" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblCount" Text='<%#Eval("Count")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </div>
            <!--villa facilities-->
            <div id="villa_facilities" class="tab-pane fade">
                <h2>Villa Facilities</h2>
                <fieldset>
                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgFacility" />
                    <div>
                        <asp:Label ID="lblMsg_facilities" runat="server" Text="" Font-Bold="True"></asp:Label>
                    </div>
                    <table class="table table-bordered">
                        <thead>
                            <tr>
                                <th>Facility</th>
                            </tr>
                        </thead>
                        <tbody>
                            <tr>
                                <td>
                                    <asp:Label ID="lblFacility_ID" runat="server" Text="Facility ID" Visible="false"></asp:Label>
                                    <asp:TextBox ID="txt_Facility_ID" runat="server" Enabled="false" Visible="false" />
                                    <asp:DropDownList runat="server" ID="ddl_Facility" Display="Dynamic" class="form-control">
                                        <asp:ListItem Value="-1">Select facility</asp:ListItem>
                                    </asp:DropDownList>
                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddl_Facility" InitialValue="-1" Display="None" CssClass="text-danger" ErrorMessage="Please select a facility." ValidationGroup="vgFacility" />
                                </td>
                            </tr>
                            <tr>
                                <td>
                                    <asp:Button ID="BtnInsert_Facility" runat="server" OnClick="BtnInsert_Facility_Click" Text="Insert" ValidationGroup="vgFacility" CssClass="btn btn-outline-primary" Visible="false" />
                                    <asp:Button ID="BtnDelete_Facility" runat="server" CssClass="btn btn-outline-danger" OnClick="BtnDelete_Facility_Click" OnClientClick="return confirm('Are you sure you want to delete this record?')" Text="Delete" ValidationGroup="vgFacility" CausesValidation="false" Visible="false" />
                                    <asp:Button ID="BtnCancel_Facility" runat="server" OnClick="BtnCancel_Facility_Click" Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-success" Visible="false" />
                                </td>
                            </tr>
                        </tbody>
                    </table>
                    <asp:GridView ID="gvs_3" OnSelectedIndexChanged="gvs_3_SelectedIndexChanged" DataKeyNames="Fac_id" AutoGenerateColumns="false" Width="100%" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_3_PageIndexChanging" runat="server" BorderWidth="1px">
                        <HeaderStyle BackColor="#9a9a9a" ForeColor="White" Font-Bold="true" Height="30" />
                        <AlternatingRowStyle BackColor="#f5f5f5" />
                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First" NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                        <Columns>
                            <asp:TemplateField HeaderText="Action" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:LinkButton ID="lbtnSelect_Facility" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" />
                                </ItemTemplate>
                            </asp:TemplateField>
                            <asp:TemplateField HeaderText="Villa Facilities" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblDet_name" Text='<%#Eval("Fac_name")%>' runat="server" />
                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </fieldset>
            </div>
            <!--Request Approval-->
            <div id="request_approval" class="tab-pane fade">
                <h2>Request Approval</h2>
                <div class="form-group">
                    <asp:Label runat="server" AssociatedControlID="txtemail" Display="Dynamic">Email Content</asp:Label>
                    <asp:TextBox runat="server" Rows="10" ID="txtemail" TextMode="MultiLine" CssClass="form-control" ForeColor="Black" />
                </div>
                <div class="form-group">
                    <asp:Button ID="BtnSendMail" runat="server" Text="Request Approval" Visible="false" class="btn btn-custom" CausesValidation="false" OnClick="BtnSendMail_Click" />
                </div>
            </div>
        </div>
    </main>
</asp:Content>
