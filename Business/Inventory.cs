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
    public class Inventory :IInventory
    {
        private Nullable<DateTime> _datereport;
        private String _SysCreator;

        public Inventory()
        {
 
        }
        public Nullable<DateTime> datereport
        {
            get { return _datereport; }
            set { _datereport = value; }
        }
        public String SysCreator
        {
            get { return _SysCreator; }
            set { _SysCreator = value; }
        }
        public Int32 Insert()
        {
            return  InventoryDac.Insert(this);
        }
    }
}