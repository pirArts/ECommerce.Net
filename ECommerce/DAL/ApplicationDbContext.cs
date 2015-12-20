using System.Data.Entity;
using ECommerce.Models;
using ECommerce.Models.Account;
using ECommerce.Models.Cart;
using ECommerce.Models.Catalog;
using ECommerce.Models.Order;
using Microsoft.AspNet.Identity.EntityFramework;

namespace ECommerce.DAL
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext()
            : base("DefaultConnection", throwIfV1Schema: false)
        {
        }

        public static ApplicationDbContext Create()
        {
            return new ApplicationDbContext();
        }

        public DbSet<Attendance> Attendances { get; set; }

        public DbSet<Vacation> Vacations { get; set; }

        public DbSet<Product> Products { get; set; }

        public DbSet<Category> Categories { get; set; }

        public DbSet<Models.Cart.Cart> Carts { get; set; }

        public DbSet<Models.Order.Order> Orders { get; set; }

        public DbSet<CartItem> CartItems { get; set; }

        public DbSet<OrderItem> OrderItems { get; set; }

        public DbSet<Address> Addresses { get; set; }
    }
}