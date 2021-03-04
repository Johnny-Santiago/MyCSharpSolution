/// Class Name:  SalesOrderDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 16-SEP-2015
/// Description: This file is the data access of Sales Order.
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
    public class SalesOrderDac : Configuration
    {
        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", Id)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SalesOrders_Select", _parameters);
        }

        public static DataSet Retrieve(ISalesOrder SalesOrder, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@FromOrderDate", SalesOrder.OrderDate)
                ,new SqlParameter("@ToOrderDate", SalesOrder.OrderDate2)
                ,new SqlParameter("@OrderNumber", SalesOrder.OrderNumber)
                ,new SqlParameter("@OrderBy", SalesOrder.OrderBy)
                ,new SqlParameter("@InvoiceTo", SalesOrder.InvoiceTo)
                ,new SqlParameter("@DeliveryTo", SalesOrder.DeliveryTo)
                ,new SqlParameter("@YourRef", SalesOrder.YourRef)
                ,new SqlParameter("@SelectionCode", SalesOrder.SelectionCode)
                ,new SqlParameter("@Description", SalesOrder.Description)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SalesOrders_Search", _parameters);
        }

        public static DataSet Retrieve(ISalesOrder SalesOrder)
        {
            SqlParameter[] _Parameters ={
                new SqlParameter("@FromDeliveryDate",SalesOrder.OrderDate)
                ,new SqlParameter("@ToDeliveryDate",SalesOrder.OrderDate2)
                ,new SqlParameter("@DeliveryTo",SalesOrder.CustNo)
                ,new SqlParameter("@DealingType",SalesOrder.DealingType)
                ,new SqlParameter("@Shift",SalesOrder.Shift)
                //,new SqlParameter("@FromTime",SalesOrder.FromTime)
                //,new SqlParameter("@ToTime",SalesOrder.ToTime)
                ,new SqlParameter("@ItemCode",SalesOrder.IchItemCode)
                                       };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SalesOrders_SearchDeliveryOrdersWithShift", _Parameters);
        }

        public static DataSet UpdateDOAndManifest(ISalesOrder SalesOrder)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", SalesOrder.Id)
                ,new SqlParameter("@OrderNumber", SalesOrder.OrderNumber)
                ,new SqlParameter("@DNOrder", SalesOrder.DNOrder)
                ,new SqlParameter("@Manifest", SalesOrder.Manifest)
                ,new SqlParameter("@RIT", SalesOrder.RIT)
                ,new SqlParameter("@SysCreator", SalesOrder.SysCreator)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SalesOrders_UpdateDOAndManifest", _parameters);
        }
    }
}