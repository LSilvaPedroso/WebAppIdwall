namespace WebAppIdwall.Models
{
    public class InterpolResponse
    {
        public int total { get; set; }
        public Query query { get; set; }
        public Embedded _embedded { get; set; }
        public Links _links { get; set; }
    }

    public class Query
    {
        public int page { get; set; }
        public int resultPerPage { get; set; }
    }

    public class Embedded
    {
        public List<Notice> notices { get; set; }
    }

    public class Notice
    {
        public string date_of_birth { get; set; }
        public List<string> nationalities { get; set; }
        public string entity_id { get; set; }
        public string forename { get; set; }
        public string name { get; set; }
        public Links _links { get; set; }
    }

    public class Links
    {
        public Self self { get; set; }
        public Self first { get; set; }
        public Self next { get; set; }
        public Self last { get; set; }
    }

    public class Self
    {
        public string href { get; set; }
    }

}
