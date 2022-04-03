using Domain.Abstract;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class LeaveType : Auditable, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        public int DefaultDays { get; set; }

        public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; }

        public virtual ICollection<RequestLeave> RequestLeaves { get; set; }

        public LeaveType()
        {
            LeaveAllocations = new HashSet<LeaveAllocation>();
            RequestLeaves = new HashSet<RequestLeave>();
        }
    }
}