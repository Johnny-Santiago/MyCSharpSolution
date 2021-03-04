///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SIOPDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 16-MAY-2015
///   Description: This file is the data access of SIOP.
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
    public class SIOPDac : Configuration
    {
        public static DataSet Retrieve(String SIOPID, String SysCreator)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SIOPID", SIOPID)
                ,new SqlParameter("@SysCreator", SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_RetrieveOrInsert", _parameters);
        }

        public static DataSet RetrieveFormatOnly()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_SetFormatOnly");
        }

        public static DataSet RetrieveGroupFormatOnly()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_Grouped_SetFormatOnly");
        }

        public static DataSet Retrieve(ISIOP SIOP, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@WhseCode", SIOP.WhseCode)
                ,new SqlParameter("@WhseLocation", SIOP.WhseLocation)
                ,new SqlParameter("@ItemCode", SIOP.ItemCode)
                ,new SqlParameter("@Description", SIOP.Description)
                ,new SqlParameter("@Model", SIOP.Model)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_Search", _parameters);
        }

        public static DataSet RetrieveGroup(ISIOP SIOP, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@WorkCenterCode", SIOP.WorkCenterCode)
                ,new SqlParameter("@WhseCode", SIOP.WhseCode)
                ,new SqlParameter("@WhseLocation", SIOP.WhseLocation)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_Grouped_Search", _parameters);
        }

        public static DataSet Arbitrate(ISIOP SIOP)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Year", SIOP.Year)
                ,new SqlParameter("@Period", SIOP.Period)
                ,new SqlParameter("@ItemCode", SIOP.ItemCode)
                ,new SqlParameter("@Quantity", SIOP.Quantity)
                ,new SqlParameter("@SysCreator", SIOP.SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_Arbitrate", _parameters);
        }

        public static DataSet ArbitrateByGroup(ISIOP SIOP)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Year", SIOP.Year)
                ,new SqlParameter("@Period", SIOP.Period)
                ,new SqlParameter("@WhseCode", SIOP.WhseCode)
                ,new SqlParameter("@WorkCenterCode", SIOP.WorkCenterCode)
                ,new SqlParameter("@WhseLocation", SIOP.WhseLocation)
                ,new SqlParameter("@Quantity", SIOP.Quantity)
                ,new SqlParameter("@SysCreator", SIOP.SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_Grouped_ArbitrateByGroup", _parameters);
        }

        public static DataSet ArbitrateByPart(ISIOP SIOP)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Year", SIOP.Year)
                ,new SqlParameter("@Period", SIOP.Period)
                ,new SqlParameter("@WhseCode", SIOP.WhseCode)
                ,new SqlParameter("@WorkCenterCode", SIOP.WorkCenterCode)
                ,new SqlParameter("@WhseLocation", SIOP.WhseLocation)
                ,new SqlParameter("@ItemCode", SIOP.ItemCode)
                ,new SqlParameter("@Quantity", SIOP.Quantity)
                ,new SqlParameter("@SysCreator", SIOP.SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_Grouped_ArbitrateByPart", _parameters);
        }

        public static string GetPeriod(Int32 Sequence)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Sequence", Sequence)
            };

            return Convert.ToString(SqlHelper.ExecuteScalar(ConnectionString, "sp_SIOP_GetPeriod", _parameters));
        }

        public static Int32 GetPeriodCoverage() 
        {
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SIOP_Coverage_GetPeriods"));
        }

        public static void Initialize(ISIOP SIOP)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SIOPID", SIOP.SIOPID)
                ,new SqlParameter("@Type", SIOP.PlanType)
                ,new SqlParameter("@SysCreator", SIOP.SysCreator)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_SIOP_Initialize", _parameters);
        }

        public static void Review(ISIOP SIOP)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SIOPID", SIOP.SIOPID)
                ,new SqlParameter("@SysModifier", SIOP.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_SIOP_Review", _parameters);
        }

        public static void Approve(ISIOP SIOP)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SIOPID", SIOP.SIOPID)
                ,new SqlParameter("@SysModifier", SIOP.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_SIOP_Approve", _parameters);
        }

        public static DataSet GetOptions()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetOptions");
        }

        public static DataSet GetPlanningTypes()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetPlanningTypes");
        }

        public static DataSet GetMessages()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetMessages");
        }

        public static Boolean HasInitialized(String SIOPID)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SIOPID", SIOPID)
            };

            return Convert.ToBoolean(SqlHelper.ExecuteScalar(ConnectionString, "sp_SIOP_HasInitialized", _parameters));
        }

        public static DataSet GetArchiveVersions(String SIOPID)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SIOPID", SIOPID.Equals(string.Empty) ? null : SIOPID)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetArchiveVersions", _parameters);
        }

        public static DataSet GetWorkCenters()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetWorkCenters");
        }

        public static DataSet GetDistinctWorkCenters()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetDistinctWorkCenters");
        }

        public static DataSet GetLineAndMachines()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetLineAndMachines");
        }

        public static DataSet GetDistinctLineAndMachines()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetDistinctLineAndMachines");
        }

        public static DataSet GetUserColumns(String SysCreator)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SysCreator", SysCreator) 
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SIOP_GetUserColumns", _parameters);
        }

        public static void SetUserColumns(String Xml, String SysCreator)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Xml", Xml) 
                ,new SqlParameter("@SysCreator", SysCreator) 
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_SIOP_SetUserColumns", _parameters);
        }
    }
}