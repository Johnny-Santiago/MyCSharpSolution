///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  StockCountResult.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 26-OCT-2015
///   Description: This file is the business rules and logic implementation of StockCountResult.
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
    public class StockCountResult : IStockCountResult
    {
        private Nullable<Int32> _ID;
        private String _InventoryTagNo;
        private Nullable<Int32> _Year;
        private Nullable<Int32> _Period;
        private String _ItemCode;
        private String _Description;
        private String _Warehouse;
        private String _Location;
        private Nullable<Decimal> _ActualQty;
        private String _UOM;
        private Nullable<DateTime> _SysCreated;
        private String _SysCreator;
        private Nullable<DateTime> _SysModified;
        private String _SysModifier;
        private String _Type;
        private DataTable _Info;

        public StockCountResult(Int32 ID)
        {
            _Info = Retrieve(ID);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("ID not found.");
            }
        }

        public StockCountResult(String InventoryTagNo)
        {
            _Info = Retrieve(InventoryTagNo);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Inventory tag no. not found.");
            }
        }
        public StockCountResult()
        {

        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _InventoryTagNo = row["InventoryTagNo"] == DBNull.Value ? string.Empty : Convert.ToString(row["InventoryTagNo"]);
            _Year = row["Year"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Year"]);
            _Period = row["Period"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Period"]);
            _ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["ItemCode"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _Warehouse = row["Warehouse"] == DBNull.Value ? string.Empty : Convert.ToString(row["Warehouse"]);
            _Location = row["Location"] == DBNull.Value ? string.Empty : Convert.ToString(row["Location"]);
            _ActualQty = row["ActualQty"] == DBNull.Value ? (Nullable<Decimal>)null : Convert.ToDecimal(row["ActualQty"]);
            _UOM = row["UOM"] == DBNull.Value ? string.Empty : Convert.ToString(row["UOM"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysModifier"]);
            _Type = row["Type"] == DBNull.Value ? string.Empty : Convert.ToString(row["Type"]);
        }

        public Nullable<Int32> ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public String InventoryTagNo
        {
            get { return _InventoryTagNo; }
            set { _InventoryTagNo = value; }
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

        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public String Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }

        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public Nullable<Decimal> ActualQty
        {
            get { return _ActualQty; }
            set { _ActualQty = value; }
        }

        public String UOM
        {
            get { return _UOM; }
            set { _UOM = value; }
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
        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public DataTable Retrieve(Int32 ID)
        {
            return StockCountResultDac.Retrieve(ID).Tables[0];
        }

        public DataTable Retrieve(String InventoryTagNo)
        {
            return StockCountResultDac.Retrieve(InventoryTagNo).Tables[0];
        }

        public DataTable Retrieve()
        {
            return StockCountResultDac.Retrieve(this).Tables[0];
        }

        public static DataTable Retrieve(Int32 Year, Int32 Period)
        {
            return StockCountResultDac.Retrieve(Year, Period).Tables[0];
        }
        public DataTable RetrieveKab()
        {
            return StockCountResultDac.RetrieveKab(this).Tables[0];
        }
    }
}