using System;
using System.Data;
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
        private readonly IConnectionRepository _connstr;
        public EmployeeRepository(IConnectionRepository connstr){
            _connstr = connstr;
        }
        public async Task<IEnumerable<Employee>> GetEmployee()
        {
            using (var conn = new SqlConnection(_connstr.GetCS()))
            {
            await conn.OpenAsync();
            Console.WriteLine("Loaded Connection:"+conn);
            string sql = @"SELECT e.Eid,e.Name,e.Email,e.Phone,e.Gender,
            d.DepName AS DepName,d2.DName AS DesName 
            FROM Employee e
             INNER JOIN Department d ON e.DepID = d.DepId 
             INNER JOIN Designation d2 ON e.Did = d2.Did";

            return await conn.QueryAsync<Employee>(sql);
        }}

        public async Task<Employee?> GetEmployeeById(int id){
            using (var conn = new SqlConnection(_connstr.GetCS())){
               await conn.OpenAsync();
                var parameters = new DynamicParameters();
                parameters.Add("@id",id,DbType.Int32);
                string sql =@"SELECT 
            e.Eid,e.Name,e.Email,e.Phone,
            e.Gender,e.DepId,e.Did,
            d.DepName AS DepName,
            d2.Dname AS DesName 
            FROM Employee e 
            INNER JOIN Department d ON e.DepID = d.DepId
             INNER JOIN Designation d2 ON e.Did = d2.Did 
             WHERE e.Eid = @id ";
             return await conn.QuerySingleOrDefaultAsync<Employee>(sql,parameters); 
            }
        }

        public async Task<IEnumerable<Department>> GetDepartment(){
            using (var conn = new SqlConnection(_connstr.GetCS())){
                await conn.OpenAsync();
                return await conn.QueryAsync<Department>("SELECT DepId,DepName FROM Department");
            }
        }

         public async Task<IEnumerable<Designation>> GetDesignation(){
            using (var conn = new SqlConnection(_connstr.GetCS())){
            await conn.OpenAsync();
            return await conn.QueryAsync<Designation>("SELECT Did, DName FROM Designation");
            }
        }

        public async Task Edit(Employee emp){
            
                using (var conn = new SqlConnection(_connstr.GetCS())){
                    await conn.OpenAsync();
                    var p = new DynamicParameters();
                    p.Add("@Eid",emp.Eid,DbType.Int32);
                    p.Add("@Name",emp.Name,DbType.String);
                    p.Add("@Email",emp.Email,DbType.String);
                    p.Add("@Phone",emp.Phone,DbType.String);
                    p.Add("@Gender",emp.Gender,DbType.String);
                    p.Add("@DepId",emp.DepId,DbType.Int32);
                    p.Add("@Did",emp.Did,DbType.Int32);
                    string sql = @"UPDATE Employee SET 
                    Name = @Name,Email = @Email,Phone = @Phone,Gender = @Gender,
                    DepId = @DepId, Did = @Did
                    WHERE Eid=@Eid";
                     await conn.ExecuteAsync(sql,p);
                }
        }

        public async Task Create(Employee emp){
            using (var conn = new SqlConnection(_connstr.GetCS())){
                await conn.OpenAsync();
                var p = new DynamicParameters();
                    p.Add("@Eid",emp.Eid,DbType.Int32);
                    p.Add("@Name",emp.Name,DbType.String);
                    p.Add("@Email",emp.Email,DbType.String);
                    p.Add("@Phone",emp.Phone,DbType.String);
                    p.Add("@Gender",emp.Gender,DbType.String);
                    p.Add("@DepId",emp.DepId,DbType.Int32);
                    p.Add("@Did",emp.Did,DbType.Int32);
                string sql = @"INSERT INTO Employee(Name,Email,Phone,Gender,DepId,Did)
                VALUES(@Name,@Email,@Phone,@Gender,@DepId,@Did)";
                await conn.ExecuteAsync(sql,p);
            }
        }

        public async Task Delete(int id){
            using(var conn = new SqlConnection(_connstr.GetCS())){
                var p = new DynamicParameters();
                p.Add("@id",id,DbType.Int32);
                string sql = @"DELETE FROM Employee WHERE Eid = @id";
                await conn.ExecuteAsync(sql,p);
            }
        }

        public async Task<Employee?> Details(int id){
            using (var conn = new SqlConnection(_connstr.GetCS())){
                await conn.OpenAsync();
                 var p = new DynamicParameters();
                p.Add("@id",id,DbType.Int32);
                string sql = @"SELECT 
            e.Eid,e.Name,e.Email,e.Phone,
            e.Gender,e.DepId,e.Did,
            d.DepName AS DepName,
            d2.Dname AS DesName 
            FROM Employee e 
            INNER JOIN Department d ON e.DepID = d.DepId
             INNER JOIN Designation d2 ON e.Did = d2.Did 
             WHERE e.Eid = @id";
             return await conn.QuerySingleOrDefaultAsync<Employee>(sql,p);
            }
        }
    
    }
}