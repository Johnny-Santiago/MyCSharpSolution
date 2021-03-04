///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  Contact.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 14-AUG-2015
///   Description: This file is the business rules and logic implementation of Contacts.
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
    public class Contact : IContact 
    {
        private Nullable<Int32> _Id;
        private String _Abbreviation;
        private String _Initials;
        private String _Fullname;
        private String _Phone;
        private String _Mobile;
        private String _JobDescription;
        private String _JobTitle;
        private String _Email;
        private String _CustomerCode;
        private Nullable<Boolean> _Main;
        private Nullable<Boolean> _Active;
        private DataTable _Info;
        private DataTable _ContactsList;

        public Contact()
        {
        }

        public Contact(String CustomerCode)
        {
            _CustomerCode = CustomerCode;

            _ContactsList = Retrieve(SearchOption.Equals);

            _Main = true;

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Customer Code not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["Id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Id"]);
            _Abbreviation = row["Abbreviation"] == DBNull.Value ? string.Empty : Convert.ToString(row["Abbreviation"]);
            _Initials = row["Initials"] == DBNull.Value ? string.Empty : Convert.ToString(row["Initials"]);
            _Fullname = row["Fullname"] == DBNull.Value ? string.Empty : Convert.ToString(row["Fullname"]);
            _Phone = row["Phone"] == DBNull.Value ? string.Empty : Convert.ToString(row["Phone"]);
            _Mobile = row["Mobile"] == DBNull.Value ? string.Empty : Convert.ToString(row["Mobile"]);
            _JobDescription = row["JobDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["JobDescription"]);
            _JobTitle = row["JobTitle"] == DBNull.Value ? string.Empty : Convert.ToString(row["JobTitle"]);
            _Email = row["Email"] == DBNull.Value ? string.Empty : Convert.ToString(row["Email"]);
            _CustomerCode = row["CustomerCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["CustomerCode"]);
            _Main = row["Main"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Main"]);
            _Active = row["Active"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Active"]);
        }

        public Nullable<Int32> Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        public String Abbreviation 
        { 
            get { return _Abbreviation; } 
            set { _Abbreviation = value; } 
        }

        public String Initials 
        { 
            get { return _Initials; } 
            set { _Initials = value; } 
        }

        public String Fullname 
        { 
            get { return _Fullname; } 
            set { _Fullname = value; } 
        }

        public String Phone 
        { 
            get { return _Phone; } 
            set { _Phone = value; } 
        }

        public String Mobile 
        { 
            get { return _Mobile; } 
            set { _Mobile = value; } 
        }

        public String JobDescription 
        { 
            get { return _JobDescription; } 
            set { _JobDescription = value; } 
        }

        public String JobTitle 
        { 
            get { return _JobTitle; } 
            set { _JobTitle = value; } 
        }

        public String Email 
        { 
            get { return _Email; } 
            set { _Email = value; } 
        }

        public String CustomerCode 
        { 
            get { return _CustomerCode; } 
            set { _CustomerCode = value; } 
        }

        public Nullable<Boolean> Main 
        { 
            get { return _Main; } 
            set { _Main = value; } 
        }

        public Nullable<Boolean> Active 
        { 
            get { return _Active; } 
            set { _Active = value; } 
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public DataTable ContactsList 
        {
            get { return _ContactsList; }
            set { _ContactsList = value; }
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

            return ContactDac.Retrieve(this, searchOption).Tables[0];
        }
    }
}
