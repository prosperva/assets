namespace SAAssetManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_division_employee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees","DivisionID", c => c.Int());
            AlterColumn("dbo.Employees","BranchID", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}
