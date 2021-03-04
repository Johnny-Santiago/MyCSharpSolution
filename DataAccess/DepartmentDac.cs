using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using Interface;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class DepartmentDac : Configuration
    {
        public static int Insert(IDepartment Department)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Department", Department.Name),
				new SqlParameter("@Sequence", Department.Sequence),
				new SqlParameter("@SysCreator", Department.SysCreator)
			};
            return Convert.ToInt32(SqlHelper.ExecuteScalar(Configuration.ConnectionString, "sp_Departments_InsertDepartment", _parameters));
        }
        public static void Update(IDepartment Department)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", Department.Id),
				new SqlParameter("@Department", Department.Name),
				new SqlParameter("@Sequence", Department.Sequence),
				new SqlParameter("@SysModifier", Department.SysModifier)
			};
            SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, "sp_Departments_UpdateById", _parameters);
        }
        public static void Delete(int Id)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Id", Id)
			};
            SqlHelper.ExecuteNonQuery(Configuration.ConnectionString, "sp_Departments_DeleteById", _parameters);
        }
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_Departments_SelectByDepartments", new object[0]);
        }
        public static DataSet Retrieve(IDepartment Department)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@Department", Department.Name),
				new SqlParameter("@Sequence", Department.Sequence),
				new SqlParameter("@SysCreator", Department.SysCreator),
				new SqlParameter("@SysCreatedFrom", Department.SysCreated),
				new SqlParameter("@SysCreatedTo", Department.SysCreated2),
				new SqlParameter("@SysModifier", Department.SysModifier),
				new SqlParameter("@SysModifiedFrom", Department.SysModified),
				new SqlParameter("@SysModifiedTo", Department.SysModified2)
			};
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_Departments_SearchDepartments", _parameters);
        }
        public static DataSet Retrieve(int Id)
        {
            SqlParameter[] _parameters = new SqlParameter[]
			{
				new SqlParameter("@ID", Id)
			};
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_Departments_SelectById", _parameters);
        }
    }
}