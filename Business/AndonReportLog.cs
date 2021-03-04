
///-----------------------------------------------------------------
///   Class:          AndonReportLog.cs
///   Description:    This file is the class for AndonReportLog
///   Author:         Usep Haris Nugraha                    
///	  Create Date: 	  December, 10 2015
///-----------------------------------------------------------------
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Interface;
using System.Data;
using DataAccess;
using Extensions;

namespace Business
{
    public class AndonReportLog : IAndonReportLog
    {
        private Nullable<Int32> _ID;
        private Nullable<Int32> _Condition;
        private String _Warehouse;
        private String _Machine;
        private String _Problem;
        private String _CorrectiveAction;
        private String _Group;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private Nullable<DateTime> _StartTime;
        private Nullable<DateTime> _EndTime;
        private String _SysModifier;
        private Nullable<DateTime> _SysModified;
        private DataTable _Info;

        public AndonReportLog()
        {
        }

        public AndonReportLog(Int32 ID)
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

        public AndonReportLog(string warehouse, string machine)
        {
            _Info = Retrieve(warehouse, machine);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("warehouse or machine not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _Condition = row["Condition"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Condition"]);
            _Warehouse = row["Warehouse"] == DBNull.Value ? string.Empty : row["Warehouse"].ToString();
            _Machine = row["Machine"] == DBNull.Value ? string.Empty : row["Machine"].ToString();
            _Problem = row["Problem"] == DBNull.Value ? string.Empty : row["Problem"].ToString();
            _CorrectiveAction = row["CorrectiveAction"] == DBNull.Value ? string.Empty : row["CorrectiveAction"].ToString();
            _Group = row["Group"] == DBNull.Value ? string.Empty : row["Group"].ToString();
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : row["SysCreator"].ToString();
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _StartTime = row["StartTime"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["StartTime"]);
            _EndTime = row["EndTime"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["EndTime"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : row["SysModifier"].ToString();
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
        }

        public DataTable Retrieve()
        {
            return AndonReportLogDac.Retrieve(this).Tables[0];
        }

        public DataTable Retrieve(Int32 ID)
        {
            return AndonReportLogDac.Retrieve(ID).Tables[0];
        }

        public DataTable Retrieve(string warehouse, string machine)
        {
            return AndonReportLogDac.Retrieve(warehouse, machine).Tables[0];
        }

        public DataTable Retrieve(string warehouse, string machine, string group)
        {
            return AndonReportLogDac.Retrieve(warehouse, machine, group).Tables[0];
        }

        public static DataTable Retrieve(string warehouse, string machine, Int32 condition)
        {
            return AndonReportLogDac.Retrieve(warehouse, machine, condition).Tables[0];
        }

        public static DataTable Retrieve(string warehouse, Int32 condition)
        {
            return AndonReportLogDac.Retrieve(warehouse, condition).Tables[0];
        }

        public Int32 Insert()
        {
            return AndonReportLogDac.Insert(this);
        }

        public Int32 Update()
        {
            return AndonReportLogDac.Update(this);
        }

        public Nullable<Int32> ID { get { return _ID; } set { _ID = value; } }
        public Nullable<Int32> Condition { get { return _Condition; } set { _Condition = value; } }
        public String Warehouse { get { return _Warehouse; } set { _Warehouse = value; } }
        public String Machine { get { return _Machine; } set { _Machine = value; } }
        public String Problem { get { return _Problem; } set { _Problem = value; } }
        public String CorrectiveAction { get { return _CorrectiveAction; } set { _CorrectiveAction = value; } }
        public String Group { get { return _Group; } set { _Group = value; } }
        public String Syscreator { get { return _SysCreator; } set { _SysCreator = value; } }
        public Nullable<DateTime> SysCreated { get { return _SysCreated; } set { _SysCreated = value; } }
        public Nullable<DateTime> StartTime { get { return _StartTime; } set { _StartTime = value; } }
        public Nullable<DateTime> EndTime { get { return _EndTime; } set { _EndTime = value; } }
        public String SysModifier { get { return _SysModifier; } set { _SysModifier = value; } }
        public Nullable<DateTime> SysModified { get { return _SysModified; } set { _SysModified = value; } }
        public DataTable Info { get { return _Info; } set { _Info = value; } }


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
