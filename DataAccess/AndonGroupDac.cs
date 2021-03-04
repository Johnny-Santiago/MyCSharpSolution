
///-----------------------------------------------------------------
///   Class:          AndonGroupDac.cs
///   Description:    This file is the class for AndonGroupDac
///   Author:         Usep Haris Nugraha                    
///	  Create Date: 	  December, 10 2015
///-----------------------------------------------------------------
///   Revision History:
///   Name:           Date:        Description:
///-----------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Common;
using Interface;
using System.Data.SqlClient;

namespace DataAccess
{
    public class AndonGroupDac : Configuration
    {

        public static DataSet Retrieve(string group)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Group",group)
                                        };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonGroups_SelectByGroup", _parameter);
        }

        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonGroups_SelectAll");
        }

        public static Int32 Insert(IAndonGroup andon)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Group",andon.Group),
                                            new SqlParameter("@Description",andon.Description),
                                            new SqlParameter("@ToList",andon.ToList),
                                            new SqlParameter("@CcList",andon.CcList),
                                            new SqlParameter("@SysCreator",andon.Syscreator)
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionString, "sp_AndonGroups_Insert", _parameter));
        }

        public static Int32 Update(IAndonGroup andon)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Id",andon.ID),
                                            new SqlParameter("@Decription",andon.Description),
                                            new SqlParameter("@ToList",andon.ToList),
                                            new SqlParameter("@CcList",andon.CcList),
                                            new SqlParameter("@SysModifier",andon.SysModifier)
                                        };
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionString, "sp_AndonGroups_UpdateById", _parameter));
        }

        public static DataSet Retrieve(int ID)
        {
            SqlParameter[] _parameter = { 
                                            new SqlParameter("@Id", ID) 
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonGroups_SelectById", _parameter);
        }
    }
}
