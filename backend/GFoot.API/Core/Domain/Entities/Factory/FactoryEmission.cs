
namespace Domain.Entities.Factory
{
    public class FactoryEmission : BaseEntity<string>
    {
        public required string RawMaterials { get; set; }
        public float PurchasedElectricity { get; set; }
        public float FuelCombustion { get; set; }
        public float TransportationEnergyCombustion { get; set; }
        public float WasteEmission { get; set; }
        public DateTime Date { get; set; } = DateTime.UtcNow;
        public float CarbonEmission { get; set; }

        // Navigational Property
        public string? FactoryUserId { get; set; }
        public virtual FactoryUser? Factory { get; set; }
    }
}
