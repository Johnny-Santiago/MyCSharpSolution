///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SupplierDac.cs
///   Author:      Karsito
///   Create Date: 23-MAR-2016
///   Description: This file is the data access of Supplier.
///   Version:     0
///-----------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public class SupplierDac : Configuration
    {
        public static DataSet Retrieve(ISupplier supplier, Int32 SearchOption)
        {
           SqlParameter[]_Parameters={
                          new SqlParameter("@Alias",supplier.Alias),
                          new SqlParameter("@SupplierName",supplier.SupplierName),
                          new SqlParameter("@SearchOption",SearchOption)
                                      };
           return SqlHelper.ExecuteDataset(ConnectionString, "[sp_Suppliers_Search]", _Parameters);
        }
    }
}