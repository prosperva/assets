using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace SAAssetManagement.Models
{
    public class Software
    {
        public int SoftwareID { get; set; }
        //[DisplayName("PO")]
        //public string PONumber { get; set; }
        [Required]
        [DisplayName("Name")]
        public string softwareName { get; set; }
        [DisplayName("Install")]
        public int? InstallNumber { get; set; }
        [DisplayName("License")]
        public int? LicenceNumber { get; set; }
        [DisplayName("OS System")]
        public int OSSystemID { get; set; }
        //[DisplayName("Licenses")]
        //public int NumberOfLicenses { get; set; }
        //[DisplayName("Assisted")]
        //public bool Assisted { get;set; }
        //[DisplayName("Serial")]
        //public string SerialNumber { get; set; }

        public virtual IList<Asset> Assets { get; set; }
        public virtual OSSystem OSSystem { get; set; }
    }
}
