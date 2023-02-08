namespace CompanyAPIFV.Domain.Models
{
    public class ProjectAssignment
    {
        public Employee Employee { get; set; }
        public Project Project { get; set; }
        public Seniority Seniority { get; set; }

        public ProjectAssignment(Employee employee, Project project, Seniority seniority)
        {
            Employee = employee;
            Project = project;
            Seniority = seniority;
        }
    }
}
