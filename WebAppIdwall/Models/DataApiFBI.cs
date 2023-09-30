using System.Net;

namespace WebAppIdwall.Models
{
    public class RootObject
    {
        public List<DataApiFBI> Items { get; set; }
    }

    public class DataApiFBI
    {
        public string Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Details { get; set; }
        public string Remarks { get; set; }
        public string RewardText { get; set; }
        public decimal RewardMin { get; set; }
        public decimal RewardMax { get; set; }
        public string PlaceOfBirth { get; set; }
        public string Status { get; set; }
        public string PersonClassification { get; set; }
        public string PosterClassification { get; set; }
        public int AgeMin { get; set; }
        public int AgeMax { get; set; }
        public decimal WeightMin { get; set; }
        public decimal WeightMax { get; set; }
        public decimal HeightMin { get; set; }
        public decimal HeightMax { get; set; }
        public string Eyes { get; set; }
        public string Hair { get; set; }
        public string Sex { get; set; }
        public string Race { get; set; }
        public string Nationality { get; set; }
        public string ScarsAndMarks { get; set; }
        public string Complexion { get; set; }
        public string Occupations { get; set; }
        public List<string> PossibleCountries { get; set; }
        public List<string> PossibleStates { get; set; }
        public string Modified { get; set; }
        public string Publication { get; set; }
        public string Path { get; set; }
    }

}
