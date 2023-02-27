using CompanyAPIFV.Application.Contracts;
using CompanyAPIFV.Application.Validators;
using CompanyAPIFV.Domain.Models;
using CompanyAPIFV.Infrastructure.Repositories;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.AspNetCore.Mvc;

namespace CompanyAPIFV.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class EmployeeController : ControllerBase
    {
        private readonly EmployeeRepository _employeeRepository;
        private readonly ProjectRepository _projectRepository;
        private readonly IValidator<RegisterEmployeeRequest> _registerValidator;

        public EmployeeController(
            EmployeeRepository employeeRepository, 
            ProjectRepository projectRepository,
            IValidator<RegisterEmployeeRequest> registerValidator)
        {
            _employeeRepository = employeeRepository;
            _projectRepository = projectRepository;

            _registerValidator = registerValidator;
        }

        [HttpPost]
        public IActionResult RegisterEmployee(RegisterEmployeeRequest request) 
        {
            //var validator = new RegisterEmployeeRequestValidator();
            ValidationResult result = _registerValidator.Validate(request);

            if (!result.IsValid)
            {
                return BadRequest(result.Errors[0].ErrorMessage);
            }

            //if (request == null)
            //    return BadRequest("Request cannot be empty");

            // TODO - Validate Email uniqueness

            Address[] addresses = request.Addresses
                .Select(x => new Address(x.Street, x.City, x.State, x.ZipCode))
                .ToArray();
            var employee = new Employee(request.Email, request.Name, addresses);
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

            Address[] addresses = request.Addresses
                .Select(x => new Address(x.Street, x.City, x.State, x.ZipCode))
                .ToArray();
            employee.EditPersonalInformation(request.Name, addresses);
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
                Addresses = employee.Addresses.Select(x => 
                new AddressDto
                {
                    City = x.City,
                    State = x.State,
                    Street = x.Street,
                    ZipCode = x.ZipCode,
                }).ToArray(),
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
