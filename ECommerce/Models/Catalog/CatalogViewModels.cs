using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Catalog
{
    public class ProductViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }
    }

    public class ProductDetailViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string Thumbnail { get; set; }

        public int Stock { get; set; }

        public int SalesCount { get; set; }

        public DateTime CreateTime { get; set; }

        public IList<ProductImage> ProductImages { get; set; } 
    }

    public class CategoryViewModel
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }
    }
}