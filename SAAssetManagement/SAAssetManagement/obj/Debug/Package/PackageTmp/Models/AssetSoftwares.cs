using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAAssetManagement.Models
{
    public class AssetSoftwares
    {
        public int SoftwareID { get; set; }
        public string SoftwareName { get; set; }
        public bool Assigned { get; set; }

        public DateTime DatePushed { get; set; }

        public string LicenseKey { get; set; }
    }
}