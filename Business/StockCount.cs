///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  StockCount.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 23-OCT-2015
///   Description: This file is the business rules and logic implementation of StockCount.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface;
using DataAccess;
using Extensions;

namespace Business
{
    public class StockCount : IStockCount
    {
        private Nullable<Int32> _ID;
        private Nullable<Int32> _Year;
        private Nullable<Int32> _Period;
        private String _Warehouse;
        private Nullable<DateTime> _StartDate;
        private Nullable<DateTime> _EndDate;
        private Nullable<Int32> _Status;
        private String _Remarks;
        private String _Reason;
        private Nullable<DateTime> _SysCreated;
        private String _SysCreator;
        private Nullable<DateTime> _SysModified;
        private String _SysModifier;
        private Nullable<Boolean> _IsDeleted;
        private Nullable<Guid> _SysGuid;
        private DataTable _Info;
        private IAuditLog _AuditLog;

        public StockCount()
        {
        }

        public StockCount(String SysCreator)
        {
            _Info = Retrieve(SysCreator);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                AuditLog _auditLog = new AuditLog(_SysGuid);
                _AuditLog = (IAuditLog)_auditLog;
            }
            else
            {
                throw new Exception("An error is encountered during initialization of stock taking.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _Year = row["Year"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Year"]);
            _Period = row["Period"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Period"]);
            _Warehouse = row["Warehouse"] == DBNull.Value ? string.Empty : Convert.ToString(row["Warehouse"]);
            _StartDate = row["StartDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["StartDate"]);
            _EndDate = row["EndDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["EndDate"]);
            _Status = row["Status"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Status"]);
            _Remarks = row["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(row["Remarks"]);
            _Reason = row["Reason"] == DBNull.Value ? string.Empty : Convert.ToString(row["Reason"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysModifier"]);
            _IsDeleted = row["IsDeleted"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["IsDeleted"]);
            _SysGuid = row["SysGuid"] == DBNull.Value ? (Nullable<Guid>)null : (Guid)(row["SysGuid"]);
        }

        public Nullable<Int32> ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Nullable<Int32> Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public Nullable<Int32> Period
        {
            get { return _Period; }
            set { _Period = value; }
        }

        public String Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }

        public Nullable<DateTime> StartDate
        {
            get { return _StartDate; }
            set { _StartDate = value; }
        }

        public Nullable<DateTime> EndDate
        {
            get { return _EndDate; }
            set { _EndDate = value; }
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

        public String Reason
        {
            get { return _Reason; }
            set { _Reason = value; }
        }

        public Nullable<DateTime> SysCreated
        {
            get { return _SysCreated; }
            set { _SysCreated = value; }
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

        public String SysModifier
        {
            get { return _SysModifier; }
            set { _SysModifier = value; }
        }

        public Nullable<Boolean> IsDeleted
        {
            get { return _IsDeleted; }
            set { _IsDeleted = value; }
        }

        public Nullable<Guid> SysGuid
        {
            get { return _SysGuid; }
            set { _SysGuid = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public IAuditLog AuditLog
        {
            get { return _AuditLog; }
            set { _AuditLog = value; }
        }

        public DataTable Retrieve(String SysCreator)
        {
            return StockCountDac.Retrieve(SysCreator).Tables[0];
        }

        public static DataTable RetrieveWarehouseToBeOpened(String Email)
        {
            return StockCountDac.RetrieveWarehouseToBeOpened(Email).Tables[0]; 
        }

        public static DataTable RetrieveWarehouseToBeReOpened(String Email)
        {
            return StockCountDac.RetrieveWarehouseToBeReOpened(Email).Tables[0];
        }

        public static DataTable RetrieveWarehouseToBeClosed(String Email)
        {
            return StockCountDac.RetrieveWarehouseToBeClosed(Email).Tables[0];
        }

        public static DataTable RetrieveWarehouseToBeChecked(String Email)
        {
            return StockCountDac.RetrieveWarehouseToBeChecked(Email).Tables[0]; 
        }

        public static DataTable RetrieveWarehouseToBeApproved(String Email) 
        {
            return StockCountDac.RetrieveWarehouseToBeApproved(Email).Tables[0];
        }

        public static DataTable RetrieveStatus(Int32 Year, Int32 Period, Nullable<Int32> Status = null)
        {
            return StockCountDac.RetrieveStatus(Year, Period, Status).Tables[0];
        }

        public static DataTable RetrieveAllPendingApprovalWarehouses(Int32 Year, Int32 Period)
        {
            return StockCountDac.RetrieveAllPendingApprovalWarehouses(Year, Period).Tables[0];
        }

        public void Close()
        {
            StockCountDac.Close(this);
        }

        public void Open()
        {
            StockCountDac.Open(this);
        }

        public void Check() 
        {
            StockCountDac.Check(this);
        }

        public void Approve()
        {
            StockCountDac.Approve(this);
        }

        public void Process()
        {
            StockCountDac.Process(this);
        }
    }
}