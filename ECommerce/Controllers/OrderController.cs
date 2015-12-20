using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using ECommerce.DAL;
using ECommerce.Models.Order;
using Microsoft.AspNet.Identity;

namespace ECommerce.Controllers
{
    [Authorize]
    [RoutePrefix("api/Order")]
    public class OrderController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // Get: api/Order/GetOrders
        [Route("GetOrders")]
        [HttpGet]
        public IList<GetOrdersViewModel> GetOrders()
        {
            var orderList = new List<GetOrdersViewModel>();
            var currentUserId = User.Identity.GetUserId();

            if (currentUserId != null)
            {
                var orders = db.Orders.Where(a => a.UserId == currentUserId).Include(a => a.Address).Include(a => a.OrderItems.Select(i => i.Product)).Select(o => new GetOrdersViewModel()
                {
                    Id = o.Id,
                    State = o.State,
                    CreateTime = o.CreateTime,
                    ShipFee = o.ShipFee,
                    TransactionType = o.TransactionType,
                    Address = new GetAddressViewModel()
                    {
                        City = o.Address.City,
                        Country = o.Address.Country,
                        Id = o.AddressId,
                        Name = o.Address.Name,
                        Phone = o.Address.Phone,
                        Province = o.Address.Province,
                        Street = o.Address.Street,
                        ZipCode = o.Address.ZipCode
                    },
                    OrderItems = o.OrderItems.Select(i => new OrderItemViewModel()
                    {
                        Id = i.Id,
                        Amount = i.Amount,
                        ProductId = i.ProductId,
                        ProductDescription = i.Product.Description,
                        ProductName = i.Product.Name,
                        ProductPrice = i.Product.Price,
                        ProductThumbnail = i.Product.Thumbnail
                    }).ToList()
                });

                orderList = orders.ToList();
            }

            return orderList;
        }

        // POST:api/Order/GetOrderById
        [Route("GetOrderById")]
        [HttpPost]
        public GetOrdersViewModel GetOrderById(GetOrderByIdBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return null;
            }

            GetOrdersViewModel order = null;

            var currentUserId = User.Identity.GetUserId();
            if (currentUserId != null)
            {
                var orders = db.Orders.Where(a => a.UserId == currentUserId).Where(o=> o.Id == model.OrderId).Include(a => a.Address).Include(a => a.OrderItems.Select(i => i.Product)).Select(o => new GetOrdersViewModel()
                {
                    Id = o.Id,
                    State = o.State,
                    CreateTime = o.CreateTime,
                    ShipFee = o.ShipFee,
                    TransactionType = o.TransactionType,
                    Address = new GetAddressViewModel()
                    {
                        City = o.Address.City,
                        Country = o.Address.Country,
                        Id = o.AddressId,
                        Name = o.Address.Name,
                        Phone = o.Address.Phone,
                        Province = o.Address.Province,
                        Street = o.Address.Street,
                        ZipCode = o.Address.ZipCode
                    },
                    OrderItems = o.OrderItems.Select(i => new OrderItemViewModel()
                    {
                        Id = i.Id,
                        Amount = i.Amount,
                        ProductId = i.ProductId,
                        ProductDescription = i.Product.Description,
                        ProductName = i.Product.Name,
                        ProductPrice = i.Product.Price,
                        ProductThumbnail = i.Product.Thumbnail
                    }).ToList()
                });

                order = orders.FirstOrDefault();
            }

            return order;
        }

        // POST: api/Order/UpdateAddress
        [Route("UpdateAddress")]
        [HttpPost]
        public IHttpActionResult UpdateAddress(UpdateOrderAddressBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = false;
            string message = string.Empty;

            try
            {
                var order = db.Orders.FirstOrDefault(o => o.Id == model.OrderId);

                if (order != null)
                {
                    order.AddressId = model.AddressId;
                    db.SaveChanges();

                    success = true;
                }
                else
                {
                    message = "OderId not found";
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

        // POST: api/Order/AddOrder
        [Route("AddOrder")]
        [HttpPost]
        public IHttpActionResult AddOrder(AddOrderBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = false;
            string message = string.Empty;
            AddOrderViewModel addOrderViewModel = null;

            using (var dbContextTransaction = db.Database.BeginTransaction())
            {
                try
                {
                    var currentUserId = User.Identity.GetUserId();

                    Order order = new Order()
                    {
                        UserId = currentUserId,
                        CreateTime = DateTime.Now,
                        State = 0,
                        ShipFee = 7,
                        TransactionType = 0
                    };

                    // Create the order and save it
                    db.Orders.Add(order);
                    db.SaveChanges();

                    // create the order items
                    foreach (var oi in model.OrderItems)
                    {
                        OrderItem orderItem = new OrderItem()
                        {
                            Amount = oi.Amount,
                            OrderId = order.Id,
                            ProductId = oi.ProductId
                        };

                        db.OrderItems.Add(orderItem);
                    }

                    db.SaveChanges();

                    dbContextTransaction.Commit();

                    success = true;

                    addOrderViewModel = new AddOrderViewModel() {OrderId = order.Id};
                }
                catch (Exception ex)
                {
                    dbContextTransaction.Rollback();
                    message = ex.Message;
                }
            }

            if (!success)
            {
                return BadRequest(message);
            }

            return Ok(addOrderViewModel);
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
