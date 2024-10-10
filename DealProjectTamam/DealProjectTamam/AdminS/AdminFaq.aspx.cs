using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam.AdminS
{
    public partial class AdminFaq : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCategoryData();
                btnUpdate.Visible = false;
                btnDelete.Visible = false;
                btnCancel.Visible = false;

            }
            getCategory();
           

        }
        private void BindCategoryData()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
         
            cmd.CommandText = "SELECT f.faq_id, f.faq_cat, f.faq_question, f.faq_answer, c.Category FROM tblFaq f inner join tblFaq_category c on c.id=f.faq_cat";
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            //Create a DataTable
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
            }
            //Set the DataTable as the DataSource
            gvs_1.DataSource = dt;
            gvs_1.DataBind();
        }

        protected void gvs_PreRender(object sender, EventArgs e)
        {
            if (gvs_1.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs_1.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs_1.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
        }
        protected void gvs_PageIndexChanging(object sender, GridViewPageEventArgs e)
        {
            gvs_1.PageIndex = e.NewPageIndex;
            BindCategoryData();
        }
        protected void gvs_SelectedIndexChanged(object sender, EventArgs e)
        {
            //Read data from GridView and Populate the form
            txtFaq_id.Text = (gvs_1.DataKeys[gvs_1.SelectedIndex].Value.ToString());
            txtques.Text = ((Label)gvs_1.SelectedRow.FindControl("lblfaqtopic")).Text;
            txtanswer.Text = ((Label)gvs_1.SelectedRow.FindControl("lblfaqanswer")).Text;
            ddlcat.SelectedValue = ((Label)gvs_1.SelectedRow.FindControl("lblcatid")).Text;

            //Hide Insert button during update/delete
            btnInsert.Visible = false;
            btnUpdate.Visible = true;
            btnDelete.Visible = true;
            btnCancel.Visible = true;
            lblMsg.Text = "";


        }



        public void getCategory()
        {
           

            SqlConnection dbcon = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = dbcon;
            cmd.CommandType = CommandType.Text;
            cmd.CommandText = "SELECT * FROM tblFaq_category";
            SqlDataReader dr;
            dbcon.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                ListItem newItem = new ListItem();
                newItem.Text = (String)dr["Category"];
                newItem.Value = dr["id"].ToString();
                ddlcat.Items.Add(newItem);
            }

        }

        protected void btnInsert_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";
           

            //Add built-in function to remove spaces from Textbox Category name
            String Faq_topic = txtques.Text;
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;

            //Add DELETE statement to delete the selected category for the above CatID
           
            cmd.CommandText = "select * from tblFaq where faq_cat=@Faq_cat and faq_question=@Faq";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@Faq_cat", ddlcat.SelectedValue.ToString());
            cmd.Parameters.AddWithValue("@Faq", Faq_topic);

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            using (da)
            {
                //Populate the DataTable
                da.Fill(dt);
                if (dt.Rows.Count > 0)
                {
                    lblMsg.Text = "Category " + ddlcat.SelectedItem.Text.ToString() + " and Topic " + Faq_topic + " already exists!";
                    lblMsg.ForeColor = System.Drawing.Color.Red;
                    ResetAll();
                }
                else
                {
                    SqlConnection con2 = new SqlConnection(_conString);
                    SqlCommand cmd2 = new SqlCommand();
                    cmd2.CommandType = CommandType.Text;

                    cmd2.Connection = con2;

                    //Add DELETE statement to delete the selected category for the above CatID
                    cmd2.CommandText = "insert into tblFaq ([faq_question],[faq_answer], [faq_cat]) values (@faq, @fanswer, @fcat)";
                    //Create a parametererized query for CatID
                    cmd2.Parameters.AddWithValue("@faq", Faq_topic);
                    cmd2.Parameters.AddWithValue("@fanswer", txtanswer.Text);
                    cmd2.Parameters.AddWithValue("@fcat", ddlcat.SelectedIndex);
                    con2.Open();
                    Boolean IsAdded = false;
                    IsAdded = cmd2.ExecuteNonQuery() > 0;
                    con2.Close();
                    if (IsAdded)
                    {
                        lblMsg.Text = Faq_topic + " FAQ topic added successfully!";
                        lblMsg.ForeColor = System.Drawing.Color.Green;
                        //Refresh the GridView by calling the BindCategoryData()
                        BindCategoryData();
                    }
                    else
                    {
                        lblMsg.Text = "Error while adding " + Faq_topic + "FAQ topic";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                    }
                    ResetAll();
                }
            }

        }

        protected void btnUpdate_Click(object sender, EventArgs e)
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.Connection = con;
            cmd.CommandText = "select * from tblFaq where faq_id=@faq_id";
            //Create a parametererized query for CatID
            cmd.Parameters.AddWithValue("@faq_id", txtFaq_id.Text.Trim());
            

            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();

            using (da)
            {
                da.Fill(dt);

                if (dt.Rows.Count > 0)
                {
                    string category_old = dt.Rows[0]["faq_cat"].ToString();
                    string topic_old = dt.Rows[0]["faq_question"].ToString();
                    string answer_old = dt.Rows[0]["faq_answer"].ToString();

                    if ((category_old == ddlcat.SelectedValue.ToString()) &&
                        (topic_old == txtques.Text.Trim()) &&
                        (answer_old == txtanswer.Text.Trim()))
                    {
                        lblMsg.Text = "No change detected";
                        lblMsg.ForeColor = System.Drawing.Color.Red;
                        ResetAll();
                    }

                    else
                    {
                        SqlConnection con1 = new SqlConnection(_conString);
                        SqlCommand cmd1 = new SqlCommand();
                        cmd1.CommandType = CommandType.Text;

                        cmd1.Connection = con1;
                        cmd1.CommandText = "select * from tblFaq where faq_cat=@faq_cat and faq_question=@faq_question and faq_id<>@faq_id";
                        //Create a parametererized query for CatID
                   
                        cmd1.Parameters.AddWithValue("@Faq_cat", ddlcat.SelectedValue.ToString());
                        cmd1.Parameters.AddWithValue("@Faq_question", txtques.Text.Trim());
                        cmd1.Parameters.AddWithValue("@Faq_id", txtFaq_id.Text.Trim());

                        SqlDataAdapter da1 = new SqlDataAdapter(cmd1);
                        DataTable dt1 = new DataTable();
                        using (da1)
                        {
                            da1.Fill(dt1);

                            if (dt1.Rows.Count > 0)
                            {

                                lblMsg.Text = "";
                                lblMsg.Text = "Category " + ddlcat.SelectedItem.Text.ToString() + " and Topic " + txtques.Text + " already exists!";
                                lblMsg.ForeColor = System.Drawing.Color.Red;
                                ResetAll();


                            }

                            else
                            {
                                SqlConnection con2 = new SqlConnection(_conString);
                                SqlCommand cmd2 = new SqlCommand();
                                cmd2.CommandType = CommandType.Text;
                                cmd2.Connection = con2;
                                //Add DELETE statement to delete the selected category for the above CatID
                                cmd2.CommandText = "update tblFaq set faq_cat=@faq_cat, faq_question=@faq_question, faq_answer=@faq_answer where faq_id=@faq_id";

                                cmd2.Parameters.AddWithValue("@faq_id", txtFaq_id.Text.Trim());
                                cmd2.Parameters.AddWithValue("@faq_cat", ddlcat.SelectedValue.ToString());
                                cmd2.Parameters.AddWithValue("@faq_question", txtques.Text.Trim());
                                cmd2.Parameters.AddWithValue("@faq_answer", txtanswer.Text.Trim());

                                con2.Open();
                                Boolean IsUpdated = false;
                                IsUpdated = cmd2.ExecuteNonQuery() > 0;
                                con2.Close();
                                if (IsUpdated)
                                {
                                    lblMsg.Text = "FAQ updated";
                                    lblMsg.ForeColor = System.Drawing.Color.Green;
                                    BindCategoryData();
                                    ResetAll();

                                }
                            }
                            con1.Close();
                        }

                    }

                }

            }
            con.Close();
        }

        protected void btnDelete_Click(object sender, EventArgs e)
        {
            lblMsg.Text = "";


            SqlConnection con2 = new SqlConnection(_conString);
            SqlCommand cmd2 = new SqlCommand();
            cmd2.CommandType = CommandType.Text;

            cmd2.CommandText = "delete from tblFaq where Faq_id=@Faq_id";
            cmd2.Parameters.AddWithValue("@Faq_id", txtFaq_id.Text.Trim());
            cmd2.Connection = con2;
            con2.Open();

            Boolean IsDeleted = false;
            IsDeleted = cmd2.ExecuteNonQuery() > 0;
            con2.Close();

            if (IsDeleted)
            {
                lblMsg.Text = "Category " + ddlcat.SelectedItem.Text.ToString() + " and Topic " + txtques.Text + " deleted successfully!";
                lblMsg.ForeColor = System.Drawing.Color.Green;
                //Refresh the GridView by calling the BindCategoryData()

            }
            else
            {
                lblMsg.Text = "Error while deleting " + "Category " + ddlcat.SelectedItem.Text.ToString() + " and Topic " + txtques.Text;
                lblMsg.ForeColor = System.Drawing.Color.Red;
            }

            BindCategoryData();
            ResetAll();
        }

        protected void btnCancel_Click(object sender, EventArgs e)
        {
            ResetAll();
        }

        private void ResetAll()
        {
            btnInsert.Visible = true;
            btnUpdate.Visible = false;
            btnDelete.Visible = false;
            btnCancel.Visible = false;
            txtFaq_id.Text = "";
            txtques.Text = "";
            txtanswer.Text = "";
            ddlcat.SelectedIndex = -1;
        }


    }
}
