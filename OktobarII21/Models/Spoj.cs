using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{

    public class Spoj{

        [Key]

        public int ID { get; set; }

        [Required]

        public int Kolicina { get; set; }

        [JsonIgnore]

        public Proizvod Proizvod { get; set; }
        public Korpa Korpa { get; set; }
    }
}