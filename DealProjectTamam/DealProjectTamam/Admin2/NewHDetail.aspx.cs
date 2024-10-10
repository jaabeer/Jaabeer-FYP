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
using Twilio;
using Twilio.Rest.Api.V2010.Account;
using Twilio.Types;
using System.Net.Http;
using Twilio.Exceptions;
using WebGrease.Css.Ast;

namespace DealProjectTamam
{
    public partial class NewHDetail : System.Web.UI.Page
    {
        String Prop_name_old;
        String Prop_phone_old;
        String Prop_email_old;
        String Prop_street_old;
        String Prop_town_old;
        String Prop_priceday_old;
        String Prop_postcode_old;
        String ddlDistrict_old;
        String type;
        String prop_id;


        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        String Sms_reject, Sms_approve, Owner_name;
        protected void Page_Load(object sender, EventArgs e)
        {

            if (!Page.IsPostBack)
            {
                getVillaID();
                pnlUpdate.Attributes["style"] = "display:none";
                get_districts();
                Populate_owner_loc_price();
                Populate_dlstImages();
                populate_details_dropdown();
                getDetails();
                populate_facilities_dropdown();
                getFacilities();
                set_owner_loc_price_readonly();
                checkNotiftype();

                pnlComment.Attributes["style"] = "display:none";





            }
        }


        void get_districts()
        {

            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDistrict";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Dist_name"] + " " + "(" + (String)dr["Dist_region"] + ")";
                newItem.Value = dr["Dist_id"].ToString();
                ddlDistrict.Items.Add(newItem);
            }

        }

        void Populate_owner_loc_price()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
          
            cmd.CommandText = "SELECT * FROM tblHotel th inner join tblDistrict td on td.Dist_id = th.Dist_id inner join tblOwner tow on tow.Own_id = th.Own_id where th.Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", prop_id);
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtProp_id.Text = dr["Hotel_id"].ToString();
                txtProp_name.Text = dr["Hotel_name"].ToString();
                txtProp_phone.Text = dr["Hotel_phone"].ToString();
                txtProp_email.Text = dr["Hotel_email"].ToString();
                txtProp_street.Text = dr["Hotel_street"].ToString();
                txtProp_town.Text = dr["Hotel_town"].ToString();
                txtProp_priceday.Text = dr["Hotel_price"].ToString();
                txtProp_postcode.Text = dr["Hotel_postcode"].ToString();
                txt_Description.Text = dr["Hotel_desc"].ToString();
               
                ddlDistrict.Items[0].Text = (dr["Dist_name"].ToString() + " " + "(" + dr["Dist_region"].ToString() + ")");
                ddlDistrict.Items[0].Value = dr["Dist_id"].ToString();


           


                lbl_owner.Text = dr["Own_fname"].ToString() + " " + dr["Own_lname"].ToString();

                img_main.ImageUrl = "~/Property/" + dr["Hotel_id"].ToString() + "/main/" + dr["Hotel_image"].ToString();

            }
        }

        void Populate_dlstImages()
        {
            
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblImage where Hotel_id = " + prop_id;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
            }
            con.Open();
            dlstImages.DataSource = dt;
            dlstImages.DataBind();
        }

        void populate_details_dropdown()
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDetails";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Det_name"];
                newItem.Value = dr["Det_id"].ToString();
                ddlDet_name.Items.Add(newItem);
            }
        }

        protected void gvs_2_PreRender(object sender, EventArgs e)
        {
            if (gvs_2.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_2.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_2.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
        }

        protected void gvs_2_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Read data from GridView and Populate the form
            txtDet_Id.Text = (gvs_2.DataKeys[gvs_2.SelectedIndex].Value.ToString());
            ddlDet_name.SelectedValue = (gvs_2.DataKeys[gvs_2.SelectedIndex].Value.ToString());
            txtCount.Text = ((Label)gvs_2.SelectedRow.FindControl("lblCount")).Text;


        }

        protected void gvs_2_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_2.PageIndex = e.NewPageIndex;
            getDetails();
        }

        void getDetails()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblDetails td inner join tblDetails_Hotel tdh on tdh.Det_id=td.Det_id where tdh.Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            gvs_2.DataSource = dt;
            gvs_2.DataBind();
        }

        void populate_facilities_dropdown()
        {
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFacilities";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Fac_name"];
                newItem.Value = dr["Fac_id"].ToString();
                ddl_Facility.Items.Add(newItem);
            }
        }

        void getFacilities()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * from tblFacilities tf inner join tblFacilities_Hotel tfh on tfh.Fac_id=tf.Fac_id where tfh.Hotel_id=@Hotel_id";
            cmd.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.ToString());
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                da.Fill(dt);
            }
            gvs_3.DataSource = dt;
            gvs_3.DataBind();
        }

        protected void gvs_3_PreRender(object sender, EventArgs e)
        {
            if (gvs_3.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_3.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_3.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
        }

        protected void gvs_3_SelectedIndexChanged(object sender, EventArgs e)
        {

            //Read data from GridView and Populate the form
            txt_Facility_ID.Text = (gvs_3.DataKeys[gvs_3.SelectedIndex].Value.ToString());
            ddl_Facility.SelectedValue = (gvs_3.DataKeys[gvs_3.SelectedIndex].Value.ToString());



        }

        protected void gvs_3_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_3.PageIndex = e.NewPageIndex;
            getFacilities();
        }

        void set_owner_loc_price_readonly()
        {
            txtProp_name.ReadOnly = true;
        
            txtProp_town.ReadOnly = true;
            txtProp_priceday.ReadOnly = true;
            txtProp_phone.ReadOnly = true;
            txtProp_town.ReadOnly = true;
            txtProp_street.ReadOnly = true;
            txtProp_postcode.ReadOnly = true;
            ddlDistrict.Enabled = false;

            ddlDistrict.CssClass = "form-control form-control-user";

            txt_Description.ReadOnly = true;
         


        }

        void set_owner_loc_price_Editable()
        {
            txtProp_name.ReadOnly = false;
           
            txtProp_town.ReadOnly = false;
            txtProp_priceday.ReadOnly = false;
            txtProp_phone.ReadOnly = false;
            txtProp_town.ReadOnly = false;
            txtProp_street.ReadOnly = false;
            txtProp_postcode.ReadOnly = false;
            ddlDistrict.Enabled = true;
        }




        void retrieve_and_store_old_values()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
        
            cmd.CommandText = "SELECT * FROM tblHotel th inner join tblDistrict td on td.Dist_id = th.Dist_id where th.Hotel_id = " + prop_id;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txtProp_id.Text = dr["Hotel_id"].ToString();
                Prop_name_old = dr["Hotel_name"].ToString();
                Prop_phone_old = dr["Hotel_phone"].ToString();
                Prop_email_old = dr["Hotel_email"].ToString();
                Prop_street_old = dr["Hotel_street"].ToString();
                Prop_town_old = dr["Hotel_town"].ToString();
                Prop_priceday_old = dr["Hotel_price"].ToString();
                Prop_postcode_old = dr["Hotel_postcode"].ToString();
              
                ddlDistrict_old = (dr["Dist_id"].ToString());

            }
        }

        protected void LinkButton1_Click(object sender, EventArgs e)
        {
            pnlComment.Attributes["style"] = "display:block";



        }

        protected void lnkapprove_Click(object sender, EventArgs e)
        {

            SqlConnection con4 = new SqlConnection(_conString);
            SqlCommand cmd4 = new SqlCommand();
            cmd4.CommandType = CommandType.Text;
            cmd4.Connection = con4;
            //Add DELETE statement to delete the selected category for the above CatID
            cmd4.CommandText = "update tblHotel set Hotel_status=@status where Hotel_id=@Hotel_id";

            cmd4.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
            cmd4.Parameters.AddWithValue("@status", 1);

            con4.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd4.ExecuteNonQuery() > 0;
            con4.Close();
            if (IsUpdated)
            {
                int qs = Convert.ToInt32(Request.QueryString["ID"]);
                SqlConnection con5 = new SqlConnection(_conString);
                SqlCommand cmd5 = new SqlCommand();
                cmd5.CommandType = CommandType.Text;
                cmd5.Connection = con5;

                cmd5.CommandText = "update tblNotification set State=@status, Comment=@comment where id=" + qs;

                cmd5.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
                cmd5.Parameters.AddWithValue("@status", 1);
                cmd5.Parameters.AddWithValue("@comment", "The Hotel " + txtProp_name.Text.Trim() + " has been approved on " + DateTime.Today + ".");
                con5.Open();
                Boolean IsUpdated1 = false;
                IsUpdated1 = cmd5.ExecuteNonQuery() > 0;
                con5.Close();
                if (IsUpdated1)
                {

                   

                    sendMailApprove();
                    Response.Redirect("~/Admin2/New_hotel.aspx");
                }

            }
            else
            {
                lblMsg.Text = "Error while  validating the Hotel " + txtProp_name.Text.Trim();
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }





        }

        protected void sendMailApprove()
        {
            string email_add = txtProp_email.Text;
            string user_name = txtProp_name.Text;
            Owner_name = lbl_owner.Text;


            text_msg_approve();
            sendApprovemsg(Sms_approve);
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                m.From = new MailAddress("dealtamam02@gmail.com");
                m.To.Add(email_add);
                m.Subject = "Hotel Approval Confirmation";
                m.IsBodyHtml = true;
                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("Dear " + Owner_name + ", <br/> The request to validate your Hotel,  " + txtProp_name.Text.Trim() + " has been validated on the " + DateTime.Today + ".<br/> For any additional request, please contact the support team. <br/> Regards Deal Tamam");



                m.Body = msgBody.ToString();
                sc.Host = "smtp.office365.com";
                sc.Port = 587;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new
                System.Net.NetworkCredential("dealtamam02@gmail.com", "Deal12345?");
                sc.EnableSsl = true;
                sc.Send(m);
                Response.Write("Validation mail Sent successfully");


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }


        }

        protected void btnReject_Click(object sender, EventArgs e)
        {

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con5 = new SqlConnection(_conString);
            SqlCommand cmd5 = new SqlCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.Connection = con5;
           
            cmd5.CommandText = "update tblNotification set State=@status, Comment=@comment where id=" + qs;

            cmd5.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
            cmd5.Parameters.AddWithValue("@status", 1);
            cmd5.Parameters.AddWithValue("@comment", "The Hotel " + txtProp_name.Text.Trim() + " has been reject on " + DateTime.Today + " as " + txtComment.Text.Trim());

            con5.Open();
            Boolean IsUpdated = false;
            IsUpdated = cmd5.ExecuteNonQuery() > 0;
            con5.Close();
            if (IsUpdated)
            {
                sendMailReject();
                Response.Redirect("~/Admin2/New_hotel.aspx");

            }
            else
            {
                lblMsg.Text = "Error while  rejecting the hotel " + txtProp_name.Text;
                lblMsg.ForeColor = System.Drawing.Color.Red;

            }


        }


        protected void sendMailReject()
        {
            string email_add = txtProp_email.Text;
            string user_name = txtProp_name.Text;
            Owner_name = lbl_owner.Text; ;



            text_msg_reject();
            sendRejectmsg(Sms_reject);
            MailMessage m = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                m.From = new MailAddress("dealtamam02@gmail.com");
                m.To.Add(email_add);
                m.Subject = "Hotel Rejection Confirmation";
                m.IsBodyHtml = true;
                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("Dear " + Owner_name + ", <br/> The request to validate your Hotel,  " + txtProp_name.Text.Trim() + " has been <b>rejected</b> on the " + DateTime.Today + ".<br/> For any additional request, please contact the support team. <br/> Regards Deal Tamam");



                m.Body = msgBody.ToString();
                sc.Host = "smtp.office365.com";
                sc.Port = 587;
                sc.UseDefaultCredentials = false;
                sc.Credentials = new
                System.Net.NetworkCredential("dealtamam02@gmail.com", "Deal12345?");
                sc.EnableSsl = true;
                sc.Send(m);
                Response.Write("Rejection mail Sent successfully");


            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }


        }

        protected void checkNotiftype()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
        
            cmd.CommandText = "select Distinct Notif_type  from tblNotification where id=" + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                type = dr["Notif_type"].ToString();
            }

            if (type == "notif_up" || type == "update")
            {
                pnlUpdate.Attributes["style"] = "display:block";
                lnkapprove.Attributes["style"] = "display:none";
                lnkreject.Attributes["style"] = "display:none";

            }
            else
            {

            }
        }

        protected void btnRead_Click(object sender, EventArgs e)
        {

            String comment = txt_optcomment.Text.Trim();

            if (comment == "")
            {
                comment = "Admin verified";
            }

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con5 = new SqlConnection(_conString);
            SqlCommand cmd5 = new SqlCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.Connection = con5;

            cmd5.CommandText = "update tblNotification set State=@status, Comment=@comment where id=" + qs;

            cmd5.Parameters.AddWithValue("@Hotel_id", txtProp_id.Text.Trim());
            cmd5.Parameters.AddWithValue("@status", 1);
            cmd5.Parameters.AddWithValue("@comment", comment);
            con5.Open();
            Boolean IsUpdated1 = false;
            IsUpdated1 = cmd5.ExecuteNonQuery() > 0;
            con5.Close();
            if (IsUpdated1)
            {
                Response.Redirect("~/Admin2/New_hotel.aspx");


            }

        }


        protected void getVillaID()
        {

            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
         
            cmd.CommandText = "select Hotel_id  from tblNotification where id=" + qs;
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                prop_id = dr["Hotel_id"].ToString();
            }

        }



        protected void text_msg_reject()
        {

            Sms_reject = "Dear " + Owner_name + ",  The request to validate your Hotel,  " + txtProp_name.Text.Trim() + " has been rejected on the " + DateTime.Today + ". For any additional request, please contact the support team. <br/> Regards Deal Tamam";
        }

        protected void text_msg_approve()
        {

            Sms_approve = "Dear " + Owner_name + ",  The request to validate your Hotel,  " + txtProp_name.Text.Trim() + " has been validated on the " + DateTime.Today + ". For any additional request, please contact the support team. <br/> Regards Deal Tamam";
        }


        void sendRejectmsg(String text)
        {


            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(); ;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from tblOwner where Own_email=@email";
            cmd.Parameters.AddWithValue("@email", txtProp_email.Text.Trim());

            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        var accountSid = "AC3482da7e0b13e543691232f480f05f36";
                        var authToken = "730b6b56d244a0a99130c018f81b56af";
                        TwilioClient.Init(accountSid, authToken);

                        var client_phone_num = "+230" + dr["Own_mobile"].ToString();

                        var message = MessageResource.Create(to: new Twilio.Types.PhoneNumber(client_phone_num), from: new Twilio.Types.PhoneNumber("+15189636978"), body: text);

                        Console.WriteLine(message.Body);
                    }
                    catch (ApiException e)

                    {
                        //handle exception state here
                    }
                }

            }
            else
            {
                lblMsg.Text = "Error";
            }


        }


        void sendApprovemsg(String text)
        {


            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand(); ;
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;


            cmd.CommandText = "select * from tblOwner where Own_email=@email";
            cmd.Parameters.AddWithValue("@email", txtProp_email.Text.Trim());

            //Create DataReader
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            // check if the DataReader contains a record
            if (dr.HasRows)
            {
                while (dr.Read())
                {
                    try
                    {
                        var accountSid = "AC3482da7e0b13e543691232f480f05f36";
                        var authToken = "730b6b56d244a0a99130c018f81b56af";
                        TwilioClient.Init(accountSid, authToken);

                        var client_phone_num = "+230" + dr["Own_mobile"].ToString();

                        var message = MessageResource.Create(to: new Twilio.Types.PhoneNumber(client_phone_num), from: new Twilio.Types.PhoneNumber("+15189636978"), body: text);

                        Console.WriteLine(message.Body);
                    }
                    catch (ApiException e)

                    {
                        //handle exception state here
                    }
                }

            }
            else
            {
                lblMsg.Text = "Error";
            }


        }

    }

}
