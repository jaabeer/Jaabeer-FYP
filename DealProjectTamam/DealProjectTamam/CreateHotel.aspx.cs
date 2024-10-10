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

namespace DealProjectTamam
{
    public partial class CreateHotel : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                populate_owner();
                get_districts();
                populate_details_dropdown();
                populate_facilities_dropdown();
            }
        }

        void populate_owner()
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblOwner where Own_id=@Own_id";
            cmd.Parameters.AddWithValue("@Own_id", Convert.ToInt32(Session["owner_id"]));
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtOwn_lname.Text = dr["Own_lname"].ToString();
                txtOwn_fname.Text = dr["Own_fname"].ToString();
                txtOwn_lname.ReadOnly = true;
                txtOwn_fname.ReadOnly = true;
            }
            dbcon.Close();
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
            dbcon.Close();
        }

        void getDetails()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDetails td inner join tblDetails_Hotel tdH on tdH.Det_id=td.Det_id where tdH.Hotel_id=@Hotel_id";
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
            cmd.CommandText = "SELECT * from tblFacilities tf inner join tblFacilities_Hotel tfH on tfH.Fac_id=tf.Fac_id where tfH.Hotel_id=@Hotel_id";
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
            dbcon.Close();
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
            dbcon.Close();
        }

        protected void gvs_2_PreRender(object sender, EventArgs e)
        {
            if (gvs_2.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_2.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_2.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void gvs_3_PreRender(object sender, EventArgs e)
        {
            if (gvs_3.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_3.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_3.HeaderRow.TableSection = TableRowSection.TableHeader;
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

        protected void btnSave_Click(object sender, EventArgs e)
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

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                lblResult1.Text = "Hotel Name " + " " + dt1.Rows[0]["Hotel_name"].ToString() + " " + " already exists";
            }

            if (dt2 != null && dt2.Rows.Count > 0)
            {
                lblResult2.Text = "Hotel Phone " + " " + dt2.Rows[0]["Hotel_phone"].ToString() + " " + " already exists";
            }

            if (txtProp_phone.Text.Length != 7)
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

            if ((lblResult1.Text.Length == 0) && (lblResult2.Text.Length == 0) && (lblResult3.Text.Length == 0) &&
                (lblResult4.Text.Length == 0) && (lblResult5.Text.Length == 0) && (lblResult6.Text.Length == 0))
            {
                cmd4.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_email", Session["owner_email"].ToString());
                cmd4.Parameters.AddWithValue("@Hotel_street", txtProp_street.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_town", txtProp_town.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_postcode", txtProp_postcode.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_image", "building.jpg");
                cmd4.Parameters.AddWithValue("@Hotel_price", txtProp_priceday.Text.Trim());
                cmd4.Parameters.AddWithValue("@Hotel_status", "0");
                cmd4.Parameters.AddWithValue("@Hotel_regis_date", DateTime.Now.ToString("MM/dd/yyyy"));
                cmd4.Parameters.AddWithValue("@Hotel_approval_date", "12/31/9999");
                cmd4.Parameters.AddWithValue("@Own_id", Session["owner_id"]);
                cmd4.Parameters.AddWithValue("@Dist_id", ddlDistrict.SelectedValue.ToString());

                con4.Open();
                cmd4.CommandText = "INSERT INTO tblHotel ([Hotel_name], [Hotel_phone], [Hotel_email], [Hotel_street], [Hotel_town], [Hotel_postcode],[Hotel_image], [Hotel_price], [Hotel_status], [Hotel_regis_date], [Hotel_approval_date], [Own_id], [Dist_id]) " +
                "VALUES (@Hotel_name, @Hotel_phone, @Hotel_email, @Hotel_street, @Hotel_town, @Hotel_postcode, @Hotel_image, @Hotel_price, @Hotel_status, @Hotel_regis_date, @Hotel_approval_date, @Own_id, @Dist_id)";

                Boolean IsAdded = cmd4.ExecuteNonQuery() > 0;
                if (IsAdded)
                {
                    // Add the notification after hotel creation
                    AddNotification();

                
                    villa_details_read_only();
                    btnSave.Visible = false;
                    btnCancel.Visible = false;
                    retrieve_property_id();

                    // Description
                    Btn_Save_description.Visible = true;
                    Btn_Cancel_description.Visible = true;

                    // Picture upload 
                    btnupload_main_pic.Visible = true;
                    btnupload_slideshow.Visible = true;
                    Session["count_slide_show"] = 0;
                    btnSave_main_pic.Visible = true;
                    btnSave_slideshow.Visible = true;
                    btnCancel_main_pic.Visible = true;
                    btnCancel_slideshow.Visible = true;

                    // Accessibilities
                    getDetails();
                    btnInsert_Add_info.Visible = true;
                    ddlDet_name.Enabled = true;
                    btnUpdate_Add_info.Visible = false;
                    btnDelete_Add_info.Visible = false;
                    btnCancel_Add_info.Visible = false;

                    // Facilities 
                    getFacilities();
                    BtnInsert_Facility.Visible = true;
                    BtnDelete_Facility.Visible = false;
                    BtnCancel_Facility.Visible = false;

                    // Email 
                    BtnSendMail.Visible = true;

                    Create_directory_with_default_pic();

                    lblResult1.Text = "Hotel created, You can now upload pictures, assign accessibilities and  Room facilities";
                    lblResult1.ForeColor = System.Drawing.Color.Green;
                }
                else
                {
                    lblResult1.Text = "An unexpected error occurred, please try again";
                }

                con4.Close();
            }
        }

        void Create_directory_with_default_pic()
        {
            SqlConnection con1 = new SqlConnection(_conString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;

            cmd1.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
            cmd1.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
            cmd1.Parameters.AddWithValue("@Own_id", Convert.ToInt32(Session["owner_id"]));

            cmd1.CommandText = "SELECT * FROM tblHotel where Hotel_name=@Hotel_name and Hotel_phone=@Hotel_phone and Own_id=@Own_id";
            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            using (da1)
            {
                //Populate the DataTable
                da1.Fill(dt1);
            }

            if (dt1 != null && dt1.Rows.Count > 0)
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
                if (File.Exists(path))
                {
                    File.Delete(path);
                }

                System.IO.File.Copy(Server.MapPath("~/images/building.jpg"), Server.MapPath(filename));
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

        protected void Btn_Save_description_Click(object sender, EventArgs e)
        {
            lbl_Prop_Desc.Text = "";

            if ((txt_Description.Text.ToString()).Length != 0)
            {
                SqlConnection con2 = new SqlConnection(_conString);
                SqlCommand cmd2 = new SqlCommand();
                cmd2.CommandType = CommandType.Text;
                cmd2.Connection = con2;
                cmd2.CommandText = "update tblHotel set Hotel_desc=@Hotel_desc where Hotel_id=@Hotel_id";
                cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
                cmd2.Parameters.AddWithValue("@Hotel_desc", txt_Description.Text.Trim());
                con2.Open();
                Boolean IsUpdated = cmd2.ExecuteNonQuery() > 0;
                con2.Close();
                if (IsUpdated)
                {
                    lbl_Prop_Desc.Text = "Hotel Description Added";
                    lbl_Prop_Desc.ForeColor = System.Drawing.Color.Green;
                    Btn_Save_description.Visible = false;
                    Btn_Cancel_description.Visible = false;
                    txt_Description.ReadOnly = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_description';", true);
                }
            }
            else
            {
                lbl_Prop_Desc.Text = "No Description added";
                lbl_Prop_Desc.ForeColor = System.Drawing.Color.Red;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_description';", true);
            }
        }

        protected void Btn_Cancel_description_Click(object sender, EventArgs e)
        {
            lbl_Prop_Desc.Text = "";
            txt_Description.Text = "";
            Btn_Save_description.Visible = true;
            Btn_Cancel_description.Visible = true;
            txt_Description.ReadOnly = false;
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_description';", true);
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
                    cmd.CommandText = "SELECT * from tblHotel where Hotel_id=@Hotel_id";
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

                        Boolean IsAdded = cmd1.ExecuteNonQuery() > 0;
                        if (IsAdded)
                        {
                            string path = Server.MapPath(filename);
                            if (File.Exists(path))
                            {
                                File.Delete(path);
                            }

                            string folderPath = Server.MapPath("~/Property/" + txtProp_id.Text.Trim() + "/main");
                            if (!Directory.Exists(folderPath))
                            {
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

                            lbl_main_pic.Text = "Main Picture " + btnupload_main_pic.FileName + " added";
                            lbl_main_pic.ForeColor = System.Drawing.Color.Green;

                            btnSave_main_pic.Visible = false;
                            btnCancel_main_pic.Visible = false;
                            btnupload_main_pic.Visible = false;

                            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
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
                    btnCancel_main_pic.Visible = true;
                    btnupload_main_pic.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                }
            }
            else
            {
                lbl_main_pic.Text = "Please upload a picture";
                lbl_main_pic.ForeColor = System.Drawing.Color.Red;
                btnSave_main_pic.Visible = true;
                btnCancel_main_pic.Visible = true;
                btnupload_main_pic.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
            }
        }

        protected void btnCancel_main_pic_Click(object sender, EventArgs e)
        {
            btnupload_slideshow.Dispose();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
        }

        protected void btnSave_slideshow_Click(object sender, EventArgs e)
        {
            if (btnupload_slideshow.FileName != "")
            {
                if (CheckFileType(btnupload_slideshow.FileName))
                {
                    String filePath = "~/Property/" + txtProp_id.Text.ToString() + "/" + btnupload_slideshow.FileName;
                    lbl_slideshow.Text = "";

                    SqlConnection con = new SqlConnection(_conString);
                    SqlCommand cmd = new SqlCommand();
                    cmd.CommandType = CommandType.Text;
                    cmd.CommandText = "SELECT count(Img_id) as num_image_slide_show from tblImage where Hotel_id=@Hotel_id";
                    cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
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
                            cmd2.CommandText = "SELECT * from tblImage where Hotel_id=@Hotel_id and Img_name=@Img_name";
                            cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
                            cmd2.Parameters.AddWithValue("@Img_name", btnupload_slideshow.FileName);
                            cmd2.Connection = con2;
                            SqlDataReader dr2;
                            con2.Open();
                            dr2 = cmd2.ExecuteReader();
                            if (dr2.HasRows)
                            {
                                lbl_slideshow.Text = "Image with same name already exists";
                                lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                            }
                            else
                            {
                                string folderPath = Server.MapPath("~/Property/" + txtProp_id.Text.ToString());
                                if (!Directory.Exists(folderPath))
                                {
                                    Directory.CreateDirectory(folderPath);
                                }

                                btnupload_slideshow.SaveAs(MapPath(filePath));

                                SqlConnection con1 = new SqlConnection(_conString);
                                SqlCommand cmd1 = new SqlCommand();
                                cmd1.Connection = con1;
                                cmd1.CommandType = CommandType.Text;
                                cmd1.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
                                cmd1.Parameters.AddWithValue("@Img_name", btnupload_slideshow.FileName);
                                if (Convert.ToInt32(Session["count_slide_show"]) < 10)
                                {
                                    con1.Open();
                                    cmd1.CommandText = "INSERT INTO tblImage ([Img_name], [Hotel_id]) VALUES (@Img_name, @Hotel_id)";
                                    Boolean IsAdded = cmd1.ExecuteNonQuery() > 0;
                                    if (IsAdded)
                                    {
                                        lbl_slideshow.Visible = true;
                                        int count_slide_show = Convert.ToInt32(Session["count_slide_show"]);
                                        count_slide_show++;
                                        lbl_slideshow.Text = btnupload_slideshow.FileName + " uploaded (Picture " + count_slide_show + ")";
                                        lbl_slideshow.ForeColor = System.Drawing.Color.Green;
                                        Session["count_slide_show"] = count_slide_show;

                                        if (count_slide_show == 10)
                                        {
                                            lbl_slideshow.Visible = true;
                                            btnupload_slideshow.Visible = false;
                                            btnSave_slideshow.Visible = false;
                                            btnCancel_slideshow.Visible = false;
                                        }

                                        ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                                    }
                                    con1.Close();
                                }
                            }
                            con2.Close();
                        }
                        else
                        {
                            lbl_slideshow.Text = "You have already reached maximum pictures";
                            lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                        }
                    }
                    con.Close();
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                }
                else
                {
                    lbl_slideshow.Text = "Wrong format, please upload a picture";
                    lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                    btnSave_slideshow.Visible = true;
                    btnCancel_slideshow.Visible = true;
                    btnupload_slideshow.Visible = true;
                    ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
                }
            }
            else
            {
                lbl_slideshow.Text = "Please upload a picture";
                lbl_slideshow.ForeColor = System.Drawing.Color.Red;
                btnSave_slideshow.Visible = true;
                btnCancel_slideshow.Visible = true;
                btnupload_slideshow.Visible = true;
                ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
            }
        }

        protected void btnCancel_slideshow_Click(object sender, EventArgs e)
        {
            btnupload_slideshow.Dispose();
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_pictures';", true);
        }

        void villa_details_read_only()
        {
            txtProp_name.ReadOnly = true;
            txtProp_email.ReadOnly = true;
            txtProp_phone.ReadOnly = true;
            txtProp_street.ReadOnly = true;
            txtProp_town.ReadOnly = true;
            ddlDistrict.Enabled = false;
            txtProp_priceday.ReadOnly = true;
            txtProp_postcode.ReadOnly = true;
        }

        void retrieve_property_id()
        {
            SqlConnection con1 = new SqlConnection(_conString);
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = con1;

            cmd1.Parameters.AddWithValue("@Hotel_name", txtProp_name.Text.Trim());
            cmd1.Parameters.AddWithValue("@Hotel_phone", txtProp_phone.Text.Trim());
            cmd1.Parameters.AddWithValue("@Own_id", Convert.ToInt32(Session["owner_id"]));

            cmd1.CommandText = "SELECT * FROM tblHotel where Hotel_name=@Hotel_name and Hotel_phone=@Hotel_phone and Own_id=@Own_id";

            SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
            DataTable dt1 = new DataTable();
            using (da1)
            {
                da1.Fill(dt1);
            }

            if (dt1 != null && dt1.Rows.Count > 0)
            {
                txtProp_id.Text = dt1.Rows[0]["Hotel_id"].ToString();
                Session["Hotel_id"] = dt1.Rows[0]["Hotel_id"].ToString();
            }
        }

        protected void btnInsert_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.CommandText = "select * from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
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
                    cmd2.CommandText = "insert into tblDetails_Hotel ([Det_id], [Hotel_id], [Count]) values (@Det_id, @Hotel_id, @Count)";
                    cmd2.Parameters.AddWithValue("@Det_id", ddlDet_name.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Count", txtCount.Text.Trim());

                    con2.Open();
                    Boolean IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " added successfully!";
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
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
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void btnUpdate_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
            String Count_old = "";

            if ((ddlDet_name.SelectedIndex == -1) || (string.IsNullOrWhiteSpace(txtCount.Text)))
            {
                lblMsg_additional_info.Text = "Please select record to update";
                lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
            }
            else
            {
                SqlConnection con0 = new SqlConnection(_conString);
                SqlCommand cmd0 = new SqlCommand();
                cmd0.CommandType = CommandType.Text;
                cmd0.Connection = con0;
                cmd0.CommandText = "select * from tblDetails_Hotel where Det_id=@Det_id and Hotel_id=@Hotel_id";
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
                    cmd2.CommandText = "update tblDetails_Hotel set Count=@Count where Det_id=@Det_id and Hotel_id=@Hotel_id";

                    cmd2.Parameters.AddWithValue("@Det_id", txtDet_Id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
                    cmd2.Parameters.AddWithValue("@Count", txtCount.Text.Trim());

                    con2.Open();
                    Boolean IsUpdated = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsUpdated)
                    {
                        lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " successfully updated!";
                        lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
                        getDetails();
                        ResetAdditional_info();
                        ddlDet_name.Enabled = true;
                    }
                }
            }
            ddlDet_name.Enabled = true;
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void btnCancel_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
            ResetAdditional_info();
            ddlDet_name.Enabled = true;
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

        protected void btnDelete_Add_info_Click(object sender, EventArgs e)
        {
            lblMsg_additional_info.Text = "";
            if ((ddlDet_name.SelectedIndex == -1) || (string.IsNullOrWhiteSpace(txtCount.Text)))
            {
                lblMsg_additional_info.Text = "Please select record to Delete";
                lblMsg_additional_info.ForeColor = System.Drawing.Color.Red;
                ddlDet_name.Enabled = true;
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

                Boolean IsDeleted = cmd2.ExecuteNonQuery() > 0;
                con2.Close();

                if (IsDeleted)
                {
                    lblMsg_additional_info.Text = ddlDet_name.SelectedItem.ToString() + " deleted successfully!";
                    lblMsg_additional_info.ForeColor = System.Drawing.Color.Green;
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
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_accessibilities';", true);
        }

        protected void BtnInsert_Facility_Click(object sender, EventArgs e)
        {
            lblMsg_facilities.Text = "";

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;

            cmd.CommandText = "select * from tblFacilities_Hotel where Hotel_id=@Hotel_id and Fac_id=@Fac_id";
            cmd.Parameters.AddWithValue("@Fac_id", ddl_Facility.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
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
                    cmd2.CommandText = "insert into tblFacilities_Hotel ([Fac_id], [Hotel_id]) values (@Fac_id, @Hotel_id)";
                    cmd2.Parameters.AddWithValue("@Fac_id", ddl_Facility.SelectedValue.ToString());
                    cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());

                    con2.Open();
                    Boolean IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg_facilities.Text = ddl_Facility.SelectedItem.ToString() + " added successfully!";
                        lblMsg_facilities.ForeColor = System.Drawing.Color.Green;
                        getFacilities();
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
            lblMsg_facilities.Text = "";
            ResetFacilities();
            ddl_Facility.Enabled = true;
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

        protected void BtnDelete_Facility_Click(object sender, EventArgs e)
        {
            lblMsg_facilities.Text = "";
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
                cmd2.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
                cmd2.Connection = con2;
                con2.Open();

                Boolean IsDeleted = cmd2.ExecuteNonQuery() > 0;
                con2.Close();

                if (IsDeleted)
                {
                    lblMsg_facilities.Text = ddl_Facility.SelectedItem.ToString() + " deleted successfully!";
                    lblMsg_facilities.ForeColor = System.Drawing.Color.Green;
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
            ClientScript.RegisterStartupScript(this.GetType(), "hash", "location.hash = '#villa_facilities';", true);
        }

        protected void BtnSendMail_Click(object sender, EventArgs e)
        {
            try
            {
                // Add notification before sending email
                AddNotification();

                // Set up message details
                MailMessage message = new MailMessage();
                message.From = new MailAddress("dealtamam02@gmail.com");
                message.To.Add(new MailAddress("dealtamam02@gmail.com")); // Recipient email
                message.Subject = txtProp_name.Text + " Hotel Approval Request";
                message.IsBodyHtml = true;

                // Construct email body
                StringBuilder msgBody = new StringBuilder();
                if (string.IsNullOrWhiteSpace(txtemail.Text))
                {
                    msgBody.Append("Dear Deal Tamam Admin,");
                    msgBody.AppendLine("<br/>Could you please approve my new hotel request?");
                    msgBody.AppendLine("<br/>Thanks.");
                    msgBody.AppendLine("<br/>Regards,");
                    msgBody.AppendLine(txtOwn_fname.Text + " " + txtOwn_lname.Text);
                    msgBody.AppendLine("<br/>Owner Email: " + txtProp_email.Text);
                }
                else
                {
                    msgBody.Append(txtemail.Text);
                    msgBody.AppendLine("<br/>Owner Email: " + txtProp_email.Text);
                }
                message.Body = msgBody.ToString();

                // Set up SMTP client
                SmtpClient client = new SmtpClient("smtp.gmail.com", 587);
                client.UseDefaultCredentials = false; // Important to set this before setting credentials
                client.Credentials = new System.Net.NetworkCredential("dealtamam02@gmail.com", "ovaujhexpehtkhwz");
                client.EnableSsl = true;

                // Send the email
                client.Send(message);
                Response.Redirect("mail_sent.aspx"); // Redirect to success page
            }
            catch (Exception ex)
            {
                Response.Write("Error: " + ex.Message); // Show error message
            }
        }

        bool CheckFileType(string fileName)
        {
            string ext = Path.GetExtension(fileName);
            switch (ext.ToLower())
            {
                case ".gif":
                case ".png":
                case ".jpg":
                case ".jpeg":
                    return true;
                default:
                    return false;
            }
        }

        protected void AddNotification()
        {
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;
            cmd2.Connection = con2;

            cmd2.CommandText = "insert into tblNotification ([Notif_details], [Notif_type], [user_id], [Hotel_id], [Notif_date], [State], [Show]) values (@details, @type, @user, @Hotel, @date, @state, @show)";
            cmd2.Parameters.AddWithValue("@details", "Approval for Villa " + txtProp_name.Text.ToString());
            cmd2.Parameters.AddWithValue("@type", "notif");
            cmd2.Parameters.AddWithValue("@user", Session["owner_id"]);
            cmd2.Parameters.AddWithValue("@Hotel", txtProp_id.Text.ToString());
            cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM/dd/yyyy"));
            cmd2.Parameters.AddWithValue("@state", 0);
            cmd2.Parameters.AddWithValue("@show", 1);

            con2.Open();
            cmd2.ExecuteNonQuery();
            con2.Close();
        }
    }
}
