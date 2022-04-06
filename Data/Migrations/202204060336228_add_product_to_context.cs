namespace Data.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class add_product_to_context : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.Product",
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
            
        }
        
        public override void Down()
        {
            DropTable("dbo.Product");
        }
    }
}
