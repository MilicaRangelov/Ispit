


using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{
    public class Drzava{


        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Naziv { get; set; }

        [JsonIgnore]
        public List<Mesto> Mesta { get; set; }

    }
}