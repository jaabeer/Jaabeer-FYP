using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam.Admin_J
{
    public partial class AdminHomepage1 : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                populate_year();
                fill_pie_chart();
                fill_cards();
                fill_bar_chart();
                fill_Notification_card();
                fill_support_card();
            }
        }

        public void fill_cards()
        {
            fill_villa_card();
            fill_client_card();
        }

        public void fill_client_card()
        {
            using (SqlConnection dbcon1 = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM tblClient", dbcon1);
                dbcon1.Open();
                hy_no_of_clients.Text = cmd.ExecuteScalar().ToString();
                dbcon1.Close();
            }
        }

        public void fill_villa_card()
        {
            using (SqlConnection dbcon1 = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM tblHotel", dbcon1);
                dbcon1.Open();
                hy_no_of_villas.Text = cmd.ExecuteScalar().ToString();
                dbcon1.Close();
            }
        }

        public void fill_pie_chart()
        {
            string query = string.Format(
                "SELECT th.Hotel_name, COUNT(tb.Bk_ref) AS Total_Book_Per_Hotel " +
                "FROM tblHotel th " +
                "INNER JOIN tblBooking tb ON th.Hotel_id = tb.Hotel_id " +
                "WHERE th.Hotel_approval_date <> '9999-12-31' " +
                "AND th.Hotel_status = 'True' " +
                "AND tb.Bk_state IN ('C', 'P') " +
                "AND YEAR(tb.Bk_date) = 2024 " +
                "GROUP BY th.Hotel_name");

            using (DataTable dt = GetData(query, ddlyear.SelectedItem.Text))
            {
                if (dt.Rows.Count > 0)
                {
                    foreach (DataRow row in dt.Rows)
                    {
                        PieChart_booking_per_hotel.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                        {
                            Category = row["Hotel_name"].ToString(),
                            Data = Convert.ToDecimal(row["Total_Book_Per_Hotel"])
                        });
                    }
                }
                else
                {
                    // Log that no data was found
                    PieChart_booking_per_hotel.PieChartValues.Add(new AjaxControlToolkit.PieChartValue
                    {
                        Category = "No Data",
                        Data = 0
                    });
                }
                PieChart_booking_per_hotel.ChartTitle = "Booking per Hotel for year " + ddlyear.SelectedItem.Text;
            }
        }

        public void fill_bar_chart()
        {
            string query = string.Format(
                "SELECT DATENAME(month, Bk_date) AS month_selected, COUNT(Bk_ref) AS number_of_booking " +
                "FROM tblBooking " +
                "WHERE YEAR(Bk_date) = 2024 " +
                "AND Bk_state IN ('C', 'P') " +
                "GROUP BY DATENAME(month, Bk_date), MONTH(Bk_date) " +
                "ORDER BY MONTH(Bk_date)");

            using (DataTable dt = GetData(query, ddlyear.SelectedItem.Text))
            {
                if (dt.Rows.Count > 0)
                {
                    string[] months = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
                    decimal[] bookings = new decimal[12];

                    foreach (DataRow row in dt.Rows)
                    {
                        string month = row["month_selected"].ToString();
                        int index = Array.IndexOf(months, month);
                        if (index >= 0)
                        {
                            bookings[index] = Convert.ToDecimal(row["number_of_booking"]);
                        }
                    }

                    BarChart_booking_per_month.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = bookings, Name = "Booking for month" });
                    BarChart_booking_per_month.CategoriesAxis = string.Join(",", months.Select(m => m.Substring(0, 3)));
                    BarChart_booking_per_month.ChartTitle = "Deal Tamam Booking per month for year " + ddlyear.SelectedItem.Text;
                }
                else
                {
                    // Log that no data was found
                    BarChart_booking_per_month.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = new decimal[12], Name = "No Data" });
                    BarChart_booking_per_month.CategoriesAxis = "Jan,Feb,Mar,Apr,May,Jun,Jul,Aug,Sep,Oct,Nov,Dec";
                    BarChart_booking_per_month.ChartTitle = "No Booking Data for year " + ddlyear.SelectedItem.Text;
                }
            }
        }

        private DataTable GetData(string query, string year)
        {
            DataTable dt = new DataTable();
            using (SqlConnection con = new SqlConnection(_conString))
            {
                using (SqlCommand cmd = new SqlCommand(query, con))
                {
                    cmd.Parameters.AddWithValue("@year", year);
                    using (SqlDataAdapter sda = new SqlDataAdapter(cmd))
                    {
                        sda.Fill(dt);
                    }
                }
            }
            return dt;
        }

        public void populate_year()
        {
            ListItem currentYear = new ListItem(DateTime.Now.Year.ToString(), "0");
            ListItem previousYear = new ListItem((DateTime.Now.Year - 1).ToString(), "1");

            ddlyear.Items.Add(currentYear);
            ddlyear.Items.Add(previousYear);
            ddlyear.CssClass = "form-control form-control-user";
        }

        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_bar_chart();
            fill_pie_chart();
        }

        public void fill_Notification_card()
        {
            using (SqlConnection dbcon1 = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand(
                    "SELECT count(*) " +
                    "FROM tblHotel th " +
                    "JOIN tblNotification tn ON tn.Hotel_id = th.Hotel_id " +
                    "JOIN tblDistrict td ON td.Dist_id = th.Dist_id " +
                    "WHERE tn.State = 0 " +
                    "AND tn.Notif_type = 'update'", dbcon1);

                dbcon1.Open();
                hy_notif.Text = cmd.ExecuteScalar().ToString();
                dbcon1.Close();
            }
        }

        public void fill_support_card()
        {
            using (SqlConnection dbcon1 = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM tblSupport", dbcon1);
                dbcon1.Open();
                hy_tickets.Text = cmd.ExecuteScalar().ToString();
                dbcon1.Close();
            }
        }
    }
}
