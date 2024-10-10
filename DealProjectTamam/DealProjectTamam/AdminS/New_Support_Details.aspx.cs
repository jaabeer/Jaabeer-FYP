using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Web.Configuration;
using System.Data.SqlClient;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
using System.Text;

namespace DealProjectTamam.AdminS
{
    public partial class New_Support_Details : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        String reference;
        string notif_id;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                getStatus();

                ddlstatus.Text = "Select Support Status";
                ListItem li1 = new ListItem("Select Status", "-1");
                ddlstatus.Items.Insert(0, li1);


                CommentData();
                SupportData();

            }
        }

        protected void SupportData()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblSupport ts inner join tblFaq_category tc on tc.id = ts.category_id inner join tblStatus s on ts.status=s.status_id  where ts.id = " + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                txtid.Text = dr["id"].ToString().Trim();
                txtfname.Text = dr["firstname"].ToString().Trim();
                txtlname.Text = dr["lastname"].ToString().Trim();
                txtemail.Text = dr["email"].ToString().Trim();
                txtphone.Text = dr["mobile"].ToString().Trim();
                txtcategory.Text = dr["Category"].ToString().Trim();
                txttitle.Text = dr["title"].ToString().Trim();
                txtdescription.Text = dr["description"].ToString().Trim();
                ddlstatus.SelectedValue = dr["status"].ToString().Trim();
                reference = dr["reference"].ToString().Trim();
            }
        }

        protected void btncomment_Click(object sender, EventArgs e)
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            Boolean IsUpdated = false;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "update tblSupport set status=@status where id=" + qs;
            cmd.Parameters.AddWithValue("@status", ddlstatus.SelectedItem.Value);



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
                updatenotif();
            }
            else
            {
                lblcommentmsg.Text = "Error while updating this ticket";
                lblcommentmsg.ForeColor = System.Drawing.Color.Red;
            }


        }

        protected void CommentData()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
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

        protected void updatenotif()
        {
            getNotifID();

            String status = ddlstatus.SelectedValue;

            if (status != "2")
            {
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

        protected void addComment()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.Connection = con2;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd2.CommandText = "insert into tblSupportComment ([Ticket_id], [Comment], [Username], [Date]) values (@ticket, @comment, @user, @date)";
            //Create a parametererized query for CatID
            cmd2.Parameters.AddWithValue("@ticket", qs);
            cmd2.Parameters.AddWithValue("@comment", txtcomment.Text.Trim());
            cmd2.Parameters.AddWithValue("@user", "Support");
            cmd2.Parameters.AddWithValue("@date", DateTime.Now);
            con2.Open();
            Boolean IsAdded = false;
            IsAdded = cmd2.ExecuteNonQuery() > 0;
            con2.Close();
            if (IsAdded)
            {

                sendMail();
                txtcomment.Text = "";

                lblcommentmsg.Text = "The ticket has been updated!";
                lblcommentmsg.ForeColor = System.Drawing.Color.Green;



            }

        }

        protected void getNotifID()
        {

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
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

        public void getStatus()
        {
            // Create Connection
            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblStatus where status_cat='support'";
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
            ddlstatus.DataSource = dt;
            //assign a field name and id to ddl
            ddlstatus.DataTextField = "status_name";
            ddlstatus.DataValueField = "status_id";
            ddlstatus.DataBind();
        }

        protected void sendMail()
        {


            String user_mail = txtemail.Text.Trim();

            MailMessage mail = new MailMessage();
            SmtpClient sc = new SmtpClient("smtp.gmail.com", 587)
            {
                Credentials = new System.Net.NetworkCredential("dealtamam02@gmail.com", "ovaujhexpehtkhwz"),
                EnableSsl = true
            };

            try
            {
                mail.From = new MailAddress("dealtamam02@gmail.com");
                mail.To.Add(new MailAddress(user_mail));
                mail.Subject = "[Support Deal Tamam] Ticket Ref:" + reference;
                mail.IsBodyHtml = true;

                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("<center><h1 style='background-color:#D0E0E3; color:#333;'>Deal Tamam Support Ticket</h1></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:#555;'>Hello <strong>" + txtlname.Text.Trim() + ",</strong> </p></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:#555;'>Response on your ticket with Reference: <strong style='color:#5577AA;'>" + reference + "</strong></p></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:#555;'>Status of your ticket has changed to <strong style='color:#5577AA;'>" + ddlstatus.SelectedItem.Text.Trim() + "</strong></p></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:#555;'>Your Title: <strong style='color:#5577AA;'>" + txttitle.Text.Trim() + "</strong></p></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:#555;'>Your issue: <strong style='color:#5577AA;'>" + txtdescription.Text.Trim() + "</strong><br/> Dated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:#555;'>Comment: <strong style='color:#5577AA;'>" + txtcomment.Text.Trim() + "</strong><br/> Dated: " + DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss") + "</p></center>");
                msgBody.Append("<center><p style='font-size:18px; font-family:Arial, sans-serif; color:red;'><strong>Important:</strong> This is an automatic e-mail. Do not reply.</p></center>");

                mail.Body = msgBody.ToString();

                // Adding the background image as an embedded resource
                AlternateView htmlView = AlternateView.CreateAlternateViewFromString(mail.Body, null, "text/html");
                LinkedResource background = new LinkedResource(Server.MapPath("~/images/home/slider3.jpg"), "image/jpeg");
                background.ContentId = "backgroundImage";
                htmlView.LinkedResources.Add(background);
                mail.AlternateViews.Add(htmlView);

                sc.Send(mail);
                Response.Write("Support request email sent successfully.");
            }
            catch (Exception ex)
            {
                Response.Write("Failed to send email. Error: " + ex.Message);
            }
        }

    }
}