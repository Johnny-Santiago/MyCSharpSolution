using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
using Interface;

namespace Business
{
    public class invoice : Iinvoice
    {
        private Nullable<Int32> _Id;
        private String _InvoiceNumber;
        private String _CustomerCode;
        private Nullable<Boolean> _Processed;
        private String _DeliveryNoteNumber;
        private String _SysCreator;

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public String InvoiceNumber
        {
            get { return _InvoiceNumber; }
            set { _InvoiceNumber = value; }
        }

        public String CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        public Nullable<Boolean> Processed
        {
            get { return _Processed; }
            set { _Processed = value; }
        }

        public String DeliveryNoteNumber
        {
            get { return _DeliveryNoteNumber; }
            set { _DeliveryNoteNumber = value; }
        }

        public String SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }

        public static DataTable GetGroup(String DeliveryNoteNumber)
        {
            return InvoiceDac.GetGroup(DeliveryNoteNumber).Tables[0];
        }

        public static DataTable GetGroupByInvoiceNo(String InvoiceNumber)
        {
            return InvoiceDac.GetGroupByInvoiceNo(InvoiceNumber).Tables[0]; 
        }

        public static DataTable GetDeliveryDetails(String DeliveryNoteNumber, Boolean IncludeWithDN)
        {
            return InvoiceDac.GetDeliveryDetails(DeliveryNoteNumber, IncludeWithDN).Tables[0];
        }

        public void UpdateDeliveryNoteNumber()
        {
            InvoiceDac.UpdateDeliveryNoteNumber(this);
        }

        public static DataTable CheckDOReport(String InvoiceNumber, String CustomerFile)
        {
            return InvoiceDac.CheckDOReport(InvoiceNumber, CustomerFile).Tables[0]; 
        }

        public static DataTable GetDOReportDetails(String InvoiceNumber, String Manifest, String CustomerPartNumber)  
        {
            return InvoiceDac.GetDOReportDetails(InvoiceNumber, Manifest, CustomerPartNumber).Tables[0];
        }

        public static DataTable RetrieveDOReport(String InvoiceNumber)
        {
            return InvoiceDac.RetrieveDOReport(InvoiceNumber).Tables[0]; 
        }
    }
}