using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class Building
    {
        public int BuildingID { get; set; }
        [DisplayName("Building Name")]
        public string BuildingName { set; get; }
        
        public string BuildingAddresss { set; get; }
        public string City { set; get; }
       
        public virtual ICollection<Employee> BuildingEmployees { set; get; }
        public virtual ICollection<Asset> BuildingAssets { set; get; }
    }
}