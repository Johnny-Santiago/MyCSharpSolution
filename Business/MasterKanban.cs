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
    public class MasterKanban : IMasterKanban
    {
        private String _ItemCode;
        private Nullable<Int32> _SNP;
        private String _SysModifier;

        public MasterKanban()
        {
        }

        public Int32 Update()
        {
            return MasterKanbanDac.Update(this);
        }

        public String ItemCode
        {
            get
            {
                return _ItemCode;
            }
            set
            {
                _ItemCode = value;
            }
        }

        public Nullable<Int32> SNP
        {
            get
            {
                return _SNP;
            }
            set
            {
                _SNP = value;
            }
        }

        public String SysModifier
        {
            get
            {
                return _SysModifier; 
            }
            set
            {
                _SysModifier = value;
            }
        }
    }
}