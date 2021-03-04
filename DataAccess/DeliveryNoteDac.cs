///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  DeliveryNoteDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-JAN-2016
///   Description: This file is the data access of delivery notes.
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
    public class DeliveryNoteDac : Configuration
    {
        public static DataSet Retrieve(IDeliveryNote DeliveryNote, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ID", DeliveryNote.ID)
                ,new SqlParameter("@CustomerCode", DeliveryNote.CustomerCode)
                ,new SqlParameter("@CustomerName", DeliveryNote.CustomerName)
                ,new SqlParameter("@OrderNo", DeliveryNote.OrderNo)
                ,new SqlParameter("@YourRef", DeliveryNote.YourRef)
                ,new SqlParameter("@DeliveryNoteNo", DeliveryNote.DeliveryNoteNo)
                ,new SqlParameter("@DeliveryNoteDate", DeliveryNote.FulfilmentDate) 
                ,new SqlParameter("@ToFulfilmentDate", DeliveryNote.FulfilmentDate2)
                ,new SqlParameter("@FromOrderDate", DeliveryNote.OrderDate)
                ,new SqlParameter("@ToOrderDate", DeliveryNote.OrderDate2)
                ,new SqlParameter("@Description", DeliveryNote.Description)
                ,new SqlParameter("@InvoiceToCode", DeliveryNote.InvoiceToCode)
                ,new SqlParameter("@DeliveryToCode", DeliveryNote.DeliveryToCode)
                ,new SqlParameter("@WhseCode", DeliveryNote.Warehouse)
                ,new SqlParameter("@DeliveryOrderNo", DeliveryNote.DeliveryOrderNo)
                ,new SqlParameter("@ManifestNo", DeliveryNote.ManifestNo)
                ,new SqlParameter("@DealingType", DeliveryNote.DealingType)
                ,new SqlParameter("@SelectionCode", DeliveryNote.SelectionCode)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

           
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_DeliveryNotes_Search", _parameters); 
        }
    }
}