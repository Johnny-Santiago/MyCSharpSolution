///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SOKanbanDetail.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-FEB-2015
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
    public class SOKanbanDetail
    {
        private Nullable<Int32> _Id;
        private String _YourRef;
        private Nullable<Int32> _Status;
        private String _Remarks;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private DataTable _Info;

        public Nullable<Int32> Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        public String YourRef 
        { 
            get { return _YourRef; } 
            set { _YourRef = value; } 
        }

        public Nullable<Int32> Status 
        { 
            get { return _Status; } 
            set { _Status = value; } 
        }

        public String Remarks 
        { 
            get { return _Remarks; } 
            set { _Remarks = value; } 
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

        public DataTable Info 
        { 
            get { return _Info; } 
            set { _Info = value; } 
        }

        //public static DataTable Validate(String xml)
        //{
        //    return SOKanbanDetailDac.Validate(xml).Tables[0];
        //}

        public static DataTable Retrieve(String YourRef)
        {
            return SOKanbanDetailDac.Retrieve(YourRef).Tables[0];
        }

        //public static void Import(String SysCreator, String xml)
        //{
        //    SOKanbanDetailDac.Import(SysCreator, xml);
        //}
    }
}