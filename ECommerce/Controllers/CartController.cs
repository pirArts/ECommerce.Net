using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using ECommerce.DAL;
using ECommerce.Models.Cart;
using Microsoft.AspNet.Identity;

namespace ECommerce.Controllers
{
    [Authorize]
    [RoutePrefix("api/Cart")]
    public class CartController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // Get: api/Cart/GetCartProducts
        [Route("GetCartProducts")]
        [HttpGet]
        public IList<GetCartProductsViewModel> GetCartProducts()
        {
            var cartProductsList = new List<GetCartProductsViewModel>();

            var currentUserId = User.Identity.GetUserId();

            if (currentUserId != null)
            {
                var cartItems = db.CartItems.Include(c => c.Cart).Where(c => c.Cart.UserId == currentUserId).Include(c => c.Product).Select(a => new GetCartProductsViewModel()
                {
                    CartItemId = a.Id,
                    ProductId = a.ProductId,
                    Amount = a.Amount,
                    ProductName = a.Product.Name,
                    Price = a.Product.Price
                });

                cartProductsList = cartItems.ToList();
            }

            return cartProductsList;
        }

        // POST: api/Cart/RemoveProduct
        [Route("RemoveProduct")]
        [HttpPost]
        public IHttpActionResult RemoveProduct(RemoveProductBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = false;
            string message = string.Empty;

            try
            {
                var cartItem = db.CartItems.FirstOrDefault(v => v.Id == model.CartItemId);

                if (cartItem != null)
                {
                    db.CartItems.Remove(cartItem);
                    db.SaveChanges();

                    success = true;
                }
                else
                {
                    message = "Cart Item Id not found";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok();
        }

        // POST: api/Cart/UpdateProductAmount
        [Route("UpdateProductAmount")]
        [HttpPost]
        public IHttpActionResult UpdateProductAmount(UpdateProductAmountBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = false;
            string message = string.Empty;

            try
            {
                var cartItem = db.CartItems.FirstOrDefault(v => v.Id == model.CartItemId);

                if (cartItem != null)
                {
                    cartItem.Amount = model.Amount;
                    db.SaveChanges();

                    success = true;
                }
                else
                {
                    message = "Cart Item Id not found";
                }

            }
            catch (Exception ex)
            {
                message = ex.Message;
            }

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok();
        }

        // POST: api/Cart/ClearCart
        [Route("ClearCart")]
        [HttpGet]
        public IHttpActionResult ClearCart()
        {
            bool success = false;
            string message = string.Empty;

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var currentUserId = User.Identity.GetUserId();

                    if (currentUserId != null)
                    {
                        var cartItems =
                            db.CartItems.Include(c => c.Cart)
                                .Where(c => c.Cart.UserId == currentUserId)
                                .AsEnumerable()
                                .ToList();
                        db.CartItems.RemoveRange(cartItems);

                        db.SaveChanges();

                        success = true;
                    }
                    else
                    {
                        message = "current user id is null";
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    dbContextTransaction.Rollback();
                }
            }

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok();
        }

        // POST: api/Cart/AddProductToCart
        [Route("AddProductToCart")]
        [HttpPost]
        public IHttpActionResult AddProductToCart(AddProductToCartBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = false;
            string message = string.Empty;

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var currentUserId = User.Identity.GetUserId();
                    var userCart = db.Carts.FirstOrDefault(v => v.UserId == currentUserId);

                    if (userCart == null)
                    {
                        Cart cart = new Cart()
                        {
                            UserId = currentUserId
                        };

                        db.Carts.Add(cart);
                        db.SaveChanges();
                    }

                    if (userCart != null)
                    {
                        CartItem cartItem = new CartItem()
                        {
                            ProductId = model.ProductId,
                            Amount = model.Amount,
                            CartId = userCart.Id
                        };

                        db.CartItems.Add(cartItem);
                        db.SaveChanges();

                        dbContextTransaction.Commit();

                        success = true;
                    }
                }
                catch (Exception ex)
                {
                    message = ex.Message;
                    dbContextTransaction.Rollback();
                }
            }

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok();
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
