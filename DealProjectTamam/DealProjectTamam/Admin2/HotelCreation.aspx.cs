using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam.Admin2
{
    public partial class HotelCreation : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            //if ((Session["admin_email"] == null))
            //{
            //    Response.Redirect("~/Admin_login.aspx");
            //}
            if (!Page.IsPostBack)
            {
                getOwners();
                get_districts();
                populate_details_dropdown();
                populate_facilities_dropdown();
               

            }

        }
        void getOwners()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblOwner";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            gvs.DataSource = dt;
            gvs.DataBind();
        }

        void getDetails()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblDetails td INNER JOIN tblHotel th ON th.Hotel_id = td.Det_id WHERE th.Hotel_id = @Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
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

        void getFacilities()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT trf.Rooms_Fac_id AS Fac_id, trf.Rooms_Fac_name, tfh.Hotel_id FROM tbl_Rooms_Facilities trf INNER JOIN tblRoomFacilities_Hotel tfh ON tfh.Rooms_Fac_id = trf.Rooms_Fac_id WHERE tfh.Hotel_id = @Hotel_id";

            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
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
            cmd.CommandText = "SELECT * from tbl_Rooms_Facilities";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Rooms_Fac_name"];
                newItem.Value = dr["Rooms_Fac_id"].ToString();
                ddl_Facility.Items.Add(newItem);
            }
        }
        protected void gvs_PreRender(object sender, EventArgs e)
        {
            if (gvs.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs.HeaderRow.TableSection =
                TableRowSection.TableHeader;
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

        protected void gvs_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Read data from GridView and Populate the form
            txtOwn_id.Text = (gvs.DataKeys[gvs.SelectedIndex].Value.ToString());
            txtOwn_fname.Text = ((Label)gvs.SelectedRow.FindControl("lblLastName")).Text;
            txtOwn_lname.Text = ((Label)gvs.SelectedRow.FindControl("lblFirstName")).Text;
            txtOwn_email.Text = ((Label)gvs.SelectedRow.FindControl("lblOwn_email")).Text;
            txtProp_email.Text = ((Label)gvs.SelectedRow.FindControl("lblOwn_email")).Text;

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

        }

        protected void gvs_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_2.PageIndex = e.NewPageIndex;
            getDetails();
        }

        protected void gvs_3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_3.PageIndex = e.NewPageIndex;
            getFacilities();
        }

        protected void btnSelect_Click(object sender, EventArgs e)
        {
            txtOwn_id.ReadOnly = true;
            txtOwn_email.ReadOnly = true;

            txtOwn_lname.ReadOnly = true;
            txtOwn_fname.ReadOnly = true;
            txtProp_email.ReadOnly = true;
            Accord_create_villa.SelectedIndex = 1;
         


        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            //Add the following codes in a TextChanged event
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

            cmd1.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
            cmd2.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());

            cmd1.CommandText = "SELECT * FROM tblHotel where Hotel_name=@Hotel_name";
            cmd2.CommandText = "SELECT * FROM tblHotel where Hotel_phone=@Hotel_phone";

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

            lblResult1.Text = "";
            lblResult2.Text = "";
            lblResult3.Text = "";
            lblResult4.Text = "";
            lblResult5.Text = "";
            lblResult6.Text = "";

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
                    lblResult2.Text = "Hotel Contact " + " " + dt2.Rows[0]["Hotel_phone"].ToString() + " " + " already exists";

                }
            }


            if (txtProp_phone.Text.Length != 8)
            {
                lblResult4.Text = "Fixed Phone Number should contain 8 digits";
            }
            else
            {
                if (txtProp_phone.Text.Trim().Length > 0)
                {
                    string first_char = txtProp_phone.Text.Trim().Substring(0, 1);

                    if (first_char == "0")
                    {
                        if (!string.IsNullOrEmpty(lblResult4.Text))
                        {
                            lblResult4.Text = lblResult4.Text + " and cannot start with 0";
                        }
                        else
                        {
                            lblResult4.Text = "Fixed Phone Number cannot start with 0";
                        }
                    }
                }
            }


            if ((txtProp_postcode.Text.Length != 0) && (txtProp_postcode.Text.Length != 5))
            {
                lblResult5.Text = "Postal code should contain 5 numbers";
            }



            if ((lblResult1.Text.Length == 0) && (lblResult2.Text.Length == 0) && (lblResult3.Text.Length == 0) &&
                (lblResult4.Text.Length == 0) && (lblResult5.Text.Length == 0) && (lblResult6.Text.Length == 0))
            {
                cmd4.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_email", txtProp_email.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_street", txtProp_street.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_town", txtProp_town.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_postcode", txtProp_postcode.Text.Trim());
              
                cmd4.Parameters.AddWithValue("@Hotel_image", "building.jpg");
                cmd4.Parameters.AddWithValue("@Hotel_price", txtProp_priceday.Text.Trim());

                cmd4.Parameters.AddWithValue("@Hotel_status", "0");


                cmd4.Parameters.AddWithValue("@Hotel_regis_date", DateTime.Now.ToString("MM/dd/yyyy"));
              
                cmd4.Parameters.AddWithValue("@Hotel_approval_date", ("12/31/9999"));
                cmd4.Parameters.AddWithValue("@Own_id", txtOwn_id.Text.Trim());


                cmd4.Parameters.AddWithValue("@Dist_id", ddlDistrict.SelectedValue.ToString());

                con4.Open();
                cmd4.CommandText = "INSERT INTO tblHotel ([Hotel_name], [Hotel_phone], [Hotel_email], [Hotel_street], [Hotel_town], [Hotel_postcode],[Hotel_image], [Hotel_price], [Hotel_status], [Hotel_regis_date], [Hotel_approval_date], [Own_id], [Dist_id]) " +
                "VALUES (@Hotel_name, @Hotel_phone, @Hotel_email, @Hotel_street, @Hotel_town, @Hotel_postcode, @Hotel_image, @Hotel_price,  @Hotel_status, @Hotel_regis_date, @Hotel_approval_date, @Own_id, @Dist_id)";

                Boolean IsAdded = false;
                IsAdded = cmd4.ExecuteNonQuery() > 0;
                if (IsAdded)
                {
                    //villa details accordion
                    villa_details_read_only();
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    retrieve_property_id();

                    //picture upload accordion
                    btnupload_main_pic.Visible = true;
                    btnSave_main_pic.Visible = true;
                    btnCancel_main_pic.Visible = true;

                    btnUpload_slideshow.Visible = true;
                    BtnSave_slideshow.Visible = true;
                    BtnCancel_slideshow.Visible = true;
                    Session["count_slide_show"] = 0;
                    //count_slide_show = 0;

                    Accord_create_villa.SelectedIndex = 2;

                    //Details (particulars of villas) accordion
                    getDetails();
                    btnInsert_Add_info.Visible = true;
                    ddlDet_name.Enabled = true;
                    btnUpdate_Add_info.Visible = false;
                    btnDelete_Add_info.Visible = false;
                    btnCancel_Add_info.Visible = false;

                    //facilities accordion
                    getFacilities();
                    BtnInsert_Facility.Visible = true;
                    BtnDelete_Facility.Visible = false;
                    BtnCancel_Facility.Visible = false;

                    //Email accordion
                    BtnSendMail.Visible = true;

                    Create_directory_with_default_pic();

                }
                else
                {
                    lblResult1.Text = "An unexpected error occured, please try again";
                }

                con4.Close();
            }

        }

        void villa_details_read_only()
        {

            txtProp_name.ReadOnly = true;
            txtProp_email.ReadOnly = true;
            txtProp_phone.ReadOnly = true;
            txtProp_street.ReadOnly = true;
            txtProp_town.ReadOnly = true;
            ddlDistrict.Enabled = false; ;
            txtProp_priceday.ReadOnly = true;

            txtProp_postcode.ReadOnly = true;
            ddlDistrict.CssClass = "form-control form-control-user";
        }

        void retrieve_property_id()
        {
            SqlConnection con1 = new SqlConnection(_conString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;

            cmd1.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
            cmd1.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
            cmd1.Parameters.AddWithValue("@Own_id", txtOwn_id.Text.Trim());

            cmd1.CommandText = "SELECT * FROM tblHotel where Hotel_name=@Hotel_name and Hotel_phone=@Hotel_phone and Own_id=@Own_id";

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

                    txtProp_id.Text = dt1.Rows[0]["Hotel_id"].ToString();

                }

            }
        }

        void Create_directory_with_default_pic()
        {
            SqlConnection con1 = new SqlConnection(_conString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;

            cmd1.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
            cmd1.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
            cmd1.Parameters.AddWithValue("@Own_id", txtOwn_id.Text.Trim());

            cmd1.CommandText = "SELECT * FROM tblHotel where Hotel_name=@Hotel_name and Hotel_phone=@Hotel_phone and Own_id=@Own_id";
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

                    Session["Hotel_id"] = dt1.Rows[0]["Hotel_id"].ToString();

                    string folderPath = Server.MapPath("~/Property/" + dt1.Rows[0]["Hotel_id"].ToString() + "/main");
                    //Check whether Directory (Folder) exists.
                    if (!Directory.Exists(folderPath))
                    {
                        //If Directory (Folder) does not exists. Create it.
                        Directory.CreateDirectory(folderPath);
                    }

                    string filename = "~/Property/" + dt1.Rows[0]["Hotel_id"].ToString() + "/main/building.jpg";

                
                    string path = Server.MapPath(filename);
                    //FileInfo file = new FileInfo(path);
                    if (File.Exists(path))//check file exsit or not  
                    {
                        File.Delete(path);


                    }

                    System.IO.File.Copy(Server.MapPath("~/images/building.jpg"), Server.MapPath(filename));

                }

            }
        }






        protected void btnCancel_Click(object sender, EventArgs e)
        {
            txtProp_name.Text = "";
           
            txtProp_street.Text = "";
            txtProp_town.Text = "";
            ddlDistrict.SelectedIndex = -1;
            txtProp_priceday.Text = "";

            txtProp_postcode.Text = "";
         
        }

        protected void btnSave_main_pic_Click(object sender, EventArgs e)
        {
            if (btnupload_main_pic.FileName != "")
            {
                if (CheckFileType(btnupload_main_pic.FileName))
                {
                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT *  from tblHotel where Hotel_id=@Hotel_id";
                    cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
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
                        cmd1.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
                        cmd1.Parameters.AddWithValue("@Hotel_image", "");
                        cmd1.Connection = con1;
                        con1.Open();

                        dr.Read();
                       
                        string filename = "~/Property/" + txtProp_id.Text.ToString() + "/main/" + dr["Hotel_image"].ToString();


                        Boolean IsAdded = false;
                        IsAdded = cmd1.ExecuteNonQuery() > 0;
                        if (IsAdded)
                        {

                            string path = Server.MapPath(filename);
                            //FileInfo file = new FileInfo(path);
                            if (File.Exists(path))//check file exsit or not  
                            {
                                File.Delete(path);


                            }


                            string folderPath = Server.MapPath("~/Property/" + txtProp_id.Text.Trim() + "/main");
                            //Check whether Directory (Folder) exists.
                            if (!Directory.Exists(folderPath))
                            {
                                //If Directory (Folder) does not exists. Create it.
                                Directory.CreateDirectory(folderPath);
                            }


                            String filePath = "~/Property/" + txtProp_id.Text.Trim() + "/main/" + btnupload_main_pic.FileName;
                            btnupload_main_pic.SaveAs(MapPath(filePath));

                            SqlConnection con2 = new SqlConnection(_conString);
                            SqlCommand cmd2 = new SqlCommand();
                            cmd2.CommandType = CommandType.Text;
                            cmd2.CommandText = "UPDATE tblHotel set Hotel_image=@Hotel_image where Hotel_id=@Hotel_id";
                            cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
                          
                            cmd2.Parameters.AddWithValue("@Hotel_image", btnupload_main_pic.FileName);
                            cmd2.Connection = con2;
                            con2.Open();
                            cmd2.ExecuteNonQuery();
                            con2.Close();

                            lbl_main_picture.Visible = true;
                            lbl_main_picture.Text = "Main Picture " + btnupload_main_pic.FileName + " added";
                            lbl_main_picture.ForeColor = System.Drawing.Color.Green;

                           
                            btnSave_main_pic.Visible = false;
                            btnCancel_main_pic.Visible = false;

                        }
                        con1.Close();
                    }
                    con.Close();
                }
                else
                {
                    lbl_main_picture.Visible = true;
                    lbl_main_picture.Text = "Wrong format, please upload a picture";
                    lbl_main_picture.ForeColor = System.Drawing.Color.Red;
                    btnSave_main_pic.Visible = true;
                    btnCancel_main_pic.Visible = true;
                    btnupload_main_pic.Visible = true;
                }


            }

            else
            {
                lbl_main_picture.Visible = true;
                lbl_main_picture.Text = "Please upload a picture";
                lbl_main_picture.ForeColor = System.Drawing.Color.Red;
                btnSave_main_pic.Visible = true;
                btnCancel_main_pic.Visible = true;
                btnupload_main_pic.Visible = true;
            }
        }

        protected void btnCancel_main_pic_Click(object sender, EventArgs e)
        {
            btnupload_main_pic.Dispose();
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

        protected void BtnSave_slideshow_Click(object sender, EventArgs e)
        {
            if (btnUpload_slideshow.FileName != "")
            {

                if (CheckFileType(btnUpload_slideshow.FileName))
                {
                    String filePath = "~/Property/" + txtProp_id.Text.ToString() + "/" + btnUpload_slideshow.FileName;
                    lbl_slideshow.Text = "";

                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT count(Img_id) as num_image_slide_show  from tblImage where Hotel_id=@Hotel_id";
                    cmd.Parameters.AddWithValue("@hotel_id", txtProp_id.Text.ToString());
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
                            cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
                           
                            cmd2.Parameters.AddWithValue("@Img_name", btnUpload_slideshow.FileName);
                            cmd2.Connection = con2;
                            SqlDataReader dr2;
                            con2.Open();

                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                lbl_slideshow.Visible = true;
                                lbl_slideshow.Text = "Image with same name already exists";
                                lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                            }

                            else
                            {

                                string folderPath = Server.MapPath("~/Property/" + txtProp_id.Text.ToString());
                                //Check whether Directory (Folder) exists.
                                if (!Directory.Exists(folderPath))
                                {
                                    //If Directory (Folder) does not exists. Create it.
                                    Directory.CreateDirectory(folderPath);
                                }


                                btnUpload_slideshow.SaveAs(MapPath(filePath));

                                SqlConnection con1 = new SqlConnection(_conString);
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
                               
                                cmd1.Parameters.AddWithValue("@Img_name", btnUpload_slideshow.FileName);
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
                                        lbl_slideshow.Text = btnUpload_slideshow.FileName + " " + "uploaded" + " " + "(Picture" + " " + count_slide_show + ")";
                                        lbl_slideshow.ForeColor = System.Drawing.Color.Green;

                                        Session["count_slide_show"] = Convert.ToInt32(Session["count_slide_show"]) + 1;

                                        if (Convert.ToInt32(Session["count_slide_show"]) == 10)
                                        {
                                            lbl_slideshow.Visible = true;
                                            btnUpload_slideshow.Visible = false;
                                            BtnSave_slideshow.Visible = false;
                                            BtnCancel_slideshow.Visible = false;
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
                        }

                    }
                    con.Close();
                }

                else
                {
                    lbl_slideshow.Text = "Wrong format, please upload a picture";
                    lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                    BtnSave_slideshow.Visible = true;
                    BtnCancel_slideshow.Visible = true;
                    btnUpload_slideshow.Visible = true;
                }

            }
            else
            {
                lbl_slideshow.Text = "Please upload a picture";
                lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                BtnSave_slideshow.Visible = true;
                BtnCancel_slideshow.Visible = true;
                btnUpload_slideshow.Visible = true;
            }
        }

        protected void BtnCancel_slideshow_Click(object sender, EventArgs e)
        {
            btnUpload_slideshow.Dispose();
        }

        protected void BtnNexstep_Click(object sender, EventArgs e)
        {
            Accord_create_villa.SelectedIndex = 3;
        }

        protected void btnInsert_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";

            //Add built-in function to remove spaces from Textbox Category name
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

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
                    ResetAdditional_info();
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
                    cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
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
                    }
                    else
                    {
                        lblMsg_additional_info.Text = "Error while adding " + ddlDet_name.SelectedItem.ToString();
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                    }
                    ResetAdditional_info();
                }
            }
        }

        protected void btnUpdate_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
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
                cmd0.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

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
                    ResetAdditional_info();
                    ddlDet_name.Enabled = true;

                }

                else
                {

                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;
                    cmd2.Connection = con2;
                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "update tblDetails_Hotel set Count=@Count where Det_id=@Det_id and Hotel_id=@Hotel_id";

                    cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Count", txtCount.Text.Trim());

                    con2.Open();
                    Boolean IsUpdated = false;
                    IsUpdated = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsUpdated)
                    {
                        lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " successfully updated!";
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindCategoryData()
                        getDetails();
                        ResetAdditional_info();
                        ddlDet_name.Enabled = true;
                    }


                }


            }
            ddlDet_name.Enabled = true;
        }

        protected void btnDelete_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
            if ((ddlDet_name.SelectedIndex == -1) || (string.IsNullOrWhiteSpace(txtCount.Text)))
            {
                lblMsg_additional_info.Text = "Please select record to Delete";
                lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                //return;
            }
            else
            {


                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "delete from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
                cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
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

        protected void btnCancel_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
            ResetAdditional_info();
            ddlDet_name.Enabled = true;
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

        protected void BtnNexstep_Detail_Click(object sender, EventArgs e)
        {
            Accord_create_villa.SelectedIndex = 4;
        }

        protected void BtnInsert_Facility_Click(object sender, EventArgs e)
        {
            lblMsg_facilities.Text = "";

            //Add built-in function to remove spaces from Textbox Category name
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblRoomFacilities_Hotel where Hotel_id=@Hotel_id and Rooms_Fac_id=@Rooms_Fac_id";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@Rooms_Fac_id", ddl_Facility.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMsg_facilities.Text = "Facility " + ddl_Facility.SelectedItem.ToString() + " already assigned!";
                    lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                    ResetFacilities();
                }
                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    cmd2.CommandText = "insert into tblRoomFacilities_Hotel ([Rooms_Fac_id], [Hotel_id]) values (@Rooms_Fac_id, @Hotel_id)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@Rooms_Fac_id", ddl_Facility.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg_facilities.Text = ddl_Facility.SelectedItem.ToString() + " added successfully!";
                        lblMsg_facilities.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindCategoryData()
                        getFacilities();
                     
                        ResetFacilities();
                        ddl_Facility.Enabled = true;
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

        protected void BtnCancel_Facility_Click(object sender, EventArgs e)
        {
            lblMsg_facilities.Text = "";
            ResetFacilities();
            ddl_Facility.Enabled = true;
        }

        void ResetFacilities()
        {
            BtnInsert_Facility.Visible = true;
            BtnDelete_Facility.Visible = false;
            BtnCancel_Facility.Visible = false;
            txt_Facility_ID.Text = "";
            ddl_Facility.SelectedIndex = -1;
        }

        protected void BtnDelete_Facility_Click(object sender, EventArgs e)
        {
            lblMsg_facilities.Text = "";
            if (ddl_Facility.SelectedIndex == -1)
            {
                lblMsg_facilities.Text = "Please select record to Delete";
                lblMsg_facilities.ForeColor = System.Drawing.Color.Red;
                ddl_Facility.Enabled = true;
                //return;
            }
            else
            {


                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;

                cmd2.CommandText = "delete from tblRoomFacilities_Hotel where Rooms_Fac_id=@Rooms_Fac_id and Hotel_id=@Hotel_id";
                cmd2.Parameters.AddWithValue("@Rooms_Fac_id", txt_Facility_ID.Text.Trim());
                cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
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
            ddl_Facility.Enabled = true;
        }

       

        protected void BtnNextstep_facility_Click(object sender, EventArgs e)
        {
            Accord_create_villa.SelectedIndex = 5;
        }

        protected void BtnSendMail_Click(object sender, EventArgs e)
        {
            SendEmail();


        }

        private void SendEmail()
        {
            // Create MailMessage and SmtpClient objects
            MailMessage mail = new MailMessage();
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("dealtamam02@gmail.com", "ovaujhexpehtkhwz"),
                EnableSsl = true
            };

            try
            {
                // Dynamically set the recipient based on the owner's email
                string recipientEmail = txtOwn_email.Text;
                mail.From = new MailAddress("dealtamam02@gmail.com");
                mail.To.Add(new MailAddress(recipientEmail));
                mail.Subject = txtProp_name.Text + " Hotel Registration";

                // Set HTML body with dynamic content
                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("Hotel Registration Confirmation<br>");
                msgBody.Append("Dear " + txtOwn_fname.Text + ",<br><br>");
                msgBody.Append("Your Hotel, " + txtProp_name.Text + ", has been successfully registered with us.<br>");
                msgBody.Append("We look forward to a successful partnership.<br><br>");
                msgBody.Append("Best Regards,<br>Your Real Estate Team");

                mail.Body = msgBody.ToString();
                mail.IsBodyHtml = true; 
                // Send the email
                sc.Send(mail);
            }
            catch (Exception ex)
            {
                Response.Write("Failed to send email. Error: " + ex.Message);
                throw; // Rethrow the exception to handle it in the calling method
            }
        }

        protected void Button2_Click(object sender, EventArgs e)
        {
            try
            {
                SendEmail();
                Response.Write("Email sent successfully.<br>");
                Response.Redirect("~/mail_sent.aspx", false);
                HttpContext.Current.ApplicationInstance.CompleteRequest();
            }
            catch (Exception ex)
            {
                Response.Write("Failed to send email. Error: " + ex.Message);
            }
        }


    }
}
