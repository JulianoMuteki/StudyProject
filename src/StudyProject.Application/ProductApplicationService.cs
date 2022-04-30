using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces.Application;
using StudyProject.Domain.Interfaces.Base;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StudyProject.Application
{
    public class ProductApplicationService : IProductApplicationService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ProductApplicationService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public async Task<IEnumerable<Product>> GetAllProducts()
        {
            return await _unitOfWork.Repository<Product>().GetAllAsync();
        }

        public ICollection<Product> GetAll()
        {
            return _unitOfWork.Repository<Product>().GetAll();
        }

    }
}
