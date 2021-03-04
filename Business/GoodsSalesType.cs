///-----------------------------------------------------------------------------------------------------------------
///   Interface:   Shift.cs
///   Author:      Karsito.
///   Create Date: 19-Nov-2015
///   Description: This file is the Business for Sales Type of Goods.
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
    public class GoodsSalesType :IGoodsSalesType
    {
        private String _GoodsType;

        public GoodsSalesType()
        {
 
        }
        public void InitaliezeProperties(DataRow row)
        {
            _GoodsType = row["GoodsType"] == DBNull.Value ? string.Empty : Convert.ToString(row["GoodsType"]);
        }
        public String GoodsType
        {
            get { return _GoodsType; }
            set { value = _GoodsType; }
        }
        public DataTable Retrieve()
        {
            return GoodsSalesTypesDac.Retrieve().Tables[0];
        }
    }
}