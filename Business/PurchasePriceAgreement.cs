///-----------------------------------------------------------------------------------------------------------------
///   Created by Karsito           Mar 21, 2016                PurchasePriceAgreement.cs
///                                                            Use for Reporting Purchase Price Agreements
///                                                            
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
    public class PurchasePriceAgreement:IPurchasePriceAgreement
    {
        private Nullable<DateTime> _OrderDate1;
        private Nullable<DateTime> _OrderDate2;
        private DataTable _dtPurchasePriceAgreement;
        private String _ItemCode;
        private String _crdnr;

        public PurchasePriceAgreement()
        { 
        }
        private void InitializeProperties(DataRow row)
        { 
        }
        public String crdnr
        {
            get { return _crdnr;}
            set { _crdnr =value;}
        }
        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }
        public Nullable<DateTime> OrderDate1
        {
            get { return _OrderDate1; }
            set { _OrderDate1 = value; }
        }
        public Nullable<DateTime> OrderDate2
        {
            get { return _OrderDate2; }
            set { _OrderDate2 = value; }
        }
        public DataTable dtPurchasePriceAgreement
        {
            get { return _dtPurchasePriceAgreement; }
        }
        public DataTable Retrieve()
        {
            return PurchasePriceAgreementDac.Retrieve(this).Tables[0];
        }
    }
}