using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel;

namespace SAAssetManagement.Models
{
    public class OSSystem
    {
        public int OSSystemID { get; set; }
        [DisplayName("OS System Name")]
        public string OSSystemname { get; set; }

        public virtual ICollection<Asset> Assets { get; set; }
        public virtual ICollection<Software> OSSoftwares { get; set; }
    }
}
