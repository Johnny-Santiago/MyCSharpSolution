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
    public class InventoryDac : Configuration
    {
        public static Int32 Insert(IInventory inventorygetdate)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@GenDate", inventorygetdate.datereport),
                new SqlParameter("@SysCreator",inventorygetdate.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_ItemCost_SchGenerateDate", _parameters));
        }
    }
}