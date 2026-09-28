using System;
using System.Data;
using System.Drawing;
using System.Web;
using System.Web.UI;
using System.Xml.Linq;

namespace Leave_Management
{
    public partial class Leave : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                calLeaveDate.SelectedDate = DateTime.Today;
                calLeaveDate.VisibleDate = DateTime.Today;

                lblSelectedLeaveDate.Text =
                    "Selected Date: " +
                    DateTime.Today.ToString("dd-MM-yyyy");

                // Create leave table
                if (Session["LeaveApplications"] == null)
                {
                    CreateLeaveTable();
                }

                // Get employee name from Session
                if (Session["EmployeeName"] != null)
                {
                    txtName.Text =
                        Session["EmployeeName"].ToString();
                }

                // Get remembered name from Cookie
                HttpCookie cookie =
                    Request.Cookies["EmployeeName"];

                if (cookie != null)
                {
                    txtName.Text = cookie.Value;
                    chkRemember.Checked = true;
                }

                LoadLeaveGrid();
            }
        }

        private void CreateLeaveTable()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("EmployeeName");
            dt.Columns.Add("LeaveDate");
            dt.Columns.Add("LeaveType");
            dt.Columns.Add("Reason");
            dt.Columns.Add("Status");

            Session["LeaveApplications"] = dt;
        }

        private void LoadLeaveGrid()
        {
            DataTable dt =
                Session["LeaveApplications"] as DataTable;

            if (dt != null)
            {
                gvLeaves.DataSource = dt;
                gvLeaves.DataBind();
            }
        }

        protected void calLeaveDate_SelectionChanged(
            object sender,
            EventArgs e)
        {
            lblSelectedLeaveDate.Text =
                "Selected Date: " +
                calLeaveDate.SelectedDate
                    .ToString("dd-MM-yyyy");
        }

        protected void btnApply_Click(
            object sender,
            EventArgs e)
        {
            if (!Page.IsValid)
            {
                return;
            }

            string employeeName =
                txtName.Text.Trim();

            DateTime leaveDate =
                calLeaveDate.SelectedDate;

            string leaveType =
                ddlLeaveType.SelectedValue;

            string reason =
                txtReason.Text.Trim();

            // Validate date
            if (leaveDate == DateTime.MinValue)
            {
                ShowError("Please select leave date.");
                return;
            }

            // Do not allow past date
            if (leaveDate.Date < DateTime.Today)
            {
                ShowError(
                    "Leave date cannot be in the past."
                );

                return;
            }

            DataTable dt =
                Session["LeaveApplications"]
                as DataTable;

            if (dt == null)
            {
                CreateLeaveTable();

                dt =
                    Session["LeaveApplications"]
                    as DataTable;
            }

            DataRow row = dt.NewRow();

            row["EmployeeName"] = employeeName;

            row["LeaveDate"] =
                leaveDate.ToString("dd-MM-yyyy");

            row["LeaveType"] = leaveType;

            row["Reason"] = reason;

            row["Status"] = "Pending";

            dt.Rows.Add(row);

            // Store table in Session
            Session["LeaveApplications"] = dt;

            // Store employee name in Session
            Session["EmployeeName"] = employeeName;

            // Remember name using Cookie
            if (chkRemember.Checked)
            {
                HttpCookie nameCookie =
                    new HttpCookie(
                        "EmployeeName",
                        employeeName
                    );

                nameCookie.Expires =
                    DateTime.Now.AddDays(30);

                nameCookie.HttpOnly = true;

                Response.Cookies.Add(nameCookie);
            }
            else
            {
                HttpCookie deleteCookie =
                    new HttpCookie("EmployeeName");

                deleteCookie.Expires =
                    DateTime.Now.AddDays(-1);

                Response.Cookies.Add(deleteCookie);
            }

            lblMessage.Text =
                "Leave application submitted successfully.";

            lblMessage.ForeColor =
                Color.Green;

            lblMessage.CssClass = "success";

            LoadLeaveGrid();

            // Clear only leave fields
            txtReason.Text = "";

            ddlLeaveType.SelectedIndex = 0;
        }

        private void ShowError(string message)
        {
            lblMessage.Text = message;

            lblMessage.ForeColor =
                Color.Red;

            lblMessage.CssClass = "error";
        }

        protected void btnClear_Click(
            object sender,
            EventArgs e)
        {
            txtReason.Text = "";

            ddlLeaveType.SelectedIndex = 0;

            calLeaveDate.SelectedDate =
                DateTime.Today;

            calLeaveDate.VisibleDate =
                DateTime.Today;

            lblSelectedLeaveDate.Text =
                "Selected Date: " +
                DateTime.Today.ToString("dd-MM-yyyy");

            lblMessage.Text = "";
        }

        protected void btnHome_Click(
            object sender,
            EventArgs e)
        {
            Response.Redirect("Default.aspx");
        }
    }
}
