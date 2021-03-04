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
    public class Finance : IFinance
    {
        private Nullable<DateTime> _datereport;
        private String _SysCreator;

        public Finance()
        { 

        }
        public String SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }
        public Nullable<DateTime> datereport
        {
            get { return _datereport; }
            set { _datereport = value; }
        }
        public Int32 Insert()
        {
            return IFinanceDac.Insert(this);
        }
    }
}