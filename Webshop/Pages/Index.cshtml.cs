using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Webshop.models;

namespace Webshop.Pages
{
    public class IndexModel : PageModel
    {
        private myshopContext shopContext;
        public List<Product> productList;
        public List<Customer> customerList;
        public List<Product> shoppingCart;

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

            /////////////////////
            SessionExtensions.SetString(HttpContext.Session, "testsession", "mysessionstring");
            
            if (addToCart != "")
            {
                
            }

            return Page();
        }
    }
}
