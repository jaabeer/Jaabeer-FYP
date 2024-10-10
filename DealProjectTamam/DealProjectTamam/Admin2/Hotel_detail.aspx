<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="Hotel_detail.aspx.cs" Inherits="DealProjectTamam.Admin2.Hotel_detail" %>
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


    <div class="container-fluid">
        <!-- Page Heading -->
        <h1 class="h3 mb-4 text-gray-800">Villa Details</h1>

        <div class="row">

            <div class="col-lg-6">

                <!-- Owner Location and Price -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Owner, Location and Price</h6>
                    </div>

                    <div class="card-body">

                        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="VgVillaDetails" />
                        <div>
                            <div>
                                <asp:Label runat="server" ID="lblResult1" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblResult2" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblResult3" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblResult4" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblResult5" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                            <div>
                                <asp:Label runat="server" ID="lblResult6" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>

                        </div>


                     
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtProp_id" Display="Dynamic" Visible="false">Property ID</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_id" class="form-control form-control-user" Visible="false" ForeColor="Black" />
                            </div>
                        </div>

                        <!-- Owner id hidden  -->
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtOwn_id" Display="Dynamic" Visible="false">Owner ID</asp:Label>
                                <asp:TextBox runat="server" ID="txtOwn_id" class="form-control form-control-user" Visible="false" ForeColor="Black" />
                            </div>
                            <div class="col-sm-6">
                                <asp:Label runat="server" AssociatedControlID="txtOwn_email" Display="Dynamic" Visible="false">Owner Email</asp:Label>
                                <asp:TextBox runat="server" ID="txtOwn_email" class="form-control form-control-user" Visible="false" ForeColor="Black" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <br />
                                <a href="#" class="btn btn-light btn-icon-split">
                                    <span class="icon text-gray-600">
                                        <i class="fas fa-arrow-right"></i>
                                    </span>
                                    <span id="sp_owner" class="text">
                                        <asp:Label ID="lbl_owner" runat="server"></asp:Label>

                                    </span>
                                </a>
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtProp_name" Display="Dynamic" ForeColor="Black">Property Name</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_name" class="form-control form-control-user" ForeColor="Black" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_name" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Property Name is required." ValidationGroup="VgVillaDetails" />
                            </div>


                            <div class="col-sm-6">
                                <asp:Label runat="server" AssociatedControlID="txtProp_phone" Display="Dynamic" ForeColor="Black">Phone number</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_phone" TextMode="Number" MaxLength="7" class="form-control form-control-user" ValidationGroup="VgVillaDetails" ForeColor="Black" />

                            </div>

                        </div>

                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtProp_email" Display="Dynamic" ForeColor="Black">Property Email</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_email" class="form-control form-control-user"  ReadOnly="true" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                                <asp:RegularExpressionValidator
                                    ID="regEmail"
                                    ControlToValidate="txtProp_email"
                                    Text="(Invalid email)"
                                    ValidationExpression="\w+([-+.’]\w+)*@\w+([-.]\w+)*\.\w+([-.]\w+)*"
                                    runat="server" CssClass="text-danger" Display="Dynamic" />

                            </div>
                            <div class="col-sm-6">
                                <asp:Label runat="server" AssociatedControlID="txtProp_street" Display="Dynamic" ForeColor="Black">Street</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_street" class="form-control form-control-user" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_street" Display="Dynamic" ForeColor="Black"
                                    CssClass="text-danger" ErrorMessage="Street is required." ValidationGroup="VgVillaDetails" />
                            </div>
                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtProp_town" Display="Dynamic" ForeColor="Black">Town</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_town" class="form-control form-control-user" ForeColor="Black" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_town" Display="Dynamic" ForeColor="Black"
                                    CssClass="text-danger" ErrorMessage="Town is required." ValidationGroup="VgVillaDetails" />
                            </div>
                            <div class="col-sm-6">
                                <asp:Label runat="server" AssociatedControlID="ddlDistrict" Display="Dynamic" ForeColor="Black">District</asp:Label><br />
                                <asp:DropDownList runat="server" ID="ddlDistrict" class="form-control form-control-user" Display="Dynamic" ForeColor="Black" ValidationGroup="VgVillaDetails">
                                    <asp:ListItem Value="-1">Select District</asp:ListItem>

                                </asp:DropDownList>
                                <asp:RequiredFieldValidator ControlToValidate="ddlDistrict" InitialValue="-1" runat="server"
                                    CssClass="text-danger" ErrorMessage="District required." Display="Dynamic" ValidationGroup="VgVillaDetails" />

                            </div>

                        </div>
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtProp_priceday" Display="Dynamic" ForeColor="Black">Price per day</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_priceday" TextMode="Number" MaxLength="4" class="form-control form-control-user" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceday" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Price per day is required." ValidationGroup="VgVillaDetails" />
                            </div>
                           
                            <div class="col-sm-6">
                                <asp:Label runat="server" AssociatedControlID="txtProp_postcode" Display="Dynamic" ForeColor="Black">Postal Code</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_postcode" TextMode="Number" MaxLength="5" class="form-control form-control-user" ValidationGroup="VgVillaDetails" ForeColor="Black" />

                            </div>
                        </div>

                    </div>


                    <div class="form-group row">

                        <asp:Button ID="btnSave_own_loc_price" runat="server" ValidateRequestMode="Enabled"
                            Text="Save" ValidationGroup="VgVillaDetails" OnClick="btnSave_own_loc_price_Click" Style="margin-left: 25px"
                            ForeColor="Black" BackColor="#0099CC" Width="70px" />

                        <asp:Button ID="btnCancel_own_loc_price" runat="server" OnClick="btnCancel_own_loc_price_Click" Style="margin-left: 25px"
                            Text="Cancel" CausesValidation="false" ValidationGroup="VgVillaDetails"
                            ForeColor="Black" BackColor="#0099CC" Width="70px" />

                        <asp:Button ID="btnEdit_own_loc_price" runat="server" OnClick="btnEdit_own_loc_price_Click" Style="margin-left: 25px"
                            Text="Edit" CausesValidation="false" ValidationGroup="VgVillaDetails"
                            ForeColor="Black" BackColor="#0099CC" Width="70px" />


                    </div>


                </div>
                <!-- end of Owner Location and Price -->

                <!-- Start of Description -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Property Description</h6>
                    </div>
                    <div class="card-body">

                        <div class="card shadow mb-4">
                            <div class="card-body">
                                <asp:Label ID="lbl_Prop_Desc" runat="server" Text=""></asp:Label>
                                <div class="table-responsive">
                                    <asp:TextBox runat="server" ID="txt_Description" TextMode="MultiLine" Columns="54" Rows="25" ValidationGroup="vgDescription" ForeColor="#003366" BorderColor="#336699" BackColor="White" /><br />
                                </div>
                            </div>
                        </div>


                        <div class="form-group row">


                            <asp:Button ID="Btn_Edit_description" runat="server" ValidationGroup="vgDescription" OnClick="Btn_Edit_description_Click"
                                Text="Edit" Style="margin-left: 25px"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" CausesValidation="false" />

                            <asp:Button ID="Btn_Save_description" runat="server" ValidationGroup="vgDescription"
                                Text="Save" Style="margin-left: 25px"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" OnClick="Btn_Save_description_Click" />

                            <asp:Button ID="Btn_Cancel_description" runat="server" OnClick="Btn_Cancel_description_Click"
                                Text="Cancel" CausesValidation="false" ValidationGroup="vgDescription" Style="margin-left: 25px"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" />

                        </div>


                    </div>
                </div>
                <!-- end of Description -->

                <!-- Hotel Accessibility -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Hotel Accessibility</h6>
                    </div>
                    <div class="card-body">
                        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdditional_info" />
                        <div class="card shadow mb-4">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:Panel ID="pnl_Details" runat="server" Visible="false">

                                        <asp:Label ID="lblMsg_additional_info" runat="server" Text=""></asp:Label>
                                        <br />
                                        <table id="tbl_Details" class="table table-bordered" width="100%" cellspacing="0">
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
                                                    <asp:DropDownList runat="server" ID="ddlDet_name" Display="Dynamic" class="form-control form-control-user" Width="300px" ValidationGroup="vgAdditional_info">
                                                        <asp:ListItem Value="-1">Select Property accessibility</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDet_name" InitialValue="-1" Display="None"
                                                        CssClass="text-danger" ErrorMessage="Please select a value for accessibility." ValidationGroup="vgAdditional_info" />
                                                </td>
                                                <td>
                                                    <asp:TextBox ID="txtCount" TextMode="Number" runat="server" class="form-control form-control-user" />
                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCount"  Display="None"
                                                        CssClass="text-danger" ErrorMessage="Please assign a value for for chosen accesibility." ValidationGroup="vgAdditional_info"  />
                                                    <asp:CompareValidator ID="CompareValidator1" runat="server" ValidationGroup="vgAdditional_info"
                                                        ControlToValidate="txtCount" ErrorMessage="Value must be greater than zero" Display="None"
                                                        Operator="GreaterThan" Type="Integer"
                                                        ValueToCompare="0" />

                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="btnInsert_Add_info" runat="server" CssClass="btn btn-outline-primary" OnClick="btnInsert_Add_info_Click" Text="Insert" ValidationGroup="vgAdditional_info" Visible="false" />
                                                    <asp:Button ID="btnUpdate_Add_info" runat="server" CssClass="btn btn-outline-warning" OnClick="btnUpdate_Add_info_Click" Text="Update" ValidationGroup="vgAdditional_info" Visible="false" />
                                                    <asp:Button ID="btnDelete_Add_info" runat="server" CausesValidation="false" CssClass="btn btn-outline-danger" OnClick="btnDelete_Add_info_Click" OnClientClick="return confirm('Are you sure you want to delete this record?')" Text="Delete" ValidationGroup="vgAdditional_info" Visible="false" />
                                                    <asp:Button ID="btnCancel_Add_info" runat="server" CausesValidation="false" CssClass="btn btn-outline-success" OnClick="btnCancel_Add_info_Click" Text="Cancel" Visible="false" />
                                                </td>
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
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect_Detail" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" Font-Bold="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Villa Details">
                                                <ItemTemplate>
                                                    <!-- display the category name -->
                                                    <asp:Label ID="lblDet_name" Text='<%#Eval("Det_name")%>'
                                                        runat="server" Font-Size="Medium" Font-Bold="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Value">
                                                <ItemTemplate>
                                                    <!-- display the category description -->
                                                    <asp:Label ID="lblCount" Text='<%#Eval("Count")%>'
                                                        runat="server" Font-Size="Medium" Font-Bold="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>


                        <div class="form-group row">


                            <asp:Button ID="Btn_Edit_additional_info" runat="server" ValidationGroup="vgAdditional_info"
                                Text="Edit" Style="margin-left: 25px" OnClick="Btn_Edit_additional_info_Click"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" CausesValidation="false" />

                            <asp:Button ID="Btn_Cancel_additional_info" runat="server" OnClick="Btn_Cancel_additional_info_Click"
                                Text="Cancel" CausesValidation="false" ValidationGroup="vgAdditional_info" Style="margin-left: 25px"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" />

                        </div>


                    </div>
                </div>

                <!-- end of Hotel Accessibility -->

            </div>

            <div class="col-lg-6">

                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Main picture</h6>
                    </div>
                    <div class="card-body">

                        <!-- Main picture -->
                        <asp:Label ID="lbl_main_pic" runat="server" Font-Bold="True"></asp:Label>
                        <div>

                            <asp:Image ID="img_main" runat="server" Height="300px" Width="400px" BorderStyle="Inset" BorderColor="#666666" BorderWidth="5px" />

                        </div>

                        <!-- End of main picture-->
                    </div>

                    <div class="col-md-10" style="padding: 10px; padding-left: 70px">

                        <asp:FileUpload ID="btnupload_main_pic" CssClass="btn btn-outline-primary" Font-Size="Smaller" runat="server" BackColor="#6666FF" ForeColor="White" ValidationGroup="VgMainpic" class="form-control form-control-user" />
                        <br />
                        <br />
                        <asp:Button ID="btnEdit_main_pic" runat="server" OnClick="btnEdit_main_pic_Click" Style="margin-left: 25px"
                            Text="Edit" CausesValidation="false" ValidationGroup="VgMainpic"
                            ForeColor="Black" BackColor="#0099CC" Width="70px" />
                        <asp:Button ID="btnSave_main_pic" runat="server" Text="Save" OnClick="btnSave_main_pic_Click" Style="margin-left: 25px"
                            ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgMainpic" />
                        <asp:Button ID="btnCancel_main_pic" runat="server"
                            Text="Cancel" CausesValidation="false" OnClick="btnCancel_main_pic_Click" Style="margin-left: 25px"
                            ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgMainpic" />
                    </div>
                </div>


                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Pictures for slideshow</h6>
                    </div>
                    <div class="card-body">
                        <asp:Label ID="lbl_slideshow" runat="server"></asp:Label>
                        
                        <!-- Slideshow pictures -->
                        <asp:DataList
                            ID="dlstImages"
                            RepeatColumns="3"
                            runat="server">

                            <ItemTemplate>
                                <asp:Image ID="Image1" ImageUrl='<%# "~/Property/" + Eval("Hotel_id") + "/" + Eval("Img_name", "{0}") %>' runat="server" Height="150" Width="180" />
                                <br>
                                <asp:Panel ID="Panel1" runat="server">
                                    <asp:LinkButton ID="lbtndelete_slideshow" runat="server"
                                        class="btn btn-danger btn-circle btn-sm"
                                        OnClick="lbtndelete_slideshow_Click" Visible="false"
                                        OnClientClick="return confirm('Are you sure you want delete')"
                                        CommandArgument='<%# Eval("Img_name") %>'>
                                        <i class="fas fa-trash"></i>
                                    </asp:LinkButton>
                                </asp:Panel>
                                <asp:Label  ID ="lbl_slideshow_name" runat="server" Text='<%# Eval("Img_name") %>'></asp:Label>
                            </ItemTemplate>

                        </asp:DataList>

                        <div class="col-md-10" style="padding: 10px; padding-left: 70px">

                            <asp:FileUpload ID="btnupload_slideshow" CssClass="btn btn-outline-primary" Font-Size="Smaller" runat="server" BackColor="#6666FF" ForeColor="White" ValidationGroup="VgSlideshow" />

                            <br />
                            <br />
                            <asp:Button ID="btnEdit_slideshow" runat="server" OnClick="btnEdit_slideshow_Click" Style="margin-left: 25px"
                                Text="Edit" CausesValidation="false" ValidationGroup="VgSlideshow"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" />
                            <asp:Button ID="btnSave_slideshow" runat="server" Text="Save" OnClick="btnSave_slideshow_Click" Style="margin-left: 25px"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgSlideshow" />
                            <asp:Button ID="btnCancel_slideshow" runat="server" OnClick="btnCancel_slideshow_Click" Style="margin-left: 25px"
                                Text="Cancel" CausesValidation="false"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgSlideshow" />
                        </div>                      
                        <!-- Slideshow pictures-->

                    </div>
                </div>

                <!-- Hotel Facilities -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Hotel Facilities</h6>
                    </div>
                    <div class="card-body">

                        <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgFacility" />
                        <div class="card shadow mb-4">
                            <div class="card-body">
                                <div class="table-responsive">
                                    <asp:Panel ID="Pnl_facilities" runat="server" Visible="false">
                                        <asp:Label ID="lblMsg_facilities" runat="server" Text=""></asp:Label>
                                        <br />

                                        <table id="tbl_facilities" class="table table-bordered" width="100%" cellspacing="0">
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
                                                    <asp:DropDownList runat="server" ID="ddl_Facility" Display="Dynamic" class="form-control form-control-user">
                                                        <asp:ListItem Value="-1">Select facility</asp:ListItem>
                                                    </asp:DropDownList>

                                                    <asp:RequiredFieldValidator runat="server" ControlToValidate="ddl_Facility" InitialValue="-1" Display="None"
                                                        CssClass="text-danger" ErrorMessage="Please select a facility." ValidationGroup="vgFacility" />
                                                </td>
                                            </tr>
                                            <tr>
                                                <td colspan="2">
                                                    <asp:Button ID="BtnInsert_Facility" runat="server" CssClass="btn btn-outline-primary" OnClick="BtnInsert_Facility_Click" Text="Insert" ValidationGroup="vgFacility" Visible="false" />
                                                    <asp:Button ID="BtnDelete_Facility" runat="server" CausesValidation="false" CssClass="btn btn-outline-danger" OnClick="BtnDelete_Facility_Click" OnClientClick="return confirm('Are you sure you want to delete this record?')" Text="Delete" ValidationGroup="vgFacility" Visible="false" />
                                                    <asp:Button ID="BtnCancel_Facility" runat="server" CausesValidation="false" CssClass="btn btn-outline-success" OnClick="BtnCancel_Facility_Click" Text="Cancel" Visible="false" ValidationGroup="vgFacility" />
                                                </td>
                                        </table>
                                    </asp:Panel>

                                    <br />
                                    <!-- set the primary for the category table as the DataKeynames-->
                                    <asp:GridView ID="gvs_3" OnSelectedIndexChanged="gvs_3_SelectedIndexChanged" DataKeyNames="Fac_id" AutoGenerateColumns="false" OnPreRender="gvs_3_PreRender"
                                        Width="500" AllowPaging="true" PageSize="5" OnPageIndexChanging="gvs_3_PageIndexChanging" runat="server" BorderWidth="1px">
                                        <HeaderStyle BackColor="#9a9a9a" ForeColor="White" Font-Bold="true" Height="30" />
                                        <AlternatingRowStyle BackColor="#f5f5f5" />
                                        <PagerSettings Mode="NextPreviousFirstLast" FirstPageText="First"
                                            NextPageText="Next" PreviousPageText="Prev" LastPageText="Last" />
                                        <Columns>
                                            <asp:TemplateField HeaderText="Action">
                                                <ItemTemplate>
                                                    <asp:LinkButton ID="lbtnSelect_Facility" runat="server" CssClass="btn btn-outline-info" CommandName="Select" Text="Select" Font-Bold="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                            <asp:TemplateField HeaderText="Villa's Facilities">
                                                <ItemTemplate>
                                                    <!-- display the category name -->
                                                    <asp:Label ID="lblDet_name" Text='<%#Eval("Fac_name")%>'
                                                        runat="server" Font-Size="Medium" Font-Bold="True" />
                                                </ItemTemplate>
                                            </asp:TemplateField>
                                        </Columns>
                                    </asp:GridView>
                                </div>
                            </div>
                        </div>

                        <div class="form-group row">

                            <asp:Button ID="Btn_Edit_facilities" runat="server" Style="margin-left: 25px"
                                Text="Edit" CausesValidation="false" ValidationGroup="vgFacility" OnClick="Btn_Edit_facilities_Click"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" />

                            <asp:Button ID="Btn_Cancel_facilities" runat="server" Style="margin-left: 25px"
                                Text="Cancel" CausesValidation="false" ValidationGroup="vgFacility" OnClick="Btn_Cancel_facilities_Click"
                                ForeColor="Black" BackColor="#0099CC" Width="70px" />

                        </div>


                    </div>
                </div>
                <!-- Hotel Facilities -->

            </div>

        </div>

    </div>

    <div style="padding-left: 550px">
        <br />
        <asp:LinkButton ID="Back" runat="server" Text="Back" PostBackUrl="~/Admin/Search_villa.aspx" CssClass="btn btn-info" Font-Size="Large" Font-Bold="True" CausesValidation="false"></asp:LinkButton>
    </div>


    <!-- /.container-fluid -->
</asp:Content>
