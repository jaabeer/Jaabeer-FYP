<%@ Page Title="" Language="C#" MasterPageFile="~/AdminSayf.Master" AutoEventWireup="true" CodeBehind="VillaCreation.aspx.cs" Inherits="DealProjectTamam.AdminS.VillaCreation" %>

<%@ Register Assembly="AjaxControlToolkit" Namespace="AjaxControlToolkit" TagPrefix="ajaxToolkit" %>

<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
     /* Accordion */
<style>
    body {
        color: #000000; /* Make all text black */
    }

    .accordionHeader {
        border: 1px solid #FFFFFF; /* Keep the border white */
        color: #000000; /* Make text black */
        background-color: #FFFFFF; /* Keep the background white */
        font-family: Arial, Sans-Serif;
        font-size: 14px;
        font-weight: bold;
        padding: 5px;
        margin-top: 5px;
        cursor: pointer;
    }

    #master_content .accordionHeader a {
        color: #000000; /* Make text black */
        background: none;
        text-decoration: none;
    }

    #master_content .accordionHeader a:hover {
        background: none;
        text-decoration: underline;
    }

    .accordionHeaderSelected {
        border: 1px solid #FFFFFF; /* Keep the border white */
        color: #000000; /* Make text black */
        background-color: #FFFFFF; /* Keep the background white */
        font-family: Arial, Sans-Serif;
        font-size: 14px;
        font-weight: bold;
        padding: 5px;
        margin-top: 5px;
        cursor: pointer;
    }

    #master_content .accordionHeaderSelected a {
        color: #000000; /* Make text black */
        background: none;
        text-decoration: none;
    }

    #master_content .accordionHeaderSelected a:hover {
        background: none;
        text-decoration: underline;
    }

    .accordionContent {
        background-color: #FFFFFF; /* Keep the background white */
        border: 1px dashed #FFFFFF; /* Keep the border white */
        border-top: none;
        padding: 5px;
        padding-top: 10px;
        color: #000000; /* Make text black */
    }
</style>
<style>
    #gvs {
        width: 100%;
    }

    th {
        background: #FFFFFF; /* Keep the background white */
        color: #000000; /* Make text black */
    }

    .tableCss {
        border: solid 1px #FFFFFF; /* Keep the border white */
    }

   
    .tableCss thead {
        background-color: #FFFFFF; /* Keep the background white */
        color: #000000; /* Make text black */
        padding: 10px;
        text-align: center;
    }

    .tableCss td {
        border: solid 1px #FFFFFF; /* Keep the border white */
        padding: 10px;
        color: #000000; /* Make text black */
    }

   
    .tabTask tfoot {
        background-color: #FFFFFF; /* Keep the background white */
        color: #000000; /* Make text black */
        padding: 10px;
    }

    
    .tabTask tbody {
        background-color: #FFFFFF; /* Keep the background white */
        color: #000000; /* Make text black */
        padding: 10px;
    }

    /* Blue button styles */
    .btn {
        padding: 10px 20px;
        margin-right: 10px;
        border-radius: 5px;
        cursor: pointer;
        border: none;
        background-color: #FFFFFF; /* Keep the background white */
        color: #000000; /* Make text black */
    }
</style>



</asp:Content>

<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">
    <br />
    <h4>Villa Registration</h4>
    <hr />
    <asp:ScriptManager ID="ScriptManager1" runat="server"></asp:ScriptManager>
    <ajaxToolkit:Accordion ID="Accord_create_villa" runat="server" SelectedIndex="0"
        HeaderCssClass="accordionHeader" HeaderSelectedCssClass="accordionHeaderSelected"
        ContentCssClass="accordionContent" FadeTransitions="false" FramesPerSecond="40"
        TransitionDuration="250" AutoSize="None" RequireOpenedPane="false" SuppressHeaderPostbacks="true">

        <Panes>

            <ajaxToolkit:AccordionPane ID="AccordionPane1" runat="server">
                <Header><a href="#" style="color:  #000000">1. Search Owner</a></Header>
                <Content>

                    <asp:GridView ID="gvs" CssClass="table table-striped table-bordered"
                        OnSelectedIndexChanged="gvs_SelectedIndexChanged" DataKeyNames="Own_id"
                        runat="server" AutoGenerateColumns="false" OnPreRender="gvs_PreRender" ClientIDMode="Static" BorderStyle="Solid">
                        <Columns>
                            <asp:BoundField DataField="Own_id" HeaderText="Owner ID" Visible="false" />

                            <asp:TemplateField HeaderText="Last Name" ControlStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblLastName" Text='<%#Eval("Own_lname")%>' runat="server" ForeColor="Black" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:TemplateField HeaderText="First Name" ControlStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblFirstName" Text='<%#Eval("Own_fname")%>'
                                        runat="server" ForeColor="Black" />
                                </ItemTemplate>
                            </asp:TemplateField>


                            <asp:BoundField DataField="Own_nic" HeaderText="NIC Number" ItemStyle-ForeColor="Black" />
                            <asp:BoundField DataField="Own_mobile" HeaderText="Mobile Phone Number" ItemStyle-ForeColor="Black" />



                            <asp:TemplateField HeaderText="Email Address" ControlStyle-ForeColor="Black">
                                <ItemTemplate>
                                    <asp:Label ID="lblOwn_email" Text='<%#Eval("Own_email")%>'
                                        runat="server" ForeColor="Black" />
                                </ItemTemplate>
                            </asp:TemplateField>

                            <asp:ImageField DataImageUrlField="Own_profpic"
                                DataImageUrlFormatString="~/images/{0}"
                                HeaderText="Profile picture" SortExpression="Poster"
                                ControlStyle-Width="35" ControlStyle-CssClass="align-top" />

                            <asp:TemplateField HeaderText="Action">
                                <ItemTemplate>
                                    <%-- Assign the User_Id to the link button using the CommandArgument --%>
                                    <asp:Button ID="btnSelect" CssClass="btn alert-success"
                                        runat="server" Text="Select" CommandName="Select" CausesValidation="false" OnClick="btnSelect_Click"
                                        ForeColor="Black" BackColor="#CCFFFF" />

                                </ItemTemplate>
                            </asp:TemplateField>
                        </Columns>
                    </asp:GridView>
                </Content>
            </ajaxToolkit:AccordionPane>


            <ajaxToolkit:AccordionPane ID="AccordionPane2" runat="server">
                <Header><a href="#" style="color:  #000000">2. Enter Villa Details</a></Header>
                <Content>
                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="VgVillaDetails" />
                    <br />
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
                    <br />

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
                            <asp:Label runat="server" AssociatedControlID="txtOwn_fname" Display="Dynamic" ForeColor="Black">Owner First Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtOwn_fname" class="form-control form-control-user" ForeColor="Black" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Label runat="server" AssociatedControlID="txtOwn_lname" Display="Dynamic" ForeColor="Black">Owner Last Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtOwn_lname" class="form-control form-control-user" ForeColor="Black" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Label runat="server" AssociatedControlID="txtProp_name" Display="Dynamic" ForeColor="Black">Villa Name</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_name" class="form-control form-control-user" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_name" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Villa Name is required." ValidationGroup="VgVillaDetails" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_phone" Display="Dynamic" ForeColor="Black">Villa Phone Number</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_phone" TextMode="Number" MaxLength="7" class="form-control form-control-user" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_phone" Display="Dynamic" ForeColor="Black"
                                CssClass="text-danger" ErrorMessage="Villa Phone Number is required." ValidationGroup="VgVillaDetails" />


                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Label runat="server" AssociatedControlID="txtProp_email" Display="Dynamic" ForeColor="Black">Villa Email</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_email" class="form-control form-control-user" ValidationGroup="VgVillaDetails" ForeColor="Black" />
                            <asp:RegularExpressionValidator
                                ID="regEmail"
                                ControlToValidate="txtProp_email"
                                Text="(Invalid email)"
                                ValidationExpression="\w+([-+.’]\w+)@\w+([-.]\w+)\.\w+([-.]\w+)*"
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
                            <asp:DropDownList runat="server" ID="ddlDistrict" Display="Dynamic" ForeColor="Black" class="form-control form-control-user">
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
                            <asp:Label runat="server" AssociatedControlID="txtProp_priceweek" Display="Dynamic" ForeColor="Black">Price per week</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_priceweek" TextMode="Number" MaxLength="5" class="form-control form-control-user" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_priceweek" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Price per week is required." ValidationGroup="VgVillaDetails" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Label runat="server" AssociatedControlID="txtProp_pricemonth" Display="Dynamic" ForeColor="Black">Price per month</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_pricemonth" TextMode="Number" MaxLength="6" class="form-control form-control-user" ForeColor="Black" ValidationGroup="VgVillaDetails" />
                            <asp:RequiredFieldValidator runat="server" ControlToValidate="txtProp_pricemonth" Display="Dynamic"
                                CssClass="text-danger" ErrorMessage="Price per month is required." ValidationGroup="VgVillaDetails" />
                        </div>
                        <div class="col-sm-6">
                            <asp:Label runat="server" AssociatedControlID="txtProp_postcode" Display="Dynamic" ForeColor="Black">Postal Code</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_postcode" TextMode="Number" MaxLength="5" class="form-control form-control-user" ValidationGroup="VgVillaDetails" ForeColor="Black" />

                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Button ID="btnSave" runat="server" ValidateRequestMode="Enabled"
                                Text="Save" ValidationGroup="VgVillaDetails" OnClick="btnSave_Click"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" CausesValidation="true" />
                            <asp:Button ID="btnCancel" runat="server"
                                Text="Cancel" OnClick="btnCancel_Click" CausesValidation="false" ValidationGroup="VgVillaDetails"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" />
                        </div>
                    </div>

                </Content>

            </ajaxToolkit:AccordionPane>

            <ajaxToolkit:AccordionPane ID="AccordionPane3" runat="server">
                <Header><a href="#" style="color:  #000000">3. Upload Pictures</a></Header>
                <Content>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Label runat="server" AssociatedControlID="txtProp_id" Display="Dynamic" Visible="false">Property ID</asp:Label>
                            <asp:TextBox runat="server" ID="txtProp_id" class="form-control form-control-user" Visible="false" ForeColor="Black" />
                        </div>
                    </div>
                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Label runat="server" Display="Dynamic" ForeColor="Black" Font-Bold="True">Main Picture</asp:Label>
                            <br />
                            <asp:FileUpload ID="btnupload_main_pic" CssClass="btn btn-outline-primary" Font-Size="Smaller" runat="server" EnableViewState="true" ViewStateMode="Enabled" ValidationGroup="VgMainpic" Visible="false" />
                            <br />
                            <asp:Label runat="server" ID="lbl_main_picture" Display="Dynamic" ForeColor="#000066" Visible="false" />
                            <br />


                            <asp:Button ID="btnSave_main_pic" runat="server" Text="Save" Visible="false"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgMainpic" OnClick="btnSave_main_pic_Click" />
                            <asp:Button ID="btnCancel_main_pic" runat="server"
                                Text="Cancel" CausesValidation="false" Visible="false"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgMainpic" OnClick="btnCancel_main_pic_Click" />
                        </div>


                        <div class="col-sm-6">
                            <asp:Label runat="server" Display="Dynamic" ForeColor="Black" Font-Bold="True">Pictures for slideshow</asp:Label>
                            <br />
                            <asp:FileUpload ID="btnUpload_slideshow" CssClass="btn btn-outline-primary" Font-Size="Smaller" runat="server" EnableViewState="true" ViewStateMode="Enabled" ValidationGroup="VgSlideshow" Visible="false" />
                            <br />
                            <asp:Label runat="server" ID="lbl_slideshow" Display="Dynamic" ForeColor="#000066" Visible="false" />
                            <br />

                            <asp:Button ID="BtnSave_slideshow" runat="server" Text="Save"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgSlideshow" OnClick="BtnSave_slideshow_Click" Visible="false" />
                            <asp:Button ID="BtnCancel_slideshow" runat="server"
                                Text="Cancel" CausesValidation="false"
                                ForeColor="Black" BackColor="#0099CC" Width="65px" ValidationGroup="VgSlideshow" OnClick="BtnCancel_slideshow_Click" Visible="false" />
                        </div>

                    </div>

                    <div class="form-group row">
                        <asp:Button ID="BtnNexstep" runat="server" Style="margin-left: 500px" Text="Next Step" Visible="true"
                            ForeColor="Black" BackColor="#0099CC" Width="85px" ValidationGroup="VgSlideshow" OnClick="BtnNexstep_Click" />
                    </div>
                </Content>
            </ajaxToolkit:AccordionPane>

            <ajaxToolkit:AccordionPane ID="AccordionPane4" runat="server">
                <Header><a href="#" style="color:  #000000">4.Specify the number of bedrooms, their size, and other relevant information.</a></Header>
                <Content>

                    <br />
                    <h4>Details of villa.</h4>
                    <hr />
                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdditional_info" />
                    <div class="card shadow mb-4">
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:Label ID="lblMsg_additional_info" runat="server" Text=""></asp:Label>
                                <br />
                                <table class="table table-bordered" width="100%" cellspacing="0">
                                    <thead>
                                        <tr>
                                            <th>Details</th>
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
                                            <asp:DropDownList runat="server" ID="ddlDet_name" Display="Dynamic">
                                                <asp:ListItem Value="-1">Select details</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddlDet_name" InitialValue="-1" Display="Dynamic"
                                                CssClass="text-danger" ErrorMessage="Please select Villa's particular." ValidationGroup="vgAdditional_info" />
                                        </td>
                                        <td>
 <asp:TextBox ID="txtCount" TextMode="Number" runat="server" />
 <asp:RequiredFieldValidator runat="server" ControlToValidate="txtCount" Display="Dynamic"
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
                                            <asp:Button ID="btnDelete_Add_info" runat="server" CssClass="btn btn-outline-danger"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                                Text="Delete" ValidationGroup="vgAdditional_info" CausesValidation="false" Visible="false" OnClick="btnDelete_Add_info_Click" />
                                            <asp:Button ID="btnCancel_Add_info" runat="server"
                                                Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-success" Visible="false" OnClick="btnCancel_Add_info_Click" />
                                        </td>
                                    </tr>
                                </table>
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
                                        <asp:TemplateField HeaderText="Villa's Particulars">
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
                        <asp:Button ID="BtnNexstep_Detail" runat="server" Style="margin-left: 500px" Text="Next Step" Visible="true" OnClick="BtnNexstep_Detail_Click"
                            ForeColor="Black" BackColor="#0099CC" Width="85px" ValidationGroup="vgAdditional_info" CausesValidation="false" />
                    </div>
                </Content>

            </ajaxToolkit:AccordionPane>





            <ajaxToolkit:AccordionPane ID="AccordionPane5" runat="server">
                <Header><a href="#" style="color:  #000000">5. Assign facilities.</a></Header>
                <Content>

                    <br />
                    <h4>Facilities of villa.</h4>
                    <hr />
                    <asp:ValidationSummary runat="server" CssClass="text-danger" ValidationGroup="vgAdditional_info" />
                    <div class="card shadow mb-4">
                        <div class="card-body">
                            <div class="table-responsive">
                                <asp:Label ID="lblMsg_facilities" runat="server" Text=""></asp:Label>
                                <br />
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
                                            <asp:DropDownList runat="server" ID="ddl_Facility" Display="Dynamic">
                                                <asp:ListItem Value="-1">Select facility</asp:ListItem>
                                            </asp:DropDownList>

                                            <asp:RequiredFieldValidator runat="server" ControlToValidate="ddl_Facility" InitialValue="-1" Display="Dynamic"
                                                CssClass="text-danger" ErrorMessage="Please select a facility." ValidationGroup="vgFacility" />
                                        </td>
                                    </tr>
                                    <tr>
                                    <tr>
                                        <td colspan="2">
                                            <asp:Button ID="BtnInsert_Facility" runat="server"
                                                Text="Insert" ValidationGroup="vgFacility" CssClass="btn btn-outline-primary" Visible="false" OnClick="BtnInsert_Facility_Click" />
                                            <asp:Button ID="BtnDelete_Facility" runat="server" CssClass="btn btn-outline-danger"
                                                OnClientClick="return confirm('Are you sure you want to delete this record?')"
                                                Text="Delete" ValidationGroup="vgFacility" CausesValidation="false" Visible="false" OnClick="BtnDelete_Facility_Click" />
                                            <asp:Button ID="BtnCancel_Facility" runat="server"
                                                Text="Cancel" CausesValidation="false" CssClass="btn btn-outline-success" Visible="false" OnClick="BtnCancel_Facility_Click" />
                                        </td>
                                    </tr>
                                </table>
                                <br />
                                <!-- set the primary for the category table as the DataKeynames-->
                                <asp:GridView ID="gvs_3" OnSelectedIndexChanged="gvs_3_SelectedIndexChanged" DataKeyNames="Fac_id" AutoGenerateColumns="false"
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
                        <asp:Button ID="BtnNextstep_facility" runat="server" Style="margin-left: 500px" Text="Next Step" Visible="true" OnClick="BtnNextstep_facility_Click"
                            ForeColor="Black" BackColor="#0099CC" Width="85px" ValidationGroup="vgFacility" CausesValidation="false" />
                    </div>
                </Content>

            </ajaxToolkit:AccordionPane>


            <ajaxToolkit:AccordionPane ID="AccordionPane6" runat="server">
                <Header><a href="#" style="color:  #000000">6. Activate Villa and Notify Owner.</a></Header>
                <Content>

                    <br />
                    <h4>Complete registration and notify owner</h4>

                    <div class="form-group row">
                        <div class="col-sm-6 mb-3 mb-sm-0">
                            <asp:Label runat="server" AssociatedControlID="txtemail" Display="Dynamic">Email Content</asp:Label>
                            <asp:TextBox runat="server" ID="txtemail" TextMode="MultiLine" CssClass="form-control form-control-user" ForeColor="Black" />
                        </div>
                    </div>

                    <div class="form-group row">
                        <asp:Button ID="BtnSendMail" runat="server" Style="margin-left: 500px" Text="Notify User" Visible="false"
                            ForeColor="Black" BackColor="#0099CC" Width="200px" CausesValidation="false" onclick="BtnSendMail_Click" />
                    </div>

                </Content>

            </ajaxToolkit:AccordionPane>




        </Panes>
    </ajaxToolkit:Accordion>
</asp:Content>