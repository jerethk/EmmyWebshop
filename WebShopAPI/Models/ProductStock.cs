using System;
using System.Collections.Generic;

#nullable disable

namespace WebshopAPI.Models
{
    public partial class ProductStock
    {
        public string ProductCode { get; set; }
        public int? Stock { get; set; }
    }
}
