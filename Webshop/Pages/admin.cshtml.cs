using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace Webshop.Pages
{
    public class adminModel : PageModel
    {
        [FromQuery]
        public string view { get; set;  }

        private models.myshopContext shopContext;
        public List<models.Customer> customerList { get; set; }
        public List<models.Transaction> transactionList { get; set; }
        
        public async void OnGetAsync()
        {
            shopContext = new models.myshopContext();
            
            if (this.view == "customers")
            {
                customerList = (from models.Customer customer in shopContext.Customers
                                select customer).ToList();
            }

            if (this.view == "transactions")
            {
                transactionList = (from models.Transaction t in shopContext.Transactions
                                   select t).ToList();
            }
        }
    }
}
