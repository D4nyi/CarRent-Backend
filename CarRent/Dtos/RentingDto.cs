using System;

namespace CarRent.Dtos
{
    public class RentingDto
    {
        public string CarId { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }

        public string Email { get; set; }
        public string Password { get; set; }
    }
}
