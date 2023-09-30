using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIdwall.Models
{
    [Table("T_BIRTH")]
    public class BirthModel
    {
        [Column("id_birth")]
        [Key]
        public int Id { get; set; }

        [Column("dt_birth")]
        public DateTime Birth { get; set; }
        public virtual ICollection<BirthWantedModel> BirthWanted { get; set; }

    }
}
