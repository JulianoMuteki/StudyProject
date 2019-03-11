using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces;
using StudyProject.Infra.Context;
using StudyProject.Infra.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Infra.Data.Repositories
{
  public  class ProductRepository: GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(StudyProjectContext context)
            : base(context)
        {

        }
    }
}
