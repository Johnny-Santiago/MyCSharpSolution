///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  Customer.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 13-AUG-2015
///   Description: This file is the business rules and logic implementation of Customer.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
///     Revision
///     Changes                             Date Modified                        Modifier
///-----------------------------------------------------------------------------------------------------------------
///     Add Methode GetTaxCustomers         May 3, 2016                          Usep Haris N.
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
    public class Customer : ICustomer
    {
        private String _CustomerCode;
        private String _CustomerName;
        private String _Alias;
        private String _Address;
        private String _City;
        private String _Country;
        private String _PostalCode;
        private String _BankAccountNumber;
        private DataTable _Info;
        private IContact _Contact;

        public Customer()
        {
        }

        public Customer(String CustomerCode)
        {
            _CustomerCode = CustomerCode;

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                Contact contact = new Contact(_CustomerCode);
                _Contact = (IContact)contact;
            }
            else
            {
                throw new KeyNotFoundException("Customer Code not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _CustomerCode = row["CustomerCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["CustomerCode"]);
            _CustomerName = row["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(row["CustomerName"]);
            _Alias = row["Alias"] == DBNull.Value ? string.Empty : Convert.ToString(row["Alias"]);
            _Address = row["Address"] == DBNull.Value ? string.Empty : Convert.ToString(row["Address"]);
            _City = row["City"] == DBNull.Value ? string.Empty : Convert.ToString(row["City"]);
            _Country = row["Country"] == DBNull.Value ? string.Empty : Convert.ToString(row["Country"]);
            _PostalCode = row["PostalCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["PostalCode"]);
        }

        public String CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        public String CustomerName
        {
            get { return _CustomerName; }
            set { _CustomerName = value; }
        }

        public String Alias
        {
            get { return _Alias; }
            set { _Alias = value; }
        }

        public String Address
        {
            get { return _Address; }
            set { _Address = value; }
        }

        public String City
        {
            get { return _City; }
            set { _City = value; }
        }

        public String Country
        {
            get { return _Country; }
            set { _Country = value; }
        }

        public String PostalCode
        {
            get { return _PostalCode; }
            set { _PostalCode = value; }
        }

        public String BankAccountNumber
        {
            get { return _BankAccountNumber; }
            set { _BankAccountNumber = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public IContact Contact 
        {
            get { return _Contact; }
            set { _Contact = value; } 
        }

        public DataTable GetAllCustomers()
        {
            return CustomerDac.Retrieve().Tables[0];
        }

        public DataTable GetKanbanCustomers()
        {
            return CustomerDac.RetrieveKanban().Tables[0];
        }

        public DataTable GetTaxCustomers()
        {
            return CustomerDac.RetrieveInvoiceTax().Tables[0];
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

            return CustomerDac.Retrieve(this, searchOption).Tables[0];
        }
    }
}
