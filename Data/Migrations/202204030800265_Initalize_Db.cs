namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Initalize_Db : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Departments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUsers",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        FullName = c.String(maxLength: 256),
                        Address = c.String(maxLength: 256),
                        Avatar = c.String(),
                        BirthDay = c.DateTime(),
                        IsActive = c.Boolean(nullable: false),
                        Email = c.String(),
                        EmailConfirmed = c.Boolean(nullable: false),
                        PasswordHash = c.String(),
                        SecurityStamp = c.String(),
                        PhoneNumber = c.String(),
                        PhoneNumberConfirmed = c.Boolean(nullable: false),
                        TwoFactorEnabled = c.Boolean(nullable: false),
                        LockoutEndDateUtc = c.DateTime(),
                        LockoutEnabled = c.Boolean(nullable: false),
                        AccessFailedCount = c.Int(nullable: false),
                        UserName = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.AppUserClaims",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        Id = c.Int(nullable: false),
                        ClaimType = c.String(),
                        ClaimValue = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.LeaveAllocations",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeaveTypeId = c.Int(nullable: false),
                        EmployeeId = c.String(maxLength: 128),
                        NumberOfDays = c.Int(nullable: false),
                        Period = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.EmployeeId)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.EmployeeId);
            
            CreateTable(
                "dbo.LeaveTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 256),
                        DefaultDays = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.RequestLeaves",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        LeaveTypeId = c.Int(nullable: false),
                        RequestedId = c.String(maxLength: 128),
                        ApprovedId = c.String(maxLength: 128),
                        StartDate = c.DateTime(nullable: false),
                        EndDate = c.DateTime(nullable: false),
                        Status = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.AppUsers", t => t.ApprovedId)
                .ForeignKey("dbo.LeaveTypes", t => t.LeaveTypeId, cascadeDelete: true)
                .ForeignKey("dbo.AppUsers", t => t.RequestedId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.LeaveTypeId)
                .Index(t => t.RequestedId)
                .Index(t => t.ApprovedId)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.RequestLeaveComments",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Comment = c.String(nullable: false, maxLength: 500),
                        RequestLeaveId = c.Int(nullable: false),
                        CreatedDate = c.DateTime(),
                        CreatedBy = c.String(maxLength: 100),
                        UpdatedDate = c.DateTime(),
                        UpdatedBy = c.String(maxLength: 100),
                        IsActive = c.Boolean(nullable: false),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.RequestLeaves", t => t.RequestLeaveId, cascadeDelete: true)
                .Index(t => t.RequestLeaveId);
            
            CreateTable(
                "dbo.AppUserLogins",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        LoginProvider = c.String(),
                        ProviderKey = c.String(),
                        AppUser_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => t.UserId)
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .Index(t => t.AppUser_Id);
            
            CreateTable(
                "dbo.AppUserRoles",
                c => new
                    {
                        UserId = c.String(nullable: false, maxLength: 128),
                        RoleId = c.String(nullable: false, maxLength: 128),
                        AppUser_Id = c.String(maxLength: 128),
                        IdentityRole_Id = c.String(maxLength: 128),
                    })
                .PrimaryKey(t => new { t.UserId, t.RoleId })
                .ForeignKey("dbo.AppUsers", t => t.AppUser_Id)
                .ForeignKey("dbo.IdentityRoles", t => t.IdentityRole_Id)
                .Index(t => t.AppUser_Id)
                .Index(t => t.IdentityRole_Id);
            
            CreateTable(
                "dbo.IdentityRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Name = c.String(),
                    })
                .PrimaryKey(t => t.Id);
            
            CreateTable(
                "dbo.EmployeeDepartments",
                c => new
                    {
                        EmployeeId = c.String(nullable: false, maxLength: 128),
                        DepartmentId = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.EmployeeId, t.DepartmentId })
                .ForeignKey("dbo.AppUsers", t => t.EmployeeId, cascadeDelete: true)
                .ForeignKey("dbo.Departments", t => t.DepartmentId, cascadeDelete: true)
                .Index(t => t.EmployeeId)
                .Index(t => t.DepartmentId);
            
            CreateTable(
                "dbo.AppRoles",
                c => new
                    {
                        Id = c.String(nullable: false, maxLength: 128),
                        Description = c.String(),
                    })
                .PrimaryKey(t => t.Id)
                .ForeignKey("dbo.IdentityRoles", t => t.Id)
                .Index(t => t.Id);
            
        }
        
        public override void Down()
        {
            DropForeignKey("dbo.AppRoles", "Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.AppUserRoles", "IdentityRole_Id", "dbo.IdentityRoles");
            DropForeignKey("dbo.AppUserRoles", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.RequestLeaves", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserLogins", "AppUser_Id", "dbo.AppUsers");
            DropForeignKey("dbo.RequestLeaves", "RequestedId", "dbo.AppUsers");
            DropForeignKey("dbo.RequestLeaves", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.RequestLeaveComments", "RequestLeaveId", "dbo.RequestLeaves");
            DropForeignKey("dbo.RequestLeaves", "ApprovedId", "dbo.AppUsers");
            DropForeignKey("dbo.LeaveAllocations", "LeaveTypeId", "dbo.LeaveTypes");
            DropForeignKey("dbo.LeaveAllocations", "EmployeeId", "dbo.AppUsers");
            DropForeignKey("dbo.EmployeeDepartments", "DepartmentId", "dbo.Departments");
            DropForeignKey("dbo.EmployeeDepartments", "EmployeeId", "dbo.AppUsers");
            DropForeignKey("dbo.AppUserClaims", "AppUser_Id", "dbo.AppUsers");
            DropIndex("dbo.AppRoles", new[] { "Id" });
            DropIndex("dbo.EmployeeDepartments", new[] { "DepartmentId" });
            DropIndex("dbo.EmployeeDepartments", new[] { "EmployeeId" });
            DropIndex("dbo.AppUserRoles", new[] { "IdentityRole_Id" });
            DropIndex("dbo.AppUserRoles", new[] { "AppUser_Id" });
            DropIndex("dbo.AppUserLogins", new[] { "AppUser_Id" });
            DropIndex("dbo.RequestLeaveComments", new[] { "RequestLeaveId" });
            DropIndex("dbo.RequestLeaves", new[] { "AppUser_Id" });
            DropIndex("dbo.RequestLeaves", new[] { "ApprovedId" });
            DropIndex("dbo.RequestLeaves", new[] { "RequestedId" });
            DropIndex("dbo.RequestLeaves", new[] { "LeaveTypeId" });
            DropIndex("dbo.LeaveAllocations", new[] { "EmployeeId" });
            DropIndex("dbo.LeaveAllocations", new[] { "LeaveTypeId" });
            DropIndex("dbo.AppUserClaims", new[] { "AppUser_Id" });
            DropTable("dbo.AppRoles");
            DropTable("dbo.EmployeeDepartments");
            DropTable("dbo.IdentityRoles");
            DropTable("dbo.AppUserRoles");
            DropTable("dbo.AppUserLogins");
            DropTable("dbo.RequestLeaveComments");
            DropTable("dbo.RequestLeaves");
            DropTable("dbo.LeaveTypes");
            DropTable("dbo.LeaveAllocations");
            DropTable("dbo.AppUserClaims");
            DropTable("dbo.AppUsers");
            DropTable("dbo.Departments");
        }
    }
}
