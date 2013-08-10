namespace SAAssetManagement.Migrations
{
    using System;
    using System.Data.Entity.Migrations;
    
    public partial class remove_employee_from_division : DbMigration
    {
        public override void Up()
        {
            DropForeignKey("dbo.Divisions", "EmployeeID", "dbo.Employees");
            DropIndex("dbo.Divisions", new[] { "EmployeeID" });
            DropColumn("dbo.Divisions", "EmployeeID");
        }
        
        public override void Down()
        {
            AddColumn("dbo.Divisions", "EmployeeID", c => c.Int());
            CreateIndex("dbo.Divisions", "EmployeeID");
            AddForeignKey("dbo.Divisions", "EmployeeID", "dbo.Employees", "EmployeeID");
        }
    }
}
