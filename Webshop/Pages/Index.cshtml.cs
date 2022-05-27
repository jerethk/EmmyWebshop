using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Webshop.Pages
{
    public class IndexModel : PageModel
    {
        private models.myshopContext shopContext;
        public List<models.Product> productList;

        [FromQuery]
        public string productCategory { get; set; }
        public string addToCart { get; set; }

        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            // Load products from database
            shopContext = new models.myshopContext();
            
            if (this.productCategory != null)
            {
                productList = (from models.Product p in shopContext.Products
                               where p.Category == this.productCategory
                               select p).ToList();
            }

            return Page();
        }
    }
}
