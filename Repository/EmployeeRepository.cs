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

        public async Task<Employee?> GetEmployeeById(int id){
            using (var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql =@"SELECT 
            e.Eid,e.Name,e.Email,e.Phone,
            e.Gender,e.DepId,e.Did,
            d.DepName AS DepName,
            d2.Dname AS DesName 
            FROM Employee e 
            INNER JOIN Department d ON e.DepID = d.DepId
             INNER JOIN Designation d2 ON e.Did = d2.Did 
             WHERE e.Eid = @id ";
             return await conn.QuerySingleOrDefaultAsync<Employee>(sql,new{id = id}); 
            }
        }

        public async Task<IEnumerable<Department>> GetDepartment(){
            using (var conn = new SqlConnection(connstr)){
                conn.Open();
                return await conn.QueryAsync<Department>("SELECT DepId,DepName FROM Department");
            }
        }

         public async Task<IEnumerable<Designation>> GetDesignation(){
            using (var conn = new SqlConnection(connstr)){
            conn.Open();
            return await conn.QueryAsync<Designation>("SELECT Did, DName FROM Designation");
            }
        }

        public async Task Edit(Employee emp){
            
                using (var conn = new SqlConnection(connstr)){
                    conn.Open();
                    string sql = @"UPDATE Employee SET 
                    Name = @Name,Email = @Email,Phone = @Phone,Gender = @Gender,
                    DepId = @DepId, Did = @Did
                    WHERE Eid=@Eid";
                     await conn.ExecuteAsync(sql,new{@Eid=emp.Eid,@Name = emp.Name,@Email = emp.Email,
                     @Phone = emp.Phone,@Gender = emp.Gender,@DepId = emp.DepId,@Did = emp.Did});
                }
        }

        public async Task Create(Employee emp){
            using (var conn = new SqlConnection(connstr)){
                conn.Open();
                string sql = @"INSERT INTO Employee(Name,Email,Phone,Gender,DepId,Did)
                VALUES(@Name,@Email,@Phone,@Gender,@DepId,@Did)";
                await conn.ExecuteAsync(sql,new{@Name = emp.Name,@Email = emp.Email,@Phone = emp.Phone,
                @Gender = emp.Gender,@DepId = emp.DepId,@Did = emp.Did});
            }
        }

        public async Task Delete(int id){
            using(var conn = new SqlConnection(connstr)){
                string sql = @"DELETE FROM Employee WHERE Eid = @id";
                await conn.ExecuteAsync(sql,new{@id = id});
            }
        }
    
    }
}