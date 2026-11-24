using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.UI;
using System.Web.UI.WebControls;

namespace P9_PlacementDashboard
{
    public partial class Default : System.Web.UI.Page
    {
        string cs = ConfigurationManager
            .ConnectionStrings["PlacementDB"]
            .ConnectionString;
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                LoadSummary();
                LoadPlacements();
            }
        }
        private void LoadSummary()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                con.Open();

                SqlCommand cmd = new SqlCommand(
                    "SELECT COUNT(*) FROM Students", con);
                lblTotal.Text = cmd.ExecuteScalar().ToString();

                cmd.CommandText =
                    "SELECT COUNT(*) FROM Students WHERE IsPlaced = 1";
                lblPlaced.Text = cmd.ExecuteScalar().ToString();

                cmd.CommandText =
                    "SELECT COUNT(*) FROM Students WHERE IsPlaced = 0";
                lblUnplaced.Text = cmd.ExecuteScalar().ToString();

                cmd.CommandText =
                    "SELECT MAX(PackageLPA) FROM Placements";

                object result = cmd.ExecuteScalar();

                decimal highest = result == DBNull.Value
                    ? 0
                    : Convert.ToDecimal(result);

                lblHighestPackage.Text = highest.ToString("N2");

                int total = Convert.ToInt32(lblTotal.Text);
                int placed = Convert.ToInt32(lblPlaced.Text);

                decimal percentage = total == 0
                    ? 0
                    : (decimal)placed * 100 / total;

                lblPercentage.Text =
                    percentage.ToString("N2") + "%";
            }
        }

        private void LoadPlacements()
        {
            using (SqlConnection con = new SqlConnection(cs))
            {
                string query = "SELECT * FROM Students";

                SqlDataAdapter da = new SqlDataAdapter(query, con);

                DataTable dt = new DataTable();
                da.Fill(dt);

                gvPlacements.DataSource = dt;
                gvPlacements.DataBind();
            }
        }

        protected void btnReport_Click(object sender, EventArgs e)
        {
            Response.Redirect("~/Companies.aspx");
        }
    }
}