using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace WebAppIdwall.Models
{
    [Table("T_CAUTION")]
    public class CautionModel
    {
        [Column("id_caution")]
        [Key]
        public int Id { get; set; }
        
        [Column("str_caution")]
        public string Name { get; set; }
        
        [Column("id_wanted")]
        public int IdWanted { get; set; }

        
        public virtual WantedModel Wanted { get; set; }

    }
}
