using Ivoluntia.BackgroudServices.Data.Repository.Implementation;
using Microsoft.EntityFrameworkCore.Infrastructure;
using System.Collections;

namespace Ivoluntia.BackgroudServices.Data.Repository.Interface
{
    public class UnitOfWork : IUnitOfWork
    {
        public readonly iVoluntiaDataContext _dbContext;
        private Hashtable _repositories;
        public DatabaseFacade Database => _dbContext.Database;

        public INotificationRepository notificationRepo { get; set; }

        public UnitOfWork(iVoluntiaDataContext dbContext)
        {
            _dbContext = dbContext;
            notificationRepo = new NotificationRepository(dbContext);
        }
        public IGenericRepository<TEntity> repository<TEntity>() where TEntity : class
        {
            if (_repositories == null) _repositories = new Hashtable();
            var Type = typeof(TEntity).Name;
            if (!_repositories.ContainsKey(Type))
            {
                var repositoryType = typeof(GenericRepository<TEntity>);
                var repositoryInstance = Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), _dbContext);
                _repositories.Add(Type, repositoryInstance);
            }
            return (IGenericRepository<TEntity>)_repositories[Type];
        }

        public async Task<int> CompleteAsync()
        {
            return await _dbContext.SaveChangesAsync();
        }

    }
}
