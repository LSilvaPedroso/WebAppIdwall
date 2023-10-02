using Newtonsoft.Json.Serialization;
using Newtonsoft.Json;
using System.Reflection;

namespace WebAppIdwall.Views
{
    public class JsonIgnore
    {
    }

    public class IgnoreOccupationsContractResolver : DefaultContractResolver
    {
        protected override JsonProperty CreateProperty(MemberInfo member, MemberSerialization memberSerialization)
        {
            var property = base.CreateProperty(member, memberSerialization);

            // Verifique se o nome da propriedade é "Occupations" e a ignore
            if (property.PropertyName == "Occupations")
            {
                property.ShouldSerialize = _ => false;
            }

            return property;
        }
    }
}
