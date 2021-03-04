///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  WarehouseDesignation.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 04-MAR-2015
/// Description: This file is the business rules and logic implementation of WarehouseDesignation.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface;
using DataAccess;

namespace Business
{
    public class WarehouseDesignation : IWarehouseDesignation
    {
        private DataTable _Designation;
        private DataTable _Receipt;
        private DataTable _Issuance;

        public WarehouseDesignation()
        {
        }

        public WarehouseDesignation(String UserID)
        {
            _Designation = WarehouseDesignationDac.GetWarehouseDesignations(UserID).Tables[0];
            _Receipt = WarehouseDesignationDac.GetReceipt(UserID).Tables[0];
            _Issuance = WarehouseDesignationDac.GetIssuance(UserID).Tables[0];
        }

        public DataTable Designation
        {
            get { return _Designation; }
            set { _Designation = value; }
        }

        public DataTable Receipt
        {
            get { return _Receipt; }
            set { _Receipt = value; }
        }

        public DataTable Issuance
        {
            get { return _Issuance; }
            set { _Issuance = value; }
        }
    }
}