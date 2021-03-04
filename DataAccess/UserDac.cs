///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  UserDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 24-FEB-2015
///   Description: This file is the data access of User.
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
    public class UserDac : Configuration
    {
        public static DataSet Retrieve(String UserID)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserID)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_BarcodeUsers_SelectByUserID", _parameters);
        }

        public static DataSet RetrieveAll()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_BarcodeUsers_SelectAll");
        }
    }
}
