using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Extensions;
using DataAccess;
using Interface;

namespace Business
{
    public class Issue : IIssue 
    {
        private Nullable<Int32> _Id;
        private String _Department;
        private String _Notes;
        Nullable<Boolean> _IsClosed { get; set; }
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private String _SysModifier;
        private Nullable<DateTime> _SysModified;
        private DataTable _Info;

        public Issue()
        {
        }

        public Issue(Int32 Id)
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
            _Department = row["Department"] == DBNull.Value ? string.Empty : Convert.ToString(row["Department"]);
            _Notes = row["Notes"] == DBNull.Value ? string.Empty : Convert.ToString(row["Notes"]);
            _IsClosed = row["IsClosed"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["IsClosed"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysModifier"]);
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
        }

        public Nullable<Int32> Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        public String Department 
        { 
            get { return _Department; } 
            set { _Department = value; } 
        }

        public String Notes 
        { 
            get { return _Notes; } 
            set { _Notes = value; } 
        }

        public Nullable<Boolean> IsClosed
        {
            get { return _IsClosed; } 
            set { _IsClosed = value; }
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

        public String SysModifier 
        { 
            get { return _SysModifier; } 
            set { _SysModifier = value; } 
        }

        public Nullable<DateTime> SysModified 
        { 
            get { return _SysModified; } 
            set { _SysModified = value; } 
        }

        public DataTable Info 
        { 
            get { return _Info; } 
            set { _Info = value; } 
        }

        public Int32 Insert()
        {
            return IssueDac.Insert(this); 
        }

        public void Update()
        {
            IssueDac.Update(this);
        }

        public void Delete(Int32 Id)
        {
            IssueDac.Delete(Id);
        }

        public void Close()
        {
            IssueDac.Close(this); 
        }

        public DataTable Retrieve()
        {
            return IssueDac.Retrieve(this).Tables[0];
        }

        public DataTable Retrieve(String Department)   
        {
            return IssueDac.Retrieve(Department).Tables[0];
        }

        public DataTable Retrieve(Int32 Id)
        {
            return IssueDac.Retrieve(Id).Tables[0];
        }
    }
}