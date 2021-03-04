///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SelectionCodeDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 16-SEP-2015
///   Description: This file is the data access of dealing types.
///   Version:     1
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
    public class DealingTypeDac : Configuration
    {
        public static DataSet Retrieve(IDealingType DealingType, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@AccountCategoryCode", DealingType.AccountCategoryCode)
                ,new SqlParameter("@Description", DealingType.Description)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_DealingTypes_Search", _parameters);
        }
    }
}