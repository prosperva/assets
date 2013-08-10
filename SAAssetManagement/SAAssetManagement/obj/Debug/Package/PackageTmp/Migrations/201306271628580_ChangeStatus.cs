namespace SAAssetManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class ChangeStatus : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assets", "AssetStatusID", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}
