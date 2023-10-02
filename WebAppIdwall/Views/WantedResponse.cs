using WebAppIdwall.Models;

namespace WebAppIdwall.Views
{
    public class WantedResponse
    {
        public int Id { get; set; }
        public string IdSource { get; set; }
        public string Name { get; set; }
        public decimal Height { get; set; }
        public decimal Weight { get; set; }
        public string Hair { get; set; }
        public string EyesColor { get; set; }
        public string Nacionalidade { get; set; }
        public string Remarks { get; set; }
        public string Sexo { get; set; }
        public string LocalNascimento { get; set; }
        public decimal Recompensa { get; set; }
        public List<CautionModel> Cautions { get; set; }
        public List<CrimesModel> Crimes { get; set; }
        public List<BirthModel> Birth { get; set; }
    }
}
