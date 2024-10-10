using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class logout : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            Session["Page_name"] = "";

            if ((Session["admin_email"] != null) || (Session["owner_email"] != null))
            {
                //Remove all session
                Session.RemoveAll();
                //Destroy all session objects
                Session.Abandon();
                //Redirect to homepage or login page
                Response.Redirect("Home.aspx");
            }
        }
    }
}