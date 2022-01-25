using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{

    public class VideoKlub{

        [Key]

        public int ID { get; set; }

        [Required]
        [MaxLength(20)]
        public string Naziv { get; set; }  

        [JsonIgnore]
        public virtual List<DVD> KlubDVD { get; set; }   
    }

}