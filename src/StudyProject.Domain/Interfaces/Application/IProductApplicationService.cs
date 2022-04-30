using StudyProject.Domain.Entities;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyProject.Domain.Interfaces.Application
{
    public interface IProductApplicationService
    {
        Task<IEnumerable<Product>> GetAllProducts();
        ICollection<Product> GetAll();
    }
}
