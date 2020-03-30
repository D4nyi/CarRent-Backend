
namespace CarRent.Contexts.Models.DAL
{
    public class User : IdentityUser
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address { get; set; }

        public Car RentedCar { get; set; }
    }
}
