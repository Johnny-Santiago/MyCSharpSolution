using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public class SemesterDac : Configuration
    {
        public static DataSet Retrieve(DateTime Date)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Date", Date)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Semesters_SelectByDate", _parameters);
        }
    }
}