///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  SOKanbanDetailDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 11-FEB-2016
/// Description: This file is the data access of SOKanbanDetail.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public class SOKanbanDetailDac : Configuration
    {
        public static DataSet Retrieve(String YourRef) 
        {
            SqlParameter[] _parameters = { new SqlParameter("@YourRef", YourRef) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanDetails_Select", _parameters); 
        }
    }
}