using System.Collections.Generic;

namespace CarRent.Contexts.Models.Core
{
    public class Premise
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }
        public List<Car> Cars { get; set; }
    }
}
