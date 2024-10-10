using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web.UI;

namespace DealProjectTamam
{
    public partial class ResetPassword : Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string email = Request.QueryString["email"];
                string userType = Request.QueryString["type"];

                if (!string.IsNullOrEmpty(email) && !string.IsNullOrEmpty(userType))
                {
                    txtEmail.Text = email;
                    txtEmail.Enabled = false; // Disable email field to prevent changes
                }
                else
                {
                    lblMessage.Text = "Invalid email or user type.";
                    btnResetPassword.Enabled = false;
                }
            }
        }

        protected void btnResetPassword_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string otp = txtOTP.Text.Trim();
            string newPassword = txtNewPassword.Text.Trim();

            if (VerifyOTP(email, otp))
            {
                string hashedPassword = HashPassword(newPassword);
                ResetPasswords(email, hashedPassword);
                lblMessage.Text = "Your password has been reset.";
                lblMessage.ForeColor = System.Drawing.Color.Green; // Set message color to green
            }
            else
            {
                lblMessage.Text = "Invalid OTP.";
                lblMessage.ForeColor = System.Drawing.Color.Red; // Ensure error messages are red
            }
        }

        private bool VerifyOTP(string email, string otp)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();
                string query = "SELECT Id FROM tblEmailOTP WHERE User_email=@Email AND OTP=@OTP AND Status=1 AND type='PasswordReset'";
                SqlCommand cmd = new SqlCommand(query, con);
                cmd.Parameters.AddWithValue("@Email", email);
                cmd.Parameters.AddWithValue("@OTP", otp);
                SqlDataReader reader = cmd.ExecuteReader();
                if (reader.HasRows)
                {
                    reader.Close();
                    // Invalidate the OTP after use
                    string updateQuery = "UPDATE tblEmailOTP SET Status=0 WHERE User_email=@Email AND OTP=@OTP";
                    SqlCommand updateCmd = new SqlCommand(updateQuery, con);
                    updateCmd.Parameters.AddWithValue("@Email", email);
                    updateCmd.Parameters.AddWithValue("@OTP", otp);
                    updateCmd.ExecuteNonQuery();
                    return true;
                }
                con.Close();
            }
            return false;
        }

        private void ResetPasswords(string email, string hashedPassword)
        {
            string connectionString = ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

            using (SqlConnection con = new SqlConnection(connectionString))
            {
                con.Open();

                // Update Client table if email exists
                string clientQuery = "UPDATE tblClient SET Client_password=@Password WHERE Client_email=@Email";
                SqlCommand clientCmd = new SqlCommand(clientQuery, con);
                clientCmd.Parameters.AddWithValue("@Email", email);
                clientCmd.Parameters.AddWithValue("@Password", hashedPassword);
                int clientRowsAffected = clientCmd.ExecuteNonQuery();

                // Update Owner table if email exists
                string ownerQuery = "UPDATE tblOwner SET Own_password=@Password WHERE Own_email=@Email";
                SqlCommand ownerCmd = new SqlCommand(ownerQuery, con);
                ownerCmd.Parameters.AddWithValue("@Email", email);
                ownerCmd.Parameters.AddWithValue("@Password", hashedPassword);
                int ownerRowsAffected = ownerCmd.ExecuteNonQuery();

                con.Close();

                // Check if neither table was updated
                if (clientRowsAffected == 0 && ownerRowsAffected == 0)
                {
                    lblMessage.Text = "Email not found in either table.";
                    lblMessage.ForeColor = System.Drawing.Color.Red; // Ensure error messages are red
                }
            }
        }

        private string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                for (int i = 0; i < bytes.Length; i++)
                {
                    builder.Append(bytes[i].ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}
