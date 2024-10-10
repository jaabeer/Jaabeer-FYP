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
using System.Globalization;
using Aspose.Pdf;
using Aspose.Pdf.Operators;

namespace DealProjectTamam
{
    public partial class formbooking : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        String hotel_name, hotel_address, h_ref, bk_ckin, bk_ckout, bk_status, user_name, bk_cost, bk_date, hotel_id;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                Session["Page_name"] = "Booking";

                HistoryData();
                ReviewData();
                OngoingData();
                profileData();
            }
        }

        protected void profileData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;

            cmd.Parameters.AddWithValue("@Client_id", Convert.ToInt32(Session["client_id"].ToString()));
            cmd.CommandText = "SELECT * FROM tblClient WHERE Client_id=@Client_id";
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                Image1.ImageUrl = "~/Client/images/profile/" + dr["Client_id"].ToString() + "/" + dr["Client_profilepicture"].ToString();
                txtfname.Text = dr["Client_fname"].ToString();
                txtlname.Text = dr["Client_lname"].ToString();
                txtnic.Text = dr["Client_nic"].ToString();

                DateTime date_of_birth = Convert.ToDateTime(dr["Client_dob"]);
                txtdob.Text = date_of_birth.ToString("dd-MMMM-yyyy");

                txtadress.Text = dr["Client_address"].ToString();
                txtemail.Text = dr["Client_email"].ToString();
                txtmobile.Text = dr["Client_contact"].ToString();
            }
            con.Close();
        }

        protected void HistoryData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblBooking b, tblHotel h WHERE Bk_state='F' AND Show=1 AND Client_id=@Client_id AND b.Hotel_id=h.Hotel_id ORDER BY Bk_date DESC";
            cmd.Parameters.AddWithValue("@Client_id", Session["client_id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            Repeater1.DataSource = dt;
            Repeater1.DataBind();
            con.Close();
        }

        protected void ReviewData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblReviews R, tblHotel H WHERE R.Hotel_id = H.Hotel_id AND R.Client_id=@Client_id ORDER BY R.Review_Date DESC";
            cmd.Parameters.AddWithValue("@Client_id", Session["client_id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            Repeater2.DataSource = dt;
            Repeater2.DataBind();
            con.Close();
        }

        protected void OngoingData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblBooking b, tblHotel h, tblDistrict d WHERE d.Dist_id=h.Dist_id AND Bk_state !='F' AND Client_id=@Client_id AND b.Hotel_id=h.Hotel_id ORDER BY Bk_date DESC";
            cmd.Parameters.AddWithValue("@Client_id", Session["client_id"]);
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            Repeater3.DataSource = dt;
            Repeater3.DataBind();
            con.Close();
        }

        protected void btnRemove_Click(object sender, EventArgs e)
        {
            int bk_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            Boolean IsUpdated = false;
            SqlConnection con4 = new SqlConnection(_conString);
            SqlCommand cmd4 = new SqlCommand();
            cmd4.CommandType = CommandType.Text;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd4.CommandText = "update tblBooking set Show='0' where Bk_id=@booking";
            cmd4.Parameters.AddWithValue("@booking", bk_id);
            cmd4.Connection = con4;
            con4.Open();
            IsUpdated = cmd4.ExecuteNonQuery() > 0;
            con4.Close();

            HistoryData();


        }

        protected void btnpassword_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            //searching for a record containing matching username & password with 
            //an active status
            cmd.CommandText = "select * from tblClient where Client_email=@Client_email and Client_password=@pass";

            cmd.Parameters.AddWithValue("@Client_email", Session["client_email"]); ;
            cmd.Parameters.AddWithValue("@pass", Encrypt(old_password.Text.Trim()));


            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                cmd.CommandType = CommandType.Text;
                if (string.IsNullOrWhiteSpace(new_password.Text))
                {
                    lblmsg.Text = "Please enter a new password";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    return;
                }

                Boolean IsUpdated = false;
                SqlConnection conu = new SqlConnection(_conString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.CommandType = CommandType.Text;
                cmd.CommandText = "update tblClient set Client_password=@newpass where Client_email=@Client_em";
                cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]); ;
                cmd.Parameters.AddWithValue("@newpass", Encrypt(new_password.Text.Trim()));
                cmd.Connection = conu;
                conu.Open();
                IsUpdated = cmd.ExecuteNonQuery() > 0;
                conu.Close();
                if (IsUpdated)
                {
                    new_password.Text = "";
                    old_password.Text = "";
                    lblmsg.Text = " Password updated successfully!";
                    lblmsg.ForeColor = System.Drawing.Color.Green;


                }
                else
                {
                    new_password.Text = "";
                    old_password.Text = "";
                    lblmsg.Text = "Error while updating Password ";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

            }
            else
            {
                lblmsg.Text = "Wrong Credentials";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            con.Close();
        }

        public string Encrypt(string clearText)
        {
            // defining encrytion key
            string EncryptionKey = "MAKV2SPBNI99212";
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new
                byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    // encoding using key
                    using (CryptoStream cs = new CryptoStream(ms,
                    encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;

        }

        protected void btnNic_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";

            string sname_portion = (txtlname.Text.Trim().Substring(0, 1)).ToUpper();
            if ((new_nic.Text.Trim().Substring(0, 1)).ToUpper() != sname_portion)
            {
                lblmsg.Text = "First character of NIC should match first character of surname";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                return;
            }

            string digit_portion = new_nic.Text.Trim().Substring(1, 13);
            if (!(digit_portion.All(char.IsDigit)))
            {
                lblmsg.Text = "Wrong format for NIC, should be 1 letter + 13 digits";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                return;
            }

            string year_from_NIC;

            int year_portion = Convert.ToInt32(new_nic.Text.Trim().Substring(5, 2));
            if ((year_portion >= 0) && (year_portion <= 22))
            {
                year_from_NIC = "20" + new_nic.Text.Trim().Substring(5, 2);
            }
            else
            {
                year_from_NIC = "19" + new_nic.Text.Trim().Substring(5, 2);
            }
            string dob_from_NIC = new_nic.Text.Trim().Substring(1, 2) + "/" + new_nic.Text.Trim().Substring(3, 2) + "/" + year_from_NIC;
            string dob_from_NIC_2 = new_nic.Text.Trim().Substring(3, 2) + "/" + new_nic.Text.Trim().Substring(1, 2) + "/" + year_from_NIC;
            if (!IsValidDate(dob_from_NIC))
            {
                lblmsg.Text = "Wrong date format in NIC";
                lblmsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                return;
            }


            DateTime date_of_birth = Convert.ToDateTime(dob_from_NIC);

            int now = int.Parse(DateTime.Now.ToString("yyyyMMdd"));
            int dob = int.Parse(date_of_birth.ToString("yyyyMMdd"));
            int age = (now - dob) / 10000;



            if ((age < 18) || (age > 85))
            {
                lblmsg.Text = "age is out of range: " + age;
                lblmsg.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                return;
            }

            if (lblmsg.Text == "")
            {
                SqlConnection con1 = new SqlConnection(_conString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT * FROM tblClient where Client_nic=@client_nic and Client_id <> @Client_id";
                cmd1.Parameters.AddWithValue("@Client_nic", new_nic.Text.Trim());
                cmd1.Parameters.AddWithValue("@Client_id", Convert.ToInt32(Session["client_id"].ToString()));
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();

                using (da1)
                {
                    //Populate the DataTable
                    da1.Fill(dt1);
                }

                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        lblmsg.Text = "NIC number " + " " + dt1.Rows[0]["client_nic"].ToString() + " " + " already exists";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                        return;
                    }
                    else
                    {
                        SqlConnection con = new SqlConnection(_conString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update tblClient set Client_nic=@nic, Client_dob=@dob where Client_email=@Client_em";
                        cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]);
                        cmd.Parameters.AddWithValue("@nic", new_nic.Text.Trim());
                        cmd.Parameters.AddWithValue("@dob", dob_from_NIC_2);


                        cmd.Connection = con;
                        con.Open();
                        //use Command method to execute UPDATE statement and return 
                        //boolean if number of records UPDATED is greater than zero
                        Boolean IsUpdated;
                        IsUpdated = cmd.ExecuteNonQuery() > 0;
                        con.Close();
                        if (IsUpdated)
                        {
                            new_nic.Text = "";
                            //txt_dobnic.Text = "";
                            lblmsg.Text = "NIC updated successfully!, change reflected in DOB if date portion has been modified";
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            //Refresh the GridView by calling the BindMovieData()
                            profileData();

                        }
                        else
                        {
                            lblmsg.Text = "Error while updating NIC ";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                        }

                    }
                }

            }


        }


        public static bool IsValidDate(string value)
        {
            DateTime tempDate;
            bool validDate = DateTime.TryParseExact(value, "dd/MM/yyyy", DateTimeFormatInfo.InvariantInfo, DateTimeStyles.None, out tempDate);
            if (validDate)
                return true;
            else
                return false;
        }




        protected void btnfname_Click(object sender, EventArgs e)
        {
            Boolean IsUpdated = false;



            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblClient set Client_fname=@fname where Client_email=@Client_em";
            cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]);
            cmd.Parameters.AddWithValue("@fname", txt_newfname.Text.Trim());


            cmd.Connection = con;
            con.Open();
            //use Command method to execute UPDATE statement and return 
            //boolean if number of records UPDATED is greater than zero
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsUpdated)
            {
                txt_newfname.Text = "";
                lblmsg.Text = " First name updated successfully!";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                //Refresh the GridView by calling the BindMovieData()
                profileData();

            }
            else
            {
                lblmsg.Text = "Error while updating your first name ";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
        }

        protected void btnLname_Click(object sender, EventArgs e)
        {

            Boolean IsUpdated = false;



            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblClient set Client_lname=@lname where Client_email=@Client_em";
            cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]);
            cmd.Parameters.AddWithValue("@lname", txt_newlname.Text.Trim());


            cmd.Connection = con;
            con.Open();
            //use Command method to execute UPDATE statement and return 
            //boolean if number of records UPDATED is greater than zero
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsUpdated)
            {
                txt_newlname.Text = "";
                lblmsg.Text = " Last name updated successfully!";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                //Refresh the GridView by calling the BindMovieData()
                profileData();

            }
            else
            {
                lblmsg.Text = "Error while updating your last name ";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
        }

        protected void btndob_Click(object sender, EventArgs e)
        {
            Boolean IsUpdated = false;



            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblClient set Client_dob=@dob where Client_email=@Client_em";
            cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]);
            //cmd.Parameters.AddWithValue("@dob", txt_newdob.Text.Trim());


            cmd.Connection = con;
            con.Open();
            //use Command method to execute UPDATE statement and return 
            //boolean if number of records UPDATED is greater than zero
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsUpdated)
            {
                //txt_newdob.Text = "";
                lblmsg.Text = " Date of birth updated successfully!";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                //Refresh the GridView by calling the BindMovieData()
                profileData();

            }
            else
            {
                lblmsg.Text = "Error while updating your Date of birth ";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
        }

        protected void btn_address_Click(object sender, EventArgs e)
        {

            Boolean IsUpdated = false;



            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblClient set Client_address=@adress where Client_email=@Client_em";
            cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]);
            cmd.Parameters.AddWithValue("@adress", txt_newadd.Text.Trim());


            cmd.Connection = con;
            con.Open();
            //use Command method to execute UPDATE statement and return 
            //boolean if number of records UPDATED is greater than zero
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsUpdated)
            {
                //txt_newdob.Text = "";
                lblmsg.Text = "Address updated successfully!";
                lblmsg.ForeColor = System.Drawing.Color.Green;
                //Refresh the GridView by calling the BindMovieData()
                profileData();

            }
            else
            {
                lblmsg.Text = "Error while updating your Address ";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
        }

        protected void btnmobile_Click(object sender, EventArgs e)
        {
            lblmsg.Text = "";
            if (txt_newmobile.Text != "")
            {
                string first_char = txt_newmobile.Text.Trim().Substring(0, 1);
                string sec_char = txt_newmobile.Text.Trim().Substring(1, 1);
                if (first_char != "5")
                {
                    lblmsg.Text = "Mobile Phone Number should start with 5 (wrong format)";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                    return;
                }

                if (sec_char == "0")
                {
                    if (lblmsg.Text.Length != 0)
                    {
                        lblmsg.Text = lblmsg.Text + " and second number should not be 0";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                        return;
                    }
                    else
                    {
                        lblmsg.Text = "Second number of Mobile Phone number should not be 0 (wrong format)";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                        return;
                    }
                }
                SqlConnection con1 = new SqlConnection(_conString);
                SqlCommand cmd1 = new SqlCommand();
                cmd1.Connection = con1;
                cmd1.CommandType = CommandType.Text;
                cmd1.CommandText = "SELECT * FROM tblClient where Client_contact=@Client_contact and Client_id<>@Client_id";
                cmd1.Parameters.AddWithValue("@Client_contact", txt_newmobile.Text.Trim());
                cmd1.Parameters.AddWithValue("@Client_id", Convert.ToInt32(Session["client_id"].ToString()));
                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                DataTable dt1 = new DataTable();
                using (da1)
                {
                    //Populate the DataTable
                    da1.Fill(dt1);
                }

                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        lblmsg.Text = "Mobile number " + " " + dt1.Rows[0]["Client_contact"].ToString() + " " + " already exists";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        Boolean IsUpdated = false;

                        SqlConnection con = new SqlConnection(_conString);
                        SqlCommand cmd = new SqlCommand();
                        cmd.CommandType = CommandType.Text;
                        cmd.CommandText = "update tblClient set Client_contact=@mobile where Client_email=@Client_em";
                        cmd.Parameters.AddWithValue("@Client_em", Session["client_email"]);
                        cmd.Parameters.AddWithValue("@mobile", txt_newmobile.Text.Trim());

                        cmd.Connection = con;
                        con.Open();
                        //use Command method to execute UPDATE statement and return 
                        //boolean if number of records UPDATED is greater than zero
                        IsUpdated = cmd.ExecuteNonQuery() > 0;
                        con.Close();
                        if (IsUpdated)
                        {
                            //txt_newdob.Text = "";
                            lblmsg.Text = "Mobile Number updated successfully!";
                            lblmsg.ForeColor = System.Drawing.Color.Green;
                            //Refresh the GridView by calling the BindMovieData()
                            profileData();

                        }
                        else
                        {
                            lblmsg.Text = "Error while updating your Mobile number ";
                            lblmsg.ForeColor = System.Drawing.Color.Red;
                        }
                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                    }
                }
                con1.Close();
            }

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);

        }



        protected void btn_email_Click(object sender, EventArgs e)
        {

            SqlConnection con1 = new SqlConnection(_conString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT * FROM tblClient where Client_email=@Client_email and Client_id<>@Client_id";
            cmd1.Parameters.AddWithValue("@Client_email", new_email.Text.Trim());
            cmd1.Parameters.AddWithValue("@Client_id", Convert.ToInt32(Session["client_id"].ToString()));
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            using (da1)
            {
                //Populate the DataTable
                da1.Fill(dt1);
            }

            if (dt1 != null)
            {
                if (dt1.Rows.Count > 0)
                {
                    lblmsg.Text = "Email " + " " + dt1.Rows[0]["Client_email"].ToString() + " " + " already exists";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                    return;
                }
                else
                {
                    Boolean IsUpdated = false;
                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "update tblClient set Client_email=@Client_email where Client_id=@Client_id";
                    cmd.Parameters.AddWithValue("@Client_email", new_email.Text.Trim());
                    cmd.Parameters.AddWithValue("@Client_id", Convert.ToInt32(Session["client_id"].ToString()));


                    cmd.Connection = con;
                    con.Open();
                    //use Command method to execute UPDATE statement and return 
                    //boolean if number of records UPDATED is greater than zero
                    IsUpdated = cmd.ExecuteNonQuery() > 0;
                    con.Close();
                    if (IsUpdated)
                    {
                        profileData();
                        //txt_newdob.Text = "";
                        lblmsg.Text = "Email Address updated successfully!";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindMovieData()
                        profileData();
                        Session["client_email"] = new_email.Text.Trim();
                        lblmsg.Text = "Email Address updated successfully! and new session email is: " + Session["client_email"].ToString();
                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                    }
                    else
                    {
                        lblmsg.Text = "Error while updating your Email Address";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                }
            }


            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
        }

        protected void btn_pp_Click(object sender, EventArgs e)
        {
            if (btn_upload_pp.FileName != "")
            {
                if (CheckFileType(btn_upload_pp.FileName))
                {
                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *  from tblClient where Client_email=@Client_email";
                    cmd.Parameters.AddWithValue("@Client_email", Session["client_email"]);
                    cmd.Connection = con;
                    SqlDataReader dr;
                    con.Open();
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {
                        dr.Read();
                        string prev_filename = "~/Client/images/profile/" + Session["client_id"].ToString() + "/" + dr["Client_profilepicture"].ToString();
                        string prev_path = Server.MapPath(prev_filename);
                        //FileInfo file = new FileInfo(path);
                        if (File.Exists(prev_path))//check file exsit or not  
                        {
                            File.Delete(prev_path);
                            //Populate_dlstImages();

                        }
                    }


                    SqlConnection con1 = new SqlConnection(_conString);
                    SqlCommand cmd1 = new SqlCommand();
                    cmd1.CommandType = CommandType.Text;
                    cmd1.CommandText = "UPDATE tblClient set Client_profilepicture=@Client_profilepicture where Client_email=@Client_email";
                    cmd1.Parameters.AddWithValue("@Client_profilepicture", btn_upload_pp.FileName);
                    cmd1.Parameters.AddWithValue("@Client_email", Session["client_email"]);
                    cmd1.Connection = con1;
                    con1.Open();

                    //dr.Read();
                    //string filename = dr["Prop_image"].ToString();
                    string filename = "~/Client/images/profile/" + Session["client_id"].ToString() + "/" + btn_upload_pp.FileName;


                    Boolean IsAdded = false;
                    IsAdded = cmd1.ExecuteNonQuery() > 0;
                    if (IsAdded)
                    {

                        string path = Server.MapPath(filename);
                        //FileInfo file = new FileInfo(path);
                        if (File.Exists(path))//check file exsit or not  
                        {
                            File.Delete(path);
                            //Populate_dlstImages();

                        }


                        string folderPath = Server.MapPath("~/Client/images/profile/" + Session["client_id"].ToString());
                        //Check whether Directory (Folder) exists.
                        if (!Directory.Exists(folderPath))
                        {
                            //If Directory (Folder) does not exists. Create it.
                            Directory.CreateDirectory(folderPath);
                        }


                        String filePath = "~/Client/images/profile/" + Session["client_id"].ToString() + "/" + btn_upload_pp.FileName;
                        btn_upload_pp.SaveAs(MapPath(filePath));
                        lblmsg.Text = "Profile picture updated";
                        lblmsg.ForeColor = System.Drawing.Color.Green;
                        profileData();

                    }
                    else
                    {
                        lblmsg.Text = "An error has occured, please try again";
                        lblmsg.ForeColor = System.Drawing.Color.Red;
                    }
                    con1.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);
                }
                else
                {
                    lblmsg.Text = "Wrong format, please upload a picture";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }
            }
            else
            {
                lblmsg.Text = "Please upload a picture";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#MyProfile';", true);

        }
        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                    return true;
                case ".png":
                    return true;
                case ".jpg":
                    return true;
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        protected void btn_PDF_Click(object sender, EventArgs e)
        {
            int booking = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tblBooking b, tblHotel h, tblDistrict d where d.Dist_id=h.Dist_id and  Bk_state !='F' and Client_id=@Client_id and b.Hotel_id=h.Hotel_id   and b.Bk_id=" + booking;
            cmd.Parameters.AddWithValue("@Client_id", Session["client_id"]);

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                String bk_state;


                hotel_name = dr["Hotel_name"].ToString();
                hotel_address = dr["Hotel_street"].ToString() + ", " + dr["Hotel_town"].ToString() + ", " + dr["Dist_name"].ToString();
                h_ref = dr["Bk_ref"].ToString();
                DateTime cin = Convert.ToDateTime(dr["Bk_checkin"]);
                bk_ckin = cin.ToString("dd-MMMM-yyyy");
                DateTime cout = Convert.ToDateTime(dr["Bk_checkout"]);
                bk_ckout = cout.ToString("dd-MMMM-yyyy");
                bk_state = dr["Bk_state"].ToString();
                user_name = txtlname.Text.Trim() + " " + txtfname.Text.Trim();
                bk_cost = dr["Bk_amnt"].ToString();
                bk_date = dr["Bk_date"].ToString();
                hotel_id = dr["Hotel_id"].ToString();


                if (bk_state == "B")
                {
                    bk_status = "Awaiting for Payment";
                }
                else if (bk_state == "P")
                {
                    bk_status = "Payment Pending ";
                }
                else
                {
                    bk_status = "Confirmed";
                }

            }
            if (h_ref != "")
            {
                generate_pdf();
            }
            else
            {
                lblonmsg.Text = "Error!!";
            }


        }

        void generate_pdf()
        {
            // Initialize document object
            Document document = new Document();

            // Add page
            Aspose.Pdf.Page page = document.Pages.Add();


            //// Add image                    
            //Aspose.Pdf.Image image = new Aspose.Pdf.Image();
            //image.File = (MapPath("~/images//.jpg"));
            //page.Paragraphs.Add(image);


            // Add text to new page          
            var title = new Aspose.Pdf.Text.TextFragment("Deal Tamam Hotel BOOKING");
            title.TextState.FontSize = 20;
            title.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            title.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            title.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Lavender);
            title.TextState.Underline = true;
            title.Margin.Bottom = 10;
            page.Paragraphs.Add(title);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var venue = new Aspose.Pdf.Text.TextFragment("Venue: " + hotel_name);
            venue.TextState.FontSize = 14;
            venue.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Italic;
            venue.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Teal);
            venue.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Honeydew);
            venue.Margin.Bottom = 8;
            page.Paragraphs.Add(venue);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var location = new Aspose.Pdf.Text.TextFragment("Location: " + hotel_address);
            location.TextState.FontSize = 14;
            location.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.DarkOliveGreen);
            location.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Honeydew);
            location.Margin.Bottom = 8;
            page.Paragraphs.Add(location);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var bookingReference = new Aspose.Pdf.Text.TextFragment("Booking Reference: " + h_ref);
            bookingReference.TextState.FontSize = 14;
            bookingReference.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Crimson);
            bookingReference.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.MistyRose);
            bookingReference.Margin.Bottom = 8;
            page.Paragraphs.Add(bookingReference);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var firstName = new Aspose.Pdf.Text.TextFragment("First Name: " + txtfname.Text.ToString());
            firstName.TextState.FontSize = 12;
            firstName.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            firstName.Margin.Bottom = 5;
            page.Paragraphs.Add(firstName);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var lastName = new Aspose.Pdf.Text.TextFragment("Last Name: " + txtlname.Text.ToString());
            lastName.TextState.FontSize = 12;
            lastName.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            lastName.Margin.Bottom = 5;
            page.Paragraphs.Add(lastName);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var nic = new Aspose.Pdf.Text.TextFragment("NIC: " + txtnic.Text.ToString());
            nic.TextState.FontSize = 12;
            nic.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            nic.Margin.Bottom = 5;
            page.Paragraphs.Add(nic);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var clientAddress = new Aspose.Pdf.Text.TextFragment("Client Address: " + txtadress.Text.ToString());
            clientAddress.TextState.FontSize = 12;
            clientAddress.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            clientAddress.Margin.Bottom = 5;
            page.Paragraphs.Add(clientAddress);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var clientEmail = new Aspose.Pdf.Text.TextFragment("Client Email: " + txtemail.Text.ToString());
            clientEmail.TextState.FontSize = 12;
            clientEmail.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            clientEmail.Margin.Bottom = 5;
            page.Paragraphs.Add(clientEmail);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var bookingDate = new Aspose.Pdf.Text.TextFragment("Booking Date: " + bk_date);
            bookingDate.TextState.FontSize = 12;
            bookingDate.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            bookingDate.Margin.Bottom = 5;
            page.Paragraphs.Add(bookingDate);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var checkInDate = new Aspose.Pdf.Text.TextFragment("Check-in Date: " + bk_ckin);
            checkInDate.TextState.FontSize = 12;
            checkInDate.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            checkInDate.Margin.Bottom = 5;
            page.Paragraphs.Add(checkInDate);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var checkOutDate = new Aspose.Pdf.Text.TextFragment("Check-out Date: " + bk_ckout);
            checkOutDate.TextState.FontSize = 12;
            checkOutDate.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            checkOutDate.Margin.Bottom = 5;
            page.Paragraphs.Add(checkOutDate);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var bookingStatus = new Aspose.Pdf.Text.TextFragment("Booking status: " + bk_status);
            bookingStatus.TextState.FontSize = 12;
            bookingStatus.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            bookingStatus.Margin.Bottom = 5;
            page.Paragraphs.Add(bookingStatus);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var separator = new Aspose.Pdf.Text.TextFragment("");
            separator.TextState.FontSize = 14;
            separator.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            separator.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray);
            separator.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            separator.Margin.Bottom = 10;
            page.Paragraphs.Add(separator);

            var total = new Aspose.Pdf.Text.TextFragment("Total: " + bk_cost);
            total.TextState.FontSize = 16;
            total.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            total.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.DarkGreen);
            total.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightYellow);
            total.Margin.Bottom = 10;
            page.Paragraphs.Add(total);

            var separatorEnd = new Aspose.Pdf.Text.TextFragment("");
            separatorEnd.TextState.FontSize = 14;
            separatorEnd.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            separatorEnd.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray);
            separatorEnd.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            page.Paragraphs.Add(separatorEnd);


            string folderPath = Server.MapPath("~/Property/" + hotel_id + "/Booking");
            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            String filename = "~/Property/" + hotel_id + "/Booking/" + h_ref + ".pdf";

            lblmsg.Text = "";

            if (!IsFileLocked(MapPath(filename)))
            {
                // Save PDF 
                document.Save(MapPath(filename));
                //Open PDF
                System.Diagnostics.Process.Start(MapPath(filename));
            }
            else
            {
                lblmsg.Text = "Document is already opened, please close it first";

            }



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



    }
}