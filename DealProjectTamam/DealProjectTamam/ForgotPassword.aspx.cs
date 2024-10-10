using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Net.Mail;
using System.Text;
using System.Web.UI;

namespace DealProjectTamam
{
    public partial class ForgotPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {

        }

        protected void btnSendOTP_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string userType = GetUserTypeByEmail(email);

            if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userType))
            {
                // Generate OTP
                string otp = GenerateOTP();
                // Save OTP to the database
                SaveOTPToDatabase(email, otp);
                // Send OTP via email
                SendOTPEmail(email, otp);
                lblMessage.Text = "OTP has been sent to your email.";

                // Redirect to ResetPassword.aspx
                Response.Redirect($"ResetPassword.aspx?email={email}&type={userType}");
            }
            else
            {
                lblMessage.Text = "Please enter a valid email address.";
            }
        }

        private string GetUserTypeByEmail(string email)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                // Check in Client table
                string query = "SELECT Client_email FROM tblClient WHERE Client_email=@Email";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return "Client";
                }
                reader.Close();

                // Check in Owner table
                query = "SELECT Own_email FROM tblOwner WHERE Own_email=@Email";
                cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    return "Owner";
                }
                con.Close();
            }
            return null; // email not found in either table
        }

        private string GenerateOTP()
        {
            Random rand = new Random();
            return rand.Next(100000, 999999).ToString(); // Generate a 6-digit OTP
        }

        private void SaveOTPToDatabase(string email, string otp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "INSERT INTO tblEmailOTP (User_email, OTP, time_stamp, Status, type) VALUES (@UserEmail, @OTP, @Timestamp, @Status, @Type)";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@UserEmail", email);
                cmd.Parameters.AddWithValue("@OTP", otp);
                cmd.Parameters.AddWithValue("@Timestamp", DateTime.Now);
                cmd.Parameters.AddWithValue("@Status", 1); // Active
                cmd.Parameters.AddWithValue("@Type", "PasswordReset");
                cmd.ExecuteNonQuery();
                con.Close();
            }
        }

        private void SendOTPEmail(string email, string otp)
        {
            MailMessage mail = new MailMessage();
            SmtpClient sc = new SmtpClient();
            try
            {
                mail.From = new MailAddress("dealtamam02@gmail.com");
                mail.To.Add(email);
                mail.Subject = "Password Reset OTP";
                mail.IsBodyHtml = true;
                StringBuilder msgBody = new StringBuilder();
                msgBody.Append("<p>Here is your OTP to reset your password: <strong>" + otp + "</strong></p>");
                mail.Body = msgBody.ToString();

                sc.Host = "smtp.gmail.com";
                sc.Port = 587;
                sc.UseDefaultCredentials = true;
                sc.Credentials = new System.Net.NetworkCredential("dealtamam02@gmail.com", "ovaujhexpehtkhwz");
                sc.EnableSsl = true;
                sc.Send(mail);
            }
            catch (Exception ex)
            {
                Response.Write(ex.Message);
            }
        }
    }
}
