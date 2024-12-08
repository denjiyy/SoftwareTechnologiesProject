namespace MusicalShop.Data.Seeders
{
    using Microsoft.AspNetCore.Identity;
    using System.Linq;
    using System.Threading.Tasks;

    public class RoleSeeder : ISeeder
    {
        private readonly MusicalShopDbContext context;

        public RoleSeeder(MusicalShopDbContext context)
        {
            this.context = context;
        }
        public async Task SeedAsync()
        {
            if (!context.Roles.Any())
            {
                await context.Roles.AddAsync(new IdentityRole
                {
                    Name = "Admin",
                    NormalizedName = "Admin"
                });

                await context.Roles.AddAsync(new IdentityRole
                {
                    Name = "User",
                    NormalizedName = "User"
                });
                await context.SaveChangesAsync();
            }
        }
    }
}
