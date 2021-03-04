///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  DeliveryNoteTransmittalDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-JAN-2016
///   Description: This file is the data access of delivery note transmittals.
///   Version:     1
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
    public class DeliveryNoteTransmittalDac : Configuration
    {
        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@Id", Id)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_DNTransmittals_Select", _parameters);
        }

        public static DataSet Retrieve(IDeliveryNoteTransmittal DeliveryNoteTransmittal, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ID", DeliveryNoteTransmittal.ID)
                ,new SqlParameter("@Debtor", DeliveryNoteTransmittal.Debtor)
                ,new SqlParameter("@Name", DeliveryNoteTransmittal.Name)
                ,new SqlParameter("@OrderNo", DeliveryNoteTransmittal.OrderNo)
                ,new SqlParameter("@YourRef", DeliveryNoteTransmittal.YourRef)
                ,new SqlParameter("@DeliveryNoteNo", DeliveryNoteTransmittal.DeliveryNoteNo)
                ,new SqlParameter("@FromFulfilmentDate", DeliveryNoteTransmittal.FulfilmentDate) 
                ,new SqlParameter("@ToFulfilmentDate", DeliveryNoteTransmittal.FulfilmentDate2)
                ,new SqlParameter("@FromOderDate", DeliveryNoteTransmittal.OrderDate)
                ,new SqlParameter("@ToOrderDate", DeliveryNoteTransmittal.OrderDate2)
                ,new SqlParameter("@Description", DeliveryNoteTransmittal.Description)
                ,new SqlParameter("@InvoiceToCode", DeliveryNoteTransmittal.InvoiceToCode)
                ,new SqlParameter("@DeliveryToCode", DeliveryNoteTransmittal.DeliveryToCode)
                ,new SqlParameter("@WhseCode", DeliveryNoteTransmittal.Warehouse)
                ,new SqlParameter("@DeliveryOrderNo", DeliveryNoteTransmittal.DeliveryOrderNo)
                ,new SqlParameter("@ManifestNo", DeliveryNoteTransmittal.ManifestNo)
                ,new SqlParameter("@DealingType", DeliveryNoteTransmittal.DealingType)
                ,new SqlParameter("@SelectionCode", DeliveryNoteTransmittal.SelectionCode)
                ,new SqlParameter("@FromReceivedDate", DeliveryNoteTransmittal.ReceivedDate)
                ,new SqlParameter("@ToReceivedDate", DeliveryNoteTransmittal.ReceivedDate2)
                ,new SqlParameter("@Remarks", DeliveryNoteTransmittal.Remarks)
                ,new SqlParameter("@FromSysCreated", DeliveryNoteTransmittal.SysCreated)
                ,new SqlParameter("@ToSysCreated", DeliveryNoteTransmittal.SysCreated2)
                ,new SqlParameter("@SysCreator", DeliveryNoteTransmittal.SysCreator)
                ,new SqlParameter("@FromSysModified", DeliveryNoteTransmittal.SysModified)
                ,new SqlParameter("@ToSysModified", DeliveryNoteTransmittal.SysModified2)
                ,new SqlParameter("@SysModifier", DeliveryNoteTransmittal.SysModifier)
                ,new SqlParameter("@SearchOption", SearchOption)
            };


            return SqlHelper.ExecuteDataset(ConnectionString, "sp_DNTransmittals_Search", _parameters);
        }

        public static Int32 Insert(IDeliveryNoteTransmittal DeliveryNoteTransmittal)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Debtor", DeliveryNoteTransmittal.Debtor)
                ,new SqlParameter("@Name", DeliveryNoteTransmittal.Name)
                ,new SqlParameter("@OrderNo", DeliveryNoteTransmittal.OrderNo)
                ,new SqlParameter("@YourRef", DeliveryNoteTransmittal.YourRef)
                ,new SqlParameter("@DeliveryNoteNo", DeliveryNoteTransmittal.DeliveryNoteNo)
                ,new SqlParameter("@FulfilmentDate", DeliveryNoteTransmittal.FulfilmentDate)
                ,new SqlParameter("@OderDate", DeliveryNoteTransmittal.OrderDate)
                ,new SqlParameter("@Description", DeliveryNoteTransmittal.Description)
                ,new SqlParameter("@InvoiceToCode", DeliveryNoteTransmittal.InvoiceToCode)
                ,new SqlParameter("@DeliveryToCode", DeliveryNoteTransmittal.DeliveryToCode)
                ,new SqlParameter("@WhseCode", DeliveryNoteTransmittal.Warehouse)
                ,new SqlParameter("@DeliveryOrderNo", DeliveryNoteTransmittal.DeliveryOrderNo)
                ,new SqlParameter("@ManifestNo", DeliveryNoteTransmittal.ManifestNo)
                ,new SqlParameter("@DealingType", DeliveryNoteTransmittal.DealingType)
                ,new SqlParameter("@SelectionCode", DeliveryNoteTransmittal.SelectionCode)
                ,new SqlParameter("@ReceivedDate", DeliveryNoteTransmittal.ReceivedDate)
                ,new SqlParameter("@Remarks", DeliveryNoteTransmittal.Remarks)
                ,new SqlParameter("@Syscreator", DeliveryNoteTransmittal.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_DNTransmittals_Insert", _parameters));
        }

        public static void Update(IDeliveryNoteTransmittal DeliveryNoteTransmittal)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ID", DeliveryNoteTransmittal.ID)
                ,new SqlParameter("@ReceivedDate", DeliveryNoteTransmittal.ReceivedDate)
                ,new SqlParameter("@Reason", DeliveryNoteTransmittal.Reason)
                ,new SqlParameter("@SysModifier", DeliveryNoteTransmittal.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_DNTransmittals_Update", _parameters);
        }

        public static void Delete(IDeliveryNoteTransmittal DeliveryNoteTransmittal)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@DeliveryNoteNo", DeliveryNoteTransmittal.DeliveryNoteNo)
                ,new SqlParameter("@Reason", DeliveryNoteTransmittal.Reason)
                ,new SqlParameter("@SysModifier", DeliveryNoteTransmittal.SysModifier)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_DNTransmittals_Delete", _parameters);
        }
    }
}