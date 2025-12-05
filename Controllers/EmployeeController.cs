using System;
using Dapper;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore.SqlServer;
using Microsoft.Data.SqlClient;
using Task4.Repository;
using Task4.Models;

namespace Task4.Controllers
{
    public class EmployeeController:Controller{
        private readonly IEmployeeRepository _Repo;
        public EmployeeController(IEmployeeRepository Repo){
            _Repo = Repo;
        }
        public async Task<IActionResult> Index(){
          var employees = await  _Repo.GetEmployee(); 
          return View(employees); 
        }
    }
}