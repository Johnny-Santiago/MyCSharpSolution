///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  StockCountPeriodDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 07-DEC-2015
///   Description: This file is the data access of StockCountPeriod.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
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
    public class StockCountPeriodDac : Configuration
    {
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountPeriods_Select");
        }

        public static void Update(IStockCountPeriod StockCountPeriod, String SysModifier)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Year", StockCountPeriod.Year)
                ,new SqlParameter("@Period", StockCountPeriod.Period)
                ,new SqlParameter("@CutOff", StockCountPeriod.CutOff)
		        ,new SqlParameter("@SysModifier", SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_StockCountPeriods_Update", _parameters);
        }
    }
}