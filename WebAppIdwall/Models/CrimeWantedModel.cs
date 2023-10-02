using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppIdwall.Models
{
    [Table("CRIME_WANTED")]
    public class CrimeWantedModel
    {
        [Key]
        [Column("id_crimewanted")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("id_crime")]
        public int? IdCrime { get; set; }
        
        [Column("id_wanted")]
        public int? IdWanted { get; set; }
        
        public virtual CrimesModel? Crimes { get; set; }
     
        [JsonIgnore]
        public virtual WantedModel? Wanted { get; set; }

    }
}
