///-----------------------------------------------------------------------------------------------------------------
///   Created by Karsito           Mar 13, 2016                SalesPriceAgreement.cs
///                                                            Use for Reporting Sales Price Agreements
///                                                            
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using Common;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Interface;

namespace DataAccess
{
    public class SalesPriceAgreementDac : Configuration
    {
      public static DataSet Retrieve(ISalesPriceAgreement slspriceAgreement)
        {
            SqlParameter[] _Parameters = {
                           new SqlParameter ("@Date1",slspriceAgreement.OrderDate1),
                           new SqlParameter ("@Date2",slspriceAgreement.OrderDate2),
                           new SqlParameter ("@CustId",slspriceAgreement.debnr),
                           new SqlParameter ("@ItemCode",slspriceAgreement.ItemCodeCust)
                                         };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SalesPriceAgreement_Get", _Parameters);
        }
      public static DataSet Retrieve(DateTime date1,DateTime date2)
      {
          SqlParameter[] _Parameters = {
                           new SqlParameter ("@Date1",date1),
                           new SqlParameter ("@Date2",date2)
                                         };
          return SqlHelper.ExecuteDataset(ConnectionString, "sp_SalesPriceAgreement_Get", _Parameters);
      }
    }
}