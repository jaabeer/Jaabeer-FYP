using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam.AdminS
{
    public partial class Searchowner : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getOwners();
            }
        }

        private void getOwners(string searchText = "")
        {
            DataTable dt = new DataTable(); // Declare and instantiate dt here

            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT TOP 3 * FROM tblOwner [to] LEFT OUTER JOIN tblEmailOTP te ON te.User_email = [to].Own_email WHERE te.User_email IS NULL";
                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " AND ([to].Own_lname LIKE @searchText OR [to].Own_fname LIKE @searchText)";
                }
                SqlCommand cmd = new SqlCommand(query, con);
                if (!string.IsNullOrEmpty(searchText))
                {
                    cmd.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                }
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                da.Fill(dt);
                gvs.DataSource = dt;
                gvs.DataBind();
            }

            DataTable dt2 = new DataTable(); // Declare and instantiate dt2 here

            using (SqlConnection con2 = new SqlConnection(_conString))
            {
                string query2 = "SELECT TOP 2 * FROM tblEmailOTP te INNER JOIN tblOwner [to] ON [to].Own_email = te.User_email AND te.Status = '0'";
                if (!string.IsNullOrEmpty(searchText))
                {
                    query2 += " AND ([to].Own_lname LIKE @searchText OR [to].Own_fname LIKE @searchText)";
                }
                SqlCommand cmd2 = new SqlCommand(query2, con2);
                if (!string.IsNullOrEmpty(searchText))
                {
                    cmd2.Parameters.AddWithValue("@searchText", "%" + searchText + "%");
                }
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                da2.Fill(dt2);

                dt.Merge(dt2);
                gvs.DataSource = dt.DefaultView;
                gvs.DataBind();
            }
        }

        protected void btnSearch_Click(object sender, EventArgs e)
        {
            getOwners(txtSearch.Text);
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
            int Own_id = Convert.ToInt32((sender as LinkButton).CommandArgument);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tblOwner SET Own_status = (CASE WHEN Own_status = 'True' THEN 'False' ELSE 'True' END) WHERE Own_id = @Own_id", con);
                cmd.Parameters.AddWithValue("@Own_id", Own_id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            getOwners();
        }

        [WebMethod]
        public static List<string> GetClientNames(string prefix)
        {
            List<string> clientNames = new List<string>();
            string conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Own_lname + ' ' + Own_fname AS FullName FROM tblOwner WHERE Own_lname LIKE @SearchText + '%' OR Own_fname LIKE @SearchText + '%'", con))
                {
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            clientNames.Add(sdr["FullName"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return clientNames;
        }
    }
}
