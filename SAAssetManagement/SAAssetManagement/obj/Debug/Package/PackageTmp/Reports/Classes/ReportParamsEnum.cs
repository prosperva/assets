using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;

namespace SAAssetManagement.Reports
{
    public class ReportParamsEnum : IEnumerator
    {
        public ReportParam[] _reportParams;

        // Enumerators are positioned before the first element 
        // until the first MoveNext() call. 
        int position = -1;

        public ReportParamsEnum(ReportParam[] list)
        {
            _reportParams = list;
        }

        public bool MoveNext()
        {
            position++;
            return (position < _reportParams.Length);
        }

        public void Reset()
        {
            position = -1;
        }

        object IEnumerator.Current
        {
            get
            {
                return Current;
            }
        }

        public ReportParam Current
        {
            get
            {
                try
                {
                    return _reportParams[position];
                }
                catch (IndexOutOfRangeException)
                {
                    throw new InvalidOperationException();
                }
            }
        }
    }
}