using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using SP23.P02.Web.Features.Roles;
using SP23.P02.Web.Features.Users;

namespace SP23.P02.Web.Data
{
    public class Seed
    {
        public static async Task Initialize(IServiceProvider services)
        {
            var context = services.GetRequiredService<DataContext>();
            await context.Database.MigrateAsync();


           
            await AddRoles(services);
            await AddUsers(services);
        }
        private static async Task AddUsers(IServiceProvider services)
        {
            const string defaultPassword = "Password123!";

            var userManager = services.GetRequiredService<UserManager<User>>();
            if (userManager.Users.Any())
            {
                return;
            }

            var adminUser = new User
            {
                UserName = "galkadi"
            };
            await userManager.CreateAsync(adminUser, defaultPassword);
            await userManager.AddToRoleAsync(adminUser, RoleNames.Admin);

            var bobUser = new User
            {
                UserName = "bob"
            };
            await userManager.CreateAsync(bobUser, defaultPassword);
            await userManager.AddToRoleAsync(bobUser, RoleNames.User);

            var sueUser = new User
            {
                UserName = "sue"
            };
            await userManager.CreateAsync(sueUser, defaultPassword);
            await userManager.AddToRoleAsync(sueUser, RoleNames.User);
        }

        private static async Task AddRoles(IServiceProvider services)
        {
            var roleManager = services.GetRequiredService<RoleManager<Role>>();
            if (roleManager.Roles.Any())
            {
                return;
            }

            await roleManager.CreateAsync(new Role
            {
                Name = RoleNames.Admin
            });

            await roleManager.CreateAsync(new Role
            {
                Name = RoleNames.User
            });
        }
    }
}
