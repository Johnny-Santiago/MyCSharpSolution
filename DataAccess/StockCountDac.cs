///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  StockCountDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 23-OCT-2015
///   Description: This file is the data access of StockCount.
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
    public class StockCountDac : Configuration
    {
        public static DataSet Retrieve(String SysCreator)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@SysCreator", SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_RetrieveOrInsert", _parameters);
        }

        public static DataSet RetrieveWarehouseToBeOpened(String Email)  
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Email", Email)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_RetrieveToBeOpened", _parameters);
        }

        public static DataSet RetrieveWarehouseToBeReOpened(String Email) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Email", Email)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_RetrieveToBeReOpened", _parameters);
        }

        public static DataSet RetrieveWarehouseToBeClosed(String Email) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Email", Email)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_RetrieveToBeClosed", _parameters);
        }

        public static DataSet RetrieveWarehouseToBeChecked(String Email) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Email", Email)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_RetrieveToBeChecked", _parameters);
        }

        public static DataSet RetrieveWarehouseToBeApproved(String Email) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Email", Email)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_RetrieveToBeApproved", _parameters);
        }

        public static DataSet RetrieveStatus(Int32 Year, Int32 Period, Nullable<Int32> Status = null)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Year", Year)
                ,new SqlParameter("@Period", Period)
                ,new SqlParameter("@Status", Status)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_GetStatus", _parameters);
        }

        public static DataSet RetrieveAllPendingApprovalWarehouses(Int32 Year, Int32 Period)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Year", Year)
                ,new SqlParameter("@Period", Period)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_StockCounts_GetAllPendingApprovalWarehouses", _parameters);
        }


        public static void Close(IStockCount StockCount)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", StockCount.ID)
                ,new SqlParameter("@Reason", StockCount.Reason)
                ,new SqlParameter("@SysModifier", StockCount.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_StockCounts_Close", _parameters);
        }

        public static void Open(IStockCount StockCount)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", StockCount.ID)
                ,new SqlParameter("@Reason", StockCount.Reason)
                ,new SqlParameter("@SysModifier", StockCount.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_StockCounts_Open", _parameters);
        }

        public static void Check(IStockCount StockCount) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", StockCount.ID)
                ,new SqlParameter("@Reason", StockCount.Reason)
                ,new SqlParameter("@SysModifier", StockCount.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_StockCounts_Check", _parameters);
        }

        public static void Approve(IStockCount StockCount)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", StockCount.ID)
                ,new SqlParameter("@Reason", StockCount.Reason)
                ,new SqlParameter("@SysModifier", StockCount.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_StockCounts_Approve", _parameters);
        }

        public static void Process(IStockCount StockCount)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", StockCount.ID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_StockCounts_Process", _parameters);
        }
    }
}