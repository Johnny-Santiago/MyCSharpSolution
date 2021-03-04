using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Collections;
using System.Web.UI.WebControls;

namespace Extensions
{
    public static class DropDownListExtensions
    {
        public static void Sort(this DropDownList ddl) 
        {
            ArrayList textList = new ArrayList();
            ArrayList valueList = new ArrayList();
            foreach (ListItem li in ddl.Items)
            {
                textList.Add(li.Text);
            }
            textList.Sort();

            foreach (object item in textList)
            {
                string value = ddl.Items.FindByText(item.ToString()).Value;
                valueList.Add(value);
            }
            ddl.Items.Clear();

            for (int i = 0; i < textList.Count; i++)
            {
                ListItem objItem = new ListItem(textList[i].ToString(), valueList[i].ToString());
                ddl.Items.Add(objItem);
            }
        }
    }
}