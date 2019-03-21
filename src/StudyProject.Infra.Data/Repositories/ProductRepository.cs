using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces.Repository;
using StudyProject.Infra.Context;
using StudyProject.Infra.Repository.Common;

namespace StudyProject.Infra.Data.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StudyProjectContext context)
            : base(context)
        {
        }
    }
}