///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  AuditLogDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 03-MAY-2015
///   Description: This file is the data access of AuditLog.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Common;
using System.Data;
using System.Data.SqlClient;

namespace DataAccess
{
    public class AuditLogDac : Configuration
    {
        public static DataSet Retrieve(Guid? SysGuid)
        {
            SqlParameter[] parameterValues = new SqlParameter[] { new SqlParameter("@SysGuid", SysGuid) };
            return SqlHelper.ExecuteDataset(Configuration.ConnectionString, "sp_AuditLogs_Retrieve", parameterValues);
        }
    }
}