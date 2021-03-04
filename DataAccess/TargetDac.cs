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
    public class TargetDac : Configuration 
    {
        public static Int32 Insert(ITarget Target)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Semester", Target.Semester)
                ,new SqlParameter("@Year", Target.Year)
		        ,new SqlParameter("@Department", Target.Department)
		        ,new SqlParameter("@Notes", Target.Notes)
		        ,new SqlParameter("@SysCreator", Target.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Targets_InsertTarget", _parameters));
        }

        public static void Update(ITarget Target)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", Target.Id)
		        ,new SqlParameter("@Semester", Target.Semester)
                ,new SqlParameter("@Year", Target.Year)
		        ,new SqlParameter("@Department", Target.Department)
		        ,new SqlParameter("@Notes", Target.Notes)
		        ,new SqlParameter("@SysModifier", Target.SysModifier)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Targets_UpdateById", _parameters);
        }

        public static void Delete(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@Id", Id) };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Targets_DeleteById", _parameters);
        }

        public static DataSet Retrieve(ITarget Target) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Target.Department)
                ,new SqlParameter("@Semester", Target.Semester)
                ,new SqlParameter("@Year", Target.Year)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Targets_SelectByDepartmentSemesterAndYear", _parameters);
        }

        public static DataSet Retrieve(ITarget Target, Nullable<Int32> Year, Nullable<Int32> Semester)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Target.Department)
                ,new SqlParameter("@Notes", Target.Notes)
                ,new SqlParameter("@SemesterFrom", Target.Semester)
                ,new SqlParameter("@SemesterTo", Semester)
                ,new SqlParameter("@YearFrom", Target.Year)
                ,new SqlParameter("@YearTo", Year)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Targets_SearchTargets", _parameters);
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Targets_SelectById", _parameters);
        }
    }
}