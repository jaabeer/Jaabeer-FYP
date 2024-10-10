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
    public partial class CartVilla : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "My_villas";
            CartData();

        }

        protected void CartData()
        {
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblBooking B, tblVilla V where B.Villa_id=V.Villa_id and Bk_state='P' and Client_id=@Client_id";
            cmd.Parameters.AddWithValue("@Client_id", Session["client_id"]);

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

        protected void btnDiscard_Click(object sender, EventArgs e)
        {
            Boolean IsDeleted = false;

            int booking = Convert.ToInt32((sender as LinkButton).CommandArgument);
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.CommandText = "delete from tblBooking where Bk_id=@booking";
            cmd2.Parameters.AddWithValue("@booking", booking);
            cmd2.Connection = con2;
            con2.Open();

            IsDeleted = cmd2.ExecuteNonQuery() > 0;
            con2.Close();

            if (IsDeleted)
            {
                CartData();
            }

        }
    }
}