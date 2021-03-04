///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  ItemTransferLog.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 03-MAR-2015
///   Description: This file is the business rules and logic implementation of ItemTransferLog.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface;
using DataAccess;

namespace Business
{
    public class ItemTransferLog : IItemTransferLog
    {
        private DataTable _Logs;

        public ItemTransferLog()
        {
        }

        public ItemTransferLog(String CargoId)
        {
            _Logs = ItemTransferLogDac.GetLogs(CargoId).Tables[0];
        }

        public DataTable Logs
        {
            get { return _Logs; }
            set { _Logs = value; }
        }
    }
}
