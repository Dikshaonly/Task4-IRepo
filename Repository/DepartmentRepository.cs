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
    public class DepartmentRepository : IDepartmentRepository{
        private string connstr;
        public DepartmentRepository(){
            connstr = "Server=182.93.94.30;Database=EMS;User Id =sa;Password=bdnquery;TrustServercertificate=True;";
        }
           public async Task<IEnumerable<Department>> GetDepartment()
        {
            using (var conn = new SqlConnection(connstr)){
               conn.Open();
               string sql = "SELECT DepId,DepName FROM Department";
               return await conn.QueryAsync<Department>(sql);
            }
            
        }
    }
}