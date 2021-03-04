
///-----------------------------------------------------------------
///   Class:          AndonReportLogDac.cs
///   Description:    This file is the class for AndonReportLogDac
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
    public class AndonReportLogDac : Configuration
    {

        public static DataSet Retrieve(string warehouse, string machine)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Warehouse",warehouse),
                                            new SqlParameter("@Machine",machine),
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonReportLogs_SelectByMachine", _parameter);
        }

        public static DataSet Retrieve(string warehouse, string machine, string group)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Warehouse",warehouse),
                                            new SqlParameter("@Machine",machine),
                                            new SqlParameter("@Group",group)
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonReportLogs_SelectByGroup", _parameter);
        }

        public static DataSet Retrieve(string warehouse, string machine, Int32 condition)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Warehouse",warehouse),
                                            new SqlParameter("@Machine",machine),
                                            new SqlParameter("@Condition",condition)
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonReportLogs_SelectByCondition", _parameter);
        }

        public static DataSet Retrieve(string warehouse, Int32 condition)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Warehouse",warehouse),
                                            new SqlParameter("@Condition",condition)
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonReportLogs_SelectByCondition", _parameter);
        }

        public static DataSet Retrieve(IAndonReportLog andon)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Warehouse",andon.Warehouse),
                                            new SqlParameter("@Machine",andon.Machine),
                                            new SqlParameter("@Condition",andon.Condition),
                                            new SqlParameter("@Group",andon.Group)
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonReportLogs_SelectAll", _parameter);
        }

        public static int Insert(IAndonReportLog andonReport)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Condition",andonReport.Condition),
                                            new SqlParameter("@Warehouse",andonReport.Warehouse),
                                            new SqlParameter("@Machine",andonReport.Machine),
                                            new SqlParameter("@Problem",andonReport.Problem),
                                            new SqlParameter("@Group",andonReport.Group),
                                            new SqlParameter("@SysCreator",andonReport.SysCreator)
                                        };
            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_AndonReportLogs_Insert", _parameter));
        }

        public static int Update(IAndonReportLog andonReport)
        {
            SqlParameter[] _parameter = { new SqlParameter("@Id",andonReport.ID),
                                            new SqlParameter("@Condition",andonReport.Condition),
                                            new SqlParameter("@CorrectiveAction",andonReport.CorrectiveAction),
                                            new SqlParameter("@StartTime",andonReport.StartTime),
                                            new SqlParameter("@EndTime",andonReport.EndTime),
                                            new SqlParameter("@SysModifier",andonReport.SysModifier)
                                        };
            return Convert.ToInt32(SqlHelper.ExecuteNonQuery(ConnectionString, "sp_AndonReportLogs_UpdateById", _parameter));
        }

        public static DataSet Retrieve(int ID)
        {
            SqlParameter[] _parameter = { 
                                            new SqlParameter("@Id", ID) 
                                        };

            return SqlHelper.ExecuteDataset(ConnectionString, "sp_AndonReportLogs_SelectById", _parameter);
        }
    }
}
