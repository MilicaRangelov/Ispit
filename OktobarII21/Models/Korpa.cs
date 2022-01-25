
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{

    public class Korpa{

        [Key]

        public int ID { get; set; }

        [Required]
        public int Ukupno { get; set; }

        [JsonIgnore]
        public List<Spoj> KorpaSpoj { get; set; }
        
    }
}