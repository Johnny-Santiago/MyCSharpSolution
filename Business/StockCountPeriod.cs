///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  StockCountPeriod.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 07-DEC-2015
///   Description: This file is the business rules and logic implementation of StockCountPeriod.
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
    public class StockCountPeriod : IStockCountPeriod
    {
        private Nullable<Int32> _ID;
        private Nullable<Int32> _Year;
        private Nullable<Int32> _Period;
        private Nullable<DateTime> _CutOff;
        private Nullable<Boolean> _Status;
        private DataTable _Info;

        public StockCountPeriod()
        {
            _Info = Retrieve();
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new Exception("Stock taking not yet initialized. Must undergo label preparation first.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _Year = row["Year"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Year"]);
            _Period = row["Period"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Period"]);
            _CutOff = row["CutOff"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["CutOff"]);
            _Status = row["Status"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Status"]);
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

        public Nullable<DateTime> CutOff
        {
            get { return _CutOff; }
            set { _CutOff = value; }
        }

        public Nullable<Boolean> Status
        {
            get { return _Status; } 
            set { _Status = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
        }

        public DataTable Retrieve()
        {
            return StockCountPeriodDac.Retrieve().Tables[0]; 
        }

        public void Update(String SysModifier)
        {
            StockCountPeriodDac.Update(this, SysModifier);
        }
    }
}