using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using ECommerce.DAL;
using ECommerce.Models;
using Microsoft.AspNet.Identity;

namespace ECommerce.Controllers
{
    [Authorize]
    [RoutePrefix("api/Attendance")]
    public class AttendanceController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Attendance/GetAttendances
        [Route("GetAttendances")]
        public IList<MyAttendanceViewModel> GetAttendances()
        {
            var myAttendances = db.Attendances.Select(a => new MyAttendanceViewModel()
            {
                Id = a.Id,
                Time = a.Time,
                Type = a.Type
            });

            return myAttendances.ToList();
        }

        // POST: api/Attendance/AddAttendance
        [Route("AddAttendance")]
        [HttpPost]
        public IHttpActionResult AddAttendance(AddAttendanceBindingModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Attendance attendance = new Attendance()
            {
                Time = model.Time,
                Type = model.Type,
                UserId = User.Identity.GetUserId()
            };

            db.Attendances.Add(attendance);
            db.SaveChanges();

            return Ok(attendance);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool AttendanceExists(int id)
        {
            return db.Attendances.Count(e => e.Id == id) > 0;
        }
    }
}