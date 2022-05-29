using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

using Webshop.models;

namespace Webshop.Pages
{
    public class IndexModel : PageModel
    {
        private myshopContext shopContext;
        public List<Product> productList { get; set; }
        public List<Customer> customerList { get; set; }

        [FromQuery]
        public string productCategory { get; set; }
        
        [FromQuery]
        public string addToCart { get; set; }

        [FromForm]
        public int customerId { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Load products from database
            shopContext = new myshopContext();
            
            if (this.productCategory != null)
            {
                productList = (from Product p in shopContext.Products
                               where p.Category == this.productCategory
                               select p).ToList();
            }

            // Load customers
            customerList = (from Customer c in shopContext.Customers
                            select c).ToList();

            // Shopping cart management
            if (addToCart != null)
            {
                List<ShoppingCartItem> shoppingCart = new List<ShoppingCartItem>();
                
                // deserialise existing shopping cart (List) from session state
                if (HttpContext.Session.GetString("cart") != null)
                {
                    shoppingCart = JsonSerializer.Deserialize<List<ShoppingCartItem>>(HttpContext.Session.GetString("cart"));
                }
                
                // query the selected product
                var q = (from p in productList
                         where p.ProductCode == addToCart
                         select p).ToList();

                // add product to cart
                if (q.Count > 0)
                {
                    // check if product is already in cart
                    Product p = q[0];
                    int index = -1;
                    for (int i = 0; i < shoppingCart.Count; i++)
                    {
                        if (shoppingCart[i].product.ProductCode == p.ProductCode)
                        {
                            index = i;
                            break;
                        }
                    }
                    
                    if (index == -1)
                    {
                        // add product to cart
                        shoppingCart.Add(new ShoppingCartItem() { product = p, count = 1 } );
                    }
                    else
                    {
                        // increment existing item in cart
                        shoppingCart[index].count++;
                    }
                }

                // serialise shopping cart and store in session
                string jsonCart = JsonSerializer.Serialize(shoppingCart);
                HttpContext.Session.SetString("cart", jsonCart);
            }

            return Page();
        }
    }
}
