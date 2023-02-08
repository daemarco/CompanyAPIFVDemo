using CompanyAPIFV.Domain.Models;

namespace CompanyAPIFV.Infrastructure.Repositories
{
    public sealed class ProjectRepository
    {
        private static readonly Project[] _allProjects =
        {
            new Project(1, "Regulated Reports"),
            new Project(2, "User Experience"),
            new Project(3, "AI Rocket")
        };

        public Project GetByName(string name)
        {
            return _allProjects.SingleOrDefault(x => x.Name == name);
        }
    }
}
