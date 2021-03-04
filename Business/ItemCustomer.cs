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
///     Add Methode GetAllCustomerItem()                  May 9, 2016                          Usep Haris N.
///-----------------------------------------------------------------------------------------------------------------
///
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
    public class ItemCustomer : IItemCustomer
    {
        private String _CustCode;
        private String _CustomerItemCode;
        private String _ItemCode;
        private String _Description;
        private DataTable _Info;
        private DataTable _ItemList;

        public ItemCustomer()
        {
        }

        public ItemCustomer(String CustomerCode)
        {
            _CustCode = CustomerCode;

            _Info = Retrieve(_CustCode);
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
            _CustCode = row["CustCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["CustomerCode"]);
            _CustomerItemCode = row["Customer item"] == DBNull.Value ? string.Empty : Convert.ToString(row["Customer item"]);
            _ItemCode = row["Ichikoh item"] == DBNull.Value ? string.Empty : Convert.ToString(row["Ichikoh item"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public DataTable Retrieve(string customercode)
        {
            return ItemCustomerDac.Retrieve(customercode).Tables[0];
        }

        public string CustCode
        {
            get
            {
                return _CustCode;
            }
            set
            {
                _CustCode = value;
            }
        }

        public string CustomerItemCode
        {
            get
            {
                return _CustomerItemCode;
            }
            set
            {
                _CustomerItemCode = value;
            }
        }

        public string ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                _ItemCode = value;
            }
        }

        public string Description
        {
            get
            {
                return _Description;
            }
            set
            {
                _Description = value;
            }
        }

        public DataTable ItemList
        {
            get
            {
                return ItemList;
            }
            set
            {
                _ItemList = value;
            }
        }

        public DataTable GetAllCustomerItem()
        {
            return ItemCustomerDac.Retrieve().Tables[0];
        }
    }
}
