using CleanUser.ApplicationCore.Entities;
using Microsoft.EntityFrameworkCore;

namespace CleanUser.Infrastructure.Persistence.Context
{
    public class UserContext : DbContext
    {
        public UserContext(DbContextOptions<UserContext> options) : base(options)
        {

        }

        public DbSet<User> Users { get; set; }
    }
}
