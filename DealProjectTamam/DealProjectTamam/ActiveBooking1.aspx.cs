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
    public partial class ActiveBooking1 : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
        }
        void get_All_Bookings()
        {
            int qs = Convert.ToInt32(Session["owner_id"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * from tblClient";
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblVilla tv on tv.Villa_id=tb.Villa_id where tv.Own_id = " + qs;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1);
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();

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

        protected void btn_confirmed_bookings_Click(object sender, EventArgs e)
        {
            get_confirm();
            lblheader.Text = "View Confirmed Bookings";
        }

        protected void btn_pending_bookings_Click(object sender, EventArgs e)
        {
            get_pending();
            lblheader.Text = "View Payment Pending Bookings";
        }

        protected void btn_all_bookings_Click(object sender, EventArgs e)
        {
            get_All_Bookings();
            lblheader.Text = "View  Bookings";
        }

        protected void get_pending()
        {
            int qs = Convert.ToInt32(Session["owner_id"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * from tblClient";
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblVilla tv on tv.Villa_id=tb.Villa_id where tb.Bk_state ='P' and tv.Own_id = " + qs;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1);
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();

        }

        protected void get_payment()
        {
            int qs = Convert.ToInt32(Session["owner_id"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * from tblClient";
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblVilla tv on tv.Villa_id=tb.Villa_id where tb.Bk_state ='B' and tv.Own_id = " + qs;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1);
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();

        }

        protected void get_confirm()
        {
            int qs = Convert.ToInt32(Session["owner_id"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            //cmd.CommandText = "SELECT * from tblClient";
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblVilla tv on tv.Villa_id=tb.Villa_id where tb.Bk_state ='C' and tv.Own_id = " + qs;
            cmd.Connection = con;
            SqlDataAdapter da = new SqlDataAdapter(cmd);
            DataTable dt = new DataTable();
            //DataSet ds1 = new DataSet();
            //da.Fill(ds1);
            da.Fill(dt);
            gvs.DataSource = dt;
            gvs.DataBind();
        }

        protected void btn_oayment_done_Click(object sender, EventArgs e)
        {
            get_payment();
            lblheader.Text = "View Confirmation Awaiting Bookings";
        }
    }
}
