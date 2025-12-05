using System;
using System.Collections.Generic;
using Task4.Models;

namespace Task4.Repository{
    public interface IEmployeeRepository
    {
        Task <IEnumerable<Employee>> GetEmployee();

       /* Task <Employee?> GetEmployeeById(int id);

        Task Insert (Employee emp);

        Task Edit(Employee emp);

        Task Delete(int id);

        Task Save();*/
    }
}