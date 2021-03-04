///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  Quotation.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 26-JUL-2016
///   Description: This file is the business rules and logic implementation of Quotation.
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
    public class Quotation : IQuotation 
    {
        private Nullable<Int32> _Id;
        private String _CustomerCode;
        private Nullable<DateTime> _ForecastDate;
        private Nullable<DateTime> _ForecastDate2;
        private String _QuotationNo;
        private Nullable<Int32> _Status;
        private Nullable<Boolean> _Failed;
        private Nullable<Int32> _FailCount;
        private String _Remarks;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private Nullable<DateTime> _SysModified;
        private String _Warehouse;
        private DataTable _Info;
        private DataTable _List;

        public Quotation()
        {
        }

        public Quotation(Int32 Id)
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
            _CustomerCode = row["CustomerCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["CustomerCode"]);
            _ForecastDate = row["ForecastDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["ForecastDate"]);
            _QuotationNo = row["Quotation"] == DBNull.Value ? string.Empty : Convert.ToString(row["Quotation"]);
            _Status = row["Status"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Status"]);
            _Failed = row["Failed"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Failed"]);
            _FailCount = row["FailCount"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["FailCount"]);
            _Remarks = row["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(row["Remarks"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
            _Warehouse = row["Warehouse"] == DBNull.Value ? string.Empty : Convert.ToString(row["Warehouse"]);

            _List = QuotationDetail.Retrieve(_CustomerCode, (DateTime)_ForecastDate);
        }

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public String CustomerCode 
        { 
            get { return _CustomerCode; } 
            set { _CustomerCode = value; } 
        }

        public Nullable<DateTime> ForecastDate 
        { 
            get { return _ForecastDate; } 
            set { _ForecastDate = value; } 
        }

        public Nullable<DateTime> ForecastDate2 
        { 
            get { return _ForecastDate2; } 
            set { _ForecastDate2 = value; } 
        }

        public String QuotationNo   
        { 
            get { return _QuotationNo; }  
            set { _QuotationNo = value; } 
        }

        public Nullable<Int32> Status  
        { 
            get { return _Status; } 
            set { _Status = value; } 
        }

        public Nullable<Boolean> Failed  
        { 
            get { return _Failed; } 
            set { _Failed = value; } 
        }

        public Nullable<Int32> FailCount 
        { 
            get { return _FailCount; } 
            set { _FailCount = value; }
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

        public Nullable<DateTime> SysModified
        { 
            get { return _SysModified; } 
            set { _SysModified = value; } 
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
            return QuotationDac.Retrieve(Id).Tables[0];
        }
    }
}