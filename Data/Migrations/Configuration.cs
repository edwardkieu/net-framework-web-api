namespace Data.Migrations
{
    using Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
    using System.Collections.Generic;
    using System.Data.Entity.Migrations;
    using System.Linq;

    internal sealed class Configuration : DbMigrationsConfiguration<Data.AppDbContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = false;
        }

        protected override void Seed(AppDbContext context)
        {
            CreateUser(context);
            CreateLeaveType(context);
            CreateDepartment(context);
        }

        private void CreateLeaveType(AppDbContext dbContext)
        {
            if (!dbContext.LeaveTypes.Any())
            {
                var leaveTypes = new List<LeaveType> 
                {
                    new LeaveType
                    {
                        Name = "Annually",
                        DefaultDays = 12,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "894dec4a-9b85-4976-8f74-efe21b01b5d6"
                    },
                    new LeaveType
                    {
                        Name = "Public holiday",
                        DefaultDays = 9,
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "894dec4a-9b85-4976-8f74-efe21b01b5d6"
                    }
                };
                dbContext.LeaveTypes.AddRange(leaveTypes);
                dbContext.SaveChanges();
            }
        }

        private void CreateDepartment(AppDbContext dbContext)
        {
            if (!dbContext.Departments.Any())
            {
                var manager = new UserManager<AppUser>(new UserStore<AppUser>(dbContext));
                var admin = manager.FindByEmail("kieuminhhien@gmail.com");
                var departments = new List<Department>
                {
                    new Department
                    {
                        Name = "IT",
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "894dec4a-9b85-4976-8f74-efe21b01b5d6",
                        Employees = new List<AppUser> { admin }
                    },
                    new Department
                    {
                        Name = "Hr",
                        CreatedDate = DateTime.UtcNow,
                        CreatedBy = "894dec4a-9b85-4976-8f74-efe21b01b5d6",
                        Employees = new List<AppUser> { admin }
                    }
                };
                dbContext.Departments.AddRange(departments);
                dbContext.SaveChanges();
            }
        }

        private void CreateUser(AppDbContext dbContext)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(dbContext));
            if (!manager.Users.Any())
            {
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(dbContext));

                var user = new AppUser()
                {
                    Id = "894dec4a-9b85-4976-8f74-efe21b01b5d6",
                    UserName = "admin",
                    Email = "kieuminhhien@gmail.com",
                    EmailConfirmed = true,
                    BirthDay = DateTime.UtcNow,
                    FullName = "Edward Kieu",
                    IsActive = true
                };
                if (manager.Users.Count(x => x.UserName == "admin") == 0)
                {
                    manager.Create(user, "Abcd12334@");

                    if (!roleManager.Roles.Any())
                    {
                        roleManager.Create(new AppRole { Name = "Admin", Description = "Admin" });
                        roleManager.Create(new AppRole { Name = "Member", Description = "Member" });
                    }

                    var adminUser = manager.FindByName("admin");

                    manager.AddToRoles(adminUser.Id, new string[] { "Admin", "Member" });
                }
            }
        }
    }
}