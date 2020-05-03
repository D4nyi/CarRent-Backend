using CarRent.Contexts.Models.Core;

namespace CarRent.Dtos
{
    public class DetailDto
    {
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Colour Colour { get; set; }
        public string LicensePlate { get; set; }
        public string EngineDescription { get; set; }
        public double Mileage { get; set; }
        public string PremiseName { get; set; }
        public bool Rented { get; set; }
    }
}
