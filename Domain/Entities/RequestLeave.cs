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

        [Index("IX_RequestLeave_LeaveTypeId", IsClustered = false, IsUnique = false)]
        public int LeaveTypeId { get; set; }

        [ForeignKey("LeaveTypeId")]
        public virtual LeaveType LeaveType { get; set; }

        [Index("IX_RequestLeave_RequestedId", IsClustered = false, IsUnique = false)]
        public string RequestedId { get; set; }

        [ForeignKey("RequestedId")]
        public virtual AppUser Requested { get; set; }

        [Index("IX_RequestLeave_ApprovedId", IsClustered = false, IsUnique = false)]
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