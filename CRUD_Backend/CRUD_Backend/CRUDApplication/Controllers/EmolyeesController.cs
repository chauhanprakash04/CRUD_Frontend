using CRUDApplication.Model;
using CRUDApplication.Repository;
using Microsoft.AspNetCore.Mvc;

namespace CRUDApplication.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EmployeesController : ControllerBase
    {
        private readonly IEmployee _dbContext;

        public EmployeesController(IEmployee dbContext)
        {
            _dbContext = dbContext;
        }

        [HttpGet]
        public IActionResult GetEmployees()
        {
            try
            {
                var employees = _dbContext.GetEmployees();
                return Ok(employees);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("{id}")]
        public IActionResult GetEmployee(int id)
        {
            try
            {
                var employee = _dbContext.GetEmployeeById(id);
                return Ok(employee);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public IActionResult InsertEmployee([FromBody] Employee employee)
        {
            if (employee == null && ModelState.IsValid)
            {
                return BadRequest("Employee not found");
            }

            try
            {
                _dbContext.AddEmployee(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id}")]
        public IActionResult Update(int id, [FromBody] Employee employee)
        {
            try
            {
                employee.id = id;
                _dbContext.UpdateEmployee(employee);
                return Ok();
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id}")]
        public IActionResult Delete(int id)
        {
            try
            {
                _dbContext.DeleteEmployee(id);
                return Ok(); 
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }
    }
}
