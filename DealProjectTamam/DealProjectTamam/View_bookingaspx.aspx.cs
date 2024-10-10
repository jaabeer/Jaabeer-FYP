using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class View_bookingaspx : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if ((Session["owner_email"] == null))
            {
                Response.Redirect("~/Owner_login.aspx");
            }
            Session["Page_name"] = "View_booking";

            if (!Page.IsPostBack)
            {
                SqlConnection con = new SqlConnection(_conString);
                getBooking();
            }

        }

        void getBooking()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * from tblClient";
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblVilla tv on tv.Villa_id=tb.Villa_id where tv.Villa_id = " + qs;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1);
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();

            h3_booking.InnerText = "View Booking for " + dt.Rows[0]["Villa_name"].ToString() + "Villa";

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
    }
}