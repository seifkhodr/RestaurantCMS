using System;
using RestoApp.Models;

namespace RestoApp.ClientsView.Items
{
    public partial class ItemDetails : System.Web.UI.Page
    {
        private ItemMenuRepository _repository = new ItemMenuRepository();

        public int CurrentItemId
        {
            get
            {
                if (int.TryParse(Request.QueryString["id"], out int id)) return id;
                return 0;
            }
        }

        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                if (CurrentItemId > 0)
                {
                    LoadItemDetails();
                }
                else
                {
                    ShowError();
                }
            }
        }

        private void LoadItemDetails()
        {
            Item item = _repository.GetItemById(CurrentItemId);

            if (item != null)
            {
                // Populate UI elements
                imgMain.ImageUrl = item.PrimaryPhotoPath;
                imgMain.AlternateText = item.ItemName;
                
                litCategory.Text = item.MenuName;
                litItemName.Text = item.ItemName;
                litCurrency.Text = item.Currency;
                litPrice.Text = item.Price.ToString("0.00");
                litOrigin.Text = string.IsNullOrEmpty(item.Origin) ? "House Special / Unspecified" : item.Origin;
                litDescription.Text = item.Description;
                litIngredients.Text = string.IsNullOrEmpty(item.Ingredients) ? "Secret Recipe" : item.Ingredients;

                // Load Similar Items (same category, limit 3)
                LoadSimilarItems(item.MenuId);
            }
            else
            {
                ShowError();
            }
        }

        private void LoadSimilarItems(int currentCategory)
        {
            var similarItems = _repository.GetSimilarItems(CurrentItemId, currentCategory, 3);
            
            if (similarItems.Count > 0)
            {
                rptSimilar.DataSource = similarItems;
                rptSimilar.DataBind();
                pnlSimilarItems.Visible = true;
            }
            else
            {
                pnlSimilarItems.Visible = false; // Hide section if no similar items
            }
        }

        private void ShowError()
        {
            pnlDetails.Visible = false;
            pnlSimilarItems.Visible = false;
            pnlError.Visible = true;
        }

        protected void btnAddToCart_Click(object sender, EventArgs e)
        {
            /*
            // Session protection snippet (commented out until needed)
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Authentication/Login.aspx");
                return;
            }
            */

            if (CurrentItemId > 0)
            {
                var item = _repository.GetItemById(CurrentItemId);
                if (item != null)
                {
                    RestoApp.CartManager.AddItem(item);
                    Response.Redirect(Request.RawUrl); 
                }
            }
        }
    }
}
