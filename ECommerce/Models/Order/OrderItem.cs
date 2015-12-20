using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using ECommerce.Models.Catalog;

namespace ECommerce.Models.Order
{
    public class OrderItem
    {
        public int Id { get; set; }

        public int OrderId { get; set; }

        public int ProductId { get; set; }

        public int Amount { get; set; }

        public virtual Order Order { get; set; }

        public virtual Product Product { get; set; }
    }
}