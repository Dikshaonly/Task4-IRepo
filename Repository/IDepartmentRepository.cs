using System;
using System.Collections.Generic;
using Task4.Models;

namespace Task4.Repository{
    public interface IDepartmentRepository
    {
        Task <IEnumerable<Department>> GetDepartment();

       /* Task <Employee?> GetEmployeeById(int id);

        Task<IEnumerable<Department>> GetDepartment();

        Task<IEnumerable<Designation>> GetDesignation();

        Task Create(Department dep);

        Task Edit(Department dep);

        Task Delete(int id);

        Task<Department?> Details(int id);*/
    }
}