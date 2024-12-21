namespace MusicalShop.App.Installers
{
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using MusicalShop.Services;
    using MusicalShop.Services.Models.Options;

    public class FacebookAuthInstaller
    {
        //Single method for Login and Reg
        public void InstallServices(IServiceCollection services, IConfiguration configuration)
        {
            var facebookAuthSettings = new FacebookAuthSettings();
            //find a section and bind the Json obj
            configuration.Bind(nameof(FacebookAuthSettings), facebookAuthSettings);
            services.AddSingleton(facebookAuthSettings);

            services.AddHttpClient();
        }
    }
}