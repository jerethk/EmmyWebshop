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
    public class checkoutModel : PageModel
    {
        public myshopContext shopContext { get; set; }
        public List<Customer> customerList { get; set; }
        public List<ShoppingCartItem> cartItems { get; set; }
        public decimal cartTotal { get; set; }

        [FromForm]
        public int customerId { get; set; }

        public void OnGet()
        {
            string cartJSON = HttpContext.Session.GetString("cart");

            if (cartJSON != null)
            {
                shopContext = new myshopContext();

                // Load cart
                cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);

                // Load customers
                customerList = (from Customer c in shopContext.Customers
                                select c).ToList();
            }

            // calculate total price of cart
            cartTotal = 0;
            foreach (ShoppingCartItem item in cartItems)
            {
                decimal price = item.product.Price * item.count;
                cartTotal += price;
            }
        }

        public async Task<IActionResult> OnPost()
        {
            // Record purchase in database
            shopContext = new myshopContext();
            string cartJSON = HttpContext.Session.GetString("cart");
            cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);

            // Transaction
            Transaction transaction = new Transaction();
            transaction.Customer = this.customerId;
            transaction.Date = DateTime.Now;
            transaction.Amount = this.cartTotal;
            shopContext.Transactions.Add(transaction);
            shopContext.SaveChanges();

            // Invoice items
            foreach (ShoppingCartItem cartItem in cartItems)
            {
                for (int i = 0; i < cartItem.count; i++)
                {
                    InvoiceItem invoiceItem = new InvoiceItem();
                    invoiceItem.Product = cartItem.product.ProductCode;
                    // should also add sale price !!!    = cartItem.product.Price;
                    invoiceItem.Invoice = transaction.InvoiceNo;
                    shopContext.InvoiceItems.Add(invoiceItem);
                }
            }

            shopContext.SaveChanges();

            return Redirect("Index");
        }
    }
}
