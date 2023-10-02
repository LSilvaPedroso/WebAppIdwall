namespace WebAppIdwall.Models
{
    public class EntityData
    {
        public string date_of_birth { get; set; }
        public string distinguishing_marks { get; set; }
        public string weight { get; set; }
        public List<string> nationalities { get; set; }
        public string entity_id { get; set; }
        public List<string> eyes_colors_id { get; set; }
        public string sex_id { get; set; }
        public string place_of_birth { get; set; }
        public string forename { get; set; }
        public List<ArrestWarrant> arrest_warrants { get; set; }
        public string country_of_birth_id { get; set; }
        public List<string> hairs_id { get; set; }
        public string name { get; set; }
        public List<string> languages_spoken_ids { get; set; }
        public string height { get; set; }
        public List<string> images { get; set; }
        public List<string> thumbnail { get; set; }
    }

    public class ArrestWarrant
    {
        public string charge { get; set; }
        public string issuing_country_id { get; set; }
        public string charge_translation { get; set; }
    }

    public class EntityEmbedded
    {
        public List<Link> links { get; set; }
    }

    public class Link
    {
        public string href { get; set; }
    }

    public class EntityDataLinks
    {
        public SelfLink self { get; set; }
        public SelfLink images { get; set; }
        public SelfLink thumbnail { get; set; }
    }

    public class SelfLink
    {
        public string href { get; set; }
    }

}
