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
    public partial class TicketDetails1 : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        String notif_id;
        protected void Page_Load(object sender, EventArgs e)

        {


            if ((Session[""] == null))
            {
                Response.Redirect("~/");
                TicketData();
                CommentData();
                getStatus();
                getCategory();
            }
        }

        protected void TicketData()
        {

            String qs = Request.QueryString["Parameter"].ToString();
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "select * from tblSupport t, tblStatus s, tblFaq_category c where t.status=s.status_Id and c.Id=t.category_id and t.email=@user and t.id=" + qs;
            cmd.Parameters.AddWithValue("@user", Session["owner_email"]);
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                txtFirstName.Text = dr["id"].ToString().Trim();
                txtFirstName.Text = dr["firstname"].ToString().Trim();
                txtLastName.Text = dr["lastname"].ToString().Trim();
                txtEmail.Text = dr["email"].ToString().Trim();
                txtMobile.Text = dr["mobile"].ToString().Trim();
                ddlCategory.SelectedValue = dr["category_id"].ToString().Trim();
                txttitle.Text = dr["title"].ToString().Trim();
                txtdspt.Text = dr["description"].ToString().Trim();
                ddlStatus.SelectedValue = dr["status"].ToString().Trim();
            }
        }


        protected void CommentData()
        {
            String qs = Request.QueryString["Parameter"].ToString();
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblSupportComment where Ticket_id=" + qs;

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
        public void getStatus()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblStatus where status_cat='support' and status_id>2";
            cmd.Connection = con;
            //Create DataAdapter

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create a Datatable or a DataSet

            DataTable dt = new DataTable();

            //Fill the Dataset and ensure the DB Connection is closed 
            using (da)
            {
                da.Fill(dt);
            }

            //To load country names in dropdown
            ddlStatus.DataSource = dt;
            //assign a field name and id to ddl
            ddlStatus.DataTextField = "status_name";
            ddlStatus.DataValueField = "status_id";
            ddlStatus.DataBind();
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {
            String qs = Request.QueryString["Parameter"].ToString();
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblSupport set status=@status where id=" + qs;
            cmd.Parameters.AddWithValue("@status", ddlStatus.SelectedItem.Value);



            cmd.Connection = con;
            con.Open();
            //use Command method to execute UPDATE statement and return 
            //boolean if number of records UPDATED is greater than zero
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();
            if (IsUpdated)
            {
                if (txtcomment.Text != "")
                {
                    addComment();
                }

                CommentData();
                getStatus();
                TicketData();
                updatenotif();
                txtcomment.Text = "";
            }
            else
            {
                lblmsg.Text = "Error while updating this ticket";
                lblmsg.ForeColor = System.Drawing.Color.Red;
            }
        }

        public void getCategory()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblFaq_category";
            cmd.Connection = con;
            //Create DataAdapter

            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create a Datatable or a DataSet

            DataTable dt = new DataTable();

            //Fill the Dataset and ensure the DB Connection is closed 
            using (da)
            {
                da.Fill(dt);
            }

            //To load country names in dropdown
            ddlCategory.DataSource = dt;
            //assign a field name and id to ddl
            ddlCategory.DataTextField = "Category";
            ddlCategory.DataValueField = "id";
            ddlCategory.DataBind();
        }

        protected void addComment()
        {
            String qs = Request.QueryString["Parameter"].ToString();
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.Connection = con2;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd2.CommandText = "insert into tblSupportComment ([Ticket_id], [Comment], [Username], [Date]) values (@ticket, @comment, @user, @date)";
            //Create a parametererized query for CatID
            cmd2.Parameters.AddWithValue("@ticket", qs);
            cmd2.Parameters.AddWithValue("@comment", txtcomment.Text.Trim());
            cmd2.Parameters.AddWithValue("@user", txtLastName.Text.Trim());
            cmd2.Parameters.AddWithValue("@date", DateTime.Now);
            con2.Open();
            Boolean IsAdded = false;
            IsAdded = cmd2.ExecuteNonQuery() > 0;
            con2.Close();
            if (IsAdded)
            {


                txtcomment.Text = "";

                lblmsg.Text = "The ticket has been updated!";
                lblmsg.ForeColor = System.Drawing.Color.Green;



            }

        }

        protected void getNotifID()
        {

            String qs = Request.QueryString["Parameter"].ToString();
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
          
            cmd.CommandText = "select id  from tblNotification where Villa_id=" + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                notif_id = dr["id"].ToString();
            }

        }

        protected void updatenotif()
        {
            getNotifID();


            int qs = Convert.ToInt32(Request.QueryString["ID"]);

            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.Connection = con;
            cmd.CommandText = "update tblNotification set State=@State where id=" + notif_id;
            cmd.Parameters.AddWithValue("@State", 1);
            con.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd.ExecuteNonQuery() > 0;
            con.Close();

        }
    }
}
