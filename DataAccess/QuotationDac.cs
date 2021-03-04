///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  QuotationDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 26-JUL-2016
/// Description: This file is the data access of Quotation.
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
    public class QuotationDac : Configuration
    {
        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Quotations_Select", _parameters);
        }
    }
}