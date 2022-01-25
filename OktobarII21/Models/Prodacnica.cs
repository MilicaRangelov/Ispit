
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{


    public class Prodavnica{

        [Key]

        public int ID { get; set; }

        [Required]
        [MaxLength(50)]

        public string Naziv { get; set; }     

        [JsonIgnore]
        public List<Proizvod> Proizvodi { get; set; }
    }

}