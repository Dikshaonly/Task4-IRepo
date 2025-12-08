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

        public async Task<Department?> GetDepartmentById(int id){
            using(var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql = @"SELECT DepId,DepName FROM Department WHERE DepId = @id";
                return await conn.QuerySingleOrDefaultAsync<Department>(sql,new{@id = id});
            }
        }

        public async Task Edit(Department dep){
            using (var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql = "UPDATE Department SET DepName = @DepName WHERE DepId=@DepId";
                await conn.ExecuteAsync(sql,new{@DepName = dep.DepName,@DepId = dep.DepId});
            }
        }

        public async Task Create(Department dep){
            using(var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql = @"INSERT INTO Department(DepName)Values(@DepName)";
                await conn.ExecuteAsync(sql,new{@DepName = dep.DepName});
            }
        }
    }
}