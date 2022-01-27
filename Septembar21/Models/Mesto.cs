using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{

    public class Mesto{

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Naziv { get; set; }


        [JsonIgnore]
        public Drzava Drzava { get; set; }
        public List<SmestajniObjekat> SmestajniObjekti { get; set; }
    }
}