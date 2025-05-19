
using System.Text.Json.Serialization;

namespace Shared.FactoryModels
{
    public class OrganizationRecommendationsResponseML
    {
        [JsonPropertyName("Organization Recommendations")]
        public Dictionary<string, string> OrganizationRecommendations { get; set; } = new();

    }
}
