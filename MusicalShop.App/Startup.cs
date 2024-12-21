namespace MusicalShop.App
{
    using CloudinaryDotNet;
    using global::Stripe;
    using Microsoft.AspNetCore.Builder;
    using Microsoft.AspNetCore.Hosting;
    using Microsoft.AspNetCore.Identity;
    using Microsoft.EntityFrameworkCore;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using Microsoft.Extensions.Hosting;
    using MusicalShop.App.Extensions;
    using MusicalShop.Data;
    using MusicalShop.Data.Models;
    using MusicalShop.Data.Seeders;
    using MusicalShop.Services;
    using MusicalShop.Services.Mapping;
    using MusicalShop.Services.Models;
    using MusicalShop.Web.InputModels.Product;
    using MusicalShop.Web.ViewModels;
    using Stripe;
    using System.Globalization;
    using Microsoft.AspNetCore.Authentication.Cookies;

    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        public void ConfigureServices(IServiceCollection services)
        {
            services.AddDbContext<MusicalShopDbContext>(options =>
                options.UseSqlServer(
                    Configuration.GetConnectionString("DefaultConnection")));

            services.AddIdentity<MusicalShopUser, IdentityRole>(options => options.User.RequireUniqueEmail = true)
                .AddEntityFrameworkStores<MusicalShopDbContext>()
                .AddDefaultTokenProviders();

            var cloudinaryCredentials = new CloudinaryDotNet.Account(
               Configuration["Cloudinary:CloudName"],
               Configuration["Cloudinary:ApiKey"],
               Configuration["Cloudinary:ApiSecret"]);

            var cloudinaryUtility = new Cloudinary(cloudinaryCredentials);
            services.AddSingleton(cloudinaryUtility);

           /* services.AddAuthentication(opts =>
            {
                opts.DefaultScheme = CookieAuthenticationDefaults.AuthenticationScheme;
            })
                
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                });*/

            services.AddResponseCompression(options =>
            {
                options.EnableForHttps = true;
            });

            services.Configure<StripeSettings>(Configuration.GetSection("Stripe"));

            services.AddTransient<AdminSeeder>();
            services.AddTransient<ProductTypeSeeder>();
            services.AddTransient<BrandSeeder>();
            services.AddTransient<RoleSeeder>();
            services.AddTransient<OrderStatusesSeeder>();
            services.AddTransient<ProductSeeder>();
            services.AddTransient<IProductService, Services.ProductService>();
            services.AddTransient<ICloudinaryService, CloudinaryService>();
            services.AddTransient<IOrderService, Services.OrderService>();

            //services.AddSingleton<IEmailSender, EmailSender>();

            services.AddRazorPages();
            services.AddControllersWithViews();
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            StripeConfiguration.ApiKey = Configuration.GetSection("Stripe")["SecretKey"];

            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseDatabaseErrorPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            CultureInfo.DefaultThreadCurrentCulture = CultureInfo.InvariantCulture;
            CultureInfo.DefaultThreadCurrentUICulture = CultureInfo.InvariantCulture;

            AutoMapperConfig.RegisterMappings(typeof(ProductCreateInputModel).Assembly,
                typeof(ProductTypeServiceModel).Assembly,
                typeof(ProductTypeViewModel).Assembly);

            app.UseRouting();

            app.UseHttpsRedirection();
            app.UseStaticFiles();
            app.UseResponseCompression();
            app.UseAuthentication();
            app.UseAuthorization();
            app.UseDatabaseSeeding();

            app.UseEndpoints(endpoints =>
            {

                endpoints.MapControllers();

                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");

                endpoints.MapControllerRoute(
                    name: "areas",
                    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");
                endpoints.MapRazorPages();
            });
        }
    }
}