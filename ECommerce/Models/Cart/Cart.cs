using ECommerce.Models.Account;

namespace ECommerce.Models.Cart
{
    public class Cart
    {
        public int Id { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }
}