
using Microsoft.EntityFrameworkCore;
using NSoft.Models;

namespace NSoft.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }
        public DbSet<Material> Materiales { get; set; }
    }
}
