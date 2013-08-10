namespace SAAssetManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class nullify_asset_fks : DbMigration
    {
        public override void Up()
        {
            AlterColumn("dbo.Assets", "WorkStationTypeID", c => c.Int());
            AlterColumn("dbo.Assets", "OSSystemID", c => c.Int());
            AlterColumn("dbo.Assets", "DivisionID", c => c.Int());
            AlterColumn("dbo.Assets", "BranchID", c => c.Int());
            AlterColumn("dbo.Assets", "AssetTypeID", c => c.Int());
            AlterColumn("dbo.Assets", "BuildingID", c => c.Int());
            AlterColumn("dbo.Assets", "AssetStatusID", c => c.Int());
            AlterColumn("dbo.Assets", "EmployeeID", c => c.Int());
        }
        
        public override void Down()
        {
        }
    }
}
