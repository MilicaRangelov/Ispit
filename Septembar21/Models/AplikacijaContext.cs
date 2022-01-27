

using Microsoft.EntityFrameworkCore;

namespace Models{

    public class AplikacijaContext: DbContext {


        public DbSet<Drzava> Drzava { get; set; }
        public DbSet<Mesto> Mesto { get; set; }

        public DbSet<SmestajniObjekat> SmestajniObjekat { get; set; }
        public DbSet<Soba> Soba { get; set; }

        public DbSet<Spoj> RezervacijaSoba { get; set; }
        public DbSet<Rezervacija> Rezervacija { get; set; }

        public AplikacijaContext(DbContextOptions options) : base(options){
            
        }



    }
}