using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Interface;
using DataAccess;

namespace Business
{
    public class InvoiceTaxList : IInvoiceTax
    {
        private Nullable<Int32> _Id;
        private String _CustomerCode;
        private String _CustomerName;
        private String _InvoiceNo;
        private Nullable<DateTime> _InvoiceDate;
        private Nullable<DateTime> _FromInvoiceDate;
        private Nullable<DateTime> _ToInvoiceDate;
        private String _OrderNumber;
        private String _CustomerPO;
        private Nullable<DateTime> _DateSent;
        private Nullable<DateTime> _DateSent2;
        private Nullable<DateTime> _SysCreated;
        private DataTable _Info;

        public InvoiceTaxList()
        {
        }

        public DataTable Retrieve()
        {
            return InvoiceTaxDac.RetrieveList(this).Tables[0];
        }

        public DataTable RetrieveHeader()
        {
            return InvoiceTaxDac.RetrieveAll(this).Tables[0];
        }

        public DataTable RetrieveDetail()
        {
            return InvoiceTaxDac.RetrieveDetail(this).Tables[0];
        }

        public DataTable RetrieveAllocated()
        {
            return InvoiceTaxDac.RetrieveAllocated(this).Tables[0];
        }


        public Nullable<Int32> Id
        {
            get
            {
                return _Id;
            }
            set
            {
                _Id = value;
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

        public Nullable<DateTime> InvoiceDate
        {
            get
            {
                return _InvoiceDate;
            }
            set
            {
                _InvoiceDate = value;
            }
        }

        public Nullable<DateTime> FromInvoiceDate
        {
            get
            {
                return _FromInvoiceDate;
            }
            set
            {
                _FromInvoiceDate = value;
            }
        }

        public Nullable<DateTime> ToInvoiceDate
        {
            get
            {
                return _ToInvoiceDate;
            }
            set
            {
                _ToInvoiceDate = value;
            }
        }

        public String CustomerName
        {
            get
            {
                return _CustomerName;
            }
            set
            {
                _CustomerName = value;
            }
        }

        public String CustomerCode
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

        public String OrderNumber
        {
            get
            {
                return _OrderNumber;
            }
            set
            {
                _OrderNumber = value;
            }
        }

        public String CustomerPO
        {
            get
            {
                return _CustomerPO;
            }
            set
            {
                _CustomerPO = value;
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

        public Nullable<DateTime> DateSent2
        {
            get
            {
                return _DateSent2;
            }
            set
            {
                _DateSent2 = value;
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


        public DataTable Info
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }


        public string InvoiceNumber
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

        private String _SerialNumber;
        public string SerialNumber
        {
            get
            {
                return _SerialNumber;
            }
            set
            {
                _SerialNumber = value;
            }
        }

        private String _SysCreator;
        public string SysCreator
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
    }
}