///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  QuotationDetail.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 26-JUL-2016
///   Description: This file is the business rules and logic implementation of QuotationDetail.
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
    public class QuotationDetail : IQuotationDetail 
    {
        private Nullable<Int32> _Id;
        private Nullable<DateTime> _ForecastDate;
        private Nullable<DateTime> _DeliveryDate; 
        private String _CustomerCode;
        private String _CustomerPartNo;
        private String _ItemCode;
        private Nullable<Decimal> _Quantity;
        private String _Xml; 
        private DataTable _Info;

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public Nullable<DateTime> ForecastDate
        {
            get { return _ForecastDate; }
            set { _ForecastDate = value; }
        }

        public Nullable<DateTime> DeliveryDate
        {
            get { return _DeliveryDate; }
            set { _DeliveryDate = value; }
        }

        public String CustomerCode
        {
            get { return _CustomerCode; }
            set { _CustomerCode = value; }
        }

        public String CustomerPartNo
        {
            get { return _CustomerPartNo; }
            set { _CustomerPartNo = value; }
        }

        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
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

        public Nullable<Decimal> Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public static DataTable Retrieve(String CustomerCode, DateTime ForecastDate)
        {
            return QuotationDetailDac.Retrieve(CustomerCode, ForecastDate).Tables[0];
        }
    }
}