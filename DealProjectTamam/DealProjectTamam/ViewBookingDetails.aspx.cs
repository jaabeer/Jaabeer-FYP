using Aspose.Pdf;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Configuration;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace DealProjectTamam
{
    public partial class ViewBookingDetails : System.Web.UI.Page
    {
        private string _conString = WebConfigurationManager.ConnectionStrings["BestDeal_DB"].ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
           
            fill_booking_details();
        }

        void fill_booking_details()
        {
            int qs = Convert.ToInt32(Request.QueryString["ID"]);
            SqlConnection con = new SqlConnection(_conString);
            SqlCommand cmd = new SqlCommand();
            cmd.CommandType = CommandType.Text;
            
            cmd.CommandText = "SELECT * from tblClient tc inner join tblBooking tb on tb.Client_id=tc.Client_id inner join tblHotel th on th.Hotel_id=tb.Hotel_id inner join tblDistrict td on td.Dist_id=th.Dist_id where tb.Bk_id = " + qs;
            cmd.Connection = con;
            SqlDataReader dr;
            con.Open();
            dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                txt_prop_id.Text = dr["Hotel_id"].ToString();
                txt_bk_ref.Text = dr["Bk_ref"].ToString();
                txt_lname.Text = dr["Client_lname"].ToString();
                txt_fname.Text = dr["Client_fname"].ToString();
                txt_NIC.Text = dr["Client_nic"].ToString();
                txt_email.Text = dr["Client_email"].ToString();
                txt_address.Text = dr["Client_address"].ToString();
                txt_email.Text = dr["Client_email"].ToString();
                DateTime bk_date = Convert.ToDateTime(dr["Bk_date"]);
                txt_bk_date.Text = bk_date.ToString("dd-MMMM-yyyy");

                DateTime bk_checkin = Convert.ToDateTime(dr["Bk_checkin"]);
                txt_check_in.Text = bk_checkin.ToString("dd-MMMM-yyyy");

                DateTime bk_checkout = Convert.ToDateTime(dr["Bk_checkout"]);
                txt_check_out.Text = bk_checkout.ToString("dd-MMMM-yyyy");
                txt_price.Text = "Rs " + dr["BK_amnt"].ToString();
                txt_villa_name.Text = dr["Hotel_name"].ToString();
                txt_villa_address.Text = dr["Hotel_street"].ToString() + ", " + dr["Hotel_town"].ToString() + ", " + dr["Dist_name"].ToString();

                if (dr["Bk_state"].ToString() == "C")
                {
                    txt_bk_status.Text = "Confirmed";
                }
                else
                {
                    txt_bk_status.Text = "Pending";
                }

                lbl_client.Text = "Booking details for " + dr["Client_fname"].ToString() + " " + dr["Client_lname"].ToString();

                btn_back.PostBackUrl = "View_booking?ID=" + Convert.ToInt32(txt_prop_id.Text);
            }
        }

        void generate_pdf()
        {
            Document document = new Document();

            
            Aspose.Pdf.Page page = document.Pages.Add();

         
            var title = new Aspose.Pdf.Text.TextFragment("Deal Tamam Hotel BOOKING");
            title.TextState.FontSize = 20;
            title.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            title.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            title.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Lavender);
            title.TextState.Underline = true;
            title.Margin.Bottom = 10;
            page.Paragraphs.Add(title);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var venue = new Aspose.Pdf.Text.TextFragment("Venue: " + txt_villa_name.Text.ToString());
            venue.TextState.FontSize = 14;
            venue.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Italic;
            venue.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Teal);
            venue.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Honeydew);
            venue.Margin.Bottom = 8;
            page.Paragraphs.Add(venue);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var location = new Aspose.Pdf.Text.TextFragment("Location: " + txt_villa_address.Text.ToString());
            location.TextState.FontSize = 14;
            location.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.DarkOliveGreen);
            location.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Honeydew);
            location.Margin.Bottom = 8;
            page.Paragraphs.Add(location);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var bookingReference = new Aspose.Pdf.Text.TextFragment("Booking Reference: " + txt_bk_ref.Text.ToString());
            bookingReference.TextState.FontSize = 14;
            bookingReference.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Crimson);
            bookingReference.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.MistyRose);
            bookingReference.Margin.Bottom = 8;
            page.Paragraphs.Add(bookingReference);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var firstName = new Aspose.Pdf.Text.TextFragment("First Name: " + txt_fname.Text.ToString());
            firstName.TextState.FontSize = 12;
            firstName.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            firstName.Margin.Bottom = 5;
            page.Paragraphs.Add(firstName);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var lastName = new Aspose.Pdf.Text.TextFragment("Last Name: " + txt_lname.Text.ToString());
            lastName.TextState.FontSize = 12;
            lastName.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            lastName.Margin.Bottom = 5;
            page.Paragraphs.Add(lastName);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var nic = new Aspose.Pdf.Text.TextFragment("NIC: " + txt_NIC.Text.ToString());
            nic.TextState.FontSize = 12;
            nic.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            nic.Margin.Bottom = 5;
            page.Paragraphs.Add(nic);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var clientAddress = new Aspose.Pdf.Text.TextFragment("Client Address: " + txt_address.Text.ToString());
            clientAddress.TextState.FontSize = 12;
            clientAddress.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            clientAddress.Margin.Bottom = 5;
            page.Paragraphs.Add(clientAddress);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var clientEmail = new Aspose.Pdf.Text.TextFragment("Client Email: " + txt_email.Text.ToString());
            clientEmail.TextState.FontSize = 12;
            clientEmail.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            clientEmail.Margin.Bottom = 5;
            page.Paragraphs.Add(clientEmail);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var bookingDate = new Aspose.Pdf.Text.TextFragment("Booking Date: " + txt_bk_date.Text.ToString());
            bookingDate.TextState.FontSize = 12;
            bookingDate.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            bookingDate.Margin.Bottom = 5;
            page.Paragraphs.Add(bookingDate);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var checkInDate = new Aspose.Pdf.Text.TextFragment("Check-in Date: " + txt_check_in.Text.ToString());
            checkInDate.TextState.FontSize = 12;
            checkInDate.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            checkInDate.Margin.Bottom = 5;
            page.Paragraphs.Add(checkInDate);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var checkOutDate = new Aspose.Pdf.Text.TextFragment("Check-out Date: " + txt_check_out.Text.ToString());
            checkOutDate.TextState.FontSize = 12;
            checkOutDate.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            checkOutDate.Margin.Bottom = 5;
            page.Paragraphs.Add(checkOutDate);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var bookingStatus = new Aspose.Pdf.Text.TextFragment("Booking status: " + txt_bk_status.Text.ToString());
            bookingStatus.TextState.FontSize = 12;
            bookingStatus.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Blue);
            bookingStatus.Margin.Bottom = 5;
            page.Paragraphs.Add(bookingStatus);

            page.Paragraphs.Add(new Aspose.Pdf.Text.TextFragment(" "));

            var separator = new Aspose.Pdf.Text.TextFragment("");
            separator.TextState.FontSize = 14;
            separator.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            separator.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray);
            separator.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            separator.Margin.Bottom = 10;
            page.Paragraphs.Add(separator);

            var total = new Aspose.Pdf.Text.TextFragment("Total: " + txt_price.Text.ToString());
            total.TextState.FontSize = 16;
            total.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            total.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.DarkGreen);
            total.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightYellow);
            total.Margin.Bottom = 10;
            page.Paragraphs.Add(total);

            var separatorEnd = new Aspose.Pdf.Text.TextFragment("");
            separatorEnd.TextState.FontSize = 14;
            separatorEnd.TextState.FontStyle = Aspose.Pdf.Text.FontStyles.Bold;
            separatorEnd.TextState.ForegroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.Gray);
            separatorEnd.TextState.BackgroundColor = Aspose.Pdf.Color.FromRgb(System.Drawing.Color.LightGray);
            page.Paragraphs.Add(separatorEnd);

            string folderPath = Server.MapPath("~/Property/" + txt_prop_id.Text.ToString() + "/Booking");
            //Check whether Directory (Folder) exists.
            if (!Directory.Exists(folderPath))
            {
                //If Directory (Folder) does not exists. Create it.
                Directory.CreateDirectory(folderPath);
            }

            String filename = "~/Property/" + txt_prop_id.Text.ToString() + "/Booking/" + txt_bk_ref.Text.ToString() + ".pdf";

            lbl_message.Text = "";

            if (!IsFileLocked(MapPath(filename)))
            {
                // Save PDF 
                document.Save(MapPath(filename));
                //Open PDF
                System.Diagnostics.Process.Start(MapPath(filename));
            }
            else
            {
                lbl_message.Text = "Document is already opened, please close it first";
            }
        }

        protected void btn_gen_pdf_Click(object sender, EventArgs e)
        {
            generate_pdf();
        }

        public bool IsFileLocked(string filename)
        {
            bool Locked = false;
            try
            {
                FileStream fs =
                    File.Open(filename, FileMode.OpenOrCreate,
                    FileAccess.ReadWrite, FileShare.None);
                fs.Close();
            }
            catch (IOException ex)
            {
                Locked = true;
            }
            return Locked;
        }
    }
}
