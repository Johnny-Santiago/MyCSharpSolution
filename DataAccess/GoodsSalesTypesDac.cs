///-----------------------------------------------------------------------------------------------------------------
///   Interface:   GoodsSalesTypeDac.cs
///   Author:      Karsito.
///   Create Date: 19-Nov-2015
///   Description: This file is the DataAccess for Sales Type of Goods.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
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
    public class GoodsSalesTypesDac : Configuration
    {
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString,"sp_GoodsSalesType");
        }
    }
}