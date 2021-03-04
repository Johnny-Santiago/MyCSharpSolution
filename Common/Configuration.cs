// ===============================================================================
// CLASS NAME:  Configuration.cs
// AUTHOR:      Juanito U. Santiago Jr.
// CREATE DATE: 07-MAR-2014
// DESCRIPTION: This file initializes the connection string from the web config
//              file.
// VERSION:     0.0
// ===============================================================================

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Configuration;

namespace Common
{
    public abstract class Configuration
    {
        public static String ConnectionString
        {
            get
            {
                return ConfigurationManager.ConnectionStrings["EXACT-SVR"].ConnectionString;
            }
        }
    }
}