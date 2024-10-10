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
    public partial class Ticket : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {

          
        }

        void AllTickets()
        {


            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From tblSupport t, tblStatus s Where s.status_id=t.status and t.email=@client Order by t.created_date DESC";
            cmd.Parameters.AddWithValue("@client", Session["owner_email"]);
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

        protected void btnAll_Click(object sender, EventArgs e)
        {

        }

        protected void btnClosed_Click(object sender, EventArgs e)
        {

        }

        protected void btnOpen_Click(object sender, EventArgs e)
        {

        }
    }
}
