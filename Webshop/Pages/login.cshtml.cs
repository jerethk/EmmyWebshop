using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Webshop.Models;

namespace Webshop.Pages
{
    public class loginModel : PageModel
    {
        [FromForm]
        public string loginEmail { get; set; }
        
        [FromForm]
        public string password { get; set; }

        public bool? isLoginSuccessful { get; set; }
        public string customerName { get; set; }

        private myshopContext shopContext { get; set; }

        public void OnGet()
        {
        }

        public async Task<IActionResult> OnPost()
        {
            if (loginEmail != null)
            {
                shopContext = new myshopContext();
                var customerQ = (from Customer c in shopContext.Customers
                                 where c.Email.ToLower() == loginEmail.ToLower()
                                 select c).ToList();

                if (customerQ.Count == 0)
                {
                    // customer not found
                    isLoginSuccessful = false;
                }
                else
                {
                    this.customerName = customerQ[0].Firstname;     // FOR TESTING

                    foreach (Customer c in customerQ)
                    {
                        if (this.password == c.Password)
                        {
                            isLoginSuccessful = true;

                            // save to session
                            HttpContext.Session.SetInt32("userLoggedIn", c.CustomerId);

                            return Redirect("Index");
                        }
                        else
                        {
                            isLoginSuccessful = false;
                        }
                    }
                }
            }

            return Page();
        }
    }
}
