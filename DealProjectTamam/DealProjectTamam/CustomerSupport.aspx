<%@ Page Title="" Language="C#" MasterPageFile="~/DealTamam.Master" AutoEventWireup="true" CodeBehind="CustomerSupport.aspx.cs" Inherits="DealProjectTamam.CustomerSupport" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
    <script type="text/javascript">
        function val_mobile_number(source, args) {
            if (args.Value.length != 8)
                args.IsValid = false;
            else
                args.IsValid = true;
        }

        function toggleParagraph(id) {
            var paragraph = document.getElementById(id);
            if (paragraph.style.display === "none") {
                paragraph.style.display = "block";
            } else {
                paragraph.style.display = "none";
            }
        }
    </script>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <main class="main" style="background-color: #f5f5f5; padding: 20px; font-family: Arial, sans-serif;">
        <div class="wrap" style="max-width: 800px; margin: auto;">

            <nav style="background-color: #fff; padding: 10px; margin-bottom: 20px; border: 1px solid #ddd; border-radius: 8px;">
                <!-- Navigation content -->
            </nav>

            <section id="uinfo" class="tab-content" style="border: 1px solid #ddd; padding: 20px; border-radius: 8px; background-color: #fff; margin-bottom: 20px;">
                <h2 style="margin-top: 0;">Suggested FAQ</h2>
                <asp:Repeater ID="Repeater1" runat="server">
                    <ItemTemplate>
                        <div class="text-wrap" style="margin-bottom: 15px;">
                            <ul>
                                <li>
                                    <h5>
                                        <%# Eval("faq_question") %> 
                                        <a href="javascript:void(0);" onclick="toggleParagraph('field<%# Eval("faq_id") %>');" class="edit right" style="text-decoration: none; color: #00aaff;">+</a>
                                    </h5>
                                </li>
                            </ul>
                            <div id="field<%# Eval("faq_id") %>" style="display: none; margin-top: 10px;">
                                <p style="font-size: x-small;"><%# Eval("faq_answer") %></p>
                            </div>
                        </div>
                    </ItemTemplate>
                </asp:Repeater>
            </section>

            <div class="row" style="display: flex; justify-content: space-between;">
                <div class="two-third" style="width: 100%;">

                    <fieldset style="border: 1px solid #ddd; padding: 20px; border-radius: 8px; background-color: #fff;">
                        <h2 style="text-align: center; color: #333;">Customer Support</h2>

                        <asp:Label ID="lblmsg" runat="server" style="color: red;"></asp:Label>
                        
                        <div class="row" style="display: flex; justify-content: space-between; margin-bottom: 15px;">
                            <div class="f-item one-half" style="flex: 1; margin-right: 10px;">
                                <asp:Label runat="server" AssociatedControlID="txtFirstName" Display="Dynamic" style="display: block; margin-bottom: 5px;">First name</asp:Label>
                                <asp:TextBox runat="server" ID="txtFirstName" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtFirstName" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="First Name is required." />
                                <asp:RegularExpressionValidator ID="regFirst_name" ControlToValidate="txtFirstName" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets allowed for First Name"></asp:RegularExpressionValidator>
                            </div>

                            <div class="f-item one-half" style="flex: 1;">
                                <asp:Label runat="server" AssociatedControlID="txtLastName" Display="Dynamic" style="display: block; margin-bottom: 5px;">Last name</asp:Label>
                                <asp:TextBox runat="server" ID="txtLastName" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtLastName" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Last Name is required." />
                                <asp:RegularExpressionValidator ID="regLast_name" ControlToValidate="txtLastName" Display="Dynamic"
                                    ValidationExpression="^[a-zA-Z]+$" runat="server" CssClass="text-danger" ErrorMessage="Only alphabets allowed for Last Name"></asp:RegularExpressionValidator>
                            </div>
                        </div>

                        <div class="row" style="display: flex; justify-content: space-between; margin-bottom: 15px;">
                            <div class="f-item one-half" style="flex: 1; margin-right: 10px;">
                                <asp:Label runat="server" AssociatedControlID="txtEmail" Display="Dynamic" style="display: block; margin-bottom: 5px;">Email Address</asp:Label>
                                <asp:TextBox runat="server" ID="txtEmail" Display="Dynamic" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtEmail" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Email is required." />
                            </div>

                            <div class="f-item one-half" style="flex: 1;">
                                <asp:Label runat="server" AssociatedControlID="txtMobile" Display="Dynamic" style="display: block; margin-bottom: 5px;">Mobile Number</asp:Label>
                                <asp:TextBox runat="server" ID="txtMobile" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtMobile" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Mobile Number is required." />
                                <asp:CustomValidator ID="val_mobNumber" ControlToValidate="txtMobile" CssClass="text-danger" Text="(Mobile Phone Number must contain 8 numbers)"
                                    ClientValidationFunction="val_mobile_number"
                                    runat="server" />
                            </div>
                        </div>

                        <div class="row" style="margin-bottom: 15px;">
                            <div class="f-item full-width" style="width: 100%;">
                                <asp:Label runat="server" AssociatedControlID="ddlCategory" Display="Dynamic" style="display: block; margin-bottom: 5px;">Issue Category</asp:Label>
                                <asp:DropDownList ID="ddlCategory" OnTextChanged="ddlCategory_TextChanged" AutoPostBack="true" runat="server" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:DropDownList>
                                <asp:Label ID="lblcat" runat="server"></asp:Label>
                            </div>
                        </div>

                        <div class="row" style="margin-bottom: 15px;">
                            <div class="f-item full-width" style="width: 100%;">
                                <asp:Label runat="server" AssociatedControlID="txtTitle" Display="Dynamic" style="display: block; margin-bottom: 5px;">Title</asp:Label>
                                <asp:TextBox ID="txttitle" runat="server" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtTitle" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Title is required." />
                            </div>
                        </div>

                        <div class="row" style="margin-bottom: 15px;">
                            <div class="f-item full-width" style="width: 100%;">
                                <asp:Label runat="server" AssociatedControlID="txtdspt" Display="Dynamic" style="display: block; margin-bottom: 5px;">Description</asp:Label>
                                <asp:TextBox ID="txtdspt" TextMode="MultiLine" runat="server" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px; height: 100px;"></asp:TextBox>
                                <asp:RequiredFieldValidator runat="server" ControlToValidate="txtdspt" Display="Dynamic"
                                    CssClass="text-danger" ErrorMessage="Description is required." />
                            </div>
                        </div>

                       <div class="row" style="margin-bottom: 15px;">
                            <div class="f-item full-width" style="width: 100%;">
                                <asp:Label runat="server" AssociatedControlID="fpdocs" Display="Dynamic" style="display: block; margin-bottom: 5px;">Attachments (optional)</asp:Label>
                                <asp:FileUpload ID="fpdocs" runat="server" CssClass="form-control" style="width: 100%; padding: 8px; border: 1px solid #ccc; border-radius: 4px;" />
                            </div>
                        </div>

                        <div class="row">
                            <div class="f-item full-width" style="width: 100%; text-align: center;">
                                <asp:Button ID="btnSend" runat="server" Text="Confirm" class="gradient-button" style="background-color: #00aaff; color: #fff; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer;" OnClick="btnSend_Click" />
                                <asp:Button ID="btnsuccess" runat="server" Visible="false" Text="Support Request Sent Successfully" class="gradient-button" style="background-color: #00aaff; color: #fff; padding: 10px 20px; border: none; border-radius: 5px; cursor: pointer;" CausesValidation="false" />
                            </div>
                        </div>
                    </fieldset>

                </div>
            </div>
        </div>
    </main>

</asp:Content>
