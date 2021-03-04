
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
    public class AndonRequestListDac : Configuration
    {

        public static DataSet Retrieve(string group)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Group",group)
                                        };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonRequestList_SelectByGroup", _parameter);
        }

        public static DataSet Retrieve(string group, int condition)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Group",group),
                                            new SqlParameter("@Condition",condition)
                                        };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonRequestList_SelectByGroupCondition", _parameter);
        }

        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonRequestList_SelectAll");
        }

        public static Int32 Insert(IAndonRequestList andon)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Group",andon.Group),
                                            new SqlParameter("@Description",andon.Description),
                                            new SqlParameter("@Name",andon.Name),
                                            new SqlParameter("@SysCreator",andon.Syscreator)
                                        };

            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionString, "sp_AndonRequestList_Insert", _parameter));
        }

        public static Int32 Update(IAndonRequestList andon)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Id",andon.ID),
                                            new SqlParameter("@Decription",andon.Description),
                                            new SqlParameter("@Name",andon.Name),
                                            new SqlParameter("@SysModifier",andon.SysModifier)
                                        };
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionString, "sp_AndonRequestList_UpdateById", _parameter));
        }

        public static DataSet Retrieve(int ID)
        {
            SqlParameter[] _parameter = { 
                                            new SqlParameter("@Id", ID) 
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonRequestList_SelectById", _parameter);
        }
    }
}
