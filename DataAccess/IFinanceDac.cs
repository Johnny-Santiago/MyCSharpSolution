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
    public class IFinanceDac : Configuration
    {
        public static Int32 Insert(IFinance financereport)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@GenDate", financereport.datereport) ,
                new SqlParameter("@SysCreator",financereport.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_FixedAsset_SchGenerateDate", _parameters));
        }
    }
}