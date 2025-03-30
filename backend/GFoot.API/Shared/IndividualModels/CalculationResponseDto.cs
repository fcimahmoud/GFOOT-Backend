
using System.Text.Json.Serialization;

namespace Shared.IndividualModels
{
    public class CalculationResponseDto
    {
        [JsonPropertyName("carbon_emission")]
        public float CarbonEmission { get; set; }
    }
}
