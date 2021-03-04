
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  Supplier.cs
///   Author:      Karsito
///   Create Date: 23-MAR-2016
///   Description: This file is the business rules and logic implementation of Supplier.
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
    public class Supplier : ISupplier
    {
        private String _SupplierCode;
        private String _SupplierName;
        private String _Alias;
        private String _Address;
        private String _City;
        private String _Country;
        private String _PostalCode;
        private DataTable _Info;

        public Supplier()
        { 
        }
        public Supplier(String SupplierCode)
        {
            _SupplierCode = SupplierCode;

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

               
            }
            else
            {
                throw new KeyNotFoundException("Supplier Code not found.");
            }
        }
        private void InitializeProperties(DataRow row)
        {
            _SupplierCode = row["SupplierCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["SupplierCode"]);
            _SupplierName = row["SupplierName"] == DBNull.Value ? string.Empty : Convert.ToString(row["SupplierName"]);
            _Alias = row["Alias"] == DBNull.Value ? string.Empty : Convert.ToString(row["Alias"]);
            _Address = row["Address"] == DBNull.Value ? string.Empty : Convert.ToString(row["Address"]);
            _City = row["City"] == DBNull.Value ? string.Empty : Convert.ToString(row["City"]);
            _Country = row["Country"] == DBNull.Value ? string.Empty : Convert.ToString(row["Country"]);
            _PostalCode = row["PostalCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["PostalCode"]);
        }
        public String SupplierCode
        {
            get { return _SupplierCode; }
            set { _SupplierCode = value; }
        }
        public String SupplierName
        {
            get { return _SupplierName; }
            set { _SupplierName = value; }
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
        public String  City
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

            return SupplierDac.Retrieve(this, searchOption).Tables[0];
        }
     }
}