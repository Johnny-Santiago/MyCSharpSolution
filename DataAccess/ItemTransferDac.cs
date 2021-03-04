///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  ItemTransferDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 03-MAR-2015
/// Description: This file is the data access of ItemTransfer.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------
///   Revision History:
///   Name:                    Date:                 Description:
///-----------------------------------------------------------------------------------------------------------------
///   Usep Haris N.            11-MAR-2015           Add RetriveByRefId Method.
///-----------------------------------------------------------------------------------------------------------------
///   Juanito U. Santiago Jr.  11-MAR-2015           Add Fulfill Method.
///-----------------------------------------------------------------------------------------------------------------
///   Juanito U. Santiago Jr.  07-APR-2015           Add Result and Parent properties.
///-----------------------------------------------------------------------------------------------------------------
///   Usep Haris N.            10-APR-2015           Add Result and Parent param.
///                                                  Add ItemProd, HasPrintOut,
///                                                  AutoTransfer, PLNo
///                                                  Retreive(), Insert() method.
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
    public class ItemTransferDac : Configuration
    {
        public static String GetNewLocationTransferSequence()
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConnectionString, "sp_ItemTransfers_GetNewIBTLSequence"));
        }

        public static String GetNewInterBranchTransferSequence()
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConnectionString, "sp_ItemTransfers_GetNewIBTISequence"));
        }

        public static String GetNewProductionResultSequence()
        {
            return Convert.ToString(SqlHelper.ExecuteScalar(ConnectionString, "sp_ItemTransfers_GetNewIBTPSequence"));
        }

        public static DataSet Retrieve(String CargoId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@CargoId", CargoId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_Select", _parameters);
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@CargoId", Id)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SelectById", _parameters);
        }

        public static DataSet RetrieveForAutoTransfer(Int32 Id)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@CargoId", Id)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SelectByIdAutoTransfer", _parameters);
        }

        public static void Confirm(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CargoId", ItemTransfer.CargoId)
                ,new SqlParameter("@ToWhseCode", ItemTransfer.ToWhseCode)
                ,new SqlParameter("@ToWhseLocation", ItemTransfer.ToWhseLocation)
                ,new SqlParameter("@SysModifier", ItemTransfer.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ItemTransfers_Confirm", _parameters);
        }

        public static DataSet RetriveByRefId(string RefId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@RefId", RefId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SelectByRefId", _parameters);
        }

        public static DataSet RetriveByProduction(string Production)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Production", Production)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SelectByProductionNo", _parameters);
        }

        public static DataSet RetriveByParent(string Parent)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Parent", Parent)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SelectByParentNo", _parameters);
        }

        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SelectAll");
        }

        public static DataSet Retrieve(IItemTransfer ItemTransfer, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Code", ItemTransfer.Code)
                ,new SqlParameter("@RefId", ItemTransfer.RefId)
                ,new SqlParameter("@PLNo", ItemTransfer.PLNo)
                ,new SqlParameter("@Parent", ItemTransfer.Parent)
                ,new SqlParameter("@Production", ItemTransfer.Production)
                ,new SqlParameter("@EqpId", ItemTransfer.EqpId)
		        ,new SqlParameter("@CargoId", ItemTransfer.CargoId)
                ,new SqlParameter("@ItemCode", ItemTransfer.ItemCode)
                ,new SqlParameter("@Description", ItemTransfer.Description)
                ,new SqlParameter("@Model", ItemTransfer.Model)
                ,new SqlParameter("@FromWhseCode", ItemTransfer.FromWhseCode)
                ,new SqlParameter("@FromWhseLocation", ItemTransfer.FromWhseLocation)
                ,new SqlParameter("@ToWhseCode", ItemTransfer.ToWhseCode)
                ,new SqlParameter("@ToWhseLocation", ItemTransfer.ToWhseLocation)
                ,new SqlParameter("@Remarks", ItemTransfer.Remarks)
                ,new SqlParameter("@Result", ItemTransfer.Result)
                ,new SqlParameter("@Status", ItemTransfer.Status)
                ,new SqlParameter("@HasPrintOut", ItemTransfer.HasPrintOut)
                ,new SqlParameter("@AutoTransfer", ItemTransfer.AutoTransfer)
                ,new SqlParameter("@FromSysCreated", ItemTransfer.SysCreated)
                ,new SqlParameter("@ToSysCreated", ItemTransfer.SysCreated2)
                ,new SqlParameter("@SysCreator", ItemTransfer.SysCreator)
                ,new SqlParameter("@FromSysModified", ItemTransfer.SysModified)
                ,new SqlParameter("@ToSysModified", ItemTransfer.SysModified2)
                ,new SqlParameter("@SysModifier", ItemTransfer.SysModifier)
                ,new SqlParameter("@Shift", ItemTransfer.Shift)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_Search", _parameters);
        }

        public static DataSet Retrieve(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ToWhseCode", ItemTransfer.ToWhseCode)
                ,new SqlParameter("@ToWhseLocation", ItemTransfer.ToWhseLocation)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_Search", _parameters);
        }

        public static Int32 Insert(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Code", ItemTransfer.Code)
                ,new SqlParameter("@RefId", ItemTransfer.RefId)
                ,new SqlParameter("@PLNo", ItemTransfer.PLNo)
                ,new SqlParameter("@Parent", ItemTransfer.Parent)
                ,new SqlParameter("@Production", ItemTransfer.Production)
                ,new SqlParameter("@EqpId", ItemTransfer.EqpId)
                ,new SqlParameter("@ItemCode", ItemTransfer.ItemCode)
                ,new SqlParameter("@FromWhseCode", ItemTransfer.FromWhseCode)
                ,new SqlParameter("@FromWhseLocation", ItemTransfer.FromWhseLocation)
                ,new SqlParameter("@ToWhseCode", ItemTransfer.ToWhseCode)
                ,new SqlParameter("@ToWhseLocation", ItemTransfer.ToWhseLocation)
                ,new SqlParameter("@Quantity", ItemTransfer.Quantity)
                ,new SqlParameter("@UOM", ItemTransfer.UOM)
                ,new SqlParameter("@Result", ItemTransfer.Result)
                ,new SqlParameter("@Repairable", ItemTransfer.Repairable)
                ,new SqlParameter("@ReasonCode", ItemTransfer.ReasonCode)
                ,new SqlParameter("@HasPrintOut", ItemTransfer.HasPrintOut)
                ,new SqlParameter("@AutoTransfer", ItemTransfer.AutoTransfer)
                ,new SqlParameter("@Remarks", ItemTransfer.Remarks)
                ,new SqlParameter("@SysCreator", ItemTransfer.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_ItemTransfers_Insert", _parameters));
        }

        public static void Reprint(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CargoId", ItemTransfer.CargoId)
                ,new SqlParameter("@SysModifier", ItemTransfer.SysModifier)
                ,new SqlParameter("@Remarks", ItemTransfer.Remarks)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ItemTransfers_Reprint", _parameters);
        }

        public static void Pick(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CargoId", ItemTransfer.CargoId)
                ,new SqlParameter("@ToWhseCode", ItemTransfer.ToWhseCode)
                ,new SqlParameter("@ToWhseLocation", ItemTransfer.ToWhseLocation)
                ,new SqlParameter("@SysModifier", ItemTransfer.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ItemTransfers_Pick", _parameters);
        }

        public static void Process(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CargoId", ItemTransfer.CargoId)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ItemTransfers_Process", _parameters);
        }

        public static void Cancel(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CargoId", ItemTransfer.CargoId)
                ,new SqlParameter("@SysModifier", ItemTransfer.SysModifier)
                ,new SqlParameter("@Remarks", ItemTransfer.Remarks)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ItemTransfers_Cancel", _parameters);
        }

        public static void Fulfill(IItemTransfer ItemTransfer)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CargoId", ItemTransfer.CargoId)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_ItemTransfers_Fulfill", _parameters);
        }
    }
}


