using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Catalog
{
    public class AddProductBindingModel
    {
        public string Name { get; set; }

        public float Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; }

        public int CategoryId { get; set; }
    }

    public class AddCategoryBindingModel
    {
        public string Name { get; set; }

        public string Description { get; set; }
    }

    public class GetProductsByCategoryBindingModel
    {
        public int CategoryId { get; set; }
    }

    public class GetProductsBindingModel
    {
        [Required]
        public int StartIndex { get; set; }

        [Required]
        public int Offset { get; set; }

        public string Filter { get; set; }

        public string Keyword { get; set; }
    }

    public class GetProductDetailBindingModel
    {
        public int ProductId { get; set; }
    }
}