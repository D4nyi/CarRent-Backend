﻿using CarRent.Contexts.Interfaces;
using CarRent.Contexts.Models.Core;

using Microsoft.EntityFrameworkCore;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;

namespace CarRent.Contexts.SQLiteContext
{
    public class CarRepositroy : Repository<Car>, ICarRepository
    {
        public CarRepositroy(SQLiteDbContext context) : base(context)
        {
        }

        public bool CancellRent(string rentId = null, string userId = null)
        {
            bool rent = String.IsNullOrWhiteSpace(rentId);
            bool user = String.IsNullOrWhiteSpace(userId);
            if (rent && user)
            {
                throw new ArgumentNullException($"{nameof(rentId)} {nameof(userId)}", "Must provide at least one argument! Every argument is null!");
            }

            DbSet<Renting> rentings = _context.Rentings;
            Renting renting = null;
            Expression<Func<Renting, bool>> predicate = null;

            if (rent)
            {
                predicate = x => x.UserId == userId;
            }
            else if (user)
            {
                predicate = x => x.Id == rentId;
            }
            else
            {
                throw new ArgumentException("Not enough information is provided to do a query!");
            }

            if (predicate != null)
            {
                renting = rentings.FirstOrDefault(predicate);
                if (renting != null)
                {
                    return rentings.Remove(renting) != null;
                }
            }

            return false;
        }

        public bool CarExists(Car car)
        {
            if (car is null)
            {
                throw new ArgumentNullException(nameof(car), "Argument 'car' cannot be null!");
            }

            return _set
                .Any(x =>
                x.Brand == car.Brand &&
                x.Model == car.Model &&
                x.Colour == car.Colour &&
                x.LicensePlate == car.LicensePlate);
        }

        public Car FindAndLoadPremise(string id)
        {
            return _set
                .Include(c => c.Premise)
                .FirstOrDefault(c => c.Id == id);
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
                .Where(x => x.Mileage >= greaterThen && x.Mileage <= smallerThen)
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

        public List<Car> GetAll(bool loadPremise)
        {
            IQueryable<Car> query = _set.AsQueryable();

            if (loadPremise)
            {
                query = query.Include(i => i.Premise);
            }

            return query.ToList();
        }

        public Car GetCarByRentingId(string rentingId)
        {
            return _set
                .Include(i => i.Premise)
                .FirstOrDefault(c => c.RentingId == rentingId);
        }

        public Renting RentCar(Car car, User user, DateTime start, DateTime end)
        {
            var limit = new DateTime(2020, 1, 1);
            DateTime up = start.AddDays(7).AddMinutes(1);
            if (car is null || user is null || start < limit || end > up)
            {
                throw new ArgumentException("Either of the arguments are invalid!");
            }

            var renting = new Renting
            {
                CarId = car.Id,
                Car = car,
                User = user,
                UserId = user.Id,
                Rented = DateTime.Now,
                PickupDate = start,
                ReturnDate = end,
                State = State.Rented
            };

            return _context.Set<Renting>().Add(renting).Entity;
        }

        public Renting RentCar(string carId, string userId, DateTime start, DateTime end)
        {
            if (String.IsNullOrWhiteSpace(carId) || String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("Arguments are invalid!", $"{nameof(carId)} {nameof(userId)}");
            }

            Car car = _set.Find(carId);
            User user = _context.Set<User>().Find(userId);

            return RentCar(car, user, start, end);
        }

        public bool RentingExists(Car car, User user)
        {
            if (car is null || user is null)
            {
                throw new ArgumentException("Either of the arguments are invalid!");
            }

            return RentingExists(car.Id, user.Id);
        }

        public bool RentingExists(string carId, string userId)
        {
            if (String.IsNullOrWhiteSpace(carId) || String.IsNullOrWhiteSpace(userId))
            {
                throw new ArgumentException("Arguments are invalid!", $"{nameof(carId)} {nameof(userId)}");
            }

            return _context.Set<Renting>().Any(x => x.UserId == userId && x.CarId == carId);
        }
    }
}
