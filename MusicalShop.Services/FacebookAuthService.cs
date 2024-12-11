namespace MusicalShop.Services
{
    using MusicalShop.Services.Models.Extarnal;
    using MusicalShop.Services.Models.Options;
    using Newtonsoft.Json;
    using System.Net.Http;
    using System.Threading.Tasks;

    public class FacebookAuthService : IFacebookAuthService
    {
        private const string TokenValidationURl = "https://graph.facebook.com/debug_token?input_token={0}&access_token={1}|{2}";
        private const string UserInfoUrl = "https://graph.facebook.com/me?fields=first_name,last_name,picture,email&access_token={0}";
        private readonly FacebookAuthSettings facebookAuthSettings;
        private readonly IHttpClientFactory httpClientFactory;
        public FacebookAuthService(FacebookAuthSettings facebookAuthSettings,
            IHttpClientFactory httpClientFactory)
        {
            this.facebookAuthSettings = facebookAuthSettings;
            this.httpClientFactory = httpClientFactory;
        }
        public async Task<FacebookUserInfoResult> GetUserInfoAsync(string accessToken)
        {
            var formattedurl = string.Format(UserInfoUrl, accessToken);

            var result = await httpClientFactory.CreateClient().GetAsync(formattedurl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookUserInfoResult>(responseAsString);
        }

        public async Task<FacebookTokenValidationResult> ValidateAccessTokenAsync(string accessToken)
        {
            var formattedurl = string.Format(TokenValidationURl, accessToken, facebookAuthSettings.AppId, facebookAuthSettings.AppSecret);

            var result = await httpClientFactory.CreateClient().GetAsync(formattedurl);
            result.EnsureSuccessStatusCode();
            var responseAsString = await result.Content.ReadAsStringAsync();
            return JsonConvert.DeserializeObject<FacebookTokenValidationResult>(responseAsString);
        }
    }
}