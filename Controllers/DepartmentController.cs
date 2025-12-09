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
        private readonly IErrorRepository _eRepo;
        public DepartmentController(IDepartmentRepository Repo,IErrorRepository eRepo){
            _Repo = Repo;
            _eRepo = eRepo;
        }
         public async Task<IActionResult> Index()
        {
            try{
            var departments = await _Repo.GetDepartment();
            return View(departments);
        }
            catch(Exception ex){
                _eRepo.ShowError(TempData, ex);
                //TempData["ErrorMessage"] = "Error Loading Department "+ex.Message;
                return View(Enumerable.Empty<Department>());
            }
        }

        public  async Task<IActionResult> Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(Department dep)
        {
            try{
            if (ModelState.IsValid)
           {
            await _Repo.Create(dep);
            return RedirectToAction("Index");
           }
           return View();
        }catch(Exception ex){
            _eRepo.ShowError(TempData,ex);
            return View();
        }
           
        }

        public  async Task<IActionResult> Edit(int id)
        {
            try{
            var data = await _Repo.GetDepartmentById(id);
            return View(data);
            }
            catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                return View(Enumerable.Empty<Department>());
            }
        }

        [HttpPost]
        public  async Task<IActionResult> Edit(Department dep)
        {
            try{
                if (ModelState.IsValid)
            {
                await _Repo.Edit(dep);
                return RedirectToAction("Index","Department");
            }
            return View();
            }catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                return View();
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _Repo.Delete(id);
            return RedirectToAction("Index");   
        }
    }
}