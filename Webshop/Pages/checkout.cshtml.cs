using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Models;

namespace Webshop.Pages
{
    [BindProperties]
    public class checkoutModel : PageModel
    {
        private myshopContext shopContext { get; set; }
        private List<ShoppingCartItem> cartItems { get; set; }

        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string phone { get; set; }
        public string email { get; set; }

        [FromForm]
        public decimal totalPrice { get; set; }

        public void OnGet()
        {
            string cartJSON = HttpContext.Session.GetString("cart");

            if (cartJSON != null)
            {
                shopContext = new myshopContext();

                // Load cart
                cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);

                // calculate total price of cart
                decimal cartTotal = 0;
                foreach (ShoppingCartItem item in cartItems)
                {
                    decimal price = item.product.Price * item.count;
                    cartTotal += price;
                }
                totalPrice = cartTotal;     // bind to hidden form element
            }

            // customer details
            int? id = HttpContext.Session.GetInt32("userLoggedIn");
            if (id != null)
            {
                this.customerId = (int)id;

                var customer = (from Customer c in shopContext.Customers
                                where c.CustomerId == this.customerId
                                select c).ToList()[0];

                this.firstName = customer.Firstname;
                this.lastName = customer.Lastname;
                this.address = customer.Address;
                this.state = customer.State;
                this.postcode = customer.Postcode;
                this.email = customer.Email;
                this.phone = customer.Phone;
            }
            else
            {
                this.customerId = -1;   // guest
            }
        }

        public async Task<IActionResult> OnPost() 
        {
            try
            {
                // Record purchase in database
                shopContext = new myshopContext();
                string cartJSON = HttpContext.Session.GetString("cart");
                cartItems = JsonSerializer.Deserialize<List<ShoppingCartItem>>(cartJSON);

                // Transaction
                Transaction transaction = new Transaction();
                transaction.Customer = this.customerId;
                transaction.Date = DateTime.Now;
                transaction.Amount = this.totalPrice;
                shopContext.Transactions.Add(transaction);
                shopContext.SaveChanges();

                // Invoice items
                foreach (ShoppingCartItem cartItem in cartItems)
                {
                    for (int i = 0; i < cartItem.count; i++)
                    {
                        InvoiceItem invoiceItem = new InvoiceItem();
                        invoiceItem.Product = cartItem.product.ProductCode;
                        invoiceItem.SoldPrice = cartItem.product.Price;
                        invoiceItem.Invoice = transaction.InvoiceNo;
                        shopContext.InvoiceItems.Add(invoiceItem);
                    }
                }

                shopContext.SaveChanges();

                // clear the cart
                HttpContext.Session.SetString("cart", JsonSerializer.Serialize(new List<ShoppingCartItem>()));
            }
            catch (Exception e)
            {
                string error = e.Message;
            }

            return Redirect("Index");
        }

    }
}
