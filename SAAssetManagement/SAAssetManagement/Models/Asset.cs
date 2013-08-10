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
        public int? AssetStatusID { set; get; }
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
        [DisplayName("Building")]
        public int? BuildingID { set; get; }
    }
}