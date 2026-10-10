using System;
using System.Web.UI.WebControls;
using RestoApp.Models;
using RestoApp.AdminRepo;
using RestoApp.Helper;

namespace RestoApp.AdminView.Tables
{
    public partial class AdminTables : System.Web.UI.Page
    {
        private TableRepositoryAdmin _repo = new TableRepositoryAdmin();

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
            gvTables.DataSource = _repo.GetAllTables();
            gvTables.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtTableNumber.Text)) { ShowMessage("Table number is required.", false); return; }
            if (!int.TryParse(txtSeatingCapacity.Text, out int capacity) || capacity <= 0) { ShowMessage("Please enter a valid seating capacity.", false); return; }
            int tableId = Convert.ToInt32(hfTableId.Value);
            RestaurantTable model = new RestaurantTable
            {
                TableId = tableId, TableNumber = txtTableNumber.Text.Trim(), SeatingCapacity = capacity,
                Location = txtLocation.Text.Trim(), PhotoPath = txtPhotoPath.Text.Trim(), IsActive = ddlIsActive.SelectedValue == "1"
            };
            if (tableId > 0)
            {
                bool success = _repo.UpdateTable(model);
                ShowMessage(success ? "Table updated successfully." : "Failed to update table.", success);
            }
            else
            {
                int newId = _repo.InsertTable(model);
                ShowMessage(newId > 0 ? $"Table created successfully (ID: {newId})." : "Failed to create table.", newId > 0);
            }
            ClearForm(); BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e) { ClearForm(); }

        protected void gvTables_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditTable")
            {
                string[] parts = e.CommandArgument.ToString().Split('|');
                hfTableId.Value = parts[0]; txtTableNumber.Text = parts[1]; txtSeatingCapacity.Text = parts[2];
                txtLocation.Text = parts[3]; txtPhotoPath.Text = parts.Length > 4 ? parts[4] : "";
                ddlIsActive.SelectedValue = parts.Length > 5 && Convert.ToBoolean(parts[5]) ? "1" : "0";
                litFormTitle.Text = "Edit Table #" + parts[0]; btnSave.Text = "Update Table"; btnCancel.Visible = true;
            }
            else if (e.CommandName == "DeleteTable")
            {
                int tableId = Convert.ToInt32(e.CommandArgument);
                bool success = _repo.DeleteTable(tableId);
                ShowMessage(success ? "Table deleted successfully." : "Failed to delete table.", success);
                BindGrid();
            }
        }

        private void ClearForm()
        {
            hfTableId.Value = "0"; txtTableNumber.Text = ""; txtSeatingCapacity.Text = ""; txtLocation.Text = "";
            txtPhotoPath.Text = ""; ddlIsActive.SelectedValue = "1"; litFormTitle.Text = "Add New Table";
            btnSave.Text = "Save Table"; btnCancel.Visible = false;
        }

        private void ShowMessage(string msg, bool isSuccess)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Text = msg;
        }
    }
}
