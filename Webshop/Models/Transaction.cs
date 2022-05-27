using System;
using System.Collections.Generic;

#nullable disable

namespace Webshop.models
{
    public partial class Transaction
    {
        public Transaction()
        {
            InvoiceItems = new HashSet<InvoiceItem>();
        }

        public int InvoiceNo { get; set; }
        public DateTime? Date { get; set; }
        public int Customer { get; set; }
        public decimal Amount { get; set; }

        public virtual Customer CustomerNavigation { get; set; }
        public virtual ICollection<InvoiceItem> InvoiceItems { get; set; }
    }
}
