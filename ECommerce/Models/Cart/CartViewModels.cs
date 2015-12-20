using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Cart
{
    public class GetCartProductsViewModel
    {
        public int CartItemId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public string ProductName { get; set; }

        public float Price { get; set; }
    }
}