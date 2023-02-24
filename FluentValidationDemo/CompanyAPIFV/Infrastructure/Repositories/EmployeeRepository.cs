using CompanyAPIFV.Domain.Models;
using CompanyAPIFV.Domain.SeedWork;

namespace CompanyAPIFV.Infrastructure.Repositories
{
    public class EmployeeRepository
    {
        private static readonly List<Employee> _existingEmployees = new List<Employee>
        {
            Marco(),
            Miguel()
        };
        private static long _lastId = _existingEmployees.Max(x => x.Id);

        public Employee GetById(long id)
        {
            // Retrieving from the database
            return _existingEmployees.SingleOrDefault(x => x.Id == id);
        }

        public void Save(Employee employee)
        {
            // Setting up the id for new employees emulates the ORM behavior
            if (employee.Id == 0)
            {
                _lastId++;
                SetId(employee, _lastId);
            }

            // Saving to the database
            _existingEmployees.RemoveAll(x => x.Id == employee.Id);
            _existingEmployees.Add(employee);
        }

        private static void SetId(Entity entity, long id)
        {
            // The use of reflection to set up the Id emulates the ORM behavior
            entity.GetType().GetProperty(nameof(Entity.Id)).SetValue(entity, id);
        }

        private static Employee Marco()
        {
            var marco = new Employee(
                "marco@supercompany.com", 
                "Marco Antonio", 
                new Address("1946 Genova St", "Genova", "IT", "16100"));
            SetId(marco, 1);
            marco.AssignToProject(new Project(1, "Regulated Reports"), Seniority.Senior);

            return marco;
        }

        private static Employee Miguel()
        {
            var miguel = new Employee(
                "miguel@supercompany.com", 
                "Miguel San", 
                new Address("29003 Malaga Street", "Malaga", "ES", "29003"));
            SetId(miguel, 2);            
            miguel.AssignToProject(new Project(2, "User Experience"), Seniority.Expert);

            return miguel;
        }
    }
}
