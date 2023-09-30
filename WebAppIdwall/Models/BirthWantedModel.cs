using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppIdwall.Models
{
    [Table("BIRTH_WANTED")]
    public class BirthWantedModel
    {
        [Column("id_birthwanted")]
        [Key]
        public int Id { get; set; }

        [Column("id_birth")]
        public int IdBirth { get; set; }

        [Column("id_wanted")]
        public int IdWanted { get; set; }

        public virtual BirthModel Birth { get; set; }

        [JsonIgnore]
        public virtual WantedModel Wanted { get; set; }
    }
}
