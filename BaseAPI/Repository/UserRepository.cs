using BaseAPI.Data;
using BaseAPI.Models;
using BaseAPI.Repository.IRepository;
using Microsoft.EntityFrameworkCore;

namespace BaseAPI.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly BaseContext _db;

        public UserRepository(BaseContext db)
        {
            _db = db;
        }

        public async Task<User> GetUser(string username, string pass)
        {
            return await _db.Users.FirstOrDefaultAsync(u => u.Name == username && u.Password == pass);
        }

        public bool IsUser(string username, string pass)
        {
            var users = _db.Users.ToList();
            return users.Where(u => u.Name == username && u.Password == pass).Count() > 0;
        }
    }
}
