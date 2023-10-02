using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WebAppIdwall.Models
{
    [Table("T_WANTED")]
    public class WantedModel
    {
        [Key]
        [Column("id_wanted")]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int? Id { get; set; }

        [Column("cd_souce")]
        public string? IdSource { get; set; }

        [Column("str_name")]
        public string? Name { get; set; }

        [NotMapped]
        public decimal? _height { get; set; }

        [NotMapped]
        public decimal? _weight { get; set; }

        [Column("str_hair")]
        public string? Hair { get; set; }

        [Column("str_eyesColor")]
        public string? EyesColor { get; set; }

        [Column("str_nacionality")]
        public string? Nacionalidade { get; set; }

        [Column("str_remarks")]
        public string? Remarks { get; set; }

        [Column("str_sex")]
        public string? Sexo { get; set; }

        [Column("str_placeBirth")]
        public string? LocalNascimento { get; set; }

        [NotMapped]
        public decimal? _recompensa { get; set; }

        [Column("nr_height")]
        public decimal? Height
        {
            get { return _height; }
            set
            {
                if (value >= 0 && value <= 9999999.99m) // Defina os limites conforme necessário
                {
                    _height = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Height deve estar dentro do intervalo permitido.");
                }
            }
        }

        [Column("nr_weight")]
        public decimal? Weight
        {
            get { return _weight; }
            set
            {
                if (value >= 0 && value <= 9999999.99m) // Defina os limites conforme necessário
                {
                    _weight = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Weight deve estar dentro do intervalo permitido.");
                }
            }
        }

        [Column("nr_recompensa")]
        public decimal? Recompensa
        {
            get { return _recompensa; }
            set
            {
                if (value >= 0 && value <= 9999999.99m) // Defina os limites conforme necessário
                {
                    _recompensa = value;
                }
                else
                {
                    throw new ArgumentOutOfRangeException("Recompensa deve estar dentro do intervalo permitido.");
                }
            }
        }
        public virtual ICollection<CautionModel>? Cautions { get; set; }

        public virtual ICollection<CrimeWantedModel>? CrimeWanted { get; set; }

        public virtual ICollection<BirthWantedModel>? BirthWanted { get; set; }

    }
}
