#nullable disable

namespace CompanyAPIFV.Application.Contracts
{
    public class RegisterEmployeeRequest
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AddressDto
    {
        public string Street { get; set; }
        public string City { get; set; }
        public string State { get; set;  }
        public string ZipCode { get; set; }
    }

    public class RegisterEmployeeResponse
    {
        public long Id { get; set; }
    }

    public class EditPersonalInformationRequest 
    {
        public string Name { get; set; }
        public AddressDto Address { get; set; }
    }

    public class AssignToProjectRequest
    { 
        public ProjectAssignmentDto[] ProjectAssignments { get; set; }
    }

    public class ProjectAssignmentDto
    {
        public string Project { get; set; }
        public string Seniority { get; set; }
    }

    public class GetEmployeeResponse
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public AddressDto Address { get; set; }
        public ProjectAssignmentDto[] ProjectAssignments { get; set; }
    }
}
