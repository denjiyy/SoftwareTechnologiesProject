namespace MusicalShop.Data.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using MusicalShop.Data.Models;
    using System.Linq;
    using System.Threading.Tasks;

    public class AdminSeeder : ISeeder
    {
        private readonly UserManager<MusicalShopUser> userManager;

        public AdminSeeder(UserManager<MusicalShopUser> userManager)
        {
            this.userManager = userManager;
        }
        public async Task SeedAsync()
        {
            if (!userManager.Users.Any())
            {
                var user = new MusicalShopUser()
                {
                    Email = "musicalshop@admin.com",
                    UserName = "Admin",
                    LastName = "Admin",
                };
                var result = await userManager.CreateAsync(user, "qwe123QWE!@#");

                if (result.Succeeded)
                {
                    await userManager.AddToRoleAsync(user, "Admin");
                }
            }
        }
    }
}
