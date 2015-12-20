using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Models.Catalog;

namespace ECommerce.Models.Cart
{
    public class CartItem
    {
        public int Id { get; set; }

        public int CartId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public virtual Cart Cart { get; set; }

        public virtual Product Product { get; set; }
    }
}