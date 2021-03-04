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
    public class Chart : IChart
    {
        private Nullable<Int32> _Id;
        private String _Department;
        private String _Title; 
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private String _SysModifier;
        private Nullable<DateTime> _SysModified;
        private DataTable _Info;

        //public Chart()
        //{
        //    _Info = ChartDac.Retrieve().Tables[0];
        //    if (!_Info.HasRows())
        //    {
        //        throw new KeyNotFoundException("No data found.");
        //    }
        //}

        public Chart()
        {
        }

        public Chart(Int32 Id)
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
            _Title = row["Title"] == DBNull.Value ? string.Empty : Convert.ToString(row["Title"]);
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

        public String Title
        {
            get { return _Title; }
            set { _Title = value; }
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
            return ChartDac.Insert(this);
        }

        public void Update()
        {
            ChartDac.Update(this);
        }

        public void Delete(Int32 Id)
        {
            ChartDac.Delete(Id);
        }

        public DataTable Retrieve(Int32 Id)
        {
            return ChartDac.Retrieve(Id).Tables[0]; 
        }

        public DataTable RetrieveAll()
        {
            return ChartDac.Retrieve().Tables[0];
        }

        public DataTable RetrieveSample()
        {
            return ChartDac.RetrieveSample().Tables[0]; 
        }
    }
}