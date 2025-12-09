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
        private readonly IErrorRepository _eRepo;
        public EmployeeController(IEmployeeRepository Repo,IErrorRepository eRepo){
            _Repo = Repo;
            _eRepo = eRepo;
        }
        public async Task<IActionResult> Index(){
            try{
                var employees = await  _Repo.GetEmployee(); 
                return View(employees); 
            }catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                 return View(Enumerable.Empty<Employee>());
            }
          
        }

        public async Task<IActionResult> Create()
        {
            try{
                var departments = await _Repo.GetDepartment();
            var designations = await _Repo.GetDesignation();
            ViewBag.Departments = new SelectList(departments, "DepId", "DepName" );
            ViewBag.Designations = new SelectList(designations, "Did", "Dname");
            return View();
            }
            catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                return View();
            }
            
        }

        [HttpPost]
        public async Task<IActionResult> Create(Employee emp)
        {
            try{
                if (!ModelState.IsValid)
            {
            var departments = await _Repo.GetDepartment();
            var designations = await _Repo.GetDesignation();
            ViewBag.Departments = new SelectList(departments, "DepId", "DepName" );
            ViewBag.Designations = new SelectList(designations, "Did", "Dname");
            return View(emp);
            }
            await _Repo.Create(emp);
            return RedirectToAction("Index","Employee");
            }
            catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                return View(emp);
            }
        }

        public async Task<IActionResult> Edit(int id){
            try{
                var employees = await _Repo.GetEmployeeById(id);
                var departments = await _Repo.GetDepartment();
                var designations = await _Repo.GetDesignation();
            ViewBag.Departments = new SelectList(departments, "DepId", "DepName" );
            ViewBag.Designations = new SelectList(designations, "Did", "Dname");
              return View(employees);  
            }
            catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                return View(Enumerable.Empty<Employee>());
            }
            
        }
        [HttpPost]
        public async Task<IActionResult> Edit(Employee emp)
        {
            try{
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
            catch(Exception ex){
                _eRepo.ShowError(TempData,ex);
                return View(emp);
            }
        }

        public async Task<IActionResult> Delete(int id)
        {
            await _Repo.Delete(id);
            return RedirectToAction("Index");
        }

        public async Task<IActionResult> Details(int id)
        {
           try{
             var employees = await _Repo.Details(id);
            return View(employees);
           }
           catch(Exception ex){
            _eRepo.ShowError(TempData,ex);
            return View(Enumerable.Empty<Employee>());
           }
        }
    }
}