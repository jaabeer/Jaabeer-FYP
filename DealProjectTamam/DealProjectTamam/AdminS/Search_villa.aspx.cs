using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace DealProjectTamam.AdminS
{
    public partial class Search_villa : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getVillas();
            }
        }

        private void getVillas(string searchText = "")
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT * FROM tblVilla tv INNER JOIN tblDistrict td ON td.Dist_id = tv.Dist_id";
                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " WHERE tv.Villa_name LIKE @searchText";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                if (!string.IsNullOrEmpty(searchText))
                {
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                gvs.DataSource = dt;
                gvs.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getVillas(txtSearch.Text);
        }

        protected void gvs_PreRender(object sender, EventArgs e)
        {
            if (gvs.Rows.Count > 0)
            {
                gvs.UseAccessibleHeader = true;
                gvs.HeaderRow.TableSection = TableRowSection.TableHeader;
            }
        }

        protected void lnkblock_Click(object sender, EventArgs e)
        {
            int Villa_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tblVilla SET Villa_status = (CASE WHEN Villa_status = 'True' THEN 'False' ELSE 'True' END) WHERE Villa_id = @Villa_id", con);
                cmd.Parameters.AddWithValue("@Villa_id", Villa_id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            getVillas(); // Refresh data
        }

        protected void lnkDelete_Click1(object sender, EventArgs e)
        {
            int Villa_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            using (SqlConnection con = new SqlConnection(_conString))
            {
                try
                {
                    con.Open();
                    // Start a transaction
                    SqlTransaction transaction = con.BeginTransaction();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.Transaction = transaction;

                    try
                    {
                        // Delete related images
                        cmd.CommandText = "DELETE FROM tblImage WHERE Villa_id = @Villa_id";
                        cmd.Parameters.AddWithValue("@Villa_id", Villa_id);
                        cmd.ExecuteNonQuery();

                        // Delete related facilities
                        cmd.CommandText = "DELETE FROM tblFacilities_Villa WHERE Villa_id = @Villa_id";
                        cmd.ExecuteNonQuery();

                        // Delete related details
                        cmd.CommandText = "DELETE FROM tblDetails_Villa WHERE Villa_id = @Villa_id";
                        cmd.ExecuteNonQuery();

                        // Delete related bookings
                        cmd.CommandText = "DELETE FROM tblBooking WHERE Villa_id = @Villa_id";
                        cmd.ExecuteNonQuery();

                        // Finally, delete the villa
                        cmd.CommandText = "DELETE FROM tblVilla WHERE Villa_id = @Villa_id";
                        cmd.ExecuteNonQuery();

                        // Commit transaction if all commands succeed
                        transaction.Commit();
                    }
                    catch
                    {
                        // Rollback transaction if an error occurs
                        transaction.Rollback();
                        throw;
                    }
                }
                catch (Exception ex)
                {
                    // 1/06/2024 Handle the exception (log/display error message)
                    System.Diagnostics.Debug.WriteLine("Error during deletion: " + ex.Message);
                    ClientScript.RegisterStartupScript(this.GetType(), "Alert", "alert('Error during deletion: " + ex.Message + "');", true);
                }
                finally
                {
                    con.Close();
                }
            }
            getVillas(); // Refresh data
        }

        [WebMethod]
        public static List<string> GetVillaNames(string prefix)
        {
            List<string> villaNames = new List<string>();
            string conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Villa_name FROM tblVilla WHERE Villa_name LIKE @SearchText + '%'", con))
                {
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            villaNames.Add(sdr["Villa_name"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return villaNames;
        }
    }
}
