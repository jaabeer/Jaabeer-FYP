<%@ Page Title="" Language="C#" MasterPageFile="~/Owner.Master" AutoEventWireup="true" CodeBehind="Modify_Hotel.aspx.cs" Inherits="DealProjectTamam.Modify_Hotel" %>
<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <link href="https://stackpath.bootstrapcdn.com/bootstrap/4.5.2/css/bootstrap.min.css" rel="stylesheet" />
    <style>
        .gradient-button {
            background: linear-gradient(to right, #1e5799 0%, #2989d8 50%, #207cca 100%);
            color: white;
            border: none;
        }
        .text-danger {
            color: #e74c3c !important;
        }
        .form-control-user {
            border: 1px solid #ddd;
            padding: 10px;
        }
    </style>
</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <main class="main">
        <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
        <div class="container">
            <div class="row">
                <section class="col-lg-9">
                    <nav class="inner-nav">
                        <ul class="nav nav-tabs">
                            <li class="nav-item">
                                <a class="nav-link" href="#hotel_details" title="Hotel Details">Hotel Details</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#hotel_description" title="Description">Hotel Description</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#hotel_pictures" title="Pictures">Upload Pictures</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#hotel_accessibilities" title="Accessibilities">Hotel Accessibilities</a>
                            </li>
                            <li class="nav-item">
                                <a class="nav-link" href="#hotel_facilities" title="Facilities">Hotel Facilities</a>
                            </li>
                        </ul>
                    </nav>
                    <!--hotel details-->
                    <section id="hotel_details" class="tab-content mt-3">
                        <article>
                            <h2>Hotel Details</h2>
                            <fieldset class="border p-4">
                                <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="VgHotelDetails" />
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblResult1" Display="Dynamic" CssClass="form-label" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblResult2" Display="Dynamic" CssClass="form-label" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblResult3" Display="Dynamic" CssClass="form-label" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblResult4" Display="Dynamic" CssClass="form-label" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblResult5" Display="Dynamic" CssClass="form-label" ForeColor="#CC3300" Font-Bold="True"></asp:Label>
                                </div>
                                <div class="form-group">
                                    <asp:Label runat="server" ID="lblResult6" Display="Dynamic" ForeColor="#993300" Font-Bold="True"></asp:Label>
                                </div>

                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtOwn_fname" Display="None" ForeColor="Black">Owner First Name</asp:Label>
                                        <asp:TextBox runat="server" ID="txtOwn_fname" class="form-control form-control-user" ForeColor="Black" />
                                    </div>

                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtOwn_lname" Display="None" ForeColor="Black">Owner Last Name</asp:Label>
                                        <asp:TextBox runat="server" ID="txtOwn_lname" class="form-control form-control-user" ForeColor="Black" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_name" Display="Dynamic" ForeColor="Black">Hotel Name</asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_name" class="form-control form-control-user" ForeColor="Black" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_name" Display="Dynamic"
                                            CssClass="text-danger" ErrorMessage="Hotel Name is required." ValidationGroup="VgHotelDetails" />
                                    </div>
                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_phone" Display="Dynamic" ForeColor="Black">Hotel Phone Number </asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_phone" TextMode="Number" MaxLength="7" class="form-control form-control-user" ValidationGroup="VgHotelDetails" ForeColor="Black" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_phone" Display="Dynamic" ForeColor="Black"
                                            CssClass="text-danger" ErrorMessage="Hotel Phone Number is required." ValidationGroup="VgHotelDetails" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_email" Display="Dynamic" ForeColor="Black">Hotel Email</asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_email" class="form-control form-control-user" ValidationGroup="VgHotelDetails" ForeColor="Black" />
                                        <asp:RegularExpressionValidator
                                            ID="RegularExpressionValidator1"
                                            ControlToValidate="txtProp_email"
                                            Text="(Invalid email)"
                                            ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                            runat="server" CssClass="text-danger" Display="Dynamic" />
                                    </div>

                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_street" Display="Dynamic" ForeColor="Black">Street</asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_street" class="form-control form-control-user" ValidationGroup="VgHotelDetails" ForeColor="Black" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_street" Display="Dynamic" ForeColor="Black"
                                            CssClass="text-danger" ErrorMessage="Street is required." ValidationGroup="VgHotelDetails" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_town" Display="Dynamic" ForeColor="Black">Town</asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_town" class="form-control form-control-user" ForeColor="Black" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_town" Display="Dynamic" ForeColor="Black"
                                            CssClass="text-danger" ErrorMessage="Town is required." ValidationGroup="VgHotelDetails" />
                                    </div>

                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="ddlDistrict" Display="Dynamic" ForeColor="Black">District</asp:Label><br />
                                        <asp:DropDownList runat="server" ID="ddlDistrict" Display="Dynamic" ForeColor="Black" class="form-control form-control-user">
                                            <asp:ListItem Value="-1">Select District</asp:ListItem>
                                        </asp:DropDownList>
                                        <asp:RequiredFieldValidator ControlToValidate="ddlDistrict" InitialValue="-1" runat="server"
                                            CssClass="text-danger" ErrorMessage="District required." Display="Dynamic" ValidationGroup="VgHotelDetails" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_priceday" Display="Dynamic" ForeColor="Black">Price per day</asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_priceday" TextMode="Number" MaxLength="4" class="form-control form-control-user" ForeColor="Black" ValidationGroup="VgHotelDetails" />
                                        <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceday" Display="Dynamic"
                                            CssClass="text-danger" ErrorMessage="Price per day is required." ValidationGroup="VgHotelDetails" />
                                        <asp:CompareValidator ID="CV_priceday" runat="server" ValidationGroup="VgHotelDetails"
                                            ControlToValidate="txtProp_priceday" CssClass="text-danger" ErrorMessage="Price per day must be greater than zero" Display="Dynamic"
                                            Operator="GreaterThan" Type="Integer"
                                            ValueToCompare="0" />
                                    </div>

                                    <div class="form-group col-md-6">
                                        <asp:Label runat="server" AssociatedControlID="txtProp_postcode" Display="Dynamic" ForeColor="Black">Postal Code</asp:Label>
                                        <asp:TextBox runat="server" ID="txtProp_postcode" TextMode="Number" MaxLength="5" class="form-control form-control-user" ValidationGroup="VgHotelDetails" ForeColor="Black" />
                                    </div>
                                </div>

                                <div class="row">
                                    <div class="form-group col-12 text-center">
                                        <asp:Button ID="btnSave_own_loc_price" runat="server" ValidateRequestMode="Enabled"
                                            Text="Save" ValidationGroup="VgHotelDetails" 
                                            OnClick="btnSave_own_loc_price_Click"
                                            class="gradient-button" />

                                        <asp:Button ID="btnCancel_own_loc_price" runat="server" 
                                            Text="Cancel" CausesValidation="false" ValidationGroup="VgHotelDetails"
                                            OnClick="btnCancel_own_loc_price_Click"
                                            class="gradient-button" />

                                        <asp:Button ID="btnEdit_own_loc_price" runat="server" 
                                            Text="Edit" CausesValidation="false" ValidationGroup="VgHotelDetails"
                                            OnClick="btnEdit_own_loc_price_Click"
                                            class="gradient-button" />
                                    </div>
                                </div>
                            </fieldset>
                        </article>
                    </section>
                    <!--hotel details-->

                    <!--Hotel Description-->
                    <section id="hotel_description" class="tab-content mt-3">
                        <article>
                            <h2>Hotel Description</h2>
                            <contenttemplate>
                                <fieldset class="border p-4">
                                    <div class="row">
                                        <div class="form-group col-12">
                                            <asp:Label ID="lbl_Prop_Desc" runat="server" Text="" Font-Bold="True"></asp:Label>
                                            <asp:TextBox runat="server" ID="txt_Description" TextMode="MultiLine" Columns="54" Rows="25" ValidationGroup="vgDescription" ForeColor="#003366" BorderColor="#336699" BackColor="White" class="form-control" /><br />
                                        </div>
                                    </div>
                                    <br />
                                    <div class="form-group row text-center">
                                        <div class="col-12">
                                            <asp:Button ID="Btn_Edit_description" runat="server" ValidationGroup="vgDescription"
                                                Text="Edit" OnClick="Btn_Edit_description_Click"
                                                CausesValidation="false" class="gradient-button" />&nbsp;

                                            <asp:Button ID="Btn_Save_description" runat="server" ValidationGroup="vgDescription"
                                                Text="Save" OnClick="Btn_Save_description_Click" class="gradient-button" />&nbsp;

                                            <asp:Button ID="Btn_Cancel_description" runat="server" OnClick="Btn_Cancel_description_Click"
                                                Text="Cancel" CausesValidation="false" ValidationGroup="vgDescription" class="gradient-button" />
                                        </div>
                                    </div>
                                </fieldset>
                            </contenttemplate>
                        </article>
                    </section>
                    <!--Hotel Description-->

                    <!--Hotel Pictures-->
                    <section id="hotel_pictures" class="tab-content mt-3">
                        <article class="col-12">
                            <h2>Hotel Main Picture</h2>
                            <asp:Label ID="lbl_main_pic" runat="server" Font-Bold="True"></asp:Label>
                            <br /><br />
                            <asp:Image ID="img_main" runat="server" Height="300px" Width="400px" BorderStyle="Inset" BorderColor="#666666" BorderWidth="5px" class="img-thumbnail" />
                            <asp:FileUpload ID="btnupload_main_pic" CssClass="btn btn-outline-primary" Font-Size="Smaller" runat="server" BackColor="#6666FF" ForeColor="White" ValidationGroup="VgMainpic" class="form-control form-control-user" />
                            <br /><br />
                            <asp:Button ID="btnEdit_main_pic" runat="server" OnClick="btnEdit_main_pic_Click"
                                Text="Edit" CausesValidation="false" ValidationGroup="VgMainpic"
                                class="gradient-button" />
                            <asp:Button ID="btnSave_main_pic" runat="server" OnClick="btnSave_main_pic_Click"
                                Text="Save" ValidationGroup="VgMainpic"
                                class="gradient-button" />
                            <asp:Button ID="btnCancel_main_pic" runat="server" OnClick="btnCancel_main_pic_Click"
                                Text="Cancel" CausesValidation="false" ValidationGroup="VgMainpic"
                                class="gradient-button" />
                        </article>

                        <br /><br />

                        <article class="col-12">
                            <h2>Hotel Slideshow Pictures</h2>
                            <asp:Label ID="lbl_slideshow" runat="server" Font-Bold="True"></asp:Label>
                            <br />
                            <!-- Slideshow pictures -->
                            <asp:DataList
                                ID="dlstImages"
                                RepeatColumns="3"
                                runat="server" GridLines="None" ItemStyle-BorderStyle="None" class="row">
                                <ItemTemplate>
                                    <div class="col-4 mb-4">
                                        <div class="details">
                                            <asp:Image ID="Image1" ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/" + Eval("Img_name", "{0}") %>' runat="server" Height="150" Width="180" class="img-thumbnail" />
                                            <br>
                                            <asp:Panel ID="Panel1" runat="server">
                                                <asp:LinkButton ID="lbtndelete_slideshow" runat="server"
                                                    class="btn btn-danger btn-circle btn-sm"
                                                    Visible="false"
                                                    OnClientClick="return confirm('Are you sure you want delete')"
                                                    OnClick="lbtndelete_slideshow_Click"
                                                    ValidationGroup="VgSlideshow"
                                                    CommandArgument='<%# Eval("Img_name") %>'>
                                                    <i class="fas fa-trash"></i>
                                                </asp:LinkButton>
                                            </asp:Panel>
                                            <asp:Label  ID ="lbl_slideshow_name" runat="server" Text='<%# Eval("Img_name") %>'></asp:Label>
                                        </div>
                                    </div>
                                </ItemTemplate>
                            </asp:DataList>

                            <div>
                                <asp:FileUpload ID="btnupload_slideshow" CssClass="btn btn-outline-primary" Font-Size="Smaller" runat="server" BackColor="#6666FF" ForeColor="White" ValidationGroup="VgSlideshow" class="form-control form-control-user" />
                                <br /><br />
                                <asp:Button ID="btnEdit_slideshow" runat="server"
                                    Text="Edit" CausesValidation="false" ValidationGroup="VgSlideshow" OnClick="btnEdit_slideshow_Click" class="gradient-button" />
                                <asp:Button ID="btnSave_slideshow" runat="server" Text="Save" class="gradient-button"
                                    ValidationGroup="VgSlideshow" OnClick="btnSave_slideshow_Click" />
                                <asp:Button ID="btnCancel_slideshow" runat="server"
                                    Text="Cancel" CausesValidation="false"
                                    ValidationGroup="VgSlideshow" OnClick="btnCancel_slideshow_Click" class="gradient-button" />
                            </div>
                        </article>
                    </section>
                    <!--Hotel Pictures-->

                    <!--hotel accessibilities-->
                    <section id="hotel_accessibilities" class="tab-content mt-3">
                        <article>
                            <h2>Hotel Accessibilities</h2>
                            <contenttemplate>
                                <fieldset class="border p-4">
                                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdditional_info" />
                                    <div class="form-group">
                                        <asp:Label ID="lblMsg_additional_info" runat="server" Text="" Font-Bold="True"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:Panel ID="pnl_Details" runat="server" Visible="false">
                                        <table class="table table-bordered" width="100%" cellspacing="0">
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
                                                    <asp:DropDownList runat="server" CssClass="form-control" ID="ddlDet_name" Display="Dynamic">
                                                        <asp:ListItem Value="-1">Select accessibility</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDet_name" InitialValue="-1" Display="None"
                                                        CssClass="text-danger" ErrorMessage="Please select an accessibility." ValidationGroup="vgAdditional_info" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCount" TextMode="Number" runat="server" class="form-control" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCount" Display="None"
                                                        CssClass="text-danger" ErrorMessage="Please assign a value." ValidationGroup="vgAdditional_info" />
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgAdditional_info"
                                                        ControlToValidate="txtCount" ErrorMessage="Value must be greater than zero" Display="None"
                                                        Operator="GreaterThan" Type="Integer"
                                                        ValueToCompare="0" />
                                                </td>
                                            </tr>
                                            <tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnInsert_Add_info" runat="server"
                                                        Text="Insert" ValidationGroup="vgAdditional_info" CssClass="btn btn-outline-primary" Visible="false" OnClick="btnInsert_Add_info_Click" />
                                                    <asp:Button ID="btnUpdate_Add_info" runat="server"
                                                        Text="Update" ValidationGroup="vgAdditional_info" CssClass="btn btn-outline-warning" Visible="false" OnClick="btnUpdate_Add_info_Click" />
                                                    <asp:Button ID="btnDelete_Add_info" runat="server" CssClass="btn btn-outline-danger" OnClick="btnDelete_Add_info_Click"
                                                        OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                                        Text="Delete" ValidationGroup="vgAdditional_info" CausesValidation="false" Visible="false" />
                                                    <asp:Button ID="btnCancel_Add_info" runat="server"
                                                        Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-success" Visible="false" OnClick="btnCancel_Add_info_Click" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <br />
                                    <!-- set the primary for the category table as the DataKeynames-->
                                    <asp:GridView ID="gvs_2" OnSelectedIndexChanged="gvs_2_SelectedIndexChanged" DataKeyNames="Det_id" AutoGenerateColumns="false"
                                        Width="500" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_2_PageIndexChanging" runat="server" BorderWidth="1px">
                                        <HeaderStyle BackColor="#9a9a9a" ForeColor="White" Font-Bold="true" Height="30" />
                                        <AlternatingRowStyle BackColor="#f5f5f5" />
                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                            NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect_Detail" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hotel Accessibilities" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                                <ItemTemplate>
                                                    <!-- display the category name -->
                                                    <asp:Label ID="lblDet_name" Text='<%#Eval("Det_name")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                                <ItemTemplate>
                                                    <!-- display the category description -->
                                                    <asp:Label ID="lblCount" Text='<%#Eval("Count")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>

                                    <div class="form-group row text-center">
                                        <div class="col-12">
                                            <asp:Button ID="Btn_Edit_additional_info" runat="server" ValidationGroup="vgAdditional_info"
                                                Text="Edit" class="gradient-button" CausesValidation="false" OnClick="Btn_Edit_additional_info_Click" />

                                            <asp:Button ID="Btn_Cancel_additional_info" runat="server" OnClick="Btn_Cancel_additional_info_Click"
                                                Text="Cancel" CausesValidation="false" ValidationGroup="vgAdditional_info" class="gradient-button" />
                                        </div>
                                    </div>
                                </fieldset>
                            </contenttemplate>
                        </article>
                    </section>
                    <!--hotel accessibilities-->

                    <!--hotel facilities-->
                    <section id="hotel_facilities" class="tab-content mt-3">
                        <article>
                            <h2>Hotel Facilities</h2>
                            <contenttemplate>
                                <fieldset class="border p-4">
                                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgFacility" />
                                    <div class="form-group">
                                        <asp:Label ID="lblMsg_facilities" runat="server" Text="" Font-Bold="True"></asp:Label>
                                    </div>
                                    <br />
                                    <asp:Panel ID="Pnl_facilities" runat="server" Visible="false">
                                        <table class="table table-bordered" width="100%" cellspacing="0">
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
                                                    <asp:DropDownList runat="server" ID="ddl_Facility" Display="Dynamic" class="form-control">
                                                        <asp:ListItem Value="-1">Select facility</asp:ListItem>
                                                    </asp:DropDownList>
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddl_Facility" InitialValue="-1" Display="None"
                                                        CssClass="text-danger" ErrorMessage="Please select a facility." ValidationGroup="vgFacility" />
                                                </td>
                                            </tr>
                                            <tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="BtnInsert_Facility" runat="server" OnClick="BtnInsert_Facility_Click"
                                                        Text="Insert" ValidationGroup="vgFacility" CssClass="btn btn-outline-primary" Visible="false" />
                                                    <asp:Button ID="BtnDelete_Facility" runat="server" CssClass="btn btn-outline-danger" OnClick="BtnDelete_Facility_Click"
                                                        OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                                        Text="Delete" ValidationGroup="vgFacility" CausesValidation="false" Visible="false" />
                                                    <asp:Button ID="BtnCancel_Facility" runat="server" OnClick="BtnCancel_Facility_Click"
                                                        Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-success" Visible="false" />
                                                </td>
                                            </tr>
                                        </table>
                                    </asp:Panel>
                                    <br />
                                    <!-- set the primary for the category table as the DataKeynames-->
                                    <asp:GridView ID="gvs_3" OnSelectedIndexChanged="gvs_3_SelectedIndexChanged" DataKeyNames="Fac_id" AutoGenerateColumns="false"
                                        Width="500" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_3_PageIndexChanging" runat="server" BorderWidth="1px">
                                        <HeaderStyle BackColor="#9a9a9a" ForeColor="White" Font-Bold="true" Height="30" />
                                        <AlternatingRowStyle BackColor="#f5f5f5" />
                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                            NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect_Facility" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Hotel Facilities" ControlStyle-ForeColor="Black" HeaderStyle-ForeColor="Black">
                                                <ItemTemplate>
                                                    <!-- display the category name -->
                                                    <asp:Label ID="lblDet_name" Text='<%#Eval("Fac_name")%>'
                                                        runat="server" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                    <div class="form-group row text-center">
                                        <div class="col-12">
                                            <asp:Button ID="Btn_Edit_facilities" runat="server"
                                                Text="Edit" CausesValidation="false" ValidationGroup="vgFacility"
                                                class="gradient-button" OnClick="Btn_Edit_facilities_Click" />

                                            <asp:Button ID="Btn_Cancel_facilities" runat="server"
                                                Text="Cancel" CausesValidation="false" ValidationGroup="vgFacility"
                                                class="gradient-button" OnClick="Btn_Cancel_facilities_Click" />
                                        </div>
                                    </div>
                                </fieldset>
                            </contenttemplate>
                        </article>
                    </section>
                    <!--hotel facilities-->
                </section>

                <!--Request Approval-->
                <aside class="col-lg-3">
                    <asp:UpdatePanel ID="UpdatePanel1" runat="server">
                        <ContentTemplate>
                            <!--Need Help Booking?-->
                            <article class="widget">
                                <h4>Issue with Modification</h4>
                                <p>Call our customer support team on the number below to speak to one of our advisors who will help you with creation issues.</p>
                                <p class="number">(+230) 216-21-21</p>
                                <asp:Button ID="Button3" runat="server" Text="Contact Support" ValidationGroup="vg_support" />
                            </article>
                        </ContentTemplate>
                    </asp:UpdatePanel>
                </aside>
            </div>
        </div>
    </main>
</asp:Content>
