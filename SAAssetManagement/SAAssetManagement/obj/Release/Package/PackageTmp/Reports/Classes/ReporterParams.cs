using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SAAssetManagement.Reports
{
    public class ReportParams : IEnumerable
    {
        private ReportParam[] _reportParam;
        public ReportParams(ReportParam[] pArray)
        {
            _reportParam = new ReportParam[pArray.Length];

            for (int i = 0; i < pArray.Length; i++)
            {
                _reportParam[i] = pArray[i];
            }
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return (IEnumerator)GetEnumerator();
        }

        public ReportParamsEnum GetEnumerator()
        {
            return new ReportParamsEnum(_reportParam);
        }
    }
}