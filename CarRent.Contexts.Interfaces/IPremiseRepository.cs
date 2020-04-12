using CarRent.Contexts.Models.Core;

namespace CarRent.Contexts.Interfaces
{
    public interface IPremiseRepository : IRepository<Premise>
    {
        bool HasCar(Car car);
        Premise FindByName(string premiseName);
    }
}
