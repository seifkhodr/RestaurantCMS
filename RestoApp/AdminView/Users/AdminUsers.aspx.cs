using System;
using System.Web.UI.WebControls;
using RestoApp.AdminRepo;
using RestoApp.Helper;

namespace RestoApp.AdminView.Users
{
    public partial class AdminUsers : System.Web.UI.Page
    {
        private UserRepositoryAdmin _repo = new UserRepositoryAdmin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthHelper.RequireAdmin(this))
            {
                return;
            }

            if (!IsPostBack) { BindGrid(); }
        }

        private void BindGrid()
        {
            gvUsers.DataSource = _repo.GetAllUsers();
            gvUsers.DataBind();
        }

        protected void gvUsers_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            int userId = Convert.ToInt32(e.CommandArgument);
            bool success = false;
            if (e.CommandName == "MakeAdmin")
            {
                success = _repo.UpdateUserRole(userId, "Admin");
                ShowMessage(success ? $"User #{userId} promoted to Admin." : "Failed to update role.", success);
            }
            else if (e.CommandName == "MakeClient")
            {
                success = _repo.UpdateUserRole(userId, "Client");
                ShowMessage(success ? $"User #{userId} demoted to Client." : "Failed to update role.", success);
            }
            else if (e.CommandName == "Deactivate")
            {
                success = _repo.ToggleUserActive(userId, false);
                ShowMessage(success ? $"User #{userId} has been deactivated." : "Failed to deactivate user.", success);
            }
            else if (e.CommandName == "Activate")
            {
                success = _repo.ToggleUserActive(userId, true);
                ShowMessage(success ? $"User #{userId} has been activated." : "Failed to activate user.", success);
            }
            BindGrid();
        }

        private void ShowMessage(string msg, bool isSuccess)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Text = msg;
        }
    }
}
