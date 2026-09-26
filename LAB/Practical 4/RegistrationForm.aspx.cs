using System;

namespace Lab_4
{
    public partial class RegistrationForm : System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
        }

        protected void cvTerms_ServerValidate(
            object source,
            System.Web.UI.WebControls.ServerValidateEventArgs args)
        {
            args.IsValid = chkTerms.Checked;
        }

        protected void btnRegister_Click(object sender, EventArgs e)
        {
            if (Page.IsValid)
            {
                lblMessage.Text =
                    "Registration successful! Welcome, "
                    + txtName.Text.Trim()
                    + ". You are registered for "
                    + ddlEvent.SelectedValue
                    + ".";

                lblMessage.Visible = true;
            }
            else
            {
                lblMessage.Visible = false;
            }
        }
    }
}
