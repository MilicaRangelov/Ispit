using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;
using System.Collections.Generic;

namespace Models{


    public class Proizvod{

        [Key]
        public int ID { get; set; }


        [Required]
        [MaxLength(20)]

        public string  Naziv { get; set; }

        [Required]
        public int Cena { get; set; }

        [Required]
        public int Kolicina { get; set; }


        [JsonIgnore]
        public Prodavnica Prodavnica { get; set; }
        
        public Tip Tip { get; set; }

        public List<Spoj> ProizvodKorpa { get; set; }
    }
}