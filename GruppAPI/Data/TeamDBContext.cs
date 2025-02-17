using GruppAPI.Models;
using Microsoft.EntityFrameworkCore;

namespace GruppAPI.Data
{
    public class TeamDBContext : DbContext
    {
        public TeamDBContext(DbContextOptions<TeamDBContext> options) : base(options) { }
        public DbSet<Team> Teams { get; set; }
    };


    
}


