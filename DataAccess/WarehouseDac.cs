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
    public class WarehouseDac : Configuration
    {
        public static DataSet Retrieve(IWarehouse warehouse, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@WhseCode", warehouse.WhseCode)
                ,new SqlParameter("@Warehouse", warehouse.warehouse)
                ,new SqlParameter("@WhseLocation", warehouse.WhseLocation)
                ,new SqlParameter("@Location", warehouse.Location)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Warehouse_Search", _parameters);
        }


        public static DataSet NewLocRetrieve(IWarehouse warehouse, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@WhseCode", warehouse.WhseCode)
                ,new SqlParameter("@Warehouse", warehouse.warehouse)
                ,new SqlParameter("@WhseLocation", warehouse.WhseLocation)
                ,new SqlParameter("@Location", warehouse.Location)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_NewLoc_Warehouse_Search", _parameters);
        }

        public static DataSet NewRetrieve(IWarehouse warehouse, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Warehouse", warehouse.warehouse)
                ,new SqlParameter("@Description", warehouse.WhseLocation)                
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_NewWarehouses_Search", _parameters);
        }

        public static DataSet Retrieve(String Warehouse, String Description, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Warehouse", Warehouse)
                ,new SqlParameter("@Description", Description)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Warehouses_Search", _parameters);
        }

        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Warehouses_Select");
        }

        public static DataSet RetrieveItems(String WhseCode)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@WhseCode", WhseCode)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Warehouses_SelectItems", _parameters);
        }

        public static DataSet RetrieveLocations(String WhseCode)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@WhseCode", WhseCode)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Warehouses_GetLocations", _parameters);
        }
    }
}