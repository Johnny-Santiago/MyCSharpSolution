///-----------------------------------------------------------------------------------------------------------------
///  Karsito                    07 Dec 2015 
///                             Reporting data in Itemtransfer process
///                             Inherits from ItemTransfer
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
    public class ItemTransferReportDac : Configuration
    {   

      public static DataSet Retrieve(IItemTransferReport ItemTransfer, Int32 SearchOption)
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

  
        public static DataSet Retrieve(IItemTransferReport ItemTransfer)
        {
            SqlParameter[] _Parameters = {
                new SqlParameter ("@FromCreatedDate",ItemTransfer.SysCreated)  
                ,new SqlParameter ("@ToCreatedDate",ItemTransfer.SysCreated2) 
                ,new SqlParameter ("@WhseCode",ItemTransfer.FromWhseCode)
                ,new SqlParameter ("@WhseLocation",ItemTransfer.FromWhseLocation)
                ,new SqlParameter("@Shift",ItemTransfer.ShiftName)
                ,new SqlParameter ("@ItemCode",ItemTransfer.ItemCode)
                ,new SqlParameter("@ResultId",ItemTransfer.Result)  
                ,new SqlParameter("@TransId",ItemTransfer.TransId)
                                         };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransfers_SearchWithShift", _Parameters);
        }
        
    }
}


