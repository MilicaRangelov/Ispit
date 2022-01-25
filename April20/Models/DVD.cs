using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace Models{


    public class DVD{

        [Key]
        public int ID { get; set; }

        [Required]
        public int Broj { get; set; }

        [JsonIgnore]
        public virtual Police Police { get; set; }
        public virtual VideoKlub Klubovi { get; set; }


    }
}