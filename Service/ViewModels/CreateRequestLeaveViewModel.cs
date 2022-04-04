using System;
using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels
{
    public class CreateRequestLeaveViewModel
    {
        public int LeaveTypeId { get; set; }

        [Required]
        public string RequestedId { get; set; }

        [Required]
        public string ApprovedId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public string Comment { get; set; }
    }
}