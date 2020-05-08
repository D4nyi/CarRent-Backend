using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Contexts.Models.Core
{
    public class Car
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public Colour Colour { get; set; }
        public string LicensePlate { get; set; }
        public string EngineDescription { get; set; }
        public double Mileage { get; set; }
        public string ImagePath { get; set; }

        public string PremiseId { get; set; }
        public virtual Premise Premise { get; set; }

        public string RentingId { get; set; }
        public virtual Renting Renting { get; set; }
    }
}
