using Ivoluntia.BackgroudServices.Data.Repository.Interface;

namespace Ivoluntia.BackgroudServices.Data.Repository.Implementation
{
    public class UnitofWork : IUnitofWork
    {
        private readonly iVoluntiaDataContext _context;

        public UnitofWork(iVoluntiaDataContext context)
        {
            _context = context;
        }

        public Task<int> SaveChangesAsync() => _context.SaveChangesAsync();
    }
}
