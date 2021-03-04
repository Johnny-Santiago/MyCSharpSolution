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
    public class Semester
    {
        private Nullable<Int32> _Id;
        private Nullable<Int32> _Number;
        private Nullable<Int32> _Year;
        private Nullable<DateTime> _DateFrom;
        private Nullable<DateTime> _DateTo;
        private DataTable _Info;

        public Semester()
        {
        }

        public Semester(DateTime Date) 
        {
            _Info = Retrieve(Date);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Date not found. Please setup semesters table.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _Number = row["Number"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Number"]);
            _Year = row["Year"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Year"]);
            _DateFrom = row["DateFrom"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateFrom"]);
            _DateTo = row["DateTo"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateTo"]);
        }

        public Nullable<Int32> Id 
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public Nullable<Int32> Number 
        {
            get { return _Number; }
            set { _Number = value; } 
        }

        public Nullable<Int32> Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public Nullable<DateTime> DateFrom 
        {
            get { return _DateFrom; }
            set { _DateFrom = value; } 
        }

        public Nullable<DateTime> DateTo 
        {
            get { return _DateTo; }
            set { _DateTo = value; } 
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public DataTable Retrieve(DateTime Date) 
        {
            return SemesterDac.Retrieve(Date).Tables[0];
        }
    }
}