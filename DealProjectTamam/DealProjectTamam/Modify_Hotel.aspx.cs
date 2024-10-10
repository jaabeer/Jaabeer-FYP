using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class Modify_Hotel : System.Web.UI.Page
    {
        String Prop_name_old;
        String Prop_phone_old;
        String Prop_email_old;
        String Prop_street_old;
        String Prop_town_old;
        String Prop_priceday_old;
        String Prop_priceweek_old;
        String Prop_pricemonth_old;
        String Prop_postcode_old;
        String ddlDistrict_old;
        string modif_detail;

        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {


            if (!Page.IsPostBack)
            {
                get_districts();
                Populate_owner_loc_price();
                Populate_dlstImages();
                set_own_info_readonly();
                set_owner_loc_price_readonly();
                populate_details_dropdown();
                getDetails();
                populate_facilities_dropdown();
                getFacilities();
                set_owner_loc_price_readonly();
                initialise_button_status();

            }

            initialise_button_status();
        }

        void get_districts()
        {

            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDistrict";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Dist_name"] + " " + "(" + (String)dr["Dist_region"] + ")";
                newItem.Value = dr["Dist_id"].ToString();
                ddlDistrict.Items.Add(newItem);
            }

        }

        void Populate_owner_loc_price()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHotel th inner join tblDistrict td on td.Dist_id = th.Dist_id inner join tblOwner tow on tow.Own_id = th.Own_id where th.Hotel_id = " + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
              
                txtOwn_fname.Text = dr["Own_fname"].ToString();
                txtOwn_lname.Text = dr["Own_lname"].ToString();
                txtProp_name.Text = dr["Hotel_name"].ToString();
                txtProp_phone.Text = dr["Hotel_phone"].ToString();
                txtProp_email.Text = dr["Hotel_email"].ToString();
                txtProp_street.Text = dr["Hotel_street"].ToString();
                txtProp_town.Text = dr["Hotel_town"].ToString();
                txtProp_priceday.Text = dr["Hotel_price"].ToString();
                txtProp_postcode.Text = dr["Hotel_postcode"].ToString();
                txt_Description.Text = dr["Hotel_desc"].ToString();
                ddlDistrict.Items[0].Text = (dr["Dist_name"].ToString() + " " + "(" + dr["Dist_region"].ToString() + ")");
                ddlDistrict.Items[0].Value = dr["Dist_id"].ToString();

           
                img_main.ImageUrl = "~/Property/" + qs + "/main/" + dr["Hotel_image"].ToString();
            }
        }

        void set_own_info_readonly()
        {
            txtOwn_fname.ReadOnly = true;
            txtOwn_lname.ReadOnly = true;
            txtProp_email.ReadOnly = true;
        }

        void set_owner_loc_price_readonly()
        {
            txtProp_name.ReadOnly = true;
            txtProp_town.ReadOnly = true;
            txtProp_priceday.ReadOnly = true;
            txtProp_phone.ReadOnly = true;
            txtProp_town.ReadOnly = true;
            txtProp_street.ReadOnly = true;
            txtProp_postcode.ReadOnly = true;
            ddlDistrict.Enabled = false;

            ddlDistrict.CssClass = "form-control form-control-user";

            txt_Description.ReadOnly = true;
        }

        void populate_details_dropdown()
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDetails";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Det_name"];
                newItem.Value = dr["Det_id"].ToString();
                ddlDet_name.Items.Add(newItem);
            }
        }
        void populate_facilities_dropdown()
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFacilities";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Fac_name"];
                newItem.Value = dr["Fac_id"].ToString();
                ddl_Facility.Items.Add(newItem);
            }
        }
        void initialise_button_status()
        {

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con3 = new SqlConnection(_conString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Hotel_id=@Hotel_id";
            cmd3.Parameters.AddWithValue("@Hotel_id", qs);
            cmd3.Connection = con3;
            SqlDataReader dr3;
            con3.Open();
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                Session["count_slide_show"] = dr3["num_image_slide_show"];
            }

            //Owner, location and price
            btnCancel_own_loc_price.Visible = false;
            btnSave_own_loc_price.Visible = false;
            btnEdit_own_loc_price.Visible = true;

            //main picture
            btnupload_main_pic.Visible = false;
            btnEdit_main_pic.Visible = true;
            btnSave_main_pic.Visible = false;
            btnCancel_main_pic.Visible = false;

            //slideshow
            btnupload_slideshow.Visible = false;
            btnEdit_slideshow.Visible = true;
            btnSave_slideshow.Visible = false;
            btnCancel_slideshow.Visible = false;
            foreach (DataListItem di in dlstImages.Items)
            {
                di.FindControl("lbtndelete_slideshow").Visible = false;
            }


            //Additional details (accessibility)
            Btn_Edit_additional_info.Visible = true;
            Btn_Cancel_additional_info.Visible = false;
            gvs_2.Columns[0].Visible = false;

            //Facilities
            Btn_Edit_facilities.Visible = true;
            Btn_Cancel_facilities.Visible = false;
            gvs_3.Columns[0].Visible = false;

            //Description
            Btn_Save_description.Visible = false;
            Btn_Edit_description.Visible = true;
            Btn_Cancel_description.Visible = false;
        }

        void Populate_dlstImages()
        {
          
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblImage where Hotel_id = " + qs;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
            }
            con.Open();
            dlstImages.DataSource = dt;
            dlstImages.DataBind();


            SqlConnection con3 = new SqlConnection(_conString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Hotel_id=@Hotel_id";
            cmd3.Parameters.AddWithValue("@Hotel_id", qs);
            cmd3.Connection = con3;
            SqlDataReader dr3;
            con3.Open();
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                Session["count_slide_show"] = dr3["num_image_slide_show"];
            }
        }

        protected void btnSave_own_loc_price_Click(object sender, EventArgs e)
        {
            clear_labels();
            retrieve_and_store_old_values();

            if ((txtProp_name.Text == Prop_name_old)
                && (txtProp_phone.Text == Prop_phone_old)
                && (txtProp_email.Text == Prop_email_old)
                && (txtProp_street.Text == Prop_street_old)
                && (txtProp_town.Text == Prop_town_old)
                && (txtProp_priceday.Text == Prop_priceday_old)
                && (txtProp_postcode.Text == Prop_postcode_old)
                && (ddlDistrict.SelectedValue.ToString() == ddlDistrict_old))

            {
                lblResult1.Text = "No change detected";

           
                Populate_owner_loc_price();
                set_owner_loc_price_readonly();
                initialise_button_status();

            }
            else
            {
                SqlConnection con1 = new SqlConnection(_conString);
                SqlConnection con2 = new SqlConnection(_conString);
                SqlConnection con4 = new SqlConnection(_conString);

                SqlCommand cmd1 = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                SqlCommand cmd4 = new SqlCommand();

                cmd1.Connection = con1;
                cmd2.Connection = con2;
                cmd4.Connection = con4;

                cmd1.CommandType = CommandType.Text;
                cmd2.CommandType = CommandType.Text;
                cmd4.CommandType = CommandType.Text;

                int qs = Convert.ToInt32(Request.QueryString["ID"]);

                cmd1.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
                cmd1.Parameters.AddWithValue("@Hotel_id", qs);
                cmd2.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
                cmd2.Parameters.AddWithValue("@Hotel_id", qs);

                cmd1.CommandText = "SELECT * FROM tblHotel where Hotel_name=@Hotel_name and Hotel_id <> @Hotel_id";
                cmd2.CommandText = "SELECT * FROM tblHotel where Hotel_phone=@Hotel_phone and Hotel_id <> @Hotel_id";

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();

                using (da1)
                {
                    //Populate the DataTable
                    da1.Fill(dt1);
                }

                using (da2)
                {
                    //Populate the DataTable
                    da2.Fill(dt2);
                }


                clear_labels();

                if (dt1 != null)
                {
                    if (dt1.Rows.Count > 0)
                    {
                        lblResult1.Text = "Hotel Name " + " " + dt1.Rows[0]["Hotel_name"].ToString() + " " + " already exists";
                    }
                }

                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        lblResult2.Text = "Hotel Phone " + " " + dt2.Rows[0]["Hotel_phone"].ToString() + " " + " already exists";
                    }


                    if (txtProp_phone.Text.Length != 8)
                    {
                        lblResult4.Text = " Phone Number should contain 8 digits";

                    }

                    string first_char = txtProp_phone.Text.Trim().Substring(0, 1);

                    if (first_char == "0")
                    {
                        if (lblResult4.Text.Length != 0)
                        {
                            lblResult4.Text = lblResult4.Text + " and cannot start with 0";
                        }
                        else
                        {
                            lblResult4.Text = " Phone Number cannot start with 0";
                        }

                    }


                    if ((txtProp_postcode.Text.Length != 0) && (txtProp_postcode.Text.Length != 5))
                    {
                        lblResult5.Text = "Postal code should contain 5 numbers";
                    }

                   

                    if ((lblResult1.Text.Length == 0) && (lblResult2.Text.Length == 0)
                        && (lblResult3.Text.Length == 0) && (lblResult4.Text.Length == 0)
                        && (lblResult5.Text.Length == 0) && (lblResult6.Text.Length == 0))
                    {
                        cmd4.Parameters.AddWithValue("@Hotel_id", qs);
                        cmd4.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_email", txtProp_email.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_street", txtProp_street.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_town", txtProp_town.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_postcode", txtProp_postcode.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_price", txtProp_priceday.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Hotel_regis_date", DateTime.Now.ToString("MM/dd/yyyy"));
                        cmd4.Parameters.AddWithValue("@Hotel_approval_date", DateTime.Now.ToString("MM/dd/yyyy"));

                        cmd4.Parameters.AddWithValue("@Dist_id", ddlDistrict.SelectedValue.ToString());

                        con4.Open();
                        cmd4.CommandText = "update tblHotel set Hotel_name=@Hotel_name, Hotel_phone=@Hotel_phone, Hotel_email=@Hotel_email, Hotel_street=@Hotel_street, Hotel_town=@Hotel_town, Hotel_postcode=@Hotel_postcode, Hotel_price=@Hotel_price, Dist_id=@Dist_id where Hotel_id=" + qs;

                        Boolean IsAdded = false;
                        IsAdded = cmd4.ExecuteNonQuery() > 0;
                        if (IsAdded)
                        {
                            clear_labels();
                            //villa details 
                            Populate_owner_loc_price();
                            set_owner_loc_price_readonly();
                            initialise_button_status();


                            lblResult1.ForeColor = System.Drawing.Color.Green;
                            lblResult1.Text = "Hotel modified";
                            AddNotification();

                        }
                        else
                        {
                            lblResult1.Text = "An unexpected error occured, please try again";
                        }
                        con1.Close();
                        con2.Close();
                        con4.Close();
                    }
                    else
                    {
                        btnSave_own_loc_price.Visible = true;
                        btnCancel_own_loc_price.Visible = true;
                        btnEdit_own_loc_price.Visible = false;

                        //27/06/2022
                        disable_edit_buttons();
                    }

                }
            }
        }

        void retrieve_and_store_old_values()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHotel th inner join tblDistrict td on td.Dist_id = th.Dist_id where th.Hotel_id = " + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
            
                Prop_name_old = dr["Hotel_name"].ToString();
                Prop_phone_old = dr["Hotel_phone"].ToString();
                Prop_email_old = dr["Hotel_email"].ToString();
                Prop_street_old = dr["Hotel_street"].ToString();
                Prop_town_old = dr["Hotel_town"].ToString();
                Prop_priceday_old = dr["Hotel_price"].ToString();
                Prop_postcode_old = dr["Hotel_postcode"].ToString();
       
                ddlDistrict_old = (dr["Dist_id"].ToString());

            }
        }

        void clear_labels()
        {
            lblResult1.Text = "";
            lblResult2.Text = "";
            lblResult3.Text = "";
            lblResult4.Text = "";
            lblResult5.Text = "";
            lblResult6.Text = "";
            lbl_main_pic.Text = "";
            lbl_slideshow.Text = "";
            lbl_Prop_Desc.Text = "";

          
            lblMsg_additional_info.Text = "";
            lblMsg_facilities.Text = "";
        }
        protected void btnCancel_own_loc_price_Click(object sender, EventArgs e)
        {
            set_owner_loc_price_readonly();
            initialise_button_status();
            clear_labels();
            Populate_owner_loc_price();
        }

        protected void btnEdit_own_loc_price_Click(object sender, EventArgs e)
        {
            clear_labels();
            set_owner_loc_price_Editable();
            btnSave_own_loc_price.Visible = true;
            btnCancel_own_loc_price.Visible = true;

            disable_edit_buttons();
        }

        void set_owner_loc_price_Editable()
        {
            txtProp_name.ReadOnly = false;
            txtProp_town.ReadOnly = false;
            txtProp_priceday.ReadOnly = false;
            txtProp_phone.ReadOnly = false;
            txtProp_town.ReadOnly = false;
            txtProp_street.ReadOnly = false;
            txtProp_postcode.ReadOnly = false;
            ddlDistrict.Enabled = true;
        }

        void disable_edit_buttons()
        {
            btnEdit_own_loc_price.Visible = false;
            btnEdit_main_pic.Visible = false;
            btnEdit_slideshow.Visible = false;
            Btn_Edit_additional_info.Visible = false;
            Btn_Edit_facilities.Visible = false;
            Btn_Edit_description.Visible = false;
        }

        protected void Btn_Edit_description_Click(object sender, EventArgs e)
        {
            clear_labels();

            Btn_Save_description.Visible = true;
            Btn_Edit_description.Visible = false;
            Btn_Cancel_description.Visible = true;
            disable_edit_buttons();

            txt_Description.ReadOnly = false;
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_description';", true);
        }

        protected void Btn_Save_description_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblHotel where Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", qs);
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();

            lbl_Prop_Desc.Text = "";

            if (dr.HasRows)
            {
                dr.Read();
                string description_old = dr["Hotel_desc"].ToString();
                if (txt_Description.Text == description_old)
                {
                    lbl_Prop_Desc.Text = "No change detected";
                    lbl_Prop_Desc.ForeColor = System.Drawing.Color.Red;
                    initialise_button_status();
                    set_owner_loc_price_readonly();
                }

                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = con2;
                    cmd2.CommandText = "update tblHotel set Hotel_desc=@Hotel_desc where Hotel_id=@Hotel_id";
                    cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                    cmd2.Parameters.AddWithValue("@Hotel_desc", txt_Description.Text.Trim());
                    con2.Open();
                    Boolean IsUpdated = false;
                    IsUpdated = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsUpdated)
                    {
                        lbl_Prop_Desc.Text = "Hotel Description updated";
                        lbl_Prop_Desc.ForeColor = System.Drawing.Color.Green;
                        modif_detail = "Description updated for " + txtProp_name.Text;
                        AddNotification_update();

                        Populate_owner_loc_price();
                        initialise_button_status();
                        set_owner_loc_price_readonly();


                    }

                }
            }
            dbcon.Close();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_description';", true);
        }

        protected void Btn_Cancel_description_Click(object sender, EventArgs e)
        {
            initialise_button_status();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_description';", true);
        }

        protected void btnEdit_main_pic_Click(object sender, EventArgs e)
        {
            btnSave_main_pic.Visible = true;
            btnEdit_main_pic.Visible = false;
            btnCancel_main_pic.Visible = true;
            btnupload_main_pic.Visible = true;

            disable_edit_buttons();
            //show_delete_buttons();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
        }

        protected void btnCancel_main_pic_Click(object sender, EventArgs e)
        {

            initialise_button_status();
            clear_labels();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
        }

        protected void btnSave_main_pic_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            if (btnupload_main_pic.FileName != "")
            {
                if (CheckFileType(btnupload_main_pic.FileName))
                {
                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *  from tblHotel where Hotel_id=@Hotel_id";
                    cmd.Parameters.AddWithValue("@Hotel_id", qs);
                    cmd.Connection = con;
                    SqlDataReader dr;
                    con.Open();
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        SqlConnection con1 = new SqlConnection(_conString);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "UPDATE tblHotel set Hotel_image=@Hotel_image where Hotel_id=@Hotel_id";
                        cmd1.Parameters.AddWithValue("@Hotel_id", qs);
                        cmd1.Parameters.AddWithValue("@Hotel_image", "");
                        cmd1.Connection = con1;
                        con1.Open();

                        dr.Read();
                      
                        string filename = "~/Property/" + qs + "/main/" + dr["Hotel_image"].ToString();



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


                            string folderPath = Server.MapPath("~/Property/" + qs + "/main");
                            //Check whether Directory (Folder) exists.
                            if (!Directory.Exists(folderPath))
                            {
                                //If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(folderPath);
                            }


                            String filePath = "~/Property/" + qs + "/main/" + btnupload_main_pic.FileName;
                            btnupload_main_pic.SaveAs(MapPath(filePath));

                            SqlConnection con2 = new SqlConnection(_conString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "UPDATE tblHotel set Hotel_image=@Hotel_image where Hotel_id=@Hotel_id";
                            cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                            //cmd2.Parameters.AddWithValue("@Prop_image", filePath);
                            cmd2.Parameters.AddWithValue("@Hotel_image", btnupload_main_pic.FileName);
                            cmd2.Connection = con2;
                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

                            Populate_owner_loc_price();
                            clear_labels();
                            lbl_main_pic.Text = "Main Picture updated";
                            lbl_main_pic.ForeColor = System.Drawing.Color.Green;
                            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                            modif_detail = "Main Image updated for " + txtProp_name.Text;
                            AddNotification_update();

                        }
                        con1.Close();
                    }
                    con.Close();
                }

                else
                {
                    lbl_main_pic.Text = "Wrong format, please upload a picture";
                    lbl_main_pic.ForeColor = System.Drawing.Color.Red;
                    btnSave_main_pic.Visible = true;
                    btnEdit_main_pic.Visible = false;
                    btnCancel_main_pic.Visible = true;
                    btnupload_main_pic.Visible = true;
                    disable_edit_buttons();
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                }

            }
            else
            {
                lbl_main_pic.Text = "Please upload a picture";
                lbl_main_pic.ForeColor = System.Drawing.Color.Red;
                btnSave_main_pic.Visible = true;
                btnEdit_main_pic.Visible = false;
                btnCancel_main_pic.Visible = true;
                btnupload_main_pic.Visible = true;
                disable_edit_buttons();
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
            }
        }

        protected void btnEdit_slideshow_Click(object sender, EventArgs e)
        {
            clear_labels();
            btnEdit_slideshow.Visible = false;
            btnCancel_slideshow.Visible = true;
            if (Convert.ToInt32(Session["count_slide_show"]) < 10)
            {
                btnupload_slideshow.Visible = true;
                btnSave_slideshow.Visible = true;
            }
            disable_edit_buttons();
            show_delete_buttons();

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);

        }

        protected void btnSave_slideshow_Click(object sender, EventArgs e)
        {
            if (btnupload_slideshow.FileName != "")
            {
                int qs = Convert.ToInt32(Request.QueryString["ID"]);

                if (CheckFileType(btnupload_slideshow.FileName))
                {
                    String filePath = "~/Property/" + qs + "/" + btnupload_slideshow.FileName;
                    lbl_slideshow.Text = "";

                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Hotel_id=@Hotel_id";
                    cmd.Parameters.AddWithValue("@Hotel_id", qs);
                    cmd.Connection = con;
                    SqlDataReader dr;
                    con.Open();
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        Session["count_slide_show"] = dr["num_image_slide_show"];

                        if (Convert.ToInt32(Session["count_slide_show"]) < 10)
                        {
                            SqlConnection con2 = new SqlConnection(_conString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "SELECT *  from tblImage where Hotel_id=@Hotel_id and Img_name=@Img_name";
                            cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                          
                            cmd2.Parameters.AddWithValue("@Img_name", btnupload_slideshow.FileName);
                            cmd2.Connection = con2;
                            SqlDataReader dr2;
                            con2.Open();

                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {

                                lbl_slideshow.Text = "Image with same name already exists";
                                lbl_slideshow.ForeColor = System.Drawing.Color.Red;

                                initialise_button_status();
                               
                                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                               
                                return;
                            }

                            else
                            {

                                string folderPath = Server.MapPath("~/Property/" + qs);
                                //Check whether Directory (Folder) exists.
                                if (!Directory.Exists(folderPath))
                                {
                                    //If Directory (Folder) does not exists. Create it.
                                    Directory.CreateDirectory(folderPath);
                                }


                                btnupload_slideshow.SaveAs(MapPath(filePath));

                                SqlConnection con1 = new SqlConnection(_conString);
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.AddWithValue("@Hotel_id", qs);
                                //cmd1.Parameters.AddWithValue("@Img_name", filePath);
                                cmd1.Parameters.AddWithValue("@Img_name", btnupload_slideshow.FileName);
                                if (Convert.ToInt32(Session["count_slide_show"]) < 10)
                                {
                                    con1.Open();
                                    cmd1.CommandText = "INSERT INTO tblImage ([Img_name], [Hotel_id]) VALUES (@Img_name, @Hotel_id)";
                                    Boolean IsAdded = false;

                                    int count_slide_show = Convert.ToInt32(Session["count_slide_show"]);

                                    IsAdded = cmd1.ExecuteNonQuery() > 0;
                                    if (IsAdded)
                                    {
                                        lbl_slideshow.Visible = true;

                                        count_slide_show++;
                                        lbl_slideshow.Text = btnupload_slideshow.FileName + " " + "uploaded" + " " + "(Picture" + " " + count_slide_show + ")";
                                        lbl_slideshow.ForeColor = System.Drawing.Color.Green;

                                        Session["count_slide_show"] = Convert.ToInt32(Session["count_slide_show"]) + 1;

                                     
                                        btnupload_slideshow.Visible = true;
                                        btnSave_slideshow.Visible = true;
                                        btnEdit_slideshow.Visible = false;
                                        btnCancel_slideshow.Visible = true;
                                        show_delete_buttons();


                                        if (Convert.ToInt32(Session["count_slide_show"]) == 10)
                                        {
                                           
                                            btnupload_slideshow.Visible = false;
                                            btnSave_slideshow.Visible = false;
                                         
                                            btnEdit_slideshow.Visible = false;
                                            btnCancel_slideshow.Visible = true;

                                           
                                            disable_edit_buttons();
                                        }

                                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                                        modif_detail = "New Images added for " + txtProp_name.Text;
                                        AddNotification_update();

                                    }

                                }

                            

                                con1.Close();
                            }

                            con2.Close();


                        }
                        else
                        {
                            lbl_slideshow.Text = "";
                            lbl_slideshow.Visible = true;
                            lbl_slideshow.Text = "You have already reached maximum pictures";
                            lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                            initialise_button_status();
                        }

                    }
                    con.Close();
                    Populate_dlstImages();
                    show_delete_buttons();
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                }

                else
                {
                    lbl_slideshow.Text = "Wrong format, please upload a picture";
                    lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                    btnEdit_slideshow.Visible = false;
                    btnCancel_slideshow.Visible = true;
                    if (Convert.ToInt32(Session["count_slide_show"]) < 10)
                    {
                        btnupload_slideshow.Visible = true;
                        btnSave_slideshow.Visible = true;
                    }
                    disable_edit_buttons();
                    show_delete_buttons();
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                }

            }
            else
            {
                lbl_slideshow.Text = "Please upload a picture";
                lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                btnEdit_slideshow.Visible = false;
                btnCancel_slideshow.Visible = true;
                if (Convert.ToInt32(Session["count_slide_show"]) < 10)
                {
                    btnupload_slideshow.Visible = true;
                    btnSave_slideshow.Visible = true;
                }
                disable_edit_buttons();
                show_delete_buttons();
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
            }
        }

        void show_delete_buttons()
        {
            foreach (DataListItem di in dlstImages.Items)
            {
                di.FindControl("lbtndelete_slideshow").Visible = true;
            }
        }

        protected void btnCancel_slideshow_Click(object sender, EventArgs e)
        {
            initialise_button_status();
            clear_labels();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
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

        protected void lbtndelete_slideshow_Click(object sender, EventArgs e)
        {
            string filename = (sender as LinkButton).CommandArgument;

            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.CommandText = "delete from tblImage where Img_name=@Img_name";
            cmd2.Parameters.AddWithValue("@Img_name", filename);
            cmd2.Connection = con2;
            con2.Open();


            cmd2.ExecuteNonQuery();
            con2.Close();

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con3 = new SqlConnection(_conString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Hotel_id=@Hotel_id";
            cmd3.Parameters.AddWithValue("@Hotel_id", qs);
            cmd3.Connection = con3;
            SqlDataReader dr3;
            con3.Open();
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                Session["count_slide_show"] = dr3["num_image_slide_show"];
            }

            string path = Server.MapPath(filename);
           
            if (File.Exists(path))
            {
                File.Delete(path);
             

            }
            Populate_dlstImages();
            clear_labels();
            btnEdit_slideshow.Visible = false;
            btnCancel_slideshow.Visible = true;
            if (Convert.ToInt32(Session["count_slide_show"]) < 10)
            {
                btnupload_slideshow.Visible = true;
                btnSave_slideshow.Visible = true;
            }
            disable_edit_buttons();
            show_delete_buttons();

            modif_detail = "Images deleted for " + txtProp_name.Text;
            AddNotification_update();

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
        }

        protected void gvs_2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Read data from GridView and Populate the form
            txtDet_Id.Text = (gvs_2.DataKeys[gvs_2.SelectedIndex].Value.ToString());
            ddlDet_name.SelectedValue = (gvs_2.DataKeys[gvs_2.SelectedIndex].Value.ToString());
            txtCount.Text = ((Label)gvs_2.SelectedRow.FindControl("lblCount")).Text;

            btnInsert_Add_info.Visible = false;
            btnUpdate_Add_info.Visible = true;
            btnDelete_Add_info.Visible = true;
            btnCancel_Add_info.Visible = true;

            ddlDet_name.Enabled = false;

            lblMsg_additional_info.Text = "";

         
            disable_edit_buttons();

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);

        }

        protected void gvs_3_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Read data from GridView and Populate the form
            txt_Facility_ID.Text = (gvs_3.DataKeys[gvs_3.SelectedIndex].Value.ToString());
            ddl_Facility.SelectedValue = (gvs_3.DataKeys[gvs_3.SelectedIndex].Value.ToString());

            BtnInsert_Facility.Visible = false;
            BtnDelete_Facility.Visible = true;
            BtnCancel_Facility.Visible = true;

            ddl_Facility.Enabled = false;

            lblMsg_facilities.Text = "";

        
            disable_edit_buttons();

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);

        }

        protected void gvs_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_2.PageIndex = e.NewPageIndex;
            getDetails();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void gvs_3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_3.PageIndex = e.NewPageIndex;
            getFacilities();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
        }
        protected void Btn_Edit_additional_info_Click(object sender, EventArgs e)
        {
            clear_labels();

            pnl_Details.Visible = true;
            gvs_2.Columns[0].Visible = true;

            Btn_Cancel_additional_info.Visible = true;
            btnInsert_Add_info.Visible = true;

            disable_edit_buttons();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }
        protected void Btn_Cancel_additional_info_Click(object sender, EventArgs e)
        {
            clear_labels();

            initialise_button_status();
            pnl_Details.Visible = false;
            gvs_2.Columns[0].Visible = false;

            lblMsg_additional_info.Text = "";
            ResetAdditional_info();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }
        void ResetAdditional_info()
        {
            btnInsert_Add_info.Visible = true;
            btnUpdate_Add_info.Visible = false;
            btnDelete_Add_info.Visible = false;
            btnCancel_Add_info.Visible = false;
            txtDet_Id.Text = "";
            txtCount.Text = "";
            ddlDet_name.SelectedIndex = -1;
        }

        protected void btnInsert_Add_info_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);

          
            clear_labels();
            
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

           
            cmd.CommandText = "select * from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hotel_id", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " already assigned!";
                    lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;

                

                    getDetails();
                    ResetAdditional_info();
                    pnl_Details.Visible = true;
                    gvs_2.Columns[0].Visible = true;
                    Btn_Cancel_additional_info.Visible = true;
                    disable_edit_buttons();

                }
                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "insert into tblDetails_Hotel ([Det_id], [Hotel_id], [Count]) values (@Det_id, @Hotel_id, @Count)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                    cmd2.Parameters.AddWithValue("@Count", txtCount.Text.Trim());

                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " added successfully!";
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindCategoryData()
                        getDetails();
                     
                        ResetAdditional_info();
                        pnl_Details.Visible = true;
                        gvs_2.Columns[0].Visible = true;
                        Btn_Cancel_additional_info.Visible = true;
                        disable_edit_buttons();

                        modif_detail = "Details added to " + txtProp_name.Text;
                        AddNotification_update();

                    }
                    else
                    {
                        lblMsg_additional_info.Text = "Error while adding " + ddlDet_name.SelectedItem.ToString();
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                        ResetAdditional_info();
                    }

                }
            }
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void btnUpdate_Add_info_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);

          
            clear_labels();
            //lblMsg_additional_info.Text = "";

            String Count_old = "";

            if ((ddlDet_name.SelectedIndex == -1) || (string.IsNullOrWhiteSpace(txtCount.Text)))
            {
                lblMsg_additional_info.Text = "Please select record to update";
                lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                //return;
            }
            else
            {
                SqlConnection con0 = new SqlConnection(_conString);
                SqlCommand cmd0 = new SqlCommand();
                cmd0.CommandType = CommandType.Text;

                cmd0.Connection = con0;
                cmd0.CommandText = "select * from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
                //Create a parametererized query for CatID
                cmd0.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                cmd0.Parameters.AddWithValue("@Hotel_id", qs);

                SqlDataAdapter da0 = new SqlDataAdapter(cmd0);
                DataTable dt0 = new DataTable();

                using (da0)
                {
                    da0.Fill(dt0);
                    if (dt0.Rows.Count > 0)
                    {
                        Count_old = dt0.Rows[0]["Count"].ToString();

                    }

                }

                if (txtCount.Text.Trim() == Count_old)
                {
                   

                    lblMsg_additional_info.Text = "No change detected";
                    lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                    //initialise_button_status();
                    pnl_Details.Visible = true;
                    gvs_2.Columns[0].Visible = true;
                    ResetAdditional_info();


                }

                else
                {

                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = con2;
                    cmd2.CommandText = "update tblDetails_Hotel set Count=@Count where Det_id=@Det_id and Hotel_id=@Hotel_id";

                    cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                    cmd2.Parameters.AddWithValue("@Count", txtCount.Text.Trim());

                    con2.Open();
                    Boolean IsUpdated = false;
                    IsUpdated = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsUpdated)
                    {
                        lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " successfully updated!";
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
                        
                        ResetAdditional_info();
                        pnl_Details.Visible = true;
                        gvs_2.Columns[0].Visible = true;
                        getDetails();
                        Btn_Cancel_additional_info.Visible = true;
                        modif_detail = "Details updated for " + txtProp_name.Text;
                        AddNotification_update();
                        disable_edit_buttons();

                    }

                }

            }
            ddlDet_name.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void btnDelete_Add_info_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);

            lblMsg_additional_info.Text = "";
            if ((ddlDet_name.SelectedIndex == -1) || (string.IsNullOrWhiteSpace(txtCount.Text)))
            {
                lblMsg_additional_info.Text = "Please select record to Delete";
                lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                ddlDet_name.Enabled = true;
                //return;
            }
            else
            {

                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "delete from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
                cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                cmd2.Connection = con2;
                con2.Open();

                Boolean IsDeleted = false;
                IsDeleted = cmd2.ExecuteNonQuery() > 0;
                con2.Close();

                if (IsDeleted)
                {
                    lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " deleted successfully!";
                    lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
                    //Refresh the GridView by calling the BindCategoryData()
                    getDetails();
                  
                    ResetAdditional_info();
                    pnl_Details.Visible = true;
                    gvs_2.Columns[0].Visible = true;
                    Btn_Cancel_additional_info.Visible = true;
                    disable_edit_buttons();

                    modif_detail = "Details deleted for " + txtProp_name.Text;
                    AddNotification_update();
                }
                else
                {
                    lblMsg_additional_info.Text = "Error while deleting " + ddlDet_name.SelectedItem.ToString();
                    lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                }
                getDetails();
                ResetAdditional_info();
            }
            ddlDet_name.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void btnCancel_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
            getDetails();
            clear_labels();
            ResetAdditional_info();
            pnl_Details.Visible = true;
            gvs_2.Columns[0].Visible = true;
            Btn_Cancel_additional_info.Visible = true;
            disable_edit_buttons();
            ddlDet_name.Enabled = true;

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        void getDetails()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDetails td inner join tblDetails_Hotel tdh on tdh.Det_id=td.Det_id where tdh.Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", qs);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            gvs_2.DataSource = dt;
            gvs_2.DataBind();
        }

        protected void Btn_Edit_facilities_Click(object sender, EventArgs e)
        {
            clear_labels();
            Pnl_facilities.Visible = true;
            gvs_3.Columns[0].Visible = true;
            Btn_Cancel_facilities.Visible = true;
            Btn_Edit_facilities.Visible = false;
            BtnInsert_Facility.Visible = true;
            disable_edit_buttons();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
        }

        protected void Btn_Cancel_facilities_Click(object sender, EventArgs e)
        {

            clear_labels();
            initialise_button_status();
            Pnl_facilities.Visible = false;
            gvs_3.Columns[0].Visible = false;
            lblMsg_facilities.Text = "";
            ResetFacilities();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
        }

        void ResetFacilities()
        {
            BtnInsert_Facility.Visible = true;
            BtnDelete_Facility.Visible = false;
            BtnCancel_Facility.Visible = false;
            txt_Facility_ID.Text = "";
            ddl_Facility.SelectedIndex = -1;
        }

        void getFacilities()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFacilities tf inner join tblFacilities_Hotel tfh on tfh.Fac_id=tf.Fac_id where tfh.Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", qs);
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            gvs_3.DataSource = dt;
            gvs_3.DataBind();
        }

        protected void BtnInsert_Facility_Click(object sender, EventArgs e)
        {
           
            clear_labels();

            int qs = Convert.ToInt32(Request.QueryString["ID"]);

            //Add built-in function to remove spaces from Textbox Category name
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            cmd.CommandText = "select * from tblFacilities_Hotel where Hotel_id=@Hotel_id and Fac_id=@Fac_id";

            cmd.Parameters.AddWithValue("@Fac_id", ddl_Facility.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hotel_id", qs);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                  
                    clear_labels();
                    lblMsg_facilities.Text = "Facility " + ddl_Facility.SelectedItem.ToString() + " already assigned!";
                    lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                    getFacilities();
                    ResetFacilities();
                    Pnl_facilities.Visible = true;
                    gvs_3.Columns[0].Visible = true;
                    Btn_Cancel_facilities.Visible = true;
                    disable_edit_buttons();

                }
                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "insert into tblFacilities_Hotel ([Fac_id], [Hotel_id]) values (@Fac_id, @Hotel_id)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@Fac_id", ddl_Facility.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Hotel_id", qs);

                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                      
                        clear_labels();
                        lblMsg_facilities.Text = ddl_Facility.SelectedItem.ToString() + " added successfully!";
                        lblMsg_facilities.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindCategoryData()

                        getFacilities();
                      
                        clear_labels();
                        ResetFacilities();
                        Pnl_facilities.Visible = true;
                        gvs_3.Columns[0].Visible = true;
                        Btn_Cancel_facilities.Visible = true;
                        disable_edit_buttons();

                        modif_detail = "Facilities added to " + txtProp_name.Text;
                        AddNotification_update();
                    }
                    else
                    {
                        lblMsg_facilities.Text = "Error while adding " + ddl_Facility.SelectedItem.ToString();
                        lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                        ResetFacilities();
                    }

                }
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
            }
        }

        protected void BtnCancel_Facility_Click(object sender, EventArgs e)
        {
            
            clear_labels();
           
            getFacilities();
            ResetFacilities();
            Pnl_facilities.Visible = true;
            gvs_3.Columns[0].Visible = true;
            Btn_Cancel_facilities.Visible = true;
            disable_edit_buttons();
            ddl_Facility.Enabled = true;

            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
        }

        protected void BtnDelete_Facility_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);

            //27/06/2022
            clear_labels();
            //lblMsg_facilities.Text = "";
            if (ddl_Facility.SelectedIndex == -1)
            {
                lblMsg_facilities.Text = "Please select record to Delete";
                lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                ddl_Facility.Enabled = true;
            }
            else
            {


                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "delete from tblFacilities_Hotel where Fac_id=@Fac_id and Hotel_id=@Hotel_id";
                cmd2.Parameters.AddWithValue("@Fac_id", txt_Facility_ID.Text.Trim());
                cmd2.Parameters.AddWithValue("@Hotel_id", qs);
                cmd2.Connection = con2;
                con2.Open();

                Boolean IsDeleted = false;
                IsDeleted = cmd2.ExecuteNonQuery() > 0;
                con2.Close();

                if (IsDeleted)
                {
                    lblMsg_facilities.Text = ddl_Facility.SelectedItem.ToString() + " deleted successfully!";
                    lblMsg_facilities.ForeColor = System.Drawing.Color.Green;
                    //Refresh the GridView by calling the BindCategoryData()

                    modif_detail = "Facilites removed for " + txtProp_name.Text;
                    AddNotification_update();
                }
                else
                {
                    lblMsg_facilities.Text = "Error while deleting " + ddl_Facility.SelectedItem.ToString();
                    lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                }
            }
            getFacilities();
            //27/06/2022
            //clear_labels();
            ResetFacilities();
            Pnl_facilities.Visible = true;
            gvs_3.Columns[0].Visible = true;
            Btn_Cancel_facilities.Visible = true;
            disable_edit_buttons();
            ddl_Facility.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
        }

        protected void AddNotification()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.Connection = con2;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd2.CommandText = "insert into tblNotification ([Notif_details], [Notif_type], [user_id], [Hotel_id], [Notif_date], [State], [Show]) values (@details, @type, @user, @Hotel, @date, @state, @show)";
            //Create a parametererized query for CatID
            cmd2.Parameters.AddWithValue("@details", "Modification on Villa " + txtProp_name.Text.ToString());
            cmd2.Parameters.AddWithValue("@type", "notif_up");
            cmd2.Parameters.AddWithValue("@user", Session["owner_id"]);
            cmd2.Parameters.AddWithValue("@Hotel", qs);
            cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM/dd/yyyy"));
            cmd2.Parameters.AddWithValue("@state", 0);
            cmd2.Parameters.AddWithValue("@show", 1);


            con2.Open();
            Boolean IsAdded = false;
            IsAdded = cmd2.ExecuteNonQuery() > 0;
            con2.Close();
        }

        protected void AddNotification_update()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.Connection = con2;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd2.CommandText = "insert into tblNotification ([Notif_details], [Notif_type], [user_id], [Hotel_id], [Notif_date], [State], [Show]) values (@details, @type, @user, @Hotel, @date, @state, @show)";
            //Create a parametererized query for CatID
            cmd2.Parameters.AddWithValue("@details", modif_detail);
            cmd2.Parameters.AddWithValue("@type", "update");
            cmd2.Parameters.AddWithValue("@user", Session["owner_id"]);
            cmd2.Parameters.AddWithValue("@Hotel", qs);
            cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM/dd/yyyy"));
            cmd2.Parameters.AddWithValue("@state", 0);
            cmd2.Parameters.AddWithValue("@show", 1);


            con2.Open();
            Boolean IsAdded = false;
            IsAdded = cmd2.ExecuteNonQuery() > 0;
            con2.Close();
        }


    }
}



