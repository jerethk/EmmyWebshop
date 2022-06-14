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
    [BindProperties]
    public class customerAccountModel : PageModel
    {
        private myshopContext _shopContext;

        [FromQuery]
        public string mode { get; set; }

        public string email { get; set; }
        public string password { get; set; }
        public string password2 { get; set; }
        public int customerId { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string address { get; set; }
        public string state { get; set; }
        public string postcode { get; set; }
        public string phone { get; set; }

        public string message { get; set; }


        public void OnGet()
        {
            if (this.mode == "edit")
            {
                // Get customer
                Customer customer = null;
                int sessionCustomerId = (int) HttpContext.Session.GetInt32("userLoggedIn");

                using (_shopContext = new myshopContext())
                {
                    var queryResult = (from Customer c in _shopContext.Customers
                                       where c.CustomerId == sessionCustomerId
                                       select c).ToList();

                    if (queryResult.Count > 0)
                    {
                        customer = queryResult[0];

                        this.customerId = customer.CustomerId;
                        this.email = customer.Email;
                        this.password = customer.Password;
                        this.firstName = customer.Firstname;
                        this.lastName = customer.Lastname;
                        this.address = customer.Address;
                        this.state = customer.State;
                        this.postcode = customer.Postcode;
                        this.phone = customer.Phone;
                    }
                }
            }
        }

        public async Task<IActionResult> OnPostAsync()
        {
            if (this.mode == "edit")
            {
                using (_shopContext = new myshopContext())
                {
                    var queryResult = (from Customer c in _shopContext.Customers
                                       where c.CustomerId == this.customerId
                                       select c).ToList();

                    if (queryResult.Count > 0)
                    {
                        Customer customer = queryResult[0];
                        customer.Email = this.email;
                        customer.Firstname = this.firstName;
                        customer.Lastname = this.lastName;
                        customer.Address = this.address;
                        customer.State = this.state;
                        customer.Postcode = this.postcode;
                        customer.Phone = this.phone;

                        _shopContext.SaveChanges();
                    }
                }

                this.message = "Account details have been updated.";
            }
            else if (this.mode == "create")
            {
                using (_shopContext = new myshopContext())
                {
                    // Check if email already exists in db
                    var queryResult = (from Customer c in _shopContext.Customers
                                       where c.Email == this.email
                                       select c).ToList();

                    if (queryResult.Count > 0)
                    {
                        this.message = $"The email address {this.email} is already taken. Please enter another email address.";
                        this.email = "";
                        return Page();
                    }
                    else
                    {
                        Customer newCustomer = new Customer();
                        newCustomer.Email = this.email;
                        newCustomer.Firstname = this.firstName;
                        newCustomer.Lastname = this.lastName;
                        newCustomer.Address = this.address;
                        newCustomer.State = this.state;
                        newCustomer.Postcode = this.postcode;
                        newCustomer.Phone = this.phone;
                        newCustomer.Password = this.password;

                        _shopContext.Customers.Add(newCustomer);
                        _shopContext.SaveChanges();

                        // Log the new customer in
                        HttpContext.Session.SetInt32("userLoggedIn", newCustomer.CustomerId);
                    }
                }

                this.message = "Successfully created new account!";
                this.mode = "edit";
            }

            return Page();
        }
    }
}
