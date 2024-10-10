using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class Hotel_details : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        private string Rating;
        private string booking_id;
        private string booking_checkout;
        private string review_id;
        private string RefNO;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Page_name"] = "Villa_details";
                btnreview_update.Attributes["style"] = "display:none";
                PropertyData();
                LoadHotelImages();
                FacilitiesData();
                checkwifi();
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

        protected void LoadHotelImages()
        {
            string hotelId = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(hotelId))
            {
                lblreview.Text = "Invalid hotel parameter.";
                lblreview.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT Hotel_id, Img_name FROM tblImage WHERE Hotel_id = @Hotel_id";
                cmd.Parameters.AddWithValue("@Hotel_id", hotelId);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                catch (Exception ex)
                {
                    lblreview.Text = "An error occurred: " + ex.Message;
                    lblreview.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void PropertyData()
        {
            string villa = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(villa))
            {
                lblreview.Text = "Invalid hotel parameter.";
                lblreview.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tblHotel Where Hotel_id=@Hotel";
                cmd.Parameters.AddWithValue("@Hotel", villa);

                try
                {
                    con.Open();
                    using (SqlDataReader dr = cmd.ExecuteReader())
                    {
                        while (dr.Read())
                        {
                            villa_img.ImageUrl = dr["Hotel_image"].ToString();
                            lbl_day.Text = dr["Hotel_price"].ToString();
                            lbl_desc.Text = dr["Hotel_desc"].ToString();
                            lblpropname.Text = dr["Hotel_name"].ToString().Trim();
                            lblrating.Text = dr["Hotel_Rating"].ToString();
                        }
                    }
                }
                catch (Exception ex)
                {
                    lblreview.Text = "An error occurred: " + ex.Message;
                    lblreview.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void FacilitiesData()
        {
            string villa = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(villa))
            {
                lblreview.Text = "Invalid hotel parameter.";
                lblreview.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT F.Rooms_Fac_name FROM tbl_Rooms_Facilities F, tblRoomFacilities_Hotel FH WHERE F.Rooms_Fac_id = FH.Rooms_Fac_id AND Hotel_id=@Hotel";
                cmd.Parameters.AddWithValue("@Hotel", villa);
                SqlDataAdapter da = new SqlDataAdapter(cmd);

                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);
                    Repeater2.DataSource = dt;
                    Repeater2.DataBind();
                }
                catch (Exception ex)
                {
                    lblreview.Text = "An error occurred: " + ex.Message;
                    lblreview.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void checkwifi()
        {
            string villa = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(villa))
            {
                lblwifi.Text = "Invalid hotel parameter.";
                lblwifi.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tblRoomFacilities_Hotel FH WHERE Hotel_id=@Hotel AND Rooms_Fac_id=70";
                cmd.Parameters.AddWithValue("@Hotel", villa);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        lblwifi.Text = "Free! WiFi is available in all areas and is free of charge";
                    }
                    else
                    {
                        lblwifi.Text = "No Wifi Available";
                    }
                }
                catch (Exception ex)
                {
                    lblwifi.Text = "An error occurred: " + ex.Message;
                    lblwifi.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void checkparking()
        {
            string villa = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(villa))
            {
                lblparking.Text = "Invalid hotel parameter.";
                lblparking.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tblroomFacilities_Hotel FH WHERE Hotel_id=@Hotel AND Rooms_Fac_id=71";
                cmd.Parameters.AddWithValue("@Hotel", villa);

                try
                {
                    con.Open();
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        lblparking.Text = "Free Parking is available on this property";
                    }
                    else
                    {
                        lblparking.Text = "No Parking Available";
                    }
                }
                catch (Exception ex)
                {
                    lblparking.Text = "An error occurred: " + ex.Message;
                    lblparking.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void btnsave_Click(object sender, EventArgs e)
        {
            if (txtcheckin.Text != "" && txtcheckout.Text != "")
            {
                DateTime checkin = Convert.ToDateTime(txtcheckin.Text);
                DateTime checkout = Convert.ToDateTime(txtcheckout.Text);

                if (checkin > DateTime.Now && checkout > DateTime.Now)
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
                    lblbooking.Text = "Checkin or Checkout date should be after " + DateTime.Now;
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
            string villa = Request.QueryString["Parameter"].ToString();
            if (txtcheckin.Text != "" && txtcheckout.Text != "")
            {
                DateTime checkin = DateTime.Parse(txtcheckin.Text);
                DateTime checkout = DateTime.Parse(txtcheckout.Text);
                double days = (checkout.Date - checkin.Date).TotalDays;

                if (days <= 7)
                {
                    using (SqlConnection con = new SqlConnection(_conString))
                    {
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;

                        cmd.CommandText = "SELECT * FROM tblBooking WHERE Client_id=@clientid AND Hotel_id=@Hotelid AND (Bk_checkin BETWEEN @checkin AND @checkout OR Bk_checkout BETWEEN @checkin AND @checkout)";
                        cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
                        cmd.Parameters.AddWithValue("@Hotelid", villa);
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
                                int cost = ((int)days) * price;

                                using (SqlConnection con2 = new SqlConnection(_conString))
                                {
                                    SqlCommand cmd2 = new SqlCommand();
                                    cmd2.CommandType = CommandType.Text;
                                    cmd2.Connection = con2;

                                    cmd2.CommandText = "INSERT INTO tblBooking (Bk_date, Bk_checkin, Bk_checkout, Bk_type, Bk_amnt, Bk_state, Hotel_id, Client_id, Show) VALUES (@date, @checkin, @checkout, @type, @cost, @state, @Hotelid, @clientid, @show)";
                                    cmd2.Parameters.AddWithValue("@date", DateTime.Now);
                                    cmd2.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                                    cmd2.Parameters.AddWithValue("@checkout", txtcheckout.Text);
                                    cmd2.Parameters.AddWithValue("@type", "Daily");
                                    cmd2.Parameters.AddWithValue("@cost", cost);
                                    cmd2.Parameters.AddWithValue("@state", "P");
                                    cmd2.Parameters.AddWithValue("@Hotelid", villa);
                                    cmd2.Parameters.AddWithValue("@show", 1);
                                    cmd2.Parameters.AddWithValue("@clientid", Session["client_id"]);

                                    con2.Open();
                                    bool IsAdded = cmd2.ExecuteNonQuery() > 0;
                                    con2.Close();

                                    if (IsAdded)
                                    {
                                        Check_booking_id();
                                        Referencenumber();
                                        updateRefnum();
                                        Response.Redirect("~/BookingDetails?Parameter=" + booking_id);
                                    }
                                    else
                                    {
                                        lblbooking.Text = "Error while making the booking";
                                        lblbooking.ForeColor = System.Drawing.Color.Red;
                                    }
                                }
                            }
                        }
                    }
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
            string villa = Request.QueryString["Parameter"].ToString();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tblBooking WHERE Client_id=@clientid AND Hotel_id=@Hotelid AND Bk_checkin=@checkin AND BK_checkout=@checkout";
                cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
                cmd.Parameters.AddWithValue("@Hotelid", villa);
                cmd.Parameters.AddWithValue("@checkin", txtcheckin.Text);
                cmd.Parameters.AddWithValue("@checkout", txtcheckout.Text);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    while (dr.Read())
                    {
                        booking_id = dr["Bk_id"].ToString();
                        booking_checkout = dr["Bk_checkout"].ToString();
                    }
                }
            }
        }

        protected void Referencenumber()
        {
            RefNO = "BK" + DateTime.Now.ToString("MMddyyyy") + booking_id;
        }

        protected void updateRefnum()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = "UPDATE tblBooking SET Bk_ref=@ref WHERE Bk_id=@booking";
                cmd.Parameters.AddWithValue("@booking", booking_id);
                cmd.Parameters.AddWithValue("@ref", RefNO);

                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        protected void check_rating()
        {
            string villa = Request.QueryString["Parameter"];

            if (string.IsNullOrEmpty(villa))
            {
                lblreview.Text = "Invalid hotel parameter.";
                lblreview.ForeColor = System.Drawing.Color.Red;
                return;
            }

            if (Session["client_id"] == null)
            {
                lblreview.Text = "User session expired. Please log in again.";
                lblreview.ForeColor = System.Drawing.Color.Red;
                return;
            }

            string clientId = Session["client_id"].ToString();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblBooking WHERE Client_id=@clientid AND Hotel_id=@Hotelid", con);
                cmd.Parameters.AddWithValue("@clientid", clientId);
                cmd.Parameters.AddWithValue("@Hotelid", villa);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                try
                {
                    con.Open();
                    da.Fill(dt);

                    if (dt.Rows.Count > 0)
                    {
                        DataRow row = dt.Rows[0];
                        booking_id = row["Bk_id"].ToString();
                        booking_checkout = row["Bk_checkout"].ToString();
                        string booking_checkin = row["Bk_checkin"].ToString();

                        DateTime checkout = DateTime.Parse(booking_checkout);
                        DateTime checkin = DateTime.Parse(booking_checkin);
                        DateTime limit = checkout.AddDays(10);
                        DateTime today = DateTime.Today;

                        if (limit > today)
                        {
                            using (SqlConnection con2 = new SqlConnection(_conString))
                            {
                                SqlCommand cmd2 = new SqlCommand("SELECT * FROM tblReviews WHERE Client_id=@clientid AND Hotel_id=@Hotelid", con2);
                                cmd2.Parameters.AddWithValue("@clientid", clientId);
                                cmd2.Parameters.AddWithValue("@Hotelid", villa);

                                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                                DataTable dt2 = new DataTable();

                                con2.Open();
                                da2.Fill(dt2);

                                if (dt2.Rows.Count > 0)
                                {
                                    DataRow row2 = dt2.Rows[0];
                                    txtpos.Text = row2["Positive_Review"].ToString();
                                    txtneg.Text = row2["Negative_Review"].ToString();
                                    review_id = row2["Review_id"].ToString();

                                    btnreview.Attributes["style"] = "display:none";
                                    btnreview_update.Attributes["style"] = "display:block";
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
                            lblreview.ForeColor = System.Drawing.Color.Red;
                            btnrev.Attributes["style"] = "display:none";
                        }
                    }
                    else
                    {
                        lblreview.Text = "You cannot post review for this property as you never made a booking here.";
                        lblreview.ForeColor = System.Drawing.Color.Red;
                        btnrev.Attributes["style"] = "display:none";
                    }
                }
                catch (Exception ex)
                {
                    lblreview.Text = "An error occurred: " + ex.Message;
                    lblreview.ForeColor = System.Drawing.Color.Red;
                }
            }
        }

        protected void ReviewData()
        {
            if (!String.IsNullOrEmpty(Request.QueryString["Parameter"]))
            {
                string villa = Request.QueryString["Parameter"];
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT * FROM tblReviews R, tblClient C WHERE R.Client_id = C.Client_id AND R.Hotel_id=@Hotel ORDER BY R.Review_Date DESC";
                    cmd.Parameters.AddWithValue("@Hotel", villa);
                    SqlDataAdapter da = new SqlDataAdapter(cmd);

                    DataTable dt = new DataTable();

                    using (da)
                    {
                        da.Fill(dt);
                    }

                    Repeater4.DataSource = dt;
                    Repeater4.DataBind();
                }
            }
            else
            {
                Response.Write("Error: Query string parameter 'Parameter' is missing or empty.");
            }
        }

        protected void btnreview_Click(object sender, EventArgs e)
        {
            if (ddlrating.SelectedValue != "-1")
            {
                string villa = Request.QueryString["Parameter"];
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.CommandText = "INSERT INTO tblReviews (Hotel_id, Client_id, Positive_Review, Negative_Review, Rating, Review_Date) VALUES (@Hotel_id, @client_id, @pos, @neg, @rating, @today)";
                    cmd.Parameters.AddWithValue("@Hotel_id", villa);
                    cmd.Parameters.AddWithValue("@client_id", Session["client_id"]);
                    cmd.Parameters.AddWithValue("@pos", txtpos.Text.Trim());
                    cmd.Parameters.AddWithValue("@neg", txtneg.Text.Trim());
                    cmd.Parameters.AddWithValue("@rating", ddlrating.SelectedValue);
                    cmd.Parameters.AddWithValue("@today", DateTime.Today);

                    con.Open();
                    bool IsAdded = cmd.ExecuteNonQuery() > 0;
                    con.Close();

                    if (IsAdded)
                    {
                        RatingAVG();
                        using (SqlConnection con2 = new SqlConnection(_conString))
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Connection = con2;

                            cmd2.CommandText = "UPDATE tblHotel SET Hotel_Rating=@rating WHERE Hotel_id=@Hotel";
                            cmd2.Parameters.AddWithValue("@rating", Rating);
                            cmd2.Parameters.AddWithValue("@Hotel", villa);

                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

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

        protected void btnreview_update_Click(object sender, EventArgs e)
        {
            if (ddlrating.SelectedValue != "-1")
            {
                string villa = Request.QueryString["Parameter"];
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.Connection = con;

                    cmd.CommandText = "UPDATE tblReviews SET Positive_Review=@pos, Negative_Review=@neg, Rating=@rating, Review_Date=@today WHERE Review_id=@rev_id";
                    cmd.Parameters.AddWithValue("@rev_id", review_id);
                    cmd.Parameters.AddWithValue("@Hotel_id", villa);
                    cmd.Parameters.AddWithValue("@client_id", Session["client_id"]);
                    cmd.Parameters.AddWithValue("@pos", txtpos.Text.Trim());
                    cmd.Parameters.AddWithValue("@neg", txtneg.Text.Trim());
                    cmd.Parameters.AddWithValue("@rating", ddlrating.SelectedValue);
                    cmd.Parameters.AddWithValue("@today", DateTime.Today);

                    con.Open();
                    bool IsUpdated = cmd.ExecuteNonQuery() > 0;
                    con.Close();

                    if (IsUpdated)
                    {
                        RatingAVG();
                        using (SqlConnection con2 = new SqlConnection(_conString))
                        {
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.Connection = con2;

                            cmd2.CommandText = "UPDATE tblHotel SET Hotel_Rating=@rating WHERE Hotel_id=@Hotel_id";
                            cmd2.Parameters.AddWithValue("@rating", Rating);
                            cmd2.Parameters.AddWithValue("@Hotel_id", villa);

                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

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
            }
            else
            {
                lblreview.Text = "Please choose a Rating Value";
                lblreview.ForeColor = System.Drawing.Color.Red;
                check_rating();
            }

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#reviews';", true);
            ResetAll();
        }

        protected void checkavailability()
        {
            string villa = Request.QueryString["Parameter"].ToString();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;

                cmd.CommandText = "SELECT * FROM tblBooking WHERE Client_id=@clientid AND Hotel_id=@Hotelid AND (Bk_checkin BETWEEN @checkin AND @checkout OR Bk_checkout BETWEEN @checkin AND @checkout)";
                cmd.Parameters.AddWithValue("@clientid", Session["client_id"]);
                cmd.Parameters.AddWithValue("@Hotelid", villa);
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
        }

        protected void loadmap()
        {
            string villa = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(villa))
            {
                lblreview.Text = "Invalid hotel parameter.";
                lblreview.ForeColor = System.Drawing.Color.Red;
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tblHotel WHERE Hotel_id=@Hotel_id";
                cmd.Parameters.AddWithValue("@Hotel_id", villa);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.HasRows)
                    {
                        while (dr.Read())
                        {
                            string q = dr["Hotel_name"].ToString() + " " + dr["Hotel_street"].ToString() + " " + dr["Hotel_town"].ToString();
                            q = q.Replace(" ", "+");
                            gmap.Src = "https://www.google.com/maps/embed/v1/place?key=AIzaSyBVVltNubZbwIl6VXnKq8ocxyg1ulrVerA&q=" + q;
                        }
                    }
                }
            }
        }

        protected void RatingAVG()
        {
            string villa = Request.QueryString["Parameter"];

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT AVG(Rating) FROM tblReviews WHERE Hotel_id=@Hotel";
                cmd.Parameters.AddWithValue("@Hotel", villa);

                con.Open();
                Rating = cmd.ExecuteScalar().ToString();
                con.Close();
            }
        }
    }
}
