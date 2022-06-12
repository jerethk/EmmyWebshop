﻿using System;
using System.Collections.Generic;

#nullable disable

namespace WebshopAPI.Models
{
    public partial class Customer
    {
        public Customer()
        {
            Transactions = new HashSet<Transaction>();
        }

        public int CustomerId { get; set; }
        public string Firstname { get; set; }
        public string Lastname { get; set; }
        public string Address { get; set; }
        public string Postcode { get; set; }
        public string State { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public decimal Balance { get; set; }
        public string Password { get; set; }

        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
