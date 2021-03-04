///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  AuditLog.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 03-MAY-2015
///   Description: This file is the business rules and logic implementation of AuditLog.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using Interface;

namespace Business
{
    public class AuditLog : IAuditLog  
    {
        private DataTable _TransactionLog;

        public AuditLog()
        {
        }

        public AuditLog(Guid? SysGuid)
        {
            this._TransactionLog = AuditLogDac.Retrieve(SysGuid).Tables[0];
        }

        public DataTable TransactionLog
        {
            get
            {
                return this._TransactionLog;
            }
            set
            {
                this._TransactionLog = value;
            }
        }
    }
}
