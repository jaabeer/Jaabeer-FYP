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
    public partial class New_hotel : System.Web.UI.Page
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
           
            cmd.CommandText = "SELECT * from tblHotel th, tblNotification tn, tblDistrict td where td.Dist_id= th.Dist_id and  tn.Hotel_id = th.Hotel_id and tn.Notif_type IN ('notif', 'notif_up') and tn.State=0";
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();
            con.Close();

        }

        protected void lnkblock_Click(object sender, EventArgs e)
        {

        }
    }
}