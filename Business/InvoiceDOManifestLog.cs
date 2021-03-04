///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SalesOrder.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 21-SEP-2015
///   Description: This file is the business rules and logic implementation of Invoice DO No. and Manifest No. Logs.
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
    public class InvoiceDOManifestLog : IInvoiceDOManifestLog
    {
        private Nullable<Int32> _Id;
        private Nullable<Int32> _RecID;
        private String _DeliveryNote;
        private String _DNOrder;
        private String _Manifest;
        private String _RIT;
        private String _ColumnName;
        private String _OldValue;
        private String _NewValue;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private DataTable _Info;

        public InvoiceDOManifestLog()
        {
        }

        public InvoiceDOManifestLog(String DeliveryNote) 
        {
            _Info = Retrieve(DeliveryNote);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Delivery note not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _RecID = row["RecID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["RecID"]);
            _DeliveryNote = row["DeliveryNote"] == DBNull.Value ? string.Empty : Convert.ToString(row["DeliveryNote"]);
            _DNOrder = row["DNOrder"] == DBNull.Value ? string.Empty : Convert.ToString(row["DNOrder"]);
            _Manifest = row["Manifest"] == DBNull.Value ? string.Empty : Convert.ToString(row["Manifest"]);
            _RIT = row["RIT"] == DBNull.Value ? string.Empty : Convert.ToString(row["RIT"]);
        }

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; } 
        }

        public Nullable<Int32> RecID
        {
            get { return _RecID; }
            set { _RecID = value; }
        }

        public String DeliveryNote
        {
            get { return _DeliveryNote; }
            set { _DeliveryNote = value; }
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

        public String ColumnName
        {
            get { return _ColumnName; }
            set { _ColumnName = value; }
        }

        public String OldValue
        {
            get { return _OldValue; }
            set { _OldValue = value; }
        }

        public String NewValue
        {
            get { return _NewValue; }
            set { _NewValue = value; }
        }

        public String SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }

        public Nullable<DateTime> SysCreated
        {
            get { return _SysCreated; }
            set { _SysCreated = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
        }

        public DataTable Retrieve(String DeliveryNote)
        {
            return InvoiceDOManifestLogDac.Retrieve(DeliveryNote).Tables[0];
        }


        public void Insert()
        {
            InvoiceDOManifestLogDac.Insert(this); 
        }

        public void Update()
        {
            InvoiceDOManifestLogDac.Update(this);
        }
    }
}