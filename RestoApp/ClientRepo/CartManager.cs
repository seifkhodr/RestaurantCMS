using System.Collections.Generic;
using System.Linq;
using System.Web;
using RestoApp.Models;

//  just Session[key] if you’re inside a page or control.
//If you’re outside that scope, you need HttpContext.Current to reach the session object.

//HttpContext represents the current HTTP request/response context in ASP.NET.
//It holds objects like Request, Response, Session, Server, etc.

namespace RestoApp
{
    public static class CartManager
    {
        private const string CartSessionKey = "UserCart";

        public static List<CartItem> GetCart()
        {
            //Session stores everything as object
            var cart = HttpContext.Current.Session[CartSessionKey] as List<CartItem>;
            if (cart == null)
            {
                cart = new List<CartItem>();
                HttpContext.Current.Session[CartSessionKey] = cart;
            }
            return cart;
        }

        public static void AddItem(Item item)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(i => i.ItemId == item.ItemId);
            if (existing != null)
            {
                existing.Quantity++;
            }
            else
            {
                cart.Add(new CartItem { ItemId = item.ItemId, ItemName = item.ItemName, Price = item.Price, Quantity = 1 });
            }
        }

        public static void RemoveItem(int itemId)
        {
            var cart = GetCart();
            var existing = cart.FirstOrDefault(i => i.ItemId == itemId);
            if (existing != null)
            {
                cart.Remove(existing);
            }
        }

        public static void ClearCart()
        {
            HttpContext.Current.Session[CartSessionKey] = null;
        }

        public static decimal GetTotal()
        {
            return GetCart().Sum(i => i.TotalPrice);
        }

        public static int GetTotalCount()
        {
            return GetCart().Sum(i => i.Quantity);
        }
    }
}
