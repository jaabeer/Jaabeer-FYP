﻿using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class HHotel : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
               
                LoadLatestHotelData();
                LoadHotelRatingData();
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