using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
using Interface;

namespace Business
{
    public class InvoiceSchedule : IInvoiceSchedule
    {
        private Nullable<Int32> _ID;
        private String _InvoiceNo;
        private Nullable<DateTime> _DateSent;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private String _SysModifier;
        private Nullable<DateTime> _SysModified;
        private DataTable _Info;

        public InvoiceSchedule()
        {
        }
        
        public Int32 Insert()
        {
            return InvoiceScheduleDac.Insert(this);
        }

        public Int32 Delete()
        {
            return InvoiceScheduleDac.Delete(this);
        }

        public Int32 Update()
        {
            return InvoiceScheduleDac.Update(this);
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public Nullable<Int32> ID
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public String InvoiceNo
        {
            get
            {
                return _InvoiceNo;
            }
            set
            {
                _InvoiceNo = value;
            }
        }

        public Nullable<DateTime> DateSent
        {
            get
            {
                return _DateSent;
            }
            set
            {
                _DateSent = value;
            }
        }

        public String SysCreator
        {
            get
            {
                return _SysCreator;
            }
            set
            {
                _SysCreator = value;
            }
        }


        public Nullable<DateTime> SysCreated
        {
            get
            {
                return _SysCreated;
            }
            set
            {
                _SysCreated = value;
            }
        }

        public String SysModifier
        {
            get
            {
                return _SysModifier;
            }
            set
            {
                _SysModifier = value;

            }
        }

        public Nullable<DateTime> SysModified
        {
            get
            {
                return _SysModified;
            }
            set
            {
                _SysModified = value;
            }
        }
    }
}
