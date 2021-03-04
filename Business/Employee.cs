using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using Extensions;
using DataAccess;
using Interface;

namespace Business
{
    public class Employee : IEmployee
    {
        private Nullable<Int32> _Id;
        private Nullable<Int32> _ResourceID;
        private String _EmployeeId;
        private String _LastName;
        private String _FirstName;
        private String _Fullname;
        private String _Title;
        private String _TitleOfCourtesy;
        private String _Email;
        private String _Department;
        private String _Section;
        private Nullable<DateTime> _BirthDate; 
        private Nullable<DateTime> _HireDate;
        private String _Address;
        private String _City;
        private String _Region;
        private String _PostalCode;
        private String _Country;
        private String _HomePhone;
        private String _Extension;
        private Byte[] _Photo;
        private String _Notes;
        private Nullable<Int32> _ReportsTo;
        private DataTable _Info;
        private IReportingDepartment _ReportingDepartment;
        private IWarehouseDesignation _WarehouseDesignation;

        public Employee()
        {
        }


        public Employee(Int32 Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                ReportingDepartment reportingDepartments = new ReportingDepartment(_ResourceID);
                _ReportingDepartment = reportingDepartments;

                WarehouseDesignation warehouseDesignation = new WarehouseDesignation(_EmployeeId);
                _WarehouseDesignation = (IWarehouseDesignation)warehouseDesignation;
            }
            else
            {
                throw new KeyNotFoundException("Id not found.");
            }
        }

        public Employee(String EmployeeId)
        {
            _Info = Retrieve(EmployeeId);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Employee Id not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["ID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ID"]);
            _ResourceID = row["ResourceID"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ResourceID"]);
            _EmployeeId = row["EmployeeId"] == DBNull.Value ? string.Empty : Convert.ToString(row["EmployeeId"]);
            _LastName = row["LastName"] == DBNull.Value ? string.Empty : Convert.ToString(row["LastName"]);
            _FirstName = row["FirstName"] == DBNull.Value ? string.Empty : Convert.ToString(row["FirstName"]);
            _Fullname = row["Fullname"] == DBNull.Value ? string.Empty : Convert.ToString(row["Fullname"]);
            _Title = row["Title"] == DBNull.Value ? string.Empty : Convert.ToString(row["Title"]);
            _TitleOfCourtesy = row["TitleOfCourtesy"] == DBNull.Value ? string.Empty : Convert.ToString(row["TitleOfCourtesy"]);
            _Email = row["Email"] == DBNull.Value ? string.Empty : Convert.ToString(row["Email"]);
            _Department = row["Department"] == DBNull.Value ? string.Empty : Convert.ToString(row["Department"]);
            _Section = row["Section"] == DBNull.Value ? string.Empty : Convert.ToString(row["Section"]);
            _BirthDate = row["BirthDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["BirthDate"]);
            _HireDate = row["HireDate"] == DBNull.Value ? (Nullable<DateTime>)null : Convert.ToDateTime(row["HireDate"]);
            _Address = row["Address"] == DBNull.Value ? string.Empty : Convert.ToString(row["Address"]);
            _City = row["City"] == DBNull.Value ? string.Empty : Convert.ToString(row["City"]);
            _Region = row["Region"] == DBNull.Value ? string.Empty : Convert.ToString(row["Region"]);
            _PostalCode = row["PostalCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["PostalCode"]);
            _Country = row["Country"] == DBNull.Value ? string.Empty : Convert.ToString(row["Country"]);
            _HomePhone = row["HomePhone"] == DBNull.Value ? string.Empty : Convert.ToString(row["HomePhone"]);
            _Extension = row["Extension"] == DBNull.Value ? string.Empty : Convert.ToString(row["Extension"]);
            _Photo = row["Photo"] == DBNull.Value ? null : (Byte[])row["Photo"];
            _Notes = row["Notes"] == DBNull.Value ? string.Empty : Convert.ToString(row["Notes"]);
            _ReportsTo = row["ReportsTo"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ReportsTo"]);
        }

        public Nullable<Int32> Id 
        { 
            get { return _Id; } 
            set { _Id = value; } 
        }

        public Nullable<Int32> ResourceID 
        {
            get { return _ResourceID; }
            set { _ResourceID = value; } 
        }

        public String EmployeeId 
        { 
            get { return _EmployeeId; } 
            set { _EmployeeId = value; } 
        }

        public String LastName 
        { 
            get { return _LastName; } 
            set { _LastName = value; } 
        }

        public String FirstName 
        { 
            get { return _FirstName; } 
            set { _FirstName = value; } 
        }

        public String Fullname 
        {
            get { return _Fullname; } 
            set { _Fullname = value; } 
        }

        public String Title 
        { 
            get { return _Title; } 
            set { _Title = value; } 
        }

        public String TitleOfCourtesy 
        { 
            get { return _TitleOfCourtesy; } 
            set { _TitleOfCourtesy = value; } 
        }

        public String Email
        {
            get { return _Email; }
            set { _Email = value; }
        }

        public String Department
        {
            get { return _Department; }
            set { _Department = value; }
        }

        public String Section
        {
            get { return _Section; }
            set { _Section = value; }
        }

        public Nullable<DateTime> BirthDate 
        { 
            get { return _BirthDate; } 
            set { _BirthDate = value; } 
        }

        public Nullable<DateTime> HireDate
        { 
            get { return _HireDate; } 
            set { _HireDate = value; } 
        }

        public String Address 
        { 
            get { return _Address; } 
            set { _Address = value; } 
        }

        public String City 
        { 
            get { return _City; } 
            set { _City = value; } 
        }

        public String Region 
        { 
            get { return _Region; } 
            set { _Region = value; } 
        }

        public String PostalCode 
        { 
            get { return _PostalCode; } 
            set { _PostalCode = value; } 
        }

        public String Country 
        { 
            get { return _Country; } 
            set { _Country = value; } 
        }

        public String HomePhone 
        { 
            get { return _HomePhone; } 
            set { _HomePhone = value; } 
        }

        public String Extension 
        { 
            get { return _Extension; } 
            set { _Extension = value; } 
        }

        public Byte[] Photo 
        { 
            get { return _Photo; } 
            set { _Photo = value; } 
        }

        public String Notes 
        { 
            get { return _Notes; } 
            set { _Notes = value; } 
        }

        public Nullable<Int32> ReportsTo 
        { 
            get { return _ReportsTo; } 
            set { _ReportsTo = value; } 
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public IReportingDepartment ReportingDepartment
        {
            get { return _ReportingDepartment; }
            set { _ReportingDepartment = value; }
        }

        public IWarehouseDesignation WarehouseDesignation
        {
            get { return _WarehouseDesignation; }
            set { _WarehouseDesignation = value; }
        }

        public Int32 Insert()
        {
            return EmployeeDac.Insert(this); 
        }

        public void Update()
        {
            EmployeeDac.Update(this);
        }

        public void Delete(Int32 Id)
        {
            EmployeeDac.Delete(Id);
        }

        public DataTable RetrieveAll()  
        {
            return EmployeeDac.Retrieve().Tables[0];
        }

        public DataTable Retrieve(Int32 Id)
        {
            return EmployeeDac.Retrieve(Id).Tables[0];
        }

        public DataTable Retrieve(String EmployeeId)
        {
            return EmployeeDac.Retrieve(EmployeeId).Tables[0];
        }

        public void Retrieve()
        {
            _Info = EmployeeDac.Retrieve(this).Tables[0];
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                ReportingDepartment reportingDepartments = new ReportingDepartment(_ResourceID);
                _ReportingDepartment = reportingDepartments;

                WarehouseDesignation warehouseDesignation = new WarehouseDesignation(_EmployeeId);
                _WarehouseDesignation = (IWarehouseDesignation)warehouseDesignation;
            }
            else
            {
                throw new KeyNotFoundException("Email not found.");
            }
        }
    }
}