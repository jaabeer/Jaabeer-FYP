<%@ Page Title="" Language="C#" MasterPageFile="~/OwnerHome.Master" AutoEventWireup="true" CodeBehind="OwnerFaq.aspx.cs" Inherits="DealProjectTamam.OwnerFaq" %>
<asp:Content ID="Content1" ContentPlaceHolderID="head" runat="server">
<%--<link rel="stylesheet" href="https://cdnjs.cloudflare.com/ajax/libs/font-awesome/4.7.0/css/font-awesome.min.css" />--%>
</asp:Content>
<asp:Content ID="Content2" ContentPlaceHolderID="ContentPlaceHolder1" runat="server">

    <script type="text/javascript">
        function toggleContent(id) {
            var content = document.getElementById(id);
            if (content.style.display === "none") {
                content.style.display = "block";
            } else {
                content.style.display = "none";
            }
        }
    </script>

    <main class="main" style="font-family: Arial, sans-serif; color: #000000; background-color: #ffffff; padding: 20px;">
        <div class="wrap" style="max-width: 1200px; margin: auto;">
            <div class="row">
                <!--hotel three-fourth content-->
                <section class="three-fourth" style="width: 75%; float: left;">
                    
                    <!--inner navigation-->
                    <nav class="inner-nav" style="background-color: #f8f8f8; padding: 10px; border-radius: 5px;">
                        <ul style="list-style: none; padding: 0; display: flex; justify-content: space-around; margin: 0;">
                            <asp:Repeater ID="Repeater2" runat="server">
                                <ItemTemplate>
                                    <li class="description" style="flex: 1; text-align: center;">
                                        <a href='#<%# Eval("Tag") %>' title="Availability" style="text-decoration: none; color: #000000;"><%# Eval("Category") %></a>
                                    </li>
                                </ItemTemplate>
                            </asp:Repeater>
                        </ul>
                    </nav>
                    <!--//inner navigation-->

                    <!--availability-->
                    <section id="uinfo" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">Useful Information</h2>
                            <asp:Repeater ID="Repeater1" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;">
                                                                <%# Eval("faq_question") %>
                                                                <a href="javascript:void(0);" onclick="toggleContent('field<%# Eval("faq_id") %>')" class="gradient-button edit right" style="color: #a52a2a; text-decoration: none; float: right;">See more</a>
                                                            </h4>
                                                        </li>
                                                    </ul>
                                                    <div class="edit_field" id="field<%# Eval("faq_id") %>" style="display: none; margin-top: 10px;">
                                                        <p><label for="new_last_name"><%# Eval("faq_answer") %></label></p>
                                                    </div>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--user registration-->
                    <section id="uregister" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">User Registration</h2>
                            <asp:Repeater ID="Repeater3" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--property registration-->
                    <section id="register" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">Property Registration</h2>
                            <asp:Repeater ID="Repeater4" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--booking issues-->
                    <section id="booking" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">Booking Issues</h2>
                            <asp:Repeater ID="Repeater5" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--payment issues-->
                    <section id="payment" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">Payment Issues</h2>
                            <asp:Repeater ID="Repeater6" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--after sale issues-->
                    <section id="abooking" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">After Sale Issues</h2>
                            <asp:Repeater ID="Repeater7" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--troubleshoot issues-->
                    <section id="tbshoot" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">Troubleshoot Issues</h2>
                            <asp:Repeater ID="Repeater8" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>

                    <!--removal issues-->
                    <section id="removal" class="tab-content" style="margin-top: 20px;">
                        <article>
                            <h2 style="color: #000000;">Removal Issues</h2>
                            <asp:Repeater ID="Repeater9" runat="server">
                                <ItemTemplate>
                                    <div class="text-wrap" style="margin-bottom: 20px; border: 1px solid #ddd; padding: 10px; border-radius: 5px; background-color: #ffffff;">
                                        <table style="width: 100%;">
                                            <tr>
                                                <td>
                                                    <ul style="list-style: none; padding: 0; margin: 0;">
                                                        <li>
                                                            <h4 style="margin: 0;"><%# Eval("faq_question") %></h4>
                                                        </li>
                                                    </ul>
                                                    <label for="new_name">Answer: <%# Eval("faq_answer") %></label>
                                                </td>
                                            </tr>
                                        </table>
                                    </div>
                                </ItemTemplate>
                            </asp:Repeater>
                        </article>
                    </section>
                </section>
            </div>
        </div>
    </main>
</asp:Content>