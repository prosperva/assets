using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations.Schema;

namespace SAAssetManagement.Models
{
    public class Employee
    {
        public int EmployeeID { get; set; }
        [Required]
        [DisplayName("User ID")]
        public string UserID { get; set; }
        [DisplayName("First Name")]
        public string EmployeeFirstName { get; set; }
        [DisplayName("Last Name")]
        public string EmployeeLastName { get; set; }
        [DisplayName("Employee Title")]
        public string EmployeeTitle { get; set; }
        [DisplayName("Division")]
        public int? DivisionID { get; set; }
        public virtual Division Division { get; set; }
        [DisplayName("Branch")]
        public int? BranchID { get; set; }
        public virtual Branch Branch { get; set; }
        public int? Manager { get; set; }
        public virtual Employee EmployeeManager { get; set; }
        public int? BuildingID { get; set; }
        public virtual Building Building { get; set; }

        //public string Address { get; set; }
        //public string City { get; set; }
        //public string Floor { get; set; }
        //[DisplayName("Phone Number")]
        //public string Phone { get; set; }
        //[DisplayName("Last Updated On")]
        //public DateTime LastUpdatedOn { set; get; }
        //[DisplayName("Created On")]
        //public DateTime CreatedOn { set; get; }
        
        public virtual ICollection<Asset> Assets { get; set; }
    }
}