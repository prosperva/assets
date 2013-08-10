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
        public DbSet<Asset> Assets { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder){}
    }
}