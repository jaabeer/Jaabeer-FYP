using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class ClientVilla : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "villa";

            if (!IsPostBack)
            {
                PropertyData();
            }
        }
        protected void PropertyData()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_regis_date DESC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();
        }

        protected void regis_asc_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_regis_date ASC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

          
            DataTable dt = new DataTable();

           
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();

        }

        protected void regis_desc_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_regis_date DESC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

           
            DataTable dt = new DataTable();

           
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();

        }

        protected void price_asc_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_priceday ASC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

          
            DataTable dt = new DataTable();

          
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();

        }

        protected void price_desc_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_priceday DESC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            DataTable dt = new DataTable();

            
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();

        }

        protected void rat_asc_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_Rating ASC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

           
            DataTable dt = new DataTable();

            
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();

        }

        protected void rat_desc_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
            
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_Rating DESC";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

          
            DataTable dt = new DataTable();

           
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();
        }

        protected void btn_filter_Click(object sender, EventArgs e)
        {
            string sql_dynamic = "";
            string sql_per_day = "";
            
            if (ch_price_per_day1.Checked)
            {
                
                sql_per_day = " (tv.villa_priceday > 0 and tv.villa_priceday < = 1000) ";
            }

            if (ch_price_per_day2.Checked)
            {
                if (sql_per_day == "")
                {
                    sql_per_day = " (tv.villa_priceday > 1000 and tv.villa_priceday < = 3000) ";
                }
                else
                {
                    
                    sql_per_day = sql_per_day + " or " + " (tv.villa_priceday > 1000 and tv.villa_priceday < = 3000) ";
                }

            }

            if (ch_price_per_day3.Checked)
            {
                if (sql_per_day == "")
                {
                   
                    sql_per_day = " (tv.villa_priceday > 3000 and tv.villa_priceday < = 5000) ";
                }
                else
                {
                   
                    sql_per_day = sql_per_day + " or " + " (tv.villa_priceday > 3000 and tv.villa_priceday < = 5000) ";
                }

            }

            if (ch_price_per_day4.Checked)
            {
                if (sql_per_day == "")
                {
                   
                    sql_per_day = " (tv.villa_priceday > 5000) ";
                }
                else
                {
                    
                    sql_per_day = sql_per_day + " or " + " (tv.villa_priceday > 5000) ";
                }

            }


            if (sql_per_day != "")
            {
                sql_dynamic = " where (" + sql_per_day + " )";
            }

         

            string sql_per_week = "";
            if (ch_price_per_week1.Checked)
            {
                sql_per_week = " (tv.villa_priceweek < 1000) ";
            }

            if (ch_price_per_week2.Checked)
            {
                if (sql_per_week == "")
                {
                    sql_per_week = " (tv.villa_priceweek < 5000) ";
                }
                else
                {
                    sql_per_week = sql_per_week + " or " + " (tv.villa_priceweek < 5000) ";
                }

            }

            if (ch_price_per_week3.Checked)
            {
                if (sql_per_week == "")
                {
                    sql_per_week = " (tv.villa_priceweek < 7500) ";
                }
                else
                {
                    sql_per_week = sql_per_week + " or " + " (tv.villa_priceweek < 7500) ";
                }

            }

            if (ch_price_per_week4.Checked)
            {
                if (sql_per_week == "")
                {
                    sql_per_week = " (tv.villa_priceweek < 10000) ";
                }
                else
                {
                    sql_per_week = sql_per_week + " or " + " (tv.villa_priceweek < 10000) ";
                }

            }

            if (ch_price_per_week5.Checked)
            {
                if (sql_per_week == "")
                {
                    sql_per_week = " (tv.villa_priceweek > 10000) ";
                }
                else
                {
                    sql_per_week = sql_per_week + " or " + " (tv.villa_priceweek > 10000) ";
                }

            }


            if (sql_per_week != "")
            {
                if (sql_dynamic != "")
                {
                    sql_dynamic = sql_dynamic + " and (" + sql_per_week + " )";
                }
                else
                {
                    sql_dynamic = " where (" + sql_per_week + " )";
                }

            }

         
            string sql_per_month = "";

            if (ch_price_per_month1.Checked)
            {
                sql_per_month = " (tv.villa_pricemonth < 3000) ";
            }

            if (ch_price_per_month2.Checked)
            {
                if (sql_per_month == "")
                {
                    sql_per_month = " (tv.villa_pricemonth < 7000) ";
                }
                else
                {
                    sql_per_month = sql_per_month + " or " + " (tv.villa_pricemonth < 7000) ";
                }

            }

            if (ch_price_per_month3.Checked)
            {
                if (sql_per_month == "")
                {
                    sql_per_month = " (tv.villa_pricemonth < 10000) ";
                }
                else
                {
                    sql_per_month = sql_per_month + " or " + " (tv.villa_pricemonth < 10000) ";
                }

            }

            if (ch_price_per_month4.Checked)
            {
                if (sql_per_month == "")
                {
                    sql_per_month = " (tv.villa_pricemonth < 20000) ";
                }
                else
                {
                    sql_per_month = sql_per_month + " or " + " (tv.villa_pricemonth < 20000) ";
                }

            }

            if (ch_price_per_month5.Checked)
            {
                if (sql_per_month == "")
                {
                    sql_per_month = " (tv.villa_pricemonth < 50000) ";
                }
                else
                {
                    sql_per_month = sql_per_month + " or " + " (tv.villa_pricemonth < 50000) ";
                }

            }

            if (ch_price_per_month6.Checked)
            {
                if (sql_per_month == "")
                {
                    sql_per_month = " (tv.villa_pricemonth > 50000) ";
                }
                else
                {
                    sql_per_month = sql_per_month + " or " + " (tv.villa_pricemonth > 50000) ";
                }

            }

            if (sql_per_month != "")
            {
                if (sql_dynamic != "")
                {
                    sql_dynamic = sql_dynamic + " and (" + sql_per_month + " )";
                }
                else
                {
                    sql_dynamic = " where (" + sql_per_month + " )";
                }

            }


           
            string sql_region = "";
            if (Ch_region_1.Checked)
            {
                sql_region = "'Center'";

            }

            if (Ch_region_2.Checked)
            {
                if (sql_region != "")
                {
                    sql_region = sql_region + "," + "'East'";
                }
                else
                {
                    sql_region = "'East'";
                }
            }

            if (Ch_region_3.Checked)
            {
                if (sql_region != "")
                {
                    sql_region = sql_region + "," + "'North'";
                }
                else
                {
                    sql_region = "'North'";
                }
            }

            if (Ch_region_4.Checked)
            {
                if (sql_region != "")
                {
                    sql_region = sql_region + "," + "'South'";
                }
                else
                {
                    sql_region = "'South'";
                }
            }

            if (Ch_region_5.Checked)
            {
                if (sql_region != "")
                {
                    sql_region = sql_region + "," + "'West'";
                }
                else
                {
                    sql_region = "'West'";
                }
            }

            if (sql_region != "")
            {
                sql_dynamic = " inner join tbldistrict td on td.dist_id = tv.dist_id" + sql_dynamic + " and td.dist_region in (" + sql_region + ")";
            }

           
            string sql_facility = "";

            if (Ch_fac_1.Checked)
            {
                sql_facility = "'AC'";

            }

            if (Ch_fac_2.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'BBQ Area'";
                }
                else
                {
                    sql_facility = "'BBQ Area'";
                }

            }
            if (Ch_fac_3.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Fitness Center'";
                }
                else
                {
                    sql_facility = "'Fitness Center'";
                }


            }

            if (Ch_fac_4.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Kids Playground'";
                }
                else
                {
                    sql_facility = "'Kids Playground'";
                }

            }

            if (Ch_fac_5.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Parking'";
                }
                else
                {
                    sql_facility = "'Parking'";
                }


            }

            if (Ch_fac_6.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Roomservice'";
                }
                else
                {
                    sql_facility = "'Roomservice'";
                }


            }

            if (Ch_fac_7.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Spa'";
                }
                else
                {
                    sql_facility = "'Spa'";
                }


            }

            if (Ch_fac_8.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Swimming pool'";
                }
                else
                {
                    sql_facility = "'Swimming pool'";
                }

            }

            if (Ch_fac_9.Checked)
            {
                if (sql_facility != "")
                {
                    sql_facility = sql_facility + "," + "'Wifi'";
                }
                else
                {
                    sql_facility = "'Wifi'";
                }

            }

            if (sql_facility != "")
            {
                sql_dynamic = " inner join tblfacilities_villa tfv on tfv.villa_id = tv.villa_id inner join tblfacilities tf on tf.fac_id = tfv.fac_id" + sql_dynamic + " and tf.fac_name in (" + sql_facility + ")";
            }

           


            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            if (sql_dynamic == "")
            {
                cmd.CommandText = "SELECT  * FROM tblVilla Where Villa_status=1 Order by Villa_regis_date DESC";
            }
            else
            {
                cmd.CommandText = "SELECT  * FROM tblVilla tv " + sql_dynamic + " and tv.Villa_status =1 Order by tv.Villa_regis_date DESC";
            }


            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }

            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            con.Close();



          
        }





    }
}