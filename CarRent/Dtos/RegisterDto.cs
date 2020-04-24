using System;

namespace CarRent.Dtos
{
    public class RegisterDto
    {
        public string Email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string FullName => FirstName + " " + LastName;
        public string UserName { get; set; }
        public DateTime BirthDate { get; set; }
        public int Age
        {
            get
            {
                DateTime today = DateTime.Today;
                int age = today.Year - BirthDate.Year;
                return BirthDate.Date > today.AddYears(-age) ? age-- : age;
            }
        }
        public string Address { get; set; }
        public string Password { get; set; }
    }
}
