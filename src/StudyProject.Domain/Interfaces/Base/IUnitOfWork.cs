using System.Threading.Tasks;

namespace StudyProject.Domain.Interfaces.Base
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;
        T RepositoryCustom<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }
}