///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  ItemClas.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 04-MAR-2015
///   Description: This file is the business rules and logic implementation of ItemClas.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using Extensions;

namespace Business
{
    public class ItemClass
    {
        public static DataTable GetClassifications() 
        {
            return ItemClassDac.RetrieveClassifications().Tables[0];
        }

        public static DataTable GetFamilies() 
        {
            return ItemClassDac.RetrieveFamilies().Tables[0];
        }

        public static DataTable GetCategories()
        {
            return ItemClassDac.RetrieveCategories().Tables[0];
        }

        public static DataTable GetPositions()
        {
            return ItemClassDac.RetrievePositions().Tables[0];
        }

        public static DataTable GetTypes()
        {
            return ItemClassDac.RetrieveTypes().Tables[0];
        }

        public static DataTable GetIdentities()
        {
            return ItemClassDac.RetrieveIdentities().Tables[0];
        }

        public static DataTable GetModels()
        {
            return ItemClassDac.RetrieveModels().Tables[0];
        }
    }
}
