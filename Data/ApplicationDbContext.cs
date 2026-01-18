using Microsoft.EntityFrameworkCore;
using RegionalTempleInfo.Models;

namespace RegionalTempleInfo.Data
{
    public class ApplicationDbContext:DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

            
        }
        public DbSet<Registration>  registrations{ get; set; }
        public DbSet<Information>   information{ get; set; } 

    }
}
