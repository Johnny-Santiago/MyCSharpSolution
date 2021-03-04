using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data;
using System.Data.SqlClient;
using Interface;

namespace DataAccess
{
    public class SQLJobDac : Configuration
    {
        public static DataSet Retrieve(string JobName)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@JobName", JobName)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_job_statuscheck",_parameters);
        }

        public static void Execute(string jobName)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@JobName", jobName)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Job_Start", _parameters);
        }
    }
}