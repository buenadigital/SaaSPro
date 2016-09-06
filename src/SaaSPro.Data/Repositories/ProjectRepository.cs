using SaaSPro.Data;
using SaaSPro.Data.Repositories;
using SaaSPro.Domain;

namespace SaasPro.Data.Repositories
{
    public class ProjectRepository : EFRespository<Project>, IProjectRepository
    {
        public ProjectRepository(EFDbContext dbContext)
            : base(dbContext)
        {
        }
    }
}
