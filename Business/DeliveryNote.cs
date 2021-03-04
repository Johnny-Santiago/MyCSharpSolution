///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  DeliveryNote.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-JAN-2016
///   Description: This file is the business rules and logic implementation of Delivery Notes.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Interface;
using DataAccess;
using Extensions;

namespace Business
{
    public class DeliveryNote : IDeliveryNote
    {
        private Nullable<Int32> _ID;
        private String _CustomerCode;
        private String _CustomerName;
        private String _OrderNo;
        private String _YourRef;
        private String _DeliveryNoteNo;
        private String _DeliveryOrderNo;
        private String _ManifestNo;
        private Nullable<DateTime> _FulfilmentDate;
        private Nullable<DateTime> _FulfilmentDate2;
        private Nullable<DateTime> _OrderDate;
        private Nullable<DateTime> _OrderDate2;
        private String _Description;
        private String _DeliveryToCode;
        private String _InvoiceToCode;
        private String _DealingType;
        private String _Warehouse;
        private String _SelectionCode; 
        private DataTable _Info;

        public DeliveryNote()
        {
        }

        public DeliveryNote(Nullable<Int32> ID) 
        {
            _ID = ID; 

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("ID not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _CustomerCode = row["Debtor"] == DBNull.Value ? string.Empty : Convert.ToString(row["Debtor"]);
            _CustomerName = row["Name"] == DBNull.Value ? string.Empty : Convert.ToString(row["Name"]);
            _OrderNo = row["Order"] == DBNull.Value ? string.Empty : Convert.ToString(row["Order"]);
            _YourRef = row["Your ref."] == DBNull.Value ? string.Empty : Convert.ToString(row["Your ref."]);
            _DeliveryNoteNo = row["DN no."] == DBNull.Value ? string.Empty : Convert.ToString(row["DN no."]);
            _DeliveryOrderNo = row["Order No."] == DBNull.Value ? string.Empty : Convert.ToString(row["Order No."]);
            _ManifestNo = row["Manifest"] == DBNull.Value ? string.Empty : Convert.ToString(row["Manifest"]);
            _FulfilmentDate = row["Fulfilment date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["Fulfilment date"]);
            _OrderDate = row["Order date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["Order date"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _DealingType = row["Dealing type"] == DBNull.Value ? string.Empty : Convert.ToString(row["Dealing type"]);
            _DeliveryToCode = row["Delivery to"] == DBNull.Value ? string.Empty : Convert.ToString(row["Delivery to"]);
            _InvoiceToCode = row["Invoice to"] == DBNull.Value ? string.Empty : Convert.ToString(row["Invoice to"]);
            _SelectionCode = row["Selection code"] == DBNull.Value ? string.Empty : Convert.ToString(row["Selection code"]);
        }

        public Nullable<Int32> ID 
        {
            get { return _ID; }
            set { _ID = value; } 
        }

        public String CustomerCode 
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        public String CustomerName 
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        public String OrderNo 
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }

        public String YourRef 
        {
            get { return _YourRef; }
            set { _YourRef = value; }
        }

        public String DeliveryNoteNo 
        {
            get { return _DeliveryNoteNo; }
            set { _DeliveryNoteNo = value; }
        }

        public String DeliveryOrderNo 
        {
            get { return _DeliveryOrderNo; }
            set { _DeliveryOrderNo = value; }
        }

        public String ManifestNo 
        {
            get { return _ManifestNo; }
            set { _ManifestNo = value; }
        }

        public Nullable<DateTime> FulfilmentDate 
        {
            get { return _FulfilmentDate; }
            set { _FulfilmentDate = value; }
        }

        public Nullable<DateTime> FulfilmentDate2 
        {
            get { return _FulfilmentDate2; }
            set { _FulfilmentDate2 = value; }
        }

        public Nullable<DateTime> OrderDate 
        {
            get { return _OrderDate; }
            set { _OrderDate = value; }
        }

        public Nullable<DateTime> OrderDate2 
        {
            get { return _OrderDate2; }
            set { _OrderDate2 = value; }
        }

        public String Description 
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public String DeliveryToCode 
        {
            get { return _DeliveryToCode; }
            set { _DeliveryToCode = value; }
        }

        public String InvoiceToCode 
        {
            get { return _InvoiceToCode; }
            set { _InvoiceToCode = value; }
        }

        public String DealingType 
        {
            get { return _DealingType; }
            set { _DealingType = value; }
        }

        public String Warehouse 
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }

        public String SelectionCode
        {
            get { return _SelectionCode; }
            set { _SelectionCode = value; }
        }

        public DataTable Info 
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public DataTable Retrieve(SearchOption SearchOption)
        {
            Int32 searchOption;
            switch (SearchOption)
            {
                case SearchOption.StartsWith: searchOption = 2; break;
                case SearchOption.Contains: searchOption = 3; break;
                case SearchOption.EndsWith: searchOption = 4; break;
                default: searchOption = 1; break;
            }

            return DeliveryNoteDac.Retrieve(this, searchOption).Tables[0];
        }
    }
}