using System;
using System.ComponentModel.DataAnnotations;
using ECommerce.Models.Account;

namespace ECommerce.Models
{
    public class Vacation
    {
        public int Id { get; set; }

        public int Type { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Start Date")]
        public DateTime StartTime { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "End Date")]
        public DateTime EndTime { get; set; }

        public int Status { get; set; }

        public string UserId { get; set; }

        public string ApproverId { get; set; }

        public virtual ApplicationUser Approver { get; set; }

        public virtual ApplicationUser User { get; set; }
    }

    public class ApproveVacationBindingModel
    {
        [Required]
        [Display(Name = "Vacation Id")]
        public int Id { get; set; }
    }

    public class AddVacationBindingModel
    {
        [Required]
        [Display(Name = "Type")]
        public int Type { get; set; }

        [Required]
        [Display(Name = "Start Time")]
        public DateTime StartTime { get; set; }

        [Required]
        [Display(Name = "End Time")]
        public DateTime EndTime { get; set; }

        [Required]
        [Display(Name = "ApproverId")]
        public string ApproverId { get; set; }
    }

    public class UserAppliedVacationViewModel
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Status { get; set; }

        public ApplicationUser Approver { get; set; }
    }

    public class UserApproveVacationViewModel
    {
        public int Id { get; set; }

        public int Type { get; set; }

        public DateTime StartTime { get; set; }

        public DateTime EndTime { get; set; }

        public int Status { get; set; }

        public ApplicationUser Applier { get; set; }
    }
}