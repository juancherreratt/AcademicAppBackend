using Core.Domain.Entities;
using Core.Domain.Interfaces;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Models
{
    public class AppUser : IdentityUser, IUser
    {
        public Student? Student { get; set; }
        public Teacher? Teacher { get; set; }
    }
}
