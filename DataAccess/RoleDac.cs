///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  RoleDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 24-FEB-2015
///   Description: This file is the data access of Role.
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
    public class RoleDac : Configuration
    {
        public static DataSet GetUserRoles(String UserId, String ProgramId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
                ,new SqlParameter("@ProgramID", ProgramId) 
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_BarcodeUsers_GetUserRoles", _parameters);
        }

        public static DataSet GetUserRoles(String UserId)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_BarcodeUsers_GetAllUserRoles", _parameters);
        }

        public static void GrantUserRoles(String UserId, String ProgramId, String Rights)  
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@UserId", UserId)
                ,new SqlParameter("@ProgramId", ProgramId)
                ,new SqlParameter("@Rights", Rights)
            };

            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_BarcodeUsers_GrantUserRoles", _parameters);
        }
    }
}
