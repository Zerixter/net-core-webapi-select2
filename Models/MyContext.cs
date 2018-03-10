using Microsoft.EntityFrameworkCore;

namespace net_core_webapi_select2.Models 
{

    public class MyContext : DbContext
    {
        public MyContext(DbContextOptions<MyContext> options) : base(options) 
        {}

        public DbSet<Persona> Gent { get; set; } 
    }
    
}