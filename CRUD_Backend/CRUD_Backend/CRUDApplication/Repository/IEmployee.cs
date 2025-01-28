using System.Collections.Generic;
using CRUDApplication.Model;

namespace CRUDApplication.Repository
{
    public interface IEmployee
    {
        public List<Employee> GetEmployees();
        public void AddEmployee(Employee emp);
        public void UpdateEmployee(Employee emp);
        public void DeleteEmployee(int id);
        public Employee GetEmployeeById(int id);
    }
}
