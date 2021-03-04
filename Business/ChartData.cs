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
    public class ChartData : IChartData
    {
        private DataTable _Info;
        private String _NewInfo;
        private Nullable<Int32> _ChartId;
        private Nullable<DateTime> _ReportDate;

        public ChartData()
        {
        }

        public ChartData(Int32 ChartId, DateTime ReportDate)  
        {
            _Info = ChartDataDac.Retrieve(ChartId, ReportDate).Tables[0];
            if (!_Info.HasRows())
            {
                throw new KeyNotFoundException("No data found.");
            }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public  String NewInfo 
        { 
            get { return _NewInfo; }
            set { _NewInfo = value; } 
        }

        public Nullable<Int32> ChartId
        {
            get { return _ChartId; }
            set { _ChartId = value; }
        }

        public Nullable<DateTime> ReportDate 
        {
            get { return _ReportDate; }
            set { _ReportDate = value; }
        }

        public void Insert()
        {
            ChartDataDac.Insert(this); 
        }
    }
}