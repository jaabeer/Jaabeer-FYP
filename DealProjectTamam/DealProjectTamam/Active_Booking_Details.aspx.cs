using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;
using Aspose.Pdf;
using Aspose.Pdf.Operators;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Net.Http;
using Twilio.Exceptions;
using System.Net.Mail;
using System.Text;


namespace DealProjectTamam
{
    public partial class Active_Booking_Details : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        String text_sms, client_id;
        protected void Page_Load(object sender, EventArgs e)
        {
           

            fill_booking_details();
            getPaymentDetail();
        }
        void fill_booking_details()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
          
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblHotel th on th.Hotel_id=tb.Hotel_id inner join tblDistrict td on td.Dist_id=th.Dist_id where tb.Bk_id = " + qs;
            cmd.Connection = con;
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                client_id = dr["Client_id"].ToString();
                txt_prop_id.Text = dr["Hotel_id"].ToString();
                txt_bk_ref.Text = dr["Bk_ref"].ToString();
                txt_lname.Text = dr["Client_lname"].ToString();
                txt_fname.Text = dr["Client_fname"].ToString();
                txt_NIC.Text = dr["Client_nic"].ToString();
                txt_email.Text = dr["Client_email"].ToString();
                txt_address.Text = dr["Client_address"].ToString();
                txt_email.Text = dr["Client_email"].ToString();
                DateTime bk_date = Convert.ToDateTime(dr["Bk_date"]);
                txt_bk_date.Text = bk_date.ToString("dd-MMMM-yyyy");

                DateTime bk_checkin = Convert.ToDateTime(dr["Bk_checkin"]);
                txt_check_in.Text = bk_checkin.ToString("dd-MMMM-yyyy");

                DateTime bk_checkout = Convert.ToDateTime(dr["Bk_checkout"]);
                txt_check_out.Text = bk_checkout.ToString("dd-MMMM-yyyy");
                txt_price.Text = "Rs " + dr["BK_amnt"].ToString();
                txt_villa_name.Text = dr["Hotel_name"].ToString();
                txt_villa_address.Text = dr["Hotel_street"].ToString() + ", " + dr["Hotel_town"].ToString() + ", " + dr["Dist_name"].ToString();

                if (dr["Bk_state"].ToString() == "C")
                {
                    txt_bk_status.Text = "Confirmed";
                    btn_approve.Attributes["style"] = "display:none";
                    btn_reject.Attributes["style"] = "display:none";


                }
                else
                 if (dr["Bk_state"].ToString() == "B")
                {
                    txt_bk_status.Text = "Awaiting Confirmation";
                    btn_approve.Attributes["style"] = "display:block";
                    btn_reject.Attributes["style"] = "display:block";

                }
                else
                if (dr["Bk_state"].ToString() == "P")
                {
                    txt_bk_status.Text = "Pending Payment";
                    btn_reject.Attributes["style"] = "display:block";
                }
                else

                {
                    txt_bk_status.Text = "Completed";
                    btn_approve.Attributes["style"] = "display:none";
                    btn_reject.Attributes["style"] = "display:none";
                }

                lbl_client.Text = "Booking details for " + dr["Client_fname"].ToString() + " " + dr["Client_lname"].ToString();

             
            }
        }

        void generate_pdf()
        {
            Document document = new Document();

           
            Aspose.Pdf.Page page = document.Pages.Add();       
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Deal Tamam BOOKING"));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Venue: " + txt_villa_name.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Location: " + txt_villa_address.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Booking Reference: " + txt_bk_ref.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Last Name: " + txt_lname.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("First Name: " + txt_fname.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("NIC: " + txt_NIC.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Client Address: " + txt_address.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Client Email: " + txt_email.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Booking Date: " + txt_bk_date.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Check-in Date: " + txt_check_in.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Check-out Date: " + txt_check_out.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Booking status: " + txt_bk_status.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("========================================= "));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("Total: " + txt_price.Text.ToString()));
            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment("========================================= "));

            string folderPath = Server.MapPath("~/Property/" + txt_prop_id.Text.ToString() + "/Booking");
            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            String filename = "~/Property/" + txt_prop_id.Text.ToString() + "/Booking/" + txt_bk_ref.Text.ToString() + ".pdf";

            lbl_message.Text = "";

            if (!IsFileLocked(MapPath(filename)))
            {
                // Save PDF 
                document.Save(MapPath(filename));
                //Open PDF
                System.Diagnostics.Process.Start(MapPath(filename));
            }
            else
            {
                lbl_message.Text = "Document is already opened, please close it first";

            }



        }

        protected void btn_gen_pdf_Click(object sender, EventArgs e)
        {
            generate_pdf();
        }

        public bool IsFileLocked(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                Locked = true;
            }
            return Locked;
        }

        protected void getPaymentDetail()
        {

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            
            cmd.CommandText = "SELECT * from tblPayment where Bk_id = " + qs;
            cmd.Connection = con;
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtpaydate.Text = dr["Payment_date"].ToString();
                txtpaytype.Text = dr["Payment_type"].ToString();
                txtpaid.Text = dr["Payment_amountpaid"].ToString();
               

            }

        }

       

            

        protected void btn_reject_Click(object sender, EventArgs e)
        {

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Delete From tblPayment  where Bk_id=" + qs;

            cmd.Connection = con;
            con.Open();
            Boolean isDeleted = false;
            isDeleted = cmd.ExecuteNonQuery() > 0;
            con.Close();

            if (isDeleted)
            {

                SqlConnection con1 = new SqlConnection(_conString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "delete from tblNotification  where Notif_type='booking' and Hotel_id=" + qs;

                cmd1.Connection = con1;
                con1.Open();
                Boolean IsUpdated1 = false;
                IsUpdated1 = cmd1.ExecuteNonQuery() > 0;
                con1.Close();
                if (IsUpdated1)
                {


                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.CommandText = "delete from tblBooking  where Bk_id=" + qs;

                    cmd2.Connection = con2;
                    con2.Open();
                    Boolean IsUpdated2 = false;
                    IsUpdated2 = cmd2.ExecuteNonQuery() > 0;
                    con1.Close();

                    PrePurchaseMail();
                    lbl_message.Text = "Booking has been Confirmed";


                }



            }

        }

        protected void PrePurchaseMail()
        {
            String client_email = txt_email.Text.Trim();

            text_msg();
            PrePurchaseMsg(text_sms);

            MailMessage mail = new MailMessage();
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("dealtamam02@gmail.com", "ovaujhexpehtkhwz"),
                EnableSsl = true
            };

            try
            {
                mail.From = new MailAddress("dealtamam02@gmail.com");
                mail.To.Add(client_email);
                mail.Subject = "[Deal Tamam]: Confirmation for " + txt_bk_ref.Text.Trim();
                mail.IsBodyHtml = true;

                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("<html><head>");
                msgBody.Append("<style>");
                msgBody.Append("body {font-family: Arial, sans-serif; background-image: url('cid:backgroundImage'); background-size: cover; padding: 20px;}");
                msgBody.Append(".container {background-color: rgba(255, 255, 255, 0.85); padding: 20px; border-radius: 10px; max-width: 600px; margin: auto;}");
                msgBody.Append("h1 {background-color: #4CAF50; color: white; padding: 10px; border-radius: 5px;}");
                msgBody.Append("p {font-size: 16px; color: #333;}");
                msgBody.Append(".highlight {color: #FF5722; font-weight: bold;}");
                msgBody.Append(".important {color: red; font-weight: bold;}");
                msgBody.Append("</style>");
                msgBody.Append("</head><body>");
                msgBody.Append("<div class='container'>");
                msgBody.Append("<center><h1>Booking Confirmation</h1></center>");
                msgBody.Append("<p>Hello <span class='highlight'>" + txt_fname.Text.Trim() + " " + txt_lname.Text.Trim() + ",</span></p>");
                msgBody.Append("<p>We are delighted to confirm your booking. Here are the details:</p>");
                msgBody.Append("<p>Booking Reference: <span class='highlight'>" + txt_bk_ref.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Villa: <span class='highlight'>" + txt_villa_name.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Check-in Date: <span class='highlight'>" + txt_check_in.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Check-out Date: <span class='highlight'>" + txt_check_out.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Total Price: <span class='highlight'>" + txt_price.Text.Trim() + "</span></p>");
                msgBody.Append("<br><hr>");
                msgBody.Append("<center><h1 style='background-color:powderblue;'>Order Details</h1></center>");
                msgBody.Append("<p>Client Address: <span class='highlight'>" + txt_address.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Client Email: <span class='highlight'>" + txt_email.Text.Trim() + "</span></p>");
                msgBody.Append("<br><hr>");
                msgBody.Append("<center><h5 style='background-color:powderblue;'>Copyright Deal Tamam Hotel/h5></center>");
                msgBody.Append("</div>");
                msgBody.Append("</body></html>");

                mail.Body = msgBody.ToString();

                // Adding the background image as an embedded resource
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                LinkedResource background = new LinkedResource(Server.MapPath("~/images/home/slider3.jpg"), "image/jpeg");
                background.ContentId = "backgroundImage";
                htmlView.LinkedResources.Add(background);
                mail.AlternateViews.Add(htmlView);

                sc.Send(mail);
                Response.Write("Confirmation mail sent successfully.");
            }
            catch (Exception ex)
            {
                Response.Write("Failed to send email. Error: " + ex.Message);
            }
        }

        void PrePurchaseMsg(String text)
        {


            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(); ;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from tblClient where Client_email=@Client_email";
            cmd.Parameters.AddWithValue("@Client_email", txt_email.Text.Trim());

            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        var accountSid = "AC3482da7e0b13e543691232f480f05f36";
                        var authToken = "730b6b56d244a0a99130c018f81b56af";
                        TwilioClient.Init(accountSid, authToken);

                        var client_phone_num = "+230" + dr["Client_contact"].ToString();

                        var message = MessageResource.Create(to: new Twilio.Types.PhoneNumber(client_phone_num), from: new Twilio.Types.PhoneNumber(""), body: text);

                        Console.WriteLine(message.Body);
                    }
                    catch (ApiException e)

                    {
                        //handle exception state here
                    }
                }

            }
            else
            {
                lbl_message.Text = "Error";
            }


        }

        protected void text_msg()
        {
            text_sms = "Hello Client  " + txt_lname.Text.Trim() + " " + txt_fname.Text.Trim() + ", we are delighted to confirm your order  #" + txt_bk_ref.Text.Trim() + " for the Hotel " + txt_villa_name.Text.Trim() + " from " + txt_check_in.Text.Trim() + " till " + txt_check_out.Text.Trim() + ". For Customer Service call on  245-45-90";
        }



        protected void rejectMail()
        {

            String client_email = txt_email.Text.Trim();

            text_msg_reject();
            reject_msg(text_sms);

            MailMessage mail = new MailMessage();
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("dealtamam02@gmail.com", "ovaujhexpehtkhwz"),
                EnableSsl = true
            };

            try
            {
                mail.From = new MailAddress("dealtamam02@gmail.com");
                mail.To.Add(client_email);
                mail.Subject = "[Deal Tamam]: Rejection for " + txt_bk_ref.Text.Trim();
                mail.IsBodyHtml = true;

                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("<html><head>");
                msgBody.Append("<style>");
                msgBody.Append("body {font-family: Arial, sans-serif; background-image: url('cid:backgroundImage'); background-size: cover; padding: 20px;}");
                msgBody.Append(".container {background-color: rgba(255, 255, 255, 0.85); padding: 20px; border-radius: 10px; max-width: 600px; margin: auto;}");
                msgBody.Append("h1 {background-color: #FF5722; color: white; padding: 10px; border-radius: 5px;}");
                msgBody.Append("p {font-size: 16px; color: #333;}");
                msgBody.Append(".highlight {color: #4CAF50; font-weight: bold;}");
                msgBody.Append(".important {color: red; font-weight: bold;}");
                msgBody.Append("</style>");
                msgBody.Append("</head><body>");
                msgBody.Append("<div class='container'>");
                msgBody.Append("<center><h1>Booking Rejection</h1></center>");
                msgBody.Append("<p>Hello <span class='highlight'>" + txt_fname.Text.Trim() + " " + txt_lname.Text.Trim() + ",</span></p>");
                msgBody.Append("<p>We regret to inform you that your booking has been rejected. Here are the details:</p>");
                msgBody.Append("<p>Booking Reference: <span class='highlight'>" + txt_bk_ref.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Hotel: <span class='highlight'>" + txt_villa_name.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Check-in Date: <span class='highlight'>" + txt_check_in.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Check-out Date: <span class='highlight'>" + txt_check_out.Text.Trim() + "</span></p>");
                msgBody.Append("<p class='important'>Please make another booking or contact customer support for more assistance.</p>");
                msgBody.Append("<br><hr>");
                msgBody.Append("<center><h1 style='background-color:powderblue;'>Order Details</h1></center>");
                msgBody.Append("<p>Client Address: <span class='highlight'>" + txt_address.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Client Email: <span class='highlight'>" + txt_email.Text.Trim() + "</span></p>");
                msgBody.Append("<br><hr>");
                msgBody.Append("<center><h5 style='background-color:powderblue;'>Copyright Deal Tamam Hotel</h5></center>");
                msgBody.Append("</div>");
                msgBody.Append("</body></html>");

                mail.Body = msgBody.ToString();

                // Adding the background image as an embedded resource
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                LinkedResource background = new LinkedResource(Server.MapPath("~/images/home/slider3.jpg"), "image/jpeg");
                background.ContentId = "backgroundImage";
                htmlView.LinkedResources.Add(background);
                mail.AlternateViews.Add(htmlView);

                sc.Send(mail);
                Response.Write("Rejection mail sent successfully.");
            }
            catch (Exception ex)
            {
                Response.Write("Failed to send email. Error: " + ex.Message);
            }
        }

        protected void text_msg_reject()
        {
            text_sms = "Hello Client  " + txt_lname.Text.Trim() + " " + txt_fname.Text.Trim() + ", your order  #" + txt_bk_ref.Text.Trim() + " for the Hotel " + txt_villa_name + " from " + txt_check_in.Text.Trim() + " till " + txt_check_out.Text.Trim() + " has been rejected. Make a new booking or contact Customer Service call on 245-45-90. Soory for any inconvenience";
        }

        protected void btn_approve_Click(object sender, EventArgs e)
        {
           
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblBooking set Bk_state='C' where Bk_id=" + qs;

            cmd.Connection = con;
            con.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();

            if (IsUpdated)
            {

                SqlConnection con1 = new SqlConnection(_conString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "update tblPayment set Payment_status='1' where Bk_id=" + qs;

                cmd1.Connection = con1;
                con1.Open();
                Boolean IsUpdated1 = false;
                IsUpdated1 = cmd1.ExecuteNonQuery() > 0;
                con1.Close();
                if (IsUpdated1)
                {
                    PrePurchaseMail();
                    lbl_message.Text = "Booking has been Confirmed";
                    fill_booking_details();


                }



            }



        }
    

        void reject_msg(String text)
        {


            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(); ;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from tblClient where Client_email=@Client_email";
            cmd.Parameters.AddWithValue("@Client_email", txt_email.Text.Trim());

            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        var accountSid = "AC3482da7e0b13e543691232f480f05f36";
                        var authToken = "730b6b56d244a0a99130c018f81b56af";
                        TwilioClient.Init(accountSid, authToken);

                        var client_phone_num = "+230" + dr["Client_contact"].ToString();

                        var message = MessageResource.Create(to: new Twilio.Types.PhoneNumber(client_phone_num), from: new Twilio.Types.PhoneNumber("+15189636978"), body: text);

                        Console.WriteLine(message.Body);
                    }
                    catch (ApiException e)

                    {
                        //handle exception state here
                    }
                }

            }
            else
            {
                lbl_message.Text = "Error";
            }


        }

    }
}