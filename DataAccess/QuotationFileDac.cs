///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  QuotationDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 26-JUL-2016
/// Description: This file is the data access of Quotation.
/// Version:     1
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
    public class QuotationFileDac : Configuration
    {
        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_QuotationFiles_Select", _parameters);
        }

        public static DataSet Validate(String xml)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@xml", xml)
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_QuotationFiles_Validate", _parameters);
        }

        public static Int32 Import(IQuotationFile QuotationFile)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Name", QuotationFile.Name)
                ,new SqlParameter("@ContentType", QuotationFile.ContentType)
                ,new SqlParameter("@Data", QuotationFile.Data)
                ,new SqlParameter("@Xml", QuotationFile.Xml)
                ,new SqlParameter("@SysCreator", QuotationFile.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_QuotationFiles_Import", _parameters));
        }

        public static DataSet Export(IQuotationFile QuotationFile) 
        {
            SqlParameter[] _parameters = { 
                new SqlParameter("@CustomerCode", QuotationFile.CustomerCode) 
                ,new SqlParameter("@ForecastDate", QuotationFile.ForecastDate) 
                ,new SqlParameter("@SysCreator", QuotationFile.SysCreator) 
            };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Quotations_GetTemplate", _parameters);
        }

        public static Int32 Insert(IQuotationFile QuotationFile)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@CustomerCode", QuotationFile.CustomerCode)
                ,new SqlParameter("@ForecastDate", QuotationFile.ForecastDate)
                ,new SqlParameter("@Name", QuotationFile.Name)
                ,new SqlParameter("@ContentType", QuotationFile.ContentType)
                ,new SqlParameter("@Data", QuotationFile.Data)
                ,new SqlParameter("@SysCreator", QuotationFile.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_QuotationFiles_Insert", _parameters));
        }
    }
}