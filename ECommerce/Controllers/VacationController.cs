using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using ECommerce.DAL;
using ECommerce.Models;
using Microsoft.AspNet.Identity;

namespace ECommerce.Controllers
{
    [Authorize]
    [RoutePrefix("api/Vacation")]
    public class VacationController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Vacation/GetUserAppliedVacations
        [Route("GetUserAppliedVacations")]
        public IList<UserAppliedVacationViewModel> GetUserAppliedVacations()
        {
            var currentUserId = User.Identity.GetUserId();
            var userAppliedVacations = db.Vacations.Where(v => v.UserId == currentUserId).Include(v => v.Approver).Select(v => new UserAppliedVacationViewModel()
            {
                Id = v.Id,
                Type = v.Type,
                StartTime = v.StartTime,
                EndTime = v.EndTime,
                Approver = v.Approver,
                Status = v.Status,
            });

            return userAppliedVacations.ToList();
        }

        // GET: api/Vacation/GetUserApproveVacations
        [Route("GetUserApproveVacations")]
        public IList<UserApproveVacationViewModel> GetUserApproveVacations()
        {
            var currentUserId = User.Identity.GetUserId();
            var userApproveVacations =
                db.Vacations.Where(v => v.ApproverId == currentUserId).Include(b => b.User).Select(v => new UserApproveVacationViewModel()
                {
                    Id = v.Id,
                    Type = v.Type,
                    StartTime = v.StartTime,
                    EndTime = v.EndTime,
                    Applier = v.User,
                    Status = v.Status,
                });

            return userApproveVacations.ToList();
        }

        // POST: api/Vacation/AddVacation
        [Route("AddVacation")]
        [HttpPost]
        public IHttpActionResult AddVacation(AddVacationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var currentUserId = User.Identity.GetUserId();

            Vacation vacation = new Vacation()
            {
                Type = model.Type,
                ApproverId = model.ApproverId,
                UserId = currentUserId,
                EndTime = model.EndTime,
                StartTime = model.StartTime,
                Status = 0 // when first created, status is 0, means waiting for approve
            };

            db.Vacations.Add(vacation);
            db.SaveChanges();

            return Ok(vacation);
        }

        // POST: api/Vacation/ApproveVacation
        [Route("ApproveVacation")]
        [HttpPost]
        public IHttpActionResult ApproveVacation(ApproveVacationBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Vacation vacation = db.Vacations.FirstOrDefault(v => v.Id == model.Id);

            var currentUserId = User.Identity.GetUserId();
            if (vacation != null && vacation.Status == 0 && vacation.ApproverId == currentUserId)
            {
                vacation.Status = 1;
                db.SaveChanges();

                return Ok("Update Success");
            }

            return Ok("Update Fail");
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
