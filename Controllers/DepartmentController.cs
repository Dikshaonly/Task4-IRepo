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

namespace Task4.Controllers{
    public class DepartmentController:Controller{
        private readonly IDepartmentRepository _Repo;
        public DepartmentController(IDepartmentRepository Repo){
            _Repo = Repo;
        }
         public async Task<IActionResult> Index()
        {
            var departments = await _Repo.GetDepartment();
            return View(departments);
        }
    }
}