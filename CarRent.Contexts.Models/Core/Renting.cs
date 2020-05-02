using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Contexts.Models.Core
{
    public class Renting
    {
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public string Id { get; set; }
        public DateTime Rented { get; set; } = DateTime.Now;
        public DateTime PickupDate { get; set; }
        public DateTime ReturnDate { get; set; }
        public State State { get; set; }
        public bool IsOverTime => State == State.Pickedup && ReturnDate > DateTime.Now;

        public string CarId { get; set; }
        public virtual Car Car { get; set; }

        public string UserId { get; set; }
        public virtual User User { get; set; }
    }
}
