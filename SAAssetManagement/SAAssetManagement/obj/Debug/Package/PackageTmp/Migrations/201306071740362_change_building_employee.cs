namespace SAAssetManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class change_building_employee : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Employees", "BuildingID", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}
