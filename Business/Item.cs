///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  Item.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 04-MAR-2015
///   Description: This file is the business rules and logic implementation of Item.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------
///   Modified Date    : 09-MAR-2015
///   Modifier         : Usep Haris Nugraha.
///   Changelog Desc   : Add Setter for Search purpose 
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
    public class Item : IItem
    {
        private Nullable<Int32> _Id;
        private String _ItemCode;
        private String _Description;
        private String _Model;
        private String _Type;
        private String _Classification;
        private String _ClassificationDescription;
        private String _Family;
        private String _FamilyDescription;
        private String _Category;
        private String _CategoryDescription;
        private String _Position;
        private String _PositionDescription;
        private String _ItemClassType;
        private String _ItemClassTypeDescription;
        private String _Identity;
        private String _IdentityDescription;
        private String _Warehouse;
        private String _Location;
        private String _Location2;
        private String _UOM;
        private DataTable _Info;

        public Item()
        {
        }

        public Item(Int32 Id)
        {
            _Info = Retrieve(Id);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("Id not found.");
            }
        }

        public Item(String ItemCode)
        {
            _Info = Retrieve(ItemCode);
            if (_Info.HasRows())
            {
                InitializeProperties(_Info.Rows[0]);
            }
            else
            {
                throw new KeyNotFoundException("ItemCode not found.");
            }
        }

        private void InitializeProperties(DataRow row)
        {
            _Id = row["Id"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["Id"]);
            _ItemCode = row["ItemCode"] == DBNull.Value ? string.Empty : Convert.ToString(row["ItemCode"]);
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _Description = row["Model"] == DBNull.Value ? string.Empty : Convert.ToString(row["Model"]);
            _Type = row["Type"] == DBNull.Value ? string.Empty : Convert.ToString(row["Type"]);
            _Classification = row["Classification"] == DBNull.Value ? string.Empty : Convert.ToString(row["Classification"]);
            _ClassificationDescription = row["ClassificationDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["ClassificationDescription"]);
            _Family = row["Family"] == DBNull.Value ? string.Empty : Convert.ToString(row["Family"]);
            _FamilyDescription = row["FamilyDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["FamilyDescription"]);
            _Category = row["Category"] == DBNull.Value ? string.Empty : Convert.ToString(row["Category"]);
            _CategoryDescription = row["CategoryDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["CategoryDescription"]);
            _Position = row["Position"] == DBNull.Value ? string.Empty : Convert.ToString(row["Position"]);
            _PositionDescription = row["PositionDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["PositionDescription"]);
            _ItemClassType = row["ItemClassType"] == DBNull.Value ? string.Empty : Convert.ToString(row["ItemClassType"]);
            _ItemClassTypeDescription = row["ItemClassTypeDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["ItemClassTypeDescription"]);
            _Identity = row["Identity"] == DBNull.Value ? string.Empty : Convert.ToString(row["Identity"]);
            _IdentityDescription = row["IdentityDescription"] == DBNull.Value ? string.Empty : Convert.ToString(row["IdentityDescription"]);
            _Warehouse = row["WareHouse"] == DBNull.Value ? string.Empty : Convert.ToString(row["WareHouse"]);
            _Location = row["Location"] == DBNull.Value ? string.Empty : Convert.ToString(row["Location"]);
            _UOM = row["UOM"] == DBNull.Value ? string.Empty : Convert.ToString(row["UOM"]);
        }

        public Nullable<Int32> Id
        {
            get { return _Id; }
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

        public String Type
        {
            get { return _Type; }
            set { _Type = value; }
        }

        public String Classification
        {
            get { return _Classification; }
            set { _Classification = value; }
        }

        public String ClassificationDescription
        {
            get { return _ClassificationDescription; }
        }

        public String Family
        {
            get { return _Family; }
            set { _Family = value; }
        }

        public String FamilyDescription
        {
            get { return _FamilyDescription; }
        }

        public String Category
        {
            get { return _Category; }
            set { _Category = value; }
        }

        public String CategoryDescription
        {
            get { return _CategoryDescription; }
        }

        public String Position
        {
            get { return _Position; }
            set { _Position = value; }
        }

        public String PositionDescription
        {
            get { return _PositionDescription; }
        }

        public String ItemClassType
        {
            get { return _ItemClassType; }
            set { _ItemClassType = value; }
        }

        public String ItemClassTypeDescription
        {
            get { return _ItemClassTypeDescription; }
        }

        public String Identity
        {
            get { return _Identity; }
            set { _Identity = value; }
        }

        public String IdentityDescription
        {
            get { return _IdentityDescription; }
        }

        public String Warehouse
        {
            get { return _Warehouse; }
            set { _Warehouse = value; }
        }

        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public String Location2
        {
            get { return _Location2; }
            set { _Location2 = value; }
        }

        public String UOM
        {
            get { return _UOM; }
        }

        public DataTable Info
        {
            get { return _Info; }
        }

        public DataTable Retrieve(String ItemCode)
        {
            return ItemDac.Retrieve(ItemCode).Tables[0];
        }

        public DataTable Retrieve(Int32 Id)
        {
            return ItemDac.Retrieve(Id).Tables[0];
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

            return ItemDac.Retrieve(this, searchOption).Tables[0];
        }

        public DataTable Retrieve()
        {
            return ItemDac.Retrieve(this).Tables[0];
        }

        public Decimal GetAvailableStock() 
        {
            return ItemDac.GetAvailableStock(this);
        }

        public DataTable GetItemsHasChild()
        {
            return ItemDac.RetrieveAllHasChild().Tables[0];
        }

        public DataTable GetItemsBOM()
        {
            return ItemDac.RetrieveAllBOM().Tables[0];
        }
    }
}
