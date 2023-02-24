﻿#nullable disable

using CompanyAPIFV.Domain.SeedWork;

namespace CompanyAPIFV.Domain.Models
{
    public class Employee : Entity
    {
        public string Email { get; }
        public string Name { get; private set; }
        public Address[] Addresses { get; private set; }

        private readonly List<ProjectAssignment> _projectAssignment = new List<ProjectAssignment>();
        public virtual IReadOnlyList<ProjectAssignment> ProjectAssignments => _projectAssignment;

        protected Employee() { }

        public Employee(string email, string name, Address[] addresses)
            : this()
        {
            Email = email;
            EditPersonalInformation(name, addresses);
        }

        public void EditPersonalInformation(string name, Address[] addresses)
        {
            Name = name;
            Addresses = addresses;
        }

        public virtual void AssignToProject(Project project, Seniority seniority)
        {
            if (_projectAssignment.Count >= 2)
                throw new Exception("Employee cannot be assign to more than 2 projects");

            if (_projectAssignment.Any(pa => pa.Project == project))
                throw new Exception($"Employee '{Name}' is already assigned to Project '{project.Name}'");

            var projectAssignment = new ProjectAssignment(this, project, seniority);

            _projectAssignment.Add(projectAssignment);
        }
    }
}
