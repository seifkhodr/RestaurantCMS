using System;
using RestoApp.Models;

using System.Web.UI.WebControls;
using RestoApp.AdminRepo;
using RestoApp.Helper;

namespace RestoApp.AdminView.Menus
{
    public partial class AdminMenus : System.Web.UI.Page
    {
        private MenuRepositoryAdmin _repo = new MenuRepositoryAdmin();

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
            gvMenus.DataSource = _repo.GetAllMenus();
            gvMenus.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtMenuName.Text)) { ShowMessage("Menu name is required.", false); return; }
            int menuId = Convert.ToInt32(hfMenuId.Value);
            RestoApp.Models.Menu model = new RestoApp.Models.Menu
            {
                MenuId = menuId,
                MenuName = txtMenuName.Text.Trim(),
                Description = txtDescription.Text.Trim(),
                DisplayOrder = int.TryParse(txtDisplayOrder.Text, out int order) ? order : 0,
                IsActive = ddlIsActive.SelectedValue == "1"
            };
            if (menuId > 0)
            {
                bool success = _repo.UpdateMenu(model);
                ShowMessage(success ? "Menu updated successfully." : "Failed to update menu.", success);
            }
            else
            {
                int newId = _repo.InsertMenu(model);
                ShowMessage(newId > 0 ? $"Menu created successfully (ID: {newId})." : "Failed to create menu.", newId > 0);
            }
            ClearForm(); BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e) { ClearForm(); }

        protected void gvMenus_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditMenu")
            {
                string[] parts = e.CommandArgument.ToString().Split('|');
                hfMenuId.Value = parts[0]; txtMenuName.Text = parts[1]; txtDescription.Text = parts[2];
                txtDisplayOrder.Text = parts[3]; ddlIsActive.SelectedValue = Convert.ToBoolean(parts[4]) ? "1" : "0";
                litFormTitle.Text = "Edit Menu #" + parts[0]; btnSave.Text = "Update Menu"; btnCancel.Visible = true;
            }
            else if (e.CommandName == "DeleteMenu")
            {
                int menuId = Convert.ToInt32(e.CommandArgument);
                bool success = _repo.DeleteMenu(menuId);
                ShowMessage(success ? "Menu deleted successfully." : "Failed to delete menu.", success);
                BindGrid();
            }
        }

        private void ClearForm()
        {
            hfMenuId.Value = "0"; txtMenuName.Text = ""; txtDescription.Text = ""; txtDisplayOrder.Text = "0";
            ddlIsActive.SelectedValue = "1"; litFormTitle.Text = "Add New Menu"; btnSave.Text = "Save Menu"; btnCancel.Visible = false;
        }

        private void ShowMessage(string msg, bool isSuccess)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Text = msg;
        }
    }
}
