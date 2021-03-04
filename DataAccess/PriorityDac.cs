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
    public class PriorityDac : Configuration 
    {
        public static Int32 Insert(IPriority Priority)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ReportDate", Priority.ReportDate)
		        ,new SqlParameter("@Department", Priority.Department)
		        ,new SqlParameter("@Notes", Priority.Notes)
		        ,new SqlParameter("@SysCreator", Priority.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Priorities_InsertPriority", _parameters));
        }

        public static void Update(IPriority Priority)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", Priority.Id)
		        ,new SqlParameter("@ReportDate", Priority.ReportDate)
		        ,new SqlParameter("@Department", Priority.Department)
		        ,new SqlParameter("@Notes", Priority.Notes)
		        ,new SqlParameter("@SysModifier", Priority.SysModifier)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Priorities_UpdateById", _parameters);
        }

        public static void Delete(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@Id", Id) };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Priorities_DeleteById", _parameters);
        }

        public static DataSet Retrieve(String Department, DateTime ReportDate)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Department)
                ,new SqlParameter("@ReportDate", ReportDate)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Priorities_SelectByDepartmentAndReportDate", _parameters);
        }

        public static DataSet Retrieve(IPriority Priority)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Priority.Department)
                ,new SqlParameter("@DateFrom", Priority.SysCreated)
                ,new SqlParameter("@DateTo", Priority.SysModified)
                ,new SqlParameter("@Notes", Priority.Notes)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Priorities_SearchPriorities", _parameters);
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Priorities_SelectById", _parameters);
        }
    }
}