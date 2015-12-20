using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ECommerce.DAL;
using ECommerce.Models.Order;
using Microsoft.AspNet.Identity;

namespace ECommerce.Controllers
{
    [Authorize]
    [RoutePrefix("api/Address")]
    public class AddressController : ApiController
    {
        private readonly ApplicationDbContext db = new ApplicationDbContext();

        // Get: api/Address/GetAddresses
        [Route("GetAddresses")]
        [HttpGet]
        public IList<GetAddressViewModel> GetAddresses()
        {
            var addressList = new List<GetAddressViewModel>();
            var currentUserId = User.Identity.GetUserId();

            if (currentUserId != null)
            {
                var addresses = db.Addresses.Where(a => a.UserId == currentUserId)
                    .Select(a => new GetAddressViewModel()
                    {
                        City = a.City,
                        Country = a.Country,
                        Id = a.Id,
                        Name = a.Name,
                        Phone = a.Phone,
                        Province = a.Province,
                        Street = a.Street,
                        ZipCode = a.ZipCode
                    });

                addressList = addresses.ToList();
            }

            return addressList;
        }

        // POST: api/Address/AddAddress
        [Route("AddAddress")]
        [HttpPost]
        public IHttpActionResult AddAddress(AddAddressBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            bool success = false;
            string message = string.Empty;

            try
            {
                var currentUserId = User.Identity.GetUserId();

                Address address = new Address()
                {
                    City = model.City,
                    Country = model.Country,
                    Name = model.Name,
                    Phone = model.Phone,
                    Province = model.Province,
                    Street = model.Street,
                    UserId = currentUserId,
                    ZipCode = model.ZipCode
                };

                db.Addresses.Add(address);
                db.SaveChanges();

                success = true;
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
