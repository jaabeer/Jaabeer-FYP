using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.Services;
using System.Web.UI.WebControls;

namespace DealProjectTamam.Admin2
{
    public partial class Search_hotel : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                getHotels();
            }
        }

        private void getHotels(string searchText = "")
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                string query = "SELECT * FROM tblHotel th INNER JOIN tblDistrict td ON td.Dist_id = th.Dist_id";
                if (!string.IsNullOrEmpty(searchText))
                {
                    query += " WHERE th.Hotel_name LIKE @searchText";
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
            getHotels(txtSearch.Text);
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
            int Hotel_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("UPDATE tblHotel SET Hotel_status = (CASE WHEN Hotel_status = 'True' THEN 'False' ELSE 'True' END) WHERE Hotel_id = @Hotel_id", con);
                cmd.Parameters.AddWithValue("@Hotel_id", Hotel_id);
                con.Open();
                cmd.ExecuteNonQuery();
            }
            getHotels(); // Refresh data
        }

        [WebMethod]
        public static List<string> GetHotelNames(string prefix)
        {
            List<string> hotelNames = new List<string>();
            string conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(conString))
            {
                using (SqlCommand cmd = new SqlCommand("SELECT Hotel_name FROM tblHotel WHERE Hotel_name LIKE @SearchText + '%'", con))
                {
                    cmd.Parameters.AddWithValue("@SearchText", prefix);
                    con.Open();
                    using (SqlDataReader sdr = cmd.ExecuteReader())
                    {
                        while (sdr.Read())
                        {
                            hotelNames.Add(sdr["Hotel_name"].ToString());
                        }
                    }
                    con.Close();
                }
            }
            return hotelNames;
        }

        protected void lnkDelete_Click(object sender, EventArgs e)
        {
            int Hotel_id = Convert.ToInt32((sender as LinkButton).CommandArgument);
            try
            {
                using (SqlConnection con = new SqlConnection(_conString))
                {
                    con.Open();
                    // Delete related records from dependent tables
                    DeleteRelatedRecords(con, Hotel_id);

                    // Now, delete the hotel
                    SqlCommand cmdDeleteHotel = new SqlCommand("DELETE FROM tblHotel WHERE Hotel_id = @Hotel_id", con);
                    cmdDeleteHotel.Parameters.AddWithValue("@Hotel_id", Hotel_id);
                    cmdDeleteHotel.ExecuteNonQuery();
                }
                getHotels(); // Refresh the data
            }
            catch (Exception ex)
            {
                // Ideally, handle the error properly or log it
                ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Failed to delete hotel: " + ex.Message + "');", true);
            }
        }

        private void DeleteRelatedRecords(SqlConnection con, int hotelId)
        {
            // Delete from tblBooking first (if there are references to the hotel_id)
            new SqlCommand("DELETE FROM tblBooking WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();

            // Delete from tblRoomFacilities_Hotel
            new SqlCommand("DELETE FROM tblRoomFacilities_Hotel WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();

            // Delete from tblFacilities_Hotel
            new SqlCommand("DELETE FROM tblFacilities_Hotel WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();

            // Delete from tblDetails_Hotel
            new SqlCommand("DELETE FROM tblDetails_Hotel WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();

            // Delete from tblImage
            new SqlCommand("DELETE FROM tblImage WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();

            // Ensure tblNotification and tblReviews are also handled if they reference hotel_id
            new SqlCommand("DELETE FROM tblNotification WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();

            new SqlCommand("DELETE FROM tblReviews WHERE Hotel_id = @Hotel_id", con)
            { Parameters = { new SqlParameter("@Hotel_id", hotelId) } }.ExecuteNonQuery();
        }
    }
}
