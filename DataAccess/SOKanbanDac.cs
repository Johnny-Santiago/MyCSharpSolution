///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  SOKanbanDetailDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 10-MAR-2016
/// Description: This file is the data access of SOKanban.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------

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
    public class SOKanbanDac : Configuration
    {
        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbans_Select", _parameters);
        }

        public static DataSet Retrieve(ISOKanban SOKanban, Int32 SearchOption) 
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@RefID", SOKanban.RefID)
                ,new SqlParameter("@FromOrderDate", SOKanban.OrderDate)
                ,new SqlParameter("@ToOrderDate", SOKanban.OrderDate2)
                ,new SqlParameter("@FromDeliveryDate", SOKanban.DeliveryDate)
                ,new SqlParameter("@ToDeliveryDate", SOKanban.DeliveryDate2)
                ,new SqlParameter("@OrderBy", SOKanban.OrderedBy)
                ,new SqlParameter("@DeliveryTo", SOKanban.DeliverTo)
                ,new SqlParameter("@YourRef", SOKanban.YourRef)
                ,new SqlParameter("@OrderNo", SOKanban.OrderNo)
                ,new SqlParameter("@Description", SOKanban.Description)
                ,new SqlParameter("@DeliveryOrderNo", SOKanban.DeliveryOrderNo)
                ,new SqlParameter("@ManifestNo", SOKanban.ManifestNo)
                ,new SqlParameter("@RIT", SOKanban.RIT)
                ,new SqlParameter("@ItemCode", SOKanban.ItemCode)
                ,new SqlParameter("@CustomerPartNo", SOKanban.CustomerPartNo)
                ,new SqlParameter("@ItemDescription", SOKanban.ItemDescription)
                ,new SqlParameter("@DealingType", SOKanban.DealingType)
                ,new SqlParameter("@Status", SOKanban.Status)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbans_Search", _parameters);
        }

        public static void RetryImport(Int32 ID) 
        { 
            SqlParameter[] _parameters = {
                new SqlParameter("@ID", ID)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_SOKanbans_RetryImport", _parameters);
        }
    }
}
