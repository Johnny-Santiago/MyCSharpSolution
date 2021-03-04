///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SelectionCode.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-JAN-2016
///   Description: This file is the business rules and logic implementation of dealing types.
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
    public class DealingType : IDealingType
    {
        private Nullable<Guid> _ID;
        private String _AccountCategoryCode;
        private String _Description;
        private DataTable _Info;

        public DealingType()
        {
        }

        public DealingType(Nullable<Guid> ID) 
        {
            _ID = ID; 

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Dealing type not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Guid>)null : (Guid)row["ID"];
            _AccountCategoryCode = row["AccountCategoryCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["AccountCategoryCode"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
        }

        public Nullable<Guid> ID 
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public String AccountCategoryCode 
        {
            get { return _AccountCategoryCode; }
            set { _AccountCategoryCode = value; }
        }

        public String Description 
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public DataTable Info 
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public DataTable Retrieve(SearchOption SearchOption)
        {
            Int32 searchOption;
            switch (SearchOption)
            {
                case SearchOption.StartsWith: searchOption = 2; break;
                case SearchOption.Contains: searchOption = 3; break;
                case SearchOption.EndsWith: searchOption = 4; break;
                default: searchOption = 1; break;
            }

            return DealingTypeDac.Retrieve(this, searchOption).Tables[0];
        }
    }
}