using Common.Helpers;
using Domain.Abstract;
using Domain.Entities;
using Microsoft.AspNet.Identity.EntityFramework;
using System;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Data
{
    public class AppDbContext : IdentityDbContext<AppUser>
    {
        public AppDbContext() : base("AppConnection")
        {
            Configuration.LazyLoadingEnabled = false;
        }

        public static AppDbContext Create()
        {
            return new AppDbContext();
        }

        public DbSet<Department> Departments { set; get; }
        public DbSet<LeaveType> LeaveTypes { set; get; }
        public DbSet<LeaveAllocation> LeaveAllocations { set; get; }
        public DbSet<RequestLeave> RequestLeaves { set; get; }
        public DbSet<RequestLeaveComment> RequestLeaveComments { set; get; }

        protected override void OnModelCreating(DbModelBuilder builder)
        {
            builder.Entity<AppUser>().HasKey(r => r.Id).ToTable("AppUsers");
            builder.Entity<AppRole>().HasKey(r => r.Id).ToTable("AppRoles");
            builder.Entity<IdentityUserRole>().HasKey(i => new { i.UserId, i.RoleId }).ToTable("AppUserRoles");
            builder.Entity<IdentityUserLogin>().HasKey(i => i.UserId).ToTable("AppUserLogins");
            builder.Entity<IdentityUserClaim>().HasKey(i => i.UserId).ToTable("AppUserClaims");
            builder.Entity<AppUser>()
                .HasMany(x => x.Departments)
                .WithMany(x => x.Employees)
                .Map(m =>
                {
                    m.MapLeftKey("EmployeeId");
                    m.MapRightKey("DepartmentId");
                    m.ToTable("EmployeeDepartments");
                });
        }

        public override int SaveChanges()
        {
            UpdateAuditableEntity(ChangeTracker);
            return base.SaveChanges();
        }

        public override async Task<int> SaveChangesAsync()
        {
            UpdateAuditableEntity(ChangeTracker);
            return await base.SaveChangesAsync();
        }

        private void UpdateAuditableEntity(DbChangeTracker dbChangeTracker)
        {
            if (Thread.CurrentPrincipal.Identity.IsAuthenticated)
            {
                var identityId = IdentityHelper.GetClaimType("uid");
                var modifiedEntries = dbChangeTracker
                .Entries()
                .Where(x => x.Entity is Auditable && (x.State == EntityState.Added || x.State == EntityState.Modified));

                foreach (var entry in modifiedEntries)
                {
                    var entity = entry.Entity as Auditable;
                    if (entity != null)
                    {
                        var now = DateTime.UtcNow;

                        if (entry.State == EntityState.Added)
                        {
                            entity.CreatedBy = identityId;
                            entity.CreatedDate = now;
                        }
                        else
                        {
                            base.Entry(entity).Property(x => x.CreatedBy).IsModified = false;
                            base.Entry(entity).Property(x => x.CreatedDate).IsModified = false;
                        }

                        entity.UpdatedBy = identityId;
                        entity.UpdatedDate = now;
                    }
                }
            }
        }
    }
}