///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SelectionCodeDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 16-SEP-2015
///   Description: This file is the data access of Selection Code.
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
    public class SelectionCodeDac : Configuration
    {
        public static DataSet Retrieve(ISelectionCode SelectionCode, Int32 SearchOption)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@SelectionCode", SelectionCode.selectionCode)
                ,new SqlParameter("@Description", SelectionCode.Description)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SelectionCodes_Search", _parameters);
        }
    }
}