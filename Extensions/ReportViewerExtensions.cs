using Microsoft.Reporting.WebForms;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Runtime.CompilerServices;
using System.Web.UI;

namespace Extensions
{
    public static class ReportViewerExtensions
    {
        public static ReportParameter[] GetCurrentParameters(this ReportViewer viewer)
        {
            Control parent = FindParametersArea(viewer);
            List<ReportParameter> list = new List<ReportParameter>();
            FindParameters(parent, list);
            return list.ToArray();
        }

        private static void FindParameters(Control parent, List<ReportParameter> params1)
        {
            Type type = Assembly.GetAssembly(typeof(ReportViewer)).GetType("Microsoft.Reporting.WebForms.BaseParameterInputControl");
            foreach (Control control in parent.Controls)
            {
                if (type.IsAssignableFrom(control.GetType()))
                {
                    ReportParameterInfo propertyValue = (ReportParameterInfo)GetPropertyValue(control, "ReportParameter");
                    if (propertyValue == null)
                    {
                        continue;
                    }
                    string[] strArray = (string[])GetPropertyValue(control, "CurrentValue");
                    if ((strArray != null) && (strArray.Length > 0))
                    {
                        ReportParameter item = new ReportParameter();

                        ////item.

                        //item.Name(propertyValue.get_Name());
                        //item.get_Values().AddRange(strArray);
                        //params1.Add(item);
                        ////param = new ReportParameter();
                        ////param.Name = paramInfo.Name;
                        ////param.Values.AddRange(paramValues);
                        ////params1.Add(param);

                        item.Name = propertyValue.Name;
                        item.Values.AddRange(strArray);
                        params1.Add(item);
                    }
                }
                FindParameters(control, params1);
            }
        }

        private static Control FindParametersArea(ReportViewer viewer)
        {
            foreach (Control control in viewer.Controls)
            {
                if (control.GetType().Name == "ParametersArea")
                {
                    return control;
                }
            }
            return null;
        }

        

        private static object GetPropertyValue(object target, string propertyName)
        {
            return target.GetType().GetProperty(propertyName, BindingFlags.NonPublic | BindingFlags.Public | BindingFlags.Instance | BindingFlags.IgnoreCase).GetValue(target, null);
        }
    }
}