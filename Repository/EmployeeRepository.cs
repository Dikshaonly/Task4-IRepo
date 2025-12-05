using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc.Rendering;
using Dapper;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Task4.Models;

namespace Task4.Repository{
    public class EmployeeRepository : IEmployeeRepository{
        private string connstr;
        public EmployeeRepository(){
            connstr = "Server=182.93.94.30;Database = EMS;User Id=sa;Password=bdnquery;TrustServerCertificate=True;";
        }
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            using (var conn = new SqlConnection(connstr)){
            conn.Open();
            string sql = @"SELECT e.Eid,e.Name,e.Email,e.Phone,e.Gender,
            d.DepName AS DepName,d2.DName AS DesName 
            FROM Employee e
             INNER JOIN Department d ON e.DepID = d.DepId 
             INNER JOIN Designation d2 ON e.Did = d2.Did";
            return await conn.QueryAsync<Employee>(sql);
            
            }
            
        }
    }
}