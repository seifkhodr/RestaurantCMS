using System;
using System.Web.UI.WebControls;


namespace RestoApp.ClientsView
{
    public partial class CartPage: System.Web.UI.Page
    {
        protected void Page_Load(object sender, EventArgs e)
        {
            if (!IsPostBack)
            {
                BindCart();
            }
        }

        private void BindCart()
        {
            var items = RestoApp.CartManager.GetCart();

            if (items != null && items.Count > 0)
            {
                pnlCartItems.Visible = true;
                pnlEmptyCart.Visible = false;

                rptCartItems.DataSource = items;
                rptCartItems.DataBind();

                decimal subtotal = RestoApp.CartManager.GetTotal();
                decimal tax = subtotal * 0.08m; 
                decimal total = subtotal + tax;

                litSubtotal.Text = "$" + subtotal.ToString("0.00");
                litTax.Text = "$" + tax.ToString("0.00");
                litTotal.Text = "$" + total.ToString("0.00");
            }
            else
            {
                pnlCartItems.Visible = false;
                pnlEmptyCart.Visible = true;
            }
        }

        protected void rptCartItems_ItemCommand(object source, RepeaterCommandEventArgs e)
        {
            if (e.CommandName == "Remove")
            {
                int itemId = Convert.ToInt32(e.CommandArgument);
                RestoApp.CartManager.RemoveItem(itemId);
                
                BindCart(); 
                
                Response.Redirect(Request.RawUrl);
            }
        }

        protected void btnCheckout_Click(object sender, EventArgs e)
        {
            if (Session["UserId"] == null)
            {
                Response.Redirect("~/Authentication/Login.aspx");
                return;
            }

            RestoApp.CartManager.ClearCart();
            ClientScript.RegisterStartupScript(this.GetType(), "alert", "alert('Checkout Successful! Payment processing is a placeholder.'); window.location='../Menu/Menu.aspx';", true);
        }
    }
}
