using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces.Repository;
using StudyProject.Infra.Context;
using StudyProject.Infra.Repository.Common;

namespace StudyProject.Infra.Repository.Repositories
{
    public class ClientRepository : GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(StudyProjectContext context)
            : base(context)
        {
        }
    }
}