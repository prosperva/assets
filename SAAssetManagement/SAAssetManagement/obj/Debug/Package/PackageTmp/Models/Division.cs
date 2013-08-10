using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAAssetManagement.Models
{
    public class Division
    {
        public int DivisionID { get; set; }
        [Required]
        [DisplayName("Division Name")]
        public string DivisionName { get; set; }

        //[DisplayName("Site Contact")]
     
        //public int? EmployeeID { get; set; }

        //public virtual Employee Employee { set; get;}

        public virtual ICollection<Employee> DivisionEmployees { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Branch> Branches { get; set; }

        [Required]
        [DisplayName("Created On")]
        public DateTime CreatedOn { set; get; }
        [Required]
        [DisplayName("Last Updated On")]
        public DateTime LastUpdatedOn { set; get; }
    }
}