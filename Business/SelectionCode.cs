///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SelectionCode.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 13-AUG-2015
///   Description: This file is the business rules and logic implementation of Selection Codes.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface;
using DataAccess;
using Extensions;

namespace Business
{
    public class SelectionCode : ISelectionCode
    {
        private String _SelectionCode;
        private String _Description;
        private DataTable _Info;

        public SelectionCode()
        {
        }

        public SelectionCode(String SelectionCode)
        {
            _SelectionCode = SelectionCode; 

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Selection Code not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _SelectionCode = row["SelectionCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["SelectionCode"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
        }

        public String selectionCode
        {
            get { return _SelectionCode; }
            set { _SelectionCode = value; }
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

            return SelectionCodeDac.Retrieve(this, searchOption).Tables[0];
        }
    }
}