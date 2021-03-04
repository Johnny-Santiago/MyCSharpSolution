using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using DataAccess;
using Interface;
using System.Data;

namespace Business
{
    public class ReportingDepartment : IReportingDepartment
    {
        private DataTable _Departments;
        public DataTable Departments
        {
            get { return _Departments; }
            set { _Departments = value; }
        }

        public ReportingDepartment()
        {
        }

        public ReportingDepartment(Nullable<Int32> ResourceId)
        {
            _Departments = ReportingDepartmentDac.GetReportingDepartments(ResourceId).Tables[0];
        }
    }
}