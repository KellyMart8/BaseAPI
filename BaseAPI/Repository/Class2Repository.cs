using BaseAPI.Data;
using BaseAPI.Repository.IRepository;

namespace BaseAPI.Repository
{
    public class Class2Repository : Repository<Class2>, IClass2Repository
    {
        private readonly BaseContext _db;

        public Class2Repository(BaseContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Class2> Update(Class2 entity)
        {
            _db.Class2s.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
