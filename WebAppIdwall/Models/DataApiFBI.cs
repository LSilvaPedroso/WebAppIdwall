using Newtonsoft.Json;
using System.Net;
using System.Text.Json.Serialization;

namespace WebAppIdwall.Models
{
    public class RootObject
    {
        public int Total { get; set; }
        public int Page { get; set; }
        public List<DataApiFBI> Items { get; set; }
    }

    public class DataApiFBI
    {
        public string? Eyes { get; set; }

        public string? Caution { get; set; }
        public List<string>? LegatNames { get; set; }

        [JsonProperty("reward_min")]
        public decimal? RewardMin { get; set; }

        public List<string>? Aliases { get; set; }
        public List<string>? FieldOffices { get; set; }
        public string? Ncic { get; set; }

        [JsonProperty("weight_min")]
        public decimal? WeightMin { get; set; }
        public string? Complexion { get; set; }
        public string? AgeRange { get; set; }
        public List<Image>? Images { get; set; }
        public List<File>? Files { get; set; }
        public string? WarningMessage { get; set; }
        public string? Remarks { get; set; }
        public string? Details { get; set; }
        public string? Sex { get; set; }
        public string? AdditionalInformation { get; set; }
        public string? RewardText { get; set; }

        [JsonProperty("dates_of_birth_used")]
        public List<string>? DatesOfBirthUsed { get; set; }
        public string? PlaceOfBirth { get; set; }
        public List<string>? Locations { get; set; }

        [JsonProperty("poster_classification")]
        public string? PosterClassification { get; set; }

        public List<string>? PossibleStates { get; set; }

        [JsonProperty("height_min")]
        public decimal? HeightMin { get; set; }

        public string? Title { get; set; }
        public string? ScarsAndMarks { get; set; }

        [JsonProperty("age_max")]
        public int? AgeMax { get; set; }

        [JsonProperty("race_raw")]
        public string? RaceRaw { get; set; }

        public string? Modified { get; set; }
        public string? Race { get; set; }
        public string? Publication { get; set; }

        [JsonProperty("weight_max")]
        public decimal? WeightMax { get; set; }

        [JsonProperty("age_min")]
        public int? AgeMin { get; set; }

        public string? Uid { get; set; }
        public string? Nationality { get; set; }
        public string? Url { get; set; }

        public string? Hair { get; set; }

        [JsonProperty("height_max")]
        public decimal? HeightMax { get; set; }

        [JsonProperty("reward_max")]
        public decimal? RewardMax { get; set; }

        public List<string>? Subjects { get; set; }

        [JsonProperty("path")]
        public string? Path { get; set; }
    }


    public class Image
    {
        public string Caption { get; set; }
        public string Original { get; set; }
        public string Large { get; set; }
        public string Thumb { get; set; }
    }

    public class File
    {
        public string Url { get; set; }
        public string Name { get; set; }
    }

}
