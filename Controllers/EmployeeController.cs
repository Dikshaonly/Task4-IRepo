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

        public async Task<IActionResult> Edit(int id){
            var employees = await _Repo.GetEmployeeById(id);
                var departments = await _Repo.GetDepartment();
                var designations = await _Repo.GetDesignation();
            ViewBag.Departments = new SelectList(departments, "DepId", "DepName" );
            ViewBag.Designations = new SelectList(designations, "Did", "Dname");
              return View(employees);  
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            if (!ModelState.IsValid)
            {
                 var departments = await _Repo.GetDepartment();
                var designations = await _Repo.GetDesignation();
            ViewBag.Departments = new SelectList(departments, "DepId", "DepName" );
            ViewBag.Designations = new SelectList(designations, "Did", "Dname");
                return View(emp);
            }
            await _Repo.Edit(emp);
            return RedirectToAction("Index");
        }
    }
}