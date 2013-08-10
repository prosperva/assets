using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class Branch
    {
        public int BranchID { get; set; }
        [DisplayName("Branch")]
        public string BranchName { get; set; }

        [DisplayName("Division")]
        public int? DivisionID { get; set; }
        public virtual Division Division { get; set; }

        public virtual ICollection<Employee> BranchEmployees { get; set; }
        public virtual ICollection<Asset> Assets { get; set; }

    }

}