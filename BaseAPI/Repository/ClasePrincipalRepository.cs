using BaseAPI.Data;
using BaseAPI.Repository.IRepository;

namespace BaseAPI.Repository
{
    public class ClasePrincipalRepository : Repository<ClasePrincipal>, IClasePrincipalRepository
    {
        private readonly BaseContext _db;

        public ClasePrincipalRepository(BaseContext db) : base(db)
        {
            _db = db;
        }
        public async Task<ClasePrincipal> Update(ClasePrincipal entity)
        {
            _db.ClasePrincipals.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
