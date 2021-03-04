///-----------------------------------------------------------------------------------------------------------------
///   Created by Karsito           Mar 21, 2016                PurchasePriceAgreement.cs
///                                                            Use for Reporting Purchase Price Agreements
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
    public class PurchasePriceAgreementDac : Configuration
    {
        public static DataSet Retrieve(IPurchasePriceAgreement purcpriceagreement)
        {
            SqlParameter[] _Parameters = {
                           new SqlParameter ("@Date1",purcpriceagreement.OrderDate1),
                           new SqlParameter ("@Date2",purcpriceagreement.OrderDate2),
                           new SqlParameter ("@SupId",purcpriceagreement.crdnr),
                           new SqlParameter ("@ItemCode",purcpriceagreement.ItemCode)
                                         };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_purchasePriceAgreement_Get", _Parameters);
        }
    }
}