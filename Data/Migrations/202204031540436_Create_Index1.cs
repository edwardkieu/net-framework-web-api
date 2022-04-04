namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Index1 : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LeaveAllocations", "IX_LeaveAllocation_LeaveTypeId");
            DropIndex("dbo.LeaveAllocations", "IX_LeaveAllocation_EmployeeId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_LeaveTypeId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_RequestedId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_ApprovedId");
            CreateIndex("dbo.LeaveAllocations", "LeaveTypeId", name: "IX_LeaveAllocation_LeaveTypeId");
            CreateIndex("dbo.LeaveAllocations", "EmployeeId", name: "IX_LeaveAllocation_EmployeeId");
            CreateIndex("dbo.RequestLeaves", "LeaveTypeId", name: "IX_RequestLeave_LeaveTypeId");
            CreateIndex("dbo.RequestLeaves", "RequestedId", name: "IX_RequestLeave_RequestedId");
            CreateIndex("dbo.RequestLeaves", "ApprovedId", name: "IX_RequestLeave_ApprovedId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_ApprovedId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_RequestedId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_LeaveTypeId");
            DropIndex("dbo.LeaveAllocations", "IX_LeaveAllocation_EmployeeId");
            DropIndex("dbo.LeaveAllocations", "IX_LeaveAllocation_LeaveTypeId");
            CreateIndex("dbo.RequestLeaves", "ApprovedId", unique: true, name: "IX_RequestLeave_ApprovedId");
            CreateIndex("dbo.RequestLeaves", "RequestedId", unique: true, name: "IX_RequestLeave_RequestedId");
            CreateIndex("dbo.RequestLeaves", "LeaveTypeId", unique: true, name: "IX_RequestLeave_LeaveTypeId");
            CreateIndex("dbo.LeaveAllocations", "EmployeeId", unique: true, name: "IX_LeaveAllocation_EmployeeId");
            CreateIndex("dbo.LeaveAllocations", "LeaveTypeId", unique: true, name: "IX_LeaveAllocation_LeaveTypeId");
        }
    }
}
