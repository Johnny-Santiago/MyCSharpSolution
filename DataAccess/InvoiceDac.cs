/// Class Name:  InvoiceDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 09-OCT-2015
/// Description: This file is the data access of Invoice.
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
    public class InvoiceDac : Configuration
    {
        public static DataSet GetGroup(String DeliveryNoteNumber) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@DeliveryNoteNumber", DeliveryNoteNumber)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_GetGroup", _parameters);
        }

        public static DataSet GetGroupByInvoiceNo(String InvoiceNumber) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@InvoiceNumber", InvoiceNumber)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_GetGroupByInvoiceNo", _parameters);
        }

        public static DataSet GetDeliveryDetails(String DeliveryNoteNumber, Boolean IncludeWithDN) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@DeliveryNoteNumber", DeliveryNoteNumber)
                ,new SqlParameter("@IncludeWithDN", IncludeWithDN)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_GetDeliveryDetails", _parameters);
        }

        public static void UpdateDeliveryNoteNumber(Iinvoice invoice) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ID", invoice.Id)
                ,new SqlParameter("@InvoiceNumber", invoice.InvoiceNumber)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@DeliveryNoteNumber", invoice.DeliveryNoteNumber)
                ,new SqlParameter("@Processed", invoice.Processed)
                ,new SqlParameter("@SysCreator", invoice.SysCreator)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Invoices_UpdateDeliveryNoteNumber", _parameters); 
        }

        public static DataSet CheckDOReport(String InvoiceNumber, String CustomerFile)  
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@InvoiceNumber", InvoiceNumber)
                ,new SqlParameter("@CustomerFile", CustomerFile) 
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_CheckDOReport", _parameters); 
        }

        public static DataSet GetDOReportDetails(String InvoiceNumber, String Manifest, String CustomerPartNumber)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@InvoiceNumber", InvoiceNumber)
                ,new SqlParameter("@Manifest", Manifest) 
                ,new SqlParameter("@CustomerPartNumber", CustomerPartNumber) 
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_CheckDOReportDetails", _parameters);
        }

        public static DataSet RetrieveDOReport(String InvoiceNumber)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@OurRef", InvoiceNumber)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_RetrieveDOReport", _parameters);
        }
    }
}