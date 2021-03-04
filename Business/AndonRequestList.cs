﻿///-----------------------------------------------------------------
///   Class:          AndonRequestList.cs
///   Description:    This file is the class for AndonRequestList
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
using DataAccess;
using Interface;
using Extensions;

namespace Business
{
    public class AndonRequestList : IAndonRequestList
    {
        private Nullable<Int32> _ID;
        private Nullable<Int32> _Condition;
        private String _Group;
        private String _Description;
        private String _Name;
        private String _Syscreator;
        private Nullable<DateTime> _SysCreated;
        private String _SysModifier;
        private Nullable<DateTime> _SysModified;
        private DataTable _Info;

        public Nullable<Int32> ID { get { return _ID; } set { _ID = value; } }
        public Nullable<Int32> Condition { get { return _Condition; } set { _Condition = value; } }
        public String Group { get { return _Group; } set { _Group = value; } }
        public String Description { get { return _Description; } set { _Description = value; } }
        public String ToList { get { return _Name; } set { _Name = value; } }
        public String Syscreator { get { return _Syscreator; } set { _Syscreator = value; } }
        public Nullable<DateTime> SysCreated { get { return _SysCreated; } set { _SysCreated = value; } }
        public String SysModifier { get { return _SysModifier; } set { _SysModifier = value; } }
        public Nullable<DateTime> SysModified { get { return _SysModified; } set { _SysModified = value; } }
        public DataTable Info { get { return _Info; } set { _Info = value; } }

        public AndonRequestList()
        {
        }

        public AndonRequestList(Int32 ID)
        {
            _Info = Retrieve(ID);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("ID not found.");
            }
        }

        public AndonRequestList(string group)
        {
            _Info = Retrieve(group);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("group not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _Group = row["Group"] == DBNull.Value ? string.Empty : row["Group"].ToString();
            _Description = row["Description"] == DBNull.Value ? string.Empty : row["Description"].ToString();
            _Name = row["ToList"] == DBNull.Value ? string.Empty : row["ToList"].ToString();
            _Syscreator = row["SysCreator"] == DBNull.Value ? string.Empty : row["SysCreator"].ToString();
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : row["SysModifier"].ToString();
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
        }

        public DataTable Retrieve()
        {
            return AndonRequestListDac.Retrieve().Tables[0];
        }

        public DataTable Retrieve(Int32 ID)
        {
            return AndonRequestListDac.Retrieve(ID).Tables[0];
        }

        public DataTable Retrieve(string group)
        {
            return AndonRequestListDac.Retrieve(group).Tables[0];
        }

        public DataTable Retrieve(string group, int condition)
        {
            return AndonRequestListDac.Retrieve(group, condition).Tables[0];
        }

        public Int32 Insert()
        {
            return AndonRequestListDac.Insert(this);
        }

        public Int32 Update()
        {
            return AndonRequestListDac.Update(this);
        }


    }
}
