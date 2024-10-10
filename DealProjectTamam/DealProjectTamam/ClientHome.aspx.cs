using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class ClientHome : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLatestVillaData();
                LoadVillaRatingData();
                LoadLatestHotelData();
                LoadHotelRatingData();
            }
        }

        private void LoadLatestVillaData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Villa_id, Villa_name, Villa_priceday, Villa_image, Villa_town, Villa_priceweek, Villa_pricemonth, Villa_rating FROM tblVilla ORDER BY Villa_regis_date DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptLatestVilla.DataSource = dt;
                rptLatestVilla.DataBind();
            }
        }

        private void LoadVillaRatingData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Villa_id, Villa_name, Villa_priceday, Villa_image, Villa_town, Villa_priceweek, Villa_pricemonth, Villa_rating FROM tblVilla ORDER BY Villa_rating DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptVillaRating.DataSource = dt;
                rptVillaRating.DataBind();
            }
        }

        private void LoadLatestHotelData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Hotel_id, Hotel_name, Hotel_price, Hotel_image, Hotel_town, Hotel_rating FROM tblHotel ORDER BY Hotel_regis_date DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptLatestHotel.DataSource = dt;
                rptLatestHotel.DataBind();
            }
        }

        private void LoadHotelRatingData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Hotel_id, Hotel_name, Hotel_price, Hotel_image, Hotel_town, Hotel_rating FROM tblHotel ORDER BY Hotel_rating DESC";

            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                rptHotelRating.DataSource = dt;
                rptHotelRating.DataBind();
            }
        }
    }
}

 