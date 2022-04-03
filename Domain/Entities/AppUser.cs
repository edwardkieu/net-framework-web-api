using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Security.Claims;
using System.Threading.Tasks;

namespace Domain.Entities
{
    [Table("AppUsers", Schema = "dbo")]
    public class AppUser : IdentityUser
    {
        [MaxLength(256)]
        public string FullName { set; get; }

        [MaxLength(256)]
        public string Address { set; get; }

        public string Avatar { get; set; }

        public DateTime? BirthDay { set; get; }

        public bool IsActive { get; set; }

        public virtual ICollection<Department> Departments { get; set; }

        public virtual ICollection<LeaveAllocation> LeaveAllocations { get; set; }

        public virtual ICollection<RequestLeave> RequestLeaves { get; set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<AppUser> manager, string authenticationType)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, authenticationType);
            // Add custom user claims here
            return userIdentity;
        }

        public AppUser()
        {
            IsActive = true;
            Departments = new HashSet<Department>();
            LeaveAllocations = new HashSet<LeaveAllocation>();
            RequestLeaves = new HashSet<RequestLeave>();
        }
    }
}