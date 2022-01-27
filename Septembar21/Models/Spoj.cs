
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{


    public class Spoj{

        [Key]

        public int ID { get; set; }

        [Required]
        public bool Zauzeto { get; set; }

        [JsonIgnore]
        public Soba Soba { get; set; }
        public Rezervacija Rezervacija { get; set; }

    }
}