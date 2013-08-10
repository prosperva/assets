using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace SAAssetManagement.Reports
{
    public class ReportParam
    {
        public string ParamName { get; set; }
        public object ParamValue { get; set; }
        public ReportParam(string paramName, object paramValue)
        {
            this.ParamName = paramName;
            this.ParamValue = paramValue;
        }
    }
}