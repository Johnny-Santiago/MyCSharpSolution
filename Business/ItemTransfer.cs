///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
/// Class Name:  ItemTransfer.cs
/// Author:      Juanito U. Santiago Jr.
/// Create Date: 03-MAR-2015
/// Description: This file is the business rules and logic implementation of ItemTransfer.
/// Version:     1
///-----------------------------------------------------------------------------------------------------------------
///   Revision History:
///   Name:                     Date:                 Description:
///-----------------------------------------------------------------------------------------------------------------
///   Usep Haris N.             11-MAR-2015           Add RetriveByRefId Method.
///-----------------------------------------------------------------------------------------------------------------
///   Juanito U. Santiago Jr.   11-MAR-2015           Add Fulfill Method.
///-----------------------------------------------------------------------------------------------------------------
///   Usep Haris N.             April 15, 2014        Add Property HasPrintOut
///                                                   Add Property PLNo
///                                                   Add Property ItemProd
///                                                   Add Property AutoTransfer
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
    public class ItemTransfer : IItemTransfer
    {
        private Nullable<Int32> _Id;
        private String _Code;
        private String _RefId;
        private String _PLNo;
        private String _Parent;
        private String _Production;
        private String _EqpId;
        private String _CargoId;
        private String _ItemProd;
        private String _ItemCode;
        private String _Description;
        private String _Model;
        private String _FromWhseCode;
        private String _FromWhseLocation;
        private String _ToWhseCode;
        private String _ToWhseLocation;
        private Nullable<Decimal> _Quantity;
        private String _UOM;
        private String _Remarks;
        private Nullable<Int32> _Result;
        private Nullable<Boolean> _Repairable;
        private String _ReasonCode;
        private Nullable<Int32> _Status;
        private Nullable<Boolean> _HasPrintOut;
        Nullable<Int32> _PrintCount;
        private Nullable<Boolean> _AutoTransfer;
        private Nullable<DateTime> _SysCreated;
        private Nullable<DateTime> _SysCreated2;
        private String _SysCreator;
        private Nullable<DateTime> _SysModified;
        private Nullable<DateTime> _SysModified2;
        private String _SysModifier;
        private Nullable<DateTime> _SysProcessed;
        Nullable<Int32> _Shift;
        private DataTable _Info;
        private IItemTransferLog _ItemTransferLog;

        public ItemTransfer()
        {
        }

        public ItemTransfer(String CargoId)
        {
            _Info = Retrieve(CargoId);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                ItemTransferLog itemTransferLog = new ItemTransferLog(CargoId);
                _ItemTransferLog = (IItemTransferLog)itemTransferLog;
            }
            else
            {
                throw new KeyNotFoundException("Cargo Id not found.");
            }
        }

        public ItemTransfer(Int32 Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                ItemTransferLog itemTransferLog = new ItemTransferLog(CargoId);
                _ItemTransferLog = (IItemTransferLog)itemTransferLog;
            }
            else
            {
                throw new KeyNotFoundException("Id not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["Id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Id"]);
            _Code = row["Code"] == DBNull.Value ? string.Empty : Convert.ToString(row["Code"]);
            _RefId = row["RefId"] == DBNull.Value ? string.Empty : Convert.ToString(row["RefId"]);
            _PLNo = row["PLNo"] == DBNull.Value ? string.Empty : Convert.ToString(row["PLNo"]);
            _Parent = row["Parent"] == DBNull.Value ? string.Empty : Convert.ToString(row["Parent"]);
            _Production = row["Production"] == DBNull.Value ? string.Empty : Convert.ToString(row["Production"]);
            _EqpId = row["EqpId"] == DBNull.Value ? string.Empty : Convert.ToString(row["EqpId"]);
            _CargoId = row["CargoId"] == DBNull.Value ? string.Empty : Convert.ToString(row["CargoId"]);
            _ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["ItemCode"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _Model = row["Model"] == DBNull.Value ? string.Empty : Convert.ToString(row["Model"]);
            _FromWhseCode = row["FromWhseCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["FromWhseCode"]);
            _FromWhseLocation = row["FromWhseLocation"] == DBNull.Value ? string.Empty : Convert.ToString(row["FromWhseLocation"]);
            _ToWhseCode = row["ToWhseCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["ToWhseCode"]);
            _ToWhseLocation = row["ToWhseLocation"] == DBNull.Value ? string.Empty : Convert.ToString(row["ToWhseLocation"]);
            _Quantity = row["Quantity"] == DBNull.Value ? (Nullable<Decimal>)null : Convert.ToDecimal(row["Quantity"]);
            _UOM = row["UOM"] == DBNull.Value ? string.Empty : Convert.ToString(row["UOM"]);
            _Remarks = row["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(row["Remarks"]);
            _Result = row["Result"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Result"]);
            _Repairable = row["Repairable"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Repairable"]);
            _ReasonCode = row["ReasonCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["ReasonCode"]);
            _Status = row["Status"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Status"]);
            _HasPrintOut = row["HasPrintOut"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["HasPrintOut"]);
            _PrintCount = row["PrintCount"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["PrintCount"]);
            _AutoTransfer = row["AutoTransfer"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AutoTransfer"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysModifier"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysProcessed = row["SysProcessed"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysProcessed"]);
        }

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public String Code
        {
            get { return _Code; }
            set { _Code = value; }
        }

        public String RefId
        {
            get { return _RefId; }
            set { _RefId = value; }
        }

        public String PLNo
        {
            get { return _PLNo; }
            set { _PLNo = value; }
        }

        public String Parent
        {
            get { return _Parent; }
            set { _Parent = value; }
        }

        public String Production
        {
            get { return _Production; }
            set { _Production = value; }
        }

        public String EqpId
        {
            get { return _EqpId; }
            set { _EqpId = value; }
        }

        public String CargoId
        {
            get { return _CargoId; }
            set { _CargoId = value; }
        }

        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }

        public String Model
        {
            get { return _Model; }
            set { _Model = value; }
        }

        public String ItemProd
        {
            get { return _ItemProd; }
            set { _ItemProd = value; }
        }

        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
        }

        public String FromWhseCode
        {
            get { return _FromWhseCode; }
            set { _FromWhseCode = value; }
        }

        public String FromWhseLocation
        {
            get { return _FromWhseLocation; }
            set { _FromWhseLocation = value; }
        }

        public String ToWhseCode
        {
            get { return _ToWhseCode; }
            set { _ToWhseCode = value; }
        }

        public String ToWhseLocation
        {
            get { return _ToWhseLocation; }
            set { _ToWhseLocation = value; }
        }

        public Nullable<Decimal> Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public String UOM
        {
            get { return _UOM; }
            set { _UOM = value; }
        }

        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public Nullable<Int32> Result
        {
            get { return _Result; }
            set { _Result = value; }
        }

        public Nullable<Boolean> Repairable
        {
            get { return _Repairable; }
            set { _Repairable = value; }
        }

        public String ReasonCode
        {
            get { return _ReasonCode; }
            set { _ReasonCode = value; }
        }

        public Nullable<Int32> Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Nullable<Boolean> HasPrintOut
        {
            get { return _HasPrintOut; }
            set { _HasPrintOut = value; }
        }

        public Nullable<Int32> PrintCount
        {
            get { return _PrintCount; }
            set { _PrintCount = value; }
        }

        public Nullable<Boolean> AutoTransfer
        {
            get { return _AutoTransfer; }
            set { _AutoTransfer = value; }
        }

        public Nullable<DateTime> SysCreated
        {
            get { return _SysCreated; }
            set { _SysCreated = value; }
        }

        public Nullable<DateTime> SysCreated2
        {
            get { return _SysCreated2; }
            set { _SysCreated2 = value; }
        }

        public String SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }

        public Nullable<DateTime> SysModified
        {
            get { return _SysModified; }
            set { _SysModified = value; }
        }

        public Nullable<DateTime> SysModified2
        {
            get { return _SysModified2; }
            set { _SysModified2 = value; }
        }

        public String SysModifier
        {
            get { return _SysModifier; }
            set { _SysModifier = value; }
        }

        public Nullable<DateTime> SysProcessed
        {
            get { return _SysProcessed; }
            set { _SysProcessed = value; }
        }

        public Nullable<Int32> Shift
        {
            get { return _Shift; }
            set { _Shift = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public IItemTransferLog ItemTransferLog
        {
            get { return _ItemTransferLog; }
            set { _ItemTransferLog = value; }
        }

        public static String GetNewLocationTransferSequence()
        {
            return ItemTransferDac.GetNewLocationTransferSequence();
        }

        public static String GetNewInterBranchTransferSequence()
        {
            return ItemTransferDac.GetNewInterBranchTransferSequence();
        }

        public static String GetNewProductionResultSequence()
        {
            return ItemTransferDac.GetNewProductionResultSequence();
        }

        public DataTable RetrieveAll()
        {
            return ItemTransferDac.Retrieve().Tables[0];
        }

        public DataTable Retrieve(String CargoId)
        {
            return ItemTransferDac.Retrieve(CargoId).Tables[0];
        }

        public DataTable Retrieve(Int32 Id)
        {
            return ItemTransferDac.Retrieve(Id).Tables[0];
        }

        public DataTable RetrieveForAutoTransfer(Int32 Id)
        {
            return ItemTransferDac.RetrieveForAutoTransfer(Id).Tables[0];
        }

        public DataTable RetrieveByRefId(String RefId)
        {
            return ItemTransferDac.RetriveByRefId(RefId).Tables[0];
        }

        public DataTable RetrieveByProduction(String Production)
        {
            return ItemTransferDac.RetriveByProduction(Production).Tables[0];
        }

        public DataTable RetrieveByParent(String Parent)
        {
            return ItemTransferDac.RetriveByParent(Parent).Tables[0];
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

            return ItemTransferDac.Retrieve(this, searchOption).Tables[0];
        }

        public Int32 Insert()
        {
            return ItemTransferDac.Insert(this);
        }

        public void Reprint()
        {
            ItemTransferDac.Reprint(this);
        }

        public void Confirm()
        {
            ItemTransferDac.Confirm(this);
        }

        public void Pick()
        {
            ItemTransferDac.Pick(this);
        }

        public void Process()
        {
            ItemTransferDac.Process(this);
        }

        public void Cancel()
        {
            ItemTransferDac.Cancel(this);
        }

        public void Fulfill()
        {
            ItemTransferDac.Fulfill(this);
        }
    }
}
