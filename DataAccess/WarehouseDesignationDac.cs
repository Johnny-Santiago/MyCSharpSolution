///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  WarehouseDesignationDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 04-MAR-2015
///   Description: This file is the data access of WarehouseDesignationDac.
///   Version:     1
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
    public class WarehouseDesignationDac : Configuration
    {
        public static DataSet GetReceipt(String UserId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_WarehouseDesignations_SelectReceipt", _parameters);
        }

        public static DataSet GetIssuance(String UserId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_WarehouseDesignations_SelectIssuance", _parameters);
        }

        public static DataSet GetWarehouseDesignations(String UserId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_WarehouseDesignations_Select", _parameters);
        }

        public static DataSet UpdateWarehouseDesignations(String UserId, String Designations)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
                ,new SqlParameter("@Designations", Designations)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_WarehouseDesignations_Update", _parameters);
        }
    }
}