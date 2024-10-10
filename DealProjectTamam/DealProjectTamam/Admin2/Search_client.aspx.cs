using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.Services;

namespace DealProjectTamam.Admin2
{
    public partial class Search_client : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getClients();
            }
        }

        private void getClients(string searchText = "")
        {
            DataTable dt = new DataTable(); // Declare and instantiate dt here

            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT * FROM tblClient tc LEFT OUTER JOIN tblEmailOTP te ON te.User_email = tc.Client_email WHERE te.User_email IS NULL";
                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " AND (tc.Client_lname LIKE @searchText OR tc.Client_fname LIKE @searchText)";
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
                string query2 = "SELECT * FROM tblEmailOTP te INNER JOIN tblClient tc ON tc.Client_email = te.User_email AND te.Status = '0'";
                if (!string.IsNullOrEmpty(searchText))
                {
                    query2 += " AND (tc.Client_lname LIKE @searchText OR tc.Client_fname LIKE @searchText)";
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
            getClients(txtSearch.Text);
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
            int Client_id = Convert.ToInt32((sender as LinkButton).CommandArgument);

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tblClient SET Client_status = (CASE WHEN Client_status = 'True' THEN 'False' ELSE 'True' END) WHERE Client_id = @Client_id", con);
                cmd.Parameters.AddWithValue("@Client_id", Client_id);
                con.Open();
                cmd.ExecuteNonQuery();
                con.Close();
            }

            getClients();
        }

        [WebMethod]
        public static List<string> GetClientNames(string prefix)
        {
            List<string> clientNames = new List<string>();
            string conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Client_lname + ' ' + Client_fname AS FullName FROM tblClient WHERE Client_lname LIKE @SearchText + '%' OR Client_fname LIKE @SearchText + '%'", con))
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
