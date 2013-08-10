using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAAssetManagement.Models
{
    public class SoftwarePerAsset
    {

        public int ID { get; set; }

        public int SoftwareID { get; set; }
        public int AssetID { get; set; }
        public string SerialKey { get; set; }
        public int? install { get; set; }
        public string PONumber { get; set; }
        //public DateTime? DatePushed { get; set; }

        public virtual Software Software { get; set; }
        public virtual Asset Asset { get; set; }
        
    }
}