using CarRent.Contexts.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace CarRent.Contexts.Interfaces
{
    public interface ICarRepository : IRepository<Car>
    {
        Car FindByLicensePlate(string plateNo);
        List<Car> FindByBrand(string brandName);
        List<Car> FindByModel(string modelName);
        List<Car> FindByMilage(double greaterThen = 0, double smallerThen = 0);
        List<Car> FindByMilage(Expression<Func<Car, bool>> predicate);
    }
}
