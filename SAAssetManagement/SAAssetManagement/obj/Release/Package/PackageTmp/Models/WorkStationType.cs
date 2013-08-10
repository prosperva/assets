using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class WorkStationType
    {
        public int WorkStationTypeID { get; set; }
        [Required]
        [DisplayName("WorkStation Type Name")]
        public string WorkStationTypeName { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
    }
}