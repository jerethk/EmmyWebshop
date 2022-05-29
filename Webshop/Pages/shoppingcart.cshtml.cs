using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.models;

namespace Webshop.Pages
{
    public class shoppingcartModel : PageModel
    {
        [FromQuery]
        public string action { get; set; }

        [FromQuery]
        public string product { get; set; }
        
        public List<ShoppingCartItem> cartItems { get; set; }
        public decimal cartTotal { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string cartJSON = HttpContext.Session.GetString("cart");
            
            if (cartJSON != null)
            {
                cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);

                // add item
                if (action == "add")
                {
                    foreach (ShoppingCartItem item in cartItems)
                    {
                        if (item.product.ProductCode == this.product)
                        {
                            item.count += 1;
                            break;
                        }
                    }

                    updateCart();
                }

                // remove item
                if (action == "remove")
                {
                    foreach (ShoppingCartItem item in cartItems)
                    {
                        if (item.product.ProductCode == this.product)
                        {
                            item.count -= 1;

                            if (item.count == 0)
                            {
                                cartItems.Remove(item);
                            }
                            
                            break;
                        }
                    }

                    updateCart();
                }

                // calculate total price of cart
                cartTotal = 0;
                foreach (ShoppingCartItem item in cartItems)
                {
                    decimal price = item.product.Price * item.count;
                    cartTotal += price;
                }
            }

            return Page();
        }

        private void updateCart()
        {
            // reserialise and store in session
            string cartJSON = JsonSerializer.Serialize(cartItems);
            HttpContext.Session.SetString("cart", cartJSON);
        }
    }

    public class ShoppingCartItem
    {
        public Product product { get; set; }
        public int count { get; set; }
    }
}
