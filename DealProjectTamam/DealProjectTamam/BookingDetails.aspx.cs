using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.Text;
using System.IO;
using System.Security.Cryptography;
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Net.Http;
using Twilio.Exceptions;
using System.Net.Mail;

namespace DealProjectTamam
{
    public partial class BookingDetails : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        String hotel_id, text_sms;
        protected void Page_Load(object sender, EventArgs e)
        {
            profileData();

        }

        protected void profileData()
        {
            String booking = Request.QueryString["Parameter"].ToString();

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@booking", booking);
            cmd.CommandText = "Select C.Client_fname, C.Client_lname, C.Client_email, C.Client_contact, H.Hotel_name, H.Hotel_town, B.Bk_checkin,  B.Bk_checkout,B.BK_amnt, B.Bk_type, B.Bk_srequest, H.Hotel_id, B.Bk_ref from tblHotel H, tblClient C, tblBooking B Where h.Hotel_id = B.Hotel_id AND C.Client_id = B.Client_id AND b.Bk_id =@booking";
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt_fname.Text = dr["Client_fname"].ToString();
                txt_lname.Text = dr["Client_lname"].ToString();
                txt_email.Text = dr["Client_email"].ToString();
                txt_contact.Text = dr["Client_contact"].ToString();
                txt_region.Text = dr["Hotel_town"].ToString();

                DateTime cin = Convert.ToDateTime(dr["Bk_checkin"]);
                txt_checkin.Text = cin.ToString("dd-MMMM-yyyy");

                DateTime cout = Convert.ToDateTime(dr["Bk_checkout"]);
                txt_checkout.Text = cout.ToString("dd-MMMM-yyyy");

                txt_type.Text = dr["Bk_type"].ToString();
                txt_villa.Text = dr["Hotel_name"].ToString();
                txt_total.Text = dr["BK_amnt"].ToString();
                hotel_id = dr["Hotel_id"].ToString();
                lblref.Text = dr["Bk_ref"].ToString();

            }



            con.Close();
        }



        protected void next_step_Click(object sender, EventArgs e)
        {
            String booking = Request.QueryString["Parameter"].ToString();

            SqlConnection con4 = new SqlConnection(_conString);
            SqlCommand cmd4 = new SqlCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.Connection = con4;
            //Add DELETE statement to delete the selected category for the above CatID
            cmd4.CommandText = "update tblBooking set Bk_srequest=@sreq where Bk_id=@booking";
            cmd4.Parameters.AddWithValue("@booking", booking);
            cmd4.Parameters.AddWithValue("@sreq", txtsreq.Text);

            con4.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd4.ExecuteNonQuery() > 0;
            con4.Close();
            if (IsUpdated)
            {

                lblerror.Text = "Booking updated";
                lblerror.ForeColor = System.Drawing.Color.Green;
                //PrePurchaseMail();
                addNotif();
                Response.Redirect("~/PaymentForm?Parameter=" + booking);

            }
            else
            {
                lblerror.Text = "Error while adding making the booking";
                lblerror.ForeColor = System.Drawing.Color.Red;

            }
        }

        protected void cancel_Click(object sender, EventArgs e)
        {
            Boolean IsDeleted = false;

            String booking = Request.QueryString["Parameter"].ToString();
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.CommandText = "delete from tblBooking where Bk_id=@booking";
            cmd2.Parameters.AddWithValue("@booking", booking);
            cmd2.Connection = con2;
            con2.Open();

            IsDeleted = cmd2.ExecuteNonQuery() > 0;
            con2.Close();

            if (IsDeleted)
            {
                Response.Redirect("~/Hotel_details.aspx?Parameter=" + hotel_id);
            }
            else
            {
                lblerror.Text = "Error while deleting your booking";
                lblerror.ForeColor = System.Drawing.Color.Red;
            }

        }

        protected void text_msg()
        {
            text_sms = "Hello " + txt_lname.Text.Trim() + " " + txt_fname.Text.Trim() + ", payment of Rs: " + txt_total.Text.Trim() + " for the Villa " + txt_villa.Text.Trim() + " is still pending for order #" + lblref.Text.Trim() + " with Best Deals Villa. Refer to your mail for more payment details.";
        }


      

        void PrePurchaseMsg(String text)
        {


            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(); ;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from tblClient where Client_email=@Client_email";
            cmd.Parameters.AddWithValue("@Client_email", Session["client_email"]);

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
                lblerror.Text = "Error";
            }


        }

        protected void addNotif()
        {
            String booking = Request.QueryString["Parameter"].ToString();

            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.Connection = con2;


            cmd2.CommandText = "insert into tblNotification ([Notif_details], [Notif_type], [user_id], [Hotel_id], [Notif_date], [State]) values (@details, @type, @user, @Hotel, @date, @state)";

            cmd2.Parameters.AddWithValue("@details", "Booking of Villa " + txt_villa.Text.Trim() + " from " + txt_checkin.Text.Trim() + " to " + txt_checkout.Text.Trim());
            cmd2.Parameters.AddWithValue("@type", "booking");
            cmd2.Parameters.AddWithValue("@user", Session["client_id"]);
            cmd2.Parameters.AddWithValue("@Hotel", booking);
            cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM/dd/yyyy"));
            cmd2.Parameters.AddWithValue("@state", 0);


            con2.Open();
            Boolean IsAdded = false;
            IsAdded = cmd2.ExecuteNonQuery() > 0;
            con2.Close();
        }


    }
}