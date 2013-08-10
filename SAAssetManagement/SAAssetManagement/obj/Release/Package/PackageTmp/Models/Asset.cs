using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class Asset
    {
        public int AssetID { get; set; }
        [DisplayName("Asset Tag")]
        public string AssetTag { get; set; }
        [DisplayName("Workstation Type")]
        public int? WorkStationTypeID { get; set; }
        [DisplayName("Bar Code")]
        public string BarCode { get; set; }
        [DisplayName("Being Used By")]
        public int? EmployeeID { set; get; }
        [DisplayName("Computer Location")]
        public string ComputerLocation { get; set; }
        [DisplayName("Received Date")]
        public DateTime? ReceivedDate { get; set; }
        [DisplayName("Operating System")]
        public int? OSSystemID { set; get; }
        public string Manufacturer { get; set; }
        [DisplayName("Make and Model")]
        public string MakeModel { get; set; }
        [DisplayName("Serial")]
        public string SerialNumber { get; set; }
        [DisplayName("Asset Status")]
        public int AssetStatusID { set; get; }
        [DisplayName("PO Number")]
        public string PONumber { get; set; }
        [DisplayName("Deployed Date")]
        public DateTime? DeployedDate { get; set; }
        [DisplayName("Part Number")]
        public string PartNumber { get; set; }
        [DisplayName("Created On")]
        public DateTime? CreatedOn { get; set; }
        [DisplayName("Last Updated On")]
        public DateTime? LastUpdatedOn { get; set; }
        public string Floor { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string Notes { get; set; }
        [DisplayName("Division")]
        public int? DivisionID { set; get; }
        [DisplayName("Branch")]
        public int? BranchID { set; get; }
        [DisplayName("Asset Type")]
        public int? AssetTypeID { set; get; }

        public virtual OSSystem OSSystem { get; set; }
        public virtual Division Division { get; set; }
        public virtual Branch Branch { get; set; }
        public virtual Employee Employee { get; set; }
        public virtual AssetType AssetType { get; set; }
        public virtual WorkStationType WorkStationType { get; set; }
        public virtual AssetStatus AssetStatus { get; set; }

        //[DisplayName("Warranty Start Date")]
        //public DateTime? StartWarrantyDate { get; set;}
        //[DisplayName("Warranty End Date")]
        //public DateTime? EndWarrantyDate { get; set; }
        //[DisplayName("Warranty Term")]
        //public string WarrantyTerm { get; set; }
        //public string Description { get; set; }
        //public string Color { get; set; }
        //public string Size { get; set; }
        [DisplayName("Building")]
        public int? BuildingID { set; get; }
        public virtual Building Building { get; set; }

        public virtual ICollection<Software> Softwares { get; set; }
        public virtual ICollection<SoftwarePerAsset> SoftwarePerAssets { get; set; }

        //These fields can be removed but due to the nature the data looks like,
        //they have been added. Basically, some assets do not have a user using them because they
        //belong to a meeting room

    }
}