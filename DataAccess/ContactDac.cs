///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  ContactDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 14-AUG-2015
///   Description: This file is the data access of Contacts.
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
    public class ContactDac : Configuration
    {
        public static DataSet Retrieve(IContact Contact, Int32 SearchOption) 
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CustomerCode", Contact.CustomerCode)
                ,new SqlParameter("@Main", Contact.Main)
                ,new SqlParameter("@SearchOption", SearchOption)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Contacts_Search", _parameters);
        }
    }
}
