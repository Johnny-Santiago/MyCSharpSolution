using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using Interface;
using System.Linq;
using System.Text;
using Common;

namespace DataAccess
{
    public class InvoiceTaxSerialDac : Configuration
    {
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTaxSerial_Select");
        }

        public static DataSet RetrieveAll()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTaxSerial_SelectAll");
        }

        public static Int32 Insert(IInvoiceTaxSerial invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@StartSerialNumber", invoice.StartSerialNumber)             
                ,new SqlParameter("@EndSerialNumber", invoice.EndSerialNumber)             
                ,new SqlParameter("@LastSerialNumber", invoice.LastSerialNumber)             
                ,new SqlParameter("@SysCreator", invoice.SysCreator)
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTaxSerial_Insert", _parameters);
        }

        public static Int32 Update(IInvoiceTaxSerial invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@StartSerialNumber", invoice.StartSerialNumber)             
                ,new SqlParameter("@EndSerialNumber", invoice.EndSerialNumber) 
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTaxSerial_Update", _parameters);
        }

        public static Int32 UpdateID(IInvoiceTaxSerial invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@StartSerialNumber", invoice.StartSerialNumber)             
                ,new SqlParameter("@EndSerialNumber", invoice.EndSerialNumber) 
                ,new SqlParameter("@Id", invoice.Id) 
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTaxSerial_UpdateID", _parameters);
        }
        public static Int32 DeleteID(IInvoiceTaxSerial invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", invoice.Id) 
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTaxSerial_DeleteID", _parameters);
        }

        public static Int32 UpdateLastSerial(IInvoiceTaxSerial invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@LastSerialNumber", invoice.LastSerialNumber)             
                ,new SqlParameter("@SysCreator", invoice.SysCreator)
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTaxSerial_UpdateLastSerial", _parameters);
        }
    }
}