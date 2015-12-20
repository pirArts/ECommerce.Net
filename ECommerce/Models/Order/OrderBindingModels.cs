using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace ECommerce.Models.Order
{
    public class AddAddressBindingModel
    {
        public string Name { get; set; }

        public string Phone { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }
    }

    public class OrderItemBindingModel
    {
        public int ProductId { get; set; }

        public int Amount { get; set; }
    }

    public class AddOrderBindingModel
    {
        public IList<OrderItemBindingModel> OrderItems { get; set; }
    }

    public class GetOrderByIdBindingModel
    {
        public int OrderId { get; set; }
    }

    public class UpdateOrderAddressBindingModel
    {
        public int OrderId { get; set; }
        public int AddressId { get; set; }
    }
}