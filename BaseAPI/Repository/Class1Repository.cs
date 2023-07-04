using BaseAPI.Data;
using BaseAPI.Repository.IRepository;

namespace BaseAPI.Repository
{
    public class Class1Repository : Repository<Class1>, IClass1Repository
    {
        private readonly BaseContext _db;

        public Class1Repository(BaseContext db) : base(db)
        {
            _db = db;
        }
        public async Task<Class1> Update(Class1 entity)
        {
            _db.Class1s.Update(entity);
            await _db.SaveChangesAsync();
            return entity;
        }
    }
}
