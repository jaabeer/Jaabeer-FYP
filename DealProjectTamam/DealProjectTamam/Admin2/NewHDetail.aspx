<%@ Page Title="" Language="C#" MasterPageFile="~/AdminJs.Master" AutoEventWireup="true" CodeBehind="NewHDetail.aspx.cs" Inherits="DealProjectTamam.NewHDetail" %>
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
                                <asp:Label runat="server" ID="lblMsg" Display="Dynamic" ForeColor="#993300"></asp:Label>
                            </div>
                         

                        </div>


                        <!-- Hotel id hidden -->
                        <div class="form-group row">
                            <div class="col-sm-6 mb-3 mb-sm-0">
                                <asp:Label runat="server" AssociatedControlID="txtProp_id" Display="Dynamic" Visible="false">Hotel ID</asp:Label>
                                <asp:TextBox runat="server" ID="txtProp_id" class="form-control form-control-user" Visible="false" ForeColor="Black" />
                            </div>
                        </div>

                        <!-- Owner id hidden -->
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




                    </div>
                </div>
                <!-- end of Description -->

                <!-- Hotel Accessibility -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Property Accessibility</h6>
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
                        <div>

                            <asp:Image ID="img_main" runat="server" Height="300px" Width="400px" BorderStyle="Inset" BorderColor="#666666" BorderWidth="5px" />

                        </div>

                        <!-- End of main picture-->
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
                                
                            </ItemTemplate>

                        </asp:DataList>

                
                        <!-- Slideshow pictures-->

                    </div>
                </div>

                <!-- Hotel Facilities -->
                <div class="card shadow mb-4">
                    <div class="card-header py-3">
                        <h6 class="m-0 font-weight-bold text-primary">Property Facilities</h6>
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

                       


                    </div>
                </div>
                <!-- Hotel Facilities -->

            </div>

        </div>

    </div>

   <div >
        <br />
        <table style="width:100%">
        <tr><td><center>
          <asp:LinkButton ID="lnkapprove" CssClass="btn alert-success"
                        runat="server" OnClick="lnkapprove_Click"
                       OnClientClick="return confirm('Are you sure you want to approve this villa?');"
                        Text="Approves"
                        Font-Bold="True"></asp:LinkButton>
           </center> </td><td> <center>
          <asp:LinkButton ID="lnkreject" CssClass="btn alert-warning"
                        runat="server" OnClick="LinkButton1_Click"
                      OnClientClick="return confirm('Are you sure you want to reject this villa?');"
                        Text="Decline"
                        Font-Bold="True"></asp:LinkButton>
        </center>  </td> </tr> </table>
        <br /><br />
       <panel id="pnlComment" runat="server">
           <asp:Label ID="lblComment" runat="server" CssClass="form-control" Text="Rejection Reason"></asp:Label>
           <asp:TextBox ID="txtComment" TextMode="MultiLine" runat="server" CssClass=" form-control"></asp:TextBox><br />
           <asp:Button ID="btnReject" runat="server" CssClass="btn alert-primary" OnClick="btnReject_Click" Text="Confirm Reject" />
       </panel>
        <panel id="pnlUpdate" runat="server">
           <asp:Label ID="Label1" runat="server" CssClass="form-control" Text="Optional Comment"></asp:Label>
           <asp:TextBox ID="txt_optcomment" TextMode="MultiLine" runat="server" CssClass=" form-control"></asp:TextBox><br />
           <asp:Button ID="btnRead" runat="server" CssClass="btn alert-primary" OnClick="btnRead_Click" Text="Mark as Seen" />
       </panel>
       <br /><br />
       <center>  <asp:LinkButton ID="Back" runat="server" Text="Back" PostBackUrl="~/Admin2/New_hotel.aspx" CssClass="btn btn-info" Font-Size="Large" Font-Bold="True" CausesValidation="false"></asp:LinkButton></center>
    </div>


    <!-- /.container-fluid -->
</asp:Content>

