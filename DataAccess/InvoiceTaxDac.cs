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
    public class InvoiceTaxDac : Configuration
    {
        public static DataSet RetrieveList(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNumber)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@CustomerName", invoice.CustomerName)
                ,new SqlParameter("@FromInvoiceDate", invoice.FromInvoiceDate)
                ,new SqlParameter("@ToInvoiceDate", invoice.ToInvoiceDate)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTax_HeaderLists", _parameters);
        }

        public static DataSet RetrieveAll(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNumber)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@CustomerName", invoice.CustomerName)
                ,new SqlParameter("@FromInvoiceDate", invoice.FromInvoiceDate)
                ,new SqlParameter("@ToInvoiceDate", invoice.ToInvoiceDate)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTax_Lists", _parameters);
        }

        public static DataSet RetrieveAllocated(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNumber)
                ,new SqlParameter("@SerialNumber", invoice.SerialNumber)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@CustomerName", invoice.CustomerName)
                ,new SqlParameter("@FromInvoiceDate", invoice.FromInvoiceDate)
                ,new SqlParameter("@ToInvoiceDate", invoice.ToInvoiceDate)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTax_Lists_Export", _parameters);
        }

        public static DataSet RetrieveDetail(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNumber)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTax_DetailLists", _parameters);
        }

        public static DataSet Retrieve(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNumber", invoice.InvoiceNumber)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@SerialNumber", invoice.SerialNumber)
                ,new SqlParameter("@CustomerName", invoice.CustomerName)
                ,new SqlParameter("@FromInvoiceDate", invoice.FromInvoiceDate)
                ,new SqlParameter("@ToInvoiceDate", invoice.ToInvoiceDate)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTax_Allocated_Select", _parameters);
        }

        public static DataSet RetrieveUnallocated()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoicesTax_Allocated_SelectUnallocated");
        }

        
        public static Int32 Allocate(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNumber", invoice.InvoiceNumber)
                ,new SqlParameter("@SerialNumber", invoice.SerialNumber)             
                ,new SqlParameter("@InvoiceDate", invoice.InvoiceDate)
                ,new SqlParameter("@SysCreator", invoice.SysCreator)
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTax_Allocate", _parameters);
        }

        public static Int32 Update(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNumber", invoice.InvoiceNumber)
                ,new SqlParameter("@SerialNumber", invoice.SerialNumber)             
                ,new SqlParameter("@InvoiceDate", invoice.InvoiceDate)
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTax_Allocated_Update", _parameters);
        }

        public static Int32 UpdateID(IInvoiceTax invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNumber", invoice.InvoiceNumber)
                ,new SqlParameter("@Id", invoice.Id)             
                ,new SqlParameter("@InvoiceDate", invoice.InvoiceDate)
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_InvoicesTax_Allocated_Update_ByID", _parameters);
        }


    }
}