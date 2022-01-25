
using Microsoft.EntityFrameworkCore;

namespace Models{

    public class VideotekaContext : DbContext{

        public DbSet<VideoKlub> VideoKlubovi {get; set;}

        public DbSet<DVD> DVDS { get; set; }

        public DbSet<Police> Police { get; set; }  


        public VideotekaContext(DbContextOptions options) :base(options){

            
        }      


    }

}