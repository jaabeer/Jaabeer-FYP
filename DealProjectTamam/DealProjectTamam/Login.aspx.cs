using System;
using System.Configuration;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class Login : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            // Additional initialization code can be added here if needed
        }

        protected void btnLogin_Click(object sender, EventArgs e)
        {
            string email = txtEmail.Text.Trim();
            string password = txtPassword.Text;

            // Check for empty fields
            if (string.IsNullOrEmpty(email) && string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Email and Password are required.";
                return;
            }
            else if (string.IsNullOrEmpty(email))
            {
                lblMessage.Text = "Email is required.";
                return;
            }
            else if (string.IsNullOrEmpty(password))
            {
                lblMessage.Text = "Password is required.";
                return;
            }

            // Validate email format
            System.Text.RegularExpressions.Regex emailRegex = new System.Text.RegularExpressions.Regex(@"^[^@\s]+@[^@\s]+\.[^@\s]+$");
            if (!emailRegex.IsMatch(email))
            {
                lblMessage.Text = "Please enter a valid email address.";
                return;
            }

            string hashedPassword = HashPassword(password); // Hash the entered password

            string connectionString = ConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
            using (SqlConnection con = new SqlConnection(connectionString))
            {
                try
                {
                    con.Open();

                    // Check hardcoded admin credentials first
                    if ((email == "admin001.dealtamam@gmail.com" && password == "12345?") ||
                        (email == "Jaabeer@admin.com" && password == "Jaabeer12345?"))
                    {
                        // Redirect based on email
                        Response.Redirect(email == "admin001.dealtamam@gmail.com" ? "AdminS/AdminHomepage.aspx" : "Admin2/AdminHomepage1.aspx");
                    }
                    else
                    {
                        // Owner login check with hashed password
                        string ownerQuery = "SELECT Own_id, Own_fname, Own_lname, Own_email, Own_password FROM tblOwner WHERE Own_email=@Email AND Own_password=@Password";
                        SqlCommand ownerCmd = new SqlCommand(ownerQuery, con);
                        ownerCmd.Parameters.AddWithValue("@Email", email);
                        ownerCmd.Parameters.AddWithValue("@Password", hashedPassword); // Use the hashed password
                        SqlDataReader ownerReader = ownerCmd.ExecuteReader();

                        if (ownerReader.Read())
                        {
                            // Successful login as owner
                            Session["owner_email"] = ownerReader["Own_email"].ToString();
                            Session["owner_fname"] = ownerReader["Own_fname"].ToString();
                            Session["owner_lname"] = ownerReader["Own_lname"].ToString();
                            Session["owner_id"] = ownerReader["Own_id"].ToString();
                            Response.Redirect("OwnerHomepage.aspx");
                        }
                        else
                        {
                            ownerReader.Close();

                            // Client login check with hashed password
                            string clientQuery = "SELECT Client_id, Client_fname, Client_password, Client_status FROM tblClient WHERE Client_email=@Email AND Client_password=@Password AND Client_status = 1";
                            SqlCommand clientCmd = new SqlCommand(clientQuery, con);
                            clientCmd.Parameters.AddWithValue("@Email", email);
                            clientCmd.Parameters.AddWithValue("@Password", hashedPassword); // Use the hashed password
                            SqlDataReader clientReader = clientCmd.ExecuteReader();

                            if (clientReader.Read())
                            {
                                // Successful login as client
                                Session["client_email"] = email;
                                Session["client_id"] = clientReader["Client_id"];
                                Session["client_username"] = clientReader["Client_fname"].ToString();
                                Response.Redirect("ClientHome.aspx");
                            }
                            else
                            {
                                lblMessage.Text = "Invalid credentials. Please try again.";
                            }
                            clientReader.Close();
                        }
                    }
                }
                catch (Exception ex)
                {
                    // Handle potential errors (e.g., database connection errors)
                    lblMessage.Text = "An error occurred: " + ex.Message;
                }
                finally
                {
                    con.Close();
                }
            }
        }

        public string HashPassword(string password)
        {
            using (SHA256 sha256 = SHA256.Create())
            {
                byte[] bytes = sha256.ComputeHash(Encoding.UTF8.GetBytes(password));
                StringBuilder builder = new StringBuilder();
                foreach (byte b in bytes)
                {
                    builder.Append(b.ToString("x2"));
                }
                return builder.ToString();
            }
        }
    }
}


