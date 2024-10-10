using System;
using System.Data;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Web.Configuration;
using System.Web.UI;

namespace DealProjectTamam
{
    public partial class PaymentForm : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                Session["Page_name"] = "payment";
                LoadUserDetails();
            }
        }

        protected void LoadUserDetails()
        {
            string bookingId = Request.QueryString["Parameter"];
            if (string.IsNullOrEmpty(bookingId))
            {
                lblError.Text = "Booking ID is missing.";
                return;
            }

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    Connection = con,
                    CommandType = CommandType.Text,
                    CommandText = "SELECT c.Client_fname, c.Client_lname, c.Client_email, b.BK_amnt " +
                                  "FROM tblBooking b " +
                                  "JOIN tblClient c ON b.Client_id = c.Client_id " +
                                  "WHERE b.Bk_id = @bookingId"
                };
                cmd.Parameters.AddWithValue("@bookingId", bookingId);

                con.Open();
                using (SqlDataReader dr = cmd.ExecuteReader())
                {
                    if (dr.Read())
                    {
                        txtName.Text = dr["Client_fname"].ToString() + " " + dr["Client_lname"].ToString();
                        txtEmail.Text = dr["Client_email"].ToString();
                        txtAmount.Text = dr["BK_amnt"].ToString();
                    }
                    else
                    {
                        lblError.Text = "Booking details not found.";
                    }
                }
            }
        }

        protected void ddlPaymentMethod_SelectedIndexChanged(object sender, EventArgs e)
        {
            pnlJuice.Visible = ddlPaymentMethod.SelectedValue == "1";
        }

        protected void btnSubmit_Click(object sender, EventArgs e)
        {
            lblError.Text = string.Empty;
            lblSuccess.Text = string.Empty;

            if (ddlPaymentMethod.SelectedValue == "-1")
            {
                lblError.Text = "Please select a payment method.";
                return;
            }

            if (!CheckBoxChecked())
            {
                lblError.Text = "You must agree to the booking conditions.";
                return;
            }

            InsertPayment();
        }

        private bool CheckBoxChecked()
        {
            foreach (string key in Request.Form.AllKeys)
            {
                if (key.Contains("check"))
                {
                    return true;
                }
            }
            return false;
        }

        protected void InsertPayment()
        {
            string bookingId = Request.QueryString["Parameter"];
            string amount = txtAmount.Text.Trim();

            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    Connection = con,
                    CommandText = "INSERT INTO tblPayment (Payment_date, Payment_total, Payment_amountpaid, Payment_status, Bk_id) " +
                                  "VALUES (@date, @total, @paid, @status, @Bk_id)"
                };

                cmd.Parameters.AddWithValue("@date", DateTime.Today);
                cmd.Parameters.AddWithValue("@total", amount);
                cmd.Parameters.AddWithValue("@paid", amount);
                cmd.Parameters.AddWithValue("@status", 1);
                cmd.Parameters.AddWithValue("@Bk_id", bookingId);

                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    UpdateBookingStatus(bookingId);
                    SendPaymentEmail(); 
                }
                else
                {
                    lblError.Text = "Error while saving payment.";
                }
            }
        }

        protected void UpdateBookingStatus(string bookingId)
        {
            using (SqlConnection con = new SqlConnection(_conString))
            {
                SqlCommand cmd = new SqlCommand
                {
                    CommandType = CommandType.Text,
                    Connection = con,
                    CommandText = "UPDATE tblBooking SET Bk_state = 'B' WHERE Bk_id = @bookingId"
                };
                cmd.Parameters.AddWithValue("@bookingId", bookingId);

                con.Open();
                if (cmd.ExecuteNonQuery() > 0)
                {
                    lblSuccess.Text = "Payment Saved. Wait for further validation.";
                }
                else
                {
                    lblError.Text = "Error while updating booking status.";
                }
            }
        }

        protected void btnJuice_Click(object sender, EventArgs e)
        {
            btnSubmit_Click(sender, e);
        }

        private void SendPaymentEmail()
        {
            string user_mail = txtEmail.Text.Trim();
            string clientName = txtName.Text.Trim();
            string amount = txtAmount.Text.Trim();

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
                mail.Subject = "[Deal Tamam] Payment Confirmation";
                mail.IsBodyHtml = true;

                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("<html><head>");
                msgBody.Append("<style>");
                msgBody.Append("body {font-family: Arial, sans-serif; background-color: #f0f2f5; padding: 20px;}");
                msgBody.Append(".container {background-color: #ffffff; padding: 20px; border-radius: 10px; max-width: 600px; margin: auto;}");
                msgBody.Append("h1 {background-color: #4CAF50; color: white; padding: 10px; border-radius: 5px;}");
                msgBody.Append("p {font-size: 16px; color: #333;}");
                msgBody.Append(".highlight {color: #FF5722; font-weight: bold;}");
                msgBody.Append("</style>");
                msgBody.Append("</head><body>");
                msgBody.Append("<div class='container'>");
                msgBody.Append("<center><h1>Payment Confirmation</h1></center>");
                msgBody.Append("<p>Hello <span class='highlight'>" + clientName + ",</span></p>");
                msgBody.Append("<p>We have received your payment. Here are the details:</p>");
                msgBody.Append("<p>Amount Paid: <span class='highlight'>" + amount + "</span></p>");
                msgBody.Append("<p>Payment Date: <span class='highlight'>" + DateTime.Now.ToString("dd MMM yyyy") + "</span></p>");
                msgBody.Append("<p class='highlight'>Thank you for your payment.</p>");
                msgBody.Append("</div>");
                msgBody.Append("</body></html>");

                mail.Body = msgBody.ToString();

                sc.Send(mail);
                lblSuccess.Text += " A confirmation email has been sent.";
            }
            catch (Exception ex)
            {
                lblError.Text = "Failed to send confirmation email. Error: " + ex.Message;
            }
        }
    }
}
