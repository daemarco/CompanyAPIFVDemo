using CompanyAPIFV.Application.Contracts;
using CompanyAPIFV.Domain.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.RegularExpressions;

namespace CompanyAPIFV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        public EmployeeController()
        {

        }

        [HttpPost]
        public IActionResult RegisterEmplyee(RegisterEmployeeRequest request) 
        {
            if (request == null)
                return BadRequest("Request cannot be empty");

            if (string.IsNullOrWhiteSpace(request.Name))
                return BadRequest("Name cannot be empty");
            if (request.Name.Length > 250)
                return BadRequest("Name too long");

            if (string.IsNullOrWhiteSpace(request.Email))
                return BadRequest("Name cannot be empty");
            if (request.Email.Length > 100)
                return BadRequest("Name too long");
            if (Regex.IsMatch(request.Email, @"^(.+)@(.+)$") == false)
                return BadRequest("Email in invalid format");

            // TODO - Validate Email uniqueness

            var employee = new Employee(request.Email, request.Name);
            //_employeeRepository.Save(employee);

            var response = new RegisterEmployeeResponse { Id = employee.Id };

            return Ok(response);
        }
    }
}
