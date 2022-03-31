namespace Data.Migrations
{
    using Domain.Entities;
    using Microsoft.AspNet.Identity;
    using Microsoft.AspNet.Identity.EntityFramework;
    using System;
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
        }

        private void CreateUser(AppDbContext context)
        {
            var manager = new UserManager<AppUser>(new UserStore<AppUser>(context));
            if (!manager.Users.Any())
            {
                var roleManager = new RoleManager<AppRole>(new RoleStore<AppRole>(context));

                var user = new AppUser()
                {
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