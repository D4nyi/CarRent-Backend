using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Contexts.Models.Core
{
    public class Premise
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public string Name { get; set; }
        public string Address { get; set; }

        public List<Car> Cars { get; set; }
    }
}
