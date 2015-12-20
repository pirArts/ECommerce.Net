using System;
using System.ComponentModel.DataAnnotations;
using ECommerce.Models.Account;

namespace ECommerce.Models
{
    public class Attendance
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime Time { get; set; }

        public string UserId { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class MyAttendanceViewModel
    {
        public int Id { get; set; }

        public string Type { get; set; }

        public DateTime Time { get; set; }
    }

    public class AddAttendanceBindingModel
    {
        [Required]
        [Display(Name = "Type")]
        public string Type { get; set; }

        [Required]
        [Display(Name = "Time")]
        public DateTime Time { get; set; }
    }
}