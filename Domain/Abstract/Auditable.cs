using System;
using System.ComponentModel.DataAnnotations;

namespace Domain.Abstract
{
    public abstract class Auditable : IAuditable
    {
        public DateTime? CreatedDate { set; get; }

        [MaxLength(100)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(100)]
        public string UpdatedBy { set; get; }

        public bool IsActive { set; get; }

        public Auditable()
        {
            IsActive = true;
        }
    }
}