namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Create_Index : DbMigration
    {
        public override void Up()
        {
            DropIndex("dbo.LeaveAllocations", new[] { "LeaveTypeId" });
            DropIndex("dbo.LeaveAllocations", new[] { "EmployeeId" });
            DropIndex("dbo.RequestLeaves", new[] { "LeaveTypeId" });
            DropIndex("dbo.RequestLeaves", new[] { "RequestedId" });
            DropIndex("dbo.RequestLeaves", new[] { "ApprovedId" });
            DropIndex("dbo.RequestLeaveComments", new[] { "RequestLeaveId" });
            CreateIndex("dbo.LeaveAllocations", "LeaveTypeId", unique: true, name: "IX_LeaveAllocation_LeaveTypeId");
            CreateIndex("dbo.LeaveAllocations", "EmployeeId", unique: true, name: "IX_LeaveAllocation_EmployeeId");
            CreateIndex("dbo.RequestLeaves", "LeaveTypeId", unique: true, name: "IX_RequestLeave_LeaveTypeId");
            CreateIndex("dbo.RequestLeaves", "RequestedId", unique: true, name: "IX_RequestLeave_RequestedId");
            CreateIndex("dbo.RequestLeaves", "ApprovedId", unique: true, name: "IX_RequestLeave_ApprovedId");
            CreateIndex("dbo.RequestLeaveComments", "RequestLeaveId", unique: true, name: "IX_RequestLeaveComment_RequestLeaveId");
        }
        
        public override void Down()
        {
            DropIndex("dbo.RequestLeaveComments", "IX_RequestLeaveComment_RequestLeaveId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_ApprovedId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_RequestedId");
            DropIndex("dbo.RequestLeaves", "IX_RequestLeave_LeaveTypeId");
            DropIndex("dbo.LeaveAllocations", "IX_LeaveAllocation_EmployeeId");
            DropIndex("dbo.LeaveAllocations", "IX_LeaveAllocation_LeaveTypeId");
            CreateIndex("dbo.RequestLeaveComments", "RequestLeaveId");
            CreateIndex("dbo.RequestLeaves", "ApprovedId");
            CreateIndex("dbo.RequestLeaves", "RequestedId");
            CreateIndex("dbo.RequestLeaves", "LeaveTypeId");
            CreateIndex("dbo.LeaveAllocations", "EmployeeId");
            CreateIndex("dbo.LeaveAllocations", "LeaveTypeId");
        }
    }
}
