///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  ItemTransferLogDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 03-MAR-2015
///   Description: This file is the data access of ItemTransferLogDac.
///   Version:     1
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
    public class ItemTransferLogDac : Configuration
    {
        public static DataSet GetLogs(String CargoId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@CargoId", CargoId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemTransferLogs_Select", _parameters);
        }
    }
}
