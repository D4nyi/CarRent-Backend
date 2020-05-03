using CarRent.Contexts.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarRent.Contexts.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Car FindByLicensePlate(string plateNo);
        Renting RentCar(Car car, User user, DateTime start, DateTime end);
        Renting RentCar(string carId, string userId, DateTime start, DateTime end);
        Car GetCarByRentingId(string rentingId);
        bool RentingExists(Car car, User user);
        bool RentingExists(string carId, string userId);
        bool CancellRent(string rentId = null, string userId = null);
        Car FindAndLoadPremise(string id);
        List<Car> FindByBrand(string brandName);
        List<Car> FindByModel(string modelName);
        List<Car> FindByMilage(double greaterThen = 0, double smallerThen = 0);
        List<Car> FindByMilage(Expression<Func<Car, bool>> predicate);
    }
}
