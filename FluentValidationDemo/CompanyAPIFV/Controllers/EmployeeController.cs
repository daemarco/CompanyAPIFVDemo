using CompanyAPIFV.Application.Contracts;
using CompanyAPIFV.Application.Validators;
using CompanyAPIFV.Domain.Models;
using CompanyAPIFV.Infrastructure.Repositories;
using FluentValidation.Results;
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
            var validator = new RegisterEmployeeRequestValidator();
            ValidationResult result = validator.Validate(request);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors[0].ErrorMessage);
            }

            //if (request == null)
            //    return BadRequest("Request cannot be empty");

            // TODO - Validate Email uniqueness

            var address = new Address(
                request.Address.Street, 
                request.Address.City, 
                request.Address.State, 
                request.Address.ZipCode);
            var employee = new Employee(request.Email, request.Name, address);
            _employeeRepository.Save(employee);

            var response = new RegisterEmployeeResponse { Id = employee.Id };

            return Ok(response);
        }

        [HttpPut("{id}")]
        public IActionResult EditPersonalInformation(long id, [FromBody]EditPersonalInformationRequest request)
        {
            Employee employee = _employeeRepository.GetById(id);

            var validator = new EditPersonalInformationRequestValidator();
            ValidationResult result = validator.Validate(request);

            if (!result.IsValid) 
            {
                return BadRequest(result.Errors[0].ErrorMessage);
            }

            var address = new Address(
                request.Address.Street,
                request.Address.City,
                request.Address.State,
                request.Address.ZipCode);
            employee.EditPersonalInformation(request.Name, address);
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
                Address = new AddressDto
                {
                    City = employee.Address.City,
                    State = employee.Address.State,
                    Street= employee.Address.Street,
                    ZipCode= employee.Address.ZipCode,
                },
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
