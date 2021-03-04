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
    public class InvoiceLogDac : Configuration
    {
        public static DataSet Retrieve(String InvoiceNumber) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@InvoiceNumber", InvoiceNumber)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_InvoiceLogs_GetHistory", _parameters);
        }
    }
}