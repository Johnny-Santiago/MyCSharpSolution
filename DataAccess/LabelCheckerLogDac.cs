using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Common;
using System.Data;
using System.Data.SqlClient;
using Interface;

namespace DataAccess
{
    public class LabelCheckerLogDac : Configuration
    {
        public static DataSet Retreive(ILabelCheckerLog labelLog)
        {
            SqlParameter[] _parameter = {
                                new SqlParameter("@FromDate",labelLog.ScanStartDate),
                                new SqlParameter("@ToDate",labelLog.ScanToDate),
                                new SqlParameter("@CustId",labelLog.CustomerCode),
                                new SqlParameter("@ShipToId",labelLog.ShipTo),
                                new SqlParameter("@ItemCode",labelLog.ItemCode),
                                new SqlParameter("@CustItemCode",labelLog.CustomerItem),
                                new SqlParameter("@Shift",labelLog.Shift),
                                new SqlParameter("@Status",labelLog.Status)
                            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_LabelCheckerLogReport", _parameter);
        }
    }
}