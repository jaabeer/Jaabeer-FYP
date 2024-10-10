using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam.AdminS
{
    public partial class AdminDistrict : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryData();
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnCancel.Visible = false;
            }
        }
        private void BindCategoryData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblDistrict";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataTable
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
            }
            //Set the DataTable as the DataSource
            gvs.DataSource = dt;
            gvs.DataBind();
        }
        protected void gvs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs.PageIndex = e.NewPageIndex;
            BindCategoryData();
        }
        protected void gvs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Read data from GridView and Populate the form
            txtDist_id.Text = (gvs.DataKeys[gvs.SelectedIndex].Value.ToString());
            txtDist_name.Text = ((Label)gvs.SelectedRow.FindControl("lblDistName")).Text;

            ddlDistrict_reg.Items.Insert(0, ((Label)gvs.SelectedRow.FindControl("lblDistReg")).Text);
            ddlDistrict_reg.SelectedIndex = 0;


            //Hide Insert button during update/delete
            btnInsert.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            btnCancel.Visible = true;
            lblMsg.Text = "";

            
        }
        private void ResetAll()
        {
            btnInsert.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            txtDist_id.Text = "";
            txtDist_name.Text = "";
            ddlDistrict_reg.SelectedValue = "-1";

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            //Add built-in function to remove spaces from Textbox Category name
            String Dist_name = txtDist_name.Text;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblDistrict where Dist_name=@Dist_name";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@Dist_name", Dist_name);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "District " + Dist_name + " already exists!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ResetAll();
                }
                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "insert into tblDistrict ([Dist_name], [Dist_region]) values (@Dist_name, @Dist_region)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@Dist_name", Dist_name);
                    cmd2.Parameters.AddWithValue("@Dist_region", ddlDistrict_reg.SelectedItem.ToString());
                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg.Text = Dist_name + " District added successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindCategoryData()
                        BindCategoryData();
                    }
                    else
                    {
                        lblMsg.Text = "Error while adding " + Dist_name + " District";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    ResetAll();
                }
            }
        }
        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            String Dist_name_old = "";
            String Dist_region_old = "";
            String Dist_name = txtDist_name.Text.Trim();
            int Dist_id = Convert.ToInt32(txtDist_id.Text);
            if (string.IsNullOrWhiteSpace(txtDist_id.Text))
            {
                lblMsg.Text = "Please select record to update";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                SqlConnection con0 = new SqlConnection(_conString);
                SqlCommand cmd0 = new SqlCommand();
                cmd0.CommandType = CommandType.Text;

                cmd0.Connection = con0;
                cmd0.CommandText = "select * from tblDistrict where Dist_id=@Dist_id";
                //Create a parametererized query for CatID
                cmd0.Parameters.AddWithValue("@Dist_id", Dist_id);

                SqlDataAdapter da0 = new SqlDataAdapter(cmd0);
                DataTable dt0 = new DataTable();



                using (da0)
                {
                    da0.Fill(dt0);
                    if (dt0.Rows.Count > 0)
                    {
                        Dist_name_old = dt0.Rows[0]["Dist_name"].ToString();
                        Dist_region_old = dt0.Rows[0]["Dist_region"].ToString();
                    }

                }

                if ((txtDist_name.Text == Dist_name_old) && (ddlDistrict_reg.SelectedItem.ToString() == Dist_region_old))
                {
                    lblMsg.Text = "No change detected";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ResetAll();
                    return;

                }

                else
                {
                    if ((txtDist_name.Text == Dist_name_old) && (ddlDistrict_reg.SelectedItem.ToString() != Dist_region_old))
                    {
                        SqlConnection con2 = new SqlConnection(_conString);
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.CommandType = CommandType.Text;
                        cmd2.Connection = con2;
                        //Add DELETE statement to delete the selected category for the above CatID
                        cmd2.CommandText = "update tblDistrict set Dist_region=@Dist_reg where Dist_id=@Dist_id";

                        cmd2.Parameters.AddWithValue("@Dist_id", Dist_id);
                        cmd2.Parameters.AddWithValue("@Dist_reg", ddlDistrict_reg.SelectedItem.ToString());

                        con2.Open();
                        Boolean IsUpdated = false;
                        IsUpdated = cmd2.ExecuteNonQuery() > 0;
                        con2.Close();
                        if (IsUpdated)
                        {
                            lblMsg.Text = Dist_name + " District successfully updated!";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            //Refresh the GridView by calling the BindCategoryData()
                            BindCategoryData();
                            ResetAll();
                            return;
                        }
                        else
                        {
                            lblMsg.Text = "Error while adding " + Dist_name + " District";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                            ResetAll();
                            return;
                        }


                    }

                    if (((txtDist_name.Text != Dist_name_old) && (ddlDistrict_reg.SelectedItem.ToString() == Dist_region_old)) || ((txtDist_name.Text != Dist_name_old) && (ddlDistrict_reg.SelectedItem.ToString() != Dist_region_old)))
                    {
                        SqlConnection con3 = new SqlConnection(_conString);
                        SqlCommand cmd3 = new SqlCommand();
                        cmd3.CommandType = CommandType.Text;
                        cmd3.Connection = con3;
                        //Add DELETE statement to delete the selected category for the above CatID
                        cmd3.CommandText = "select * from tblDistrict where Dist_name=@Dist_name";

                        cmd3.Parameters.AddWithValue("@Dist_name", Dist_name);

                        SqlDataAdapter da = new SqlDataAdapter(cmd3);
                        DataTable dt = new DataTable();
                        using (da)
                        {
                            //Populate the DataTable
                            da.Fill(dt);
                            if (dt.Rows.Count > 0)
                            {
                                lblMsg.Text = "District " + Dist_name + " already exists!";
                                //lblMsg.Text = Cat_name_old + "" + Cat_Desc_old;
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                ResetAll();
                                return;

                            }

                            else
                            {
                                SqlConnection con4 = new SqlConnection(_conString);
                                SqlCommand cmd4 = new SqlCommand();
                                cmd4.CommandType = CommandType.Text;
                                cmd4.Connection = con4;
                                //Add DELETE statement to delete the selected category for the above CatID
                                cmd4.CommandText = "update tblDistrict set Dist_name=@Dist_name, Dist_region=@Dist_region where Dist_id=@Dist_id";

                                cmd4.Parameters.AddWithValue("@Dist_id", Dist_id);
                                cmd4.Parameters.AddWithValue("@Dist_name", Dist_name);
                                cmd4.Parameters.AddWithValue("@Dist_region", ddlDistrict_reg.SelectedItem.ToString());

                                con4.Open();
                                Boolean IsUpdated = false;
                                IsUpdated = cmd4.ExecuteNonQuery() > 0;
                                con4.Close();
                                if (IsUpdated)
                                {
                                    lblMsg.Text = Dist_name + " District successfully updated!";
                                    lblMsg.ForeColor = System.Drawing.Color.Green;
                                    //Refresh the GridView by calling the BindCategoryData()
                                    BindCategoryData();
                                    ResetAll();
                                    return;
                                }
                                else
                                {
                                    lblMsg.Text = "Error while adding " + Dist_name + " District";
                                    lblMsg.ForeColor = System.Drawing.Color.Red;
                                    ResetAll();
                                    return;
                                }
                            }

                        }


                    }

                }


            }
        }
        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (string.IsNullOrWhiteSpace(txtDist_id.Text))
            {
                lblMsg.Text = "Please select record to delete";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                Boolean IsDeleted = false;
                int Dist_id = Convert.ToInt32(txtDist_id.Text);
                //Add built-in function to remove spaces from Textbox Category name
                String Dist_name = txtDist_name.Text;
                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;

                cmd.Connection = con;

                //Add DELETE statement to delete the selected category for the above CatID
                cmd.CommandText = "select * from tblVilla where Dist_id=@Dist_id";
                //Create a parametererized query for CatID
                cmd.Parameters.AddWithValue("@Dist_id", Dist_id);

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                using (da)
                {
                    //Populate the DataTable
                    da.Fill(dt);
                    if (dt.Rows.Count > 0)
                    {
                        lblMsg.Text = "Cannot delete District " + Dist_name + ", it is referenced in other tables!";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }

                    else
                    {
                        SqlConnection con2 = new SqlConnection(_conString);
                        SqlCommand cmd2 = new SqlCommand();
                        cmd2.CommandType = CommandType.Text;

                        cmd2.CommandText = "delete from tblDistrict where Dist_id=@Dist_id";
                        cmd2.Parameters.AddWithValue("@Dist_id", Dist_id);
                        cmd2.Connection = con2;
                        con2.Open();

                        IsDeleted = cmd2.ExecuteNonQuery() > 0;
                        con2.Close();

                        if (IsDeleted)
                        {
                            lblMsg.Text = Dist_name + " District deleted successfully!";
                            lblMsg.ForeColor = System.Drawing.Color.Green;
                            //Refresh the GridView by calling the BindCategoryData()
                            BindCategoryData();
                        }
                        else
                        {
                            lblMsg.Text = "Error while deleting " + Dist_name + " District";
                            lblMsg.ForeColor = System.Drawing.Color.Red;
                        }
                        ResetAll();
                    }
                }




            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            ResetAll();
        }

    }
}
