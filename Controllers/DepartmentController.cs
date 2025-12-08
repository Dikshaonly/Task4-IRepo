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

        public  async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department dep)
        {
           if (ModelState.IsValid)
           {
            await _Repo.Create(dep);
            return RedirectToAction("Index");
           }
            return View();
        }

        public  async Task<IActionResult> Edit(int id)
        {
            var data = await _Repo.GetDepartmentById(id);
            return View(data);
        }

        [HttpPost]
        public  async Task<IActionResult> Edit(Department dep)
        {
            if (ModelState.IsValid)
            {
                await _Repo.Edit(dep);
                return RedirectToAction("Index","Department");
            }
          
            return View();
        }
    }
}