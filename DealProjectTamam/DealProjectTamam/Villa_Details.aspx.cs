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
namespace DealProjectTamam
{
    public partial class Villa_Details : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        String Rating;

        protected void Page_Load(object sender, EventArgs e)
        {
            check_rating();

            if (!Page.IsPostBack)
            {
                Session["Page_name"] = "Villa_details";
                btnreview_update.Attributes["style"] = "display:none";
                PropertyData();
                VilaImage();
                FacilitiesData();
                checkwifi();
                check_rating();
                checkparking();
                loadmap();
                ReviewData();



                lblbooking.Text = "Please select your booking date here";
                ListItem li1 = new ListItem("Select Rating", "-1");
                ListItem li2 = new ListItem("0", "0");
                ListItem li3 = new ListItem("1", "1");
                ListItem li4 = new ListItem("2", "2");
                ListItem li5 = new ListItem("3", "3");
                ListItem li6 = new ListItem("4", "4");
                ListItem li7 = new ListItem("5", "5");
                ddlrating.Items.Insert(0, li1);
                ddlrating.Items.Insert(1, li2);
                ddlrating.Items.Insert(2, li3);
                ddlrating.Items.Insert(3, li4);
                ddlrating.Items.Insert(4, li5);
                ddlrating.Items.Insert(5, li6);
                ddlrating.Items.Insert(6, li7);

            }

        }
        String booking_id;
        String booking_checkout;
        String booking_checkin;
        String review_id;
        String RefNO;
        protected void VilaImage()
        {

            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblImage Where Villa_id=@villa";
            cmd.Parameters.AddWithValue("@villa", villa);

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();
        }

        protected void PropertyData()
        {
            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblVilla Where Villa_id=@villa";
            cmd.Parameters.AddWithValue("@villa", villa);
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                villa_img.ImageUrl = dr["Villa_image"].ToString();
                Image2.ImageUrl = dr["Villa_image"].ToString();
                Image3.ImageUrl = dr["Villa_image"].ToString();
                lbl_day.Text = dr["Villa_priceday"].ToString();
                lbl_month.Text = dr["Villa_pricemonth"].ToString();
                lbl_week.Text = dr["Villa_priceweek"].ToString();
                lbl_desc.Text = dr["Villa_desc"].ToString();
                lblpropname.Text = dr["Villa_name"].ToString().Trim();
                lblrating.Text = dr["Villa_Rating"].ToString();

            }



            con.Close();



            con.Close();
        }

        protected void FacilitiesData()
        {
            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  F.Fac_name FROM tblFacilities F, tblFacilities_Villa FV Where  F.Fac_id = FV.Fac_id AND Villa_id=@villa";
            cmd.Parameters.AddWithValue("@villa", villa);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }

            Repeater2.DataSource = dt;
            Repeater2.DataBind();

            con.Close();
        }

        protected void checkwifi()
        {
            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblFacilities_Villa FV Where Villa_id=@villa and Fac_id=71";
            cmd.Parameters.AddWithValue("@villa", villa);


            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                lblwifi.Text = "Ac";

            }
            else
            {
                lblwifi.Text = "No Ac Available";
            }

        }

        protected void checkparking()
        {
            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblFacilities_Villa FV Where Villa_id=@villa and Fac_id=72";
            cmd.Parameters.AddWithValue("@villa", villa);


            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                lblparking.Text = "Parking";

            }
            else
            {
                lblparking.Text = "No Parking";
            }

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtcheckin.Text != "" && txtcheckout.Text != "")

            {
                DateTime checkin = Convert.ToDateTime(txtcheckin.Text);
                DateTime checkout = Convert.ToDateTime(txtcheckout.Text);

                if (checkin > System.DateTime.Now && checkout > System.DateTime.Now)
                {
                    if (checkout > checkin)
                    {
                        checkavailability();
                    }
                    else
                    {
                        lblbooking.Text = "Checkout date should be after Checkin date";
                        lblbooking.ForeColor = System.Drawing.Color.Red;
                    }

                }
                else
                {
                    lblbooking.Text = "Checkin or Checkout date should be after " + System.DateTime.Now;
                    lblbooking.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblbooking.Text = "Please select a checkin and checkout date.";
                lblbooking.ForeColor = System.Drawing.Color.Red;

            }

        }

        protected void btn_booking_Click(object sender, EventArgs e)
        {


            String villa = Request.QueryString["Parameter"].ToString();
            if (txtcheckin.Text != "" && txtcheckout.Text != "")

            {
                DateTime checkin = DateTime.Parse(txtcheckin.Text);
                DateTime checkout = DateTime.Parse(txtcheckout.Text);
                double x = (checkout.Date - checkin.Date).TotalDays;

                if (x < 6)
                {

                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = con;


                    cmd.CommandText = "select * from tblBooking where Client_id=@clientid and Villa_id=@villaid and Bk_checkin between @checkin and @checkout or Bk_checkout between @checkin and @checkout";
                    cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
                    cmd.Parameters.AddWithValue("@villaid", villa);
                    cmd.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                    cmd.Parameters.AddWithValue("@checkout", txtcheckout.Text);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    using (da)
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblbooking.Text = "There is already a booking for this villa for the dates " + txtcheckin.Text + " and " + txtcheckout.Text;
                            lblbooking.ForeColor = System.Drawing.Color.Red;
                        }


                        else
                        {

                            int price = int.Parse(lbl_day.Text);
                            int cost = ((int)x) * price;


                            SqlConnection con2 = new SqlConnection(_conString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;

                            cmd2.Connection = con2;

                            cmd2.CommandText = "insert into tblBooking (Bk_date, Bk_checkin, Bk_checkout, Bk_type,Bk_amnt, Bk_state, Villa_id, Client_id, Show) values (@date, @checkin, @checkout, @type, @cost, @state, @villaid, @clientid, @show)";
                            cmd2.Parameters.AddWithValue("@date", System.DateTime.Now);
                            cmd2.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                            cmd2.Parameters.AddWithValue("@checkout", txtcheckout.Text);
                            cmd2.Parameters.AddWithValue("@type", "Daily");
                            cmd2.Parameters.AddWithValue("@cost", cost);
                            cmd2.Parameters.AddWithValue("@state", "P");
                            cmd2.Parameters.AddWithValue("@villaid", villa);
                            cmd2.Parameters.AddWithValue("@show", 1);
                            cmd2.Parameters.AddWithValue("@clientid", Session["client_id"]);
                            con2.Open();
                            Boolean IsAdded = false;
                            IsAdded = cmd2.ExecuteNonQuery() > 0;
                            con2.Close();
                            if (IsAdded)
                            {
                                Check_booking_id();
                                Referencenumber();
                                updateRefnum();
                                Response.Redirect("~/Booking_Details?Parameter=" + booking_id);
                            }
                            else
                            {
                                lblbooking.Text = "Error while making the booking";
                                lblbooking.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    lblbooking.Text = "Your staying is more than 7 days,please choose other packages";
                    lblbooking.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {

                lblbooking.Text = "Please select a checkin and checkout date.";
                lblbooking.ForeColor = System.Drawing.Color.Red;

            }
        }



        protected void Check_booking_id()
        {
            String villa = Request.QueryString["Parameter"].ToString();

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tblBooking where Client_id=@clientid and Villa_id=@villaid and Bk_checkin=@checkin and BK_checkout=@checkout";
            cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
            cmd.Parameters.AddWithValue("@villaid", villa);
            cmd.Parameters.AddWithValue("@checkin", txtcheckin.Text);
            cmd.Parameters.AddWithValue("@checkout", txtcheckout.Text);


            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                booking_id = dr["Bk_id"].ToString();
                booking_checkout = dr["Bk_checkout"].ToString();
            }

        }

        protected void Referencenumber()
        {


            RefNO = "BK" + DateTime.Now.ToString("MMddyyyy") + booking_id;
        }


        protected void updateRefnum()
        {
            Boolean IsUpdated = false;
            SqlConnection con4 = new SqlConnection(_conString);
            SqlCommand cmd4 = new SqlCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.Connection = con4;

            cmd4.CommandText = "update tblBooking set Bk_ref=@ref  where Bk_id=@booking";
            cmd4.Parameters.AddWithValue("@booking", booking_id);
            cmd4.Parameters.AddWithValue("@ref", RefNO);
            cmd4.Connection = con4;
            con4.Open();
            IsUpdated = cmd4.ExecuteNonQuery() > 0;
            con4.Close();

        }

        protected void check_rating()
        {
            String villa = Request.QueryString["Parameter"].ToString();

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tblBooking where Client_id=@clientid and Villa_id=@villaid";
            cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
            cmd.Parameters.AddWithValue("@villaid", villa);



            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlDataReader dr;
                    con.Open();
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {

                        booking_id = dr["Bk_id"].ToString();
                        booking_checkout = dr["Bk_checkout"].ToString();
                        booking_checkin = dr["Bk_checkin"].ToString();
                    }
                    con.Close();

                    DateTime checkout = DateTime.Parse(booking_checkout);
                    DateTime checkin = DateTime.Parse(booking_checkin);
                    DateTime limit = checkout.AddDays(10);
                    DateTime today = DateTime.Today;

                    if (limit > today)
                    {

                        SqlConnection con2 = new SqlConnection(_conString);
                        // Create Command
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.Connection = con2;
                        cmd2.CommandType = CommandType.Text;
                        cmd2.CommandText = "select * from tblReviews where Client_id=@clientid and Villa_id=@villaid";
                        cmd2.Parameters.AddWithValue("@clientid", Session["client_id"]);
                        cmd2.Parameters.AddWithValue("@villaid", villa);
                        SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                        DataTable dt2 = new DataTable();
                        using (da2)
                        {
                            da2.Fill(dt2);
                            if (dt2.Rows.Count > 0)
                            {
                                btnreview.Attributes["style"] = "display:none";
                                btnreview_update.Attributes["style"] = "display:block";
                                SqlDataReader dr2;
                                con2.Open();
                                dr2 = cmd2.ExecuteReader();
                                while (dr2.Read())
                                {

                                    txtpos.Text = dr2["Positive_Review"].ToString();
                                    txtneg.Text = dr2["Negative_Review"].ToString();
                                    review_id = dr2["Review_id"].ToString();
                                }

                            }
                            else
                            {
                                btnreview.Attributes["style"] = "display:block";
                                btnreview_update.Attributes["style"] = "display:none";
                            }

                        }


                    }
                    else
                    {
                        lblreview.Text = "Your 10 days limit to post a review is over!!";
                        btnrev.Attributes["style"] = "display:none";
                        lblreview.ForeColor = System.Drawing.Color.Red;

                    }


                }
                else
                {
                    lblreview.Text = "You cannot post review for this Villa as you never make a booking here";
                    lblreview.ForeColor = System.Drawing.Color.Red;
                    btnrev.Attributes["style"] = "display:none";

                }


            }


        }

        protected void ReviewData()
        {
            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "Select * from tblReviews R, tblClient C where R.Client_id = C.Client_id and R.Villa_id=@villa Order By R.Review_Date DESC";
            cmd.Parameters.AddWithValue("@villa", villa);
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }

            Repeater4.DataSource = dt;
            Repeater4.DataBind();

            con.Close();
        }

        protected void btnreview_Click(object sender, EventArgs e)
        {
            if (ddlrating.SelectedValue != "-1")
            {

                String villa = Request.QueryString["Parameter"].ToString();
                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.Connection = con2;

                //Add DELETE statement to delete the selected category for the above CatID
                cmd2.CommandText = "insert into tblReviews ([Villa_id], [Client_id],[Positive_Review], [Negative_Review], [Rating], [Review_Date]) values (@villa_id, @client_id, @pos, @neg, @rating, @today)";
                //Create a parametererized query for CatID
                cmd2.Parameters.AddWithValue("@villa_id", villa);
                cmd2.Parameters.AddWithValue("@client_id", Session["client_id"]);
                cmd2.Parameters.AddWithValue("@pos", txtpos.Text.Trim());
                cmd2.Parameters.AddWithValue("@neg", txtneg.Text.Trim());
                cmd2.Parameters.AddWithValue("@rating", ddlrating.SelectedValue);
                cmd2.Parameters.AddWithValue("@today", DateTime.Today);

                con2.Open();
                Boolean IsAdded = false;
                IsAdded = cmd2.ExecuteNonQuery() > 0;
                con2.Close();
                if (IsAdded)
                {
                    RatingAVG();
                    Boolean IsUpdated = false;
                    SqlConnection con4 = new SqlConnection(_conString);
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.Connection = con4;

                    cmd4.CommandText = "update tblVilla set Villa_Rating=@rating  where Villa_id=villa";
                    cmd4.Parameters.AddWithValue("@rating", Rating);
                    cmd2.Parameters.AddWithValue("@villa_id", villa);

                    cmd4.Connection = con4;
                    con4.Open();
                    IsUpdated = cmd4.ExecuteNonQuery() > 0;
                    con4.Close();
                    if (IsUpdated)
                    {
                        PropertyData();
                        check_rating();
                        ReviewData();
                    }


                }
                else
                {
                    lblrating_error.Text = "Error while posting your review";
                }
            }
            else
            {
                lblreview.Text = "Please Select a Rating";
            }

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#reviews';", true);
            ResetAll();
        }

        protected void ResetAll()
        {
            txtneg.Text = "";
            txtpos.Text = "";
            ddlrating.SelectedValue = "-1";
        }


        protected void btn_bookingweek_Click(object sender, EventArgs e)
        {
            String villa = Request.QueryString["Parameter"].ToString();

            if (txtcheckin.Text != "" && txtcheckout.Text != "")

            {
                DateTime checkin = DateTime.Parse(txtcheckin.Text);
                DateTime checkout = DateTime.Parse(txtcheckout.Text);
                double x = (checkout.Date - checkin.Date).TotalDays;

                if (x > 5 && x < 28)
                {

                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = con;


                    cmd.CommandText = "select * from tblBooking where Client_id=@clientid and Villa_id=@villaid and Bk_checkin between @checkin and @checkout or Bk_checkout between @checkin and @checkout";
                    cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
                    cmd.Parameters.AddWithValue("@villaid", villa);
                    cmd.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                    cmd.Parameters.AddWithValue("@checkout", txtcheckout.Text);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    using (da)
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblbooking.Text = "There is already a booking for this villa for the dates " + txtcheckin.Text + " and " + txtcheckout.Text;
                            lblbooking.ForeColor = System.Drawing.Color.Red;
                        }


                        else
                        {

                            int price = int.Parse(lbl_week.Text);
                            double cost = (((double)x) / 6) * price;


                            SqlConnection con2 = new SqlConnection(_conString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;

                            cmd2.Connection = con2;

                            cmd2.CommandText = "insert into tblBooking (Bk_date, Bk_checkin, Bk_checkout, Bk_type,Bk_amnt, Bk_state, Villa_id, Client_id) values (@date, @checkin, @checkout, @type, @cost, @state, @villaid, @clientid)";
                            cmd2.Parameters.AddWithValue("@date", System.DateTime.Now);
                            cmd2.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                            cmd2.Parameters.AddWithValue("@checkout", txtcheckout.Text);
                            cmd2.Parameters.AddWithValue("@type", "Weekly");
                            cmd2.Parameters.AddWithValue("@cost", cost);
                            cmd2.Parameters.AddWithValue("@state", "P");
                            cmd2.Parameters.AddWithValue("@villaid", villa);
                            cmd2.Parameters.AddWithValue("@clientid", Session["client_id"]);
                            con2.Open();
                            Boolean IsAdded = false;
                            IsAdded = cmd2.ExecuteNonQuery() > 0;
                            con2.Close();
                            if (IsAdded)
                            {
                                Check_booking_id();
                                Referencenumber();
                                updateRefnum();
                                Response.Redirect("~/Client/Booking_Details?Parameter=" + booking_id);
                            }
                            else
                            {
                                lblbooking.Text = "Error while making the booking";
                                lblbooking.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
                else
                {
                    lblbooking.Text = "Ensure the stay duration is more than 6 days and less than 28 days";
                    lblbooking.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {

                lblbooking.Text = "Please select a checkin and checkout date.";
                lblbooking.ForeColor = System.Drawing.Color.Red;
            }
        }


        protected void btn_bookingmonth_Click(object sender, EventArgs e)
        {




            String villa = Request.QueryString["Parameter"].ToString();


            if (txtcheckin.Text != "" && txtcheckout.Text != "")

            {
                DateTime checkin = DateTime.Parse(txtcheckin.Text);
                DateTime checkout = DateTime.Parse(txtcheckout.Text);
                double x = (checkout.Date - checkin.Date).TotalDays;


                if (x > 28)
                {


                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;

                    cmd.Connection = con;


                    cmd.CommandText = "select * from tblBooking where  Villa_id=@villaid and Bk_checkin between @checkin and @checkout or Bk_checkout between @checkin and @checkout";
                    cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
                    cmd.Parameters.AddWithValue("@villaid", villa);
                    cmd.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                    cmd.Parameters.AddWithValue("@checkout", txtcheckout.Text);


                    SqlDataAdapter da = new SqlDataAdapter(cmd);
                    DataTable dt = new DataTable();
                    using (da)
                    {
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblbooking.Text = "There is already a booking for this villa for the dates " + txtcheckin.Text + " and " + txtcheckout.Text;
                            lblbooking.ForeColor = System.Drawing.Color.Red;
                        }


                        else
                        {

                            int price = int.Parse(lbl_month.Text);
                            decimal cost = (((decimal)x) / 30) * price;


                            SqlConnection con2 = new SqlConnection(_conString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;

                            cmd2.Connection = con2;

                            cmd2.CommandText = "insert into tblBooking (Bk_date, Bk_checkin, Bk_checkout, Bk_type,Bk_amnt, Bk_state, Villa_id, Client_id) values (@date, @checkin, @checkout, @type, @cost, @state, @villaid, @clientid)";
                            cmd2.Parameters.AddWithValue("@date", System.DateTime.Now);
                            cmd2.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                            cmd2.Parameters.AddWithValue("@checkout", txtcheckout.Text);
                            cmd2.Parameters.AddWithValue("@type", "Monthly");
                            cmd2.Parameters.AddWithValue("@cost", cost);
                            cmd2.Parameters.AddWithValue("@state", "P");
                            cmd2.Parameters.AddWithValue("@villaid", villa);
                            cmd2.Parameters.AddWithValue("@clientid", Session["client_id"]);
                            con2.Open();
                            Boolean IsAdded = false;
                            IsAdded = cmd2.ExecuteNonQuery() > 0;
                            con2.Close();
                            if (IsAdded)
                            {
                                Check_booking_id();
                                Referencenumber();
                                updateRefnum();
                                Response.Redirect("~/Client/Booking_Details?Parameter=" + booking_id);
                            }
                            else
                            {
                                lblbooking.Text = "Error while making the booking";
                                lblbooking.ForeColor = System.Drawing.Color.Red;
                            }
                        }
                    }
                }
                else
                {

                    lblbooking.Text = "Ensure the stay duration is  more 28 days";
                    lblbooking.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblbooking.Text = "Please select a checkin and checkout date.";
                lblbooking.ForeColor = System.Drawing.Color.Red;
            }
        }

        protected void btnreview_update_Click(object sender, EventArgs e)
        {

            if (ddlrating.SelectedValue != "-1")
            {


                String villa = Request.QueryString["Parameter"].ToString();



                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.Connection = con2;


                cmd2.CommandText = "Update tblReviews set Positive_Review=@pos, Negative_Review=@neg, Rating=@rating, Review_Date=@today Where Review_id=@rev_id";

                cmd2.Parameters.AddWithValue("@rev_id", review_id);
                cmd2.Parameters.AddWithValue("@villa_id", villa);
                cmd2.Parameters.AddWithValue("@client_id", Session["client_id"]);
                cmd2.Parameters.AddWithValue("@pos", txtpos.Text.Trim());
                cmd2.Parameters.AddWithValue("@neg", txtneg.Text.Trim());
                cmd2.Parameters.AddWithValue("@rating", ddlrating.SelectedValue);
                cmd2.Parameters.AddWithValue("@today", DateTime.Today);

                con2.Open();
                Boolean IsUpdated = false;
                IsUpdated = cmd2.ExecuteNonQuery() > 0;
                con2.Close();
                if (IsUpdated)
                {
                    villa = Request.QueryString["Parameter"].ToString();

                    RatingAVG();
                    Boolean IsUpdated2 = false;
                    SqlConnection con4 = new SqlConnection(_conString);
                    SqlCommand cmd4 = new SqlCommand();
                    cmd4.CommandType = CommandType.Text;
                    cmd4.Connection = con4;

                    cmd4.CommandText = "update tblVilla set Villa_Rating=@rating  where Villa_id=@villa_id";
                    cmd4.Parameters.AddWithValue("@rating", Rating);
                    cmd4.Parameters.AddWithValue("@villa_id", villa);

                    cmd4.Connection = con4;
                    con4.Open();
                    IsUpdated2 = cmd4.ExecuteNonQuery() > 0;
                    con4.Close();
                    if (IsUpdated2)
                    {
                        PropertyData();
                        ReviewData();
                        check_rating();
                    }
                }
                else
                {

                    lblrating_error.Text = "Error while Updating your review";
                    lblrating_error.ForeColor = System.Drawing.Color.Red;

                    check_rating();
                }
            }
            else
            {


                lblreview.Text = "Please choosing a Rating Value";
                lblreview.ForeColor = System.Drawing.Color.Red;
                check_rating();
            }



            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#reviews';", true);
            ResetAll();

        }


        protected void checkavailability()
        {
            String villa = Request.QueryString["Parameter"].ToString();

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;


            cmd.CommandText = "select * from tblBooking where Client_id=@clientid and Villa_id=@villaid and Bk_checkin between @checkin and @checkout or Bk_checkout between @checkin and @checkout";
            cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
            cmd.Parameters.AddWithValue("@villaid", villa);
            cmd.Parameters.AddWithValue("@checkin", txtcheckin.Text);
            cmd.Parameters.AddWithValue("@checkout", txtcheckout.Text);


            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblbooking.Text = "There is already a booking for this villa for the dates " + txtcheckin.Text + " and " + txtcheckout.Text;
                    lblbooking.ForeColor = System.Drawing.Color.Red;
                }


                else
                {
                    lblbooking.Text = "This Villa is available for booking from " + txtcheckin.Text + " till " + txtcheckout.Text + ".";
                    lblbooking.ForeColor = System.Drawing.Color.Green;
                }
            }
        }

        void loadmap()
        {
            String villa = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(); ;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from tblVilla where Villa_id=@Villa_id";
            cmd.Parameters.AddWithValue("@Villa_id", villa);

            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    string q = dr["Villa_name"].ToString() + " " + dr["Villa_street"].ToString() + " " + dr["Villa_town"].ToString();
                    q = q.Replace(" ", "+");
                    gmap.Src = "https://www.google.com/maps/embed/v1/place?key=AIzaSyBVVltNubZbwIl6VXnKq8ocxyg1ulrVerA&q=" + q;

                }


            }

            //gmap.Src = "https://www.google.com/maps/embed/v1/place?key=AIzaSyBVVltNubZbwIl6VXnKq8ocxyg1ulrVerA&q=villa+wolmar+royal+road+flic";

        }

        protected void RatingAVG()
        {
            String villa = Request.QueryString["Parameter"].ToString();




            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select AVG(Rating)  from tblReviews where Villa_id=@villa";
            cmd.Parameters.AddWithValue("@villa", villa);

            dbcon1.Open();

            Rating = cmd.ExecuteScalar().ToString();
            dbcon1.Close();




        }



    }
}