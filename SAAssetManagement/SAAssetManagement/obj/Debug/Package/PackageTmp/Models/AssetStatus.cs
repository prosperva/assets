using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class AssetStatus
    {
        public int AssetStatusID { get; set; }
        [Required]
        [DisplayName("Asset Status")]
        public string AssetStatusName { get; set; }
    
        public virtual ICollection<Asset> Assets { get; set; }
    }
}