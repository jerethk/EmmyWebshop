using System;
using System.Collections.Generic;

#nullable disable

namespace WebshopAPI.Models
{
    public partial class Product
    {
        public Product()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public string ProductCode { get; set; }
        public string Description { get; set; }
        public string Quantity { get; set; }
        public decimal Price { get; set; }
        public string Image { get; set; }
        public string Category { get; set; }

        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
