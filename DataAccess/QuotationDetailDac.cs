///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  QuotationDetailDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 26-JUL-2016
/// Description: This file is the data access of QuotationDetail.
/// Version:     1
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
    public class QuotationDetailDac : Configuration
    {
        public static DataSet Retrieve(String CustomerCode, DateTime ForecastDate)
        {
            SqlParameter[] _parameters = { 
                new SqlParameter("@CustomerCode", CustomerCode)
                ,new SqlParameter("@CustomerName", ForecastDate)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_QuotationDetails_Select", _parameters);
        }
    }
}