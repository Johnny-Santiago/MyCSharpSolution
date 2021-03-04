/// Class Name:  InvoiceDOManifestLogDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 21-SEP-2015
/// Description: This file is the data access of Invoice DO No. and Manifest No. Logs.
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
    public class InvoiceDOManifestLogDac : Configuration
    {
        public static DataSet Retrieve(String DeliveryNote) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@DeliveryNote", DeliveryNote)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoiceDOManifest_Select", _parameters);
        }

        public static void Insert(IInvoiceDOManifestLog InvoiceDOManifestLog)  
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@RecID", InvoiceDOManifestLog.RecID)
                ,new SqlParameter("@DeliveryNote", InvoiceDOManifestLog.DeliveryNote)
                ,new SqlParameter("@ColumnName", InvoiceDOManifestLog.ColumnName)
                ,new SqlParameter("@OldValue", InvoiceDOManifestLog.OldValue)
                ,new SqlParameter("@NewValue", InvoiceDOManifestLog.NewValue)
                ,new SqlParameter("@SysCreator", InvoiceDOManifestLog.SysCreator)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoiceDOManifestLogs_Insert", _parameters);
        }

        public static void Update(IInvoiceDOManifestLog InvoiceDOManifestLog)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@DeliveryNote", InvoiceDOManifestLog.DeliveryNote)
                ,new SqlParameter("@DNOrder", InvoiceDOManifestLog.DNOrder)
                ,new SqlParameter("@Manifest", InvoiceDOManifestLog.Manifest)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoiceDOManifest_Update", _parameters);
        }
    }
}