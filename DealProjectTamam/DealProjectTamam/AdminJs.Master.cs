using System;
using System.Data;
using System.Data.SqlClient;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class AdminJs : System.Web.UI.MasterPage
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
            using (SqlConnection dbcon1 = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM tblNotification WHERE State=0 AND Notif_type IN ('notif', 'notif_up')", dbcon1);
                dbcon1.Open();
                lblnotif.Text = cmd.ExecuteScalar().ToString();
            }
        }

        public void SupportCount()
        {
            using (SqlConnection dbcon1 = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT count(*) FROM tblNotification WHERE State=0 AND Notif_type='support'", dbcon1);
                dbcon1.Open();
                lblsupport.Text = cmd.ExecuteScalar().ToString();
            }
        }

        protected void NotifDetails()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblNotification WHERE State=0 AND Notif_type IN ('notif', 'notif_up') ORDER BY id DESC", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater1.DataSource = dt;
                Repeater1.DataBind();
            }
        }

        protected void SupportDetails()
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand("SELECT * FROM tblNotification WHERE State=0 AND Notif_type='support' ORDER BY id DESC", con);
                SqlDataAdapter da = new SqlDataAdapter(cmd);
                DataTable dt = new DataTable();
                da.Fill(dt);
                Repeater2.DataSource = dt;
                Repeater2.DataBind();
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            Session.Clear(); // Clears all session data
            Session.Abandon(); // Ends the session
            Response.Redirect("~/Home.aspx"); // Redirects to Home.aspx
        }
    }
}
