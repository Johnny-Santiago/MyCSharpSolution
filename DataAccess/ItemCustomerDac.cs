///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  CustomerDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 13-AUG-2015
///   Description: This file is the data access of Customer.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
///-----------------------------------------------------------------------------------------------------------------
///     Revision
///     Changes                             Date Modified                        Modifier
///-----------------------------------------------------------------------------------------------------------------
///     sp_CustomerItems_MasterKanban_SelectAll         May 9, 2016                          Usep Haris N.
///-----------------------------------------------------------------------------------------------------------------
///
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
    public class ItemCustomerDac : Configuration
    {
        public static DataSet Retrieve(string customercode)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CustCode", customercode)
               };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_CustomerItems_MasterKanban_Select", _parameters);
        }

        public static DataSet Retrieve()
        {
            //return SqlHelper.ExecuteDataset(ConnectionString, "sp_CustomerItems_MasterKanban_Select");
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_CustomerItems_MasterKanban_SelectAll");
        }
    }
}
