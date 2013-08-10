namespace SAAssetManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class uploaddb : DbMigration
    {
        public override void Up()
        {
            CreateTable(
                "dbo.AssetTypes",
                c => new
                    {
                        AssetTypeID = c.Int(nullable: false, identity: true),
                        AssetTypeName = c.String(nullable: false),
                        WarantyTerm = c.Int(),
                        CreatedOn = c.DateTime(),
                        LastUpdatedOn = c.DateTime(),
                    })
                .PrimaryKey(t => t.AssetTypeID);
            
            CreateTable(
                "dbo.Assets",
                c => new
                    {
                        AssetID = c.Int(nullable: false, identity: true),
                        AssetTag = c.String(),
                        WorkStationTypeID = c.Int(nullable: false),
                        BarCode = c.String(),
                        EmployeeID = c.Int(nullable: false),
                        ComputerLocation = c.String(),
                        ReceivedDate = c.DateTime(),
                        OSSystemID = c.Int(nullable: false),
                        Manufacturer = c.String(),
                        MakeModel = c.String(),
                        SerialNumber = c.String(),
                        AssetStatusID = c.Int(nullable: false),
                        PONumber = c.String(),
                        DeployedDate = c.DateTime(),
                        PartNumber = c.String(),
                        CreatedOn = c.DateTime(),
                        LastUpdatedOn = c.DateTime(),
                        Floor = c.String(),
                        Address = c.String(),
                        City = c.String(),
                        Notes = c.String(),
                        DivisionID = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                        AssetTypeID = c.Int(nullable: false),
                        BuildingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.AssetID)
                .ForeignKey("dbo.OSSystems", t => t.OSSystemID)
                .ForeignKey("dbo.Branches", t => t.BranchID)
                .ForeignKey("dbo.Buildings", t => t.BuildingID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .ForeignKey("dbo.Divisions", t => t.DivisionID)
                .ForeignKey("dbo.WorkStationTypes", t => t.WorkStationTypeID)
                .ForeignKey("dbo.AssetStatus", t => t.AssetStatusID)
                .ForeignKey("dbo.AssetTypes", t => t.AssetTypeID)
                .Index(t => t.OSSystemID)
                .Index(t => t.BranchID)
                .Index(t => t.BuildingID)
                .Index(t => t.EmployeeID)
                .Index(t => t.DivisionID)
                .Index(t => t.WorkStationTypeID)
                .Index(t => t.AssetStatusID)
                .Index(t => t.AssetTypeID);
            
            CreateTable(
                "dbo.OSSystems",
                c => new
                    {
                        OSSystemID = c.Int(nullable: false, identity: true),
                        OSSystemname = c.String(),
                    })
                .PrimaryKey(t => t.OSSystemID);
            
            CreateTable(
                "dbo.Softwares",
                c => new
                    {
                        SoftwareID = c.Int(nullable: false, identity: true),
                        softwareName = c.String(nullable: false),
                        InstallNumber = c.Int(),
                        LicenceNumber = c.Int(),
                        OSSystemID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.SoftwareID)
                .ForeignKey("dbo.OSSystems", t => t.OSSystemID)
                .Index(t => t.OSSystemID);
            
            CreateTable(
                "dbo.Divisions",
                c => new
                    {
                        DivisionID = c.Int(nullable: false, identity: true),
                        DivisionName = c.String(nullable: false),
                        EmployeeID = c.Int(),
                        CreatedOn = c.DateTime(nullable: false),
                        LastUpdatedOn = c.DateTime(nullable: false),
                    })
                .PrimaryKey(t => t.DivisionID)
                .ForeignKey("dbo.Employees", t => t.EmployeeID)
                .Index(t => t.EmployeeID);
            
            CreateTable(
                "dbo.Employees",
                c => new
                    {
                        EmployeeID = c.Int(nullable: false, identity: true),
                        UserID = c.String(nullable: false),
                        EmployeeFirstName = c.String(),
                        EmployeeLastName = c.String(),
                        EmployeeTitle = c.String(),
                        DivisionID = c.Int(nullable: false),
                        BranchID = c.Int(nullable: false),
                        Manager = c.Int(),
                        BuildingID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.EmployeeID)
                .ForeignKey("dbo.Branches", t => t.BranchID)
                .ForeignKey("dbo.Employees", t => t.Manager)
                .ForeignKey("dbo.Buildings", t => t.BuildingID)
                .ForeignKey("dbo.Divisions", t => t.DivisionID)
                .Index(t => t.BranchID)
                .Index(t => t.Manager)
                .Index(t => t.BuildingID)
                .Index(t => t.DivisionID);
            
            CreateTable(
                "dbo.Branches",
                c => new
                    {
                        BranchID = c.Int(nullable: false, identity: true),
                        BranchName = c.String(),
                        DivisionID = c.Int(nullable: false),
                    })
                .PrimaryKey(t => t.BranchID)
                .ForeignKey("dbo.Divisions", t => t.DivisionID)
                .Index(t => t.DivisionID);
            
            CreateTable(
                "dbo.Buildings",
                c => new
                    {
                        BuildingID = c.Int(nullable: false, identity: true),
                        BuildingName = c.String(),
                        BuildingAddresss = c.String(),
                        City = c.String(),
                    })
                .PrimaryKey(t => t.BuildingID);
            
            CreateTable(
                "dbo.WorkStationTypes",
                c => new
                    {
                        WorkStationTypeID = c.Int(nullable: false, identity: true),
                        WorkStationTypeName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.WorkStationTypeID);
            
            CreateTable(
                "dbo.AssetStatus",
                c => new
                    {
                        AssetStatusID = c.Int(nullable: false, identity: true),
                        AssetStatusName = c.String(nullable: false),
                    })
                .PrimaryKey(t => t.AssetStatusID);
            
            CreateTable(
                "dbo.SoftwarePerAssets",
                c => new
                    {
                        ID = c.Int(nullable: false, identity: true),
                        SoftwareID = c.Int(nullable: false),
                        AssetID = c.Int(nullable: false),
                        SerialKey = c.String(),
                        install = c.Int(),
                        PONumber = c.String(),
                    })
                .PrimaryKey(t => t.ID)
                .ForeignKey("dbo.Softwares", t => t.SoftwareID)
                .ForeignKey("dbo.Assets", t => t.AssetID)
                .Index(t => t.SoftwareID)
                .Index(t => t.AssetID);
            
            CreateTable(
                "dbo.AssetSoftwares",
                c => new
                    {
                        assetid = c.Int(nullable: false),
                        softwareid = c.Int(nullable: false),
                    })
                .PrimaryKey(t => new { t.assetid, t.softwareid })
                .ForeignKey("dbo.Assets", t => t.assetid)
                .ForeignKey("dbo.Softwares", t => t.softwareid)
                .Index(t => t.assetid)
                .Index(t => t.softwareid);
            
        }
        
        public override void Down()
        {
            DropIndex("dbo.AssetSoftwares", new[] { "softwareid" });
            DropIndex("dbo.AssetSoftwares", new[] { "assetid" });
            DropIndex("dbo.SoftwarePerAssets", new[] { "AssetID" });
            DropIndex("dbo.SoftwarePerAssets", new[] { "SoftwareID" });
            DropIndex("dbo.Branches", new[] { "DivisionID" });
            DropIndex("dbo.Employees", new[] { "DivisionID" });
            DropIndex("dbo.Employees", new[] { "BuildingID" });
            DropIndex("dbo.Employees", new[] { "Manager" });
            DropIndex("dbo.Employees", new[] { "BranchID" });
            DropIndex("dbo.Divisions", new[] { "EmployeeID" });
            DropIndex("dbo.Softwares", new[] { "OSSystemID" });
            DropIndex("dbo.Assets", new[] { "AssetTypeID" });
            DropIndex("dbo.Assets", new[] { "AssetStatusID" });
            DropIndex("dbo.Assets", new[] { "WorkStationTypeID" });
            DropIndex("dbo.Assets", new[] { "DivisionID" });
            DropIndex("dbo.Assets", new[] { "EmployeeID" });
            DropIndex("dbo.Assets", new[] { "BuildingID" });
            DropIndex("dbo.Assets", new[] { "BranchID" });
            DropIndex("dbo.Assets", new[] { "OSSystemID" });
            DropForeignKey("dbo.AssetSoftwares", "softwareid", "dbo.Softwares");
            DropForeignKey("dbo.AssetSoftwares", "assetid", "dbo.Assets");
            DropForeignKey("dbo.SoftwarePerAssets", "AssetID", "dbo.Assets");
            DropForeignKey("dbo.SoftwarePerAssets", "SoftwareID", "dbo.Softwares");
            DropForeignKey("dbo.Branches", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.Employees", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.Employees", "BuildingID", "dbo.Buildings");
            DropForeignKey("dbo.Employees", "Manager", "dbo.Employees");
            DropForeignKey("dbo.Employees", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.Divisions", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Softwares", "OSSystemID", "dbo.OSSystems");
            DropForeignKey("dbo.Assets", "AssetTypeID", "dbo.AssetTypes");
            DropForeignKey("dbo.Assets", "AssetStatusID", "dbo.AssetStatus");
            DropForeignKey("dbo.Assets", "WorkStationTypeID", "dbo.WorkStationTypes");
            DropForeignKey("dbo.Assets", "DivisionID", "dbo.Divisions");
            DropForeignKey("dbo.Assets", "EmployeeID", "dbo.Employees");
            DropForeignKey("dbo.Assets", "BuildingID", "dbo.Buildings");
            DropForeignKey("dbo.Assets", "BranchID", "dbo.Branches");
            DropForeignKey("dbo.Assets", "OSSystemID", "dbo.OSSystems");
            DropTable("dbo.AssetSoftwares");
            DropTable("dbo.SoftwarePerAssets");
            DropTable("dbo.AssetStatus");
            DropTable("dbo.WorkStationTypes");
            DropTable("dbo.Buildings");
            DropTable("dbo.Branches");
            DropTable("dbo.Employees");
            DropTable("dbo.Divisions");
            DropTable("dbo.Softwares");
            DropTable("dbo.OSSystems");
            DropTable("dbo.Assets");
            DropTable("dbo.AssetTypes");
        }
    }
}
