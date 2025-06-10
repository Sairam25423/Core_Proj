using SixthDayTsk.Models;
using Microsoft.EntityFrameworkCore;

namespace SixthDayTsk.Data
{
    public class UserDBContext : DbContext
    {
        public UserDBContext(DbContextOptions<UserDBContext> options) : base(options)
        {
        }
        public DbSet<User> Users { get; set; }
    }
}
