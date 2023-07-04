using BaseAPI.Data;

namespace BaseAPI.Repository.IRepository
{
    public interface IClass1Repository : IRepository<Class1>
    {
        Task<Class1> Update(Class1 entity);
    }
}
