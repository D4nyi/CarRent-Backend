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
        public string ImagePath { get; set; }
        public bool Rented { get; set; }
        public string PremiseId { get; set; }

        public static implicit operator Car(DetailDto dto) => new Car
        {
            Id = dto.Id,
            Brand = dto.Brand,
            Model = dto.Model,
            Colour = dto.Colour,
            LicensePlate = dto.LicensePlate,
            EngineDescription = dto.EngineDescription,
            Mileage = dto.Mileage,
            ImagePath = dto.ImagePath,
            PremiseId = dto.PremiseId
        };

        public static explicit operator DetailDto(Car car)=> new DetailDto
        {
            Id = car.Id,
            Brand = car.Brand,
            Model = car.Model,
            Colour = car.Colour,
            LicensePlate = car.LicensePlate,
            EngineDescription = car.EngineDescription,
            Mileage = car.Mileage,
            ImagePath = car.ImagePath,
            PremiseName = car.Premise?.Name + ", " + car.Premise?.Address,
            Rented = car.RentingId != null,
            PremiseId = car.PremiseId
        };
    }
}
