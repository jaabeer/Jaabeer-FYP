using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net.Mail;
using System.Security.Cryptography;
using System.Text;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class RegisterClient : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "";
            rvdob.MinimumValue = DateTime.Now.AddYears(-45).ToShortDateString();
            rvdob.MaximumValue = DateTime.Now.AddYears(-18).ToShortDateString();
            rvdob.Type = ValidationDataType.Date;

        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            using (SqlConnection con1 = new SqlConnection(_conString))
            using (SqlConnection con2 = new SqlConnection(_conString))
            using (SqlConnection con3 = new SqlConnection(_conString))
            using (SqlConnection con4 = new SqlConnection(_conString))
            {
                SqlCommand cmd1 = new SqlCommand();
                SqlCommand cmd2 = new SqlCommand();
                SqlCommand cmd3 = new SqlCommand();
                SqlCommand cmd4 = new SqlCommand();

                cmd1.Connection = con1;
                cmd2.Connection = con2;
                cmd3.Connection = con3;
                cmd4.Connection = con4;

                cmd1.CommandType = CommandType.Text;
                cmd2.CommandType = CommandType.Text;
                cmd3.CommandType = CommandType.Text;
                cmd4.CommandType = CommandType.Text;

                cmd1.Parameters.AddWithValue("@Client_nic", txtNIC.Text.Trim());
                cmd2.Parameters.AddWithValue("@Client_contact", txtMobile.Text.Trim());
                cmd3.Parameters.AddWithValue("@Client_email", txtEmail.Text.Trim());

                cmd4.Parameters.AddWithValue("@Client_fname", txtFirstName.Text.Trim());
                cmd4.Parameters.AddWithValue("@Client_lname", txtLastName.Text.Trim());
                cmd4.Parameters.AddWithValue("@Gender", rdlGender.SelectedValue);
                cmd4.Parameters.AddWithValue("@Client_nic", txtNIC.Text.Trim());
                cmd4.Parameters.AddWithValue("@Client_contact", txtMobile.Text.Trim());
                cmd4.Parameters.AddWithValue("@Client_dob", txtDOB.Text.Trim());
                cmd4.Parameters.AddWithValue("@Client_address", txtAddress.Text.Trim());
                cmd4.Parameters.AddWithValue("@Client_email", txtEmail.Text.Trim());
                cmd4.Parameters.AddWithValue("@Client_password", HashPassword(txtPassword.Text.Trim())); // Hash the password
                cmd4.Parameters.AddWithValue("@Client_status", "1");

                if (rdlGender.SelectedValue == "Male")
                {
                    cmd4.Parameters.AddWithValue("@Client_profilepicture", "avatar_male.jpg");
                }
                else
                {
                    cmd4.Parameters.AddWithValue("@Client_profilepicture", "avatar_female.jpg");
                }

                cmd1.CommandText = "SELECT * FROM tblClient where Client_nic=@Client_nic";
                cmd2.CommandText = "SELECT * FROM tblClient where Client_contact=@Client_contact";
                cmd3.CommandText = "SELECT * FROM tblClient where Client_email=@Client_email";

                SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                SqlDataAdapter da2 = new SqlDataAdapter(cmd2);
                SqlDataAdapter da3 = new SqlDataAdapter(cmd3);

                DataTable dt1 = new DataTable();
                DataTable dt2 = new DataTable();
                DataTable dt3 = new DataTable();

                da1.Fill(dt1);
                da2.Fill(dt2);
                da3.Fill(dt3);

                lblResult1.Text = "";
                lblResult2.Text = "";
                lblResult3.Text = "";
                lblResult4.Text = "";
                lblResult5.Text = "";

                if (dt1.Rows.Count > 0)
                {
                    lblResult1.Text = "NIC number " + dt1.Rows[0]["Client_nic"].ToString() + " already exists";
                }
                if (dt2.Rows.Count > 0)
                {
                    lblResult2.Text = "Mobile number " + dt2.Rows[0]["Client_contact"].ToString() + " already exists";
                }
                if (dt3.Rows.Count > 0)
                {
                    lblResult3.Text = "Email address " + dt3.Rows[0]["Client_email"].ToString() + " already exists";
                }

                int check_nic_number = validate_NIC() ? 0 : 1;

                if (check_nic_number == 1)
                {
                    lblResult5.Text = "NIC should always start with the first letter of the first name or surname followed by the date of birth";
                }

                if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0 && dt3.Rows.Count == 0 && check_nic_number == 0)
                {
                    con4.Open();
                    cmd4.CommandText = "INSERT INTO tblClient ([Client_fname], [Client_lname], [Gender], [Client_nic], [Client_contact], [Client_dob], [Client_address], [Client_email], [Client_password], [Client_profilepicture], [Client_status]) VALUES (@Client_fname, @Client_lname, @Gender, @Client_nic, @Client_contact, @Client_dob, @Client_address, @Client_email, @Client_password, @Client_profilepicture, @Client_status)";
                    Boolean IsAdded = cmd4.ExecuteNonQuery() > 0;
                    if (IsAdded)
                    {
                        btnsuccess.Visible = true;
                        btnRegister.Visible = false;
                        lblResult1.Text = "Registration successful";
                        Response.Redirect("Home.aspx");
                    }
                    else
                    {
                        lblResult1.Text = "An unexpected error occurred, please try again";
                    }
                    con4.Close();
                }
            }
        }

        public void clear_fields()
        {
            txtFirstName.Text = "";
            txtLastName.Text = "";
            rdlGender.SelectedValue = "";
            txtNIC.Text = "";
            txtMobile.Text = "";
            txtDOB.Text = "";
            txtAddress.Text = "";
            txtEmail.Text = "";
            txtConfirmPassword.Text = "";
            txtPassword.Text = "";
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

        private Boolean validate_NIC()
        {
            string firstLetter = txtFirstName.Text.Trim().Substring(0, 1).ToUpper();
            string lastLetter = txtLastName.Text.Trim().Substring(0, 1).ToUpper();
            string dob = DateTime.Parse(txtDOB.Text.Trim()).ToString("ddMMyyyy");

            string expectedNICStart = firstLetter + dob;
            string expectedNICStartAlt = lastLetter + dob;

            string actualNICStart = txtNIC.Text.Trim().Substring(0, 9); // Considering DDMMYYYY is 8 characters

            return actualNICStart.Equals(expectedNICStart, StringComparison.OrdinalIgnoreCase) || actualNICStart.Equals(expectedNICStartAlt, StringComparison.OrdinalIgnoreCase);
        }
    }
}
   