using System;
using System.Collections.Generic;
using System.Linq;
using System.Data;
using System.Text;
using Interface;
using DataAccess;

namespace Business
{
    public class InvoiceTaxSerial : IInvoiceTaxSerial
    {
        private Nullable<Int32> _ID;
        private String _StartSerialNumber;
        private String _EndSerialNumber;
        private String _LastSerialNumber;
        private String _SysCreator;
        private Nullable<DateTime> _SysCreated;
        private DataTable _Info;

        public InvoiceTaxSerial()
        {
            _Info = Retrieve();
            if (_Info.Rows.Count == 1)
            {
                InitializeProperties(_Info.Rows[0]);
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["Id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Id"]);
            _StartSerialNumber = row["StartSerialNumber"] == DBNull.Value ? string.Empty : Convert.ToString(row["StartSerialNumber"]);
            _EndSerialNumber = row["EndSerialNumber"] == DBNull.Value ? string.Empty : Convert.ToString(row["EndSerialNumber"]);
            _LastSerialNumber = row["LastSerialNumber"] == DBNull.Value ? string.Empty : Convert.ToString(row["LastSerialNumber"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
        }

        public DataTable Retrieve()
        {
            return InvoiceTaxSerialDac.Retrieve().Tables[0];
        }

        public DataTable RetrieveAll()
        {
            return InvoiceTaxSerialDac.RetrieveAll().Tables[0];
        }

        public Int32 Insert()
        {
            return InvoiceTaxSerialDac.Insert(this);
        }

        public Int32 UpdateLastSerial()
        {
            return InvoiceTaxSerialDac.UpdateLastSerial(this);
        }

        public Int32 Update()
        {
            return InvoiceTaxSerialDac.Update(this);
        }

        public Int32 UpdateID()
        {
            return InvoiceTaxSerialDac.UpdateID(this);
        }

        public Int32 DeleteID()
        {
            return InvoiceTaxSerialDac.DeleteID(this);
        }

        public Nullable<Int32> Id
        {
            get
            {
                return _ID;
            }
            set
            {
                _ID = value;
            }
        }

        public String StartSerialNumber
        {
            get
            {
                return _StartSerialNumber;
            }
            set
            {
                _StartSerialNumber = value;
            }
        }

        public String EndSerialNumber
        {
            get
            {
                return _EndSerialNumber;
            }
            set
            {
                _EndSerialNumber = value;
            }
        }
        public String LastSerialNumber
        {
            get
            {
                return _LastSerialNumber;
            }
            set
            {
                _LastSerialNumber = value;
            }
        }

        public String SysCreator
        {
            get
            {
                return _SysCreator;
            }
            set
            {
                _SysCreator = value;
            }
        }

        public Nullable<DateTime> SysCreated
        {
            get
            {
                return _SysCreated;
            }
            set
            {
                _SysCreated = value;
            }
        }


        public DataTable Info
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
        }

    }
}