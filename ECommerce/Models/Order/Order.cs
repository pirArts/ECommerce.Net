using System;
using System.Collections.Generic;
using ECommerce.Models.Account;

namespace ECommerce.Models.Order
{
    public class Order
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public int? State { get; set; }

        public int? TransactionType { get; set; }

        public float? ShipFee { get; set; }

        public DateTime CreateTime { get; set; }

        public int? AddressId { get; set; }

        public virtual Address Address { get; set; }

        public virtual ApplicationUser User { get; set; }

        public virtual IList<OrderItem> OrderItems { get; set; }
    }
}