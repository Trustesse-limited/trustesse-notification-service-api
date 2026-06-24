
namespace Ivoluntia.BackgroudServices.Data.Repository.Interface
{
    public interface IUnitOfWork
    {
        IGenericRepository<TEntity> repository<TEntity>() where TEntity : class;
        Task<int> CompleteAsync();
    }
}
