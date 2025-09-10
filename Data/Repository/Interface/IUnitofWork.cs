
namespace Ivoluntia.BackgroudServices.Data.Repository.Interface
{
    public interface IUnitofWork
    {
        Task<int> SaveChangesAsync();
    }
}
