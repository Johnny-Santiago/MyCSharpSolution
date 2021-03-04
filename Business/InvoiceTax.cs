using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Interface;
using DataAccess;

namespace Business
{
    public class InvoiceTax : IInvoiceTax
    {
        private Nullable<Int32> _ID;
        private String _SerialNumber;
        private String _CustomerCode;
        private String _CustomerName;
        private String _InvoiceNumber;
        private Nullable<DateTime> _InvoiceDate;
        private Nullable<DateTime> _FromInvoiceDate;
        private Nullable<DateTime> _ToInvoiceDate;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private DataTable _Info;

        public InvoiceTax()
        {
        }

        public DataTable Retrieve()
        {
            return InvoiceTaxDac.Retrieve(this).Tables[0];
        }

        public DataTable RetrieveUnallocated()
        {
            return InvoiceTaxDac.RetrieveUnallocated().Tables[0];
        }

        public Int32 TaxAllocated()
        {
            return InvoiceTaxDac.Allocate(this);
        }

        public Int32 TaxAllocatedUpdate()
        {
            return InvoiceTaxDac.Update(this);
        }

        public Int32 TaxAllocatedUpdateID()
        {
            return InvoiceTaxDac.UpdateID(this);
        }

        public Nullable<Int32> Id
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

        public String InvoiceNumber
        {
            get
            {
                return _InvoiceNumber;
            }
            set
            {
                _InvoiceNumber = value;
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


        public String SerialNumber
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
    }
}