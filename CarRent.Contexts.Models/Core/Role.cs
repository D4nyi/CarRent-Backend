using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarRent.Contexts.Models.Core
{
    public class Role : IdentityRole
    {
        public Role() : base() { }

        public Role(string roleName) : base(roleName) { }

        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public override string Id { get => base.Id; set => base.Id = value; }
    }
}
