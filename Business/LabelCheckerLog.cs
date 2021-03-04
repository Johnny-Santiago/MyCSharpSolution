using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Interface;
using System.Data;
using DataAccess;

namespace Business
{
    public class LabelCheckerLog : ILabelCheckerLog
    {
        private Nullable<DateTime> _ScanStartDate;
        private Nullable<DateTime> _ScanToDate;
        private String _CustomerCode;
        private String _CustomerItem;
        private String _ItemCode;
        private String _ShipTo;
        private String _Shift;
        private String _Status;

        public LabelCheckerLog()
        {
        }

        public DataTable GetLogs()
        {
            return LabelCheckerLogDac.Retreive(this).Tables[0];
        }

        public Nullable<DateTime> ScanStartDate
        {
            get
            {
                return _ScanStartDate;
            }
            set
            {
                _ScanStartDate = value;
            }
        }

        public Nullable<DateTime> ScanToDate
        {
            get
            {
                return _ScanToDate;
            }
            set
            {
                _ScanToDate = value;
            }
        }

        public string CustomerCode
        {
            get
            {
                return _CustomerCode;
            }
            set
            {
                _CustomerCode = value;
            }
        }

        public string ShipTo
        {
            get
            {
                return _ShipTo;
            }
            set
            {
                _ShipTo = value;
            }
        }

        public string CustomerItem
        {
            get
            {
                return _CustomerItem;
            }
            set
            {
                _CustomerItem = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                _ItemCode = value;
            }
        }

        public string Shift
        {
            get
            {
                return _Shift;
            }
            set
            {
                _Shift = value;
            }
        }

        public string Status
        {
            get
            {
                return _Status;
            }
            set
            {
                _Status = value;
            }
        }
    }
}