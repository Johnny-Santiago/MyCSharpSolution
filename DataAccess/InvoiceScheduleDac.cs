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
    public class InvoiceListDac : Configuration
    {
        public static DataSet Retrieve(IInvoice invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNo)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@CustomerName", invoice.CustomerName)
                ,new SqlParameter("@CustomerPO", invoice.CustomerPO)
                ,new SqlParameter("@FromInvoiceDate", invoice.FromInvoiceDate)
                ,new SqlParameter("@ToTransactionDate", invoice.ToInvoiceDate)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_Lists", _parameters);
        }

        public static DataSet RetrieveSchedules(IInvoice invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNo)
                ,new SqlParameter("@CustomerCode", invoice.CustomerCode)
                ,new SqlParameter("@CustomerName", invoice.CustomerName)
                ,new SqlParameter("@CustomerPO", invoice.CustomerPO)
                ,new SqlParameter("@DateSent", invoice.DateSent)
                ,new SqlParameter("@DateSent2", invoice.DateSent2)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Invoices_ScheduleLists", _parameters);
        }
    }
  
    public class InvoiceScheduleDac : Configuration
    {
        public static Int32 Insert(IInvoiceSchedule invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNo)
                ,new SqlParameter("@DateSent", invoice.DateSent)
                ,new SqlParameter("@SysCreator", invoice.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Invoices_ScheduleInsert", _parameters));
        }

        public static Int32 Update(IInvoiceSchedule invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNo)
                ,new SqlParameter("@DateSent", invoice.DateSent)
                ,new SqlParameter("@SysModifier", invoice.SysModifier)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Invoices_ScheduleUpdate", _parameters));
        }

        public static Int32 Delete(IInvoiceSchedule invoice)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@InvoiceNo", invoice.InvoiceNo)
                ,new SqlParameter("@SysModifier", invoice.SysModifier)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Invoices_ScheduleDelete", _parameters));
        }
    }
}
