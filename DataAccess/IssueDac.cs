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
    public class IssueDac : Configuration 
    {
        public static Int32 Insert(IIssue Issue)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Issue.Department)
		        ,new SqlParameter("@Notes", Issue.Notes)
		        ,new SqlParameter("@SysCreator", Issue.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Issues_InsertIssue", _parameters));
        }

        public static void Update(IIssue Issue)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", Issue.Id)
		        ,new SqlParameter("@Department", Issue.Department)
		        ,new SqlParameter("@Notes", Issue.Notes)
		        ,new SqlParameter("@SysModifier", Issue.SysModifier)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Issues_UpdateById", _parameters);
        }

        public static void Close(IIssue Issue) 
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", Issue.Id)
                ,new SqlParameter("@IsClosed", Issue.IsClosed)
		        ,new SqlParameter("@SysModifier", Issue.SysModifier)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Issues_CloseById", _parameters);
        }

        public static void Delete(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@Id", Id) };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Issues_DeleteById", _parameters);
        }

        public static DataSet Retrieve(String Department)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Department)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Issues_SelectByDepartment", _parameters);
        }

        public static DataSet Retrieve(IIssue Issue)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Issue.Department)
                ,new SqlParameter("@Notes", Issue.Notes)
                ,new SqlParameter("@IsClosed", Issue.IsClosed)
                ,new SqlParameter("@DateFrom", Issue.SysCreated)
                ,new SqlParameter("@DateTo", Issue.SysModified)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Issues_SearchIssues", _parameters);
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Issues_SelectById", _parameters);
        }
    }
}