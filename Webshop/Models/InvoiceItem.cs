using System;
using System.Collections.Generic;

#nullable disable

namespace Webshop.Models
{
    public partial class InvoiceItem
    {
        public int RecordId { get; set; }
        public int Invoice { get; set; }
        public string Product { get; set; }
        public decimal? SoldPrice { get; set; }

        public virtual Transaction InvoiceNavigation { get; set; }
        public virtual Product ProductNavigation { get; set; }
    }
}
