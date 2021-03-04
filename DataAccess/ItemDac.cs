///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  ItemDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 04-MAR-2015
/// Description: This file is the data access of Item.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public class ItemDac : Configuration
    {
        public static DataSet Retrieve(String ItemCode)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ItemCode", ItemCode)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Items_Select", _parameters);
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Id", Id)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Items_SelectByID", _parameters);
        }

        public static DataSet Retrieve(IItem Item, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ItemCode", Item.ItemCode)
                ,new SqlParameter("@Description", Item.Description)
                ,new SqlParameter("@Model", Item.Model)
                ,new SqlParameter("@Classification", Item.Classification)
                ,new SqlParameter("@Family", Item.Family)
                ,new SqlParameter("@Category", Item.Category)
                ,new SqlParameter("@Position", Item.Position)
                ,new SqlParameter("@ItemClassType", Item.ItemClassType)
                ,new SqlParameter("@Identity", Item.Identity)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Items_Search", _parameters);
        }

        public static DataSet Retrieve(IItem Item)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ItemCode", Item.ItemCode)
                ,new SqlParameter("@WhseCode", Item.Warehouse)
                ,new SqlParameter("@FromWhseLocation", Item.Location)
                ,new SqlParameter("@ToWhseLocation", Item.Location2)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Items_SearchForLocationTransfer", _parameters);
        }

        public static Decimal GetAvailableStock(IItem Item)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ItemCode", Item.ItemCode)
                ,new SqlParameter("@WhseCode", Item.Warehouse)
                ,new SqlParameter("@WhseLocation", Item.Location)
            };

            return Convert.ToDecimal(SqlHelper.ExecuteScalar(ConnectionString, "sp_Items_GetStockPerWarehouseLocation", _parameters));
        }

        public static DataSet RetrieveAllHasChild()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Items_HasChild_SelectAll");
        }

        public static DataSet RetrieveAllBOM()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_BOM_Reports");
        }
    }
}
