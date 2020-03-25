namespace CarRent.Models.DAL
{
    public class User
    {
        public string Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Address { get; set; }

        public Car RentedCar { get; set; }
    }
}
