using ECommerce.Models.Account;

namespace ECommerce.Models.Order
{
    public class Address
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Province { get; set; }

        public string City { get; set; }

        public string Country { get; set; }

        public string Street { get; set; }

        public string ZipCode { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}