using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class ClientHotel : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "hotel";

            if (!IsPostBack)
            {
                PropertyData();
            }
        }

        protected void PropertyData()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = "SELECT * FROM tblHotel WHERE Hotel_status = 1 ORDER BY Hotel_regis_date DESC";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Response.Write("No data found.");
                }
            }
        }

        protected void regis_asc_Click(object sender, EventArgs e)
        {
            SortData("Hotel_regis_date ASC");
        }

        protected void regis_desc_Click(object sender, EventArgs e)
        {
            SortData("Hotel_regis_date DESC");
        }

        protected void price_asc_Click(object sender, EventArgs e)
        {
            SortData("Hotel_price ASC");
        }

        protected void price_desc_Click(object sender, EventArgs e)
        {
            SortData("Hotel_price DESC");
        }

        protected void rat_asc_Click(object sender, EventArgs e)
        {
            SortData("Hotel_Rating ASC");
        }

        protected void rat_desc_Click(object sender, EventArgs e)
        {
            SortData("Hotel_Rating DESC");
        }

        private void SortData(string sortOrder)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = $"SELECT * FROM tblHotel WHERE Hotel_status = 1 ORDER BY {sortOrder}";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Response.Write("No data found.");
                }
            }
        }

        protected void btn_filter_Click(object sender, EventArgs e)
        {
            string sql_dynamic = "";
            string sql_per_day = "";

            // Price per day filtering logic...
            if (ch_price_per_day1.Checked)
            {
                sql_per_day = " (th.Hotel_price > 1000 and th.Hotel_price <= 1500) ";
            }
            if (ch_price_per_day2.Checked)
            {
                sql_per_day += (string.IsNullOrEmpty(sql_per_day) ? "" : " OR ") + " (th.Hotel_price > 1500 and th.Hotel_price <= 3000) ";
            }
            if (ch_price_per_day3.Checked)
            {
                sql_per_day += (string.IsNullOrEmpty(sql_per_day) ? "" : " OR ") + " (th.Hotel_price > 3000 and th.Hotel_price <= 5000) ";
            }
            if (ch_price_per_day4.Checked)
            {
                sql_per_day += (string.IsNullOrEmpty(sql_per_day) ? "" : " OR ") + " (th.Hotel_price > 5000) ";
            }

            if (!string.IsNullOrEmpty(sql_per_day))
            {
                sql_dynamic = " WHERE (" + sql_per_day + ")";
            }

            // Region and Facilities filtering logic...
            string sql_region = "";
            if (Ch_region_1.Checked)
            {
                sql_region = "'Center'";
            }
            if (Ch_region_2.Checked)
            {
                sql_region += (string.IsNullOrEmpty(sql_region) ? "" : ",") + "'East'";
            }
            if (Ch_region_3.Checked)
            {
                sql_region += (string.IsNullOrEmpty(sql_region) ? "" : ",") + "'North'";
            }
            if (Ch_region_4.Checked)
            {
                sql_region += (string.IsNullOrEmpty(sql_region) ? "" : ",") + "'South'";
            }
            if (Ch_region_5.Checked)
            {
                sql_region += (string.IsNullOrEmpty(sql_region) ? "" : ",") + "'West'";
            }

            if (!string.IsNullOrEmpty(sql_region))
            {
                sql_dynamic += " INNER JOIN tblDistrict td ON td.Dist_id = th.Dist_id" + sql_dynamic + " AND td.Dist_region IN (" + sql_region + ")";
            }

            // Facilities filtering logic...
            string sql_facility = "";
            if (Ch_fac_1.Checked)
            {
                sql_facility = "'AC'";
            }
            if (Ch_fac_2.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'BBQ Area'";
            }
            if (Ch_fac_3.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Fitness Center'";
            }
            if (Ch_fac_4.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Kids Playground'";
            }
            if (Ch_fac_5.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Parking'";
            }
            if (Ch_fac_6.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Roomservice'";
            }
            if (Ch_fac_7.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Spa'";
            }
            if (Ch_fac_8.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Swimming pool'";
            }
            if (Ch_fac_9.Checked)
            {
                sql_facility += (string.IsNullOrEmpty(sql_facility) ? "" : ",") + "'Wifi'";
            }

            if (!string.IsNullOrEmpty(sql_facility))
            {
                sql_dynamic += " INNER JOIN tblFacilities_Hotel tfh ON tfh.Hotel_id = th.Hotel_id INNER JOIN tblFacilities tf ON tf.Fac_id = tfh.Fac_id" + sql_dynamic + " AND tf.Fac_name IN (" + sql_facility + ")";
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;
                cmd.CommandText = string.IsNullOrEmpty(sql_dynamic) ?
                    "SELECT * FROM tblHotel WHERE Hotel_status = 1 ORDER BY Hotel_regis_date DESC" :
                    "SELECT * FROM tblHotel th " + sql_dynamic + " AND th.Hotel_status = 1 ORDER BY th.Hotel_regis_date DESC";

                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();

                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    Repeater1.DataSource = dt;
                    Repeater1.DataBind();
                }
                else
                {
                    Response.Write("No data found.");
                }
            }
        }
    }
}
