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

namespace DealProjectTamam.AdminS
{
    public partial class Villa_details : System.Web.UI.Page
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

        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                get_districts();
                Populate_owner_loc_price();
                Populate_dlstImages();
                populate_details_dropdown();
                getDetails();
                populate_facilities_dropdown();
                getFacilities();
                set_owner_loc_price_readonly();
              


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
            
            cmd.CommandText = "SELECT * FROM tblVilla tv inner join tblDistrict td on td.Dist_id = tv.Dist_id inner join tblOwner tow on tow.Own_id = tv.Own_id where tv.Villa_id = " + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtProp_id.Text = dr["Villa_id"].ToString();
                txtProp_name.Text = dr["Villa_name"].ToString();
                txtProp_phone.Text = dr["Villa_phone"].ToString();
                txtProp_email.Text = dr["Villa_email"].ToString();
                txtProp_street.Text = dr["Villa_street"].ToString();
                txtProp_town.Text = dr["Villa_town"].ToString();
                txtProp_priceday.Text = dr["Villa_priceday"].ToString();
                txtProp_priceweek.Text = dr["Villa_priceweek"].ToString();
                txtProp_pricemonth.Text = dr["Villa_pricemonth"].ToString();
                txtProp_postcode.Text = dr["Villa_postcode"].ToString();
                txt_Description.Text = dr["Villa_desc"].ToString();
            
                ddlDistrict.Items[0].Text = (dr["Dist_name"].ToString() + " " + "(" + dr["Dist_region"].ToString() + ")");
                ddlDistrict.Items[0].Value = dr["Dist_id"].ToString();

                lbl_owner.Text = dr["Own_fname"].ToString() + " " + dr["Own_lname"].ToString();

                img_main.ImageUrl = "~/Property/" + dr["Villa_id"].ToString() + "/main/" + dr["Villa_image"].ToString();

            }
        }

        void Populate_dlstImages()
        {
           
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblImage where Villa_id = " + qs;
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
            cmd3.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Villa_id=@Villa_id";
            cmd3.Parameters.AddWithValue("@Villa_id", qs);
            cmd3.Connection = con3;
            SqlDataReader dr3;
            con3.Open();
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                Session["count_slide_show"] = dr3["num_image_slide_show"];
            }
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

        protected void gvs_2_PreRender(object sender, EventArgs e)
        {
            if (gvs_2.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_2.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_2.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
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

        }

        protected void gvs_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_2.PageIndex = e.NewPageIndex;
            getDetails();
        }

        void getDetails()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDetails td inner join tblDetails_Villa tdv on tdv.Det_id=td.Det_id where tdv.Villa_id=@Villa_id";
            cmd.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.ToString());
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

        void getFacilities()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFacilities tf inner join tblFacilities_Villa tfv on tfv.Fac_id=tf.Fac_id where tfv.Villa_id=@Villa_id";
            cmd.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.ToString());
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

        protected void gvs_3_PreRender(object sender, EventArgs e)
        {
            if (gvs_3.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_3.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_3.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
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

        }

        protected void gvs_3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_3.PageIndex = e.NewPageIndex;
            getFacilities();
        }

        void set_owner_loc_price_readonly()
        {
            txtProp_name.ReadOnly = true;
           
            txtProp_town.ReadOnly = true;
            txtProp_priceday.ReadOnly = true;
            txtProp_priceweek.ReadOnly = true;
            txtProp_pricemonth.ReadOnly = true;
            txtProp_phone.ReadOnly = true;
            txtProp_town.ReadOnly = true;
            txtProp_street.ReadOnly = true;
            txtProp_postcode.ReadOnly = true;
            ddlDistrict.Enabled = false;

            ddlDistrict.CssClass = "form-control form-control-user";

            txt_Description.ReadOnly = true;

        }

        void set_owner_loc_price_Editable()
        {
            txtProp_name.ReadOnly = false;
         
            txtProp_town.ReadOnly = false;
            txtProp_priceday.ReadOnly = false;
            txtProp_priceweek.ReadOnly = false;
            txtProp_pricemonth.ReadOnly = false;
            txtProp_phone.ReadOnly = false;
            txtProp_town.ReadOnly = false;
            txtProp_street.ReadOnly = false;
            txtProp_postcode.ReadOnly = false;
            ddlDistrict.Enabled = true;
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
                && (txtProp_priceweek.Text == Prop_priceweek_old)
                && (txtProp_pricemonth.Text == Prop_pricemonth_old)
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

                cmd1.Parameters.AddWithValue("@Villa_name", txtProp_name.Text.Trim());
                cmd1.Parameters.AddWithValue("@Villa_id", qs);
                cmd2.Parameters.AddWithValue("@Villa_phone", txtProp_phone.Text.Trim());
                cmd2.Parameters.AddWithValue("@Villa_id", qs);

                cmd1.CommandText = "SELECT * FROM tblVilla where Villa_name=@Villa_name and Villa_id <> @Villa_id";
                cmd2.CommandText = "SELECT * FROM tblVilla where Villa_phone=@Villa_phone and Villa_id <> @Villa_id";

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
                        lblResult1.Text = "Villa Name " + " " + dt1.Rows[0]["Villa_name"].ToString() + " " + " already exists";
                    }
                }

                if (dt2 != null)
                {
                    if (dt2.Rows.Count > 0)
                    {
                        lblResult2.Text = "Villa Phone " + " " + dt2.Rows[0]["Villa_phone"].ToString() + " " + " already exists";
                    }


                    if (txtProp_phone.Text.Length != 7)
                    {
                        lblResult4.Text = "Fixed Phone Number should contain 8 digits";

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
                            lblResult4.Text = "Fixed Phone Number cannot start with 0";
                        }

                    }


                    if ((txtProp_postcode.Text.Length != 0) && (txtProp_postcode.Text.Length != 5))
                    {
                        lblResult5.Text = "Postal code should contain 5 numbers";
                    }

                    if ((txtProp_priceday.Text.Length != 0) && (txtProp_priceweek.Text.Length != 0) && (txtProp_pricemonth.Text.Length != 0))
                    {
                        int priceday = Int32.Parse(txtProp_priceday.Text);
                        int priceweek = Int32.Parse(txtProp_priceweek.Text);
                        int pricemonth = Int32.Parse(txtProp_pricemonth.Text);

                        if ((priceday >= priceweek) || (priceweek >= pricemonth))
                        {
                            lblResult6.Text = "Price per day should be less than price per week and price per week should be less than price per month";
                        }
                    }

                    if ((lblResult1.Text.Length == 0) && (lblResult2.Text.Length == 0)
                        && (lblResult3.Text.Length == 0) && (lblResult4.Text.Length == 0)
                        && (lblResult5.Text.Length == 0) && (lblResult6.Text.Length == 0))
                    {
                        cmd4.Parameters.AddWithValue("@Villa_id", qs);
                        cmd4.Parameters.AddWithValue("@Villa_name", txtProp_name.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_phone", txtProp_phone.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_email", txtProp_email.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_street", txtProp_street.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_town", txtProp_town.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_postcode", txtProp_postcode.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_priceday", txtProp_priceday.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_priceweek", txtProp_priceweek.Text.Trim());
                        cmd4.Parameters.AddWithValue("@Villa_month", txtProp_pricemonth.Text.Trim());

                        cmd4.Parameters.AddWithValue("@Villa_regis_date", DateTime.Now.ToString("MM/dd/yyyy"));
                        cmd4.Parameters.AddWithValue("@Villa_approval_date", DateTime.Now.ToString("MM/dd/yyyy"));

                        cmd4.Parameters.AddWithValue("@Dist_id", ddlDistrict.SelectedValue.ToString());

                        con4.Open();
                        cmd4.CommandText = "update tblVilla set Villa_name=@Villa_name, Villa_phone=@Villa_phone, Villa_email=@Villa_email, Villa_street=@Villa_street, Villa_town=@Villa_town, Villa_postcode=@Villa_postcode, Prop_priceday=@Villa_priceday, Villa_priceweek=@Villa_priceweek, Villa_pricemonth=@Villa_month, Dist_id=@Dist_id where Villa_id=" + qs;

                        Boolean IsAdded = false;
                        IsAdded = cmd4.ExecuteNonQuery() > 0;
                        if (IsAdded)
                        {
                            clear_labels();
                           
                            Populate_owner_loc_price();
                            set_owner_loc_price_readonly();
                            initialise_button_status();


                            lblResult1.ForeColor = System.Drawing.Color.Green;
                            lblResult1.Text = "Property modified";

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
       
            cmd.CommandText = "SELECT * FROM tblVilla tv inner join tblDistrict td on td.Dist_id = tv.Dist_id where tv.Villa_id = " + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtProp_id.Text = dr["Villa_id"].ToString();
                Prop_name_old = dr["Villa_name"].ToString();
                Prop_phone_old = dr["Villa_phone"].ToString();
                Prop_email_old = dr["Villa_email"].ToString();
                Prop_street_old = dr["Villa_street"].ToString();
                Prop_town_old = dr["Villa_town"].ToString();
                Prop_priceday_old = dr["Villa_priceday"].ToString();
                Prop_priceweek_old = dr["Villa_priceweek"].ToString();
                Prop_pricemonth_old = dr["Villa_pricemonth"].ToString();
                Prop_postcode_old = dr["Villa_postcode"].ToString();
               
                ddlDistrict_old = (dr["Dist_id"].ToString());

            }
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

        void initialise_button_status()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con3 = new SqlConnection(_conString);
            SqlCommand cmd3 = new SqlCommand();
            cmd3.CommandType = CommandType.Text;
            cmd3.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Villa_id=@Villa_id";
            cmd3.Parameters.AddWithValue("@Villa_id", qs);
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

        //Accessibility edit button
        protected void Btn_Edit_additional_info_Click(object sender, EventArgs e)
        {
            clear_labels();

            pnl_Details.Visible = true;
            gvs_2.Columns[0].Visible = true;

            Btn_Cancel_additional_info.Visible = true;
            btnInsert_Add_info.Visible = true;

            disable_edit_buttons();

        }

        //Accessibility cancel button
        protected void Btn_Cancel_additional_info_Click(object sender, EventArgs e)
        {
            clear_labels();

            initialise_button_status();
            pnl_Details.Visible = false;
            gvs_2.Columns[0].Visible = false;

            lblMsg_additional_info.Text = "";
            ResetAdditional_info();
        }

        //Accessibility insert button (for dropdown)
        protected void btnInsert_Add_info_Click(object sender, EventArgs e)
        {
          
            clear_labels();
           

            //Add built-in function to remove spaces from Textbox Category name
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblDetails_Villa where Det_id=@Det_id and Villa_id=@Villa_id";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());

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
                    cmd2.CommandText = "insert into tblDetails_Villa ([Det_id], [Villa_id], [Count]) values (@Det_id, @Villa_id, @Count)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());
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

                    }
                    else
                    {
                        lblMsg_additional_info.Text = "Error while adding " + ddlDet_name.SelectedItem.ToString();
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                        ResetAdditional_info();
                    }

                }
            }
        }

        //Accessibility update button (for dropdown)
        protected void btnUpdate_Add_info_Click(object sender, EventArgs e)
        {
           
            clear_labels();
           
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
                cmd0.CommandText = "select * from tblDetails_Villa where Det_id=@Det_id and Villa_id=@Villa_id";
                //Create a parametererized query for CatID
                cmd0.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                cmd0.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());

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
                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "update tblDetails_Villa set Count=@Count where Det_id=@Det_id and Villa_id=@Villa_id";

                    cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());
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
                        disable_edit_buttons();


                    }


                }


            }
            ddlDet_name.Enabled = true;
        }

        //Accessibility delete button (for dropdown)
        protected void btnDelete_Add_info_Click(object sender, EventArgs e)
        {
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

                cmd2.CommandText = "delete from tblDetails_Villa where Det_id=@Det_id and Villa_id=@Villa_id";
                cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                cmd2.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());
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
        }

        //Accessibility cancel button (for dropdown)
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

        }

        protected void Btn_Cancel_facilities_Click(object sender, EventArgs e)
        {
            clear_labels();
            initialise_button_status();
            Pnl_facilities.Visible = false;
            gvs_3.Columns[0].Visible = false;
            lblMsg_facilities.Text = "";
            ResetFacilities();

        }



        protected void BtnInsert_Facility_Click(object sender, EventArgs e)
        {
           
            clear_labels();
            //lblMsg_facilities.Text = "";

            //Add built-in function to remove spaces from Textbox Category name
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblFacilities_Villa where Villa_id=@Villa_id and Fac_id=@Fac_id";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@Fac_id", ddl_Facility.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());

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
                    cmd2.CommandText = "insert into tblFacilities_Villa ([Fac_id], [Villa_id]) values (@Fac_id, @Villa_id)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@Fac_id", ddl_Facility.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());

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
                       
                        ResetFacilities();
                        Pnl_facilities.Visible = true;
                        gvs_3.Columns[0].Visible = true;
                        Btn_Cancel_facilities.Visible = true;
                        disable_edit_buttons();
                    }
                    else
                    {
                        lblMsg_facilities.Text = "Error while adding " + ddl_Facility.SelectedItem.ToString();
                        lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                        ResetFacilities();
                    }

                }
            }
        }

        protected void BtnDelete_Facility_Click(object sender, EventArgs e)
        {
          
            clear_labels();
            //lblMsg_facilities.Text = "";
            if (ddl_Facility.SelectedIndex == -1)
            {

                lblMsg_facilities.Text = "Please select record to Delete";
                lblMsg_facilities.ForeColor = System.Drawing.Color.Red;

            }
            else
            {

                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "delete from tblFacilities_Property where Fac_id=@Fac_id and Villa_id=@Villa_id";
                cmd2.Parameters.AddWithValue("@Fac_id", txt_Facility_ID.Text.Trim());
                cmd2.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());
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

                }
                else
                {
                    lblMsg_facilities.Text = "Error while deleting " + ddl_Facility.SelectedItem.ToString();
                    lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                }
            }
            getFacilities();
           
            ResetFacilities();
            Pnl_facilities.Visible = true;
            gvs_3.Columns[0].Visible = true;
            Btn_Cancel_facilities.Visible = true;
            disable_edit_buttons();
            ddl_Facility.Enabled = true;

        }

        protected void BtnCancel_Facility_Click(object sender, EventArgs e)
        {
         
            clear_labels();
            //lblMsg_facilities.Text = "";
            getFacilities();
            ResetFacilities();
            Pnl_facilities.Visible = true;
            gvs_3.Columns[0].Visible = true;
            Btn_Cancel_facilities.Visible = true;
            disable_edit_buttons();
            ddl_Facility.Enabled = true;
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

        void ResetFacilities()
        {
            BtnInsert_Facility.Visible = true;
            BtnDelete_Facility.Visible = false;
            BtnCancel_Facility.Visible = false;
            txt_Facility_ID.Text = "";
            ddl_Facility.SelectedIndex = -1;
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
            cmd3.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Villa_id=@Villa_id";
            cmd3.Parameters.AddWithValue("@Villa_id", qs);
            cmd3.Connection = con3;
            SqlDataReader dr3;
            con3.Open();
            dr3 = cmd3.ExecuteReader();

            while (dr3.Read())
            {
                Session["count_slide_show"] = dr3["num_image_slide_show"];
            }

            string path = Server.MapPath(filename);
            //FileInfo file = new FileInfo(path);
            if (File.Exists(path))//check file exsit or not  
            {
                File.Delete(path);
                //Populate_dlstImages();

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

        }

        protected void btnCancel_slideshow_Click(object sender, EventArgs e)
        {
            initialise_button_status();
            clear_labels();

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
                    cmd.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Villa_id=@Villa_id";
                    cmd.Parameters.AddWithValue("@Villa_id", qs);
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
                            cmd2.CommandText = "SELECT *  from tblImage where Villa_id=@Villa_id and Img_name=@Img_name";
                            cmd2.Parameters.AddWithValue("@Villa_id", qs);
                            //cmd2.Parameters.AddWithValue("@Img_name", filePath);
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
                                
                                return;
                            }

                            else
                            {

                                string folderPath = Server.MapPath("~/Property/" + qs);
                               
                                if (!Directory.Exists(folderPath))
                                {
                                   
                                    Directory.CreateDirectory(folderPath);
                                }


                                btnupload_slideshow.SaveAs(MapPath(filePath));

                                SqlConnection con1 = new SqlConnection(_conString);
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.AddWithValue("@Villa_id", qs);
                            
                                cmd1.Parameters.AddWithValue("@Img_name", btnupload_slideshow.FileName);
                                if (Convert.ToInt32(Session["count_slide_show"]) < 10)
                                {
                                    con1.Open();
                                    cmd1.CommandText = "INSERT INTO tblImage ([Img_name], [Villa_id]) VALUES (@Img_name, @Villa_id)";
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
                                        //Populate_dlstImages();
                                        show_delete_buttons();


                                        if (Convert.ToInt32(Session["count_slide_show"]) == 10)
                                        {
                                          
                                            btnupload_slideshow.Visible = false;
                                            btnSave_slideshow.Visible = false;
                                           
                                            btnEdit_slideshow.Visible = false;
                                            btnCancel_slideshow.Visible = true;
                                          
                                            disable_edit_buttons();
                                            Populate_dlstImages();
                                            show_delete_buttons();
                                            return;


                                        }


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
            }

        }


        //delete slideshow images buttons
        void show_delete_buttons()
        {
            foreach (DataListItem di in dlstImages.Items)
            {
                di.FindControl("lbtndelete_slideshow").Visible = true;
            }
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

        protected void btnCancel_main_pic_Click(object sender, EventArgs e)
        {
            initialise_button_status();
            clear_labels();
        }

        protected void btnEdit_main_pic_Click(object sender, EventArgs e)
        {
            btnSave_main_pic.Visible = true;
            btnEdit_main_pic.Visible = false;
            btnCancel_main_pic.Visible = true;
            btnupload_main_pic.Visible = true;

            disable_edit_buttons();
            //show_delete_buttons();
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
                    cmd.CommandText = "SELECT *  from tblVilla where Villa_id=@Villa_id";
                    cmd.Parameters.AddWithValue("@Villa_id", qs);
                    cmd.Connection = con;
                    SqlDataReader dr;
                    con.Open();
                    dr = cmd.ExecuteReader();

                    if (dr.HasRows)
                    {

                        SqlConnection con1 = new SqlConnection(_conString);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.Text;
                        cmd1.CommandText = "UPDATE tblVilla set Villa_image=@Villa_image where Villa_id=@Villa_id";
                        cmd1.Parameters.AddWithValue("@Villa_id", qs);
                        cmd1.Parameters.AddWithValue("@Villa_image", "");
                        cmd1.Connection = con1;
                        con1.Open();

                        dr.Read();
                    
                        string filename = "~/Property/" + qs + "/main/" + dr["Villa_image"].ToString();



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
                            cmd2.CommandText = "UPDATE tblVilla set Villa_image=@Villa_image where Villa_id=@Villa_id";
                            cmd2.Parameters.AddWithValue("@Villa_id", qs);
                          
                            cmd2.Parameters.AddWithValue("@Villa_image", btnupload_main_pic.FileName);
                            cmd2.Connection = con2;
                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

                            Populate_owner_loc_price();
                            clear_labels();
                            lbl_main_pic.Text = "Main Picture updated";
                            lbl_main_pic.ForeColor = System.Drawing.Color.Green;
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
            }

        }

        protected void Btn_Edit_description_Click(object sender, EventArgs e)
        {
            clear_labels();

            Btn_Save_description.Visible = true;
            Btn_Edit_description.Visible = false;
            Btn_Cancel_description.Visible = true;
            disable_edit_buttons();

            txt_Description.ReadOnly = false;
          

        }

        protected void Btn_Save_description_Click(object sender, EventArgs e)
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
          
            cmd.CommandText = "SELECT * FROM tblVilla where Villa_id=@Villa_id";
            cmd.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.ToString());
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();

            lbl_Prop_Desc.Text = "";

            if (dr.HasRows)
            {
                dr.Read();
                string description_old = dr["Villa_desc"].ToString();
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
                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "update tblVilla set Villa_desc=@Villa_desc where Villa_id=@Villa_id";
                    cmd2.Parameters.AddWithValue("@Villa_id", txtProp_id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Villa_desc", txt_Description.Text.Trim());
                    con2.Open();
                    Boolean IsUpdated = false;
                    IsUpdated = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsUpdated)
                    {
                        lbl_Prop_Desc.Text = "Property Description updated";
                        lbl_Prop_Desc.ForeColor = System.Drawing.Color.Green;

                        Populate_owner_loc_price();
                        initialise_button_status();
                        set_owner_loc_price_readonly();


                    }

                }
            }
            dbcon.Close();
        }

        protected void Btn_Cancel_description_Click(object sender, EventArgs e)
        {
            initialise_button_status();
        }
    }
}