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
    public partial class OwnerFaq : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "test";

            FAQCategory();
            FAQ_Question();

        }

        protected void btnsave_Click(object sender, EventArgs e)
        {

        }

        protected void FAQCategory()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFaq_category";

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }

            Repeater2.DataSource = dt;
            Repeater2.DataBind();

            con.Close();
        }


        protected void FAQ_Question()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFaq";

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
