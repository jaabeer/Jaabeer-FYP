using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Data;
using System.Data.SqlClient;
using System.Text;
using System.Web.Configuration;
using System.IO;
using System.Security.Cryptography;
using System.Net.Mail;
namespace DealProjectTamam
{
    public partial class Customer_sup : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        string ticket_id, count, reference;
        int ref_no;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "Support";

            if (!Page.IsPostBack)
            {
                getCategory();
                ListItem li1 = new ListItem("Select a category for your issue", "-1");
                ddlCategory.Items.Insert(0, li1);


                ResetAll();
                lblmsg.Text = "";

                if (Session["owner_email"] != null)
                {
                    getUserDetail();
                }


            }
        }

        protected void btnSend_Click(object sender, EventArgs e)
        {


            if (ddlCategory.SelectedValue != "-1")
            {
                lblcat.Text = "";
                string category = ddlCategory.SelectedValue;
                string attachment;

                if (fpdocs.HasFile)
                {
                    attachment = Path.GetFileName(fpdocs.PostedFile.FileName);
                    fpdocs.PostedFile.SaveAs(Server.MapPath("~/Support/") + attachment);
                }
                else
                {
                    attachment = "";
                }



                Reference();

                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.CommandType = CommandType.Text;
                cmd.Connection = con;
                cmd.CommandText = "INSERT INTO tblSupport ([firstname], [lastname], [email], [mobile], [category_id], [title], [description], [attachment], [status],[reference], [created_date]) VALUES (@fname, @lname, @email, @mobile, @category, @title, @dspt, @files, @status,@ref, @date)";
                cmd.Parameters.AddWithValue("@fname", txtFirstName.Text.Trim());
                cmd.Parameters.AddWithValue("@lname", txtLastName.Text.Trim());
                cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());
                cmd.Parameters.AddWithValue("@mobile", txtMobile.Text.Trim());
                cmd.Parameters.AddWithValue("@category", category);
                cmd.Parameters.AddWithValue("@title", txttitle.Text.Trim());
                cmd.Parameters.AddWithValue("@dspt", txtdspt.Text.Trim());
                cmd.Parameters.AddWithValue("@files", attachment);
                cmd.Parameters.AddWithValue("@status", "1");
                cmd.Parameters.AddWithValue("@ref", reference);
                cmd.Parameters.AddWithValue("@date", DateTime.Now);

                con.Open();


                Boolean IsAdded = false;
                IsAdded = cmd.ExecuteNonQuery() > 0;
                con.Close();
                if (IsAdded)
                {
                    AddNotification();
                    btnsuccess.Visible = true;
                    btnSend.Visible = false;

                    sendMail();
                    lblmsg.Text = "Support Request Sent succesfully";
                    lblmsg.ForeColor = System.Drawing.Color.Green;
                    ResetAll();

                }
                else
                {
                    lblmsg.Text = "An unexpected error occured, please try again";
                    lblmsg.ForeColor = System.Drawing.Color.Red;
                }

                con.Close();



            }
            else
            {
                lblcat.Text = "Please select a category for your issue!!";

            }



        }

        protected void Reference()
        {
            getcount();
            reference = "REF" + DateTime.Now.ToString("MMddyyyy") + ref_no;
        }

        public void ResetAll()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            txtEmail.Text = "";
            txtMobile.Text = "";
            txttitle.Text = "";
            txtdspt.Text = "";
            ddlCategory.SelectedValue = "-1";

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

        protected void FAQ_Question()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@cat", ddlCategory.SelectedIndex);
            cmd.CommandText = "SELECT * from tblFaq where faq_cat=@cat ";

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

        protected void ddlCategory_TextChanged(object sender, EventArgs e)
        {
            FAQ_Question();
        }

        protected void AddNotification()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
            cmd.CommandText = "select * from tblSupport where title=@title and email=@email";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@title", txttitle.Text.Trim());
            cmd.Parameters.AddWithValue("@email", txtEmail.Text.Trim());

            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ticket_id = dr["id"].ToString();
            }
            con.Close();

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "insert into tblNotification ([Notif_details], [Notif_type], [user_id], [Villa_id], [Hotel_id], [Notif_date], [State]) values (@details, @type, @user, @Villa, @Hotel, @date, @state)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@details", txttitle.Text);
                    cmd2.Parameters.AddWithValue("@type", "support");
                    cmd2.Parameters.AddWithValue("@user", "");
                    cmd2.Parameters.AddWithValue("@Villa", ticket_id);
                    cmd2.Parameters.AddWithValue("@Hotel", ticket_id);
                    cmd2.Parameters.AddWithValue("@date", DateTime.Now.ToString("MM/dd/yyyy"));
                    cmd2.Parameters.AddWithValue("@state", 0);


                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                }
            }
        }


        protected void getcount()
        {
            string counts;

            // Create Connection
            SqlConnection dbcon1 = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd1 = new SqlCommand();
            cmd1.Connection = dbcon1;
            cmd1.CommandType = CommandType.Text;
            cmd1.CommandText = "SELECT count(*) FROM tblSupport";

            dbcon1.Open();

            counts = cmd1.ExecuteScalar().ToString();
            dbcon1.Close();

            if (counts == "0")
            {
                ref_no = 1;
            }
            else
            {
                SqlConnection con = new SqlConnection(_conString);
                SqlCommand cmd = new SqlCommand();
                cmd.Connection = con;
                cmd.CommandType = CommandType.Text;


                cmd.CommandText = "select Top 1 id from tblSupport  order by Id DESC";
                SqlDataReader dr;
                con.Open();
                dr = cmd.ExecuteReader();
                while (dr.Read())
                {


                    count = dr["id"].ToString();
                    ref_no = int.Parse(count) + 1;



                }

            }

        }



        protected void sendMail()
        {
            String user_mail = txtEmail.Text.Trim();
            Reference();
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
                msgBody.Append("<html><head>");
                msgBody.Append("<style>");
                msgBody.Append("body {font-family: Arial, sans-serif; background-image: url('cid:backgroundImage'); background-size: cover; padding: 20px;}");
                msgBody.Append(".container {background-color: rgba(255, 255, 255, 0.85); padding: 20px; border-radius: 10px; max-width: 600px; margin: auto;}");
                msgBody.Append("h1 {background-color: #4CAF50; color: white; padding: 10px; border-radius: 5px;}");
                msgBody.Append("p {font-size: 16px; color: #333;}");
                msgBody.Append(".highlight {color: #FF5722; font-weight: bold;}");
                msgBody.Append(".important {color: red; font-weight: bold;}");
                msgBody.Append("</style>");
                msgBody.Append("</head><body>");
                msgBody.Append("<div class='container'>");
                msgBody.Append("<center><h1>Support Ticket Created</h1></center>");
                msgBody.Append("<p>Hello <span class='highlight'>" + txtLastName.Text.Trim() + ",</span></p>");
                msgBody.Append("<p>We have received your support request. Here are the details:</p>");
                msgBody.Append("<p>Reference Number: <span class='highlight'>" + reference + "</span></p>");
                msgBody.Append("<p>Category: <span class='highlight'>" + ddlCategory.SelectedItem.Text + "</span></p>");
                msgBody.Append("<p>Title: <span class='highlight'>" + txttitle.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Issue: <span class='highlight'>" + txtdspt.Text.Trim() + "</span></p>");
                msgBody.Append("<p>Date: <span class='highlight'>" + DateTime.Now + "</span></p>");
                msgBody.Append("<p class='important'>Important: Do not reply.</p>");
                msgBody.Append("</div>");
                msgBody.Append("</body></html>");

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




        protected void getUserDetail()
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblOwner where Own_email=@email";
            cmd.Parameters.AddWithValue("@email", Session["owner_email"]);
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {

                txtFirstName.Text = dr["Own_fname"].ToString().Trim();
                txtLastName.Text = dr["Own_lname"].ToString().Trim();
                txtEmail.Text = dr["Own_email"].ToString().Trim();
                txtMobile.Text = dr["Own_mobile"].ToString().Trim();

            }
        }
    }
}