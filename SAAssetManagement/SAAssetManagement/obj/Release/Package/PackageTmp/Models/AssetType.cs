using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class AssetType
    {
        public int AssetTypeID { get; set; }
        [Required]
        [DisplayName("Asset Type Name")]
        public string AssetTypeName { get; set; }

        public int? WarantyTerm { get; set; }

        [DisplayName("Created On")]
        public DateTime? CreatedOn { set; get; }
        [DisplayName("Last Updated On")]
        public DateTime? LastUpdatedOn { set; get; }

        public virtual ICollection<Asset> Assets { get; set; }
    }
}