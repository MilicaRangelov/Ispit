using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{


    public class Police{

        [Key]
        public int ID { get; set; }

        [Required]
        [MaxLength(50)]
        public string Vrsta { get; set; }

        [Required]
        public int Velicina { get; set; }

        [JsonIgnore]

        public virtual List<DVD> PoliceDVD { get; set; }
    }
}