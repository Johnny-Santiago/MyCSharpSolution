///-----------------------------------------------------------------------------------------------------------------
///   Created by Karsito           Dec 07, 2015                ProductionDescriptionDac.cs
///                                                            Use for Reporting Production or Itemtransfer
///                                                            
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
    public class ProductionDescription : IProductionDescription
    {
        private String _Description;
        private Nullable<Int32> _ResultId;

        public ProductionDescription()
        { 
        }
        private void InitializedProperties(DataRow row)
        {
            _Description = row["Description"] == DBNull.Value ? string.Empty : Convert.ToString(row["Description"]);
            _ResultId = row["ResultId"] == DBNull.Value ? (Nullable<Int32>)null : Convert.ToInt32(row["ResultId"]);
        }
        public String Description
        {
            get { return _Description; }
            set { _Description = value; }
        }
        public Nullable<Int32> ResultId
        {
            get { return _ResultId; }
            set { _ResultId = value; }
        }
        public DataTable Retrieve()
        {
            return ProductionDescriptionDac.Retrieve().Tables[0];
        }
        public DataTable Retrieve(int ResultId)
        {
            return ProductionDescriptionDac.Retrieve(ResultId).Tables[0];
        }
    }
}