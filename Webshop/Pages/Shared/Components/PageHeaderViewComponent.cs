using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Webshop.Models;
using System.Text.Json;

namespace Webshop.Pages
{
    public class PageHeaderViewComponent : ViewComponent
    {
        public async Task<IViewComponentResult> InvokeAsync()
        {
            // Obtain logged in status and cart item count, and send to viewcomponent
            HeaderData headerData = new HeaderData();
            headerData.isLoggedIn = (HttpContext.Session.GetInt32("userLoggedIn") > 0);

            string cartJSON = HttpContext.Session.GetString("cart");
            if (cartJSON != null)
            {
                List<ShoppingCartItem> cart = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);
                headerData.cartItemCount = cart.Count();
            }
            else 
                headerData.cartItemCount = 0;
            
            return View(headerData);
        }
    }
}
