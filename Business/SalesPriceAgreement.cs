///-----------------------------------------------------------------------------------------------------------------
///   Created by Karsito           Mar 13, 2016                SalesPriceAgreement.cs
///                                                            Use for Reporting Sales Price Agreements
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
    public class SalesPriceAgreement:ISalesPriceAgreement
    {
        private Nullable<DateTime> _OrderDate1;
        private Nullable<DateTime> _OrderDate2;
        private DataTable _dtSalesPriceAgreement;
        private String _ItemCode;
        private String _debnr;

        public SalesPriceAgreement ()
        {
            
        }
        public SalesPriceAgreement(DateTime date1, DateTime date2)
        {
            _dtSalesPriceAgreement = Retrieve(date1, date2);
        }

        private DataTable Retrieve(DateTime date1, DateTime date2)
        {
            return SalesPriceAgreementDac.Retrieve(date1,date2).Tables[0];
        }

        private void InitializeProperties(DataRow row)
        {

            
        }
        public String debnr
        {
            get { return _debnr; }
            set { _debnr = value; }
        }
        public String ItemCodeCust
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
        public DataTable dtSalesPriceAgreement
        {
            get { return _dtSalesPriceAgreement; }
           
        }
        public DataTable Retrieve()
        {
            return SalesPriceAgreementDac.Retrieve(this).Tables[0];
        }

    }
}