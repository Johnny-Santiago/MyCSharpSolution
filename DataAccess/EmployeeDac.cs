using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;
using System.Data.SqlClient;
using Common;
using Interface;

namespace DataAccess
{
    public class EmployeeDac : Configuration
    {
        public static Int32 Insert(IEmployee Employee) 
        {
            SqlParameter[] _parameters = {
		        new SqlParameter("@EmployeeId", Employee.EmployeeId)
		        ,new SqlParameter("@LastName", Employee.LastName)
		        ,new SqlParameter("@FirstName", Employee.FirstName)
		        ,new SqlParameter("@Title", Employee.Title)
		        ,new SqlParameter("@TitleOfCourtesy", Employee.TitleOfCourtesy)
                ,new SqlParameter("@Email", Employee.Email)
                ,new SqlParameter("@Department", Employee.Department)
                ,new SqlParameter("@Section", Employee.Section)
		        ,new SqlParameter("@BirthDate", Employee.BirthDate)
		        ,new SqlParameter("@HireDate", Employee.HireDate)
		        ,new SqlParameter("@Address", Employee.Address)
		        ,new SqlParameter("@City", Employee.City)
		        ,new SqlParameter("@Region", Employee.Region)
		        ,new SqlParameter("@PostalCode", Employee.PostalCode)
		        ,new SqlParameter("@Country", Employee.Country)
		        ,new SqlParameter("@HomePhone", Employee.HomePhone)
		        ,new SqlParameter("@Extension", Employee.Extension)
		        ,new SqlParameter("@Photo", Employee.Photo)
		        ,new SqlParameter("@Notes", Employee.Notes)
		        ,new SqlParameter("@ReportsTo", Employee.ReportsTo)
            };

            return Convert.ToInt32(SqlHelper.ExecuteScalar(ConnectionString, "sp_Employees_InsertEmployee", _parameters));
        }

        public static void Update(IEmployee Employee)
        {
            SqlParameter[] _parameters = {
                new SqlParameter("@Id", Employee.Id)
		        ,new SqlParameter("@EmployeeId", Employee.EmployeeId)
		        ,new SqlParameter("@LastName", Employee.LastName)
		        ,new SqlParameter("@FirstName", Employee.FirstName)
		        ,new SqlParameter("@Title", Employee.Title)
		        ,new SqlParameter("@TitleOfCourtesy", Employee.TitleOfCourtesy)
                ,new SqlParameter("@Email", Employee.Email)
                ,new SqlParameter("@Department", Employee.Department)
                ,new SqlParameter("@Section", Employee.Section)
		        ,new SqlParameter("@BirthDate", Employee.BirthDate)
		        ,new SqlParameter("@HireDate", Employee.HireDate)
		        ,new SqlParameter("@Address", Employee.Address)
		        ,new SqlParameter("@City", Employee.City)
		        ,new SqlParameter("@Region", Employee.Region)
		        ,new SqlParameter("@PostalCode", Employee.PostalCode)
		        ,new SqlParameter("@Country", Employee.Country)
		        ,new SqlParameter("@HomePhone", Employee.HomePhone)
		        ,new SqlParameter("@Extension", Employee.Extension)
		        ,new SqlParameter("@Photo", Employee.Photo)
		        ,new SqlParameter("@Notes", Employee.Notes)
		        ,new SqlParameter("@ReportsTo", Employee.ReportsTo)
            };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Employees_UpdateById", _parameters);
        }

        public static void Delete(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@Id", Id) };
            SqlHelper.ExecuteNonQuery(ConnectionString, "sp_Employees_DeleteById", _parameters);
        }

        public static DataSet Retrieve()
        {
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Employees_SelectEmployees");
        }

        public static DataSet Retrieve(Int32 Id)
        {
            SqlParameter[] _parameters = { new SqlParameter("@ID", Id) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Employees_SelectById", _parameters);
        }

        public static DataSet Retrieve(String EmployeeId)
        {
            SqlParameter[] _parameters = { new SqlParameter("@EmployeeId", EmployeeId) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Employees_SelectByEmployeeId", _parameters);
        }

        public static DataSet Retrieve(IEmployee Employee)
        {
            SqlParameter[] _parameters = { new SqlParameter("@Email", Employee.Email) };
            return SqlHelper.ExecuteDataset(ConnectionString, "sp_Employees_SelectByEmail", _parameters);
        }
    }
}