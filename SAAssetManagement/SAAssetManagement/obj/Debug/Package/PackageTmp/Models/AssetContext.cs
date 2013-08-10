using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.Data.Objects;
using System.Data.Entity.Infrastructure;

namespace SAAssetManagement.Models
{
    public class AssetContext:DbContext
    {
        public DbSet<AssetType> AssetTypes { get; set; }
        public DbSet<AssetStatus> AssetStatuses { get; set; }
        public DbSet<Asset> Assets { get; set; }
        public DbSet<Employee> Employees { get; set; }
        public DbSet<Division> Divisions { get; set; }
        public DbSet<Branch> Branches { get; set; }
        public DbSet<Software> Softwares { get; set; }
        public DbSet<WorkStationType> WorkStationTypes { get; set; }
        public DbSet<Building> Buildings { get; set; }
        public DbSet<OSSystem> OSSystems { get; set; }
        public DbSet<SoftwarePerAsset> SoftwarePerAssets { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Division>()
                .HasMany(l => l.DivisionEmployees)
                .WithRequired(e => e.Division)
                .HasForeignKey(e => e.DivisionID);

            //modelBuilder.Entity<Division>()
            //    .HasOptional(l => l.Employee)
            //    .WithMany()
            //    .HasForeignKey(l => l.EmployeeID);
            
            modelBuilder.Entity<Division>()
                .HasMany(l => l.Branches)
                .WithRequired(e => e.Division)
                .HasForeignKey(l => l.DivisionID);

            modelBuilder.Entity<Division>()
                .HasMany(l => l.Assets)
                .WithRequired(e => e.Division)
                .HasForeignKey(l => l.DivisionID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.BranchEmployees)
                .WithRequired(d => d.Branch)
                .HasForeignKey(b => b.BranchID);

            modelBuilder.Entity<Branch>()
                .HasMany(e => e.Assets)
                .WithRequired(d => d.Branch)
                .HasForeignKey(b => b.BranchID);

            modelBuilder.Entity<Building>()
                .HasMany(e => e.BuildingEmployees)
                .WithRequired(d => d.Building)
                .HasForeignKey(b => b.BuildingID);

            modelBuilder.Entity<Building>()
                .HasMany(e => e.BuildingAssets)
                .WithRequired(d => d.Building)
                .HasForeignKey(b => b.BuildingID);

            modelBuilder.Entity<Employee>()
                .HasOptional(l => l.EmployeeManager)
                .WithMany()
                .HasForeignKey(l => l.Manager);

            modelBuilder.Entity<Employee>()
                .HasMany(l => l.Assets)
                .WithRequired(e => e.Employee)
                .HasForeignKey(l => l.EmployeeID);

            modelBuilder.Entity<AssetStatus>()
                .HasMany(l => l.Assets)
                .WithRequired(e => e.AssetStatus)
                .HasForeignKey(l => l.AssetStatusID);

            modelBuilder.Entity<AssetType>()
                .HasMany(l => l.Assets)
                .WithRequired(e => e.AssetType)
                .HasForeignKey(l => l.AssetTypeID);

            modelBuilder.Entity<OSSystem>()
                .HasMany(l => l.Assets)
                .WithRequired(e => e.OSSystem)
                .HasForeignKey(l => l.OSSystemID);

            modelBuilder.Entity<OSSystem>()
               .HasMany(l => l.OSSoftwares)
               .WithRequired(e => e.OSSystem)
               .HasForeignKey(l => l.OSSystemID);

            modelBuilder.Entity<WorkStationType>()
               .HasMany(l => l.Assets)
               .WithRequired(e => e.WorkStationType)
               .HasForeignKey(l => l.WorkStationTypeID);

            modelBuilder.Entity<Asset>()
                .HasMany(x => x.Softwares)
                .WithMany(x => x.Assets)
            .Map(x =>
            {
                x.ToTable("AssetSoftwares");
                x.MapLeftKey("assetid");
                x.MapRightKey("softwareid");
            });

            //modelBuilder.Entity<SoftwarePerAsset>()
            //   .HasMany(l => l.Assets)
            //   .WithRequired(e => e.SoftwarePerAssets)
            //   .HasForeignKey(l => l.);

            }

   
    }
}