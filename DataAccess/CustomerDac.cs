///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  CustomerDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 13-AUG-2015
///   Description: This file is the data access of Customer.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
///     Revision
///     Changes                                 Date Modified                        Modifier
///-----------------------------------------------------------------------------------------------------------------
///     Add Methode RetrieveInvoiceTax()        May 3, 2016                          Usep Haris N.
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
    public class CustomerDac : Configuration
    {
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Customers_SelectAll");
        }

        public static DataSet RetrieveKanban()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Customer_MasterKanban");
        }

        public static DataSet RetrieveInvoiceTax()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Customer_InvoiceTax");
        }

        public static DataSet Retrieve(ICustomer Customer, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Alias", Customer.Alias)
                ,new SqlParameter("@CustomerName", Customer.CustomerName)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Customers_Search", _parameters);
        }
    }
}
