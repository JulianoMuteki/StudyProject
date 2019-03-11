using System.Threading.Tasks;

namespace StudyProject.Infra.Repository.Interfaces
{
    public interface IUnitOfWork
    {
        IGenericRepository<T> Repository<T>() where T : class;

        Task<int> Commit();

        void Rollback();
    }
}
