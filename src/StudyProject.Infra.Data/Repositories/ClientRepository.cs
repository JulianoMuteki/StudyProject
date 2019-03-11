using StudyProject.Domain.Entities;
using StudyProject.Domain.Interfaces;
using StudyProject.Infra.Context;
using StudyProject.Infra.Repository.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace StudyProject.Infra.Data.Repositories
{
   public class ClientRepository: GenericRepository<Client>, IClientRepository
    {
        public ClientRepository(StudyProjectContext context)
            :base(context)
        {

        }

       
    }
}