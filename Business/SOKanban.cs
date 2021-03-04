///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SOKanban.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-FEB-2015
///   Description: This file is the business rules and logic implementation of SOKanban.
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
    public class SOKanban : ISOKanban
    {
        private Nullable<Int32> _Id;
        private Nullable<Int32> _RefID;
        private Nullable<DateTime> _OrderDate;
        private Nullable<DateTime> _OrderDate2;
        private Nullable<DateTime> _DeliveryDate;
        private Nullable<DateTime> _DeliveryDate2; 
        private String _OrderedBy;
        private String _DeliverTo;
        private String _YourRef;
        private String _Description;
        private String _DeliveryOrderNo;
        private String _ManifestNo;
        private String _RIT;
        private String _ItemCode;
        private String _CustomerPartNo;
        private String _ItemDescription;
        private String _OrderNo; 
        private Nullable<Int32> _Status;
        private String _Remarks;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private String _DealingType;
        private String _Warehouse;
        private DataTable _Info;
        private DataTable _List;

        public SOKanban()
        {
        }

        public SOKanban(Int32 Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Id not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _RefID = row["RefID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["RefID"]);
            _OrderDate = row["OrderDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["OrderDate"]);
            _OrderedBy = row["OrderedBy"] == DBNull.Value ? string.Empty : Convert.ToString(row["OrderedBy"]);
            _DeliverTo = row["DeliverTo"] == DBNull.Value ? string.Empty : Convert.ToString(row["DeliverTo"]);
            _YourRef = row["YourRef"] == DBNull.Value ? string.Empty : Convert.ToString(row["YourRef"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _DeliveryOrderNo = row["DeliveryOrderNo"] == DBNull.Value ? string.Empty : Convert.ToString(row["DeliveryOrderNo"]);
            _ManifestNo = row["ManifestNo"] == DBNull.Value ? string.Empty : Convert.ToString(row["ManifestNo"]);
            _RIT = row["RIT"] == DBNull.Value ? string.Empty : Convert.ToString(row["RIT"]);
            _OrderNo = row["SalesOrder"] == DBNull.Value ? string.Empty : Convert.ToString(row["SalesOrder"]);
            _Status = row["Status"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Status"]);
            _Remarks = row["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(row["Remarks"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _Warehouse = row["Warehouse"] == DBNull.Value ? string.Empty : Convert.ToString(row["Warehouse"]);

            _List = SOKanbanDetail.Retrieve(_YourRef);
        }


        public Nullable<Int32> Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        public Nullable<Int32> RefID 
        {
            get { return _RefID; }
            set { _RefID = value; } 
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

        public Nullable<DateTime> DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        public Nullable<DateTime> DeliveryDate2
        {
            get { return _DeliveryDate2; }
            set { _DeliveryDate2 = value; }
        }

        public String OrderedBy 
        {
            get { return _OrderedBy; }
            set { _OrderedBy = value; } 
        }

        public String DeliverTo 
        {
            get { return _DeliverTo; }
            set { _DeliverTo = value; } 
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

        public String RIT 
        {
            get { return _RIT; }
            set { _RIT = value; } 
        }

        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        public String CustomerPartNo
        {
            get { return _CustomerPartNo; }
            set { _CustomerPartNo = value; }
        }

        public String ItemDescription
        {
            get { return _ItemDescription; }
            set { _ItemDescription = value; }
        }

        public String OrderNo 
        {
            get { return _OrderNo; }
            set { _OrderNo = value; } 
        }

        public Nullable<Int32> Status 
        { 
            get { return _Status; } 
            set { _Status = value; } 
        }

        public String Remarks 
        { 
            get { return _Remarks; } 
            set { _Remarks = value; } 
        }

        public String SysCreator 
        { 
            get { return _SysCreator; } 
            set { _SysCreator = value; } 
        }

        public Nullable<DateTime> SysCreated 
        { 
            get { return _SysCreated; } 
            set { _SysCreated = value; } 
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

        public DataTable Info 
        { 
            get { return _Info; } 
            set { _Info = value; } 
        }

        public DataTable List
        {
            get { return _List; }
            set { _List = value; }
        }

        public DataTable Retrieve(Int32 Id)
        {
            return SOKanbanDac.Retrieve(Id).Tables[0];
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

            return SOKanbanDac.Retrieve(this, searchOption).Tables[0]; 
        }

        public static void RetryImport(Int32 ID) 
        {
            SOKanbanDac.RetryImport(ID); 
        }
    }
}