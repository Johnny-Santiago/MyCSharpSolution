///-----------------------------------------------------------------------------------------------------------------
///   Architecture Designed By:
///   Juanito U. Santiago Jr.
///   E-mail: juanitousantiagojr@gmail.com
///-----------------------------------------------------------------------------------------------------------------
///   Class Name:  Role.cs
///   Author:      Juanito U. Santiago Jr.
///   Create Date: 24-FEB-2015
///   Description: This file is the business rules and logic implementation of Role.
///   Version:     1
///-----------------------------------------------------------------------------------------------------------------

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data;
using Interface;
using DataAccess;

namespace Business
{
    public class Role : IRole
    {
        private DataTable _UserRoles;

        public Role()
        {
        }

        public Role(String UserId, String ProgramId)
        {
            _UserRoles = RoleDac.GetUserRoles(UserId, ProgramId).Tables[0];
        }

        public Role(String UserId)
        {
            _UserRoles = RoleDac.GetUserRoles(UserId).Tables[0];
        }

        public DataTable UserRoles
        {
            get { return _UserRoles; }
            set { _UserRoles = value; }
        }
    }
}
