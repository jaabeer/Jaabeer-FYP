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
    public partial class AdminSayf : System.Web.UI.MasterPage
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                NotifCount();
                SupportCount();
                NotifDetails();
                SupportDetails();
            }


        }
        public void NotifCount()
        {
            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*) From tblNotification Where State=0 AND Notif_type IN ('notif', 'notif_up')";

            dbcon1.Open();
         
            lblnotif.Text = cmd.ExecuteScalar().ToString();
            dbcon1.Close();
        }

        public void SupportCount()
        {
            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon1;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT count(*) From tblNotification Where State=0 AND Notif_type='support' ";

            dbcon1.Open();
         
            lblsupport.Text = cmd.ExecuteScalar().ToString();
            dbcon1.Close();
        }

        protected void NotifDetails()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblNotification Where State=0 and Notif_type IN ('notif', 'notif_up') order by id Desc";

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

        protected void SupportDetails()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblNotification Where State=0 and Notif_type='support' order by id Desc";

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

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); // Clears all session data
            Session.Abandon(); // Ends the session
            Response.Redirect("~/Home.aspx"); // Redirects to Home.aspx
        }
    }
}