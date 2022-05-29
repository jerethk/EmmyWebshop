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
        public List<ShoppingCartItem> cartItems { get; set; }

        public async Task<IActionResult> OnGetAsync()
        {
            string cartJSON = HttpContext.Session.GetString("cart");
            cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);

            return Page();
        }
    }

    public class ShoppingCartItem
    {
        public Product product { get; set; }
        public int count { get; set; }
    }
}
