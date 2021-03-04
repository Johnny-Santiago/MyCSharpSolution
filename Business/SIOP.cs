///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  SIOP.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 16-MAY-2016
///   Description: This file is the business rules and logic implementation of SIOP.
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
    public class SIOP : ISIOP
    {
        private Nullable<Int32> _ID;
        private Nullable<Int32> _Year;
        private Nullable<Int32> _Period;
        private String _WorkCenterCode;
        private String _WorkCenterDescription;
        private String _WhseCode;
        private String _WhseLocation;
        private String _ItemCode;
        private String _Description;
        private String _Model;
        private Nullable<Decimal> _Quantity;
        private String _SIOPID;
        private String _NextSIOPID;
        private Nullable<DateTime> _DateFrom;
        private Nullable<DateTime> _DateTo;
        private Nullable<Int32> _Coverage;
        private String _PlanType;
        private String _PlanDescription;
        private String _Remarks;
        private Nullable<Int32> _Status;
        private Nullable<DateTime> _SysCreated;
        private String _SysCreator;
        private Nullable<DateTime> _SysModified;
        private String _SysModifier;
        private Nullable<Guid> _SysGuid;
        private Nullable<Boolean> _AllowCreateNew;
        private Nullable<Boolean> _AllowViewCurrentVersion;
        private Nullable<Boolean> _AllowArbitrateWorkingVersion;
        private Nullable<Boolean> _AllowViewArchiveVersions;
        private Nullable<Boolean> _AllowInitialization;
        private Nullable<Boolean> _AllowArbitration;
        private Nullable<Boolean> _AllowViewReport;
        private Nullable<Boolean> _AllowReview;
        private Nullable<Boolean> _AllowApproval;
        private Nullable<DateTime> _DateLastReviewed;
        private Nullable<DateTime> _DateLastApproved;
        private Nullable<DateTime> _DateLastArbitrated;
        private Nullable<DateTime> _DateSettingsLastUpdated;
        private String _Message;
        private Nullable<Boolean> _InitializationFailed;
        private String _ReportType;
        private DataTable _Items;
        private DataTable _Warehouses;
        private DataTable _ArchiveVersions;

        public SIOP()
        {
        }

        public SIOP(String SIOPID, String SysCreator)
        {
            DataTable dt = Retrieve(SIOPID, SysCreator);
            if (dt.HasRows())
            {
                InitializeProperties(dt.Rows[0]);
            }
            else
            {
                throw new Exception("An error is encountered during initialization of SIOP.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _ID = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _SIOPID = row["SIOPID"] == DBNull.Value ? string.Empty : Convert.ToString(row["SIOPID"]);
            _DateFrom = row["DateFrom"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateFrom"]);
            _DateTo = row["DateTo"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateTo"]);
            _Coverage = row["Coverage"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Coverage"]);
            _PlanType = row["PlanType"] == DBNull.Value ? string.Empty : Convert.ToString(row["PlanType"]);
            _PlanDescription = row["PlanDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["PlanDescription"]);
            _Remarks = row["Remarks"] == DBNull.Value ? string.Empty : Convert.ToString(row["Remarks"]);
            _Status = row["Status"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Status"]);
            _SysCreated = row["SysCreated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysCreated"]);
            _SysCreator = row["SysCreator"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysCreator"]);
            _SysModified = row["SysModified"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["SysModified"]);
            _SysModifier = row["SysModifier"] == DBNull.Value ? string.Empty : Convert.ToString(row["SysModifier"]);
            _SysGuid = row["SysGuid"] == DBNull.Value ? (Nullable<Guid>)null : (Guid)(row["SysGuid"]);
            _DateLastReviewed = row["DateLastReviewed"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateLastReviewed"]);
            _DateLastApproved = row["DateLastApproved"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateLastApproved"]);
            _DateLastArbitrated = row["DateLastArbitrated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateLastArbitrated"]);
            _DateSettingsLastUpdated = row["DateSettingsLastUpdated"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["DateSettingsLastUpdated"]);
            _AllowInitialization = row["AllowInitialization"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowInitialization"]);
            _AllowArbitration = row["AllowArbitration"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowArbitration"]);
            _AllowViewReport = row["AllowViewReport"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowViewReport"]);
            _AllowReview = row["AllowReview"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowReview"]);
            _AllowApproval = row["AllowApproval"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowApproval"]);
        }

        private void InitializeOptions(DataRow row)
        {
            _SIOPID = row["SIOPID"] == DBNull.Value ? string.Empty : Convert.ToString(row["SIOPID"]);
            _NextSIOPID = row["NextSIOPID"] == DBNull.Value ? string.Empty : Convert.ToString(row["NextSIOPID"]);
            _AllowCreateNew = row["AllowCreateNew"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowCreateNew"]);
            _AllowViewCurrentVersion = row["AllowViewCurrentVersion"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowViewCurrentVersion"]);
            _AllowArbitrateWorkingVersion = row["AllowArbitrateWorkingVersion"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowArbitrateWorkingVersion"]);
            _AllowViewArchiveVersions = row["AllowViewArchiveVersions"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["AllowViewArchiveVersions"]);
        }

        public DataTable Retrieve(String SIOPID, String SysCreator)
        {
            return SIOPDac.Retrieve(SIOPID, SysCreator).Tables[0];
        }

        public static DataTable RetrieveFormatOnly()
        {
            return SIOPDac.RetrieveFormatOnly().Tables[0];
        }

        public static DataTable RetrieveGroupFormatOnly()
        {
            return SIOPDac.RetrieveGroupFormatOnly().Tables[0];
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

            return SIOPDac.Retrieve(this, searchOption).Tables[0];
        }

        public DataTable RetrieveGroup(SearchOption SearchOption)
        {
            Int32 searchOption;
            switch (SearchOption)
            {
                case SearchOption.StartsWith: searchOption = 2; break;
                case SearchOption.Contains: searchOption = 3; break;
                case SearchOption.EndsWith: searchOption = 4; break;
                default: searchOption = 1; break;
            }

            return SIOPDac.RetrieveGroup(this, searchOption).Tables[0];
        }

        public void Arbitrate()
        {
            DataSet ds = SIOPDac.Arbitrate(this);
            _Items = ds.Tables[0];
            _Warehouses = ds.Tables[1];
        }

        public void ArbitrateByGroup()
        {
            DataSet ds = SIOPDac.ArbitrateByGroup(this);
            _Items = ds.Tables[0];
            _Warehouses = ds.Tables[1];
        }

        public void ArbitrateByPart()
        {
            DataSet ds = SIOPDac.ArbitrateByPart(this);
            _Items = ds.Tables[0];
            _Warehouses = ds.Tables[1];
        }

        public static string GetPeriod(Int32 Sequence)
        {
            return SIOPDac.GetPeriod(Sequence);
        }

        public static Int32 GetPeriodCoverage()
        {
            return SIOPDac.GetPeriodCoverage();
        }

        public void Initialize()
        {
            SIOPDac.Initialize(this);
        }

        public void Review()
        {
            SIOPDac.Review(this);
        }

        public void Approve()
        {
            SIOPDac.Approve(this);
        }

        public void GetOptions()
        {
            DataTable dt = SIOPDac.GetOptions().Tables[0];
            if (dt.HasRows())
            {
                InitializeOptions(dt.Rows[0]);
            }
            else
            {
                throw new Exception("An error is encountered getting SIOP options.");
            }
        }

        public static DataTable GetPlanningTypes()
        {
            return SIOPDac.GetPlanningTypes().Tables[0];
        }

        public void GetMessages()
        {
            DataRow row = SIOPDac.GetMessages().Tables[0].Rows[0];
            _Message = row["Message"] == DBNull.Value ? string.Empty : Convert.ToString(row["Message"]);
            _InitializationFailed = row["InitializationFailed"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["InitializationFailed"]);
        }

        public static Boolean HasInitialized(String SIOPID)
        {
            return SIOPDac.HasInitialized(SIOPID);
        }

        public static DataTable GetArchiveVersions(String SIOPID = "")
        {
            return SIOPDac.GetArchiveVersions(SIOPID).Tables[0];
        }

        public static DataTable GetWorkCenters()
        {
            return SIOPDac.GetWorkCenters().Tables[0];
        }

        public static DataTable GetDistinctWorkCenters()
        {
            return SIOPDac.GetDistinctWorkCenters().Tables[0];
        }

        public static DataTable GetLineAndMachines()
        {
            return SIOPDac.GetLineAndMachines().Tables[0];
        }

        public static DataTable GetDistinctLineAndMachines()
        {
            return SIOPDac.GetDistinctLineAndMachines().Tables[0];
        }

        public static DataTable GetUserColumns(string SysCreator)
        {
            return SIOPDac.GetUserColumns(SysCreator).Tables[0];
        }

        public static void SetUserColumns(String Xml, String SysCreator)
        {
            SIOPDac.SetUserColumns(Xml, SysCreator);
        }

        public Nullable<Int32> ID
        {
            get { return _ID; }
            set { _ID = value; }
        }

        public Nullable<Int32> Year
        {
            get { return _Year; }
            set { _Year = value; }
        }

        public Nullable<Int32> Period
        {
            get { return _Period; }
            set { _Period = value; }
        }

        public String WorkCenterCode
        {
            get { return _WorkCenterCode; }
            set { _WorkCenterCode = value; }
        }

        public String WorkCenterDescription
        {
            get { return _WorkCenterDescription; }
            set { _WorkCenterDescription = value; }
        }

        public String WhseCode
        {
            get { return _WhseCode; }
            set { _WhseCode = value; }
        }

        public String WhseLocation
        {
            get { return _WhseLocation; }
            set { _WhseLocation = value; }
        }

        public String ItemCode
        {
            get { return _ItemCode; }
            set { _ItemCode = value; }
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

        public Nullable<Decimal> Quantity
        {
            get { return _Quantity; }
            set { _Quantity = value; }
        }

        public String SIOPID
        {
            get { return _SIOPID; }
            set { _SIOPID = value; }
        }

        public String NextSIOPID
        {
            get { return _NextSIOPID; }
            set { _NextSIOPID = value; }
        }

        public Nullable<DateTime> DateFrom
        {
            get { return _DateFrom; }
            set { _DateFrom = value; }
        }

        public Nullable<DateTime> DateTo
        {
            get { return _DateTo; }
            set { _DateTo = value; }
        }

        public Nullable<Int32> Coverage
        {
            get { return _Coverage; }
            set { _Coverage = value; }
        }

        public String PlanType
        {
            get { return _PlanType; }
            set { _PlanType = value; }
        }

        public String PlanDescription
        {
            get { return _PlanDescription; }
            set { _PlanDescription = value; }
        }

        public String Remarks
        {
            get { return _Remarks; }
            set { _Remarks = value; }
        }

        public Nullable<Int32> Status
        {
            get { return _Status; }
            set { _Status = value; }
        }

        public Nullable<DateTime> SysCreated
        {
            get { return _SysCreated; }
            set { _SysCreated = value; }
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

        public String SysModifier
        {
            get { return _SysModifier; }
            set { _SysModifier = value; }
        }

        public Nullable<Guid> SysGuid
        {
            get { return _SysGuid; }
            set { _SysGuid = value; }
        }

        public Nullable<Boolean> AllowCreateNew
        {
            get { return _AllowCreateNew; }
            set { _AllowCreateNew = value; }
        }

        public Nullable<Boolean> AllowViewCurrentVersion
        {
            get { return _AllowViewCurrentVersion; }
            set { _AllowViewCurrentVersion = value; }
        }

        public Nullable<Boolean> AllowArbitrateWorkingVersion
        {
            get { return _AllowArbitrateWorkingVersion; }
            set { _AllowArbitrateWorkingVersion = value; }
        }

        public Nullable<Boolean> AllowViewArchiveVersions
        {
            get { return _AllowViewArchiveVersions; }
            set { _AllowViewArchiveVersions = value; }
        }

        public Nullable<Boolean> AllowInitialization
        {
            get { return _AllowInitialization; }
            set { _AllowInitialization = value; }
        }

        public Nullable<Boolean> AllowArbitration
        {
            get { return _AllowArbitration; }
            set { _AllowArbitration = value; }
        }

        public Nullable<Boolean> AllowViewReport
        {
            get { return _AllowViewReport; }
            set { _AllowViewReport = value; }
        }

        public Nullable<Boolean> AllowReview
        {
            get { return _AllowReview; }
            set { _AllowReview = value; }
        }


        public Nullable<Boolean> AllowApproval
        {
            get { return _AllowApproval; }
            set { _AllowApproval = value; }
        }

        public Nullable<DateTime> DateLastReviewed
        {
            get { return _DateLastReviewed; }
            set { _DateLastReviewed = value; }
        }

        public Nullable<DateTime> DateLastApproved
        {
            get { return _DateLastApproved; }
            set { _DateLastApproved = value; }
        }

        public Nullable<DateTime> DateLastArbitrated
        {
            get { return _DateLastArbitrated; }
            set { _DateLastArbitrated = value; }
        }

        public Nullable<DateTime> DateSettingsLastUpdated
        {
            get { return _DateSettingsLastUpdated; }
            set { _DateSettingsLastUpdated = value; }
        }

        public String Message
        {
            get { return _Message; }
            set { _Message = value; }
        }

        public Nullable<Boolean> InitializationFailed
        {
            get { return _InitializationFailed; }
            set { _InitializationFailed = value; }
        }

        public String ReportType
        {
            get { return _ReportType; }
            set { _ReportType = value; }
        }


        public DataTable Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public DataTable Warehouses
        {
            get { return _Warehouses; }
            set { _Warehouses = value; }
        }

        public DataTable ArchiveVersions
        {
            get { return _ArchiveVersions; }
            set { _ArchiveVersions = value; }
        }
    }
}