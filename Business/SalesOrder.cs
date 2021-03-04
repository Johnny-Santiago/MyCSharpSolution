///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SalesOrder.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 16-SEP-2015
///   Description: This file is the business rules and logic implementation of Sales Order.
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
    public class SalesOrder : ISalesOrder
    {
        private Nullable<Int32> _Id;
        private String _CustomerName;
        private String _OrderNumber;
        private Nullable<DateTime> _OrderDate;
        private Nullable<DateTime> _OrderDate2;
        private String _YourRef;
        private String _Currency;
        private Nullable<Decimal> _Amount;
        private String _DNOrder;
        private String _Manifest;
        private String _RIT;
        private String _OrderBy;
        private String _InvoiceTo;
        private String _DeliveryTo;
        private String _SelectionCode;
        private String _Description;
        private String _SysCreator;
        private String _SoNo;//Start from here 24-Nov-2015
        private String _CustNo;
        private String _CustName; 
        private String _DealingType;
        private String _CustAlias;
        private String _DnNo;
        private String _CustPo;
        private Nullable<DateTime> _ShipDate; 
        private String _OrderNo;
        private String _IchItemCode;
        private String _CustItemCode;
        private String _PartDescription; 
        private String _Project;
        private Nullable<Int32> _Qty;
        private String _InvoiceNo;
        private String _Shift; 
        private String _FromTime;
        private String _ToTime;
        private String _ItemCodeParam;
        private String _FromDate;
        private String _ToDate;
        private DataTable _Info;

        public SalesOrder()
        {
        }

        public SalesOrder(Int32 Id)
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
            _Id = row["Id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Id"]);
            _CustomerName = row["CustomerName"] == DBNull.Value ? string.Empty : Convert.ToString(row["CustomerName"]);
            _OrderNumber = row["OrderNumber"] == DBNull.Value ? string.Empty : Convert.ToString(row["OrderNumber"]);
            _OrderDate = row["OrderDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["OrderDate"]);
            _YourRef = row["YourRef"] == DBNull.Value ? string.Empty : Convert.ToString(row["YourRef"]);
            _Currency = row["Currency"] == DBNull.Value ? string.Empty : Convert.ToString(row["Currency"]);
            _Amount = row["Amount"] == DBNull.Value ? (Nullable<Decimal>)null : Convert.ToDecimal(row["Amount"]);
            _DNOrder = row["DNOrder"] == DBNull.Value ? string.Empty : Convert.ToString(row["DNOrder"]);
            _Manifest = row["Manifest"] == DBNull.Value ? string.Empty : Convert.ToString(row["Manifest"]);
            _RIT = row["RIT"] == DBNull.Value ? string.Empty : Convert.ToString(row["RIT"]);
            _OrderBy = row["OrderBy"] == DBNull.Value ? string.Empty : Convert.ToString(row["OrderBy"]);
            _InvoiceTo = row["InvoiceTo"] == DBNull.Value ? string.Empty : Convert.ToString(row["InvoiceTo"]);
            _DeliveryTo = row["DeliveryTo"] == DBNull.Value ? string.Empty : Convert.ToString(row["DeliveryTo"]);
            _SelectionCode = row["SelectionCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["SelectionCode"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _SoNo=row["SO No."] == DBNull.Value ? string.Empty : Convert.ToString(row["SO No."]);
            _CustNo = row["Cust. No."] == DBNull.Value ? string.Empty : Convert.ToString(row["Cust. No."]);
            _CustName = row["Cust. Name"] == DBNull.Value ? string.Empty : Convert.ToString(row["Cust. Name"]);
            _DealingType = row["Dealing Type"] == DBNull.Value ? string.Empty : Convert.ToString(row["Dealing Type"]);
            _CustAlias = row["Cust. Alias"] == DBNull.Value ? string.Empty : Convert.ToString(row["Cust. Alias"]);
            _DnNo = row["DN. No."] == DBNull.Value ? string.Empty : Convert.ToString(row["DN. No."]);
            _CustPo = row["Cust. PO"] == DBNull.Value ? string.Empty : Convert.ToString(row["Cust. PO"]);
            _ShipDate = row["ShipDateTime"] == DBNull.Value ? (Nullable< DateTime >) null : Convert.ToDateTime(row["ShipDateTime"]);
            _OrderNo = row["Order No"] == DBNull.Value ? string.Empty : Convert.ToString(row["Order No"]);
            _IchItemCode = row["Part No.(PTII)"] == DBNull.Value ? string.Empty : Convert.ToString(row["Part No.(PTII)"]);
            _CustItemCode = row["Part No.(Cust.)"] == DBNull.Value ? string.Empty : Convert.ToString(row["Part No.(Cust.)"]);
            _PartDescription = row["Part Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Part Description"]);
            _Project = row["Project"] == DBNull.Value ? string.Empty : Convert.ToString(row["Project"]);
            _Qty = row["Qty"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Qty"]);            
            _InvoiceNo = row["Invoice No"] == DBNull.Value ? string.Empty : Convert.ToString(row["Invoice No"]);
        }

        public String ToDate
        {
            get { return _ToDate; }
            set { _ToDate = value; }
        }
        public String FromDate
        {
            get { return _FromDate; }
            set { _FromDate = value; }
        }
        public String ItemCodeParam
        {
            get { return _ItemCodeParam; }
            set { _ItemCodeParam=value;}
        }
        public String Shift
        {
            get { return _Shift; }
            set { _Shift = value; }
            
        }
        public String FromTime
        {
            get { return _FromTime; }
            set { _FromTime = value; }
        }
        public String ToTime
        {
            get { return _ToTime; }
            set { _ToTime = value; }
        }

        public String SoNo
        {
            get { return _SoNo; }
            set { _SoNo = value; }
        }
        public String CustNo
        {
            get { return _CustNo; }
            set { _CustNo = value; }
        }
        public String CustName
        {
            get { return _CustName; }
            set { _CustName = value; }
        }
        public String DealingType
        {
            get { return _DealingType; }
            set { _DealingType = value; }
        }
        public String CustAlias
        {
            get { return _CustAlias; }
            set { _CustAlias = value; }
        }
        public String DnNo
        {
            get { return _DnNo; }
            set { _DnNo = value; }
        }
        public String CustPo
        {
            get { return _CustPo; }
            set { _CustPo = value; }
        }
        public Nullable<DateTime> ShipDate
        {
            get { return _ShipDate; }
            set { _ShipDate = value; }
        }
        public String OrderNo
        {
            get { return _OrderNo; }
            set { _OrderNo = value; }
        }
        public String IchItemCode
        {
            get { return _IchItemCode;}
            set { _IchItemCode = value; }
        }
        public String CustItemCode
        {
            get { return _CustItemCode; }
            set { _CustItemCode = value; }
        }
        public String PartDescription
        {
            get { return _PartDescription; }
            set { _PartDescription = value; }
        }
        public String Project
        {
            get { return _Project; }
            set { _Project = value; }
        }
        public Nullable<Int32> Qty
        {
            get { return _Qty; }
            set { _Qty = value; }
        }
        public String InvoiceNo
        {
            get { return _InvoiceNo; }
            set { _InvoiceNo = value; }
        }
        public Nullable<Int32> Id 
        { 
            get { return _Id; }
            set { _Id = value; } 
        }

        public String CustomerName 
        { 
            get { return _CustomerName; } 
            set { _CustomerName = value; } 
        }

        public String OrderNumber 
        { 
            get { return _OrderNumber; } 
            set { _OrderNumber = value; } 
        }

        public Nullable<DateTime> OrderDate 
        { 
            get { return _OrderDate; } 
            set { _OrderDate = value; } 
        }

        public Nullable<DateTime> OrderDate2 
        { 
            get { return _OrderDate2; } 
            set { _OrderDate2 = value; } 
        }

        public String YourRef 
        { 
            get { return _YourRef; } 
            set { _YourRef = value; }
        }

        public String Currency 
        { 
            get { return _Currency; } 
            set { _Currency = value; } 
        }

        public Nullable<Decimal> Amount 
        { 
            get { return _Amount; } 
            set { _Amount = value; } 
        }

        public String DNOrder 
        { 
            get { return _DNOrder; } 
            set { _DNOrder = value; } 
        }

        public String Manifest 
        { 
            get { return _Manifest; } 
            set { _Manifest = value; } 
        }

        public String RIT
        {
            get { return _RIT; }
            set { _RIT = value; }
        }

        public String OrderBy 
        { 
            get { return _OrderBy; } 
            set { _OrderBy = value; } 
        }

        public String InvoiceTo 
        { 
            get { return _InvoiceTo; } 
            set { _InvoiceTo = value; } 
        }

        public String DeliveryTo 
        { 
            get { return _DeliveryTo; } 
            set { _DeliveryTo = value; } 
        }

        public String SelectionCode 
        { 
            get { return _SelectionCode; } 
            set { _SelectionCode = value; } 
        }

        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public String SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }

        public DataTable Info 
        { 
            get { return _Info; } 
        }

        public DataTable Retrieve(Int32 Id)
        {
            return SalesOrderDac.Retrieve(Id).Tables[0];
        }
        public DataTable Retrieve()
        {
            return SalesOrderDac.Retrieve(this).Tables[0];
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

            return SalesOrderDac.Retrieve(this, searchOption).Tables[0];
        }

        public void Update()
        {
            SalesOrderDac.UpdateDOAndManifest(this);
        }

        
    }
}