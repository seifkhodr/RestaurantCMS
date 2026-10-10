using System;
using System.Web.UI.WebControls;
using RestoApp.Models;
using RestoApp.AdminRepo;
using RestoApp.Helper;

namespace RestoApp.AdminView.Items
{
    public partial class AdminItems : System.Web.UI.Page
    {
        private ItemRepositoryAdmin _itemRepo = new ItemRepositoryAdmin();
        private MenuRepositoryAdmin _menuRepo = new MenuRepositoryAdmin();

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!AuthHelper.RequireAdmin(this))
            {
                return;
            }

            if (!IsPostBack) { LoadMenuDropdown(); BindGrid(); }
        }

        private void LoadMenuDropdown()
        {
            ddlMenu.Items.Clear();
            var menus = _menuRepo.GetAllMenus();
            foreach (var menu in menus) { ddlMenu.Items.Add(new ListItem(menu.MenuName, menu.MenuId.ToString())); }
        }

        private void BindGrid()
        {
            gvItems.DataSource = _itemRepo.GetAllItems();
            gvItems.DataBind();
        }

        protected void btnSave_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(txtItemName.Text)) { ShowMessage("Item name is required.", false); return; }
            if (!decimal.TryParse(txtPrice.Text, out decimal price) || price < 0) { ShowMessage("Please enter a valid price.", false); return; }
            int itemId = Convert.ToInt32(hfItemId.Value);
            Item model = new Item
            {
                ItemId = itemId, MenuId = Convert.ToInt32(ddlMenu.SelectedValue), ItemName = txtItemName.Text.Trim(),
                Description = txtDescription.Text.Trim(), Price = price,
                Currency = string.IsNullOrWhiteSpace(txtCurrency.Text) ? "$" : txtCurrency.Text.Trim(),
                Ingredients = txtIngredients.Text.Trim(), Origin = txtOrigin.Text.Trim(), IsAvailable = ddlIsAvailable.SelectedValue == "1"
            };
            if (itemId > 0)
            {
                bool success = _itemRepo.UpdateItem(model);
                ShowMessage(success ? "Item updated successfully." : "Failed to update item.", success);
            }
            else
            {
                int newId = _itemRepo.InsertItem(model);
                if (newId > 0)
                {
                    if (!string.IsNullOrWhiteSpace(txtPhotoUrl.Text)) { _itemRepo.InsertItemPhoto(newId, txtPhotoUrl.Text.Trim(), true); }
                    ShowMessage($"Item created successfully (ID: {newId}).", true);
                }
                else { ShowMessage("Failed to create item.", false); }
            }
            ClearForm(); BindGrid();
        }

        protected void btnCancel_Click(object sender, EventArgs e) { ClearForm(); }

        protected void gvItems_RowCommand(object sender, GridViewCommandEventArgs e)
        {
            if (e.CommandName == "EditItem")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                Item item = _itemRepo.GetItemById(itemId);
                if (item != null)
                {
                    hfItemId.Value = item.ItemId.ToString(); txtItemName.Text = item.ItemName;
                    ddlMenu.SelectedValue = item.MenuId.ToString(); txtPrice.Text = item.Price.ToString("0.00");
                    txtCurrency.Text = item.Currency; txtDescription.Text = item.Description;
                    txtIngredients.Text = item.Ingredients; txtOrigin.Text = item.Origin;
                    txtPhotoUrl.Text = item.PrimaryPhotoPath; ddlIsAvailable.SelectedValue = item.IsAvailable ? "1" : "0";
                    litFormTitle.Text = "Edit Item #" + item.ItemId; btnSave.Text = "Update Item"; btnCancel.Visible = true;
                }
            }
            else if (e.CommandName == "DeleteItem")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                bool success = _itemRepo.DeleteItem(itemId);
                ShowMessage(success ? "Item deleted successfully." : "Failed to delete item.", success);
                BindGrid();
            }
        }

        private void ClearForm()
        {
            hfItemId.Value = "0"; txtItemName.Text = ""; txtDescription.Text = ""; txtPrice.Text = "";
            txtCurrency.Text = "$"; txtIngredients.Text = ""; txtOrigin.Text = ""; txtPhotoUrl.Text = "";
            ddlIsAvailable.SelectedValue = "1"; if (ddlMenu.Items.Count > 0) ddlMenu.SelectedIndex = 0;
            litFormTitle.Text = "Add New Item"; btnSave.Text = "Save Item"; btnCancel.Visible = false;
        }

        private void ShowMessage(string msg, bool isSuccess)
        {
            pnlMessage.Visible = true;
            pnlMessage.CssClass = isSuccess ? "alert alert-success" : "alert alert-danger";
            lblMessage.Text = msg;
        }
    }
}
