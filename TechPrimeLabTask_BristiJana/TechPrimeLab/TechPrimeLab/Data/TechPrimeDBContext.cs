using Microsoft.EntityFrameworkCore;
using TechPrimeLab.Models;

namespace TechPrimeLab.Data
{
    public class TechPrimeDBContext: DbContext
    {
        public TechPrimeDBContext(DbContextOptions<TechPrimeDBContext> options): base(options) { }
        public DbSet<UserDetails> user_details { get; set; }
        public DbSet<ProjectDetails> project_details { get; set; }
    }
}
