using Microsoft.EntityFrameworkCore;
using CarpoolService.Models;

namespace CarpoolService.Data
{
    public class CarpoolContext : DbContext
    {
        public CarpoolContext(DbContextOptions<CarpoolContext> options) : base(options)
        {
        }

        public DbSet<CarpoolUser> CarpoolUsers { get; set; }
    }
}
