
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{

    public class Soba{

        [Key]
        public int ID { get; set; }

        [Required]
        
        public int BrSobe { get; set; }

        [JsonIgnore]
        public SmestajniObjekat SmestajniObjekat { get; set; }

        public List<Spoj> SobaRezervacija { get; set; }


    }
}