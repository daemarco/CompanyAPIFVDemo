using CompanyAPIFV.Domain.SeedWork;

namespace CompanyAPIFV.Domain.Models
{
    public class Project : Entity
    {
        public string Name { get; set; }

        public Project(long id, string name)
        {
            Id = id;
            Name = name;
        }
    }
}
