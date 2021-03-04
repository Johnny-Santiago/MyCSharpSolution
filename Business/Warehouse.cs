using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using DataAccess;
using Interface;
using Extensions;

namespace Business
{
    public class Warehouse : IWarehouse
    {
        private String _WhseCode;
        private String _warehouse;
        private String _WhseLocation;
        private String _Location;
        private DataTable _Items;
        private DataTable _Locations;

        public Warehouse()
        {
        }

        public Warehouse(String WhseCode)
        {
            _Items = WarehouseDac.RetrieveItems(WhseCode).Tables[0];
            _Locations = WarehouseDac.RetrieveLocations(WhseCode).Tables[0];
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

            return WarehouseDac.Retrieve(this, searchOption).Tables[0];
        }

        public DataTable NewLocRetrieve(SearchOption SearchOption)
        {
            Int32 searchOption;
            switch (SearchOption)
            {
                case SearchOption.StartsWith: searchOption = 2; break;
                case SearchOption.Contains: searchOption = 3; break;
                case SearchOption.EndsWith: searchOption = 4; break;
                default: searchOption = 1; break;
            }

            return WarehouseDac.NewLocRetrieve(this, searchOption).Tables[0];
        }

        public DataTable NewRetrieve(SearchOption SearchOption)
        {
            Int32 searchOption;
            switch (SearchOption)
            {
                case SearchOption.StartsWith: searchOption = 2; break;
                case SearchOption.Contains: searchOption = 3; break;
                case SearchOption.EndsWith: searchOption = 4; break;
                default: searchOption = 1; break;
            }

            return WarehouseDac.NewRetrieve(this, searchOption).Tables[0];
        }

        public String WhseCode
        {
            get { return _WhseCode; }
            set { _WhseCode = value; }
        }

        public String warehouse
        {
            get { return _warehouse; }
            set { _warehouse = value; }
        }

        public String WhseLocation
        {
            get { return _WhseLocation; }
            set { _WhseLocation = value; }
        }

        public String Location
        {
            get { return _Location; }
            set { _Location = value; }
        }

        public DataTable Items
        {
            get { return _Items; }
            set { _Items = value; }
        }

        public DataTable Locations
        {
            get { return _Locations; }
            set { _Locations = value; }
        }

        public static DataTable Retrieve()
        {
            return WarehouseDac.Retrieve().Tables[0];
        }

        public static DataTable Retrieve(String Warehouse, String Description, SearchOption SearchOption)
        {
            Int32 searchOption;
            switch (SearchOption)
            {
                case SearchOption.StartsWith: searchOption = 2; break;
                case SearchOption.Contains: searchOption = 3; break;
                case SearchOption.EndsWith: searchOption = 4; break;
                default: searchOption = 1; break;
            }

            return WarehouseDac.Retrieve(Warehouse, Description, searchOption).Tables[0];
        }
    }
}