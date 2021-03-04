///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  User.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 24-FEB-2015
///   Description: This file is the business rules and logic implementation of User.
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
    public class User : IUser
    {
        private Nullable<Int32> _Id;
        private String _UserID;
        private String _UserName;
        private String _Password;
        private String _ScanCode;
        private Nullable<Boolean> _Locked;
        private Nullable<Boolean> _Resigned;
        private DataTable _Info;
        private IRole _Roles;
        private IWarehouseDesignation _WarehouseDesignation;

        public User()
        {
        }

        public User(String UserId)
        {
            _Info = Retrieve(UserId);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                Role roles = new Role(_UserID);
                _Roles = (IRole)roles;

                WarehouseDesignation warehouseDesignation = new WarehouseDesignation(_UserID);
                _WarehouseDesignation = (IWarehouseDesignation)warehouseDesignation;
            }
            else
            {
                throw new KeyNotFoundException("User ID not found.");
            }
        }

        public User(String UserId, String ProgramId)
        {
            _Info = Retrieve(UserId);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                Role roles = new Role(_UserID, ProgramId);
                _Roles = (IRole)roles;

                if (!_Roles.UserRoles.HasRows())
                    throw new Exception("Access Denied.\n\nYou don't have permission to access the program.");

                WarehouseDesignation warehouseDesignation = new WarehouseDesignation(_UserID);
                _WarehouseDesignation = (IWarehouseDesignation)warehouseDesignation;
            }
            else
            {
                throw new KeyNotFoundException("User ID not found.");
            }
        }

        public User(String UserId, String Password, String ProgramId)
        {
            _Info = Retrieve(UserId);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);

                if (Password != _Password)
                    throw new Exception("Access Denied.");

                Role roles = new Role(_UserID, ProgramId);
                _Roles = (IRole)roles;

                if (!_Roles.UserRoles.HasRows())
                    throw new Exception("Access Denied.\n\nYou don't have permission to access the program.");

                WarehouseDesignation warehouseDesignation = new WarehouseDesignation(_UserID);
                _WarehouseDesignation = (IWarehouseDesignation)warehouseDesignation;
            }
            else
            {
                throw new KeyNotFoundException("User ID not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["Id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Id"]);
            _UserID = row["UserID"] == DBNull.Value ? string.Empty : Convert.ToString(row["UserID"]);
            _UserName = row["UserName"] == DBNull.Value ? string.Empty : Convert.ToString(row["UserName"]);
            _Password = row["Password"] == DBNull.Value ? string.Empty : Convert.ToString(row["Password"]);
            _ScanCode = row["ScanCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["ScanCode"]);
            _Locked = row["Locked"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Locked"]);
            _Resigned = row["Resigned"] == DBNull.Value ? (Nullable<Boolean>)null : Convert.ToBoolean(row["Resigned"]);
        }

        public Nullable<Int32> Id
        {
            get { return _Id; }
            set { _Id = value; }
        }

        public String UserID
        {
            get { return _UserID; }
            set { _UserID = value; }
        }

        public String UserName
        {
            get { return _UserName; }
            set { _UserName = value; }
        }

        public String Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        public String ScanCode
        {
            get { return _ScanCode; }
            set { _ScanCode = value; }
        }

        public Nullable<Boolean> Locked
        {
            get { return _Locked; }
            set { _Locked = value; }
        }

        public Nullable<Boolean> Resigned
        {
            get { return _Resigned; }
            set { _Resigned = value; }
        }

        public DataTable Info
        {
            get { return _Info; }
            set { _Info = value; }
        }

        public IRole Role
        {
            get { return _Roles; }
            set { _Roles = value; }
        }

        public IWarehouseDesignation WarehouseDesignation
        {
            get { return _WarehouseDesignation; } 
            set { _WarehouseDesignation = value; }
        }

        public DataTable Retrieve(String UserID)
        {
            return UserDac.Retrieve(UserID).Tables[0];
        }

        public static void GrantUserRoles(String UserId, String ProgramId, String Rights)
        {
            RoleDac.GrantUserRoles(UserId, ProgramId, Rights);
        }

        public static void UpdateWarehouseDesignations(String UserId, String Designations)
        {
            WarehouseDesignationDac.UpdateWarehouseDesignations(UserId, Designations);
        }

        public static bool IsInRole(DataTable Roles, string ProgramID, string ModuleName)
        {
            DataRow row = Roles.AsEnumerable().FirstOrDefault(r => String.Equals(r.Field<string>("ProgramID").ToLower(), ProgramID.ToLower()) && String.Equals(r.Field<string>("ModuleName").ToLower(), ModuleName.ToLower()) && r.Field<bool>("Authorized") == true);
            return row != null;
        }

        public static DataTable GetAllUsers()
        {
            return UserDac.RetrieveAll().Tables[0];
        }
    }
}
