///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  DeliveryNoteReference.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 11-JAN-2016
///   Description: This file is the business rules and logic implementation of Delivery Note References.
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
    public class DeliveryNoteReference : IDeliveryNoteReference 
    {
        private Nullable<Int32> _ID;
        private String _DeliveryNoteNo;
        private Nullable<DateTime> _DeliveryNoteDate;
        private String _Description;
        private DataTable _Info;

        public DeliveryNoteReference()
        {
        }

        public DeliveryNoteReference(Nullable<Int32> ID) 
        {
            _ID = ID; 

            _Info = Retrieve(SearchOption.Equals);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("ID not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _DeliveryNoteNo = row["Delivery note number"] == DBNull.Value ? string.Empty : Convert.ToString(row["Delivery note number"]);
            _DeliveryNoteDate = row["Delivery note date"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["Delivery note date"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
        }

        public Nullable<Int32> ID 
        {
            get { return _ID; }
            set { _ID = value; } 
        }

        public String DeliveryNoteNo 
        {
            get { return _DeliveryNoteNo; }
            set { _DeliveryNoteNo = value; }
        }

        public Nullable<DateTime> DeliveryNoteDate 
        {
            get { return _DeliveryNoteDate; }
            set { _DeliveryNoteDate = value; }
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

            return DeliveryNoteReferenceDac.Retrieve(this, searchOption).Tables[0];
        }
    }
}