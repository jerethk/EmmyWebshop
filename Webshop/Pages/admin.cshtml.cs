using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Models;

namespace Webshop.Pages
{
    public class adminModel : PageModel
    {
        [FromQuery]
        public string viewType { get; set;  }

        [FromForm]
        public int? invNumber { get; set; }

        private myshopContext shopContext;
        public List<Customer> customerList { get; set; }
        public List<Transaction> transactionList { get; set; }
        
        // Properties for viewing invoice items
        public List<InvoiceItemWithPrice> invoiceItemList { get; set; }
        public decimal invoiceTotal;
        
        public async Task<IActionResult> OnGetAsync()
        {
            shopContext = new myshopContext();
            
            if (this.viewType == "customers")
            {
                customerList = (from Customer customer in shopContext.Customers
                                select customer).ToList();
            }

            if (this.viewType == "transactions")
            {
                transactionList = (from Transaction t in shopContext.Transactions
                                   select t).ToList();
            }

            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            shopContext = new myshopContext();

            transactionList = (from Transaction t in shopContext.Transactions
                               select t).ToList();

            if (this.invNumber != null)
            {
                // Get the invoice total. 
                var q = from Transaction t in transactionList
                        where t.InvoiceNo == invNumber
                        select t;
                
                foreach (Transaction t in q) invoiceTotal = t.Amount;   // there should only be 1 item with matching invNumber

                // Get the invoice items
                this.invoiceItemList = (from InvoiceItem i in shopContext.InvoiceItems
                                        join Product p in shopContext.Products on i.Product equals p.ProductCode
                                        where i.Invoice == invNumber
                                        select new InvoiceItemWithPrice()
                                        {
                                            productCode = i.Product,
                                            priceSold = i.SoldPrice,
                                            priceCurrent = p.Price
                                        }).ToList();
            }

            return Page();
        }
    }

    public class InvoiceItemWithPrice
    {
        public string productCode { get; set; }
        public decimal? priceSold { get; set; }
        public decimal priceCurrent { get; set; }
    }
}
