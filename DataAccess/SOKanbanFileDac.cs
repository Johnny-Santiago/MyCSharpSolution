///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  SOKanbanDetailDac.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 10-MAR-2016
/// Description: This file is the data access of SOKanbanFile.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public enum EDIFIles { Standard, TAM, ADM, HPM,ADMOES,StandardQCReturn }  

    public class SOKanbanFileDac : Configuration
    {
        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_Select", _parameters);
        }

        public static DataSet Validate(String xml, EDIFIles EDIFIles)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@xml", xml)
            };

            switch (EDIFIles)
            {
                case EDIFIles.Standard:
                    return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_Standard_Validate", _parameters);

                case EDIFIles.TAM:
                    return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_TAM_Validate", _parameters);

                case EDIFIles.ADM:
                    return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_ADM_Validate", _parameters);

                case EDIFIles.HPM:
                    return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_HPM_Validate", _parameters); 
               
                case EDIFIles.ADMOES:
                    return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_ADM_OES_Validate", _parameters);
               
                case EDIFIles.StandardQCReturn:
                    return SqlHelper.ExecuteDataset(ConnectionString, "sp_SOKanbanFiles_Standard_QCReturn_Validate", _parameters);

                default: return (DataSet)null;
            }
        }

        public static Int32 Import(ISOKanbanFile SOKanbanFile, EDIFIles EDIFIles)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Name", SOKanbanFile.Name)
                ,new SqlParameter("@ContentType", SOKanbanFile.ContentType)
                ,new SqlParameter("@Data", SOKanbanFile.Data)
                ,new SqlParameter("@Xml", SOKanbanFile.Xml)
                ,new SqlParameter("@SysCreator", SOKanbanFile.SysCreator)
            };

            switch (EDIFIles)
            {
                case EDIFIles.Standard:
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SOKanbanFiles_Standard_Import", _parameters));

                case EDIFIles.TAM:
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SOKanbanFiles_TAM_Import", _parameters));

                case EDIFIles.ADM:
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SOKanbanFiles_ADM_Import", _parameters));

                case EDIFIles.HPM:
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SOKanbanFiles_HPM_Import", _parameters));
                
                case EDIFIles.ADMOES:
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString,"sp_SOKanbanFiles_ADM_OES_Import",_parameters));
                
                case EDIFIles.StandardQCReturn:
                    return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SOKanbanFiles_Standard_QCReturn_Import", _parameters));
                
                default: return 0;
            }

            
        }

        public static Int32 Insert(ISOKanbanFile SOKanbanFile) 
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Name", SOKanbanFile.Name)
                ,new SqlParameter("@ContentType", SOKanbanFile.ContentType)
                ,new SqlParameter("@Data", SOKanbanFile.Data)
                ,new SqlParameter("@SysCreator", SOKanbanFile.SysCreator)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_SOKanbanFiles_Insert", _parameters));
        }
    }
}