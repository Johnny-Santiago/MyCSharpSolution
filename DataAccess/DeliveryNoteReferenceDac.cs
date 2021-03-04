///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  DeliveryNoteReferenceDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-JAN-2016
///   Description: This file is the data access of Delivery Note References.
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
    public class DeliveryNoteReferenceDac : Configuration
    {
        public static DataSet Retrieve(IDeliveryNoteReference DeliveryNoteReference, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@ID", DeliveryNoteReference.ID)
                ,new SqlParameter("@DeliveryNoteNo", DeliveryNoteReference.DeliveryNoteNo)
                ,new SqlParameter("@DeliveryNoteDate", DeliveryNoteReference.DeliveryNoteDate)
                ,new SqlParameter("@Description", DeliveryNoteReference.Description)
                ,new SqlParameter("@SearchOption", SearchOption)
            };


            return SqlHelper.ExecuteDataset(ConnectionString, "sp_DeliveryNoteReferences_Search", _parameters);
        }
    }
}