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
    public class ChartDataDac : Configuration
    {
        public static DataSet Retrieve(Int32 ChartId, DateTime ReportDate)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ChartId", ChartId)
		        ,new SqlParameter("@ReportDate", ReportDate)
            };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ChartData_SelectByChartIdAndReportDate", _parameters);
        }

        public static void Insert(IChartData ChartData)    
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ChartId", ChartData.ChartId)
		        ,new SqlParameter("@ReportDate", ChartData.ReportDate)
                ,new SqlParameter("@Info", ChartData.NewInfo) 
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ChartData_InsertInfo", _parameters); 
        }
    }
}