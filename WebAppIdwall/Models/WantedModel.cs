using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIdwall.Models
{
    [Table("T_WANTED")]
    public class WantedModel
    {
        [Key]
        [Column("id_wanted")]
        public int Id { get; set; }
        
        [Column("str_name")]
        public string Name { get; set; }
        
        [Column("nr_height")]
        public decimal Height { get; set; }
        
        [Column("nr_weight")]
        public decimal Weight { get; set; }
        
        [Column("str_hair")]
        public string Hair { get; set; }
        
        [Column("str_eyesColor")]
        public string EyesColor { get; set; }

        [Column("str_nacionality")]
        public string Nacionalidade { get; set; }
        
        [Column("str_remarks")]
        public string Remarks { get; set; }

        [Column("str_sex")]
        public string Sexo { get; set; }
                
        [Column("str_placeBirth")]
        public string LocalNascimento { get; set; }
        
        [Column("nr_recompensa")]
        public decimal Recompensa { get; set; }

        public virtual ICollection<CautionModel> Cautions { get; set; }

        public virtual ICollection<CrimeWantedModel> CrimeWanted { get; set; }

        public virtual ICollection<BirthWantedModel> BirthWanted { get; set; }

    }
}
