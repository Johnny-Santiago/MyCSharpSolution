using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using DataAccess;
//using Interface;

namespace Business
{
    public class InvoiceLog
    {
        public static DataTable Retrieve(String InvoiceNumber)
        {
            return InvoiceLogDac.Retrieve(InvoiceNumber).Tables[0];
        }
    }
}