using Microsoft.AspNetCore.Identity;

namespace CarRent.Contexts.Models.Core
{
    public class User : IdentityUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public Car RentedCar { get; set; }
    }
}
