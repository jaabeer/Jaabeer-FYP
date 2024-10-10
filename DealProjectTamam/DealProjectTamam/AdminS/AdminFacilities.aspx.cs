using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Configuration;

namespace DealProjectTamam.AdminS
{
    public partial class AdminFacilities : System.Web.UI.Page
    {
        private string _conString = ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
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
            cmd.CommandText = "SELECT * FROM tblFacilities";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            // Create a DataTable
            DataTable dt = new DataTable();
            using (da)
            {
                // Populate the DataTable
                da.Fill(dt);
            }
            // Set the DataTable as the DataSource
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
            // Read data from GridView and Populate the form
            txtFac_id.Text = (gvs.DataKeys[gvs.SelectedIndex].Value.ToString());
            txtFac_name.Text = ((Label)gvs.SelectedRow.FindControl("lblFacName")).Text;

            // Hide Insert button during update/delete
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
            txtFac_id.Text = "";
            txtFac_name.Text = "";
        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";

            // Add built-in function to remove spaces from Textbox Category name
            String Fac_name = txtFac_name.Text;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            // Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblFacilities where Fac_name=@Fac_name";
            // Create a parameterized query for CatID
            cmd.Parameters.AddWithValue("@Fac_name", Fac_name);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                // Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "Facility " + Fac_name + " already exists!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ResetAll();
                }
                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    // Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "insert into tblFacilities ([Fac_name]) values (@Fac_name)";
                    // Create a parameterized query for CatID
                    cmd2.Parameters.AddWithValue("@Fac_name", Fac_name);
                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg.Text = Fac_name + " Facility added successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        // Refresh the GridView by calling the BindCategoryData()
                        BindCategoryData();
                    }
                    else
                    {
                        lblMsg.Text = "Error while adding " + Fac_name + " Facility";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    ResetAll();
                }
            }
        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            String Fac_name_old = "";
            String Fac_name = txtFac_name.Text.Trim();
            int Fac_id = Convert.ToInt32(txtFac_id.Text);
            if (string.IsNullOrWhiteSpace(txtFac_id.Text))
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
                cmd0.CommandText = "select * from tblFacilities where Fac_id=@Fac_id";
                // Create a parameterized query for CatID
                cmd0.Parameters.AddWithValue("@Fac_id", Fac_id);

                SqlDataAdapter da0 = new SqlDataAdapter(cmd0);
                DataTable dt0 = new DataTable();

                using (da0)
                {
                    da0.Fill(dt0);
                    if (dt0.Rows.Count > 0)
                    {
                        Fac_name_old = dt0.Rows[0]["Fac_name"].ToString();
                    }
                }

                if (txtFac_name.Text == Fac_name_old)
                {
                    lblMsg.Text = "No change detected";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ResetAll();
                    return;
                }
                else
                {
                    SqlConnection con3 = new SqlConnection(_conString);
                    SqlCommand cmd3 = new SqlCommand();
                    cmd3.CommandType = CommandType.Text;
                    cmd3.Connection = con3;
                    // Add DELETE statement to delete the selected category for the above CatID
                    cmd3.CommandText = "select * from tblFacilities where Fac_name=@Fac_name";

                    cmd3.Parameters.AddWithValue("@Fac_name", Fac_name);

                    SqlDataAdapter da = new SqlDataAdapter(cmd3);
                    DataTable dt = new DataTable();
                    using (da)
                    {
                        // Populate the DataTable
                        da.Fill(dt);
                        if (dt.Rows.Count > 0)
                        {
                            lblMsg.Text = "Facility " + Fac_name + " already exists!";
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
                            // Add DELETE statement to delete the selected category for the above CatID
                            cmd4.CommandText = "update tblFacilities set Fac_name=@Fac_name where Fac_id=@Fac_id";

                            cmd4.Parameters.AddWithValue("@Fac_name", Fac_name);
                            cmd4.Parameters.AddWithValue("@Fac_id", Fac_id);

                            con4.Open();
                            Boolean IsUpdated = false;
                            IsUpdated = cmd4.ExecuteNonQuery() > 0;
                            con4.Close();
                            if (IsUpdated)
                            {
                                lblMsg.Text = Fac_name + " Facility successfully updated!";
                                lblMsg.ForeColor = System.Drawing.Color.Green;
                                // Refresh the GridView by calling the BindCategoryData()
                                BindCategoryData();
                                ResetAll();
                                return;
                            }
                            else
                            {
                                lblMsg.Text = "Error while updating " + Fac_name + " Facility";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                ResetAll();
                                return;
                            }
                        }
                    }
                }
            }
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            if (string.IsNullOrWhiteSpace(txtFac_id.Text))
            {
                lblMsg.Text = "Please select record to delete";
                lblMsg.ForeColor = System.Drawing.Color.Red;
                return;
            }
            else
            {
                Boolean IsDeleted = false;
                int Fac_id = Convert.ToInt32(txtFac_id.Text);
                String Fac_name = txtFac_name.Text;  // Ensure Fac_name is defined before use

                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    // Delete related records first
                    using (SqlCommand cmd = new SqlCommand("DELETE FROM tblFacilities_Villa WHERE Fac_id=@Fac_id", con))
                    {
                        cmd.Parameters.AddWithValue("@Fac_id", Fac_id);
                        cmd.ExecuteNonQuery();
                    }

                    // Now delete the main record
                    using (SqlCommand cmd2 = new SqlCommand("DELETE FROM tblFacilities WHERE Fac_id=@Fac_id", con))
                    {
                        cmd2.Parameters.AddWithValue("@Fac_id", Fac_id);
                        IsDeleted = cmd2.ExecuteNonQuery() > 0;
                    }
                    con.Close();
                }

                if (IsDeleted)
                {
                    lblMsg.Text = Fac_name + " Facility deleted successfully!";
                    lblMsg.ForeColor = System.Drawing.Color.Green;
                    // Refresh the GridView by calling the BindCategoryData()
                    BindCategoryData();
                }
                else
                {
                    lblMsg.Text = "Error while deleting " + Fac_name + " Facility";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                }
                ResetAll();
            }
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
            ResetAll();
        }
    }
}