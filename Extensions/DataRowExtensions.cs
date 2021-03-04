using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace Extensions
{
    public static class DataRowExtensions
    {
        public static void Delete(this IEnumerable<DataRow> rows)
        {
            foreach (var row in rows)
                row.Delete();
        }
    }
}