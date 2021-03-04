using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class ReportingDepartmentDac : Configuration
    {
        public static DataSet GetReportingDepartments(Nullable<Int32> ResourceId)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", ResourceId)
			};
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_Employees_SelectReportingDepartments", _parameters);
        }
    }
}