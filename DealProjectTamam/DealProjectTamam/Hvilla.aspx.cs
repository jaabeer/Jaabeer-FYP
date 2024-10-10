using System;
using System.Data.SqlClient;
using System.Data;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class Hvilla : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadLatestVillaData();
                LoadVillaRatingData();
                LoadVillaPriceData();
            }
        }

        private void LoadLatestVillaData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Villa_id, Villa_name, Villa_priceday, Villa_image, Villa_town, Villa_priceweek, Villa_pricemonth, Villa_rating FROM tblVilla ORDER BY Villa_regis_date DESC";
            LoadVillaData(connectionString, query, rptLatestVilla);
        }

        private void LoadVillaRatingData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Villa_id, Villa_name, Villa_priceday, Villa_image, Villa_town, Villa_priceweek, Villa_pricemonth, Villa_rating FROM tblVilla ORDER BY Villa_rating DESC";
            LoadVillaData(connectionString, query, rptVillaRating);
        }

        private void LoadVillaPriceData()
        {
            string connectionString = System.Configuration.ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            string query = "SELECT TOP 4 Villa_id, Villa_name, Villa_priceday, Villa_image, Villa_town, Villa_priceweek, Villa_pricemonth FROM tblVilla ORDER BY Villa_priceday DESC";
            LoadVillaData(connectionString, query, rptVillaPrice);
        }

        private void LoadVillaData(string connectionString, string query, Repeater repeater)
        {
            using (SqlConnection connection = new SqlConnection(connectionString))
            {
                SqlDataAdapter da = new SqlDataAdapter(query, connection);
                DataTable dt = new DataTable();
                da.Fill(dt);
                repeater.DataSource = dt;
                repeater.DataBind();
            }
        }

        protected void btnMoreDetails_Click(object sender, EventArgs e)
        {
            ClientScript.RegisterStartupScript(this.GetType(), "LoginAlert", "alert('You need to login as client to book this villa.');", true);
        }
    }
}
