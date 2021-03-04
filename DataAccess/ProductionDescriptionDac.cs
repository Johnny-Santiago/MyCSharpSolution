///-----------------------------------------------------------------------------------------------------------------
///   Created by Karsito           Dec 07, 2015                ProductionDescriptionDac.cs
///                                                            Use for Reporting Production or Itemtransfer
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
    public class ProductionDescriptionDac : Configuration
    {
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemtransfersDescription");
        }
        public static DataSet Retrieve(int ResultId)
        {
            SqlParameter[] _Parameters = {
                           new SqlParameter ("@Id",ResultId)
                                         };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_GetProductionDescription",_Parameters);
        }
    }
}