///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  DeliveryNoteTransmittal.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 12-JAN-2016
///   Description: This file is the business rules and logic implementation of delivery note transmittals.
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
    public class DeliveryNoteTransmittal : IDeliveryNoteTransmittal
    {
        private Nullable<Int32> _ID;
        private String _Debtor;
        private String _Name;
        private String _DeliveryNoteNo;
        private String _YourRef;
        private String _Description;
        private Nullable<DateTime> _FulfilmentDate;
        private Nullable<DateTime> _FulfilmentDate2;
        private String _DeliveryOrderNo;
        private String _ManifestNo;
        private String _InvoiceToCode;
        private String _DeliveryToCode;
        private String _Warehouse;
        private String _SelectionCode;
        private String _DealingType;
        private String _OrderNo;
        private Nullable<DateTime> _OrderDate;
        private Nullable<DateTime> _OrderDate2;
        private Nullable<DateTime> _ReceivedDate;
        private Nullable<DateTime> _ReceivedDate2;
        private String _Remarks;
        private Nullable<Boolean> _IsDeleted;
        private String _Reason;
        private Nullable<Guid> _SysGuid;
        private Nullable<DateTime> _SysCreated;
        private Nullable<DateTime> _SysCreated2;
        private String _SysCreator;
        private Nullable<DateTime> _SysModified;
        private Nullable<DateTime> _SysModified2;
        private String _SysModifier;
        private DataTable _Info;

        public DeliveryNoteTransmittal()
        {
        }

        public DeliveryNoteTransmittal(Nullable<Int32> ID) 
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
            _Debtor = row["Debtor"] == DBNull.Value ? string.Empty : Convert.ToString(row["Debtor"]);
            _Name = row["Name"] == DBNull.Value ? string.Empty : Convert.ToString(row["Name"]);
            _DeliveryNoteNo = row["DN no."] == DBNull.Value ? string.Empty : Convert.ToString(row["DN no."]);
            _YourRef = row["Your ref."] == DBNull.Value ? string.Empty : Convert.ToString(row["Your ref."]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _FulfilmentDate = row["Fulfilment date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["Fulfilment date"]);
            _DeliveryOrderNo = row["Delivery order no."] == DBNull.Value ? string.Empty : Convert.ToString(row["Delivery order no."]);
            _ManifestNo = row["Manifest no."] == DBNull.Value ? string.Empty : Convert.ToString(row["Manifest no."]);
            _InvoiceToCode = row["Invoice to"] == DBNull.Value ? string.Empty : Convert.ToString(row["Invoice to"]);
            _DeliveryToCode = row["Delivery to"] == DBNull.Value ? string.Empty : Convert.ToString(row["Delivery to"]);
            _Warehouse = row["Warehouse"] == DBNull.Value ? string.Empty : Convert.ToString(row["Warehouse"]);
            _SelectionCode = row["Selection code"] == DBNull.Value ? string.Empty : Convert.ToString(row["Selection code"]);
            _DealingType = row["Dealing type"] == DBNull.Value ? string.Empty : Convert.ToString(row["Dealing type"]);
            _OrderNo = row["Order"] == DBNull.Value ? string.Empty : Convert.ToString(row["Order"]);
            _OrderDate = row["Order date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["Order date"]);
            _ReceivedDate = row["Received date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["Received date"]);
            _Remarks = row["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(row["Remarks"]);
            _IsDeleted = row["IsDeleted"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["IsDeleted"]);
            _Reason = row["Reason"] == DBNull.Value ? string.Empty : Convert.ToString(row["Reason"]);
            _SysGuid = row["SysGuid"] == DBNull.Value ? (Nullable<Guid>)null : (Guid)(row["SysGuid"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysModifier"]);
        }

        public Nullable<Int32> ID 
        { 
            get { return _ID; } 
            set { _ID = value; } 
        }

        public String Debtor 
        { 
            get { return _Debtor; } 
            set { _Debtor = value; } 
        }

        public String Name 
        { 
            get { return _Name; } 
            set { _Name = value; } 
        }

        public String DeliveryNoteNo 
        { 
            get { return _DeliveryNoteNo; } 
            set { _DeliveryNoteNo = value; } 
        }

        public String YourRef 
        { 
            get { return _YourRef; } 
            set { _YourRef = value; } 
        }

        public String Description 
        { 
            get { return _Description; } 
            set { _Description = value; } 
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

        public String InvoiceToCode 
        { 
            get { return _InvoiceToCode; } 
            set { _InvoiceToCode = value; } 
        }

        public String DeliveryToCode
        { 
            get { return _DeliveryToCode; } 
            set { _DeliveryToCode = value; } 
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

        public String DealingType
        { 
            get { return _DealingType; } 
            set { _DealingType = value; } 
        }

        public String OrderNo 
        { 
            get { return _OrderNo; } 
            set { _OrderNo = value; } 
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

        public Nullable<DateTime> ReceivedDate 
        { 
            get { return _ReceivedDate; } 
            set { _ReceivedDate = value; } 
        }

        public Nullable<DateTime> ReceivedDate2 
        { 
            get { return _ReceivedDate2; } 
            set { _ReceivedDate2 = value; } 
        }

        public String Remarks 
        { 
            get { return _Remarks; } 
            set { _Remarks = value; } 
        }

        public Nullable<Boolean> IsDeleted 
        { 
            get { return _IsDeleted; } 
            set { _IsDeleted = value; } 
        }

        public String Reason 
        { 
            get { return _Reason; } 
            set { _Reason = value; } 
        }

        public Nullable<Guid> SysGuid 
        { 
            get { return _SysGuid; } 
            set { _SysGuid = value; } 
        }

        public Nullable<DateTime> SysCreated 
        { 
            get { return _SysCreated; } 
            set { _SysCreated = value; } 
        }

        public Nullable<DateTime> SysCreated2 
        { 
            get { return _SysCreated2; }
            set { _SysCreated2 = value; } 
        }

        public String SysCreator 
        { 
            get { return _SysCreator; } 
            set { _SysCreator = value; }
        }

        public Nullable<DateTime> SysModified
        { 
            get { return _SysModified; } 
            set { _SysModified = value; } 
        }

        public Nullable<DateTime> SysModified2
        { 
            get { return _SysModified2; } 
            set { _SysModified2 = value; }
        }

        public String SysModifier 
        { 
            get { return _SysModifier; }
            set { _SysModifier = value; }
        }

        public DataTable Info 
        { 
            get { return _Info; } 
            set { _Info = value; }
        }

        public DataTable Retrieve(Int32 Id)
        {
            return DeliveryNoteTransmittalDac.Retrieve(Id).Tables[0];
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

            return DeliveryNoteTransmittalDac.Retrieve(this, searchOption).Tables[0];
        }

        public Int32 Insert()
        {
            return DeliveryNoteTransmittalDac.Insert(this);
        }

        public void Update()
        {
            DeliveryNoteTransmittalDac.Update(this);
        }

        public void Delete()
        {
            DeliveryNoteTransmittalDac.Delete(this);
        }
    }
}