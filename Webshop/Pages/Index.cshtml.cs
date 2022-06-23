using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Collections.Generic;
using System.Threading.Tasks;


namespace Webshop.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> OnGetAsync()
        {
            /*
            // Load products from database based on chosen category
            shopContext = new myshopContext();
            
            if (this.productCategory != null)
            {
                productList = (from Product p in shopContext.Products
                               where p.Category == this.productCategory
                               select p).ToList();
            }

            // Load customers
            // customerList = (from Customer c in shopContext.Customers select c).ToList();

            */

            return Page();
        }
    }
}
