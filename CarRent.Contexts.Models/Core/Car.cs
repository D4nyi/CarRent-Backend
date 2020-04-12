namespace CarRent.Contexts.Models.Core
{
    public class Car
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Colour Colour { get; set; }
        public string LicensePlate { get; set; }
        public string EngineDescription { get; set; }
        public double Mileage { get; set; }

        public string PremiseId { get; set; }
        public Premise Premise { get; set; }

        public string TenantId { get; set; }
        public User Tenant { get; set; }
    }
}
