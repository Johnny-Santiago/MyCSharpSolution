///-----------------------------------------------------------------------------------------------------------------
///   Interface:   ShiftDac.cs
///   Author:      Karsito.
///   Create Date: 18-Nov-2015
///   Description: This file is the DataAccess for Shift.
///   Version:     1
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
    public class ShiftDac : Configuration
    {
        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString,"sp_GetShiftNo");
        }
    }
}