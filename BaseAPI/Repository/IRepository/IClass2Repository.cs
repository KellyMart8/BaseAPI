using BaseAPI.Data;

namespace BaseAPI.Repository.IRepository
{
    public interface IClass2Repository : IRepository<Class2>
    {
        Task<Class2> Update(Class2 entity);
    }
}
