using Domain.Abstract;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace Domain.Entities
{
    public class RequestLeaveComment : Auditable, IEntityBase
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { set; get; }

        [Required]
        [MaxLength(500)]
        public string Comment { set; get; }

        [Index("IX_RequestLeaveComment_RequestLeaveId", IsClustered = false, IsUnique = true)]
        public int RequestLeaveId { get; set; }

        [ForeignKey("RequestLeaveId")]
        public virtual RequestLeave RequestLeave { get; set; }
    }
}