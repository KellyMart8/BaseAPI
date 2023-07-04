using BaseAPI.Data;

namespace BaseAPI.Repository.IRepository
{
    public interface IClasePrincipalRepository : IRepository<ClasePrincipal>
    {
        Task<ClasePrincipal> Update(ClasePrincipal entity);
    }
}
