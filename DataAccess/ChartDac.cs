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
    public class ChartDac : Configuration
    {
        public static Int32 Insert(IChart Chart)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Department", Chart.Department)
		        ,new SqlParameter("@Title", Chart.Title)
		        ,new SqlParameter("@SysCreator", Chart.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Charts_InsertChart", _parameters));
        }

        public static void Update(IChart Chart)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", Chart.Id)
		        ,new SqlParameter("@Department", Chart.Department)
		        ,new SqlParameter("@Title", Chart.Title)
		        ,new SqlParameter("@SysModifier", Chart.SysModifier)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Charts_UpdateById", _parameters);
        }

        public static void Delete(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@Id", Id) };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Charts_DeleteById", _parameters);
        }

        public static DataSet RetrieveSample()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Charts_Select");
        }

        public static DataSet Retrieve() 
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Charts_SelectCharts");
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Charts_SelectById", _parameters);
        }
    }
}