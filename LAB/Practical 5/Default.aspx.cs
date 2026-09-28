using System;
using System.Web;

namespace Leave_Management
{
    public partial class Default : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                // Today's date
                calAcademic.SelectedDate = DateTime.Today;
                calAcademic.VisibleDate = DateTime.Today;

                // Check Session
                if (Session["EmployeeName"] != null)
                {
                    lblWelcome.Text =
                        "Welcome, " +
                        Session["EmployeeName"].ToString();
                }
                else
                {
                    // Check Cookie
                    HttpCookie cookie =
                        Request.Cookies["EmployeeName"];

                    if (cookie != null)
                    {
                        lblWelcome.Text =
                            "Welcome, " + cookie.Value;
                    }
                    else
                    {
                        lblWelcome.Text =
                            "Welcome to Academic Calendar";
                    }
                }
            }
        }

        protected void calAcademic_SelectionChanged(
            object sender,
            EventArgs e)
        {
            lblSelectedDate.Text =
                "Selected Date: " +
                calAcademic.SelectedDate
                    .ToString("dd-MM-yyyy");
        }

        protected void btnLeave_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect("Leave.aspx");
        }
    }
}
