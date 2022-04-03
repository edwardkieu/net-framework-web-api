using Common.Enums;
using Domain.Abstract;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RequestLeave : Auditable, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        public int LeaveTypeId { get; set; }

        [ForeignKey("LeaveTypeId")]
        public virtual LeaveType LeaveType { get; set; }

        public string RequestedId { get; set; }

        [ForeignKey("RequestedId")]
        public virtual AppUser Requested { get; set; }

        public string ApprovedId { get; set; }

        [ForeignKey("ApprovedId")]
        public virtual AppUser Approved { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public RequestLeaveStatus Status { get; set; }

        public ICollection<RequestLeaveComment> Comments { get; set; }

        public RequestLeave()
        {
            Status = RequestLeaveStatus.Pending;
            Comments = new HashSet<RequestLeaveComment>();
        }
    }
}