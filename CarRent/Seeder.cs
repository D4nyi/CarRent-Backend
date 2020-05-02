using CarRent.Contexts.Models.Core;
using CarRent.Contexts.SQLiteContext;

using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

using System;
using System.Linq;

namespace CarRent
{
    public static class Seeder
    {
        public static IHost Seed(this IHost host)
        {
            using IServiceScope scope = host.Services.CreateScope();
            SQLiteDbContext context = scope.ServiceProvider.GetService<SQLiteDbContext>();

            if (context.Users.Any() || !Program.IsDevelopment)
            {
                return host;
            }

            var hasher = new PasswordHasher<User>();
            var user1 = new User
            {
                FirstName = "Teszt",
                LastName = "Elek",
                UserName = "tesztelek",
                BirthDate = new DateTime(1996, 6, 5),
                Email = "test@example.com",
                Address = "Itthon"
            };
            user1.PasswordHash = hasher.HashPassword(user1, "123456789A!");

            var user2 = new User
            {
                FirstName = "Hans",
                LastName = "Regenkurt",
                UserName = "hans_regenkurt",
                BirthDate = new DateTime(1978, 10, 9),
                Email = "hans@example.com",
                Address = "Otthon"
            };
            user2.PasswordHash = hasher.HashPassword(user2, "123456789A!");

            context.Users.Add(user1);
            context.Users.Add(user2);

            var car1 = new Car
            {
                Brand = "VW",
                Model = "Golf",
                Colour = Colour.TurmericYellow,
                EngineDescription = "1.4 TSI, 122HP, 200Nm",
                LicensePlate = "RPP-469",
                Mileage = 6.5
            };

            var car2 = new Car
            {
                Brand = "BMW",
                Model = "5",
                Colour = Colour.Blue,
                EngineDescription = "2.5 TFSI, 300HP, 600Nm",
                LicensePlate = "FGH-420",
                Mileage = 8.3,
            };

            Premise premise = context.Premises.Add(new Premise
            {
                Address = "Tiszaújváros Iparipark út 4.",
                Name = "Rent a Car"
            }).Entity;

            car1.PremiseId = premise.Id;
            car2.PremiseId = premise.Id;

            context.Cars.Add(car1);
            context.Cars.Add(car2);

            context.SaveChanges();
            return host;
        }
    }
}
