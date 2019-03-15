using StudyProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyProject.Domain.Interfaces.Application
{
    public interface IClientApplicationService
    {
        Task<IEnumerable<Client>> GetAllClient();
        ICollection<Client> GetAll();
    }
}
