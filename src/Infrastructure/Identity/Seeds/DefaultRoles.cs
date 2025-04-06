using Infrastructure.Identity.Enums;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Identity.Seeds
{
    public class DefaultRoles
    {
        public static async Task SeedRoles(RoleManager<IdentityRole> roleManager)
        {
            await CreateRoleIfNotExists(roleManager, Roles.Admin.ToString());
            await CreateRoleIfNotExists(roleManager, Roles.Teacher.ToString());
            await CreateRoleIfNotExists(roleManager, Roles.Student.ToString());
        }

        private static async Task CreateRoleIfNotExists(RoleManager<IdentityRole> roleManager, string roleName)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole(roleName));
            }
        }
    }
}
