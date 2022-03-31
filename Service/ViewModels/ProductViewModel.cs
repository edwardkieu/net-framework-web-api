using System;
using System.ComponentModel.DataAnnotations;

namespace Service.ViewModels
{
    public class ProductViewModel
    {
        public int Id { set; get; }

        [Required]
        [MaxLength(256)]
        public string Name { set; get; }

        public DateTime? CreatedDate { set; get; }

        [MaxLength(100)]
        public string CreatedBy { set; get; }

        public DateTime? UpdatedDate { set; get; }

        [MaxLength(100)]
        public string UpdatedBy { set; get; }

        public bool IsActive { set; get; }
    }
}