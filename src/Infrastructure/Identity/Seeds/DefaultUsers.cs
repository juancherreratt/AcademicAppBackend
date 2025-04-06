using Infrastructure.Identity.Enums;
using Infrastructure.Identity.Models;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seeds
{
    public class DefaultUsers
    {
        public static async Task SeedUsers(UserManager<AppUser> userManager)
        {
            #region defaultUser1
            var defaultUser1 = new AppUser
            {
                UserName = "admin",
                Email = "admin@admin.com",
                EmailConfirmed = true,
                PhoneNumberConfirmed = true
            };

            if (userManager.Users.All(u => u.Id != defaultUser1.Id))
            {
                var user = await userManager.FindByEmailAsync(defaultUser1.Email);
                if (user == null)
                {
                    await userManager.CreateAsync(defaultUser1, "Administrador2025@");
                    await userManager.AddToRoleAsync(defaultUser1, Roles.Admin.ToString());
                }
            }
            #endregion
        }
    }
}