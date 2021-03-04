///-----------------------------------------------------------------------------------------------------------------
///   Interface:   Shift.cs
///   Author:      Karsito.
///   Create Date: 18-Nov-2015
///   Description: This file is the Business for Shift.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface;
using DataAccess;
using Extensions;


namespace Business
{
    public class Shift : IShift
    {
        private string _ShiftNo;
        private string _Time;

        public Shift()
        {
 
        }
        private void InitializeProperties(DataRow row)
        {
            _ShiftNo = row["Shift"] == DBNull.Value ? string.Empty : Convert.ToString(row["Shift"]);
            _Time = row["Time"] == DBNull.Value ? string.Empty : Convert.ToString(row["Time"]);
        }
        public String ShiftNo
        {
            get { return _ShiftNo; }
            set { _ShiftNo = value; }
        }
        public String Time
        {
            get { return _Time; }
            set { _Time = value; }
        }
        public DataTable Retrieve()
        {
            return ShiftDac.Retrieve().Tables[0];
        }
        
    }
}