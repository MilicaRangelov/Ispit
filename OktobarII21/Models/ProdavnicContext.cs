
using Microsoft.EntityFrameworkCore;

namespace Models {

    public class ProdavnicaContext : DbContext {


        public DbSet<Prodavnica> Prodavnica { get; set; }

        public DbSet<Proizvod> Proizvod { get; set; }

        public DbSet<Tip> Tip { get; set; }

        public DbSet<Korpa> Korpa { get; set; }
        
        public DbSet<Spoj> Spoj { get; set; }


        public ProdavnicaContext(DbContextOptions ops) : base(ops){

        }
    }
}