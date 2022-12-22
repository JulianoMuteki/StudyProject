using StudyProject.Domain.Interfaces.Base;
using StudyProject.Domain.Interfaces.Repository;
using StudyProject.Infra.Context;
using StudyProject.Infra.Repository.Common;
using StudyProject.Infra.Repository.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudyProject.Infra.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly StudyProjectContext _dbContext;
        private readonly Dictionary<Type, object> _repositories = new Dictionary<Type, object>();

        public Dictionary<Type, object> Repositories
        {
            get { return _repositories; }
            set { Repositories = value; }
        }

        public UnitOfWork(StudyProjectContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGenericRepository<T> Repository<T>() where T : class
        {
            if (Repositories.Keys.Contains(typeof(T)))
            {
                return Repositories[typeof(T)] as IGenericRepository<T>;
            }

            IGenericRepository<T> repo = new GenericRepository<T>(_dbContext);
            Repositories.Add(typeof(T), repo);
            return repo;
        }

        private readonly Dictionary<Type, object> _repositoriesCustom = new Dictionary<Type, object>();

        public Dictionary<Type, object> RepositoriesCustom
        {
            get { return _repositoriesCustom; }
            set { RepositoriesCustom = value; }
        }

        public T RepositoryCustom<T>() where T : class
        {
            if (!RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                CreateReository<T>();
            }

            var obj = RepositoriesCustom[typeof(T)] as T;
            return obj;
        }

        private void CreateReository<T>() where T : class
        {
            if (typeof(IClientRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IClientRepository repository = new ClientRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
            else if (typeof(IProductRepository).Equals((typeof(T))) && !RepositoriesCustom.Keys.Contains(typeof(T)))
            {
                IProductRepository repository = new ProductRepository(_dbContext);
                RepositoriesCustom.Add(typeof(T), repository);
            }
        }

        public async Task<int> CommitAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

        public void Rollback()
        {
            _dbContext.ChangeTracker.Entries().ToList().ForEach(x => x.Reload());
        }

        public int Commit()
        {
            return  _dbContext.SaveChanges();
        }
    }
}