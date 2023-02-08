using CompanyAPIFV.Application.Contracts;
using CompanyAPIFV.Domain.Models;
using CompanyAPIFV.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using System.Text.RegularExpressions;

namespace CompanyAPIFV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ProjectRepository _projectRepository;

        public EmployeeController(EmployeeRepository employeeRepository, ProjectRepository projectRepository)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;
        }

        [HttpPost]
        public IActionResult RegisterEmployee(RegisterEmployeeRequest request) 
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

            var employee = new Employee(request.Email, request.Name, request.Address);
            _employeeRepository.Save(employee);

            var response = new RegisterEmployeeResponse { Id = employee.Id };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInformation(long id, [FromBody]EditPersonalInformationRequest request)
        {
            Employee employee = _employeeRepository.GetById(id);

            employee.EditPersonalInformation(request.Name, request.Address);
            _employeeRepository.Save(employee);

            return Ok();
        }

        [HttpPost("{id}/assign")]
        public IActionResult AssignToProject(long id, [FromBody]AssignToProjectRequest request)
        {
            Employee employee = _employeeRepository.GetById(id);

            foreach (ProjectAssignmentDto projectAssignmentDto in request.ProjectAssignments)
            {
                Project project = _projectRepository.GetByName(projectAssignmentDto.Project);
                var seniority = Enum.Parse<Seniority>(projectAssignmentDto.Seniority);

                employee.AssignToProject(project, seniority);
            }

            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(long id)
        {
            Employee employee = _employeeRepository.GetById(id);

            var resonse = new GetEmployeeResponse
            {
                Address = employee.Address,
                Email = employee.Email,
                Name = employee.Name,
                ProjectAssignments = employee.ProjectAssignments.Select(x => new ProjectAssignmentDto
                {
                    Project = x.Project.Name,
                    Seniority = x.Seniority.ToString()
                }).ToArray()
            };            
            return Ok(resonse);
        }
    }
}
