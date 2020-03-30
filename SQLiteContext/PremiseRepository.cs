using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CarRent.Contexts.SQLiteContext
{
    public class PremiseRepository : Repository<Premise>, IPremiseRepository
    {
        public PremiseRepository(SQLiteDbContext context) : base(context)
        {
        }

        public Premise FindByName(string premiseName)
        {
            return _set.FirstOrDefault(f => f.Name == premiseName);
        }

        public bool HasCar(Car car)
        {
            return _set.Any(x => x.Id == car.Id);
        }
    }
}
