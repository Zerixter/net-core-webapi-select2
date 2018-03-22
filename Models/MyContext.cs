using Microsoft.EntityFrameworkCore;
using core_classe.Models;

namespace core_classe.Models 
{

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        {}

        public DbSet<Persona> Gent { get; set; } 

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Nota>()
                .HasOne(x=>x.alumne)
                .WithMany(x=>x.notes)
                .HasForeignKey(x=>x.persona_fk);

        }

        public DbSet<core_classe.Models.Nota> Nota { get; set; }

    }
    
}