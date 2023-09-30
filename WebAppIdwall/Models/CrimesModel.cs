using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppIdwall.Models
{
    [Table("T_CRIMES")]
    public class CrimesModel
    {
        [Key]
        [Column("id_crime")]
        public int Id { get; set; }
        
        [Column("nm_crime")]
        public string Name { get; set; }
        
        [Column("ds_crime")]
        public string Description { get; set; }
        
        [JsonIgnore]
        public virtual ICollection<CrimeWantedModel> CrimeWanted { get; set; }


    }
}
