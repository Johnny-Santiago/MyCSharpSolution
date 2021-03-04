///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  StockCountResultDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 26-OCT-2015
///   Description: This file is the data access of StockCountResult.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
///
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
    public class StockCountResultDac : Configuration
    {
        public static DataSet Retrieve(Int32 ID)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", ID)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_SelectByID", _parameters);
        }

        public static DataSet Retrieve(String InventoryTagNo)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@InventoryTagNo", InventoryTagNo)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_SelectByInventoryTagNo", _parameters);
        }

        public static DataSet Retrieve(Int32 Year, Int32 Period)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Year", Year)
                ,new SqlParameter("@Period", Period)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_SelectByYearPeriod", _parameters);
        }

        public static DataSet Retrieve(IStockCountResult StockCountResult)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Year", StockCountResult.Year)
                ,new SqlParameter("@Period", StockCountResult.Period)
                ,new SqlParameter("@Warehouse", StockCountResult.Warehouse)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_SelectByYearPeriodWarehouse", _parameters);
        }

        public static DataSet RetrieveKab(IStockCountResult StockCountResult)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Year", StockCountResult.Year)
                ,new SqlParameter("@Period", StockCountResult.Period)
                ,new SqlParameter("@Warehouse", StockCountResult.Warehouse)
                ,new SqlParameter("@WhseLocation",StockCountResult.Location)
                ,new SqlParameter("@ItemCode",StockCountResult.ItemCode)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_RetrieveStockTakingResult", _parameters);
        }
        public static DataSet Insert(IStockCountResult StockCountResult)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Year", StockCountResult.Year)
                ,new SqlParameter("@Period", StockCountResult.Period)
                ,new SqlParameter("@ItemCode", StockCountResult.ItemCode)
                ,new SqlParameter("@UOM", StockCountResult.UOM)
                ,new SqlParameter("@Warehouse", StockCountResult.Warehouse)
                ,new SqlParameter("@Location", StockCountResult.Location)
                ,new SqlParameter("@SysCreator", StockCountResult.SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_Insert", _parameters);
        }

        public static DataSet Update(IStockCountResult StockCountResult)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@InventoryTagNo", StockCountResult.InventoryTagNo)
                ,new SqlParameter("@ActualQty", StockCountResult.ActualQty)
                ,new SqlParameter("@SysModifier", StockCountResult.SysModifier)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCountResults_Update", _parameters);
        }
    }
}