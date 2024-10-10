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
    public partial class AdminHomepage : System.Web.UI.Page
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
            }

        }

        public void fill_cards()
        {
            fill_villa_card();
            fill_client_card();

        }

        public void fill_client_card()
        {
            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*) FROM tblClient";

            dbcon1.Open();
          
            hy_no_of_clients.Text = cmd.ExecuteScalar().ToString();
            dbcon1.Close();
        }

        public void fill_villa_card()
        {
            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*) FROM tblVilla";

            dbcon1.Open();
           
            hy_no_of_villas.Text = cmd.ExecuteScalar().ToString();
            dbcon1.Close();
        }

        public void fill_pie_chart()
        {
           
            string query = string.Format("select tv.Villa_name, COUNT(tb.Bk_ref) AS Total_Book_Per_Hotel from tblVilla tv inner join tblBooking tb on tv.Villa_id = tb.Villa_id where tv.Villa_approval_date <> '12/31/9999' and tv.Villa_status = '1' and tb.Bk_state in ('C','F') and year(Bk_date) = '{0}' group by tv.Villa_name", ddlyear.SelectedItem.Text.ToString());
           
            DataTable dt = GetData(query);
            //Loop and add each datatable row to the Pie Chart Values
            foreach (DataRow row in dt.Rows)
            {
                PieChart_booking_per_villa.PieChartValues.Add(new
                AjaxControlToolkit.PieChartValue
                {

                    Category = row["Villa_name"].ToString(),
                    Data = Convert.ToDecimal(row["Total_Book_Per_Villa"])
                });
            }
            PieChart_booking_per_villa.ChartTitle = "Booking per Villa for year " + ddlyear.SelectedItem.Text.ToString();
        }

        private static DataTable GetData(string query)
        {
            DataTable dt = new DataTable();
            string constr = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(constr))
            {
                using (SqlCommand cmd = new SqlCommand(query))
                {
                    using (SqlDataAdapter sda = new SqlDataAdapter())
                    {
                        cmd.CommandType = CommandType.Text;
                        cmd.Connection = con;
                        sda.SelectCommand = cmd;
                        sda.Fill(dt);
                    }
                }
                return dt;
            }
        }

        public void fill_bar_chart()
        {
            string qs;
            qs = string.Format("select DATENAME(month,Bk_date) as month_selected, count(Bk_ref) as number_of_booking from tblBooking where year(Bk_date) = '{0}' and Bk_state in ('C','F') group by DATENAME(month, Bk_date) order by month_selected desc", ddlyear.SelectedItem.Text.ToString());
         
            string query = qs;
            DataTable dt = GetData(query);
            string[] x = new string[] { "January", "February", "March", "April", "May", "June", "July", "August", "September", "October", "November", "December" };
            decimal[] y = new decimal[] { 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0, 0 };
          
            for (int i = 0; i < x.Length; i++)
            {
                for (int j = 0; j < dt.Rows.Count; j++)
                {
                    if (x[i].ToString() == dt.Rows[j][0].ToString())
                    {
                        y[i] = Convert.ToInt32(dt.Rows[j][1]);


                    }
                }

                x[i] = (x[i].ToString()).Substring(0, 3);

            }
            BarChart_booking_per_month.Series.Add(new AjaxControlToolkit.BarChartSeries { Data = y, Name = "Booking for month" });
            BarChart_booking_per_month.CategoriesAxis = string.Join(",", x);
           
            BarChart_booking_per_month.ChartTitle = " Deal Tamam Booking per month for year " + ddlyear.SelectedItem.Text.ToString();

        }

        public void populate_year()
        {
            ListItem newItem1 = new ListItem();
            newItem1.Text = DateTime.Now.Year.ToString();
            newItem1.Value = "0";
            ddlyear.Items.Add(newItem1);
            ListItem newItem2 = new ListItem();
            newItem2.Text = ((DateTime.Now.Year) - 1).ToString();
            newItem2.Value = "1";
            ddlyear.Items.Add(newItem2);
            ddlyear.CssClass = "form-control form-control-user";
        }

        protected void ddlyear_SelectedIndexChanged(object sender, EventArgs e)
        {
            fill_bar_chart();
            fill_pie_chart();

        }

        public void fill_Notification_card()
        {
            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*)  from tblVilla tv, tblNotification tn, tblDistrict td where td.Dist_id= tv.Dist_id and  tn.Villa_id = tv.Villa_id and tn.State=0 and tn.Notif_type='update'";

            dbcon1.Open();
         
            hy_notif.Text = cmd.ExecuteScalar().ToString();
            dbcon1.Close();
        }

        public void fill_support_card()
        {
            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*)  from tblSupport";

            dbcon1.Open();
        
            hy_tickets.Text = cmd.ExecuteScalar().ToString();
            dbcon1.Close();
        }
    }
}