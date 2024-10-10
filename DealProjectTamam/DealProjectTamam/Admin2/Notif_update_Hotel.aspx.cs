using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam.Admin2
{
    public partial class Notif_update_Hotel : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;

        protected void Page_Load(object sender, EventArgs e)
        {
            getVillas();

        }

        protected void gvs_PreRender(object sender, EventArgs e)
        {
            if (gvs.Rows.Count > 0)
            {
                //This replaces <td> with <th> and adds the scope attribute
                gvs.UseAccessibleHeader = true;
                //This will add the <thead> and <tbody> elements
                gvs.HeaderRow.TableSection =
                TableRowSection.TableHeader;
            }
        }

        void getVillas()
        {
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;

            cmd.CommandText = "SELECT * from tblHotel th, tblNotification tn, tblDistrict td where td.Dist_id= th.Dist_id and  tn.Hotel_id = th.Hotel_id and tn.State=0 and tn.Notif_type='update'";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();
            con.Close();

        }


        protected void ReadAll()
        {

            SqlConnection con5 = new SqlConnection(_conString);
            SqlCommand cmd5 = new SqlCommand();
            cmd5.CommandType = CommandType.Text;
            cmd5.Connection = con5;

            cmd5.CommandText = "update tblNotification set State=@status, Comment=@comment where Notif_type='update' and state=0";


            cmd5.Parameters.AddWithValue("@status", 1);
            cmd5.Parameters.AddWithValue("@comment", "Admin Verified");
            con5.Open();
            Boolean IsUpdated1 = false;
            IsUpdated1 = cmd5.ExecuteNonQuery() > 0;
            con5.Close();
            if (IsUpdated1)
            {
                getVillas();


            }
        }

        protected void btnReadAll_Click(object sender, EventArgs e)
        {
            ReadAll();
        }
    }
}