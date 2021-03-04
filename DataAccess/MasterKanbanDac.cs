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
    public class MasterKanbanDac : Configuration
    {
        public static Int32 Update(IMasterKanban Kanban)
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@ItemCode", Kanban.ItemCode),
                new SqlParameter("@SNP", Kanban.SNP),
                new SqlParameter("@SysModifier",Kanban.SysModifier)
            };

            return SqlHelper.ExecuteNonQuery(ConnectionString, "sp_MasterKanban_SNP_Update", _parameters);
        }
    }
}