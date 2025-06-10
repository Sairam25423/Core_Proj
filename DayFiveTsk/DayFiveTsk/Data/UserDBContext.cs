using DayFiveTsk.Models;
using Microsoft.EntityFrameworkCore;

namespace DayFiveTsk.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        { }
        public DbSet<User>Users { get; set; }
    }
}
