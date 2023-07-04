using BaseAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Diagnostics;

namespace BaseAPI.Data
{
    public class BaseContext : DbContext
    {
        public BaseContext(DbContextOptions options) : base(options)
        {

        }
        public DbSet<ClasePrincipal> ClasePrincipals { get; set; }
        public DbSet<Class1> Class1s { get; set; }
        public DbSet<Class2> Class2s { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
