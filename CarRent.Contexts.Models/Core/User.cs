using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Contexts.Models.Core
{
    public class User : IdentityUser
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get => base.Id; set => base.Id = value; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public string CarId { get; set; }
        public Car RentedCar { get; set; }
    }
}
