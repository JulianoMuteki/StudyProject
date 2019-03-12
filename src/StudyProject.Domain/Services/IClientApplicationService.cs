using StudyProject.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace StudyProject.Domain.Services
{
    public interface IClientApplicationService
    {
        Task<IEnumerable<Client>> GetAllClient();
    }
}
