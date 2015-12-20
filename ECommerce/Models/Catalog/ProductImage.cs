using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Catalog
{
    public class ProductImage
    {
        public int Id { get; set; }

        public int ProductId { get; set; }

        public string Url { get; set; }

        public virtual Product Product { get; set; }
    }
}