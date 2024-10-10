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
    public partial class RegisterOwner : System.Web.UI.Page
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

                cmd1.Parameters.AddWithValue("@Own_nic", txtNIC.Text.Trim());
                cmd2.Parameters.AddWithValue("@Own_mobile", txtMobile.Text.Trim());
                cmd3.Parameters.AddWithValue("@Own_email", txtEmail.Text.Trim());

                cmd4.Parameters.AddWithValue("@Own_fname", txtFirstName.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_lname", txtLastName.Text.Trim());
                cmd4.Parameters.AddWithValue("@Gender", rdlGender.SelectedValue);
                cmd4.Parameters.AddWithValue("@Own_nic", txtNIC.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_mobile", txtMobile.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_office", txtOfficeNo.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_dob", txtDOB.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_address", txtAddress.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_email", txtEmail.Text.Trim());
                cmd4.Parameters.AddWithValue("@Own_password", HashPassword(txtPassword.Text.Trim())); // Hash the password
                cmd4.Parameters.AddWithValue("@Own_status", "1");

                if (rdlGender.SelectedValue == "Male")
                {
                    cmd4.Parameters.AddWithValue("@Own_profpic", "avatar_male.jpg");
                }
                else
                {
                    cmd4.Parameters.AddWithValue("@Own_profpic", "avatar_female.jpg");
                }

                cmd1.CommandText = "SELECT * FROM tblOwner where Own_nic=@Own_nic";
                cmd2.CommandText = "SELECT * FROM tblOwner where Own_mobile=@Own_mobile";
                cmd3.CommandText = "SELECT * FROM tblOwner where Own_email=@Own_email";

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
                    lblResult1.Text = "NIC number " + dt1.Rows[0]["Own_nic"].ToString() + " already exists";
                }
                if (dt2.Rows.Count > 0)
                {
                    lblResult2.Text = "Mobile number " + dt2.Rows[0]["Own_mobile"].ToString() + " already exists";
                }
                if (dt3.Rows.Count > 0)
                {
                    lblResult3.Text = "Email address " + dt3.Rows[0]["Own_email"].ToString() + " already exists";
                }

                if (dt1.Rows.Count == 0 && dt2.Rows.Count == 0 && dt3.Rows.Count == 0)
                {
                    con4.Open();
                    cmd4.CommandText = "INSERT INTO tblOwner ([Own_fname], [Own_lname], [Gender], [Own_nic], [Own_mobile], [Own_office], [Own_dob], [Own_address], [Own_email], [Own_password], [Own_profpic], [Own_status]) VALUES (@Own_fname, @Own_lname, @Gender, @Own_nic, @Own_mobile, @Own_office, @Own_dob, @Own_address, @Own_email, @Own_password, @Own_profpic, @Own_status)";
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
            txtOfficeNo.Text = "";
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
    }
}
