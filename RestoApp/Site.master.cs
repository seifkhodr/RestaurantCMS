using System;
using RestoApp.Helper;

namespace RestoApp
{
    public partial class Site : System.Web.UI.MasterPage
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                string fullName;
                string role;

                if (AuthHelper.TryGetCurrentUser(Page, out fullName, out role))
                {
                    phGuest.Visible = false;
                    phUser.Visible = true;
                    litUserName.Text = fullName;

                    if (role.Equals("admin", StringComparison.OrdinalIgnoreCase))
                    {
                        phClientLinks.Visible = false;
                        phAdminLinks.Visible = true;
                    }
                    else
                    {
                        phClientLinks.Visible = true;
                        phAdminLinks.Visible = false;
                    }
                }
                else
                {
                    phGuest.Visible = true;
                    phUser.Visible = false;
                    phClientLinks.Visible = true;
                    phAdminLinks.Visible = false;
                }
            }
        }

        protected void btnLogout_Click(object sender, EventArgs e)
        {
            AuthHelper.SignOut(Page);

            Response.Redirect("~/Default.aspx");
        }
    }
}
