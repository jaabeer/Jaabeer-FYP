using System;
using System.Collections;
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
    public partial class MyHotel : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["DealTamamDB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!Page.IsPostBack)
            {
                // Since you've already checked for null, this should now be safe.

                PropertyData();
            }


        }

        protected void PropertyData()
        {

            SqlConnection con = new SqlConnection(_conString);
            // Create Command
            SqlCommand cmd = new SqlCommand();
            cmd.Connection = con;
            cmd.CommandType = CommandType.Text;
            cmd.Parameters.AddWithValue("@Own_id", Convert.ToInt32(Session["owner_id"]));
            //cmd.CommandText = "SELECT  * FROM tblProperty Where Prop_status=1 and Own_id=@Own_id Order by Prop_regis_date DESC";
            cmd.CommandText = "SELECT  * FROM tblHotel tH left join tblBooking tb on tb.Hotel_id=tH.Hotel_id Where  tH.Own_id=@Own_id Order by tH.Hotel_regis_date DESC";
            SqlDataAdapter da = new SqlDataAdapter(cmd);

            //Create DataSet/DataTable named dtMovies
            DataTable dt = new DataTable();

            //Populate the datatable using the Fill()
            using (da)
            {
                da.Fill(dt);
            }


            Hashtable hTable = new Hashtable();
            ArrayList duplicateList = new ArrayList();
            foreach (DataRow dtRow in dt.Rows)
            {
                if (hTable.Contains(dtRow["Hotel_name"]))
                    duplicateList.Add(dtRow);
                else
                    hTable.Add(dtRow["Hotel_name"], string.Empty);
            }
            foreach (DataRow dtRow in duplicateList)
            {
                dt.Rows.Remove(dtRow);
            }



            Repeater1.DataSource = dt;
            Repeater1.DataBind();

            foreach (RepeaterItem item in Repeater1.Items)
            {
                TextBox txtbook_id = item.FindControl("txt_booking") as TextBox;

                if (txtbook_id.Text == "")
                {
                    item.FindControl("lbtnview_booking").Visible = false;

                }
            }
            con.Close();
        }
    }
}
