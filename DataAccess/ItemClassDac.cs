///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  ItemClassDac.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 04-MAR-2015
///   Description: This file is the data access of ItemClass.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using System.Data.SqlClient;
using Common;

namespace DataAccess
{
    public class ItemClassDac : Configuration
    {
        public static DataSet RetrieveClassifications() 
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllClassifications");
        }

        public static DataSet RetrieveFamilies()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllFamilies");
        }

        public static DataSet RetrieveCategories()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllCategories");
        }

        public static DataSet RetrievePositions()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllPositions");
        }

        public static DataSet RetrieveTypes()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllTypes");
        }

        public static DataSet RetrieveIdentities()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllIdentities");
        }

        public static DataSet RetrieveModels()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_ItemClasses_SelectAllIdentities");
        }
    }
}
