using System;
using System.Collections.Generic;

namespace ECommerce.Models.Order
{
    public class GetOrdersViewModel
    {
        public int Id { get; set; }

        public int? State { get; set; }

        public int? TransactionType { get; set; }

        public float? ShipFee { get; set; }

        public GetAddressViewModel Address { get; set; }

        public DateTime CreateTime { get; set; }

        public IList<OrderItemViewModel> OrderItems;
    }

    public class OrderItemViewModel
    {
        public int Id { get; set; }

        public int Amount { get; set; }

        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public float ProductPrice { get; set; }

        public string ProductDescription { get; set; }

        public string ProductThumbnail { get; set; }
    }

    public class GetAddressViewModel
    {
        public int? Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }

    public class AddOrderViewModel
    {
        public int OrderId { get; set; }
    }
}