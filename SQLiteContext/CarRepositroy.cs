using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;

namespace CarRent.Contexts.SQLiteContext
{
    public class CarRepositroy : Repository<Car>, ICarRepository
    {
        public CarRepositroy(SQLiteDbContext context) : base(context)
        {
        }

        public List<Car> FindByBrand(string brandName)
        {
            return _set
                .Where(x => x.Brand == brandName)
                .ToList();
        }

        public Car FindByLicensePlate(string plateNo)
        {
            return _set.FirstOrDefault(x => x.LicensePlate == plateNo);
        }

        public List<Car> FindByMilage(double greaterThen = 0, double smallerThen = 1000)
        {
            return _set
                .Where(x=> x.Mileage >= greaterThen && x.Mileage <= smallerThen)
                .ToList();
        }

        public List<Car> FindByMilage(Expression<Func<Car, bool>> predicate)
        {
            return _set
                .Where(predicate)
                .ToList();
        }

        public List<Car> FindByModel(string modelName)
        {
            return _set
                .Where(x => x.Model == modelName)
                .ToList();
        }
    }
}
