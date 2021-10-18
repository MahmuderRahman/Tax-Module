namespace Test_Project.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class Addtaxslabtype : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.TaxSlabTypes",
                c => new
                    {
                        Id = c.Int(nullable: false, identity: true),
                        Name = c.String(nullable: false, maxLength: 500),
                        Remarks = c.String(maxLength: 1500),
                        CmnCompanyId = c.Int(nullable: false),
                        CreatedBy = c.Int(),
                        CreatedDate = c.DateTime(),
                        ModifiedBy = c.Int(),
                        ModifiedDate = c.DateTime(),
                    })
                .PrimaryKey(t => t.Id)
                .Index(t => t.Name, unique: true, name: "UK_Name");
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.TaxSlabTypes", "UK_Name");
            DropTable("dbo.TaxSlabTypes");
        }
    }
}
