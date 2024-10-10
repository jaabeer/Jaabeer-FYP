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
    public partial class AllNotif : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {


            AllNotifs();



        }

        void AllNotifs()
        {
            int User = Convert.ToInt32(Session["owner_id"]);

            SqlConnection con = new SqlConnection(_conString);
          
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * From tblNotification Where State = 1 and Show=1 AND Notif_type IN('notif','notif_up', 'update') and user_id=@Own_id Order by Notif_date DESC";
            cmd.Parameters.AddWithValue("@Own_id", User);
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




        protected void btnAllNotif_Click(object sender, EventArgs e)
        {
            AllNotifs();
            btnReadAll.Attributes["style"] = "display:none";
        }

        protected void btnCreation_Click(object sender, EventArgs e)
        {
            btnReadAll.Attributes["style"] = "display:none";

            int User = Convert.ToInt32(Session["owner_id"]);

            SqlConnection con = new SqlConnection(_conString);
           
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from  tblNotification   where State=1 and Show=1 and user_id=@Own_id and Notif_type='notif'  Order by Notif_date DESC";
            cmd.Parameters.AddWithValue("@Own_id", User);
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

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            btnReadAll.Attributes["style"] = "display:block";

            int User = Convert.ToInt32(Session["owner_id"]);

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from  tblNotification   where State=1 and Show=1 and user_id=@Own_id and Notif_type='update'  Order by Notif_date DESC";
            cmd.Parameters.AddWithValue("@Own_id", User);
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

        protected void btnMainUpdate_Click(object sender, EventArgs e)
        {
            btnReadAll.Attributes["style"] = "display:none";
            int User = Convert.ToInt32(Session["owner_id"]);

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from  tblNotification   where State=1 and Show=1 and user_id=@Own_id and Notif_type='notif_up'  Order by Notif_date DESC";
            cmd.Parameters.AddWithValue("@Own_id", User);
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

        protected void btnread_Click(object sender, EventArgs e)
        {
            int notif_id = Convert.ToInt32((sender as LinkButton).CommandArgument);

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblNotification set Show=@show where id=" + notif_id;
            cmd.Parameters.AddWithValue("@show", 0);
            cmd.Connection = con;
            con.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            AllNotifs();
        }

        protected void btnReadAll_Click(object sender, EventArgs e)
        {

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblNotification set Show=@show where Notif_type='update' and Show=1";
            cmd.Parameters.AddWithValue("@show", 0);
            cmd.Connection = con;
            con.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            AllNotifs();

        }
    }
}