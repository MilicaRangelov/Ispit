

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{


    public class Rezervacija{

        [Key]

        public int ID { get; set; }

        [Required]
        public DateTime DatumOd { get; set; }

        [Required]
        public DateTime DatumDo { get; set; }

        [Required]
        public int Kapacitet { get; set; }

        [JsonIgnore]
        public List<Spoj> RezvervacijaSoba { get; set; }
     
    }
}