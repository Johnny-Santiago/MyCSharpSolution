using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Web;

namespace Extensions
{
    public static class DataTableExtensions
    {
        public static bool HasRows(this DataTable dt)
        {
            return dt.Rows.Count > 0;
        }

        public static DataTable Filter(this DataTable dt, string filter)
        {
            DataRow[] drResults = dt.Select(filter);
            DataTable dtResult = dt.Clone();

            foreach (DataRow dr in drResults)
            {
                object[] row = dr.ItemArray;
                dtResult.Rows.Add(row);
            }

            return dtResult;
        }

        public static DataTable Delete(this DataTable dt, string filter)
        {
            dt.Select(filter).Delete();
            return dt;
        }

        public static String ToXml(this DataTable dt)
        {
            MemoryStream s = new MemoryStream();
            dt.WriteXml(s, true);
            s.Seek(0, SeekOrigin.Begin);
            StreamReader sr = new StreamReader(s);
            string xml = sr.ReadToEnd();
            sr.Close();
            s.Close();
            return xml;
        }

        public static DataTable ConvertToDataTable<TSource>(this IEnumerable<TSource> source)
        {
            var props = typeof(TSource).GetProperties();

            var dt = new DataTable();
            dt.Columns.AddRange(
              props.Select(p => new DataColumn(p.Name, p.PropertyType)).ToArray()
            );

            source.ToList().ForEach(
              i => dt.Rows.Add(props.Select(p => p.GetValue(i, null)).ToArray())
            );

            return dt;
        }

        public static void AddRecordSelector(this DataTable dt)
        {
            DataColumn CheckedItem = new DataColumn();

            CheckedItem.DataType = Type.GetType("System.Boolean");
            CheckedItem.ColumnName = "CheckedItem";
            CheckedItem.ReadOnly = false;
            CheckedItem.Unique = false;

            dt.Columns.Add(CheckedItem);
            CheckedItem.SetOrdinal(0);

            foreach (DataRow row in dt.Rows)
            {
                row["CheckedItem"] = false;
            }
        }
    }
}