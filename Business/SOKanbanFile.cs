///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SOKanbanDetail.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 10-FEB-2016
///   Description: This file is the business rules and logic implementation of SOKanbanDetail.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Interface;
using DataAccess;
using Extensions;

namespace Business
{
    public class SOKanbanFile : ISOKanbanFile
    {
        private Nullable<Int32> _Id;
        private String _Name;
        private String _ContentType;
        private Byte[] _Data;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private DataTable _Info;
        private String _Xml;

        public SOKanbanFile()
        {
        }

        public SOKanbanFile(Int32 Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Id not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _Name = row["Name"] == DBNull.Value ? string.Empty : Convert.ToString(row["Name"]);
            _ContentType = row["ContentType"] == DBNull.Value ? string.Empty : Convert.ToString(row["ContentType"]);
            _Data = row["Data"] == DBNull.Value ? null : (Byte[])row["Data"]; 
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
        }

        public Nullable<Int32> Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        public String Name 
        { 
            get { return _Name; } 
            set { _Name = value; } 
        }

        public String ContentType 
        { 
            get { return _ContentType; } 
            set { _ContentType = value; } 
        }

        public Byte[] Data 
        { 
            get { return _Data; } 
            set { _Data = value; } 
        }

        public String SysCreator 
        { 
            get { return _SysCreator; } 
            set { _SysCreator = value; } 
        }

        public Nullable<DateTime> SysCreated 
        { 
            get { return _SysCreated; } 
            set { _SysCreated = value; } 
        }

        public String Xml
        {
            get { return _Xml; }
            set { _Xml = value; }
        }

        public DataTable Info 
        { 
            get { return _Info; } 
            set { _Info = value; } 
        }

        public DataTable Retrieve(Int32 Id)
        {
            return SOKanbanFileDac.Retrieve(Id).Tables[0];
        }

        public static DataTable Validate_Standard(String xml)
        {
            return SOKanbanFileDac.Validate(xml, EDIFIles.Standard).Tables[0];
        }

        public static DataTable Validate_TAM(String xml) 
        {
            return SOKanbanFileDac.Validate(xml, EDIFIles.TAM).Tables[0];
        }

        public static DataTable Validate_ADM(String xml)
        {
            return SOKanbanFileDac.Validate(xml, EDIFIles.ADM).Tables[0];
        }
        public static DataTable Validate_ADM_OES(String xml)
        {
            return SOKanbanFileDac.Validate(xml,EDIFIles.ADMOES).Tables[0];
        }
        public static DataTable Validate_HPM(String xml)
        {
            return SOKanbanFileDac.Validate(xml, EDIFIles.HPM).Tables[0];
        }

        public Int32 Import_Standard()
        {
            return SOKanbanFileDac.Import(this, EDIFIles.Standard);
        }

        public Int32 Import_TAM() 
        {
            return SOKanbanFileDac.Import(this, EDIFIles.TAM);
        }

        public Int32 Import_ADM()
        {
            return SOKanbanFileDac.Import(this, EDIFIles.ADM);
        }

        public Int32 Import_HPM()
        {
            return SOKanbanFileDac.Import(this, EDIFIles.HPM);
        }

        public Int32 Insert()
        {
            return SOKanbanFileDac.Insert(this); 
        }
    }
}