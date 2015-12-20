using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Cart
{
    public class AddProductToCartBindingModel
    {
        [Required]
        public int ProductId { get; set; }

        [Required]
        public int Amount { get; set; }
    }

    public class RemoveProductBindingModel
    {
        public int CartItemId { get; set; }
    }

    public class UpdateProductAmountBindingModel
    {
        public int CartItemId { get; set; }

        public int Amount { get; set; }
    }
}