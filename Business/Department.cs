using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using Extensions;
using Interface;
using System.Data;

namespace Business
{
    public class Department : IDepartment
    {
        private Nullable<Int32> _Id;
        private string _Name;
        private Nullable<Int32> _Sequence;
        private string _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private Nullable<DateTime> _SysCreated2;
        private string _SysModifier;
        private Nullable<DateTime> _SysModified;
        private Nullable<DateTime> _SysModified2;
        private DataTable _Info;

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        public Nullable<Int32> Sequence
        {
            get { return _Sequence; }
            set { _Sequence = value; }
        }

        public string SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
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

        public string SysModifier
        {
            get { return _SysModifier; }
            set { _SysModifier = value; }
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

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public Department()
        {
        }

        public Department(int Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
                return;
            }

            throw new KeyNotFoundException("Id not found.");
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = ((row["ID"] == DBNull.Value) ? null : new int?(Convert.ToInt32(row["ID"])));
            _Name = ((row["Department"] == DBNull.Value) ? string.Empty : Convert.ToString(row["Department"]));
            _Sequence = ((row["Sequence"] == DBNull.Value) ? null : new int?(Convert.ToInt32(row["Sequence"])));
            _SysCreator = ((row["SysCreator"] == DBNull.Value) ? string.Empty : Convert.ToString(row["SysCreator"]));
            _SysCreated = ((row["SysCreated"] == DBNull.Value) ? null : new DateTime?(Convert.ToDateTime(row["SysCreated"])));
            _SysModifier = ((row["SysModifier"] == DBNull.Value) ? string.Empty : Convert.ToString(row["SysModifier"]));
            _SysModified = ((row["SysModified"] == DBNull.Value) ? null : new DateTime?(Convert.ToDateTime(row["SysModified"])));
        }

        public int Insert()
        {
            return DepartmentDac.Insert(this);
        }

        public void Update()
        {
            DepartmentDac.Update(this);
        }

        public void Delete(int Id)
        {
            DepartmentDac.Delete(Id);
        }

        public DataTable RetrieveAll()
        {
            return DepartmentDac.Retrieve().Tables[0];
        }

        public DataTable Retrieve()
        {
            return DepartmentDac.Retrieve(this).Tables[0];
        }

        public DataTable Retrieve(int Id)
        {
            return DepartmentDac.Retrieve(Id).Tables[0];
        }
    }
}